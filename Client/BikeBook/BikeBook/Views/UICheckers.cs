using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views
{

    /**
     * Extensions for checking state of UI elements safely
     */
    static class UICheckers
    {

        /**
         * Checks if a text entry has been filled
         * 
         * @param Entry entry - Xamarin.Forms.Entry object being extended
         * 
         * @return bool - True if field is populated with any text
         */
        public static bool IsPopulated(this Entry entry)
        {
            if (entry.Text != null && entry.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Checks if a text entry has been filled
         * 
         * @param Entry entry - Xamarin.Forms.Entry object being extended
         * 
         * @return bool - True if field is populated with any text
         */
        public static bool IsPopulated(this Editor editor)
        {
            if (editor.Text != null && editor.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /**
         * Checks if a label has text in it
         * 
         * @param Label label - Xamarin.Forms.Label object being extended
         * 
         * @return bool - True if label has any text to display
         */
        public static bool IsPopulated(this Label label)
        {
            if (label.Text != null && label.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /**
         * Verifies that a text entry has been filled with a properly formatted email address
         * 
         * @param Entry entry - Xamarin.Forms.Entry object being extended
         * 
         * @return bool - True if field contains properly formatted (not neccesarily valid) email address
         */
        public static bool IsFormattedEmailAddress(this Entry entry)
        {
            if (entry.IsPopulated())
            {
                return entry.Text.Contains("@") && entry.Text.Contains(".");
            }
            else
            {
                return false;
            }
        }
    }
}
