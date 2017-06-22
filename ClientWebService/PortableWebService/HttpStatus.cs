using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebService
{

    /**
     * List of common HTTP response codes
     */
    public class HttpStatus
    {
        public const int OK             = 200;
        public const int CREATED        = 201;
        public const int ACCEPTED       = 202;

        public const int UNAUTHORIZED   = 401;
        public const int UNKNOWN        = 404;

        public static bool CheckStatusCode(int code)
        {
            switch(code)
            {
                case OK:
                case CREATED:
                case ACCEPTED:
                    return true;
                default:
                    return false;
            }
        }
    }
}
