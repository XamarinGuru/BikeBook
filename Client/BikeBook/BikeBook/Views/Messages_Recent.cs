using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ClientWebService;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;

namespace BikeBook.Views
{
    /**
     * Page for showing recently received messages from friends and followers
     */
    public class MessagesRecent : ContentPage
    {
        private Messages m_retreivedMessages;

        private GeneralPageTemplate m_mainTemplate;
        private Button m_newChatButton;
        private TableView m_messageTable;
        private TableSection m_friendMessages;

        //private TableSection m_followerMessages;


        /**
         *  Class Constructor
         */
        public MessagesRecent()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GuiLayout();
                PopulateContent();
            });
        }


        /**
         * Builds up page UI layout
         */
        private void GuiLayout()
        {
            m_newChatButton = new Button() { Text = "New Message" };
            m_newChatButton.Clicked += NavigateMessageCompose;
            ViewCell ButtonCell = new ViewCell() { View = m_newChatButton };

            m_friendMessages = new TableSection("FRIEND MESSAGES");
            //m_followerMessages = new TableSection("FOLLOWER MESSAGES");

            m_messageTable = new TableView
            {
                Intent = TableIntent.Menu,
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Root = new TableRoot
                {
                    m_friendMessages,
                    //m_followerMessages,
                },
            };
            m_friendMessages.Add(ButtonCell);

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_messageTable,
            };

            Content = m_mainTemplate;
        }


        private void PopulateContent()
        {
            Service webService = Service.Instance;
            m_retreivedMessages = webService.GetMessages(webService.Email);
            foreach(Message message in MessageListQueries.GetUniqueConversations(m_retreivedMessages))
            {
                ViewCell newMessage = new ViewCell()
                {
                    View = new ChatConversationCell(message)
                };
                newMessage.Tapped += NavigateConversation;
                m_friendMessages.Add(newMessage);
            };
        }


        private void NavigateMessageCompose(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Messages_Compose());
        }


        private void NavigateConversation(object sender, EventArgs e)
        {
            ViewCell TappedCell = (ViewCell)sender;
            ChatConversationCell TappedConversation = (ChatConversationCell)TappedCell.View;
            string ConversationPartner = MessageListQueries.GetConversationPartner(TappedConversation.Message);
            List<Message> Conversation = MessageListQueries.GetConversation(m_retreivedMessages, ConversationPartner);
            this.Navigation.PushAsync(new MessagesChat(Conversation, ConversationPartner), false);
        }
    }
}
