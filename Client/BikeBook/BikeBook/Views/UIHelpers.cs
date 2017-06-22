using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebService;
using Xamarin.Forms;

namespace BikeBook.Views
{

    /**
     * Extension methods for common operations on UI elements
     */
    static class UIHelpers
    {
        public static string ToAgeString(string JavaDate)
        {
            ConvertDate dateConverter = new ConvertDate();
            return dateConverter.FromJava(JavaDate).ToAgeString();
        }

        /**
         * Builds age string from a DateTime object,
         * printing in simple form how long ago a DateTime occured
         */
        public static string ToAgeString(this DateTime postDate)
        {
            TimeSpan Diff = DateTime.Now.Subtract(postDate);
            double Days = Diff.Days;
            double Hours = Diff.Hours + Days * 24;
            double Minutes = Diff.Minutes + Hours * 60;
            if (Minutes <= 1)
            {
                return "Just Now";
            }
            double Years = Math.Floor(Diff.TotalDays / 365);
            if (Years >= 1)
            {
                return string.Format("{0} year{1} ago", Years, Years >= 2 ? "s" : null);
            }
            double Months = Math.Floor(Diff.TotalDays / 30);
            if ( Months >= 1)
            {
                return string.Format("{0} month{1} ago", Months, Months >= 2 ? "s" : null);
            }
            double Weeks = Math.Floor(Diff.TotalDays / 7);
            if (Weeks >= 1)
            {
                return string.Format("{0} week{1} ago", Weeks, Weeks >= 2 ? "s" : null);
            }
            if (Days >= 1)
            {
                return string.Format("{0} day{1} ago", Days, Days >= 2 ? "s" : null);
            }
            if (Hours >= 1)
            {
                return string.Format("{0} hour{1} ago", Hours, Hours >= 2 ? "s" : null);
            }

            // Only condition left is minutes > 1
            return string.Format("{0} mins ago", Minutes);
        }


        /**
         * Returns true if a double is a finite real number
         */
        public static bool IsFinite(this double number)
        {
            return !double.IsInfinity(number) && !double.IsNaN(number);
        }  


        /**
         * Returns true if this DateTime occurs after parameter DateTime
         */
        public static bool IsNewerThan(this DateTime date, DateTime dateToCheck)
        {
            return DateTime.Compare(date, dateToCheck) > 0;
        }
    }
    
}
