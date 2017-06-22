using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views
{
    /**
     *   Page for expanding an image to be displayed fullscreen.
     *  
     *   Displayed modally and structured as a singleton to prevent accidentally putting a bunch of these on the stack.
     */
    public class ImageExpansionPage : ContentPage
    {

        /**
         *  Returns an eventhandler which will expand the specified image
         */
        public static EventHandler ExpandImage(Image imageToExpand)
        {
            return (sender, e) =>
            {
                if (m_instance == null)
                {
                    m_instance = new ImageExpansionPage(imageToExpand);
                    Application.Current.MainPage.Navigation.PushModalAsync(m_instance);
                }
            };
        }

        /**
         *  Returns an eventhandler which will expand the specified image source
         */
        public static EventHandler ExpandImage(ImageSource imageSourceToExpand)
        {
            return (sender, e) =>
            {
                if (m_instance == null)
                {
                    m_instance = new ImageExpansionPage(imageSourceToExpand);
                    Application.Current.MainPage.Navigation.PushModalAsync(m_instance);
                }
            };
        }        

        private ImageExpansionPage(Image imageToExpand)
        {
            GuiLayout();
            PopulateContent(imageToExpand.Source);
        }

        private ImageExpansionPage(ImageSource imageSourceToExpand)
        {
            GuiLayout();
            PopulateContent(imageSourceToExpand);
        }

        static private ImageExpansionPage m_instance;

        private AbsoluteLayout m_mainLayout;
        private ContentView m_closeButtonContainer;
        private Image m_closeButton;
        private Image m_contentImage;

        private void GuiLayout()
        {
            m_contentImage = new Image()
            {
                Aspect = Aspect.AspectFit,
            };

            m_closeButton = new Image()
            {
                Source = UIImages.BUTTON_CLOSE,
                HeightRequest = UISizes.IMAGE_EXPANSION_PAGE_CLOSE_BUTTON_SIZE,
                WidthRequest = UISizes.IMAGE_EXPANSION_PAGE_CLOSE_BUTTON_SIZE,
            };
            m_closeButton.AddSingleTapHandler(ClosePage);

            m_closeButtonContainer = new ContentView()
            {
                Content = m_closeButton,
                Margin = UISizes.MARGIN_STANDARD,
            };

            m_mainLayout = new AbsoluteLayout()
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.MARGIN_NONE,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };

            m_mainLayout.Children.Add(m_contentImage,
                new Rectangle(.5, .5, 1, 1),
                AbsoluteLayoutFlags.All);

            m_mainLayout.Children.Add(m_closeButtonContainer,
                new Rectangle(1, 0, UISizes.SIZE_NONE_SPECIFIED, UISizes.SIZE_NONE_SPECIFIED),
                AbsoluteLayoutFlags.PositionProportional);

            Style = (Style)Application.Current.Resources["contentPageStyle"];
            Content = m_mainLayout;
        }

        private async void ClosePage(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            m_instance = null;
        }

        private void PopulateContent(ImageSource imageToExpand)
        {
            m_contentImage.Source = imageToExpand;
        }

    }
}
