using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class HomeWhatsNew : ContentPage
    {
        public HomeWhatsNew()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_HOME_WHATSNEW);
        }
    }

    public class Profile_Riders_Subcategory : ContentPage
    {
        public Profile_Riders_Subcategory()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_PROFILE_RIDERS_SUBCATEGORY);
        }
    }

    public class Groups_AddMembers : ContentPage
    {
        public Groups_AddMembers()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_GROUPS_ADDMEMBERS);
        }
    }

    public class Groups_NotificationSettings : ContentPage
    {
        public Groups_NotificationSettings()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_GROUPS_NOTIFICATIONSETTINGS);
        }
    }

    public class Groups_Settings : ContentPage
    {
        public Groups_Settings()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_GROUPS_SETTINGS);
        }
    }

    public class Groups_About_Members : ContentPage
    {
        public Groups_About_Members()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_GROUPS_ABOUTMEMBERS);
        }
    }

    public class Events_ViewAll : ContentPage
    {
        public Events_ViewAll()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_EVENTS_VIEWALL);
        }
    }

    public class Pages_GeneralMain : ContentPage
    {
        public Pages_GeneralMain()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_PAGES_GENERALMAIN);
        }
    }

    public class Profile_Main_About : ContentPage
    {
        public Profile_Main_About()
        {
            Content = new SimpleMockupPageTemplate(UIImages.MOCKUP_PROFILE_MAIN_ABOUT);
        }
    }
}
