using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using System.Threading.Tasks;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views
{

    /**
     * Page shows UI for login and registration buttons
     */
    public class LoginRegistration1 : ContentPage
    {
        /**
         * Constructor for Loginsplash
         * 
         * @param App parentApp - TODO, entry point for application
         */
        public LoginRegistration1()
        {
            m_parentApp = ViewHandler.Instance.ParentApp;
            GuiLayout();
        }

        private BikeBook m_parentApp;
        
        private Image m_mainLogo;
        private Entry m_email;
        private Entry m_password;
        private Button m_login;
        private Label m_or;
        private Button m_register;
        private Button m_loginWithFacebook;
        private StackLayout m_mainLayout;
        private Label m_serverUrl;

        /**
         * Defines layout of the page
         */
        private void GuiLayout()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            m_mainLogo = new Image
            {
                Source = UIImages.SPLASHLOGO,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = UISizes.MARGIN_EXTRA_WIDE
            };

            m_email = new Entry{Placeholder = "Email"};
            m_password = new Entry{Placeholder = "Password", IsPassword = true};
            m_login = new Button{ Text = "LOGIN", IsEnabled = false };
            m_or = new Label{Text = "OR"};
            m_register = new Button{Text = "REGISTER"};
            m_loginWithFacebook = new Button{Text = "LOGIN WITH FACEBOOK"};
            m_serverUrl = new Label { Text = Consts.URL, TextColor = Color.White, FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };

            m_or.HorizontalOptions = LayoutOptions.CenterAndExpand;
            m_loginWithFacebook.BackgroundColor = Color.FromHex(UIColors.COLOR_FACEBOOK_BLUE);

            m_mainLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"],
                Children = { m_mainLogo, m_email, m_password, m_login, m_or, m_register, m_loginWithFacebook, m_serverUrl }
            };

            Content = m_mainLayout;
            BackgroundImage = UIImages.BACKGROUND;

            m_email.TextChanged += emailOrPasswordTextChanged;
            m_password.TextChanged += emailOrPasswordTextChanged;
            m_login.Clicked += Button_Login_Clicked;
            m_register.Clicked += Button_Register_Clicked;
        }


        /**
         * Callback for login button
         * 
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private void Button_Login_Clicked(object sender, EventArgs e)
        {
            int LoginResult = m_parentApp.WebService.LoginLocal(m_email.Text, m_password.Text);
            switch (LoginResult)
            {
                case HttpStatus.OK:
                    Navigation.PushAsync(new ProfileRidersMain(), false);
                    //Navigation.InsertPageBefore(ViewHandler.Instance.getHomePage(),this);
                    //await Navigation.PopAsync(false);
                    break;
                case HttpStatus.UNAUTHORIZED:
                case HttpStatus.UNKNOWN:
                    DisplayAlert("Login Failed", "Username or password not recognized", "OK");
                    break;
                default:
                    DisplayAlert("Login Failed", "Received response code: " + LoginResult.ToString(), "OK");
                    break;
            }
        }


        /**
         * Callback for Register button click, opens registration page
         * 
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private void Button_Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login_Registration_2());
        }

        /**
         * Calback for change to email or password field.
         * If both have been populated, login button unlocks.
         * 
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private void emailOrPasswordTextChanged(object sender, EventArgs e)
        {
            m_login.IsEnabled = m_email.IsPopulated() && m_password.IsPopulated();            
        }

    }
}
