using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     *  Expansion of the editor class to include auto resizing, placeholder features
     */
    public class ExtendedEditor : Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
    BindableProperty.Create("Placeholder",typeof(string),typeof(ExtendedEditor),null);

       public static readonly BindableProperty PlaceholderColorProperty =
    BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(ExtendedEditor), Color.Default);

        private static DateTime m_lastResize;

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }

        /**
         * Checks if this editor has been filled
         * 
         * @return bool - True if field is populated with any text
         */
        public bool IsPopulated()
        {
            if ((this.Text != null) &&
                (this.Text.Length > 0) &&
                (this.Text != this.Placeholder))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ExtendedEditor() : base()
        {
            TextChanged += ForceResize;
        }


        private void ForceResize(Object sender, TextChangedEventArgs e)
        {
            /**
             *  Resizing must be rate-limited
             *  to prevent stuttering and slowness when entering text rapidly, for
             *  example by holding down a key on a physical keyboard
             */
            if (m_lastResize == null ||
               DateTime.Now.Subtract(m_lastResize).CompareTo(TimeSpan.FromMilliseconds(1)) > 0)
            {
                this.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                this.InvalidateMeasure();
                if (this.Height >= UISizes.EDITOR_MAX_HEIGHT)
                {
                    this.HeightRequest = UISizes.EDITOR_MAX_HEIGHT;
                }
                else
                {
                    this.HeightRequest = UISizes.SIZE_NONE_SPECIFIED;
                }
                m_lastResize = DateTime.Now;
            }
        }
    }
}
