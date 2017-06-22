using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class Groups_About_Main : ContentPage
    {

        private Image m_navButtonEditSettings;
        private Image m_navButtonMembers;
        private Image m_navButtonNotificationSettings;
        private StackLayout m_navigationLayout;

        public Groups_About_Main()
        {
            m_navButtonEditSettings = new Image() { Source = UIImages.MOCKUP_GROUPS_ABOUTMAIN_NAVBUTTON_EDITSETTINGS };
            m_navButtonMembers = new Image() { Source = UIImages.MOCKUP_GROUPS_ABOUTMAIN_NAVBUTTON_MEMBERS };
            m_navButtonNotificationSettings = new Image() { Source = UIImages.MOCKUP_GROUPS_ABOUTMAIN_NAVBUTTON_NOTIFICATIONSETTINGS };

            m_navButtonEditSettings.AddSingleTapHandler(NavigateSettings);
            m_navButtonMembers.AddSingleTapHandler(NavigateMembers);
            m_navButtonNotificationSettings.AddSingleTapHandler(NavigateNotificationSettings);

            m_navigationLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    m_navButtonEditSettings,
                    m_navButtonMembers,
                    m_navButtonNotificationSettings,
                },
            };
            Content = new NavigationMockupTemplate(UIImages.MOCKUP_GROUPS_ABOUTMAIN_UPPER, m_navigationLayout, UIImages.MOCKUP_GROUPS_ABOUTMAIN_LOWER);
        }


        private void NavigateSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Groups_Settings());
        }

        private void NavigateMembers(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Groups_About_Members());
        }

        private void NavigateNotificationSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Groups_NotificationSettings());
        }

    }
}
