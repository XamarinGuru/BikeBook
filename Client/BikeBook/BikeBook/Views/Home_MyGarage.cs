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
    public class Home_MyGarage : ContentPage
    {
        private StackLayout m_contentStack;

        private StackLayout m_manageBikesLayout;
        private Button m_manageBikesButton;
        private Button m_addBikeButton;

        private ScrollView m_contentScroll;
        private CardGrid m_cardGrid;

        public Home_MyGarage()
        {
            GuiLayout();
            PopulatePlaceholders();
        }

        public Home_MyGarage(User userProfile)
        {
            GuiLayout();
            PopulateContent(userProfile);
        }

        private void GuiLayout()
        {
            m_manageBikesButton = new Button()
            {
                Text = "MANAGE BIKES",
                HeightRequest = UISizes.HIDDEN,
            };

            m_addBikeButton = new Button()
            {
                Text = "NEW BIKE",
                HeightRequest = UISizes.HIDDEN,
            };

            m_manageBikesLayout = new StackLayout()
            {
                Margin = new Thickness(UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD, UISizes.MARGIN_NONE, UISizes.MARGIN_STANDARD),
                HeightRequest = UISizes.HIDDEN,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    m_manageBikesButton,
                    new HorizontalSpacer(UISizes.PADDING_WIDE),
                    m_addBikeButton,
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
                    m_manageBikesLayout,
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
                CardGridItem newBikeCard = new CardGridItem(MockupPages.DummyProfile.Bike);
                newBikeCard.EditTapped += EditBikeDialog(MockupPages.DummyProfile.Bike);
                newBikeCard.DeleteTapped += DeleteBikeDialog(MockupPages.DummyProfile.Bike);
                m_cardGrid.AddItem(newBikeCard);
            }
        }

        private void PopulateContent(User userProfile)
        {
            PopulatePlaceholders();
        }

        private void SetEditButtonVisibility(bool visible)
        {
            if( visible )
            {
                m_manageBikesButton.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_manageBikesButton.Clicked += ToggleEditing;

                m_addBikeButton.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                m_addBikeButton.Clicked += CreateNewBike;


                m_manageBikesLayout.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
            }
            else
            {
                m_manageBikesButton.HeightRequest = UISizes.HIDDEN;
                m_manageBikesButton.Clicked -= ToggleEditing;

                m_addBikeButton.HeightRequest = UISizes.HIDDEN;
                m_addBikeButton.Clicked -= CreateNewBike;

                m_manageBikesLayout.HeightRequest = UISizes.HIDDEN;
            }
        }


        /**
         *  Shows UI for creating new bike entry in the user's garage
         */
        private void CreateNewBike(object sender, EventArgs e)
        {
            Bike newBike = new Bike();
            Home_MyGarage_EditBike newBikePage = new Home_MyGarage_EditBike(newBike);
            newBikePage.EditCompleted += CreateNewBikeCompleted(newBike);
            newBikePage.EditCancelled += EditBikeCancelled;
            Navigation.PushAsync(newBikePage);
        }

        private EventHandler CreateNewBikeCompleted(Bike newBike)
        {
            return (sender, e) =>
            {
                // TODO: Add new bike to user's profile
                Navigation.RemovePage((Page)sender);
            };
        }


        /**
         *  Returns an EventHandler which displays dialog for editing a bike
         */
        private EventHandler EditBikeDialog(Bike bike)
        {
            return (sender, e) =>
            {
                Home_MyGarage_EditBike editBikePage = new Home_MyGarage_EditBike(bike);
                editBikePage.EditCompleted += EditBikeCompleted(bike);
                editBikePage.EditCancelled += EditBikeCancelled;
                Navigation.PushAsync(editBikePage);
            };
        }


        /**
         *  Handles cancellation of editing a bike by closing the edit bike page and pushing changes to the webservice
         */
        private EventHandler EditBikeCompleted(Bike bike)
        {
            return (sender, e) =>
            {
                //TODO: integrate func to edit bike details
                Navigation.RemovePage((Page)sender);
            };
        }


        /**
         *  Handles cancellation of editing a bike by closing the edit bike page
         */
        private void EditBikeCancelled(object sender, EventArgs e)
        {
            Navigation.RemovePage((Page)sender);
        }


        /**
         *  returns an eventhandler which puts up a dialog allowing the user to delete a bike from their profile
         */
        private EventHandler DeleteBikeDialog(Bike bike)
        {
            return (sender, e) =>
            {
                string ActionTaken = DisplayActionSheet("Are you sure you want to delete this bike?", "Cancel", "Delete").Result;
                switch (ActionTaken)
                {
                    case "Delete":
                        DeleteBike(bike);
                        break;
                    default:
                        break;
                }
            };
        }

        private void DeleteBike(Bike bike)
        {
            //TODO: integrate server funcs to delete a bike
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
