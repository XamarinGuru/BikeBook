using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ClientWebService;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;

namespace BikeBook.Views.CustomUIElements
{
    public class CardGridItem : ContentView
    {
        public ImageSource ImageSource
        {
            set { m_mainImage.Source = value; }
        }

        public string HeadlineText
        {
            set { m_headlineLabel.Text = value; }
        }

        public string ButtonText
        {
            set { m_cardActionButton.Text = value; }
        }

        /**
         * Thrown if the bottom button on the card is tapped
         */
        public event EventHandler ButtonClicked
        {
            add { m_cardActionButton.Clicked += value; }
            remove { m_cardActionButton.Clicked -= value; }
        }

        /**
         *  Thrown if the body of the card is tapped
         */
        public event EventHandler CardTapped
        {
            add { this.AddSingleTapHandler(value); }
            remove { this.RemoveSingleTapHandler(value); }
        }


        /**
         *  Thrown if the card's edit button is tapped
         */
        public event EventHandler EditTapped
        {
            add { m_editItem.AddSingleTapHandler(value); }
            remove { m_editItem.RemoveSingleTapHandler(value); }
        }


        /**
         *  Thrown if the card's delete button is tapped
         */
        public event EventHandler DeleteTapped
        {
            add { m_deleteItem.AddSingleTapHandler(value); }
            remove { m_deleteItem.RemoveSingleTapHandler(value); }
        }

        public new View Content
        {
            set { m_nestedView.Content = value; }
        }

        public bool Editable
        {
            set
            {
                if (value != m_editable)
                {
                    m_editable = value;
                    m_editingLayout.HeightRequest = m_editable ? UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE : UISizes.HIDDEN;
                }
            }
            get { return m_editable; }
        }

        private bool m_editable;

        private StackLayout m_innerLayout;

        private Image m_editItem;
        private Image m_deleteItem;

        private StackLayout m_editingLayout;
        private CircleImage m_mainImage;
        private Label m_headlineLabel;
        private ContentView m_nestedView;
        private Button m_cardActionButton;

        public CardGridItem()
        {
            GuiLayout();
            PopulatePlaceholders();
        }


        // Placeholder func for creating card from a bike
        public CardGridItem(Bike bike)
        {
            GuiLayout();
            PopulateContent(bike);
        }

        // Placeholder func for creating card from a gear
        public CardGridItem(Gear gear)
        {
            GuiLayout();
            PopulateContent(gear);
        }

        private void GuiLayout()
        {
            m_editItem = new Image()
            {
                Source = UIImages.ICON_EDIT,
                HeightRequest = UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE,
                WidthRequest = UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE,
                HorizontalOptions = LayoutOptions.Start,
            };

            m_deleteItem = new Image()
            {
                Source = UIImages.ICON_DELETE,
                HeightRequest = UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE,
                WidthRequest = UISizes.CARD_GRID_ITEM_MODIFY_BUTTON_SIZE,
                HorizontalOptions = LayoutOptions.End,
            };

            m_editingLayout = new StackLayout
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.MARGIN_NONE,
                WidthRequest = UISizes.CARD_GRID_ITEM_WIDTH,
                HeightRequest = UISizes.HIDDEN,
                BackgroundColor = Color.Transparent,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    m_editItem,
                    new HorizontalSpacer(),
                    m_deleteItem,
                },
            };

            m_mainImage = new CircleImage()
            {
                HeightRequest = UISizes.CARD_GRID_IMAGE_SIZE,
                WidthRequest = UISizes.CARD_GRID_IMAGE_SIZE,
                Aspect = Aspect.AspectFill,
            };

            m_headlineLabel = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            m_nestedView = new ContentView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Center,
            };

            m_cardActionButton = new Button()
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent,
                BorderRadius = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                
                FontSize = Device.GetNamedSize(NamedSize.Micro,typeof(Button)),
            };

            m_innerLayout = new StackLayout()
            {
                Padding = UISizes.PADDING_STANDARD,
                Margin = new Thickness(UISizes.MARGIN_STANDARD,UISizes.MARGIN_STANDARD,UISizes.MARGIN_STANDARD,UISizes.MARGIN_NONE),
                WidthRequest = UISizes.CARD_GRID_ITEM_WIDTH,
                HeightRequest = UISizes.CARD_GRID_ITEM_HEIGHT,
                BackgroundColor = Color.Transparent,
                Children =
                {
                    m_editingLayout,
                    m_mainImage,
                    m_headlineLabel,
                    m_nestedView,
                    m_cardActionButton,
                },
            };

            base.Content = m_innerLayout;

            m_editable = false;

            WidthRequest = UISizes.CARD_GRID_ITEM_WIDTH;
            HeightRequest = UISizes.CARD_GRID_ITEM_HEIGHT;
            Padding = UISizes.MARGIN_NONE;
            Margin = UISizes.MARGIN_NONE;
            BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT);
        }


        private void PopulatePlaceholders()
        {
            ImageSource = UIImages.BIKEPLACEHOLDER;
            HeadlineText = "YAMAHA YZF-R1";
            Content = new BoxView() { Color = Color.Red, };
            ButtonText = "TEST BUTTON";
            m_cardActionButton.Clicked += DummyClickAction;
        }

        private void PopulateContent(Bike bike)
        {
            ImageSource = UIImages.BIKEPLACEHOLDER;
            HeadlineText = bike.make + " " + bike.model;
            DummyInfoLayout();
            ButtonText = String.Empty;

        }

        private void PopulateContent(Gear gear)
        {
            ImageSource = UIImages.BIKEPLACEHOLDER;
            HeadlineText = gear.make + " " + gear.model;
            DummyInfoLayout();
            ButtonText = String.Empty;

        }

        private void DummyInfoLayout()
        {
            int numRows = 4;
            int numCols = 2;

            Grid newGrid = new Grid();
            for(int i = 0; i < numCols; i++)
            { 
                newGrid.ColumnDefinitions.Add( new ColumnDefinition() { Width = GridLength.Auto });
            }
            for (int i = 0; i < numRows; i++)
            {
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }


            for (int i = 0; i < numRows * numCols; i++)
            {
                Label newLabel = new Label()
                {
                    Text = "TEST",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    TextColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                };

                newGrid.Children.Add(newLabel, i % numCols, i / numCols);
            }
            Content = newGrid;
        }

        private void DummyClickAction(object sender, EventArgs e)
        {
            Navigation.NavigationStack.Last().DisplayAlert("A button was clicked!", "", "OK");
        }
    }
}
