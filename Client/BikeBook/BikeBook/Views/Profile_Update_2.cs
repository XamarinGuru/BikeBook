using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ClientWebService;
using BikeBook.Views.CustomUIElements;
using BikeBook.Views.MockupPages;

namespace BikeBook.Views
{
    /**
     * Page collecting user riding styles
     */
    public class Profile_Update_2 : ContentPage
    {
        /**
         * Class constructor, builds page with dummy content
         * 
         * @param UserProfile profileIn - profile to edit in this form
         */
        public Profile_Update_2()
        {
            m_loadedProfile = DummyProfile.User;

            InitServices();
            GuiLayout();
            PopulateContent();
        }


        /**
         * Class constructor
         * 
         * @param UserProfile profileIn - profile to edit in this form
         */
        public Profile_Update_2(User userIn, Page PreviousProfileUpdatePage)
        {
            m_loadedProfile = userIn;
            m_previousPage = PreviousProfileUpdatePage;

            InitServices();
            GuiLayout();
            PopulateContent();
        }

        private Page m_previousPage;

        private User m_loadedProfile;
        private Service m_webService;

        private ColoredSwitchCell m_ridingStyleSport;
        private ColoredSwitchCell m_ridingStyleTouring;
        private ColoredSwitchCell m_ridingStyleCruising;
        private ColoredSwitchCell m_ridingStyleAdventure;
        private ColoredSwitchCell m_ridingStyleCommuting;
        private ColoredSwitchCell m_ridingStyleTrack;
        //private Button m_continue;
        private Button m_saveProfile;
        private TableView m_ridingStylesTable;
        private StackLayout m_mainLayout;


        /**
         *  Instantiates helper classes needed on this page
         */
        private void InitServices()
        {
            m_webService = Service.Instance;
        }


        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_ridingStyleSport =     new ColoredSwitchCell { Text = "Sport" };
            m_ridingStyleTouring =   new ColoredSwitchCell { Text = "Touring" };
            m_ridingStyleCruising =  new ColoredSwitchCell { Text = "Cruising" };
            m_ridingStyleAdventure = new ColoredSwitchCell { Text = "Adventure" };
            m_ridingStyleCommuting = new ColoredSwitchCell { Text = "Commuting" };
            m_ridingStyleTrack =     new ColoredSwitchCell { Text = "Track" };

            //m_continue = new Button{Text = "CONTINUE", VerticalOptions = LayoutOptions.EndAndExpand};
            m_saveProfile = new Button { Text = "UPDATE PROFILE", VerticalOptions = LayoutOptions.EndAndExpand, Style = (Style)Application.Current.Resources["SubmitButtonStyle"] };

            m_ridingStylesTable = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot {
                    new TableSection {
                        m_ridingStyleSport    ,
                        m_ridingStyleTouring  ,
                        m_ridingStyleCruising ,
                        m_ridingStyleAdventure,
                        m_ridingStyleCommuting,
                        m_ridingStyleTrack
                    }
                }
            };

            m_mainLayout = new StackLayout
            {
                Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"],
                Children = {
                    m_ridingStylesTable,
                    //m_continue,
                    m_saveProfile
                }
            };

            Title = "UPDATE YOUR RIDING STYLES";
            Content = m_mainLayout;
            Style = (Style)Application.Current.Resources["contentPageStyle"];

            //m_continue.Clicked += showNextPage;
            m_saveProfile.Clicked += saveProfileChanges;
        }


        /**
         * Fills content from loaded profile
         */
        private void PopulateContent()
        {
            m_ridingStyleSport.On = Convert.ToBoolean(m_loadedProfile.styleSport);
            m_ridingStyleTouring.On = Convert.ToBoolean(m_loadedProfile.styleTouring);
            m_ridingStyleCruising.On = Convert.ToBoolean(m_loadedProfile.styleCruising);
            m_ridingStyleAdventure.On = Convert.ToBoolean(m_loadedProfile.styleAdventure);
            m_ridingStyleCommuting.On = Convert.ToBoolean(m_loadedProfile.styleCommuting);
            m_ridingStyleTrack.On = Convert.ToBoolean(m_loadedProfile.styleTrack);
        }


        /**
         * Callback for continue button, verifies selection and shows next registration page
         */
        private void showNextPage(object sender, EventArgs e)
        {
            if (RidingStyleSelected())
            {
                throw new NotImplementedException();
            }
        }


        /**
         * EventHandler for save button, verifies inputs and pushes changes to server
         */
        private void saveProfileChanges(object sender, EventArgs e)
        {
            if (RidingStyleSelected())
            {
                CopyRidingStylesToProfile();
                int updateStatusCode = m_webService.ChangeProfile(m_webService.Email, m_loadedProfile);
                if (!HttpStatus.CheckStatusCode(updateStatusCode))
                {
                    DisplayAlert("Profile update failed", "Status code: " + updateStatusCode, "OK");
                }
                else
                {
                    if (m_previousPage != null)
                    {
                        Navigation.RemovePage(m_previousPage);
                    }
                    Navigation.PopAsync();
                }
            }
        }


        /**
         * Verifies that at least one riding style was selected
         */
        private bool RidingStyleSelected()
        {
            if (m_ridingStyleSport.On ||
                m_ridingStyleTouring.On ||
                m_ridingStyleCruising.On ||
                m_ridingStyleAdventure.On ||
                m_ridingStyleCommuting.On ||
                m_ridingStyleTrack.On)
            {
                return true;
            }
            else
            {
                DisplayAlert("Select at least one riding style", "", "OK");
                return false;
            }
        }

        /**
         * Sets user profile riding styles to match selected options in UI
         */
        private void CopyRidingStylesToProfile()
        {
            m_loadedProfile.styleSport =      m_ridingStyleSport.On.ToString();
            m_loadedProfile.styleTouring =    m_ridingStyleTouring.On.ToString();
            m_loadedProfile.styleCruising =   m_ridingStyleCruising.On.ToString();
            m_loadedProfile.styleAdventure =  m_ridingStyleAdventure.On.ToString();
            m_loadedProfile.styleCommuting =  m_ridingStyleCommuting.On.ToString();
            m_loadedProfile.styleTrack =      m_ridingStyleTrack.On.ToString();
        }
    }
}
