using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeBook.Views.CustomUIElements;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views
{
    /**
     * Page collecting user riding styles
     */
    public class LoginRegistration3 : ContentPage
    {
        /**
         * Class constructor
         * 
         * @param UserProfile profileIn - profile to edit in this form
         */
        public LoginRegistration3(User userIn)
        {
            m_newUser = userIn;
            GuiLayout();
        }

        private User m_newUser;
        private ColoredSwitchCell m_ridingStyleSport;
        private ColoredSwitchCell m_ridingStyleTouring;
        private ColoredSwitchCell m_ridingStyleCruising;
        private ColoredSwitchCell m_ridingStyleAdventure;
        private ColoredSwitchCell m_ridingStyleCommuting;
        private ColoredSwitchCell m_ridingStyleTrack;
        private Button m_continue;
        private TableView m_ridingStylesTable;
        private StackLayout m_mainLayout;

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

            m_continue = new Button{Text = "CONTINUE", VerticalOptions = LayoutOptions.EndAndExpand};

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
                    m_continue
                }
            };

            Title = "CHOOSE YOUR RIDING STYLES";
            Content = m_mainLayout;
            Style = (Style)Application.Current.Resources["contentPageStyle"];

            //Backgroud image must be set after page style
            BackgroundImage = UIImages.BACKGROUND;

            m_continue.Clicked += ShowNextPage;

        }

        /**
         * Callback for continue button, verifies selection and shows next registration page
         * 
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private void ShowNextPage(object sender, EventArgs e)
        {
            if (RidingStyleSelected())
            {
                CopyRidingStylesToProfile();
                Navigation.PushAsync(new LoginRegistration4(m_newUser));
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
            m_newUser.styleSport =      m_ridingStyleSport.On.ToString();
            m_newUser.styleTouring =    m_ridingStyleTouring.On.ToString();
            m_newUser.styleCruising =   m_ridingStyleCruising.On.ToString();
            m_newUser.styleAdventure =  m_ridingStyleAdventure.On.ToString();
            m_newUser.styleCommuting =  m_ridingStyleCommuting.On.ToString();
            m_newUser.styleTrack =      m_ridingStyleTrack.On.ToString();
        }

    }
}
