using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;
using BikeBook.Views.MockupPages;

namespace BikeBook.Views
{
    public class ProfileMain : ContentPage
    {

        ImageSelectionDialog m_imageSelectionDialog;
        ImageSerializer m_imageSerializer;
        Service m_webService;
        User m_loadedProfile;

        private GeneralPageTemplate m_mainTemplate;
        
        // Main layout, holds page contents, no padding or margins
        private StackLayout m_mainLayout;

        // Main layout, holds page contents, has padding and margins
        private StackLayout m_profileContent;

        // Layout for profile header, contains banner image and inset profile picture
        //private RelativeLayout m_imageHeader;
        private AbsoluteLayout m_imageHeader;

        // Images contained within header
        private Image m_bannerImage;

        // Basic details on user's profile
        private Label m_profileName;

        private InfoList m_infoGrid;

        private ScrollerPanel  m_garageScroller;

        private ScrollerPanel  m_gearScroller;

        private ImageGrid m_userPhotoAlbum;

        // TODO: Build gallery widget to show small excerpt of user's photo gallery
        //private Grid m_Grid_PostedPhotos;

        // Toolbars for getting around and doing things with this profile
        private ButtonBar m_profileInteraction;
        private ButtonBar m_profileNavigation;


        /**
         * Class Contructor for profile main page, builds page using a dummy profile
         */
        public ProfileMain()
        {
            m_loadedProfile = DummyProfile.User;
            
            InitServices();
            GuiLayout();
            PopulateContent();
        }

        /**
         * Class Contructor for profile main page
         * 
         * @params string userEmail - User ID to load and display profile of
         */
        public ProfileMain(string userEmail)
        {
            LoadProfile(userEmail);

            InitServices();
            GuiLayout();
            PopulateContent();
        }


        /**
         *  Instantiates helper classes needed on this page
         */
        private void InitServices()
        {
            m_imageSelectionDialog = new ImageSelectionDialog();
            m_imageSerializer = new ImageSerializer();
        }


        /**
         *  Requests user's profile from webService
         */
        private void LoadProfile(string userEmail)
        {
            m_webService = Service.Instance;
            m_loadedProfile = m_webService.GetUser(m_webService.Email, userEmail);
        }


        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            BannerImageLayout();
            ProfileInteractionLayout();
            ProfileNavigationLayout();
            InfoGridLayout();
            GarageLayout();
            GearLayout();
            PhotoAlbumLayout();

            m_profileContent = new StackLayout()
            {
                Spacing = UISizes.SPACING_WIDE,
                Margin = UISizes.MARGIN_STANDARD,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    m_infoGrid,
                    m_garageScroller,
                    m_gearScroller,
                    m_userPhotoAlbum,
                },
            };

