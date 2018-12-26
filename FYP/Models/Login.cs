using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Models
{
    public class Login
    {
        public string UserID { get; set; }

        public string Fname { get; set; }
        public string Lname { get; set; }
        public int status { get; set; }

        public int role { get; set; }
        public int gender { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }



    }
}