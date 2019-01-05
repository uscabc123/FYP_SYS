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

        public int followup { get; set; }

        public string diagnose { get; set; }
        public string doctorid { get; set; }
        public string remarks { get; set; }
        public string symptoms { get; set; }
        public string patientid { get; set; }

        public DateTime ConsultationDate { get; set; }
        public string UserRole { get; set; }

        public string userid { get; set; }

        public int consultID { get; set; }
        public List<ConsultationSearch> consultsdata { get; set; }


    }
}