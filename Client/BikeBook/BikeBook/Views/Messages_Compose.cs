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
    public class Messages_Compose : ContentPage
    {
        private Users m_retreivedUsers;

        private GeneralPageTemplate m_mainTemplate;
        private TableView m_userTable;
        private TableSection m_usersToMessage;

        //private TableSection m_followerMessages;


        /**
         *  Class Constructor
         */
        public Messages_Compose()
        {
            GuiLayout();
            PopulateContent();
        }


        /**
         * Builds up page UI layout
         */
        private void GuiLayout()
        {
            m_usersToMessage = new TableSection("Write to:");
            //m_followerMessages = new TableSection("FOLLOWER MESSAGES");

            m_userTable = new TableView
            {
                Intent = TableIntent.Menu,
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Root = new TableRoot
                {
                    m_usersToMessage,
                },
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_userTable,
            };

            Content = m_mainTemplate;
        }

        private void PopulateContent()
        {
            Service webService = Service.Instance;
            m_retreivedUsers = webService.GetAllUsers(webService.Email);
            foreach( User user in m_retreivedUsers.user)
            {
                ViewCell newUser = new ViewCell()
                {
                    View = new UserProfileCell(user),
                };
                newUser.Tapped += NavigateConversation;
                m_usersToMessage.Add(newUser);
            }
            //InsertMockupContent();
        }

        private void InsertMockupContent()
        {
            for( int i = 0; i < 10; i++)
            {
                ViewCell newUser = new ViewCell()
                {
                    View = new UserProfileCell(new User()
                    {
                        name = "Test user " + i.ToString(),
                        email = "bob@bob",
                        picture = ImageSerializer.SAMPLEIMAGE,
                    }),
                };
                newUser.Tapped += NavigateConversation;
                m_usersToMessage.Add(newUser);
            }
        }

        private void NavigateConversation(object sender, EventArgs e)
        {
            ViewCell TappedCell = (ViewCell)sender;
            UserProfileCell TappedConversation = (UserProfileCell)TappedCell.View;
            string ConversationPartner = TappedConversation.CurrentUser.email;
            Navigation.PushAsync(new MessagesChat(ConversationPartner), false);
        }
    }
}
