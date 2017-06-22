using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views
{

    /**
     * Splash page for buy/sell service
     */
    public class BuySellHome : ContentPage
    {

        GeneralPageTemplate m_mainTemplate;
        StackLayout m_mainLayout;
        Button m_createAdButton;
        ScrollerPanel m_hottestItemsScroll;
        ButtonBar m_categoryBar;
        ScrollerPanel m_favoritesScroll;
        ScrollerPanel m_myAdsScroll;

        public BuySellHome()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GuiLayout();
                PopulateContent();
            });
        }


        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_createAdButton = new Button()
            {
                Text = "CREATE AN AD",
                //Image = UIImages.ICON_MEGAPHONE,
                Style = (Style)Application.Current.Resources["HilightButtonStyle"],
            };
            m_createAdButton.Clicked += NavigateCreateAd;

            m_hottestItemsScroll = new ScrollerPanel()
            {
                Title = "HOTTEST ITEMS"
            };

            m_categoryBar = new ButtonBar()
            {
                HeightRequest = UISizes.TOOLBAR_HEIGHT_TALL,
            };
            m_categoryBar.AddImageButton(UIImages.ADCATEGORY_BIKES, NavigateListAds);
            m_categoryBar.AddImageButton(UIImages.ADCATEGORY_BIKEPARTS, NavigateListAds);
            m_categoryBar.AddImageButton(UIImages.ADCATEGORY_ACCESSORIES, NavigateListAds);
            m_categoryBar.AddImageButton(UIImages.ADCATEGORY_GEAR, NavigateListAds);
            m_categoryBar.AddImageButton(UIImages.ADCATEGORY_CLOTHING, NavigateListAds);

            m_favoritesScroll = new ScrollerPanel()
            {
                Title = "FAVORITES"
            };

            m_myAdsScroll = new ScrollerPanel()
            {
                Title = "MY ADS"
            };


            m_mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = UISizes.SPACING_STANDARD,
                Margin = UISizes.MARGIN_STANDARD,
                Children =
                {
                    m_createAdButton,
                    m_hottestItemsScroll,
                    m_categoryBar,
                    m_favoritesScroll,
                    m_myAdsScroll,
                },
            };
            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_mainLayout,
            };
            Content = m_mainTemplate;
            BackgroundColor = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND);
        }

        private void NavigateListAds(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuySellListAds());
        }

        private void NavigateCreateAd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuySellCreateAd());
        }

        private void UnderConstruction(object sender, EventArgs e)
        {
            this.DisplayAlert("Under Construction", "", "OK");
        }


        /**
         * Requests and inserts Content into the UI widgets
         * 
         * TODO: build actual content populator, it's just placeholders for now
         */
        private void PopulateContent()
        {
            for( int i = 0; i < 6; i++)
            {
                ScrollerPanelItem newBox = new ScrollerPanelItem();
                newBox.Title = "Bike For SALE!!!";
                newBox.Subtitle = "$3.50";
                newBox.ImageSource = UIImages.BIKEPLACEHOLDER;
                m_hottestItemsScroll.AddItem(newBox);
            }

            for (int i = 0; i < 6; i++)
            {
                ScrollerPanelItem newBox = new ScrollerPanelItem();
                newBox.Title = "Bike For SALE!!!";
                newBox.Subtitle = "$3.50";
                newBox.ImageSource = UIImages.BIKEPLACEHOLDER;
                m_myAdsScroll.AddItem(newBox);
            }

            for (int i = 0; i < 6; i++)
            {
                ScrollerPanelItem newBox = new ScrollerPanelItem();
                newBox.Title = "Bike For SALE!!!";
                newBox.Subtitle = "$3.50";
                newBox.ImageSource = UIImages.BIKEPLACEHOLDER;
                m_favoritesScroll.AddItem(newBox);
            }
        }

    }
}
