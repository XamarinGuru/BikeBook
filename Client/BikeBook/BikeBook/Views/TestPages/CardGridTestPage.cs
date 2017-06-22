using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views.TestPages
{
    public class CardGridTestPage : ContentPage
    {
        Button m_addItemButton;
        Button m_toggleEditButton;
        CardGrid m_cardGrid;

        public CardGridTestPage()
        {
            BackgroundColor = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND);
            Padding = UISizes.PADDING_NONE;

            m_addItemButton = new Button() { Text = "ADD ITEM", HorizontalOptions = LayoutOptions.Center, };
            m_addItemButton.Clicked += AddNewItem;

            Button m_toggleEditButton = new Button() { Text = "TOGGLE EDITS", HorizontalOptions = LayoutOptions.Center, };
            m_toggleEditButton.Clicked += ToggleEdits;

            m_cardGrid = new CardGrid();

            Content = new StackLayout
            {
                Padding = UISizes.PADDING_WIDE,
                Children = {
                    m_addItemButton,
                    m_toggleEditButton,
                    m_cardGrid,
                },
            };
        }

        private void AddNewItem(object sender, EventArgs e)
        {
            m_cardGrid.AddItem(new CardGridItem());
        }

        private void ToggleEdits(object sender, EventArgs e)
        {
            m_cardGrid.Editable = !m_cardGrid.Editable;
        }
    }
}
