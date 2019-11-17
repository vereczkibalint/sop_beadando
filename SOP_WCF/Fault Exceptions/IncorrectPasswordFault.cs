using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SOP_WCF
{
    [DataContractAttribute]
    public class IncorrectPasswordFault
    {
        private string report;

        public IncorrectPasswordFault(string message)
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