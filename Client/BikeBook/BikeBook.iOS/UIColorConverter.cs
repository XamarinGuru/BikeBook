using System;
using Xamarin.Forms;
using UIKit;

namespace BikeBookXamarinPrototype.iOS
{
    class UIColorConverter
    {
        public UIColor FromXamrinFormsColor(Xamarin.Forms.Color baseColor)
        {
            return UIColor.FromRGB((nfloat)baseColor.R, (nfloat)baseColor.G, (nfloat)baseColor.B);
        }

        public UIColor FromHex(string HexColor)
        {
            Color baseColor = Color.FromHex(HexColor);
            return FromXamrinFormsColor(baseColor);
        }
    }
}