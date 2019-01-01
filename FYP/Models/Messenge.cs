using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Messenge
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string MessageContent { get; set; }
        public string From { get; set; }
        public DateTime DateTimePosted { get; set; }
    }
}