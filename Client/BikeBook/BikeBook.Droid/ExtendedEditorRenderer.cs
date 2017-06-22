using System.ComponentModel;
using BikeBook.Views;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(BikeBookXamarinPrototype.Droid.ExtendedEditorRenderer))]
namespace BikeBookXamarinPrototype.Droid
{
    class ExtendedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(
            ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UIColorConverter converter = new UIColorConverter();
                var element = e.NewElement as ExtendedEditor;
                this.Control.Hint = element.Placeholder;
                this.Control.SetHintTextColor(converter.FromXamrinFormsColor(element.PlaceholderColor));
            }
        }

        protected override void OnElementPropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {

            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedEditor.PlaceholderProperty.PropertyName)
            {
                UIColorConverter converter = new UIColorConverter();
                var element = this.Element as ExtendedEditor;
                this.Control.Hint = element.Placeholder;
                this.Control.SetHintTextColor(converter.FromXamrinFormsColor(element.PlaceholderColor));
            }
        }
    }
}