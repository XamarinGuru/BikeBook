using System.ComponentModel;
using BikeBook.Views;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ColoredPicker), typeof(BikeBookXamarinPrototype.Droid.CustomPickerRenderer))]
namespace BikeBookXamarinPrototype.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Renderer")
            {
                UIColorConverter colorConverter = new UIColorConverter();
                ColoredPicker ColoredElement = (ColoredPicker)Element;
                this.Control.SetTextColor(colorConverter.FromXamrinFormsColor(ColoredElement.CompletedTextColor));
                this.Control.SetHintTextColor(colorConverter.FromXamrinFormsColor(ColoredElement.PlaceholderColor));
            }
        }
    }
}