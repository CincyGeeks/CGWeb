using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ForumThreadModel
    {
        public Guid ThreadId;
        public string ThreadTitle;
        public string ThreadContent;
        public string CreatedBy;
        public int CreatedById;
        public string CreatedOn;
        public string ModifiedOn;
        public bool IsSticky;
    }
}