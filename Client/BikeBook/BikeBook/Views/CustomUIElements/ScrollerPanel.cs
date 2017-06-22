using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{


    /**
     * A UI element for scrolling horizontally through a list of UI widgets, such as images, ads, news items etc..
     */
    public class ScrollerPanel : ContentView
    {

        public String Title
        {
            set { m_title.Text = value.ToUpper(); }
            get { return m_title.Text; }
        }

        public event EventHandler TitleTapped
        {
            add { m_titleLayout.AddSingleTapHandler(value); }
            remove { m_titleLayout.RemoveSingleTapHandler(value); }
        }

        /**
         * Class constructor
         */
        public ScrollerPanel()
        {
            GuiLayout();
        }

        public void PopulatePlaceholders()
        {
            for (int i = 0; i < 10; i++)
            {
                ScrollerPanelItem newItem = new ScrollerPanelItem(MockupPages.DummyProfile.User);
                this.AddItem(newItem);
            }
        }

        private StackLayout m_mainLayout;
        private StackLayout m_titleLayout;
        private Label m_title;
        private Image m_titleChevron;
        private ScrollView m_contentScroll;
        private StackLayout m_contentLayout;

        /**
         * Adds an element to the end of the scroll
         * 
         * @param View newView - the Xamarin.Forms.View to put at the end of the scroller
         */
        public void AddItem(ScrollerPanelItem newItem)
        {
            m_contentLayout.Children.Add(newItem);
        }

        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_title = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
            };

            m_titleChevron = new Image()
            {
                Source = UIImages.CHEVRON_RIGHT,
                HeightRequest = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
            };

            m_titleLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    m_title,
                    m_titleChevron,
                }
            };

            m_contentLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                HeightRequest = UISizes.SCROLLER_PANEL_HEIGHT,
                Spacing = UISizes.SPACING_STANDARD,
            };

            m_contentScroll = new ScrollView()
            {
                Orientation = ScrollOrientation.Horizontal,
                HeightRequest = UISizes.SCROLLER_PANEL_HEIGHT,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = m_contentLayout,
            };

            m_mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = UISizes.SPACING_NARROW,
                Children =
                {
                    m_titleLayout,
                    m_contentScroll,
                }
            };

            Content = m_mainLayout;
        }
    }
}