            m_mainLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    m_imageHeader,
                    m_profileInteraction,
                    new VerticalSpacer(UISizes.SPACING_STANDARD),
                    m_profileNavigation,
                    m_profileContent
                },
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_mainLayout,
            };

            Content = m_mainTemplate;
        }




        /**
         * Builds layout for profile banner, incl. user-selected banner image and profile picture
         */
        private void BannerImageLayout()
        {
            m_imageHeader = new AbsoluteLayout()
            {
                HeightRequest = UISizes.BANNER_IMAGE_HEIGHT,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            m_bannerImage = new Image()
            {
                Aspect = Aspect.AspectFill,
                Source = UIImages.PHOTOPLACEHOLDER,
                HeightRequest = UISizes.BANNER_IMAGE_HEIGHT,
            };

            m_profileName = new Label()
            {
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            m_imageHeader.Children.Add(m_bannerImage, new Rectangle(.5, .5, 1, UISizes.BANNER_IMAGE_HEIGHT), AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
            m_imageHeader.Children.Add(m_profileName, new Rectangle(.05, .75, 1, 1), AbsoluteLayoutFlags.All);
        }


        /** 
         * Builds layout for profile interaction toolbar. If user is viewing their own profile, shows bar with modify buttons, else shows viewing buttons
         */
        private void ProfileInteractionLayout()
        {
            m_profileInteraction = new ButtonBar() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT) };
            if ((m_webService != null) &&
                (m_webService.Email != null) &&
                (m_loadedProfile.email == m_webService.Email))
            {
                m_profileInteraction.AddImageButton(UIImages.ICON_POST, UnderConstruction);
                m_profileInteraction.AddImageButton(UIImages.ICON_UPDATEINFO, UpdateProfile);
                m_profileInteraction.AddImageButton(UIImages.ICON_ACTIVITYLOG, UnderConstruction);
                m_profileInteraction.AddImageButton(UIImages.ICON_MORE, UnderConstruction);
            }
            else
            {
                m_profileInteraction.AddImageButton(UIImages.PROFILE_ICON_FRIENDS, UnderConstruction);
                m_profileInteraction.AddImageButton(UIImages.PROFILE_ICON_FOLLOWING, UnderConstruction);
                m_profileInteraction.AddImageButton(UIImages.PROFILE_ICON_MESSAGE, NavigateMessage);
                m_profileInteraction.AddImageButton(UIImages.PROFILE_ICON_MORE, UnderConstruction);
            }
        }

        private void UpdateProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile_Update_1(m_webService.Email));
        }


        /** 
         * Builds layout for profile info display
         */
        private void InfoGridLayout()
        {
            m_infoGrid = new InfoList();
        }


        /** 
         * Builds layout for garage display
         */
        private void GarageLayout()
        {
            m_garageScroller = new ScrollerPanel() { Title = "MY GARAGE" };
            m_garageScroller.TitleTapped += NavigateMyGarage(m_loadedProfile);
        }

        private EventHandler NavigateMyGarage(User profile)
        {
            return (sender, e) =>
            {
                Navigation.PushAsync(new Home_MyGarage(profile));
            };
        }


        /** 
         * Builds layout for garage display
         */
        private void GearLayout()
        {
            m_gearScroller = new ScrollerPanel() { Title = "MY GEAR" };
            m_gearScroller.TitleTapped += NavigateMyGear(m_loadedProfile);
        }

        private EventHandler NavigateMyGear(User profile)
        {
            return (sender, e) =>
            {
                Navigation.PushAsync(new Home_MyGear(profile));
            };
        }

        /**
         * Build layout for photo album
         */
        private void PhotoAlbumLayout()
        {
            m_userPhotoAlbum = new ImageGrid();
            m_userPhotoAlbum.Title = "MY PHOTOS";
            m_userPhotoAlbum.TitleTapped += NavigateMyPhotos(m_loadedProfile);
        }

        private EventHandler NavigateMyPhotos(User profile)
        {
            return (sender, e) =>
            {
                Navigation.PushAsync(new Home_MyPhotos(profile));
            };
            
        }


        /** Placeholder function for roughing in layout
         */
        void DummyOnClickAction()
        { }

        /**
         *  Navigates to a new conversation with the person whose profile you're viewing
         */
        private void NavigateMessage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagesChat(m_loadedProfile.email));
        }


        /**
         *  EventHandler to display and "Under Construction" message for not-yet implemented features
         */
        private void UnderConstruction(object sender, EventArgs e)
        {
            DisplayAlert("Under Construction", "", "OK");
        }

        /**
         * Builds layout for profile navigation toolbar
         */
        private void ProfileNavigationLayout()
        {
            m_profileNavigation = new ButtonBar() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT) };
            m_profileNavigation.AddTextButton("About", null);
            m_profileNavigation.AddTextButton("Photos", null);
            m_profileNavigation.AddTextButton("Friends", null);
        }


        /**
         * EventHandler to handle user clicking to add/change Profile Banner
         */
        private async void editableImage_Tapped(object sender, EventArgs e)
        {
            ((Image)sender).Source  = await m_imageSelectionDialog.GetImage();
            //TODO: Update profile picture with newly selected image
        }


        /**
         * Populates UI with loaded user profile
         */
        private void PopulateContent()
        {
            PopulateBanner();
            PopulateInfoGrid();
            PopulateGarage();
            PopulateGear();
            PopulatePhotoAlbum();
        }


        /**
         * Populates banner with user's profile picture and name
         */
        private void PopulateBanner()
        {
            m_profileName.Text = m_loadedProfile.name;
            m_bannerImage.Source = m_imageSerializer.DeserializeImageToCache(m_loadedProfile.picture);
        }


        /**
         *  Populates info grid with details from user's profile
         */
        private void PopulateInfoGrid()
        {
            ConvertDate dateConverter = new ConvertDate();

            // Account Ade
            string accountAgeString = "Joined " + dateConverter.FromJava(m_loadedProfile.created_at).ToAgeString();
            m_infoGrid.AddInfo(accountAgeString);

            // Riding styles
            string ridingStylesString = BuildRidingStylesString(m_loadedProfile);
            if( ridingStylesString.Length > 0 )
            {
                m_infoGrid.AddInfo( "Rides " + ridingStylesString );
            }

            // Description
            if( (m_loadedProfile.description != null ) &&
                (m_loadedProfile.description.Length >= 0))
            {
                m_infoGrid.AddInfo(m_loadedProfile.description);
            }
        }


        /**
         *  Builds comma-separated string for showing riding styles of user
         */
        private string BuildRidingStylesString(User profile)
        {
            string returnString = string.Empty;

            if (Convert.ToBoolean(profile.styleAdventure))
                returnString += "Adventure";
            if (Convert.ToBoolean(profile.styleCommuting))
            {
                if (returnString.Length > 0)
                    returnString += ", ";
                returnString += "Commuting";
            }
            if (Convert.ToBoolean(profile.styleCruising))
            {
                if (returnString.Length > 0)
                    returnString += ", ";
                returnString += "Cruising";
            }
            if (Convert.ToBoolean(profile.styleSport))
            {
                if (returnString.Length > 0)
                    returnString += ", ";
                returnString += "Sport";
            }
            if (Convert.ToBoolean(profile.styleTouring))
            {
                if (returnString.Length > 0)
                    returnString += ", ";
                returnString += "Touring";
            }
            if (Convert.ToBoolean(profile.styleTrack))
            {
                if (returnString.Length > 0)
                    returnString += ", ";
                returnString += "Track";
            }

            return returnString;
        }


        /**
         *  Populates garage scroller with pictures of user's bikes.
         */
        private void PopulateGarage()
        {
            // TODO: populate garage scroller from loaded profile
            m_garageScroller.PopulatePlaceholders();
        }

        /**
         *  Populates garage scroller with pictures of user's gear.
         */
        private void PopulateGear()
        {
            // TODO: populate gear scroller from loaded profile
            m_gearScroller.PopulatePlaceholders();
        }

        /**
         *  Populates photo grid with user's photos
         */
        private void PopulatePhotoAlbum()
        {
            // TODO: populate photo album from loaded profile
            m_userPhotoAlbum.PopulatePlaceholders(6, ImageExpansionPage.ExpandImage(UIImages.BIKEPLACEHOLDER)); ;
        }
    }
}
