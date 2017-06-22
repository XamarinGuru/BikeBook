using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BikeBook.Views
{
    /**
     * Tabbed page container for messaging pages containing Recent messages and group messages
     */
    public class MessagesRoot : TabbedPage
    {
        MessagesRecent m_messagesRecent;
        MessagesGroupConvo m_messagesGroupConvo;

        public MessagesRoot()
        {
            m_messagesRecent = new MessagesRecent();
            Children.Add(m_messagesRecent);

            //m_messagesGroupConvo = new MessagesGroupConvo();
            //Children.Add(m_messagesGroupConvo);
        }
    }
}
