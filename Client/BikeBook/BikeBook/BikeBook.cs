using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ClientWebService;

namespace BikeBook
{
    /** Instantiates and links all components of application
     * 
     */
    public partial class BikeBook : Application
    {
        internal Service WebService;
        private Views.ViewHandler m_viewHandler;
        
        /** Contructor for application.
         */
        public BikeBook()
        {
            Application.Current.Resources = Views.UIStyles.BuildStyleDictionary();
            WebService =  Service.Instance;
            m_viewHandler = Views.ViewHandler.Instance;
            m_viewHandler.ParentApp = this;
            m_viewHandler.launchGui(); 
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }


        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }


        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
