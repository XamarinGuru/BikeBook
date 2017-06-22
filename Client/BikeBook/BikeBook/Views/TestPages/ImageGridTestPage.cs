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
    public class ImageGridTestPage : ContentPage
    {
        /**
         * Class constructor
         */
        public ImageGridTestPage()
        {
            GuiLayout();
        }

        private Button m_toggleEditButton;
        private ImageGrid m_imageGrid;
        private StackLayout m_contentStack;
        private ScrollView m_contentScroll;

        /**
         * Lays out UI elements
         */
        private void GuiLayout()
        {
            m_toggleEditButton = new Button()
            {
                Text = "Toggle Editable",
            };
            m_toggleEditButton.Clicked += ToggleEditable;

            m_imageGrid = new ImageGrid();
            m_imageGrid.TitleTapped += TitleTap;
            m_imageGrid.PopulatePlaceholders(12, ImageExpansionPage.ExpandImage(UIImages.BIKEPLACEHOLDER), deleteTap);

            m_contentStack = new StackLayout()
            {
                Children =
                {
                    m_toggleEditButton,
                    m_imageGrid,
                },
            };

            m_contentScroll = new ScrollView();
            m_contentScroll.Content = m_contentStack;

            Style = (Style)Application.Current.Resources["contentPageStyle"];
            Content = m_contentScroll;
        }


        private void ToggleEditable(object sender, EventArgs e)
        {
            m_imageGrid.Editable = !m_imageGrid.Editable;
        }


        private void TitleTap(object sender, EventArgs e)
        {
            this.DisplayAlert("title tapped", "", "ok");
        }

        private void pictureTap(object sender, EventArgs e)
        {
            
            this.DisplayAlert("picture tapped", "", "ok");
        }

        private void deleteTap(object sender, EventArgs e)
        {
            this.DisplayAlert("delete tapped", "", "ok");
        }
    }
}
