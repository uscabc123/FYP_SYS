using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class SearchAnnouncement
    {
        public int AnnouncemntID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Path { get; set; }

        public DateTime AnnouncementDate { get; set; }

        public string CreatedBy { get; set; }

        public int Status { get; set; }

        public string comboStatusValue { get; set; }

        [Required]
        public string searchannouncement { get; set; }



    }
}