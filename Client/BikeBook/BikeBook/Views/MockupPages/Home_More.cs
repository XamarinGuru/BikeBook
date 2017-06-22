using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class Home_More : ContentPage
    {
        private GeneralPageTemplate m_mainTemplate;
        private StackLayout m_innerLayout;

        private Image m_fillerBlock1;
        private Image m_fillerBlock2;
        private Image m_navButton_buyandsell;
        private Image m_navButton_events;
        private Image m_navButton_groups;
        private Image m_navButton_myprofile;
        private Image m_navButton_myriders;
        private Image m_navButton_pages;
        private Image m_navButton_routeandride;

        public Home_More()
        {
            GuiLayout();
        }

        private void GuiLayout()
        {
            m_navButton_myprofile =     new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_MYPROFILE };
            m_navButton_myriders =      new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_MYRIDERS };
            m_navButton_routeandride =  new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_ROUTEANDRIDE };
            m_navButton_buyandsell =    new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_BUYANDSELL };
            m_fillerBlock1 =            new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_FILLERBLOCK1 };
            m_navButton_groups =        new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_GROUPS };
            m_navButton_events =        new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_EVENTS };
            m_navButton_pages =         new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_NAVBUTTON_PAGES };
            m_fillerBlock2 =            new Image() {Aspect = Aspect.AspectFit, HorizontalOptions=LayoutOptions.FillAndExpand, Source = UIImages.MOCKUP_HOMEMORE_FILLERBLOCK2 };

            m_navButton_myprofile.AddSingleTapHandler(NavigateMyProfile);
            m_navButton_myriders.AddSingleTapHandler(NavigateMyRiders);
            m_navButton_routeandride.AddSingleTapHandler(NavigateRouteAndRide);
            m_navButton_buyandsell.AddSingleTapHandler(NavigateBuyAndSell);
            m_navButton_groups.AddSingleTapHandler(NavigateGroups);
            m_navButton_events.AddSingleTapHandler(NavigateEvents);
            m_navButton_pages.AddSingleTapHandler(NavigatePages);

            m_innerLayout = new StackLayout()
            {
            Children =
                {
                    m_navButton_myprofile,
                    m_navButton_myriders,
                    m_navButton_routeandride,
                    m_navButton_buyandsell,
                    m_fillerBlock1,
                    m_navButton_groups,
                    m_navButton_events,
                    m_navButton_pages,
                    m_fillerBlock2,
                },
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_innerLayout,
            };

            Content = m_mainTemplate;
        }


        private void NavigateMyProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileMain(ClientWebService.Service.Instance.Email));
        }

        private void NavigateMyRiders(object sender, EventArgs e)
        {
            this.DisplayAlert("Under Construction","", "OK");
        }

        private void NavigateRouteAndRide(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Mapping_Map_ViewRiders2());
        }

        private void NavigateBuyAndSell(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuySellHome());
        }

        private void NavigateGroups(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Groups_Main());
        }

        private void NavigateEvents(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Events_ViewAll());
        }

        private void NavigatePages(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages_GeneralMain());
        }

    }
}
