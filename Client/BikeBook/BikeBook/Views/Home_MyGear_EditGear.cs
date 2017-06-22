using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using BikeBook.Views.CustomUIElements;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views
{
    /**
     * Page for creating ads for non-bike items like accesories, gear, clothing
     */
    public class Home_MyGear_EditGear : ContentPage
    {
        private User m_loadedProfile;
        private Gear m_loadedGear;
        private ImageSerializer m_imageSerializer;

        private ScrollView m_mainScroll;
        private StackLayout m_mainLayout;

        private StackLayout m_fieldsLayout;
        private ProfileNameCell m_postingProfileCell;
        private Entry m_make;
        private Entry m_model;
        private NumericEntry m_year;
        private Entry m_color;
        private ColoredPicker m_categoryPicker;
        private ExtendedEditor m_descriptionEditor;
        private AddImageCell m_imageSelectionCell;

        private StackLayout m_buttonsLayout;
        private Button m_cancelButton;
        private Button m_doneButton;

        public event EventHandler EditCompleted;
        public event EventHandler EditCancelled;

        public Home_MyGear_EditGear()
        {
            //m_loadedBike = MockupPages.DummyProfile.Gear;
            m_loadedGear = new Gear();
            InitServices();
            GuiLayout();
            LoadDummyProfile();
            PopulateContent();
        }

        public Home_MyGear_EditGear(Gear gear)
        {
            m_loadedGear = gear;
            InitServices();
            GuiLayout();
            LoadUserProfile();
            PopulateContent();
        }

        private void LoadDummyProfile()
        {
            m_loadedProfile = MockupPages.DummyProfile.User;
        }

        private void InitServices()
        {
            m_imageSerializer = new ImageSerializer();
        }

        private void LoadUserProfile()
        {
            LoadDummyProfile();
        }

        private void GuiLayout()
        {
            Style = (Style)Application.Current.Resources["contentPageStyle"];
            m_postingProfileCell = new ProfileNameCell(m_loadedProfile);

            m_make = new Entry() { Placeholder = "Make" };
            m_model = new Entry() { Placeholder = "Model" };
            m_year = new NumericEntry() { Placeholder = "Year" };
            m_color = new Entry() { Placeholder = "Color" };
            PopulatePicker();
            m_descriptionEditor = new ExtendedEditor() { Placeholder = "Description" };
            m_imageSelectionCell = new AddImageCell();

            m_fieldsLayout = new StackLayout()
            {
                Style = (Style)Application.Current.Resources["paddedStackLayoutStyle"],
                Children =
                {
                    m_imageSelectionCell,
                    m_make,
                    m_model,
                    m_year,
                    m_color,
                    m_categoryPicker,
                    m_descriptionEditor,
                },
            };

            m_cancelButton = new Button() { Text = "Cancel" };
            m_cancelButton.Clicked += CancelButtonClicked;
            m_doneButton = new Button() { Text = "Done", Style = (Style)Application.Current.Resources["SubmitButtonStyle"] };
            m_doneButton.Clicked += DoneButtonClicked;

            m_buttonsLayout = new StackLayout()
            {
                Margin = new Thickness(UISizes.MARGIN_STANDARD,UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD,UISizes.MARGIN_STANDARD),
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    m_cancelButton,
                    new HorizontalSpacer(UISizes.PADDING_WIDE),
                    m_doneButton,
                },
            };

            m_mainLayout = new StackLayout()
            {
                Children =
                {
                    m_postingProfileCell,
                    m_fieldsLayout,
                    m_buttonsLayout,
                },
            };
            m_mainScroll = new ScrollView()
            {
                Content = m_mainLayout,
            };
            Content = m_mainScroll;
        }

        private void PopulatePicker()
        {
            m_categoryPicker = new ColoredPicker()
            {
                Title = "Category",
            };

            foreach (string category in UIStrings.GearCategories)
                m_categoryPicker.Items.Add(category);
        }

        private void PopulateContent()
        {
            if ((m_loadedGear.make != null)         && (m_loadedGear.make.Length > 0) )         { m_make.Text = m_loadedGear.make; };
            if ((m_loadedGear.model != null)        && (m_loadedGear.model.Length > 0))         { m_model.Text = m_loadedGear.model; };
            if ((m_loadedGear.year != null)         && (m_loadedGear.year.Length > 0))          { m_year.Text = m_loadedGear.year; };
            if ((m_loadedGear.color != null)        && (m_loadedGear.color.Length > 0))         { m_color.Text = m_loadedGear.color; };
            if ((m_loadedGear.category != null)     && (m_loadedGear.category.Length > 0))      { m_categoryPicker.SelectedIndex = m_categoryPicker.Items.IndexOf(m_loadedGear.category); };
            if ((m_loadedGear.description != null)  && (m_loadedGear.description.Length > 0))   { m_descriptionEditor.Text = m_loadedGear.description; };
            if ((m_loadedGear.picture != null)      && (m_loadedGear.picture.Length > 0))       { m_imageSelectionCell.ImageSource = m_imageSerializer.DeserializeImageToCache(m_loadedGear.picture); };
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            if (validateInputs())
            {
                CommitChanges();
                EditCompleted(this, EventArgs.Empty);
            }
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            EditCancelled(this, EventArgs.Empty);
        }

        private void CommitChanges()
        {
            Gear NewGear = new Gear();

            if (m_make.IsPopulated())              { NewGear.make = m_make.Text; }
            if (m_model.IsPopulated())             { NewGear.model = m_model.Text; }
            if (m_year.IsPopulated())              { NewGear.year = m_year.Text; }
            if (m_color.IsPopulated())             { NewGear.color = m_color.Text; }
            if (m_categoryPicker.SelectedIndex > 0) { NewGear.category = m_categoryPicker.Items[m_categoryPicker.SelectedIndex]; }
            if (m_descriptionEditor.IsPopulated()) { NewGear.description = m_descriptionEditor.Text; }
        }

        private bool validateInputs()
        {
            if (!m_model.IsPopulated()) { DisplayAlert("Please give a model", "", "OK"); return false; }

            return true;
        }
    }
}
