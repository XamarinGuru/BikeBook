using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class Mapping_Map_ViewRiders2 : ContentPage
    {
        private NavigationMockupTemplate m_mainTemplate;
        private Grid m_navigationGrid;
        private Image m_routeDetailsNavButton;
        private Image m_finishRouteNavButton;


        public Mapping_Map_ViewRiders2()
        {
            m_routeDetailsNavButton = new Image() { Source = UIImages.MOCKUP_MAPPING_HOME_MAP_VIEWRIDERS2_NAVBUTTON_ROUTEDETAILS };
            m_finishRouteNavButton = new Image() { Source = UIImages.MOCKUP_MAPPING_HOME_MAP_VIEWRIDERS2_NAVBUTTON_FINISHROUTE };
            m_finishRouteNavButton.AddSingleTapHandler(NavigateRideOverview);

            m_navigationGrid = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.FromHex(UIColors.COLOR_DEFAULT_BUTTON_BACKGROUND),
                Padding = 0,
                Margin = 0,
                RowDefinitions =
                {
                    new RowDefinition() {Height = new GridLength(45,GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition() {Width = new GridLength(1,GridUnitType.Star) },

                },
            };
            m_navigationGrid.Children.Add(m_routeDetailsNavButton, 0, 0);
            m_navigationGrid.Children.Add(m_finishRouteNavButton, 1, 0);


            m_mainTemplate = new NavigationMockupTemplate(UIImages.NONE, m_navigationGrid, UIImages.MOCKUP_MAPPING_HOME_MAP_VIEWRIDERS2_LOWER);
            Content = m_mainTemplate;
        }

        private void NavigateRideOverview(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Mapping_Ride_Tabbed());
        }
    }
}
