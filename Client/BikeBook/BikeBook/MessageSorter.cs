using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeBook.Views;
using ClientWebService;

namespace BikeBook
{
    static class MessageSorterExtensions
    {
        public static Messages GetUniqueConversations(this Messages allMessages)
        {
            ConvertDate dateConverter = new ConvertDate();
            List<Message> uniqueSenderMessages = new List<Message>();
            foreach(Message messageToCheck in allMessages.Message)
            {
                if (uniqueSenderMessages.Exists(x => x.From == messageToCheck.From))
                {
                    Message listedMessage = uniqueSenderMessages.Find(x => x.From == messageToCheck.From);
                    DateTime listedMessageDate = dateConverter.FromJava(listedMessage.Created_at);
                    DateTime messageToCheckDate = dateConverter.FromJava(messageToCheck.Created_at);

                    if (messageToCheckDate.IsNewerThan(listedMessageDate))
                    {
                        uniqueSenderMessages.Remove(listedMessage);
                        uniqueSenderMessages.Add(messageToCheck);
                   } 
                }
            }
            uniqueSenderMessages.Sort(CompareByAge);
            return new Messages() { Message = uniqueSenderMessages };
        }


        /**
         *  Compares 2 messages by creation date for sorting
         *  
         *  @param Message x - a message to compare
         *  @param Message y - a message to compare
         *  
         *  @return int - Less than 0 if X older than Y, greater than 0 if X newer than Y, equal to 0 if simultaneous 
         */
        private static int CompareByAge(Message x, Message y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            ConvertDate dateConverter = new ConvertDate();
            DateTime XCreatedAt = dateConverter.FromJava(x.Created_at);
            DateTime YCreatedAt = dateConverter.FromJava(y.Created_at);
            return DateTime.Compare(XCreatedAt, YCreatedAt);
        }
    }
}
