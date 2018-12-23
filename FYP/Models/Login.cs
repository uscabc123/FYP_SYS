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
        public string message { get; set; }
        public string fullname { get; set; }
        public int role { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }

    //    public string ReturnURL { get; set; }

        public bool isRemember { get; set; }

    }
}