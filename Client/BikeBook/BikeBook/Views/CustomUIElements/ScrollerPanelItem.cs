using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{


    /**
     * A layout containing an image with a colored box below containing a title and subtitle
     */
    public class ScrollerPanelItem : ContentView
    {
        private ImageSerializer m_imageSerializer;

        private StackLayout m_mainLayout;
        private StackLayout m_titleLayout;

        private Image m_mainImage;
        private Label m_mainTitle;
        private Label m_subTitle;

        public string Title
        {
            set { m_mainTitle.Text = value; }
            get { return m_mainTitle.Text; }
        }

        public string Subtitle
        {
            set { m_subTitle.Text = value; }
            get { return m_subTitle.Text; }
        }

        public ImageSource ImageSource
        {
            set { m_mainImage.Source = value; }
        }


        /**
         * Class Constructor
         */
        public ScrollerPanelItem()
        {
            GuiLayout();
        }

        /**
         * Class Constructor
         */
        public ScrollerPanelItem(User user)
        {
            GuiLayout();

            m_imageSerializer = new ImageSerializer();
            Title = user.name;
            Subtitle = "Joined " + UIHelpers.ToAgeString(user.created_at);
            ImageSource LoadedImageSource = m_imageSerializer.GetProfilePicture(user.email);
            if (LoadedImageSource != null)
                ImageSource = LoadedImageSource;
            m_mainImage.AddSingleTapHandler(NavigateProfile(user));
        }

        private EventHandler NavigateProfile(User user)
        {
            return (sender, e) => { Navigation.PushAsync(new ProfileMain(user.email)); };
        }


        /**
         * Lays out widget's UI
         */
        private void GuiLayout()
        {
            m_mainImage = new Image()
            {
                Source = UIImages.ADDUSERIMAGE,
                WidthRequest = UISizes.SCROLLER_PANEL_WIDTH,
                HeightRequest = UISizes.SCROLLER_PANEL_WIDTH,
            };

            m_mainTitle = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
            };

            m_subTitle = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };

            m_titleLayout = new StackLayout()
            {
                Margin = UISizes.MARGIN_NARROW,
                Children =
                {
                    m_mainTitle,
                    m_subTitle,
                },
            };

            m_mainLayout = new StackLayout
            {
                HeightRequest = UISizes.SCROLLER_PANEL_HEIGHT,
                WidthRequest = UISizes.SCROLLER_PANEL_WIDTH,
                Children =
                {
                    m_mainImage,
                    m_titleLayout,
                },
            };

            HeightRequest = UISizes.SCROLLER_PANEL_HEIGHT;
            WidthRequest = UISizes.SCROLLER_PANEL_WIDTH;
            BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT);
            Content = m_mainLayout;
        }
    }
}