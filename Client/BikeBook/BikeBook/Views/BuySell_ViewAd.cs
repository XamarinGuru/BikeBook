using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using ClientWebService;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;

namespace BikeBook.Views
{
    public class BuySellViewAd : ContentPage
	{

        public ImageSource ImageSource
        {
            get
            {
                return m_headerImage.Source;
            }
            set
            {
                m_headerImage.Source = value;
            }
        }

        public Ad Ad
        {
            get
            {
                return m_ad;
            }
            set
            {
                PopulateContent(value);
            }
        }

        private Ad m_ad;

        private GeneralPageTemplate m_mainTemplate;
        private StackLayout m_mainLayout;
        private TableSection m_adListing;

        private Image m_headerImage;
        private Button m_chatButton;
        private Label m_price;

        private ColumnDefinition m_infoKeyColumn;
        private ColumnDefinition m_infoValueColumn;
        private Grid m_infoGrid;

        private Label m_descriptionLabel;
        private Editor m_description;

        public BuySellViewAd()
        {
            GuiLayout();
        }

        public BuySellViewAd(Ad ad)
        {
            GuiLayout();
            PopulateContent(ad);
        }


        private void GuiLayout()
        {
            m_headerImage = new Image()
            {
                HeightRequest = UISizes.BANNER_IMAGE_HEIGHT,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            m_chatButton = new Button()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_DEFAULT_BUTTON_BACKGROUND),
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                HorizontalOptions = LayoutOptions.Fill,
                Text = "CHAT",
            };
            m_chatButton.Clicked += NavigateMessageChat;

            m_price = new Label()
            {
                 TextColor = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT),
                 FontAttributes = FontAttributes.Bold,
                 HorizontalOptions = LayoutOptions.Start,
            };

            m_infoKeyColumn = new ColumnDefinition()
            {
                Width = new GridLength(.3, GridUnitType.Star),
            };
            m_infoValueColumn = new ColumnDefinition()
            {
                Width = new GridLength(.7, GridUnitType.Star),
            };

            m_infoGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.Fill,
                ColumnDefinitions =
                {
                    m_infoKeyColumn,
                    m_infoValueColumn,
                },
            };

            m_descriptionLabel = new Label()
            {
                Text = "DESCRIPTION",
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
            };

            m_description = new Editor()
            {
                //IsEnabled = false,
                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Entry)),
                TextColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                BackgroundColor = Color.Transparent,
            };

            m_mainLayout = new StackLayout()
            {
                Margin = UISizes.MARGIN_STANDARD,
                Children =
                {
                    m_headerImage,
                    m_chatButton,
                    m_price,
                    m_infoGrid,
                    m_descriptionLabel,
                    m_description,
                },
            };

            m_mainTemplate = new GeneralPageTemplate()
            {
                Content = m_mainLayout,
            };
            Content = m_mainTemplate;
        }

        private void NavigateMessageChat(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagesChat(Ad.owner));
        }

        private void PopulateContent(Ad ad)
        {
            ImageSerializer serializer = new ImageSerializer();
            ConvertDate dateConverter = new ConvertDate();
            m_ad = ad;
            ImageSource = serializer.DeserializeImageToCache(ad.picture);
            AddInfoGridItem("Title", ad.title);
            AddInfoGridItem("Posted", dateConverter.FromJava(ad.created_at).ToAgeString());
            AddInfoGridItem("Location", ad.city + ", " + ad.provence);
            m_description.Text = ad.description;
        }

        private void AddInfoGridItem(string key, string value)
        {
            RowDefinition newRow = new RowDefinition();
            m_infoGrid.RowDefinitions.Add(newRow);
            Label keyLabel = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = key,
            };
            m_infoGrid.Children.Add(keyLabel, m_infoGrid.ColumnDefinitions.IndexOf(m_infoKeyColumn), m_infoGrid.RowDefinitions.IndexOf(newRow));
            Label valueLabel = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                LineBreakMode = LineBreakMode.TailTruncation,
                Text = value,
            };
            m_infoGrid.Children.Add(valueLabel, m_infoGrid.ColumnDefinitions.IndexOf(m_infoValueColumn), m_infoGrid.RowDefinitions.IndexOf(newRow));
        }
    }
}
