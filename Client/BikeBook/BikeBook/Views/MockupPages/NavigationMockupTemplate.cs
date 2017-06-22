using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class NavigationMockupTemplate : ContentView
    {
        public ImageSource UpperImageSource { set { m_upperImage.Source = value; } }
        public ImageSource LowerImageSource { set { m_lowerImage.Source = value; } }


        private GeneralPageTemplate m_mainTemplate;
        private StackLayout m_innerLayout;
        private Image m_upperImage;
        private ContentView m_navigationContent;
        private Image m_lowerImage;


        public NavigationMockupTemplate(ImageSource upperImage, View navigationView, ImageSource lowerImage)
        {
            GuiLayout();
            PopulateContent(upperImage, navigationView, lowerImage);
        }


        public NavigationMockupTemplate()
        {
            GuiLayout();
        }

        private void GuiLayout()
        {
            m_mainTemplate = new GeneralPageTemplate();
            m_upperImage = new Image()
            {
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
            };
            m_navigationContent = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
            };
            m_lowerImage = new Image()
            {
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
            };

            m_innerLayout = new StackLayout()
            {
                Padding = 0,
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    m_upperImage,
                    m_navigationContent,
                    m_lowerImage,
                },
            };
            m_mainTemplate.Content = m_innerLayout;
            Content = m_mainTemplate;
        }

        private void PopulateContent(ImageSource upperImage, View navigationView, ImageSource lowerImage)
        {
            m_upperImage.Source = upperImage;
            m_navigationContent.Content = navigationView;
            m_lowerImage.Source = lowerImage;
        }
    }
}
