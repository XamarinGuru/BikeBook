using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class SimpleMockupPageTemplate : ContentView
    {

        GeneralPageTemplate m_mainTemplate;
        Image m_mockupImage;


        public SimpleMockupPageTemplate(ImageSource mockupImage)
        {
            GuiLayout();
            PopulateContent(mockupImage);
        }


        public SimpleMockupPageTemplate()
        {
            GuiLayout();
        }

        private void GuiLayout()
        {
            m_mainTemplate = new GeneralPageTemplate();
            m_mockupImage = new Image()
            {
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
            };
            m_mainTemplate.Content = m_mockupImage;
            Content = m_mainTemplate;
        }

        private void PopulateContent(ImageSource mockupImage)
        {
            m_mockupImage.Source = mockupImage;
        }
    }
}
