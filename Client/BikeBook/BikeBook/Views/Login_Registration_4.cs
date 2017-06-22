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
     * Registration page where user adds their first bike
     */
    public class LoginRegistration4 : ContentPage
    {
        /**
         * Class constructor
         * 
         * @param UserProfile profileIn - profile to edit in this form
         */
        public LoginRegistration4(User userIn)
        {
            m_newUser = userIn;
            m_parentApp = ViewHandler.Instance.ParentApp;
            m_imageSelectionDialog = new ImageSelectionDialog();
            Device.BeginInvokeOnMainThread(() => GuiLayout());
        }

        private User m_newUser;
        private ImageSerializer m_imageSerializer;
        private ImageSelectionDialog m_imageSelectionDialog;

        private BikeBook m_parentApp;

        private Image m_addBikeImage;
        private Entry m_make;
        private Entry m_model;
        private ColoredPicker m_modelYearPicker;
        private Button m_continue;
        private StackLayout m_mainLayout;

        private string m_bikeImagePath;

        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_addBikeImage = new Image
            {
                Source = UIImages.PHOTOPLACEHOLDER,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Margin = UISizes.MARGIN_EXTRA_WIDE,
                HeightRequest = UISizes.PROFILE_IMAGE_HEIGHT,
                WidthRequest = UISizes.PROFILE_IMAGE_WIDTH
            };

            m_make = new Entry { Placeholder = "Make" };
            m_model = new Entry { Placeholder = "Model" };
            m_modelYearPicker = new ColoredPicker { Title = "Year" };
            m_continue = new Button { Text = "CONTINUE", VerticalOptions = LayoutOptions.EndAndExpand };

            PopulateModelYears();

            m_mainLayout = new StackLayout
            {
                Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"],
                Children = {
                    m_addBikeImage,
                    m_make,
                    m_model,
                    m_modelYearPicker,
                    m_continue,
                }
            };

            Title = "ADD A BIKE";
            Content = m_mainLayout;
            BackgroundImage = UIImages.BACKGROUND;

            m_addBikeImage.AddSingleTapHandler(addProfileImage_Tapped);
            m_continue.Clicked += registrationComplete;
        }


        /**
         * Callback to handle user clicking on add photo button
         *
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private async void addProfileImage_Tapped(object sender, EventArgs e)
        {
            m_bikeImagePath = await m_imageSelectionDialog.GetImage();
            ((Image)sender).Source = m_bikeImagePath;
        }


        /**
         * fills model year picker with years to select.
         */
        private void PopulateModelYears()
        {
            for (int year = DateTime.Now.AddYears(1).Year; year >= 1900; year--)
            {
                m_modelYearPicker.Items.Add(year.ToString("D4"));
            }
        }


        /**
         * Callback for continue button, verifies inputs and takes user to their profile
         * 
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private async void registrationComplete(object sender, EventArgs e)
        {
            if (bikeIsValid())
            {
                if (createNewProfile())
                {
                    await Navigation.PopToRootAsync(false);
                }
            }
        }


        /**
         * Verifies that fields have been filled to add a bike
         */
        private bool bikeIsValid()
        {
            if (m_make.Text == null || m_make.Text.Length == 0)
            {
                DisplayAlert("Enter a make", "", "OK");
                return false;
            }
            else if (m_model.Text == null || m_model.Text.Length == 0)
            {
                DisplayAlert("Enter a model", "", "OK");
                return false;
            }
            else if (m_modelYearPicker.SelectedIndex == -1)
            {
                DisplayAlert("Select a model year", "", "OK");
                return false;
            }

            return true;
        }


        /**
         * Instantiates new bike data object and populates fields with user inputs
         */
        private bool createNewProfile()
        {
            Service webService = Service.Instance;

            //if((m_addBikeImage.Source != null) && (m_imageSerializer == null))
            //    m_imageSerializer = new ImageSerializer(); 

            //var bikeInfo = new Models.UserProfile.BikeData();
            //bikeInfo.Make = m_make.Text;
            //bikeInfo.Model = m_model.Text;
            //bikeInfo.Year = m_modelYearPicker.Items[m_modelYearPicker.SelectedIndex];
            //bikeInfo.bikeImagePath = m_bikeImagePath;
            //m_newUser.bikes.Add(bikeInfo);

            //string SerializedImage = imageSerializer.SerializeFromFile(m_newProfile.profileImagePath);

            int registerStatusCode =
                webService.RegisterLocal(m_newUser.name, 
                m_newUser.email,
                m_newUser.picture,
                Convert.ToBoolean(m_newUser.styleSport),
                Convert.ToBoolean(m_newUser.styleTouring),
                Convert.ToBoolean(m_newUser.styleCruising),
                Convert.ToBoolean(m_newUser.styleAdventure),
                Convert.ToBoolean(m_newUser.styleTrack),
                Convert.ToBoolean(m_newUser.styleCommuting),
                m_newUser.temp_password);

            if (HttpStatus.CheckStatusCode(registerStatusCode))
            {
                return true;
            }
            else
            {
                DisplayAlert("An error occured", "Received response code: " + registerStatusCode.ToString(), "Ok");
                return false;
            };
        }
    }
}
