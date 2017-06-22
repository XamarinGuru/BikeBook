using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeBook.Views;
using ClientWebService;

namespace BikeBook
{
    static class MessageListQueries
    {

        /**
         *  Gets a list of the most recent message from each unique sender from a list of messages
         */
        public static List<Message> GetUniqueConversations(Messages allMessages)
        {
            ConvertDate dateConverter = new ConvertDate();
            List<Message> uniqueSenderMessages = new List<Message>();
            foreach(Message messageToCheck in allMessages.Message)
            {
                if (uniqueSenderMessages.Exists(x => (GetConversationPartner(x) == GetConversationPartner(messageToCheck))))
                {
                    Message listedMessage = uniqueSenderMessages.Find(x => GetConversationPartner(x) == GetConversationPartner(messageToCheck));
                    DateTime listedMessageDate = dateConverter.FromJava(listedMessage.created_at);
                    DateTime messageToCheckDate = dateConverter.FromJava(messageToCheck.created_at);

                    if (messageToCheckDate.IsNewerThan(listedMessageDate))
                    {
                        uniqueSenderMessages.Remove(listedMessage);
                        uniqueSenderMessages.Add(messageToCheck);
                    } 
                }
                else
                {
                    uniqueSenderMessages.Add(messageToCheck);
                }
            }
            uniqueSenderMessages.Sort(SortByAgeDescending);
            return uniqueSenderMessages ;
        }


        /**
         *  Gets a list of all messages sent to or received from a given individual
         *  
         *  @param this List<Message> allMessages - List of messages to query
         *  @param string Partner - name of individual to get conversation with.
         */
        public static List<Message> GetConversation(Messages allMessages, string partner)
        {
            List<Message> conversationMessages = allMessages.Message.FindAll(IncludedInConversation(partner));
            conversationMessages.Sort(SortByAgeAscending);
            return conversationMessages;
        }


        /**
         *  Compares 2 messages by creation date for sorting, producing a list from ordered from newest to oldest
         *  
         *  @param Message x - a message to compare
         *  @param Message y - a message to compare
         *  
         *  @return int - Less than 0 if X older than Y, greater than 0 if X newer than Y, equal to 0 if simultaneous 
         */
        private static int SortByAgeDescending(Message x, Message y)
        {
            if (x.Equals(null))
            {
                if (y.Equals(null))
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            ConvertDate dateConverter = new ConvertDate();
            DateTime XCreatedAt = dateConverter.FromJava(x.created_at);
            DateTime YCreatedAt = dateConverter.FromJava(y.created_at);
            return DateTime.Compare(YCreatedAt, XCreatedAt);
        }

        /**
         *  Compares 2 messages by creation date for sorting, producing a list from ordered from oldest to newest
         *  
         *  @param Message x - a message to compare
         *  @param Message y - a message to compare
         *  
         *  @return int - Less than 0 if X newer than Y, greater than 0 if X older than Y, equal to 0 if simultaneous 
         */
        private static int SortByAgeAscending(Message x, Message y)
        {
            if (x.Equals(null))
            {
                if (y.Equals(null))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            ConvertDate dateConverter = new ConvertDate();
            DateTime XCreatedAt = dateConverter.FromJava(x.created_at);
            DateTime YCreatedAt = dateConverter.FromJava(y.created_at);
            return DateTime.Compare(XCreatedAt, YCreatedAt);
        }

        /**
         *  
         */
        private static Predicate<Message> IncludedInConversation(string partner)
        {
            return delegate (Message messageToCheck)
            {
                return (GetConversationPartner(messageToCheck) == partner);
            };
        }

        /**
         * 
         */
        public static string GetConversationPartner(Message msg)
        {
            Service webService = Service.Instance;
            string myEmail = webService.Email;
            if( msg.from.Equals(myEmail) && msg.to.Equals(myEmail) )
            {
                return myEmail;
            }
            else if(msg.from.Equals(myEmail))
            {
                return msg.to;
            }
            else
            {
                return msg.from;
            }
        }
    }
}
