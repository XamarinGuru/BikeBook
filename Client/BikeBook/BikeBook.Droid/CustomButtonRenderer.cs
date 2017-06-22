using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Button), typeof(BikeBookXamarinPrototype.Droid.FlatButtonRenderer))]
namespace BikeBookXamarinPrototype.Droid
{
    /**
     * Custom renderer for android buttons so that borders are visible.
     * This is a workaround for xamarin bug 36031
     * https://bugzilla.xamarin.com/show_bug.cgi?id=36031
     */
    public class FlatButtonRenderer : ButtonRenderer
    {
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}