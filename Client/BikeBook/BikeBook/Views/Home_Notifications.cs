using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ClientWebService;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;

namespace BikeBook.Views
{
    public class HomeNotifications : ContentPage
    {

        private Posts m_retreivedPosts;

        private GeneralPageTemplate m_mainTemplate;
        private TableView m_postTable;
        private TableSection m_friendPosts;

        public HomeNotifications()
        {
            GuiLayout();
            PopulateContent();
        }

        private void GuiLayout()
        {
            m_friendPosts = new TableSection("FRIEND POSTS");

            m_postTable = new TableView
            {
                Intent = TableIntent.Menu,
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Root = new TableRoot
                {
                    m_friendPosts,
                },
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_postTable,
            };

            Content = m_mainTemplate;
        }

        private void PopulateContent()
        {
            Service webService = Service.Instance;
            m_retreivedPosts = webService.GetPost(webService.Email);
            foreach (Post post in m_retreivedPosts.post)
            {
                ViewCell newMessage = new ViewCell()
                {
                    View = new NotificationCell(post),
                };
                m_friendPosts.Add(newMessage);
            };
        }
    }
}
