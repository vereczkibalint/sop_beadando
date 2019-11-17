using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOP_WCF
{
    public class JSONResponse
    {
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public JSONResponse()
        {

        }
    }
}