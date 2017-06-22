using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class ColoredPicker : Picker
    {
        public static readonly BindableProperty PlaceholderColorProperty =
    BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(ColoredPicker), Color.Default);

        public static readonly BindableProperty TextColorProperty =
    BindableProperty.Create("TextColor", typeof(Color), typeof(ColoredPicker), Color.Default);

        public Color CompletedTextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }
    }
}
