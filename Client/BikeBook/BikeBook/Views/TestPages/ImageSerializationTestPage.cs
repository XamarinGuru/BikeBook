using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views.TestPages
{

    /**
     * Form to collect user credentials for new account
     */
    public class ImageSerializationTestPage : ContentPage
    {
        /**
         * Class constructor. Creates new blank profile to edit 
         */
        public ImageSerializationTestPage()
        {
            m_imageSelectionDialog = new ImageSelectionDialog();

            Device.BeginInvokeOnMainThread(() => GuiLayout());
        }

        private ImageSelectionDialog m_imageSelectionDialog;

        private Image m_image_addProfileImage;
        private Label m_savedImagePath;
        private Image m_deserializedImage;
        private Label m_deserializedImagePath;
        private StackLayout m_StackLayout_MainLayout;

        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_image_addProfileImage = new Image
            {
                Source = UIImages.ADDUSERIMAGE,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Margin = UISizes.MARGIN_EXTRA_WIDE,
                HeightRequest = UISizes.PROFILE_IMAGE_HEIGHT,
                WidthRequest = UISizes.PROFILE_IMAGE_WIDTH
            };

            m_savedImagePath = new Label()
            {
                TextColor = Color.White
            };

            m_deserializedImage = new Image
            {
                Source = UIImages.ADDUSERIMAGE,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Margin = UISizes.MARGIN_EXTRA_WIDE,
                HeightRequest = UISizes.PROFILE_IMAGE_HEIGHT,
                WidthRequest = UISizes.PROFILE_IMAGE_WIDTH
            };

            m_deserializedImagePath = new Label()
            {
                TextColor = Color.White
            };

            m_StackLayout_MainLayout = new StackLayout
            {
                Children = {
                    m_image_addProfileImage,
                    m_savedImagePath,
                    m_deserializedImage
                }
            };

            Title = "CREATE ACCOUNT";

            Content = m_StackLayout_MainLayout;
            BackgroundColor = Color.Black;


            m_image_addProfileImage.AddSingleTapHandler(addProfileImage_Tapped);
        }


        /**
         * Callback to handle user clicking on add photo button
         *
         * @param object sender - TODO
         * @param EventArgs e - TODO
         */
        private async void addProfileImage_Tapped(object sender, EventArgs e)
        {
            ImageSerializer Serializer = new ImageSerializer();
            m_savedImagePath.Text = await m_imageSelectionDialog.GetImage();
            ((Image)sender).Source = m_savedImagePath.Text;
            string SerializedImage =  Serializer.SerializeFromFile(m_savedImagePath.Text);
            await DisplayAlert("Image Serialized", SerializedImage, "OK");
            ImageSource createdSource =  Serializer.DeserializeImageToCache(SerializedImage);
            m_deserializedImage.Source = createdSource;
        }



    }
}
