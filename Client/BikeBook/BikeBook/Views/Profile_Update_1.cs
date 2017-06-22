using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ClientWebService;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;
using BikeBook.Views.MockupPages;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views
{

    /**
     * Form to collect user credentials for new account
     */
    public class Profile_Update_1 : ContentPage
    {
        private User m_loadedProfile;

        private ImageSelectionDialog m_imageSelectionDialog;
        private ImageSerializer m_imageSerializer;
        private Service m_webService;

        private CircleImage m_image_changeProfileImage;
        private Label m_label_Email;
        private Entry m_entry_Password;
        private Entry m_entry_ConfirmPassword;
        private Entry m_entry_ActualName;
        private ExtendedEditor m_editor_Description;
        private Button m_button_Continue;
        private Button m_button_SaveProfile;
        private StackLayout m_StackLayout_MainLayout;
        private ScrollView m_ScrollView_MainContainer;

        /**
         * Class constructor. Opens a dummy profile to edit
         */
        public Profile_Update_1()
        {
            m_loadedProfile = DummyProfile.User;

            InitServices();
            GuiLayout();
            PopulateContent();
        }


        /**
         * Class constructor. Opens a specific profile to edit
         * 
         * @param string userEmail
         */
        public Profile_Update_1(string userEmail)
        {

            InitServices();
            LoadUserProfile(userEmail);
            GuiLayout();
            PopulateContent();
        }


        private void LoadUserProfile(string userEmail)
        {
            m_loadedProfile = m_webService.GetUser(m_webService.Email, userEmail);
            if( m_loadedProfile.email == null )
            {
                DisplayAlert("Could not load your profile, please check your internet connection", "", "OK");
                Navigation.PopAsync();
            }
        }


        /**
         *  Instantiates helper classes needed on this page
         */
        private void InitServices()
        {
            m_webService = Service.Instance;
            m_imageSerializer = new ImageSerializer();
            m_imageSelectionDialog = new ImageSelectionDialog();
        }


        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_image_changeProfileImage = new CircleImage()
            {
                Source = UIImages.PHOTOPLACEHOLDER,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Margin = UISizes.MARGIN_EXTRA_WIDE,
                HeightRequest = UISizes.PROFILE_IMAGE_HEIGHT,
                WidthRequest = UISizes.PROFILE_IMAGE_WIDTH
            };

            m_label_Email = new Label();
            m_entry_Password = new Entry { Placeholder = "New Password", IsPassword = true };
            m_entry_ConfirmPassword = new Entry { Placeholder = "Confirm New Password", IsPassword = true };
            m_entry_ActualName = new Entry { Placeholder = "Your Name" };
            m_editor_Description = new ExtendedEditor { Placeholder = "Add a description..." };
            m_button_Continue = new Button { Text = "CONTINUE", VerticalOptions = LayoutOptions.EndAndExpand };
            m_button_SaveProfile = new Button { Text = "UPDATE PROFILE", VerticalOptions = LayoutOptions.EndAndExpand, Style = (Style)Application.Current.Resources["SubmitButtonStyle"] };


            m_StackLayout_MainLayout = new StackLayout
            {
                Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"],
                Children = {
                    m_image_changeProfileImage,
                    m_label_Email,
                    m_entry_Password,
                    m_entry_ConfirmPassword,
                    m_entry_ActualName,
                    m_editor_Description,
                    m_button_Continue,
                    m_button_SaveProfile,
                }
            };

            m_ScrollView_MainContainer = new ScrollView();
            m_ScrollView_MainContainer.Content = m_StackLayout_MainLayout;

            Title = "UPDATE ACCOUNT";

            Content = m_ScrollView_MainContainer;
            Style = (Style)Application.Current.Resources["contentPageStyle"];


            m_image_changeProfileImage.AddSingleTapHandler(changeProfileImage_Tapped);
            m_button_Continue.Clicked += showNextPage;
            m_button_SaveProfile.Clicked += saveProfileChanges;

        }

        /**
         *  Populates page content from loaded user profile
         */
        private void PopulateContent()
        {
            ImageSource profileImage = m_imageSerializer.DeserializeImageToCache(m_loadedProfile.picture);
            if( profileImage != null )
            {
                m_image_changeProfileImage.Source = profileImage;
            }

            m_label_Email.Text = m_loadedProfile.email;
            m_entry_ActualName.Placeholder = m_loadedProfile.name;

            if( (m_loadedProfile.description != null ) &&
                (m_loadedProfile.description.Length > 0 ))
                m_editor_Description.Placeholder = m_loadedProfile.description;
        }


        /**
         * EventHandler for continue button, verifies inputs and moves to riding styles page
         */
        private void showNextPage(object sender, EventArgs e)
        {
            if (userCredentialsAreValid())
            {
                CopyContentToProfile();
                Navigation.PushAsync(new Profile_Update_2(m_loadedProfile, this));
            }
        }


        /**
         * EventHandler for save button, verifies inputs and pushes changes to server
         */
        private void saveProfileChanges(object sender, EventArgs e)
        {
            if (userCredentialsAreValid())
            {
                CopyContentToProfile();
                int updateStatusCode = m_webService.ChangeProfile(m_webService.Email, m_loadedProfile);
                if (!HttpStatus.CheckStatusCode(updateStatusCode))
                {
                    DisplayAlert("Profile update failed", "Status code: " + updateStatusCode, "OK");
                }
                else
                {
                    Navigation.PopAsync();
                }
            }
        }


        /**
         * Callback to handle user clicking on add photo button
         *
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private async void changeProfileImage_Tapped(object sender, EventArgs e)
        {
            if (m_imageSerializer == null)
                m_imageSerializer = new ImageSerializer();
            string profileImagePath = await m_imageSelectionDialog.GetImage();
            m_loadedProfile.picture = m_imageSerializer.SerializeFromFile(profileImagePath);
            ((Image)sender).Source = profileImagePath;
        }


        /**
         * Verifies valid name, password have been entered
         */
        private bool userCredentialsAreValid()
        {
            if (m_entry_Password.IsPopulated() &&
                !m_entry_ConfirmPassword.IsPopulated())
            {
                DisplayAlert("Please confirm your password", "", "OK");
                return false;
            }
            else if (m_entry_ConfirmPassword.IsPopulated() &&
                    !m_entry_Password.IsPopulated())
            {
                DisplayAlert("Please enter a new password", "", "OK");
                return false;
            }
            else if (m_entry_Password.IsPopulated() &&
                     m_entry_ConfirmPassword.IsPopulated() &&
                    !m_entry_Password.Text.Equals(m_entry_ConfirmPassword.Text))
            {
                DisplayAlert("Passwords must match", "", "OK");
                return false;
            }
            else if (m_entry_ActualName.IsPopulated() &&
                    (m_entry_ActualName.Text.Length < UISizes.ACTUAL_NAME_MIN_LENGTH))
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
            if (m_entry_ActualName.IsPopulated())
            {
                m_loadedProfile.name = m_entry_ActualName.Text;
            }

            if (m_entry_Password.IsPopulated())
            {
                m_loadedProfile.temp_password = m_entry_Password.Text;

            }
            if( m_editor_Description.IsPopulated() )
            {
                m_loadedProfile.description = m_editor_Description.Text;
            }
        }



    }
}
