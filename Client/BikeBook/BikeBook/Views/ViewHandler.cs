using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace BikeBook.Views
{
    /**
    * Singleton class to handle UI display and user input.
    */
    public class ViewHandler
    {
        private static ViewHandler m_instance;

        private BikeBook m_parentApp;


        /**
        * Accessor for singleton instance of ViewHandler
        */
        public static ViewHandler Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ViewHandler();
                }

                return m_instance;
            }
        }


        /**
         * Accesor for host app. Enables access to host app's control for navigation, form stack
         */
         public BikeBook ParentApp
        {
            get { return m_parentApp; }
            set { m_parentApp = value; }
        }


        /**
         * Constructor for ViewHandler
         */
        private ViewHandler()
        {

        }


        /**
         * Starts displaying UI
         */
        public void launchGui()
        {
            if ( m_parentApp!= null)
            {
                setRootPage(getLaunchPage());
            }
        }


        /**
         * Returns first page shown when app launches.
         * 
         * @return Page - the first page to show to user when app launches.
         */
        private Page getLaunchPage()
        {
            //if (m_parentApp.m_webService.IsAuthenticated())
            //{
            //    return getHomePage();
            //}
            //else
            //{
            //return new LoginRegistration1();
            return new BuySellCreateAd();
            //}
        }


        /**
         * Returns Homepage to be shown when user first logs in
         *
         * @return Page - User's homepage shown after successful login
         */
        public Page getHomePage()
        {
            return new ProfileRidersMain();
        }


        /**
         * Sets new root for page stack. Pushing back from this page will exit the app.
         *
         * @param page - new page to set as root of navigation.
         */
        public void setRootPage(Page page)
        {
            if (m_parentApp.MainPage == null)
            {
                m_parentApp.MainPage = new NavigationPage(page);
            }
        }
    }
}
