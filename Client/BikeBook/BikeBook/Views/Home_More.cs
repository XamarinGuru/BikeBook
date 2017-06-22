using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;

namespace BikeBook.Views
{
    public class Home_More : ContentPage
    {
        class MoreMenuItem
        {
            public MoreMenuItem(String title, String subtitle, UIConstants.EmbeddedImages image)
            {
                Title = title;
                Subtitle = subtitle;
                Image = UIConstants.EmbeddedImageName(image);
            }

            public String Title;

            public String Subtitle;

            public ImageSource Image;
        };

        List<MoreMenuItem> m_menuItems = new List<MoreMenuItem>
        {
            new MoreMenuItem("My Profile","",UIConstants.EmbeddedImages.BikePlaceHolder),
            new MoreMenuItem("Buy and Sell","",UIConstants.EmbeddedImages.BikePlaceHolder),
        };

        GeneralPageTemplate m_mainTemplate;

        ListView m_mainListView;

        public Home_More()
        {
            m_mainListView = new ListView()
            {
                ItemsSource = m_menuItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    ViewCell newCell = new ViewCell()
                    {
                        View = new NavMenuCell()
                        {

                        };
                    };

                    return newCell;
                }),
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_mainListView,
            };

            Content = m_mainTemplate;
        }
    }
}
