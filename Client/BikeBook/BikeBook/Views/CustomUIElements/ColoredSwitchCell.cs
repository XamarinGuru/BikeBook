using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class ColoredSwitchCell : ViewCell
    {
        private AbsoluteLayout m_mainLayout;
        private Label m_textLabel;
        private Switch m_switch;

        public Boolean On
        {
            get { return m_switch.IsToggled; }
            set { m_switch.IsToggled = value; }
        }

        public String Text
        {
            get { return m_textLabel.Text; }
            set { m_textLabel.Text = value; }
        }

        public Color TextColor
        {
            get { return m_textLabel.TextColor; }
            set { m_textLabel.TextColor = value; }
        }

        public ColoredSwitchCell()
        {
            m_textLabel = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            m_switch = new Switch() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, IsToggled = false};

            m_mainLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            };
            m_mainLayout.Children.Add(m_textLabel, new Rectangle(0, .5, 1, 1), AbsoluteLayoutFlags.All);
            m_mainLayout.Children.Add(m_switch, new Rectangle(1, .5, 1, 1), AbsoluteLayoutFlags.All);


            View = m_mainLayout;
            Tapped += ToggleSwitch;
        }

        private void ToggleSwitch(object sender, EventArgs e)
        {
            m_switch.IsToggled = !m_switch.IsToggled;
        }
    }
}
