using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ClientWebService;
using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views
{

    /**
     * Page for acessing suggested riders in users area, as well as recent profiles acessed.
     */
    public class ProfileRidersMain : ContentPage
    {

        GeneralPageTemplate m_mainTemplate;
        StackLayout m_mainLayout;
        ScrollerPanel m_allUsersScroller;
        ScrollerPanel m_requestScroller;
        ScrollerPanel m_localScroller;


        /**
         * Class Constructor
         */
        public ProfileRidersMain()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GuiLayout();
                PopulateContent();
            });
        }


        /**
         * Lays out page UI
         */
        private void GuiLayout()
        {
            m_allUsersScroller = new ScrollerPanel() { Title = "ALL USERS" };
            m_allUsersScroller.AddSingleTapHandler(navigateSubcategory);
            //m_requestScroller = new ScrollerPanel() { Title = "REQUEST" };
            //m_localScroller = new ScrollerPanel() { Title = "LOCAL" };
            m_mainLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                Padding = UISizes.PADDING_STANDARD,
                Children =
                {
                    m_allUsersScroller,
                    //m_requestScroller,
                    //m_localScroller,
                },
            };
            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_mainLayout,
            };
            Content = m_mainTemplate;
        }



        /**
         * Requests and inserts Content into the UI widgets
         */
        private void PopulateContent()
        {
            Service WebService = Service.Instance;
            
            Users allUsers = WebService.GetAllUsers(WebService.Email);
            foreach (User user in allUsers.user)
            {
                ScrollerPanelItem newUser = new ScrollerPanelItem(user);
                m_allUsersScroller.AddItem(newUser);
            }

           
           //for (int i = 0; i < 6; i++)
           //{
           //    ScrollerPanelItem newBox = new ScrollerPanelItem();
           //    newBox.Title = "Jon Doe";
           //    newBox.Subtitle = "9,000 Followers";
           //    newBox.ImageSource = UIImages.BIKEPLACEHOLDER;
           //    m_recentScroller.Add(newBox);
           //}
           //
           //for (int i = 0; i < 6; i++)
           //{
           //    ScrollerPanelItem newBox = new ScrollerPanelItem();
           //    newBox.Title = "Mike Hunt";
           //    newBox.Subtitle = "350 Followers";
           //    newBox.ImageSource = UIImages.BIKEPLACEHOLDER;
           //     m_requestScroller.Add(newBox);
           //}
           //
           //for (int i = 0; i < 6; i++)
           //{
           //    ScrollerPanelItem newBox = new ScrollerPanelItem();
           //    newBox.Title = "Bob Loblaw";
           //    newBox.Subtitle = "No Friends At All";
           //    newBox.ImageSource = UIImages.BIKEPLACEHOLDER;
           //     m_localScroller.Add(newBox);
           //}
        }

        /**
         *  Navigates to a page showing the placeholder for a riders subcategory
         */
        private void navigateSubcategory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MockupPages.Profile_Riders_Subcategory());
        }

    }

}
