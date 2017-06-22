using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
    * UI element to take user text input at the bottom of a comment tree.
    */
    public class CommentTreeEntry : ContentView
    {
        private ImageSelectionDialog m_imageSelector;

        private StackLayout m_mainLayout;
        private ExpandingImageViewer m_contentImage;
        private VerticalSpacer m_contentSpacer;
        private ExtendedEditor m_contentEditor;
        private Button m_addImageButton;
        private Button m_submitButton;
        private StackLayout m_submissionButtonLayout;
        
        public string ContentImagePath
        {
            get;
            private set;
        }
        
        public string ContentText
        {
            get { return m_contentEditor.Text; }
        }

        public event EventHandler SubmitClicked
        {
            add { m_submitButton.Clicked += value; }
            remove { m_submitButton.Clicked -= value; }
        }

        /**
         * Class Constructor
         */
        public CommentTreeEntry()
        {
            m_imageSelector = new ImageSelectionDialog();
            GuiLayout();
        }

        /**
         * Lays out and displays page UI
         */
        private void GuiLayout()
        {
            m_contentEditor = new ExtendedEditor()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_NESTED_ACCENT_BACKGROUND),
                TextColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                Keyboard = Keyboard.Chat,
            };


            m_contentImage = new ExpandingImageViewer();
            m_contentImage.Closeable = true;
            m_contentImage.Expanding += ExpandSpacer;
            m_contentImage.Collapsing += CollapseSpacer;

            m_contentSpacer = new VerticalSpacer(UISizes.HIDDEN);

            m_addImageButton = new Button()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Add Image",
            };
            m_addImageButton.Clicked += AddImageButtonClicked;

            m_submitButton = new Button()
            {
                Style = (Style)Application.Current.Resources["SubmitButtonStyle"],
                HorizontalOptions = LayoutOptions.End,
                Text = "Leave Comment",
            };

            m_submissionButtonLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    m_addImageButton,
                    new HorizontalSpacer(),
                    m_submitButton,
                },
            };


            m_mainLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Margin = UISizes.MARGIN_NONE,
                Padding = UISizes.MARGIN_STANDARD,
                Children =
                {
                    m_contentImage,
                    m_contentSpacer,
                    m_contentEditor,
                    new VerticalSpacer(UISizes.SPACING_STANDARD),
                    m_submissionButtonLayout,
                },
            };


            BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT);
            Content = m_mainLayout;
        }


        private void ExpandSpacer(object sender, EventArgs e)
        {
            m_contentSpacer.HeightRequest = UISizes.SPACING_STANDARD;
        }

        private void CollapseSpacer(object sender, EventArgs e)
        {
            m_contentSpacer.HeightRequest = UISizes.HIDDEN;
        }

        private async void AddImageButtonClicked(object sender, EventArgs e)
        {
            ContentImagePath = await m_imageSelector.GetImage();
            m_contentImage.ContentImagePath = ContentImagePath;
        }
    }
}
