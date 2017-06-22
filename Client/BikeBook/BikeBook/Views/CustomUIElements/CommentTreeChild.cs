using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ImageCircle.Forms.Plugin.Abstractions;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     * UI Element showing a comment in a comment tree
     */
    public class CommentTreeChild : ContentView
    {
        private Grid m_mainLayout;

        // Nested layout to force image to square aspect ratio
        private CircleImage m_posterImage;

        //colored box to place behind comment content, because xamarin doesn't support cell background colors
        private BoxView m_contentBackground;

        //Nested layout for comment content
        private StackLayout m_contentLayout;

        // header in contentLayout containing content title, age
        private Label m_contentTitle;
        private Label m_contentAge;
        private StackLayout m_headingLayout;

        private ExpandingImageViewer m_contentImage;
        private VerticalSpacer m_contentSpacer;
        private Label m_contentText;

        private Button m_likeButton;


        /**
         * Class Constructor
         */
        public CommentTreeChild()
        {
            GuiLayout();
            PopulatePlaceholders();
        }


        /**
         * Lays out widget UI
         */
        private void GuiLayout()
        {
            m_contentTitle = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            m_contentAge = new Label()
            {
                Style = (Style)Application.Current.Resources["LabelSecondaryStyle"],
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.End,
                BackgroundColor = Color.Red,
            };

            m_headingLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    m_contentTitle,
                    m_contentAge,
                }
            };

            m_posterImage = new CircleImage()
            {
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_SMALL,
                HeightRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_SMALL,
            };

            m_likeButton = new Button
            {
                Style = (Style)Application.Current.Resources["likeButtonDarkUnlikedStyle"],
                Text = "0 Likes",
                HorizontalOptions = LayoutOptions.End,
            };

            m_contentBackground = new BoxView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex(UIColors.COLOR_NESTED_ACCENT_BACKGROUND),
            };

            m_contentImage = new ExpandingImageViewer();
            m_contentImage.Expanding += ExpandSpacer;
            m_contentImage.Collapsing += CollapseSpacer;
            

            m_contentSpacer = new VerticalSpacer(UISizes.HIDDEN);

            m_contentText = new Label()
            {
                Style = (Style)Application.Current.Resources["LabelMultilineStyle"],
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };

            m_contentLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Margin = UISizes.MARGIN_STANDARD,
                Children =
                {
                    m_headingLayout,
                    m_contentSpacer,
                    m_contentImage,
                    new VerticalSpacer(UISizes.SPACING_STANDARD),
                    m_contentText,
                    m_likeButton,
                },
            };

            m_mainLayout = new Grid()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = UISizes.SPACING_NARROW,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength( UISizes.TALKING_HEAD_IMAGE_SIZE_SMALL, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star) },
                },
            };
            m_mainLayout.Children.Add(m_posterImage, 0, 0);
            m_mainLayout.Children.Add(m_contentBackground, 1, 0);
            m_mainLayout.Children.Add(m_contentLayout, 1, 0);

            Content = m_mainLayout;
        }

        private void CollapseSpacer(object sender, EventArgs e)
        {
            m_contentSpacer.HeightRequest = UISizes.HIDDEN;
        }

        private void ExpandSpacer(object sender, EventArgs e)
        {
            m_contentSpacer.HeightRequest = UISizes.SPACING_STANDARD;
        }


        /**
         * Fills placeholders
         */
        private void PopulatePlaceholders()
        {
            m_posterImage.Source = UIImages.BIKEPLACEHOLDER;
            m_contentImage.Source = UIImages.BIKEPLACEHOLDER;
            m_contentTitle.Text = "Content Title";
            m_contentAge.Text = "1 Day ago";
            m_contentText.Text = "eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet";
        }
    }
}
