using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;
using ClientWebService;

namespace BikeBook.Views
{
    /**
     * General newsfeed homepage
     */
    public class HomeWhatsNew : ContentPage
    {

        private Service m_webService;
        private ImageSelectionDialog m_imageSelector;
        private ImageSerializer m_imageSerializer;

        private GeneralPageTemplate m_mainTemplate;
        private StackLayout m_mainLayout;

        private StackLayout m_addContentLayout;
        private ButtonBar m_addContentBar;
        private ExpandingImageViewer m_contentImage;
        private ExtendedEditor m_addContentEntry;

        private StackLayout m_contentLayout;


        /** 
         * Class constructor
         */
        public HomeWhatsNew()
        {
            InitServices();
            GuiLayout();
            PopulateContent();
        }


        /**
         * Initializes Service classes needed on this page
         */
        private void InitServices()
        {
            m_webService = Service.Instance;
            m_imageSelector = new ImageSelectionDialog();
            m_imageSerializer = new ImageSerializer();
        }



    /** 
     * Lays out and displays page UI
    */
    private void GuiLayout()
        {
            m_addContentBar = new ButtonBar()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,
                ButtonBackGroundColor = Color.FromHex(UIColors.COLOR_NESTED_ACCENT_BACKGROUND),
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                Padding = 0,
                Margin = 0,
            };
            m_addContentBar.AddTextButton("Post", CommitNewsfeedPost);
            m_addContentBar.AddTextButton("Add Photo", AddPhoto);
            //m_addContentBar.AddTextButton("Add Photo/Video", UnderConstruction);
            //m_addContentBar.AddTextButton("Add Destination", UnderConstruction);

            m_contentImage = new ExpandingImageViewer();
            m_contentImage.Closeable = true;

            m_addContentEntry = new ExtendedEditor()
            {
                HorizontalOptions = LayoutOptions.Fill,
                Placeholder = "What's on your mind?",
                BackgroundColor = Color.Transparent,
                PlaceholderColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)),
                Margin = UISizes.MARGIN_STANDARD,
            };

            m_addContentLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = 0,
                Margin = 0,
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),

                Children =
                {
                    m_addContentBar,
                    m_contentImage,
                    m_addContentEntry,
                },
            };

            m_contentLayout = new StackLayout()
            {
                Margin = UISizes.MARGIN_STANDARD,
            };

            m_mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.MARGIN_NONE,
                Children =
                {
                    m_addContentLayout,
                    m_contentLayout,
                },
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_mainLayout,
                Padding = 0,
                Margin = 0,
            };


            Content = m_mainTemplate;
        }

        private void CommitNewsfeedPost(object sender, EventArgs e)
        {
            if(m_addContentEntry.IsPopulated())
            {
                int ResponseCode = m_webService.CreatePost(m_webService.Email, m_imageSerializer.SerializeFromFile(m_contentImage.ContentImagePath), m_addContentEntry.Text);
                if (HttpStatus.CheckStatusCode(ResponseCode))
                {
                    PopulateContent();
                }
                else
                {
                    DisplayAlert("Post could not be created", "Code: " + ResponseCode, "OK");
                }
            }
        }

        private void UnderConstruction(object sender, EventArgs e)
        {
            DisplayAlert("Under Construction", "", "OK");
        }

        private async void AddPhoto(object sender, EventArgs e)
        {
            m_contentImage.ContentImagePath = await m_imageSelector.GetImage();
        }

        /**
         * Inserts Content loaded from server
         */
        private void PopulateContent()
        {
            m_contentLayout.Children.Clear();

            Posts loadedPosts = m_webService.GetPost(m_webService.Email);
            if((loadedPosts.post != null) &&
                (loadedPosts.post.Count > 0))
            { 
                loadedPosts.post.Sort(PostExtensions.CompareByAgeDescending);

                foreach (Post post in loadedPosts.post.GetRange(0, Math.Min(loadedPosts.post.Count, UISizes.WHATS_NEW_MAX_FEED_ITEMS_LOADED)))
                {
                    m_contentLayout.Children.Add(new NewsfeedPostCell(post));
                }
            }
            else
            {
                m_contentLayout.Children.Add(
                    new Label()
                    {
                        Text = "No Posts to Display Yet",
                        TextColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    });
            }
        }
    }
}
