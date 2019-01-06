using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Messenge
    {
        public int MessageID { get; set; }
        public string MessageContent { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public DateTime MessageDateTime { get; set; }
        public List<Messenge> messageData { get; set; }
    }
}