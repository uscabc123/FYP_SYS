using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class MessageList
    {
        public string Receiver { get; set; }
        public string Sender { get; set; }

        public string Sender_ReceiverID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserID { get; set; }
        public List<MessageList> messageList { get; set; }

    }
}