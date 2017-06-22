using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BikeBook.Views
{
    static class UIGestureRecognizerHelper
    {
        /**
         * Adds a handler to an Xamarin.Forms.View to respond to the user tapping on it
         * 
         * @param View view - Xamarin.Forms.Image object to configure
         * @param EventHandler handler - System.EventHandler delegate to respond to tap events.
         */
        public static void AddSingleTapHandler(this View view, EventHandler handler)
        {
            TapGestureRecognizer tgr = GetRecognizer(handler);
            if (tgr != null )
                view.GestureRecognizers.Add(tgr);
        }

        /**
         * Removes a handler from a Xamarin.Forms.ContentView
         * 
         * @param View view - Xamarin.Forms.ContentView object to configure
         * @param EventHandler handler - System.EventHandler delegate to respond to tap events.
         */
        public static void RemoveSingleTapHandler(this View view, EventHandler handler)
        {
            if ((Recognizers != null) &&
                Recognizers.ContainsKey(handler))
            {
                Recognizers[handler].Tapped -= handler;
                view.GestureRecognizers.Remove(Recognizers[handler]);
                Recognizers.Remove(handler);
            }
        }

        private static Dictionary<EventHandler, TapGestureRecognizer> Recognizers;

        private static TapGestureRecognizer GetRecognizer(EventHandler handler)
        {
            if (handler != null)
            {
                if (Recognizers == null)
                    Recognizers = new Dictionary<EventHandler, TapGestureRecognizer>();

                if (Recognizers.ContainsKey(handler))
                {
                    return Recognizers[handler];
                }
                else
                {
                    TapGestureRecognizer tgr = new TapGestureRecognizer();
                    tgr.NumberOfTapsRequired = 1;
                    tgr.Tapped += handler;
                    Recognizers.Add(handler, tgr);
                    return tgr;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
