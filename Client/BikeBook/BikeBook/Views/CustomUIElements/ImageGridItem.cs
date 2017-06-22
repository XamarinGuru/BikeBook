using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class ImageGridItem : ContentView
    {
        public ImageGridItem()
        {
            GuiLayout();
            PopulatePlaceholders();
        }

        public ImageSource ImageSource
        {
            get { return m_contentImage.Source; }
            set { m_contentImage.Source = value; }
        }

        public event EventHandler DeleteTapped
        {
            add { m_deletionTapCatcher.AddSingleTapHandler(value); }
            remove { m_deletionTapCatcher.RemoveSingleTapHandler(value); }
        }

        public event EventHandler ImageTapped
        {
            add { m_contentImage.AddSingleTapHandler(value); }
            remove { m_contentImage.RemoveSingleTapHandler(value); }
        }

        public bool Editable
        {
            get
            {
                return m_editable;
            }
            set {
                m_editable = value;
                SetEditButtonVisibility(m_editable);
            }
        }

        private AbsoluteLayout m_mainLayout;
        private BoxView m_deletionTapCatcher;
        private CircleImage m_deletionIcon;
        private Image m_contentImage;
        private bool m_editable;


        private void GuiLayout()
        {
            m_contentImage = new Image()
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = UISizes.IMAGEGRID_ITEM_SIZE,
                WidthRequest = UISizes.IMAGEGRID_ITEM_SIZE,
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
            };

            m_deletionTapCatcher = new BoxView()
            {
                HeightRequest = UISizes.HIDDEN,
                WidthRequest = UISizes.IMAGEGRID_ITEM_SIZE,
                //BackgroundColor = Color.Transparent,
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
            };

            m_deletionIcon = new CircleImage()
            {
                Source = UIImages.ICON_DELETE,
                Aspect = Aspect.AspectFill,
                HeightRequest = UISizes.HIDDEN,
                WidthRequest = UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE,
            };

            m_mainLayout = new AbsoluteLayout()
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.MARGIN_NONE,
                HeightRequest = UISizes.IMAGEGRID_ITEM_SIZE,
                WidthRequest = UISizes.IMAGEGRID_ITEM_SIZE,
            };

            m_mainLayout.Children.Add(
                m_contentImage,
                new Rectangle(.5, .5, UISizes.IMAGEGRID_ITEM_SIZE, UISizes.IMAGEGRID_ITEM_SIZE),
                AbsoluteLayoutFlags.PositionProportional
             );

            m_mainLayout.Children.Add(
                m_deletionTapCatcher,
                new Rectangle(0, 0, UISizes.IMAGEGRID_ITEM_SIZE, UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE + 2* UISizes.PADDING_NARROW),
                AbsoluteLayoutFlags.None
             );

            m_mainLayout.Children.Add(
                m_deletionIcon,
                new Rectangle(UISizes.IMAGEGRID_ITEM_SIZE - UISizes.PADDING_NARROW - UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE, UISizes.PADDING_NARROW, UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE, UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE),
                AbsoluteLayoutFlags.None
             );

            m_editable = false;

            Content = m_mainLayout;
        }

        private void PopulatePlaceholders()
        {
            m_contentImage.Source = UIImages.ADDUSERIMAGE;
        }


        private void SetEditButtonVisibility(bool editable)
        {
            m_deletionTapCatcher.IsVisible = editable;
            m_deletionIcon.IsVisible = editable;
            if ( editable )
            {
                m_deletionTapCatcher.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_deletionIcon.HeightRequest = UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE;
            }
            else
            {
                m_deletionTapCatcher.HeightRequest = UISizes.HIDDEN;
                m_deletionIcon.HeightRequest = UISizes.HIDDEN;
            }
        }
    }
}
