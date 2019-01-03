using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class ConsultationSearch
    {
        [Required]
        public string searchvalue { get; set; }

        public Consultation consult { get; set; }
        public User patient { get; set; }

    }
}