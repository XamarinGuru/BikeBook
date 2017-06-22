using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using ClientWebService;
using ImageCircle.Forms.Plugin.Abstractions;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class CommentTreeHeader : ContentView
    {

        private ImageSerializer m_imageSerializer;
        private ConvertDate m_dateConverter;

        private StackLayout m_mainLayout;

        // nested layout for headings (content title, content age)
        private StackLayout m_headingLayout;
        private Label m_contentTitle;
        private Label m_contentAge;

        private Label m_contentText;
        private ExpandingImageViewer m_contentImage;

        private Button m_likeButton;

        
        /**
         * Class Constructor
         */
        public CommentTreeHeader()
        {
            InitServices();
            GuiLayout();
            PopulatePlaceholder();
        }


        /**
         * Class Constructor
         */
        public CommentTreeHeader(Post post)
        {
            InitServices();
            GuiLayout();
            PopulateContent(post);
        }


        /**
         *  Initializes helper classes
         */
        private void InitServices()
        {
            m_imageSerializer = new ImageSerializer();
            m_dateConverter = new ConvertDate();
        }


        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_contentTitle = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            m_contentAge = new Label()
            {
                Style = (Style) Application.Current.Resources["LabelSecondaryStyle"],
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.End,
            };

            m_headingLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    m_contentTitle,
                    m_contentAge,
                }
            };

            m_contentText = new Label()
            {
                Style = (Style)Application.Current.Resources["LabelMultilineStyle"],
                HeightRequest = UISizes.HIDDEN,
                IsVisible = false,
            };
            m_contentText.PropertyChanged += MakeLabelVisibleIfPopulated;

            m_contentImage = new ExpandingImageViewer();

            m_likeButton = new Button
            {
                Style = (Style)Application.Current.Resources["likeButtonLightUnlikedStyle"],
                Text = "0 Likes",
                HorizontalOptions = LayoutOptions.End,
            };

            m_mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    m_headingLayout,
                    m_contentText,
                    m_contentImage,
                    m_likeButton,
                },
            };

            Content = m_mainLayout;
        }


        /**
         * Inserts Placeholder content
         */
        private void PopulatePlaceholder()
        {
            m_contentTitle.Text = "Post Title!";
            m_contentAge.Text = "2 Days Ago";
            //m_contentText.Text = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam";
            m_contentImage.Source = UIImages.BIKEPLACEHOLDER;
        }


        /**
         *  Populates header from a post
         */
        private void PopulateContent(Post post)
        {
            m_contentTitle.Text = post.owner;
            m_contentAge.Text = m_dateConverter.FromJava(post.created_at).ToAgeString();
            m_contentText.Text = post.content;
            m_contentImage.Source = m_imageSerializer.DeserializeImageToCache(post.picture);
        }


        /**
         * PropertyChangedEventHandler for revealing a label onscreen if text to display is added.
         * 
         * @param object sender - The source of the event
         * @param PropertyChangedEventArgs e - System.PropertyChangedEventArgs indicating which property has been changed
         */
        void MakeLabelVisibleIfPopulated(object sender, PropertyChangedEventArgs e)
        {
            if (sender.GetType() == typeof(Label) && e.PropertyName != null && e.PropertyName.Equals("Text"))
            {
                Label senderLabel = (Label)sender;
                senderLabel.IsVisible = senderLabel.Text != null && senderLabel.Text.Length > 0;
                senderLabel.HeightRequest = senderLabel.IsVisible ? UISizes.SIZE_NONE_SPECIFIED : UISizes.HIDDEN;
            }
        }
    }
}
