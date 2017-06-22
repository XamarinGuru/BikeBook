using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{

    /**
     * Template for showing gear items in a list
     */
    public class GearCell : ContentView
    {
        public ImageSource ImageSource
        {
            get { return m_baseTemplate.ImageSource; }
            set { m_baseTemplate.ImageSource = value; }
        }

        public string Headline
        {
            get { return m_headLineSpan.Text; }
            set { m_headLineSpan.Text = value + "\n"; }
        }

        public string Subtitle
        {
            get { return m_subtitleSpan.Text; }
            set { m_subtitleSpan.Text = value; }
        }

        private ContentCellTemplate m_baseTemplate;

        private FormattedString m_formattedText;
        private Span m_headLineSpan;
        private Span m_subtitleSpan;

        /**
         * Class Constructor
         */
        public GearCell()
        {
            GuiLayout();
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

            m_subtitleSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };

            m_formattedText = new FormattedString();
            m_formattedText.Spans.Add(m_headLineSpan);
            m_formattedText.Spans.Add(m_subtitleSpan);

            m_baseTemplate.FormattedText = m_formattedText;
            m_baseTemplate.VerticalTextAlignment = TextAlignment.Center;
            Content = m_baseTemplate;
        }
    }
}
