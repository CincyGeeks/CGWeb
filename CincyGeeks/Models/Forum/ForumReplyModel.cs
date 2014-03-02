using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ForumReplyModel
    {
        public Guid ReplyId;
        public string ReplyContent;
        public int CreatedById;
        public string CreatedBy;
        public string CreatedOn;
        public string ModifiedOn;
    }
}