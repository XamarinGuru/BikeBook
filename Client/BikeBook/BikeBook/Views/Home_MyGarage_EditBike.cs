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
    public class Home_MyGarage_EditBike : ContentPage
    {
        private User m_loadedProfile;
        private Bike m_loadedBike;
        private ImageSerializer m_imageSerializer;

        private ScrollView m_mainScroll;
        private StackLayout m_mainLayout;

        private StackLayout m_fieldsLayout;
        private ProfileNameCell m_postingProfileCell;
        private Entry m_make;
        private Entry m_model;
        private NumericEntry m_year;
        private Entry m_color;
        private NumericEntry m_milage;
        private ExtendedEditor m_exhaustEditor;
        private ExtendedEditor m_accessoriesEditor;
        private ExtendedEditor m_descriptionEditor;
        private AddImageCell m_imageSelectionCell;

        private StackLayout m_buttonsLayout;
        private Button m_cancelButton;
        private Button m_doneButton;

        public event EventHandler EditCompleted;
        public event EventHandler EditCancelled;

        public Home_MyGarage_EditBike()
        {
            //m_loadedBike = MockupPages.DummyProfile.Bike;
            m_loadedBike = new Bike();
            InitServices();
            GuiLayout();
            LoadDummyProfile();
            PopulateContent();
        }

        public Home_MyGarage_EditBike(Bike bike)
        {
            m_loadedBike = bike;
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
            m_milage = new NumericEntry() { Placeholder = "Milage" };
            m_exhaustEditor = new ExtendedEditor() { Placeholder = "Exhaust" };
            m_accessoriesEditor = new ExtendedEditor() { Placeholder = "Accessories" };
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
                    m_milage,
                    m_exhaustEditor,
                    m_accessoriesEditor,
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

        private void PopulateContent()
        {
            if ((m_loadedBike.make != null)         && (m_loadedBike.make.Length > 0) )         { m_make.Text = m_loadedBike.make; };
            if ((m_loadedBike.model != null)        && (m_loadedBike.model.Length > 0))         { m_model.Text = m_loadedBike.model; };
            if ((m_loadedBike.year != null)         && (m_loadedBike.year.Length > 0))          { m_year.Text = m_loadedBike.year; };
            if ((m_loadedBike.color != null)        && (m_loadedBike.color.Length > 0))         { m_color.Text = m_loadedBike.color; };
            if ((m_loadedBike.milage != null)       && (m_loadedBike.milage.Length > 0))        { m_milage.Text = m_loadedBike.milage; };
            if ((m_loadedBike.exhaust != null)      && (m_loadedBike.exhaust.Length > 0))       { m_exhaustEditor.Text = m_loadedBike.exhaust; };
            if ((m_loadedBike.accessories != null)  && (m_loadedBike.accessories.Length > 0))   { m_accessoriesEditor.Text = m_loadedBike.accessories; };
            if ((m_loadedBike.description != null)  && (m_loadedBike.description.Length > 0))   { m_descriptionEditor.Text = m_loadedBike.description; };
            if ((m_loadedBike.picture != null)      && (m_loadedBike.picture.Length > 0))       { m_imageSelectionCell.ImageSource = m_imageSerializer.DeserializeImageToCache(m_loadedBike.picture); };
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
            Bike NewBike = new Bike();

            if (m_make.IsPopulated())              { NewBike.make = m_make.Text; }
            if (m_model.IsPopulated())             { NewBike.model = m_model.Text; }
            if (m_year.IsPopulated())              { NewBike.year = m_year.Text; }
            if (m_color.IsPopulated())             { NewBike.color = m_color.Text; }
            if (m_milage.IsPopulated())            { NewBike.milage = m_milage.Text; }
            if (m_exhaustEditor.IsPopulated())     { NewBike.exhaust = m_exhaustEditor.Text; }
            if (m_accessoriesEditor.IsPopulated()) { NewBike.accessories = m_accessoriesEditor.Text; }
            if (m_descriptionEditor.IsPopulated()) { NewBike.description = m_descriptionEditor.Text; }
        }

        private bool validateInputs()
        {
            if      (!m_make.IsPopulated())  { DisplayAlert("Please give a make",  "", "OK"); return false; }
            else if (!m_model.IsPopulated()) { DisplayAlert("Please give a model", "", "OK"); return false; }
            else if (!m_year.IsPopulated())  { DisplayAlert("Please give a year",  "", "OK"); return false; }
            else if (!m_color.IsPopulated()) { DisplayAlert("Please give a color", "", "OK"); return false; }

            return true;
        }
    }
}
