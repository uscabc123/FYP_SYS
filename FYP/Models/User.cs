using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Models
{
    public class User
    {
        
            [DisplayName("User ID")]
            public string UserID { get; set; }
            [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7,8})$", ErrorMessage = "Invalid Contact Number")]
        public string PhoneNumber { get; set; }
            [Required]
            [DisplayName("First Name")]

            [RegularExpression(@"^([a-zA-Z]+(_[a-zA-Z]+)*)(\s([a-zA-Z]+(_[a-zA-Z]+)*))*$", ErrorMessage = "Invalid First Name")]
            public string FName { get; set; }
            [Required]
            [DisplayName("Last Name")]

            [RegularExpression(@"^([a-zA-Z]+(_[a-zA-Z]+)*)(\s([a-zA-Z]+(_[a-zA-Z]+)*))*$", ErrorMessage = "Invalid Last Name")]
            public string LName { get; set; }
            [Required]
            [DisplayName("IC/Passport No")]
            public string ICPassport { get; set; }
            [Required]
            [DisplayName("Email")]

            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }
            [Required]
            public int Gender { get; set; }

            [Required]
            [DisplayName("Account Status")]
            public int UserStatusID { get; set; }

            public int UserRoleID { get; set; }
            [Required]
            [DisplayName("User Role")]

            public int UserRoleListID { get; set; }
            [DisplayName("Role")]

            public string UserRoleListValue { get; set; }

            [Required]
            public int UserStatusListID { get; set; }
            [DisplayName("Status")]

            public string UserStatusListValue { get; set; }

                public string UserPassword { get; set; }

            public int status { get; set; }

            public string searchuser { get; set; }


            public List<User> userdata { get; set; }

    }
}