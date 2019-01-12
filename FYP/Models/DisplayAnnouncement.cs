using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class DisplayAnnouncement
    {

        public string Title { get; set; }
        public string Content { get; set; }

        public string Path { get; set; }

        public List<DisplayAnnouncement> Announcesdata { get; set; }

    }
}