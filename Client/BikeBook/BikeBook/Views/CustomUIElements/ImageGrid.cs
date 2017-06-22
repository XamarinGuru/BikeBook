using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class ImageGrid : ContentView
    {
        public string Title
        {
            set { m_title.Text = value.ToUpper(); }
            get { return m_title.Text; }
        }

        public event EventHandler TitleTapped
        {
            add { m_titleLayout.AddSingleTapHandler(value); }
            remove { m_titleLayout.RemoveSingleTapHandler(value); }
        }

        public bool Editable
        {
            get { return m_editable; }
            set
            {
                m_editable = value;
                SetItemEditability(m_editable);
            }
        }


        public ImageGrid()
        {
            m_gridItems = new List<ImageGridItem>();
            GuiLayout();
        }

        public void PopulatePlaceholders(int count = 6, EventHandler tapCallBack = null, EventHandler deleteCallback = null)
        {
            Title = "Test Title";
            for (int i = 0; i < count; i++)
            {
                this.AddItem(UIImages.BIKEPLACEHOLDER, tapCallBack, deleteCallback);
            }
        }

        public void AddItem(ImageSource image, EventHandler tappedcallback = null, EventHandler deletioncallback = null)
        {
            ImageGridItem newItem = new ImageGridItem();
            newItem.ImageSource = image;
            newItem.ImageTapped += tappedcallback;
            newItem.DeleteTapped += deletioncallback;
            newItem.Editable = m_editable;

            int leftOffset = m_innerGrid.Children.Count % IMAGE_GRID_NUM_IMAGES_WIDE;
            int downOffset = m_innerGrid.Children.Count / IMAGE_GRID_NUM_IMAGES_WIDE;

            // Add a new row if the current one is full
            if(leftOffset == 0)
            {
                m_innerGrid.RowDefinitions.Add(new RowDefinition() { Height = UISizes.IMAGEGRID_ITEM_SIZE });
            }

            m_innerGrid.Children.Add(newItem, m_innerGrid.Children.Count % IMAGE_GRID_NUM_IMAGES_WIDE, downOffset);
            m_gridItems.Add(newItem);
        }

        private bool m_editable;

        private StackLayout m_mainLayout;
        private StackLayout m_titleLayout;
        private Label m_title;
        private Image m_titleChevron;
        private Grid m_innerGrid;

        private List<ImageGridItem> m_gridItems;

        private const int IMAGE_GRID_NUM_IMAGES_WIDE = 3;
        private void GuiLayout()
        {
            m_title = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
            };

            m_titleChevron = new Image()
            {
                Source = UIImages.CHEVRON_RIGHT,
                HeightRequest = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
            };

            m_titleLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    m_title,
                    m_titleChevron,
                }
            };

            m_innerGrid = new Grid()
            {
                Padding = UISizes.PADDING_NONE,
                RowSpacing = UISizes.SPACING_STANDARD,
                ColumnSpacing = UISizes.SPACING_STANDARD,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = UISizes.IMAGEGRID_ITEM_SIZE },
                    new ColumnDefinition() { Width = UISizes.IMAGEGRID_ITEM_SIZE },
                    new ColumnDefinition() { Width = UISizes.IMAGEGRID_ITEM_SIZE },
                },
            };

            m_mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = UISizes.SPACING_NARROW,
                Children =
                {
                    m_titleLayout,
                    m_innerGrid,
                }
            };

            m_editable = false;

            Content = m_mainLayout;
        }

        private void SetItemEditability(bool editable)
        {
            foreach(ImageGridItem item in m_gridItems)
            {
                item.Editable = editable;
            }
        }

    }
}
