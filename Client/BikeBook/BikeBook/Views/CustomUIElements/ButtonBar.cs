using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BikeBook.Views
{


    /**
     * Compound view providing toolbar-like behavior in Xamarin.Forms
     */
    class ButtonBar : ContentView
    {
        private Grid m_buttonGrid;
        private double m_itemHeight;
        private Color m_buttonBackGroundColor;
        private Color m_buttonTextColor;
        private double m_textButtonFontSize;

        public new double HeightRequest
        {
            set
            {
                m_itemHeight = value;
                m_buttonGrid.RowDefinitions[0].Height = m_itemHeight;
                foreach (View gridItem in m_buttonGrid.Children)
                {
                    gridItem.HeightRequest = m_itemHeight;
                }
            }
            get
            {
                return m_itemHeight;
            }
        }

        public Color ButtonBackGroundColor
        {
            set
            {
                m_buttonBackGroundColor = value;
                foreach (View gridItem in m_buttonGrid.Children)
                {
                    gridItem.BackgroundColor = m_buttonBackGroundColor;
                }
            }
            get
            {
                return m_buttonBackGroundColor;
            }
        }

        public Color TextColor
        {
            set
            {
                m_buttonTextColor = value;
                foreach (View gridItem in m_buttonGrid.Children)
                {
                    try
                    {
                        Button gridTextButton = (Button)gridItem;
                        gridTextButton.TextColor = m_buttonTextColor;
                    }
                    catch
                    { }
                }
            }
            get
            {
                return m_buttonTextColor;
            }
        }

        public double FontSize
        {
            set
            {
                if (value > 0)
                {
                    m_textButtonFontSize = value;
                    foreach (View gridItem in m_buttonGrid.Children)
                    {
                        try
                        {
                            Button gridTextButton = (Button)gridItem;
                            gridTextButton.FontSize = m_textButtonFontSize;
                        }
                        catch
                        { }
                    }
                }
            }
            get
            {
                return m_textButtonFontSize;
            }
        }

        /**
         * Class Constructor
         */
        public ButtonBar()
        {
            GuiLayout();
        }

        private void GuiLayout()
        {
            m_itemHeight = UISizes.TOOLBAR_HEIGHT;
            m_textButtonFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button));
            m_buttonTextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT);
            m_buttonBackGroundColor = Color.Transparent;

            m_buttonGrid = new Grid()
            {
                Padding = UISizes.PADDING_STANDARD,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(m_itemHeight, GridUnitType.Absolute)}
                },
            };
            Content = m_buttonGrid;
        }


        /**
         * Adds an image button to the bar
         * 
         * @param ImageSource imageButtonSource - the image to display as a button
         * @param EventHandler callback - System.EventHandler delegate to respond to tap events
         */
        public void AddImageButton(ImageSource imageButtonSource, EventHandler callback)
        {
            if (imageButtonSource != null)
            {
                Image imageButton = new Image() { Source = imageButtonSource, HeightRequest = m_itemHeight, BackgroundColor = m_buttonBackGroundColor, Aspect = Aspect.AspectFit};
                if (callback != null)
                    imageButton.AddSingleTapHandler(callback);
                m_buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                m_buttonGrid.Children.Add(imageButton, m_buttonGrid.ColumnDefinitions.Count - 1, 0);
            }       
        }


        /**
         * Adds a text button to the bar
         * 
         * @param String text - the text to display on the button
         * @param EventHandler callback - System.EventHandler delegate to respond to tap events
         */
        public void AddTextButton(String buttonText, EventHandler callBack)
        {
            if(buttonText != null && buttonText.Length > 0)
            {
                Button newButton = new Button()
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Text = buttonText,
                    FontSize = m_textButtonFontSize,
                    HeightRequest = m_itemHeight,
                    BackgroundColor = m_buttonBackGroundColor,
                    TextColor = m_buttonTextColor,
                };
                if(callBack != null)
                    newButton.Clicked += callBack;
                m_buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                m_buttonGrid.Children.Add(newButton, m_buttonGrid.ColumnDefinitions.Count - 1, 0);
            }
        }
    }
}
