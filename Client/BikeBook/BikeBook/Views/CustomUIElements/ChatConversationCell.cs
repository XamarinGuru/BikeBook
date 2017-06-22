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
     * Template for showing ongoing conversations in a list
     */
    public class ChatConversationCell : ContentView
    {
        public ImageSource ImageSource
        {
            get { return m_baseTemplate.ImageSource; }
            set { m_baseTemplate.ImageSource = value; }
        }

        public string Name
        {
            get { return m_headLineSpan.Text; }
            set { m_headLineSpan.Text = value + "\n"; }
        }

        public FormattedString PreviewFormatted
        {
            set
            {
                m_baseTemplate.FormattedText = new FormattedString();
                m_baseTemplate.FormattedText.Spans.Add(m_headLineSpan);
                foreach(Span span in value.Spans)
                {
                    span.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    m_baseTemplate.FormattedText.Spans.Add(span);
                };
            }
        }

        public DateTime PostTime
        {
            set
            {
                m_postTime = value;
                m_ageLabel.Text = PostTime.ToAgeString();
            }
            get
            {
                return m_postTime;
            }
        }

        public Message Message
        {
            get;
        }


        private DateTime m_postTime;

        private ContentCellTemplate m_baseTemplate;

        private FormattedString m_formattedText;
        private Span m_headLineSpan;
        private Label m_ageLabel;

        /**
         * Class constructor
         */
        public ChatConversationCell()
        {
            GuiLayout();
        }

        public ChatConversationCell(Message message)
        {
            Message = message;
            GuiLayout();
            PopulateContent(Message);
        }

        /**
         * Lays out and displays template UI
         */
        private void GuiLayout()
        {
            m_baseTemplate = new ContentCellTemplate()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND),
            };

            m_headLineSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            m_formattedText = new FormattedString();
            m_formattedText.Spans.Add(m_headLineSpan);

            m_baseTemplate.FormattedText = m_formattedText;
            m_baseTemplate.VerticalTextAlignment = TextAlignment.Center;

            m_ageLabel = new Label()
            {
                LineBreakMode = LineBreakMode.CharacterWrap,
                TextColor = Color.FromHex(UIColors.COLOR_TERTIARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
            };
            m_baseTemplate.RightNestedContent.Children.Add(m_ageLabel);

            Content = m_baseTemplate;
        }

        private void PopulateContent(Message message)
        {
            Service webService = Service.Instance;
            ImageSerializer serializer = new ImageSerializer();

            string conversationPartner = MessageListQueries.GetConversationPartner(message);

            User conversationPartnerProfile = webService.GetUser(webService.Email, conversationPartner);
            ImageSource = serializer.DeserializeImageToCache(conversationPartnerProfile.picture);

            ConvertDate dateConverter = new ConvertDate();
            PreviewFormatted = new FormattedString()
            {
                Spans =
                {
                    new Span()
                    {
                        Text = message.content,
                        ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                    }
                },
            };
            PostTime = dateConverter.FromJava(message.created_at);
            Name = conversationPartnerProfile.name;
        }
    }
}
