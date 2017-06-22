using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class NotificationCell : ContentView
    {

        public ImageSource ImageSource
        {
            get { return m_baseTemplate.ImageSource; }
            set { m_baseTemplate.ImageSource = value; }
        }

        public string Text
        {
            get { return m_contentTextSpan.Text; }
            set { m_contentTextSpan.Text = value + "\n"; }
        }

        public DateTime PostTime
        {
            set
            {
                m_postTime = value;
                m_postTimeSpan.Text = m_postTime.ToLocalTime().ToString("f");
            }
            get
            {
                return m_postTime;
            }
        }

        public Post Post
        {
            get
            {
                return m_post;
            }
            set
            {
                PopulateContent(value);
            }
        }


        private Post m_post;
        private DateTime m_postTime;
        private ContentCellTemplate m_baseTemplate;
        private FormattedString m_formattedText;
        private Span m_contentTextSpan;
        private Span m_postTimeSpan;


        public NotificationCell()
        {
            GuiLayout();
        }


        public NotificationCell(Post post)
        {
            GuiLayout();
            PopulateContent(post);
        }


        private void GuiLayout()
        {
            m_baseTemplate = new ContentCellTemplate()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
            };
            m_contentTextSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            m_postTimeSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };
            m_formattedText = new FormattedString();
            m_formattedText.Spans.Add(m_contentTextSpan);
            m_formattedText.Spans.Add(m_postTimeSpan);

            m_baseTemplate.FormattedText = m_formattedText;
            Content = m_baseTemplate;
        }


        private void PopulateContent(Post post)
        {
            ConvertDate dateConverter = new ConvertDate();
            m_post = post;
            Text = post.content;
            PostTime = dateConverter.FromJava(post.Created_at);
            ImageSource = UIConstants.EmbeddedImageName(UIConstants.EmbeddedImages.AddUserImage);
        }
    }
}
