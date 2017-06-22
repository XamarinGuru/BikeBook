using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BikeBook.Views
{


    /**
     * A GUI layout widget that maintains a fixed aspect ratio during automatic resizing 
     */
    public class FixedAspectGUIContainer : ContentView
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (widthConstraint.IsFinite() && heightConstraint.IsFinite())
                return new SizeRequest(new Size(widthConstraint, widthConstraint * this.AspectRatio));
            else
                return new SizeRequest();
        }


        /**
         * width/height ratio of the container
         */
        public double AspectRatio { get; set; }
    }
}
