using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using ClientWebService;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views
{
    public class Home_MyGear : ContentPage
    {
        private StackLayout m_contentStack;

        private StackLayout m_manageGearLayout;
        private Button m_manageGearButton;
        private Button m_addGearButton;

        private ScrollView m_contentScroll;
        private CardGrid m_cardGrid;

        public Home_MyGear()
        {
            GuiLayout();
            PopulatePlaceholders();
        }

        public Home_MyGear(User profile)
        {
            GuiLayout();
            PopulateContent(profile);
        }

        private void GuiLayout()
        {
            m_manageGearButton = new Button()
            {
                Text = "MANAGE GEAR",
                HeightRequest = UISizes.HIDDEN,
            };

            m_addGearButton = new Button()
            {
                Text = "NEW ITEM",
                HeightRequest = UISizes.HIDDEN,
            };

            m_manageGearLayout = new StackLayout()
            {
                Margin = new Thickness(UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD, UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD),
                HeightRequest = UISizes.HIDDEN,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    m_manageGearButton,
                    new HorizontalSpacer(UISizes.PADDING_WIDE),
                    m_addGearButton,
                }
            };

            m_cardGrid = new CardGrid();

            m_contentScroll = new ScrollView()
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.PADDING_NONE,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = m_cardGrid,
            };

            m_contentStack = new StackLayout()
            {
                Padding = UISizes.PADDING_NONE,
                Margin = UISizes.PADDING_NONE,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    m_manageGearLayout,
                    m_contentScroll,
                },
            };

            Style = (Style)Application.Current.Resources["contentPageStyle"];
            Content = m_contentStack;
        }


        private void PopulatePlaceholders()
        {
            SetEditButtonVisibility(true);
            for (int i = 0; i < 8; i++)
            {
                CardGridItem newGearCard = new CardGridItem(MockupPages.DummyProfile.Gear);
                newGearCard.EditTapped += EditGearDialog(MockupPages.DummyProfile.Gear);
                newGearCard.DeleteTapped += DeleteGearDialog(MockupPages.DummyProfile.Gear);
                m_cardGrid.AddItem(newGearCard);
            }
        }


        private void PopulateContent(User profile)
        {
            PopulatePlaceholders();
        }

        private void SetEditButtonVisibility(bool visible)
        {
            if (visible)
            {
                m_manageGearButton.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_manageGearButton.Clicked += ToggleEditing;

                m_addGearButton.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_addGearButton.Clicked += CreateNewGear;


                m_manageGearLayout.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
            }
            else
            {
                m_manageGearButton.HeightRequest = UISizes.HIDDEN;
                m_manageGearButton.Clicked -= ToggleEditing;

                m_addGearButton.HeightRequest = UISizes.HIDDEN;
                m_addGearButton.Clicked -= CreateNewGear;

                m_manageGearLayout.HeightRequest = UISizes.HIDDEN;
            }
        }


        /**
         *  Shows UI for creating new gear entry in the user's garage
         */
        private void CreateNewGear(object sender, EventArgs e)
        {
            Gear newGear = new Gear();
            Home_MyGear_EditGear newGearPage = new Home_MyGear_EditGear(newGear);
            newGearPage.EditCompleted += CreateNewGearCompleted(newGear);
            newGearPage.EditCancelled += EditGearCancelled;
            Navigation.PushAsync(newGearPage);
        }

        private EventHandler CreateNewGearCompleted(Gear gear)
        {
            return (sender, e) =>
            {
                // TODO: Add new gear to user's profile
                Navigation.RemovePage((Page)sender);
            };
        }


        /**
         *  Returns an EventHandler which displays dialog for editing a gear
         */
        private EventHandler EditGearDialog(Gear gear)
        {
            return (sender, e) =>
            {
                Home_MyGear_EditGear editGearPage = new Home_MyGear_EditGear(gear);
                editGearPage.EditCompleted += EditGearCompleted(gear);
                editGearPage.EditCancelled += EditGearCancelled;
                Navigation.PushAsync(editGearPage);
            };
        }


        /**
         *  Handles cancellation of editing a gear by closing the edit gear page and pushing changes to the webservice
         */
        private EventHandler EditGearCompleted(Gear gear)
        {
            return (sender, e) =>
            {
                //TODO: integrate func to edit gear details
                Navigation.RemovePage((Page)sender);
            };
        }


        /**
         *  Handles cancellation of editing a gear by closing the edit gear page
         */
        private void EditGearCancelled(object sender, EventArgs e)
        {
            Navigation.RemovePage((Page)sender);
        }


        /**
         *  returns an eventhandler which puts up a dialog allowing the user to delete a gear from their profile
         */
        private EventHandler DeleteGearDialog(Gear gear)
        {
            return (sender, e) =>
            {
                string ActionTaken = DisplayActionSheet("Are you sure you want to delete this item?", "Cancel", "Delete").Result;
                switch (ActionTaken)
                {
                    case "Delete":
                        DeleteGear(gear);
                        break;
                    default:
                        break;
                }
            };
        }

        private void DeleteGear(Gear gear)
        {
            //TODO: integrate server funcs to delete a gear
        }


        /**
         *  Shows or hides editing buttons on the card grid
         */
        private void ToggleEditing(object sender, EventArgs e)
        {
            m_cardGrid.Editable = !m_cardGrid.Editable;
        }
    }
}
