using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class CardGrid : ContentView
    {
        /**
         *  true if editing buttons are visible on cards in this grid
         */
        public bool Editable
        {
            get
            {
                return m_editable;
            }
            set
            {
                if (value != m_editable)
                {
                    m_editable = value;
                    foreach (CardGridItem Card in m_cardsInGrid)
                    {
                        Card.Editable = m_editable;
                    }
                }
            }
        }

        private Grid m_innerGrid;
        private List<CardGridItem> m_cardsInGrid;
        private bool m_editable;

        public CardGrid()
        {
            m_editable = false;
            m_cardsInGrid = new List<CardGridItem>();
            m_innerGrid = new Grid()
            {
                ColumnSpacing = UISizes.SPACING_WIDE,
                RowSpacing = UISizes.SPACING_WIDE,
                HorizontalOptions = LayoutOptions.Center,

                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition()
                    {
                        Width = UISizes.CARD_GRID_ITEM_WIDTH,
                    },
                    new ColumnDefinition()
                    {
                        Width = UISizes.CARD_GRID_ITEM_WIDTH,
                    },
                }
            };


            Content = m_innerGrid;
        }

        public void AddItem(CardGridItem newItem)
        {
            newItem.Editable = m_editable;

            int rowOffset = m_innerGrid.Children.Count / UISizes.CARD_GRID_NUM_COLUMNS;
            int colOffset = m_innerGrid.Children.Count % UISizes.CARD_GRID_NUM_COLUMNS;

            if (rowOffset == m_innerGrid.RowDefinitions.Count)
            {
                m_innerGrid.RowDefinitions.Add(new RowDefinition() { Height = UISizes.CARD_GRID_ITEM_HEIGHT });
            }

            m_cardsInGrid.Add(newItem);
            m_innerGrid.Children.Add(newItem, colOffset, rowOffset);
        }
    }
}
