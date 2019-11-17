using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SOP_WCF
{
    [DataContract]
    public class TodoModel
    {
        [DataMember]
        public int todo_id { get; set; }
        [DataMember]
        public string todo_title { get; set; }
        [DataMember]
        public string todo_body { get; set; }
        [DataMember]
        public string todo_deadline { get; set; }
        [DataMember]
        public string todo_author { get; set; }
        [DataMember]
        public string todo_priority { get; set; }

        public TodoModel()
        {

        }
    }
}