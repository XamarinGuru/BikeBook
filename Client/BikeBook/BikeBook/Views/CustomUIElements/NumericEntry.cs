using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     * Entry with built-in styling for numeric-only data entry
     * this class is styled in UIStyles.cs
     */
    public class NumericEntry : Entry
    {
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        public NumericEntry(double maxvalue = double.MaxValue,double minvalue = 0)
        {
            MaxValue = maxvalue;
            MinValue = minvalue;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == Entry.TextProperty.PropertyName)
            {
                try
                {
                    double enteredValue = Convert.ToDouble(this.Text);
                    if (enteredValue > MaxValue)
                    {
                        this.Text = MaxValue.ToString();
                    }
                    else if (enteredValue < MinValue)
                    {
                        this.Text = MinValue.ToString();
                    }
                }
                catch(FormatException)
                {
                    this.Text = string.Empty;
                }
                catch(OverflowException)
                {
                    this.Text = MaxValue.ToString();
                }
            }
            base.OnPropertyChanged(propertyName);
        }
    }
}
