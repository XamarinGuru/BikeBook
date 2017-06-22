using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     * Template for showing ongoing conversations in a list
     */
    public class NewsfeedPostCell : ContentView
    {
        public event EventHandler LikeClicked;
        
        public ImageSource ProfileImageSource
        {
            get { return m_baseTemplate.ImageSource; }
            set { m_baseTemplate.ImageSource = value; }
        }

        public ImageSource PostImageSource
        {
            get { return m_contentImage.Source; }
            set { m_contentImage.Source = value; }
        }

        public string Name
        {
            get { return m_baseTemplate.Text; }
            set { m_baseTemplate.Text = value; }
        }

        public FormattedString PreviewFormatted
        {
            get
            {
                return m_preview.FormattedText;
            }
            set
            {
                m_preview.FormattedText = value;
            }
        }

        public DateTime PostTime
        {
            set
            {
                m_postTime = value;
                m_ageLabel.Text = PostTime.ToAgeString();
            }
            get
            {
                return m_postTime;
            }
        }

        public int Likes;

        public Post Post
        {
            get;
        }


        Service m_WebService;
        ImageSerializer m_ImageSerializer;

        private DateTime m_postTime;

        private ContentCellTemplate m_baseTemplate;


        private FormattedString m_formattedText;
        private Span m_headLineSpan;
        private Label m_ageLabel;
        private Button m_likeButton;

        private Image m_contentImage;
        private VerticalSpacer m_contentSpacer;
        private Label m_preview;

        /**
         * Class constructor
         */
        public NewsfeedPostCell()
        {
            Post = MockupPages.DummyProfile.Post;
            InitServices();
            GuiLayout();
            PopulatePlaceholders();
        }

        public NewsfeedPostCell(Post post)
        {
            Post = post;
            InitServices();
            GuiLayout();
            PopulateContent(post);
        }

        private void InitServices()
        {
            m_WebService = Service.Instance;
            m_ImageSerializer = new ImageSerializer();
        }

        /**
         * Lays out and displays template UI
         */
        private void GuiLayout()
        {
            Likes = 0;


            m_baseTemplate = new ContentCellTemplate()
            {
                BackgroundColor = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND),
            };

            m_headLineSpan = new Span()
            {
                ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            m_contentImage = new Image()
            {
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = UISizes.HIDDEN,
                IsVisible = false,
            };
            m_contentImage.PropertyChanged += MakeImageVisibleIfPopulated;

            m_contentSpacer = new VerticalSpacer(UISizes.HIDDEN);

            m_preview = new Label() { Style = (Style)Application.Current.Resources["LabelSecondaryStyle"], };

            m_baseTemplate.CenterNestedContent.Children.Insert(0, m_contentImage);
            m_baseTemplate.CenterNestedContent.Children.Insert(1, m_contentSpacer);
            m_baseTemplate.CenterNestedContent.Children.Add(m_preview);



            


            m_ageLabel = new Label()
            {
                LineBreakMode = LineBreakMode.CharacterWrap,
                TextColor = Color.FromHex(UIColors.COLOR_TERTIARY_TEXT),
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };

            m_likeButton = new Button()
            {
                Style = (Style)Application.Current.Resources["likeButtonLightUnlikedStyle"],
                Text = Likes + " Likes",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
            };
            m_baseTemplate.RightNestedContent.Children.Add(m_ageLabel);
            m_baseTemplate.RightNestedContent.Children.Add(m_likeButton);

            m_baseTemplate.FormattedText = m_formattedText;
            m_baseTemplate.VerticalTextAlignment = TextAlignment.Center;

            Content = m_baseTemplate;
        }

        private void PopulatePlaceholders()
        {
            User posterProfile = MockupPages.DummyProfile.User;

            ConvertDate dateConverter = new ConvertDate();
            PreviewFormatted = new FormattedString()
            {
                Spans =
                {
                    new Span()
                    {
                        Text = MockupPages.DummyProfile.Post.content,
                        ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                    }
                },
            };
            PostTime = dateConverter.FromJava(MockupPages.DummyProfile.Post.created_at);
            Name = posterProfile.name;
            ProfileImageSource = UIImages.BIKEPLACEHOLDER;
            //m_contentImage.Source = UIImages.BIKEPLACEHOLDER;
            m_likeButton.Clicked += ToggleLikeButtonColor;
        }

        private void PopulateContent(Post post)
        {
            User posterProfile = m_WebService.GetUser(m_WebService.Email, post.owner);

            ConvertDate dateConverter = new ConvertDate();
            PreviewFormatted = new FormattedString()
            {
                Spans =
                {
                    new Span()
                    {
                        Text = post.content,
                        ForegroundColor = Color.FromHex(UIColors.COLOR_PRIMARY_TEXT),
                    }
                },
            };
            PostTime = dateConverter.FromJava(post.created_at);
            Name = posterProfile.name;
            ProfileImageSource = m_ImageSerializer.DeserializeImageToCache(posterProfile.picture);
            m_contentImage.Source = m_ImageSerializer.DeserializeImageToCache(post.picture);
        }

        private void ToggleLikeButtonColor(object sender, EventArgs e)
        {
            Style likedStyle = (Style)Application.Current.Resources["likeButtonLikedStyle"];
            Style unlikedStyle = (Style)Application.Current.Resources["likeButtonLightUnlikedStyle"];
            m_likeButton.Style = (m_likeButton.Style == likedStyle) ? unlikedStyle : likedStyle;
        }

        /**
         * PropertyChangedEventHandler for revealing an image onscreen if image to display is added.
         * 
         * @param object sender - The source of the event
         * @param PropertyChangedEventArgs e - System.PropertyChangedEventArgs containing old and new text values
         */
        private void MakeImageVisibleIfPopulated(object sender, PropertyChangedEventArgs e)
        {
            if (sender.GetType() == typeof(Image) && e.PropertyName != null && e.PropertyName.Equals("Source"))
            {
                Image senderImage = (Image)sender;
                senderImage.IsVisible = senderImage.Source != null;
                senderImage.HeightRequest = senderImage.IsVisible ? UISizes.BANNER_IMAGE_HEIGHT : UISizes.HIDDEN;

                m_contentSpacer.HeightRequest = senderImage.IsVisible ? UISizes.SPACING_STANDARD : UISizes.HIDDEN;
            }
        }
    }
}
