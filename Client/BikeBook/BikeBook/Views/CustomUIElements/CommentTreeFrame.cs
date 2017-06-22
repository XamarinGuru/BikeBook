using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ImageCircle.Forms.Plugin.Abstractions;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{


    /**
     * UI widget to display an item, such as an image, and display comments.
     */
    public class CommentTreeFrame : ContentView
    {
        CircleImage m_posterImage;
        BoxView m_dropLine;
        ContentView m_contentHeader;
        StackLayout m_contentLayout;
        ContentView m_contentEntry;
        StackLayout m_innerLayout;
        Grid m_mainLayout;


        /**
         * Class constructor
         */
        public CommentTreeFrame()
        {
            GuiLayout();
        }

        public void AddTalkingHead()
        {
            m_posterImage.Source = UIImages.BIKEPLACEHOLDER;
        }

        public void AddChild()
        {
            m_contentLayout.Children.Add(new CommentTreeChild());
        }

        public void AddHeader()
        {
            m_contentHeader.Content = new CommentTreeHeader();
        }

        public void AddHeader(Post post)
        {
            m_contentHeader.Content = new CommentTreeHeader(post);
        }

        public void ShowEntry()
        {
            m_contentEntry.Content = new CommentTreeEntry();
        }


        /**
         * Lays out the widget's GUI components
         */
        private void GuiLayout()
        {
            m_posterImage = new CircleImage()
            {
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_MEDIUM,
                WidthRequest = UISizes.TALKING_HEAD_IMAGE_SIZE_MEDIUM,
            };

            m_dropLine = new DropLine()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
                HorizontalOptions = LayoutOptions.Center,
            };

            m_contentHeader = new ContentView();
            m_contentLayout = new StackLayout() { Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"], };
            m_contentEntry = new ContentView();

            m_innerLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    m_contentHeader,
                    m_contentLayout,
                    m_contentEntry,
                }
            };

            m_mainLayout = new Grid()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(UISizes.TALKING_HEAD_IMAGE_SIZE_MEDIUM,GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                },  
            };
            m_mainLayout.Children.Add(m_dropLine, 0, 0);
            m_mainLayout.Children.Add(m_posterImage, 0, 0);
            m_mainLayout.Children.Add(m_innerLayout, 1, 0);
            Content = m_mainLayout;
        }
    }
}
