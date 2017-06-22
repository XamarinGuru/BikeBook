using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace BikeBook.Views
{

    
    /**
     * Shows menu requesting the user supply an image
     *  from their device, either by taking a new one or choosing an existing one
     */
    class ImageSelectionDialog
    {
        /**
         * Class Constructor
         * 
         */
        public ImageSelectionDialog()
        {
        }

        /**
         * Shows UI to allow user ot insert an image
         * 
         * @param Page page - Xamarin.Forms.Page requesting an image
         * @param Image image - Xamarin.Forms.Image to insert selected image into
         */
        public async Task<string> GetImage()
        {
            await CrossMedia.Current.Initialize();

            Page CurrentPage = Application.Current.MainPage.Navigation.NavigationStack.Last();
            MediaFile retrievedImage = null;
            string selectedPath = null;

            const int CANCEL = 0;
            const int TAKE_PHOTO = 1;
            const int PICK_PHOTO = 2;

            List<String> photoActions = new List<string> { "Cancel", "Take a new photo", "Pick an existing photo" };
            string selectedAction = string.Empty ;

            bool takePhotoAvailable = CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported;
            bool pickPhotoAvailable = CrossMedia.Current.IsPickPhotoSupported;


            if (takePhotoAvailable && pickPhotoAvailable)
            {
                selectedAction = await CurrentPage.DisplayActionSheet("Add a photo", photoActions[CANCEL], null, photoActions[TAKE_PHOTO], photoActions[PICK_PHOTO]);
            }
            else if (takePhotoAvailable)
            {
                selectedAction = await CurrentPage.DisplayActionSheet("Add a photo", photoActions[CANCEL], null, photoActions[TAKE_PHOTO]);
            }
            else if (pickPhotoAvailable)
            {
                selectedAction = await CurrentPage.DisplayActionSheet("Add a photo", photoActions[CANCEL], null, photoActions[PICK_PHOTO]);
            }
            else // no photo methods available
            {
                await CurrentPage.DisplayAlert("No Photos or Camera Available", "", "OK");
            }

            switch (photoActions.IndexOf(selectedAction))
            {
                case TAKE_PHOTO:
                    retrievedImage = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "BikeBook",
                        Name = "Image_" + DateTime.Now.ToString("yyyy_MM_dd_HH_MM_ss") + ".jpg",
                        AllowCropping = true,
                        CompressionQuality = 50,
                    });
                    
                    break;
                case PICK_PHOTO:
                    retrievedImage = await CrossMedia.Current.PickPhotoAsync();
                    break;
                default:
                    break;
            }

            if (retrievedImage != null)
            {
                selectedPath = retrievedImage.Path;
            }

            return selectedPath;
        }
    }
}
