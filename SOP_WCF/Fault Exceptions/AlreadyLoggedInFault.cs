using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SOP_WCF
{
    [DataContractAttribute]
    public class AlreadyLoggedInFault
    {
        private string report;

        public AlreadyLoggedInFault(string message)
        {
            this.report = message;
        }

        [DataMemberAttribute]
        public string Message
        {
            get { return this.report; }
            set { this.report = value; }
        }
    }
}