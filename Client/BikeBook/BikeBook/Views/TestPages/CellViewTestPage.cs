using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views.TestPages
{
    /**
     * Test page for testing Cell templates
     */
    public class CellViewTestPage : ContentPage
    {
        /**
         * Class constructor
         */
        public CellViewTestPage()
        {
            Device.BeginInvokeOnMainThread(() => GuiLayout());
        }


        /**
         * Lays out UI elements
         */
        private void GuiLayout()
        {
            var BaseLayout = new GeneralPageTemplate();
            BaseLayout.Scrolled += new EventHandler<ScrolledEventArgs>(scrollToPause);
            Content = BaseLayout;

            ContentCellTemplate TemplateBasic = new ContentCellTemplate()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Text = "Test\nTest\nTest",
            };

            TemplateBasic.RightNestedContent.Children.Add(
                new BoxView()
                {
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    BackgroundColor = Color.Red,
                }
            );

            BuySellCell BuySellTemplate = new BuySellCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                HeadLine = "Bike For Sale",
                Price = 3.50,
                PostTime = DateTime.Now.AddHours(-10),
            };

            GearCell GearCellTemplate = new GearCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Headline = "Motorcycle",
                Subtitle = "2011 Rocket Propelled Potato",
            };

            ChatConversationCell ChatCellTemplate = new ChatConversationCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Name = "John Doe",
                PreviewFormatted = new FormattedString()
                {
                    Spans =
                    {
                        new Span()
                        {
                            Text = "Jimmy John",
                            ForegroundColor = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT),
                        },
                        new Span()
                        {
                            Text = " owes me $12\nDefinitely not buthurt about that...",
                            ForegroundColor = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT),
                        }
                    },
                },
                PostTime = DateTime.Now.AddDays(-7),
            };

            ChatReceivedTextCell ChatReceiveCellLongTemplate = new ChatReceivedTextCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Text = "This message should wrap lines if I ramble on long enough, hey did you hear about the origin storyof the teddy bear? It's a neat piece of american history htat makes you realize that craze-based consumerism isn't nearly as modern as we all think"
            };

            ViewCell ChatReceiveCellLong = new ViewCell() { View = ChatReceiveCellLongTemplate };
            ChatReceiveCellLong.Tapped += new EventHandler(scrollToPause);

            ChatReceivedTextCell ChatReceiveCellSHortTemplate = new ChatReceivedTextCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Text = "k",
            };

            ChatSentTextCell ChatSentCellLongTemplate = new ChatSentTextCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Text = "This message should wrap lines if I ramble on long enough, hey did you hear about the origin storyof the teddy bear? It's a neat piece of american history htat makes you realize that craze-based consumerism isn't nearly as modern as we all think"
            };

            ViewCell ChatSentCellLong = new ViewCell() { View = ChatSentCellLongTemplate };
            ChatReceiveCellLong.Tapped += new EventHandler(scrollToPause);

            ChatSentTextCell ChatSentCellSHortTemplate = new ChatSentTextCell()
            {
                ImageSource = UIImages.BIKEPLACEHOLDER,
                Text = "k",
            };

            UserProfileCell UserProfileCellTemplate = new UserProfileCell(new User()
            {
                name = "Ted Kaczynsky",
                picture = ImageSerializer.SAMPLEIMAGE,
            });

           // AddImageCell AddImageCell = new AddImageCell();

            BaseLayout.Content = new TableView
            {
                Intent = TableIntent.Form,
                HasUnevenRows = true,
                RowHeight = 10,
                Root = new TableRoot()
                {
                    new TableSection()
                    {
                        new ViewCell() {View = TemplateBasic},
                        new ViewCell() {View = BuySellTemplate},
                        new ViewCell() {View = GearCellTemplate },
                        new ViewCell() {View = ChatCellTemplate },
                        ChatReceiveCellLong,
                        new ViewCell() {View = ChatReceiveCellSHortTemplate },
                        ChatSentCellLong,
                        new ViewCell() {View = ChatSentCellSHortTemplate },
                        new ViewCell() {View = UserProfileCellTemplate },
                        //new ViewCell() {View = AddImageCell },
                    }
                }
            };
        }

        private void scrollToPause(Object Sender, EventArgs e)
        {
            int a = 1;
        }
    }
}
