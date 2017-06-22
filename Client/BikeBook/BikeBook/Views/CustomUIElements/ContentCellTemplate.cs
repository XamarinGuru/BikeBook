using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{


    /**
     * Template for showing items in a list
     */
    public class ContentCellTemplate : ContentView
    {
        public CircleImage NestedImage
        {
            get { return m_talkingHeadImage; }
        }

        public ImageSource ImageSource
        {
            get{ return m_talkingHeadImage.Source; }
            set{ m_talkingHeadImage.Source = value; }
        }

        public string Text
        {
            get { return m_centralText.Text; }
            set { m_centralText.Text = value; }
        }

        public FormattedString FormattedText
        {
            get { return m_centralText.FormattedText; }
            set { m_centralText.FormattedText = value; }
        }

        public TextAlignment VerticalTextAlignment
        {
            get { return m_centralText.VerticalTextAlignment; }
            set { m_centralText.VerticalTextAlignment = value; }
        }

        public StackLayout CenterNestedContent;
        public StackLayout RightNestedContent;

        private Grid m_mainLayout;
        private CircleImage m_talkingHeadImage;
        private Label m_centralText;


        /**
         * Class constructor
         */
        public ContentCellTemplate()
        {
            GuiLayout();
        }


        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_mainLayout = new Grid()
            {
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Padding = UISizes.PADDING_STANDARD,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(UISizes.TALKING_HEAD_IMAGE_SIZE_LARGE,GridUnitType.Absolute) },
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition {Width = new GridLength(UISizes.TALKING_HEAD_IMAGE_SIZE_LARGE,GridUnitType.Absolute) },
                },
            };

            m_talkingHeadImage = new CircleImage()
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_LARGE,
                WidthRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_LARGE,
                Source = UIImages.PHOTOPLACEHOLDER,
                VerticalOptions = LayoutOptions.Start,
            };
            m_mainLayout.Children.Add(m_talkingHeadImage, 0,0);

            m_centralText = new Label()
            {
                LineBreakMode = LineBreakMode.CharacterWrap,
                VerticalOptions = LayoutOptions.Center,
            };

            CenterNestedContent = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    m_centralText,
                }
            };

            m_mainLayout.Children.Add(CenterNestedContent, 1,0);

            RightNestedContent = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                Padding = 0,
                Margin = 0,
            };
            m_mainLayout.Children.Add(RightNestedContent,2,0);

            Content = m_mainLayout;
        }
    }
}
