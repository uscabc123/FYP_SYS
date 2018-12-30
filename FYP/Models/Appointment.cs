using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Appointment
    {
        
        [Display(Name = "Enter Date")]
        public DateTime AppointmentDate { get; set; }

    
        public DateTime AppointmentTime { get; set; }




    }
}