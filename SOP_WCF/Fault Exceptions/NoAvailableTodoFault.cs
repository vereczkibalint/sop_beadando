using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SOP_WCF
{
    [DataContractAttribute]
    public class NoAvailableTodoFault
    {
        private string report;

        public NoAvailableTodoFault(string message)
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