using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using BikeBook.Views.CustomUIElements;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views
{
    public class MessagesChat : ContentPage
    {
        private GeneralPageTemplate m_mainTemplate;
        private TableView m_chatStack;
        private TableSection m_messages;
        private Grid m_inputLayout;
        private ExtendedEditor m_messageEntry;
        private Button m_submitButton;

        private string m_conversationPartner;
        private string owner;

        public MessagesChat()
        {
            Device.BeginInvokeOnMainThread(() => GuiLayout());
        }

        public MessagesChat(List<Message> conversation, string conversationPartner)
        {
            m_conversationPartner = conversationPartner;
            GuiLayout();
            PopulateContent(conversation, conversationPartner);
        }

        public MessagesChat(string ConversationPartner)
        {
            Service webService = Service.Instance;
            Messages retreivedMessages = webService.GetMessages(webService.Email);
            List<Message> conversation = MessageListQueries.GetConversation(retreivedMessages,ConversationPartner);
            m_conversationPartner = ConversationPartner;
            GuiLayout();
            PopulateContent(conversation, ConversationPartner);
        }

        private void GuiLayout()
        {
            m_messages = new TableSection();
            m_chatStack = new TableView()
            {
                Intent = TableIntent.Form,
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Root = new TableRoot()
                {
                    m_messages,
                }
            };

            ColumnDefinition imageButtonColumn = new ColumnDefinition() { Width = new GridLength(UISizes.CHAT_BUTTONS_WIDTH, GridUnitType.Absolute) };
            ColumnDefinition entryColumn = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            ColumnDefinition submitButtonColumn = new ColumnDefinition() { Width = new GridLength(UISizes.CHAT_BUTTONS_WIDTH, GridUnitType.Absolute) };

            m_inputLayout = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                Padding = UISizes.PADDING_STANDARD,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    imageButtonColumn,
                    entryColumn,
                    submitButtonColumn,
                },
            };

            m_messageEntry = new ExtendedEditor()
            {
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                MinimumHeightRequest = UISizes.CHAT_BUTTONS_WIDTH,
                HorizontalOptions = LayoutOptions.Fill,
            };
            m_inputLayout.Children.Add(m_messageEntry, m_inputLayout.ColumnDefinitions.IndexOf(entryColumn), 0);

            m_submitButton = new Button()
            {
                HeightRequest = UISizes.CHAT_BUTTONS_WIDTH,
                WidthRequest = UISizes.CHAT_BUTTONS_WIDTH,
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
                BorderRadius = Convert.ToInt16(UISizes.CHAT_BUTTONS_WIDTH/2),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium,typeof(Button)),
                Text = ">",
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0,0,0,UISizes.MARGIN_STANDARD),
            };
            m_submitButton.Clicked += SendMessage;
            m_inputLayout.Children.Add(m_submitButton, m_inputLayout.ColumnDefinitions.IndexOf(submitButtonColumn), 0);

            m_mainTemplate = new GeneralPageTemplate();

            m_mainTemplate.Content = m_chatStack;
            m_mainTemplate.AddBottomContent(m_inputLayout);
            this.Content = m_mainTemplate;
        }

        private void SendMessage(object sender, EventArgs e)
        {
            if( m_messageEntry.IsPopulated())
            {
                Service webService = Service.Instance;
                int messageResult = webService.SendMessage(webService.Email, m_conversationPartner, m_messageEntry.Text, string.Empty);
                switch(messageResult)
                {
                    case HttpStatus.OK:
                        RefreshMessages();
                        m_messageEntry.Text = string.Empty;
                        break;
                    default:
                        DisplayAlert("Message Failed", "Received response code: " + messageResult.ToString(), "OK");
                        break;
                }
            }
        }

        private void RefreshMessages()
        {
            Service webService = Service.Instance;
            Messages messages = webService.GetMessages(webService.Email);
            List<Message> updatedMessageList = MessageListQueries.GetConversation(messages, m_conversationPartner);
            m_messages.Clear();
            PopulateContent(updatedMessageList, m_conversationPartner);
        }

        private void PopulateContent(List<Message> conversation, string conversationPartner)
        {
            foreach(Message message in conversation)
            {
                ViewCell newMessage = new ViewCell()
                {
                    View = ChatTextCell.BuildFromMessage(message, conversationPartner),
                };
                m_messages.Add(newMessage);
            }
            m_mainTemplate.ContentScroll.ScrollToAsync(m_chatStack, ScrollToPosition.End, false);
        }


    }
}
