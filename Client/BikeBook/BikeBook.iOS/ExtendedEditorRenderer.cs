using System.ComponentModel;
using BikeBook.Views;
using BikeBook.Views.CustomUIElements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(BikeBookXamarinPrototype.iOS.ExtendedEditorRenderer))]
namespace BikeBookXamarinPrototype.iOS
{
    class ExtendedEditorRenderer : EditorRenderer
    {
        private static string m_oldPlaceholder = "";
        private static UIColorConverter m_colorConverter;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if( m_colorConverter == null)
            {
                m_colorConverter = new UIColorConverter();
            }

            var element = this.Element as ExtendedEditor;

            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(ExtendedEditor.PlaceholderProperty.PropertyName))
            {
                PlaceHolderChanged(element);
            }
            else if( e.PropertyName.Equals(ExtendedEditor.PlaceholderColorProperty.PropertyName))
            {
                PlaceHolderColorChanged(element);
            }
            else if(e.PropertyName.Equals(ExtendedEditor.IsFocusedProperty))
            {
                FocusChanged(element);
            }
        }


        /**
         *  Handler for changes to placeholder text
         *  
         *  If text is blank, or if the old placeholder is being shown,
         *  will replace with the new placeholder and change the text color
         */
        private void PlaceHolderChanged(ExtendedEditor element)
        {
            if ((this.Control.Text == "") ||
                (this.Control.Text == m_oldPlaceholder))
            {
                this.Control.Text = element.Placeholder;
                this.Control.TextColor = m_colorConverter.FromXamrinFormsColor(element.PlaceholderColor);
            }
            m_oldPlaceholder = this.Control.Text = element.Placeholder;
        }


        /**
         *  Handler for placeholder color changes
         *  
         *  If the placeholder is being shown, will change the color
         */
        private void PlaceHolderColorChanged(ExtendedEditor element)
        {
            if ((element.Placeholder != "") ||
                (this.Control.Text == element.Placeholder))
            {
                this.Control.TextColor = m_colorConverter.FromXamrinFormsColor(element.PlaceholderColor);
            }
        }


        /**
         *  Handler for focus change
         *  
         *  If the user enters the editor while blank or showing placeholder, the text will be cleared and color changed.
         *  
         *  If the user leaves the editor blank, the placeholder will be inserted
         */
        private void FocusChanged(ExtendedEditor element)
        {
            if (element.IsFocused)
            {
                if ((this.Control.Text == "") ||
                    (this.Control.Text == element.Placeholder))
                {
                    this.Control.Text = "";
                    this.Control.TextColor = m_colorConverter.FromXamrinFormsColor(element.TextColor);
                }
            }
            else if (element.IsFocused)
            {
                if ((this.Control.Text == "") ||
                    (this.Control.Text == element.Placeholder))
                {
                    this.Control.Text = element.Placeholder;
                    this.Control.TextColor = m_colorConverter.FromXamrinFormsColor(element.PlaceholderColor);
                }
            }
        }
    }
}