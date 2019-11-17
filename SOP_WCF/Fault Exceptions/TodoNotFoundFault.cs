using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SOP_WCF
{
    [DataContractAttribute]
    public class TodoNotFoundFault
    {
        private string report;

        public TodoNotFoundFault(string message)
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