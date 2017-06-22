using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views
{
    public class Home_MyPhotos : ContentPage
    {

        public Home_MyPhotos()
        {
            InitServices();
            GuiLayout();
            PopulatePlaceholders();
        }


        public Home_MyPhotos(User userProfile)
        {
            InitServices();
            GuiLayout();
            PopulateContent(userProfile);
        }

        private User m_loadedProfile;
        private ImageSelectionDialog m_imageSelector;
        private ImageSerializer m_imageSerializer;

        private StackLayout m_contentStack;

        private StackLayout m_managePhotosLayout;
        private Button m_managePhotoButton;
        private Button m_addImageButton;

        private ScrollView m_contentScroll;
        private ImageGrid m_albumGrid;


        private void InitServices()
        {
            m_imageSelector = new ImageSelectionDialog();
            m_imageSerializer = new ImageSerializer();
        }


        private void GuiLayout()
        {
            m_managePhotoButton = new Button()
            {
                Text = "MANAGE PHOTOS",
                HeightRequest = UISizes.HIDDEN,
            };

            m_addImageButton = new Button()
            {
                Text = "NEW PHOTO",
                HeightRequest = UISizes.HIDDEN,
            };

            m_managePhotosLayout = new StackLayout()
            {
                Margin = new Thickness(UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD, UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD),
                HeightRequest = UISizes.HIDDEN,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    m_managePhotoButton,
                    new HorizontalSpacer(UISizes.PADDING_WIDE),
                    m_addImageButton,
                }
            };

            m_albumGrid = new ImageGrid();

            m_contentScroll = new ScrollView()
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.PADDING_NONE,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = m_albumGrid,
            };

            m_contentStack = new StackLayout()
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.PADDING_NONE,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    m_managePhotosLayout,
                    m_contentScroll,
                },
            };

            Style = (Style)Application.Current.Resources["contentPageStyle"];
            Content = m_contentStack;
        }


        private void PopulatePlaceholders()
        {
            //TODO: expand add proper image expansion
            SetEditButtonVisibility(true);
            m_loadedProfile = MockupPages.DummyProfile.User;
            for (int i = 0; i < 8; i++)
            {
                m_albumGrid.PopulatePlaceholders(12,ImageExpansionPage.ExpandImage(UIImages.BIKEPLACEHOLDER),DeleteImageDialog(MockupPages.DummyProfile.Bike));
            }
        }

        private void PopulateContent(User userProfile)
        {
            m_loadedProfile = userProfile;
            PopulatePlaceholders();
        }

        private void SetEditButtonVisibility(bool visible)
        {
            if (visible)
            {
                m_managePhotoButton.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_managePhotoButton.Clicked += ToggleEditing;

                m_addImageButton.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_addImageButton.Clicked += AddNewPhoto;


                m_managePhotosLayout.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
            }
            else
            {
                m_managePhotoButton.HeightRequest = UISizes.HIDDEN;
                m_managePhotoButton.Clicked -= ToggleEditing;

                m_addImageButton.HeightRequest = UISizes.HIDDEN;
                m_addImageButton.Clicked -= AddNewPhoto;

                m_managePhotosLayout.HeightRequest = UISizes.HIDDEN;
            }
        }


        /**
         *  Shows UI for creating new image entry in the user's garage
         */
        private async void AddNewPhoto(object sender, EventArgs e)
        {
            string selectedImagePath = await m_imageSelector.GetImage();
            m_albumGrid.AddItem(selectedImagePath);
        }


        /**
         *  Expands image to fullscreen for viewing
         */
        private void ExpandImage(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        /**
         *  returns an eventhandler which puts up a dialog allowing the user to delete a image from their profile
         */
        private EventHandler DeleteImageDialog(Bike image)
        {

            // TODO: figure out image deletion
            return (sender, e) =>
            {
                string ActionTaken = DisplayActionSheet("Are you sure you want to delete this image?", "Cancel", "Delete").Result;
                switch (ActionTaken)
                {
                    case "Delete":
                        DeleteImage(image);
                        break;
                    default:
                        break;
                }
            };
        }

        private void DeleteImage(Bike image)
        {
            //TODO: integrate server funcs to delete an album image
        }


        /**
         *  Shows or hides editing buttons on the card grid
         */
        private void ToggleEditing(object sender, EventArgs e)
        {
            //TODO: enable image deletion
        }
    }
}
