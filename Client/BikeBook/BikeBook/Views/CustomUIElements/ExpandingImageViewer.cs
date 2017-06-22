using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.ComponentModel;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class ExpandingImageViewer : ContentView
    {
        public ImageSource Source
        {
            get
            {
                return m_contentImage.Source;
            }
            set
            {
                m_contentImage.Source = value;
            }
        }

        public string ContentImagePath
        {
            get
            {
                return m_contentImagePath;
            }
            set
            {
                m_contentImagePath = value;
                if (value != null)
                    m_contentImage.Source = value;
                else
                    m_contentImage.Source = null;
            }
        }

        public bool Closeable
        {
            get
            {
                return m_closeable;
            }
            set
            {
                m_closeable = value;
                UpdateVisibilities();
            }
        }

        // thrown when the image is being expanded
        public event EventHandler Expanding;

        // thrown when the image is being collapsed
        public event EventHandler Collapsing;

        public ExpandingImageViewer()
        {
            GuiLayout();
        }


        private string m_contentImagePath;
        private bool m_closeable;

        private AbsoluteLayout m_contentImageLayout;
        private Image m_contentImage;
        private Image m_removeImageButton;


        private void GuiLayout()
        {
            m_contentImage = new Image()
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = UISizes.HIDDEN,
                IsVisible = false,
            };
            m_contentImage.PropertyChanged += MakeImageVisibleIfPopulated;
            m_contentImage.AddSingleTapHandler(ImageExpansionPage.ExpandImage(m_contentImage));

            m_removeImageButton = new Image()
            {
                Source = UIImages.BUTTON_CLOSE,
                WidthRequest = UISizes.IMAGE_EXPANSION_PAGE_CLOSE_BUTTON_SIZE,
                HeightRequest = UISizes.HIDDEN,
                IsVisible = false,
            };
            m_removeImageButton.AddSingleTapHandler(RemoveSelectedImage);

            m_contentImageLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            m_contentImageLayout.Children.Add(m_contentImage,
                new Rectangle(.5, .5, 1, 1),
                AbsoluteLayoutFlags.All);
            m_contentImageLayout.Children.Add(m_removeImageButton,
                new Rectangle(1, 0, UISizes.IMAGE_EXPANSION_PAGE_CLOSE_BUTTON_SIZE, UISizes.SIZE_NONE_SPECIFIED),
                AbsoluteLayoutFlags.PositionProportional);

            Content = m_contentImageLayout;
            Closeable = false;
        }

        private void RemoveSelectedImage(object sender, EventArgs e)
        {
            ContentImagePath = null;
            m_contentImage.Source = null;
        }

        /**
         * PropertyChangedEventHandler for revealing an image onscreen if image to display is added.
         * 
         * @param object sender - The source of the event
         * @param PropertyChangedEventArgs e - System.PropertyChangedEventArgs containing old and new text values
         */
        private void MakeImageVisibleIfPopulated(object sender, PropertyChangedEventArgs e)
        {
            if ((sender.GetType() == typeof(Image)) &&
                (e.PropertyName != null && e.PropertyName.Equals(Image.SourceProperty.PropertyName)))
            {
                UpdateVisibilities();
            }
        }

        private void UpdateVisibilities()
        {
            m_contentImage.IsVisible = m_contentImage.Source != null;
            m_contentImage.HeightRequest = m_contentImage.IsVisible ? UISizes.BANNER_IMAGE_HEIGHT : UISizes.HIDDEN;

            m_removeImageButton.IsVisible = (m_closeable && m_contentImage.IsVisible);
            m_removeImageButton.HeightRequest = m_removeImageButton.IsVisible ? UISizes.IMAGE_EXPANSION_PAGE_CLOSE_BUTTON_SIZE : UISizes.HIDDEN;

            if (m_contentImage.IsVisible)
            {
                if( Expanding != null )
                    Expanding(this, EventArgs.Empty);
            }
            else
            {
                if (Collapsing != null)
                    Collapsing(this, EventArgs.Empty);
            }
        }
    }
}
