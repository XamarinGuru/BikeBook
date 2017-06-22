using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class Mapping_Ride_Tabbed : ContentPage
    {
        private NavigationMockupTemplate m_mainTemplate;
        private Grid m_navigationGrid;
        private Image m_tabRideOverview;
        private Image m_tabRideReview;
        private Image m_tabRateRide;

        public Mapping_Ride_Tabbed()
        {
            GuiLayout();
            SetTabRideOverview();
        }

        private void GuiLayout()
        {
            m_tabRideOverview = new Image() { Aspect = Aspect.AspectFit };
            m_tabRideOverview.AddSingleTapHandler(SetTabRideOverview);
            m_tabRideReview = new Image() { Aspect = Aspect.AspectFit };
            m_tabRideReview.AddSingleTapHandler(SetTabRideReview);
            m_tabRateRide = new Image() { Aspect = Aspect.AspectFit };
            m_tabRateRide.AddSingleTapHandler(SetTabRateRide);
            m_navigationGrid = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
                Padding = 0,
                Margin = 0,
                RowDefinitions =
                {
                    new RowDefinition() {Height = new GridLength(30,GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition() {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition() {Width = new GridLength(1,GridUnitType.Star) },
                },
            };
            m_navigationGrid.Children.Add(m_tabRideOverview, 0, 0);
            m_navigationGrid.Children.Add(m_tabRideReview, 1, 0);
            m_navigationGrid.Children.Add(m_tabRateRide, 2, 0);

            m_mainTemplate = new NavigationMockupTemplate(UIImages.NONE, m_navigationGrid, UIImages.NONE);

            Content = m_mainTemplate;
        }

        private void SetTabRideOverview(object sender = null, EventArgs e = null)
        {
            m_tabRideOverview.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RIDEOVERVIEW_HILITED;
            m_tabRideReview.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RIDEREVIEW;
            m_tabRateRide.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RATERIDE;
            m_mainTemplate.LowerImageSource = UIImages.MOCKUP_MAPPING_RIDE_OVERVIEW_LOWER;
        }

        private void SetTabRideReview(object sender, EventArgs e)
        {
            m_tabRideOverview.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RIDEOVERVIEW;
            m_tabRideReview.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RIDEREVIEW_HILITED;
            m_tabRateRide.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RATERIDE;
            m_mainTemplate.LowerImageSource = UIImages.MOCKUP_MAPPING_RIDE_REVIEW_LOWER;
        }

        private void SetTabRateRide(object sender, EventArgs e)
        {
            m_tabRideOverview.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RIDEOVERVIEW;
            m_tabRideReview.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RIDEREVIEW;
            m_tabRateRide.Source = UIImages.MOCKUP_MAPPING_RIDE_NAVBUTTON_RATERIDE_HILITED;
            m_mainTemplate.LowerImageSource = UIImages.MOCKUP_MAPPING_RIDE_RATE_LOWER;
        }

    }
}
