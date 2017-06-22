using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using BikeBook.Views;

using Xamarin.Forms;

namespace BikeBook.Views.CustomUIElements
{
    /**
     *  Widget for forcing images to be displayed at a square apect ratio
     */
    public class SquareImage : ContentView
    {
        public ImageSource Source
        {
            set
            {
                m_image.Source = value;
            }
            get
            {
                return m_image.Source;
            }
        }

        private RelativeLayout m_aspectContainer;
        private Image m_image;

        public SquareImage()
        {
            m_image = new Image()
            {
                Aspect = Aspect.AspectFill,
            };
            RelativeLayoutConstraintBuilder ConstraintBuilder = new RelativeLayoutConstraintBuilder();
            m_aspectContainer = new RelativeLayout();
            m_aspectContainer.Children.Add(
                m_image,
                null,
                null,
                ConstraintBuilder.ParentWidth(),
                ConstraintBuilder.ParentWidth());
            Content = m_aspectContainer;
        }
    }
}
