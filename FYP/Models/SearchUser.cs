using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class SearchUser
    {

      
        public string UserID { get; set; }

        public string PhoneNumber { get; set; }
        [DisplayName("First Name")]
        public string FName { get; set; }
        [DisplayName("Last Name")]
        public string LName { get; set; }
        [DisplayName("IC/Passport No")]

        public string ICPassport { get; set; }
     
        public string Email { get; set; }
        [DisplayName("Status")]
        public string UserStatusListValue { get; set; }
        [DisplayName("Role")]
        public string UserRoleListValue { get; set; }
        [Required]
        [DisplayName("Search")]
        public string searchvalue { get; set; }

        public List<SearchUser> userdata { get; set; }

    }
}