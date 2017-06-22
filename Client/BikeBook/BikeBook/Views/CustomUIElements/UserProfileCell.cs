using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class UserProfileCell : ContentView
    {
        public ImageSource ImageSource
        {
            get { return m_baseTemplate.ImageSource; }
            set { m_baseTemplate.ImageSource = value; }
        }

        public string Name
        {
            get { return m_nameSpan.Text; }
            set { m_nameSpan.Text = value + "\n"; }
        }

        public string Location
        {
            get { return m_locationSpan.Text; }
            set { m_locationSpan.Text = value; }
        }

        public User CurrentUser
        {
            get;
            private set;
        }

        private ContentCellTemplate m_baseTemplate;

        private FormattedString m_formattedText;
        private Span m_nameSpan;
        private Span m_locationSpan;

        /**
         * Class constructor
         */
        public UserProfileCell()
        {
            GuiLayout();
        }

        public UserProfileCell(User user)
        {
            GuiLayout();
            PopulateContent(user);
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

            m_nameSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            m_locationSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };

            m_formattedText = new FormattedString();
            m_formattedText.Spans.Add(m_nameSpan);
            m_formattedText.Spans.Add(m_locationSpan);

            m_baseTemplate.FormattedText = m_formattedText;
            m_baseTemplate.VerticalTextAlignment = TextAlignment.Center;

            Content = m_baseTemplate;
        }

        private void PopulateContent(User user)
        {
            CurrentUser = user;
            Name = user.name;
            Location = "Kelowna, BC";
            ImageSerializer serializer = new ImageSerializer();
            m_baseTemplate.ImageSource = serializer.DeserializeImageToCache(user.picture);

            //ConvertDate dateConverter = new ConvertDate();
            //LocationSubtitle = new FormattedString()
            //{
            //    Spans =
            //    {
            //        new Span()
            //        {
            //            Text = message.content,
            //            ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
            //        }
            //    },
            //};
            //PostTime = dateConverter.FromJava(message.created_at);
            //Name = message.from;
        }
    }
}
