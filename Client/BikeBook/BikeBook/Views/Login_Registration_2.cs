using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ClientWebService;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;

namespace BikeBook.Views
{

    /**
     * Form to collect user credentials for new account
     */
    public class Login_Registration_2 : ContentPage
    {
        /**
         * Class constructor. Creates new blank profile to edit 
         */
        public Login_Registration_2()
        {
            m_newUser = new User();
            m_imageSelectionDialog = new ImageSelectionDialog();

            Device.BeginInvokeOnMainThread(() => GuiLayout());
        }

        private User m_newUser;
        private ImageSelectionDialog m_imageSelectionDialog;
        private ImageSerializer m_imageSerializer;

        private CircleImage m_image_addProfileImage;
        private Entry m_entry_Email;
        private Entry m_entry_Password;
        private Entry m_entry_ConfirmPassword;
        private Entry m_entry_ActualName;
        private Button m_button_Continue;
        private StackLayout m_StackLayout_MainLayout;
        private ScrollView m_ScrollView_MainContainer;

        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_image_addProfileImage = new CircleImage
            {
                Source = UIImages.PHOTOPLACEHOLDER,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Margin = UISizes.MARGIN_EXTRA_WIDE,
                HeightRequest = UISizes.PROFILE_IMAGE_HEIGHT,
                WidthRequest = UISizes.PROFILE_IMAGE_WIDTH
            };

            m_entry_Email = new Entry{Placeholder = "Email"};
            m_entry_Password = new Entry{Placeholder = "Password",IsPassword = true};
            m_entry_ConfirmPassword = new Entry{Placeholder = "Confirm Password",IsPassword = true};
            m_entry_ActualName = new Entry{Placeholder = "Your Name"};
            m_button_Continue = new Button { Text = "CONTINUE", VerticalOptions = LayoutOptions.EndAndExpand };

            m_StackLayout_MainLayout = new StackLayout
            {
                Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"],
                Children = {
                    m_image_addProfileImage,
                    m_entry_Email,
                    m_entry_Password,
                    m_entry_ConfirmPassword,
                    m_entry_ActualName,
                    m_button_Continue
                }
            };

            m_ScrollView_MainContainer = new ScrollView();
            m_ScrollView_MainContainer.Content = m_StackLayout_MainLayout;

            Title = "CREATE ACCOUNT";

            Content = m_ScrollView_MainContainer;
            BackgroundImage = UIImages.BACKGROUND;


            m_image_addProfileImage.AddSingleTapHandler(addProfileImage_Tapped);
            m_button_Continue.Clicked += showNextPage;

        }

        /**
         * Callback for continue button, verifies credentials and moves to riding styles page
         * 
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private void showNextPage(object sender, EventArgs e)
        {
            if (userCredentialsAreValid())
            {
                CopyContentToProfile();
                Navigation.PushAsync(new LoginRegistration3(m_newUser));
            }

        }


        /**
         * Callback to handle user clicking on add photo button
         *
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private async void addProfileImage_Tapped(object sender, EventArgs e)
        {
            if (m_imageSerializer == null)
                m_imageSerializer = new ImageSerializer();
            string profileImagePath = await m_imageSelectionDialog.GetImage();
            m_newUser.picture = m_imageSerializer.SerializeFromFile(profileImagePath);
            ((Image)sender).Source = profileImagePath;
        }


        /**
         * Verifies valid email, uname, password have been entered
         */
        private bool userCredentialsAreValid()
        {
            if (!m_entry_Email.IsFormattedEmailAddress())
            {
                DisplayAlert("Email must be valid", "", "OK");
                return false;
            }
            else if (!m_entry_Password.IsPopulated())
            {
                DisplayAlert("Please enter a password", "", "OK");
                return false;
            }
            else if (!m_entry_ConfirmPassword.IsPopulated())
            {
                DisplayAlert("Please confirm your password", "", "OK");
                return false;
            }
            else if (!m_entry_Password.Text.Equals(m_entry_ConfirmPassword.Text))
            {
                DisplayAlert("Passwords must match", "", "OK");
                return false;
            }
            else if (!m_entry_ActualName.IsPopulated() || m_entry_ActualName.Text.Length < UISizes.ACTUAL_NAME_MIN_LENGTH)
            {
                DisplayAlert("Please enter your name", "", "OK");
                return false;
            }

            return true;
        }


        /**
         * Writes entered credentials to UserProfile from UI
         */
        private void CopyContentToProfile()
        {
            m_newUser.name = m_entry_ActualName.Text;
            m_newUser.email = m_entry_Email.Text;
            m_newUser.temp_password = m_entry_Password.Text;
        }
    }
}
