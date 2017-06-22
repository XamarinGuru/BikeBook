using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class NavMenuCell : ContentView
    {
        public ImageSource ImageSource
        {
            get { return m_mainImage.Source; }
            set { m_mainImage.Source = value; }
        }

        public string Title
        {
            get { return m_title.Text; }
            set { m_title.Text = value; }
        }

        public string Detail
        {
            get { return m_detail.Text; }
            set { m_detail.Text = value; }
        }

        private Grid m_mainGrid;
        private SquareImage m_mainImage;
        private StackLayout m_innerLayout;
        private Label m_title;
        private Label m_detail;
        private Image m_arrow;


        public NavMenuCell()
        {
            m_mainImage = new SquareImage();
            m_title = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
            };
            m_detail = new Label()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT),
            };
            m_innerLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    m_title,
                    m_detail,
                },
            };
            m_arrow = new Image()
            {
                Source = UIConstants.EmbeddedImageName(UIConstants.EmbeddedImages.NavigationArrowRight),
                Aspect = Aspect.AspectFit,
            };

            m_mainGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.Fill,
                Margin = UIConstants.MARGIN_STANDARD,
                RowDefinitions =
                {
                    new RowDefinition() {Height = new GridLength(1,GridUnitType.Star)},
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition () {Width = new GridLength(UIConstants.TALKING_HEAD_IMAGE_SIZE,GridUnitType.Star) },
                    new ColumnDefinition () { Width = new GridLength(1-UIConstants.TALKING_HEAD_IMAGE_SIZE,  GridUnitType.Star) },
                    new ColumnDefinition () { Width = GridLength.Auto },
                }
            };
            m_mainGrid.Children.Add(m_mainImage, 0, 0);
            m_mainGrid.Children.Add(m_innerLayout, 1, 0);
            m_mainGrid.Children.Add(m_arrow, 2, 0);

            BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT);
            Content = m_mainGrid;
        }
    }
}
