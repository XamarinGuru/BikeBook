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
     * Template for showing buy/sell items in a list
     */
    public class BuySellCell : ContentView
    {
        public ImageSource ImageSource
        {
            get { return m_baseTemplate.ImageSource; }
            set { m_baseTemplate.ImageSource = value; }
        }

        public string HeadLine
        {
            get { return m_headLineSpan.Text; }
            set { m_headLineSpan.Text = value + "\n"; }
        }

        public double Price
        {
            set
            {
                m_price = value;
                m_priceSpan.Text = m_price.ToString("C") + "\n";
            }
            get
            {
                return m_price;
            }
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

        public Ad Ad
        {
            get
            {
                return m_ad;
            }
            set
            {
                PopulateContent(value);
            }
        }

        private Ad m_ad;
        private double m_price;
        private DateTime m_postTime;

        private ContentCellTemplate m_baseTemplate;

        private FormattedString m_formattedText;
        private Span m_headLineSpan;
        private Span m_priceSpan;
        private Span m_postTimeSpan;

        private Button m_chatButton;


        /**
         * Class constructor
         */
        public BuySellCell()
        {
            m_price = 0;

            GuiLayout();
        }


        public BuySellCell(Ad ad)
        {
            GuiLayout();
            PopulateContent(ad);
        }



        /**
         * Lays out and displays template UI
         */
        private void GuiLayout()
        {
            m_baseTemplate = new ContentCellTemplate()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
            };

            m_headLineSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            m_priceSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            m_postTimeSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
            };

            m_formattedText = new FormattedString();
            m_formattedText.Spans.Add(m_headLineSpan);
            m_formattedText.Spans.Add(m_priceSpan);
            m_formattedText.Spans.Add(m_postTimeSpan);

            m_chatButton = new Button()
            {
                Text = "CHAT",
                TextColor = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Fill,
                BorderWidth = UISizes.BUTTON_BORDER_WIDTH,
                BorderRadius = UISizes.BUTTON_BORDER_RADIUS,
                BorderColor = Color.FromHex(UIColors.COLOR_NESTED_ACCENT_BACKGROUND),
            };
            m_chatButton.Clicked += NavigateMessageChat;
            
            m_baseTemplate.FormattedText = m_formattedText;
            m_baseTemplate.RightNestedContent.Children.Add(m_chatButton);
            Content = m_baseTemplate;
        }


        private void NavigateMessageChat(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagesChat(m_ad.owner));
        }


        private void PopulateContent(Ad ad)
        {
            ImageSerializer serializer = new ImageSerializer();
            m_ad = ad;
            ConvertDate dateConverter = new ConvertDate();
            HeadLine = ad.title;
            PostTime = dateConverter.FromJava(ad.created_at);
            ImageSource = serializer.DeserializeImageToCache(ad.picture);
        }
    }
}
