using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     * Cell Template for showing received text-only messages in a conversation
     */
    public class ChatTextCell : ContentView
    {
        public ImageSource ImageSource
        {
            get { return m_talkingHeadImage.Source; }
            set { m_talkingHeadImage.Source = value; }
        }

        public string Text
        {
            get { return m_centralText.Text; }
            set { m_centralText.Text = value; }
        }

        protected ImageSerializer m_imageSerializer;

        protected string m_backgroundColor;
        protected Service m_webService;

        protected Grid m_mainLayout;
        protected Image m_talkingHeadImage;
        protected ContentView m_centralContentHolder;
        protected Label m_centralText;
        protected BoxView m_textBackground;

        protected ColumnDefinition m_imageColumn;
        protected ColumnDefinition m_textColumn;
        protected ColumnDefinition m_paddingColumn;

        /**
         * Class constructor
         */
        public ChatTextCell()
        {
            m_backgroundColor = Color.Transparent.GetHashCode().ToString("X8");
            GuiLayout();
        }

        public static ChatTextCell BuildFromMessage(Message message, string conversationPartner)
        {
            if(message.from.Equals(conversationPartner))
            {
                return new ChatReceivedTextCell(message);
            }
            else
            {
                return new ChatSentTextCell(message);
            }
        }


        /**
         *  Inits service classes needed for this page
         */
         protected void InitServices()
        {
            m_webService = Service.Instance;
            m_imageSerializer = new ImageSerializer();
        }


        /**
         * Lays out and displays page UI
         */
        protected void GuiLayout()
        {
            m_imageColumn = new ColumnDefinition { Width = new GridLength(UISizes.TALKING_HEAD_IMAGE_SIZE_MEDIUM, GridUnitType.Absolute) };
            m_textColumn = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            m_paddingColumn = new ColumnDefinition { Width = new GridLength(UISizes.CHAT_STREAM_DEAD_SPACE, GridUnitType.Absolute) };

            m_mainLayout = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Padding = UISizes.PADDING_STANDARD,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                },
            };

            BuildGridColumns();

            m_talkingHeadImage = new CircleImage()
            {
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_MEDIUM,
                WidthRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_MEDIUM,
            };
            m_talkingHeadImage.AddSingleTapHandler(NavigateSenderProfile);

            m_mainLayout.Children.Add(m_talkingHeadImage, m_mainLayout.ColumnDefinitions.IndexOf(m_imageColumn), 0);

            m_textBackground = new BoxView()
            {
                BackgroundColor = Color.FromHex(m_backgroundColor),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            m_mainLayout.Children.Add(m_textBackground, m_mainLayout.ColumnDefinitions.IndexOf(m_textColumn), 0);

            m_centralText = new Label()
            {
                LineBreakMode = LineBreakMode.CharacterWrap,
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                VerticalOptions = LayoutOptions.Fill
            };

            m_centralContentHolder = new ContentView()
            {
                Margin = UISizes.MARGIN_STANDARD,
                VerticalOptions = LayoutOptions.Center,
                Content = m_centralText,
            };

            m_mainLayout.Children.Add(m_centralContentHolder, 1, 0);

            Content = m_mainLayout;
        }

        protected void PopulateContent(Message message)
        {
            m_centralText.Text = message.content;
            SetTalkingHeadImage();
        }

        protected virtual void BuildGridColumns()
        {
            throw new NotImplementedException("The ChatTextCell template is not able to be directly used to display messages, use ChatReceivedTextCell or ChatSentTextCell instead");
        }

        protected virtual void SetTalkingHeadImage()
        {
            throw new NotImplementedException("The ChatTextCell template is not able to be directly used to display messages, use ChatReceivedTextCell or ChatSentTextCell instead");
        }

        protected virtual void NavigateSenderProfile(object sender, EventArgs e)
        {
            throw new NotImplementedException("The ChatTextCell template is not able to be directly used to display messages, use ChatReceivedTextCell or ChatSentTextCell instead");
        }
    }


    /**
     * Cell layout for showing received text-only messages in a conversation
     */
    public class ChatReceivedTextCell : ChatTextCell
    {
        private string m_senderEmail;

        /**
         * Class constructor
         */
        public ChatReceivedTextCell()
        {
            m_backgroundColor = UIColors.COLOR_DEFAULT_BUTTON_BACKGROUND;
            InitServices();
            GuiLayout();
        }

        /**
         * Class constructor
         */
        public ChatReceivedTextCell(Message message)
        {
            m_senderEmail = message.from;
            m_backgroundColor = UIColors.COLOR_DEFAULT_BUTTON_BACKGROUND;
            InitServices();
            GuiLayout();
            PopulateContent(message);
        }

        /**
         * Lays out and displays page UI
         */
        protected override void BuildGridColumns()
        {
            m_mainLayout.ColumnDefinitions = new ColumnDefinitionCollection()
            {
                 m_imageColumn,
                 m_textColumn,
                 m_paddingColumn,
            };
        }

        /**
         *  
         */
        protected override void SetTalkingHeadImage()
        {
            ImageSource = m_imageSerializer.GetProfilePicture(m_senderEmail);
        }

        protected override void NavigateSenderProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileMain(m_senderEmail));
        }
    }


    /**
     * Cell layout for showing received text-only messages in a conversation
     */
    public class ChatSentTextCell : ChatTextCell
    {
        /**
         * Class constructor
         */
        public ChatSentTextCell()
        {
            m_backgroundColor = UIColors.COLOR_GENERAL_ACCENT;
            InitServices();
            GuiLayout();
        }

        /**
         * Class constructor
         */
        public ChatSentTextCell(Message message)
        {
            m_backgroundColor = UIColors.COLOR_GENERAL_ACCENT;
            InitServices();
            GuiLayout();
            PopulateContent(message);
        }

        /**
         * Lays out and displays page UI
         */
        protected override void BuildGridColumns()
        {
            m_mainLayout.ColumnDefinitions = new ColumnDefinitionCollection()
            {
                 m_paddingColumn,
                 m_textColumn,
                 m_imageColumn,
            };
        }

        /**
         * 
         */
        protected override void SetTalkingHeadImage()
        {
            ImageSource = m_imageSerializer.GetProfilePicture(m_webService.Email);
        }

        protected override void NavigateSenderProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileMain(m_webService.Email));
        }
    }
}


