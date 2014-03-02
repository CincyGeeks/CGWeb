using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Administrative
{
    public class RoleManagementModel
    {
        public int UserId { get; set; }
        public bool Root { get; set; }
        public bool Administrator { get; set; }
        public bool Moderator { get; set; }
        public bool User { get; set; }
        public bool Announcer { get; set; }
    }
}