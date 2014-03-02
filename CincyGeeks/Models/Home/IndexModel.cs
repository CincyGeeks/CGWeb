using CincyGeeksWebsite.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Home
{
    public class IndexModel
    {
        public List<AnnouncementPartialModel> Announcements { get; set; }
    }
}