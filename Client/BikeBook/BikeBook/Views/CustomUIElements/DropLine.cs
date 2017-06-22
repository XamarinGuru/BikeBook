using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     * UI element for drawing a thin vertical line filling its container
     */
    public class DropLine : BoxView
    {
        /**
         * Class Constructor
         */
        public DropLine()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            WidthRequest = 1;
        }
    }

}
