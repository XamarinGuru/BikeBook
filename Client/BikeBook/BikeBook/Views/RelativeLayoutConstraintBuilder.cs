using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BikeBook.Views
{


    /**
     *  Creates Xamarin.Forms.Constraints, i.e. delegates used to
     *  locate and size GUI elements contained in a Xamarin.Forms.RelativeLayout
     */
    class RelativeLayoutConstraintBuilder
    {
        /** Returns a constraint equalling the parent layout's width
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving the parent layout's width
         */
        public Constraint ParentHeight()
        {
            return Constraint.RelativeToParent((parent) => { return parent.Height; });
        }


        /** Returns a constraint equalling the parent layout's Height
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving the parent layout's width
         */
        public Constraint ParentWidth()
        {
            return Constraint.RelativeToParent((parent) => { return parent.Width; });
        }


        /** Returns a constraint equalling some percentage of the parent layout's Height
         * 
         * @param double percent - The decimal percentage of the parent layout's Height to return
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving a percentage of the specified parent layout's Height
         */
        public Constraint ParentHeightPercent(double percent)
        {
            return Constraint.RelativeToParent((parent) => { return parent.Height * percent; });
        }

        /** Returns a constraint which produces a constant value
         * 
         * @param double constant - A constant dimension to return
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving a constant return value
         */
        public Constraint Constant(double constant)
        {
            return Constraint.RelativeToParent((parent) => { return constant; });
        }


        /** Returns a constraint equalling some percentage of the parent layout's Width
         * 
         * @param double percent - The decimal percentage of the parent layout's width to return
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving a percentage of the specified parent layout's width
         */
        public Constraint ParentWidthPercent(double percent)
        {
            return Constraint.RelativeToParent((parent) => { return parent.Width * percent; });
        }


        /** Returns a constraint equalling some percentage of a specified sibling view's width
         * 
         * @param View view - A Xamarin.Forms.View to get width from
         * @param double percent - The decimal percentage of the sibling view's width to return
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving a percentage of the specified sibling view's width
         */
        public Constraint SiblingWidthPercent(View view, double percent)
        {
            return Constraint.RelativeToView(view, (Parent, sibling) => { return sibling.Width * percent; });
        }


        /** Returns a constraint equalling some percentage of a specified sibling view's height
         * 
         * @param View view - A Xamarin.Forms.View to get height from
         * @param double percent - The decimal percentage of the sibling view's height to return
         * 
         * @return Constraint - A Xamarin.Forms.Constraint giving a percentage of the specified sibling view's Height
         */
        public Constraint SiblingHeightPercent(View view, double percent)
        {
            return Constraint.RelativeToView(view, (Parent, sibling) => { return sibling.Height * percent; });
        }
    }
}
