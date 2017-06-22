using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.MockupPages
{
    public class Groups_Main : ContentPage
    {
        private ButtonBar m_navigationBar;

        public Groups_Main()
        {
            m_navigationBar = new ButtonBar() { BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT) };
            m_navigationBar.AddImageButton(UIImages.ICON_JOINED, UnderConstruction);
            m_navigationBar.AddImageButton(UIImages.ICON_ADDMEMBERS, NavigateAddMembers);
            m_navigationBar.AddImageButton(UIImages.ICON_SEARCH, UnderConstruction);
            m_navigationBar.AddImageButton(UIImages.ICON_MORE, UnderConstruction);
            Content = new NavigationMockupTemplate(UIImages.MOCKUP_GROUPS_MAIN_UPPER, m_navigationBar, UIImages.MOCKUP_GROUPS_MAIN_LOWER);
        }

        private void NavigateAddMembers(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Groups_AddMembers());
        }

        private void UnderConstruction(object sender, EventArgs e)
        {
            DisplayAlert("Under Construction", "", "OK");
        }
    }
}
