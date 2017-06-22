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
    public class BuySellCreateAd : ContentPage
    {
        private StackLayout m_mainLayout;
        private StackLayout m_nestedLayout;
        private GearCell m_postingProfileCell;



        private Entry m_title;
        private AddImageCell m_picture;
        private NumericEntry m_price;
        private ColoredPicker m_condition;
        private ColoredPicker m_category;
        private ColoredPicker m_subcategory;
        private Entry m_location;
        private ColoredPicker m_sellerType;
        private Entry m_make;
        private Entry m_model;
        private NumericEntry m_year;
        private NumericEntry m_odometer;
        private NumericEntry m_displacement;
        private Entry m_color;
        private Entry m_partNumber;
        private Entry m_size;
        private ExtendedEditor m_description;
        private ColoredPicker m_gender;
        private Button m_createAdButton;

        private readonly string[] CATEGORIES = 
         {
            "Bikes",
            "Accessories",
            "Parts",
            "Gear",
            "Clothing",
        };

        public BuySellCreateAd()
        {
            GuiLayout();
        }

        private void GuiLayout()
        {
            Style = (Style)Application.Current.Resources["contentPageStyle"];
            m_postingProfileCell = new GearCell();
            m_picture = new AddImageCell();
            m_price = new NumericEntry() { Placeholder = "Price" };
            m_condition = new ColoredPicker() { Title = "Condition" };
            m_category = new ColoredPicker() { Title = "Category" };
            m_subcategory = new ColoredPicker() { Title = "Subcategory" };
            m_location = new Entry() { Placeholder = "Location" };
            m_sellerType = new ColoredPicker() { Title = "Seller Type" };
            m_make = new Entry() { Placeholder = "Make", HeightRequest = UISizes.HIDDEN };
            m_model = new Entry() { Placeholder = "Model", HeightRequest = UISizes.HIDDEN };
            m_year = new NumericEntry() { Placeholder = "Year", HeightRequest = UISizes.HIDDEN };
            m_odometer = new NumericEntry() { Placeholder = "Odometer (km)", HeightRequest = UISizes.HIDDEN };
            m_displacement = new NumericEntry() { Placeholder = "Displacement (cc)", HeightRequest = UISizes.HIDDEN };
            m_color = new Entry() { Placeholder = "Color", HeightRequest = UISizes.HIDDEN };
            m_partNumber = new Entry() { Placeholder = "Part/Model No.", HeightRequest = UISizes.HIDDEN };
            m_size = new Entry() { Placeholder = "Size", HeightRequest = UISizes.HIDDEN };
            m_gender = new ColoredPicker() { Title = "Gender", HeightRequest = UISizes.HIDDEN };
            m_description = new ExtendedEditor() { Placeholder = "Description" };

            m_createAdButton = new Button() { Style = (Style)Application.Current.Resources["SubmitButtonStyle"], Text = "SUBMIT AD"};
            m_createAdButton.Clicked += CreateAdButtonClicked;
            m_nestedLayout = new StackLayout()
            {
                Padding = UISizes.PADDING_STANDARD,
                Children =
                {
                    m_picture      ,
                    m_price        ,
                    m_condition    ,
                    m_category     ,
                    m_subcategory  ,
                    m_location     ,
                    m_sellerType   ,
                    m_make         ,
                    m_model        ,
                    m_year         ,
                    m_odometer     ,
                    m_displacement ,
                    m_color        ,
                    m_partNumber   ,
                    m_size         ,
                    m_gender       ,
                    m_description  ,
                },
            };
            m_mainLayout = new StackLayout()
            {
                Children =
                {
                    m_postingProfileCell,
                    m_nestedLayout,
                },
            };
            Content = m_mainLayout;
        }

        private void CreateAdButtonClicked(object sender, EventArgs e)
        {
            if( validateInputs() )
            {
                submitAdContent();
            }
        }

        private bool validateInputs()
        {
            if( !m_title.IsPopulated())
            {
                DisplayAlert("Please create a title","","OK");
                return false;
            }
            else if( !m_price.IsPopulated())
            {
                DisplayAlert("Please set a price", "", "OK");
                return false;
            }
            else if( !m_location.IsPopulated())
            {
                DisplayAlert("Please enter a location", "", "OK");
                return false;
            }
            else if( m_category.SelectedIndex<0)
            {
                DisplayAlert("Please select an Ad Category", "", "OK");
                return false;
            }

            return true;
        }

        private void submitAdContent()
        {
            Service webService = Service.Instance;
            int resultCode = webService.CreateAdd(
                webService.Email,
                m_title.Text,
                m_picture.GetSelectedImageBase64(),
                m_description.Text,
                m_location.Text,
                "");
            if( HttpStatus.CheckStatusCode(resultCode) )
            {
                NavigateCreatedAd();
            }
            else
            {
                DisplayAlert("Ad Creation Failed, Code: " + resultCode.ToString(), "", "OK");
            }
        }

        private void NavigateCreatedAd()
        {
            //throw new NotImplementedException();
        }

        private void BuildPickerItems()
        {
            foreach(string item in CATEGORIES)
            {
                m_category.Items.Add(item); 
            }
        }
    }
}
