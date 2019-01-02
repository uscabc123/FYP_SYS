using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Consultation
    {
        public User User { get; set; }
        public string remarks { get; set; }
        [Required]
        public string symptoms { get; set; }

        [Required]
        public string diagnose { get; set; }

    }
}