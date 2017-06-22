 using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;

namespace BikeBook.Views
{
    public class UIStyles
    {
        public static ResourceDictionary BuildStyleDictionary()
        {
            ResourceDictionary StyleResources = new ResourceDictionary();

            Style contentPageStyle = new Style(typeof(ContentPage))
            {
                Setters =
                {
                    new Setter {Property = ContentPage.PaddingProperty, Value = UISizes.PADDING_NONE },
                    new Setter {Property = ContentPage.BackgroundColorProperty, Value = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND) },
                }
            };
            StyleResources.Add("contentPageStyle", contentPageStyle);

            Style listViewStyle = new Style(typeof(ListView))
            {
                Setters =
                {
                    new Setter {Property = ListView.SeparatorVisibilityProperty, Value = false },
                }
            };
            StyleResources.Add(listViewStyle);

            Style buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.FromHex(UIColors.COLOR_DEFAULT_BUTTON_BACKGROUND) },
                    new Setter {Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) },
                    new Setter {Property = Button.BorderColorProperty, Value = Color.Transparent },
                    new Setter {Property = Button.MinimumWidthRequestProperty, Value = UISizes.BUTTON_MIN_WIDTH }
                }
            };
            StyleResources.Add(buttonStyle);

            Style LabelPrimaryStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = Label.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Label.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                    new Setter {Property = Label.LineBreakModeProperty, Value = LineBreakMode.TailTruncation, },
                }
            };
            StyleResources.Add(LabelPrimaryStyle);

            Style submitButtonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.FromHex(UIColors.COLOR_SUBMIT_BUTTON_BACKGROUND) },
                    new Setter {Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) },
                    new Setter {Property = Button.BorderColorProperty, Value = Color.Transparent },
                    new Setter {Property = Button.MinimumWidthRequestProperty, Value = UISizes.BUTTON_MIN_WIDTH }
                 }
            };
            StyleResources.Add("SubmitButtonStyle", submitButtonStyle);

            Style hilightButtonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND) },
                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT) },
                    new Setter {Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) },
                    new Setter {Property = Button.BorderColorProperty, Value = Color.Transparent },
                    new Setter {Property = Button.MinimumWidthRequestProperty, Value = UISizes.BUTTON_MIN_WIDTH }
                }
            };
            StyleResources.Add("HilightButtonStyle", hilightButtonStyle);

            Style likeButtonLightUnlikedStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_TERTIARY_TEXT) },
                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Micro, typeof(Button)) },
                    new Setter {Property = Button.BorderColorProperty, Value = Color.FromHex(UIColors.COLOR_TERTIARY_TEXT) },
                    new Setter {Property = Button.MarginProperty, Value = UISizes.PADDING_NARROW },
                    new Setter {Property = Button.BorderWidthProperty, Value = 1 },
                }
            };
            StyleResources.Add("likeButtonLightUnlikedStyle", likeButtonLightUnlikedStyle);

            Style likeButtonDarkUnlikedStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND) },
                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Micro, typeof(Button)) },
                    new Setter {Property = Button.BorderColorProperty, Value = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND) },
                    new Setter {Property = Button.MarginProperty, Value = UISizes.PADDING_NARROW },
                    new Setter {Property = Button.BorderWidthProperty, Value = 1 },
                }
            };
            StyleResources.Add("likeButtonDarkUnlikedStyle", likeButtonDarkUnlikedStyle);

            Style likeButtonLikedStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT) },
                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Micro, typeof(Button)) },
                    new Setter {Property = Button.BorderColorProperty, Value = Color.FromHex(UIColors.COLOR_WIDGET_HIGHLIGHT) },
                    new Setter {Property = Button.MarginProperty, Value = UISizes.PADDING_NARROW },
                    new Setter {Property = Button.BorderWidthProperty, Value = 1 },
                }
            };
            StyleResources.Add("likeButtonLikedStyle", likeButtonLikedStyle);

            Style LabelSecondaryStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_SECONDARY_TEXT) },
                    new Setter {Property = Label.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Label.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Small, typeof(Label)) },
                    new Setter {Property = Label.LineBreakModeProperty, Value = LineBreakMode.TailTruncation, }
                }
            };
            StyleResources.Add("LabelSecondaryStyle", LabelSecondaryStyle);

            Style LabelMultilineStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.LineBreakModeProperty, Value = LineBreakMode.WordWrap },
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = Label.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Label.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Small, typeof(Label)) },
                }
            };
            StyleResources.Add("LabelMultilineStyle", LabelMultilineStyle);


            Style entryStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter {Property = Entry.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = Entry.PlaceholderColorProperty, Value = Color.FromHex(UIColors.COLOR_PLACEHOLDER_TEXT) },
                    new Setter {Property = Entry.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Entry.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                }
            };
            StyleResources.Add(entryStyle);

            Style numericEntryStyle = new Style(typeof(NumericEntry))
            {
                Setters =
                {
                    new Setter {Property = NumericEntry.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = NumericEntry.PlaceholderColorProperty, Value = Color.FromHex(UIColors.COLOR_PLACEHOLDER_TEXT) },
                    new Setter {Property = NumericEntry.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = NumericEntry.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                    new Setter { Property= NumericEntry.KeyboardProperty, Value = Keyboard.Numeric },
                }
            };
            StyleResources.Add(numericEntryStyle);

            Style editorStyle = new Style(typeof(Editor))
            {
                Setters =
                {
                    new Setter {Property = Editor.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = Editor.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = Editor.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                }
            };
            StyleResources.Add(editorStyle);

            Style resizingEditorStyle = new Style(typeof(ExtendedEditor))
            {
                Setters =
                {
                    new Setter {Property = ExtendedEditor.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = ExtendedEditor.PlaceholderColorProperty, Value = Color.FromHex(UIColors.COLOR_PLACEHOLDER_TEXT) },
                    new Setter {Property = ExtendedEditor.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter {Property = ExtendedEditor.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                }
            };
            StyleResources.Add(resizingEditorStyle);

            Style tableViewStyle = new Style(typeof(TableView))
            {
                Setters =
                {
                    new Setter {Property = TableView.BackgroundColorProperty, Value = Color.Transparent },
					//new Setter {Property = ListView.SeparatorVisibilityProperty, Value = false },
	            }
            };
            StyleResources.Add(tableViewStyle);

            Style coloredPickerStyle = new Style(typeof(ColoredPicker))
            {
                Setters =
                {
                    new Setter {Property = ColoredPicker.TextColorProperty, Value = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT) },
                    new Setter {Property = ColoredPicker.PlaceholderColorProperty, Value = Color.FromHex(UIColors.COLOR_PLACEHOLDER_TEXT) },
                }
            };
            StyleResources.Add(coloredPickerStyle);

            Style stackLayoutStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter { Property = StackLayout.PaddingProperty, Value = UISizes.PADDING_NONE },
                    new Setter { Property = StackLayout.MarginProperty, Value = UISizes.MARGIN_NONE },
                    new Setter { Property = StackLayout.SpacingProperty, Value = UISizes.SPACING_NONE },
                }
            };
            StyleResources.Add(stackLayoutStyle);

            Style paddedStackLayoutStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter { Property = StackLayout.PaddingProperty, Value = UISizes.PADDING_NONE },
                    new Setter { Property = StackLayout.MarginProperty, Value = UISizes.MARGIN_STANDARD },
                    new Setter { Property = StackLayout.SpacingProperty, Value = UISizes.SPACING_STANDARD },
                }
            };
            StyleResources.Add("paddedStackLayoutStyle", paddedStackLayoutStyle);

            Style contentViewStyle = new Style(typeof(ContentView))
            {
                Setters =
                {
                    new Setter { Property = ContentView.PaddingProperty, Value = UISizes.PADDING_NONE },
                    new Setter { Property = ContentView.MarginProperty, Value = UISizes.MARGIN_NONE },
                }
            };
            StyleResources.Add(contentViewStyle);

            return StyleResources;
        }
    }
}