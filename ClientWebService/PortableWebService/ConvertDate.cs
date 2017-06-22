using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebService
{
    public class ConvertDate
    {
        public DateTime FromJava(string dateString)
        {
            if (dateString != null)
            {
                return DateTime.ParseExact(dateString.Substring(0, 24),
                                  "ddd MMM dd yyyy HH:mm:ss",
                                  CultureInfo.InvariantCulture
                                  );
            }
            else
            {
                return new DateTime();
            }
        }

        public string ToJava(DateTime dateTime)
        {
            if (dateTime != null)
            {
                return dateTime.ToString("ddd MMM dd yyyy HH:mm:ss",
                                  CultureInfo.InvariantCulture
                                  );
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
