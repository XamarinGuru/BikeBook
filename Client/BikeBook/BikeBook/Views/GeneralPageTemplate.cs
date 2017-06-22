using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views
{


    /**
     * Template containing a bottom toolbar for user navigation and a central scrolling view for page content
     */
    public class GeneralPageTemplate : ContentView
    {
        private StackLayout m_mainLayout;
        private ScrollView m_contentScroll;
        private ButtonBar m_bottomBar;


        /**
         * Content of the central scrolling view of the template
         * 
         * Overrides base Content field of base class
         */
        public new View Content
        {
            get { return m_contentScroll.Content; }
            set { m_contentScroll.Content = value; }
        }


        /**
         * Gets the central scrolling view for the page
         */
        public ScrollView ContentScroll
        {
            get { return m_contentScroll; }
        }


        /**
         * Allows insertion of content between the scroll and bottom toolbar
         */
        public void AddBottomContent(View view)
        {
            IList<View> stackChildren = m_mainLayout.Children;
            stackChildren.Insert(stackChildren.IndexOf(m_bottomBar), view);
        }


        /**
         * 
         */
         public event EventHandler<ScrolledEventArgs> Scrolled
         {
            add { m_contentScroll.Scrolled += value; }
            remove { m_contentScroll.Scrolled -= value; }
         }


        /**
         * Class constructor
         */
        public GeneralPageTemplate()
        {
            GuiLayout();
        }


        /**
         * Lays out and displays widget's UI
         */
        private void GuiLayout()
        {
            m_contentScroll = new ScrollView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,
                Padding = 0,
                Margin = 0,
            };
            BottomBarLayout();
            m_mainLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND),
                Padding = 0,
                Margin = 0,
                Children =
                {
                    m_contentScroll,
                    m_bottomBar
                },
            };

            base.Content = m_mainLayout;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;

        }

        
        /**
         * Builds layout for bottom toolbar
         */
        private void BottomBarLayout()
        {
            m_bottomBar = new ButtonBar() { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.End, BackgroundColor = Color.FromHex(UIColors.COLOR_DEFAULT_BUTTON_BACKGROUND) };
            m_bottomBar.AddImageButton(UIImages.TABICON_MESSAGES, NavigateMessages);
            m_bottomBar.AddImageButton(UIImages.TABICON_WHATSNEW, NavigateWhatsNew);
            m_bottomBar.AddImageButton(UIImages.TABICON_RIDERS, NavigateRiders);
            m_bottomBar.AddImageButton(UIImages.TABICON_NOTIFICATIONS, UnderConstruction);
            m_bottomBar.AddImageButton(UIImages.TABICON_MORE, MoreNavigationMenu);
        }

        private void UnderConstruction(object sender, EventArgs e)
        {
            Navigation.NavigationStack.Last().DisplayAlert("Under Construction", "", "OK");
        }
        /**
         * Navigate to Whats New homepage
         *          
         * @param object sender - object throwing event being thrown, required for EventHandler delegate
         * @param EventArgs e - required for EventHandler delegate
         */
        private async void NavigateMessages(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessagesRecent());
        }


        /**
         * Navigate to Whats New homepage
         *          
         * @param object sender - object throwing event being thrown, required for EventHandler delegate
         * @param EventArgs e - required for EventHandler delegate
         */
        private async void NavigateWhatsNew(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new HomeWhatsNew(),false);
            await Navigation.PushAsync(new HomeWhatsNew());
        }

        /**
         * Navigate to Riders homepage
         *          
         * @param object sender - object throwing event being thrown, required for EventHandler delegate
         * @param EventArgs e - required for EventHandler delegate
         */
        private async void NavigateRiders(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfileRidersMain());
        }

        /**
         * Navigate to buy/sell homepage
         */
        private async void NavigateBuySell()
        {
            await Navigation.PushAsync(new BuySellListAds());
        }

        /**
         * Navigate to logged in user's homepage
         */
        private async void NavigateProfileHome()
        {
            Service WebService = Service.Instance;
            await Navigation.PushAsync(new ProfileMain(WebService.Email));
        }

        /**
         * Navigate to Riders homepage
         *          
         * @param object sender - object throwing event being thrown, required for EventHandler delegate
         * @param EventArgs e - required for EventHandler delegate
         */
        private async void MoreNavigationMenu(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MockupPages.Home_More());
            //Page CurrentPage = Navigation.NavigationStack[0];
            //string SelectedAction = await CurrentPage.DisplayActionSheet("More Options", "Close",null, "Buy/Sell", "My Profile");
            //
            //switch(SelectedAction)
            //{
            //    case "My Profile":  NavigateProfileHome();  break;
            //    case "Buy/Sell":    NavigateBuySell();      break;
            //    default:                                    break;
            //}
        }

    }
}
