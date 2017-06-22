using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    public class VerticalSpacer : BoxView
    {
        public VerticalSpacer(double height) : base()
        {
            BackgroundColor = Color.Transparent;
            HorizontalOptions = LayoutOptions.Fill;
            HeightRequest = height;
        }

        public VerticalSpacer() : base()
        {
            BackgroundColor = Color.Transparent;
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }

    public class HorizontalSpacer : BoxView
    {
        public HorizontalSpacer(double width) : base()
        {
            BackgroundColor = Color.Transparent;
            VerticalOptions = LayoutOptions.Fill;
            WidthRequest = width;
        }

        public HorizontalSpacer() : base()
        {
            BackgroundColor = Color.Transparent;
            VerticalOptions = LayoutOptions.Fill;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
