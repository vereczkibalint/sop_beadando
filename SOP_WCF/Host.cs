using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOP_WCF
{
    public class Host
    {
        public static Object toLock = new Object();
        public static List<UserClient> loggedIn = new List<UserClient>();
    }
}