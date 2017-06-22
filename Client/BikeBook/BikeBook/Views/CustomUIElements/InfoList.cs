using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     *  Custom view for showing lists of basic string info on profile, bike, etc...
     */
    public class InfoList : ContentView
    {
        private Grid m_innerGrid;

        public InfoList()
        {
            m_innerGrid = new Grid()
            {
                Padding = UISizes.PADDING_NONE,
                RowSpacing = UISizes.SPACING_NARROW,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(1,GridUnitType.Star), },
                },
            };
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Content = m_innerGrid;
        }

        public void AddInfo(string info)
        {
            m_innerGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Label infoLabel = new Label()
            {
                Text = info,
                Style = (Style)Application.Current.Resources["LabelSecondaryStyle"],
            };
            ContentView paddingContainer = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = infoLabel,
                Padding = UISizes.PADDING_STANDARD,
                BackgroundColor = Color.FromHex(UIColors.COLOR_GENERAL_ACCENT),
            };
            m_innerGrid.Children.Add(
                paddingContainer,
                0,
                m_innerGrid.RowDefinitions.Count - 1);
        }
    }
}
