using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebService;

namespace BikeBook
{
    public static class PostExtensions
    {

        /**
         *  Compares 2 Posts by creation date for sorting, producing a list from ordered from newest to oldest
         *  
         *  @param Post x - a message to compare
         *  @param Post y - a message to compare
         *  
         *  @return int - Less than 0 if X older than Y, greater than 0 if X newer than Y, equal to 0 if simultaneous 
         */
        public static int CompareByAgeDescending(Post x, Post y)
        {
            if (y.Equals(null))
            {
                if (x.Equals(null))
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
            return DateTime.Compare(YCreatedAt, XCreatedAt);
        }
    }
}
