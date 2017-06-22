using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BikeBookXamarinPrototype.Droid
{
    class UIColorConverter
    {
        public Android.Graphics.Color FromXamrinFormsColor(Xamarin.Forms.Color formsColor)
        {
            return new Android.Graphics.Color((byte)(formsColor.R*byte.MaxValue), (byte)(formsColor.G * byte.MaxValue), (byte)(formsColor.B * byte.MaxValue));
        }
    }
}