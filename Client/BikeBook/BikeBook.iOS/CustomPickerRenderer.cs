using System.ComponentModel;
using BikeBook.Views;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ColoredPicker), typeof(BikeBookXamarinPrototype.iOS.CustomPickerRenderer))]
namespace BikeBookXamarinPrototype.iOS
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            UIColorConverter colorConverter = new UIColorConverter();
            ColoredPicker ColoredElement = (ColoredPicker)Element;
            base.OnElementChanged(e);
            this.Control.TextColor = colorConverter.FromXamrinFormsColor(ColoredElement.TextColor);
            this.Control.Text = this.Element.Title;
            this.Control.BackgroundColor = UIKit.UIColor.Clear;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName.Equals(ColoredPicker.TextColorProperty.PropertyName) ||
                e.PropertyName.Equals(ColoredPicker.PlaceholderColorProperty.PropertyName) ||
                e.PropertyName.Equals(ColoredPicker.SelectedIndexProperty.PropertyName))
            {
                UIColorConverter colorConverter = new UIColorConverter();
                ColoredPicker ColoredElement = (ColoredPicker)Element;
                if (Element.SelectedIndex < 0)
                {
                    this.Control.TextColor = colorConverter.FromXamrinFormsColor(ColoredElement.PlaceholderColor);
                }
                else
                {
                    this.Control.TextColor = colorConverter.FromXamrinFormsColor(ColoredElement.TextColor);
                }
            }
        }
    }
}