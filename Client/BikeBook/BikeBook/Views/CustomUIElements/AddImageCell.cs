using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
    * Template for showing gear items in a list
    */
    public class AddImageCell : ContentView
    {
        private ContentCellTemplate m_baseTemplate;
        
        private FormattedString m_formattedText;
        private Span m_headLineSpan;

        private ImageSelectionDialog m_imageSelectionDialog;
        private string m_selectedImagePath;

        public ImageSource ImageSource
        {
            get { return m_selectedImagePath; }
            set { m_baseTemplate.ImageSource = value; }
        }

        /**
        * Class Constructor
        */
        public AddImageCell()
        {
            GuiLayout();
        }


        /**
         * Gets selected image as a base64 string
         */
        public string GetSelectedImageBase64()
        {
            ImageSerializer serializer = new ImageSerializer();
            return serializer.SerializeFromFile(m_selectedImagePath);
        }


        /**
        * Lays out and displays template UI
        */
        private void GuiLayout()
        {
            m_baseTemplate = new ContentCellTemplate()
            {
                BackgroundColor = Color.Transparent,
            };

            m_headLineSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_TERTIARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = "Upload Picture",
            };

            m_formattedText = new FormattedString();
            m_formattedText.Spans.Add(m_headLineSpan);

            m_baseTemplate.FormattedText = m_formattedText;
            m_baseTemplate.VerticalTextAlignment = TextAlignment.Center;
            m_baseTemplate.ImageSource = UIImages.ADDUSERIMAGE;
            
            Content = m_baseTemplate;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(propertyName.Equals("Renderer"))
            {
                if ((Navigation.NavigationStack != null) &&
                   (Navigation.NavigationStack.Count > 0))
                {
                    m_baseTemplate.AddSingleTapHandler(addImageTapped);
                    Page CurrentPage = Navigation.NavigationStack.Last();
                    if (CurrentPage != null)
                    {
                        m_imageSelectionDialog = new ImageSelectionDialog();
                    }
                }
            }
        }

        private async void addImageTapped(object sender, EventArgs e)
        {
            m_selectedImagePath = await m_imageSelectionDialog.GetImage();
            if( System.IO.Path.HasExtension( m_selectedImagePath))
                m_baseTemplate.ImageSource = m_selectedImagePath;
        }
    }
}
