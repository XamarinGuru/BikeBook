using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views.TestPages
{
    /**
     * Test page for testing Cell templates
     */
    public class ScrollerPanelTestPage : ContentPage
    {
        /**
         * Class constructor
         */
        public ScrollerPanelTestPage()
        {
            GuiLayout();
        }

        private ScrollerPanel m_scrollerPanel;

        /**
         * Lays out UI elements
         */
        private void GuiLayout()
        {
            var BaseLayout = new GeneralPageTemplate();

            m_scrollerPanel = new ScrollerPanel()
            {
                Title = "Test Title",
            };
            m_scrollerPanel.PopulatePlaceholders();
            StackLayout Stack = new StackLayout
            {
                Children =
                {
                    m_scrollerPanel,
                },
            };
            BaseLayout.Content = Stack;

            Content = BaseLayout;
        }

        private void DummyCallback(object sender, EventArgs e)
        {
            int i = 0;
            i++;
        }
    }
}
