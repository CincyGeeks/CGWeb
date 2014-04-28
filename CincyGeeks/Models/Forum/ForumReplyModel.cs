using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ForumReplyModel
    {
        public Guid ThreadId { get; set; }
        public Guid ReplyId { get; set; }
        [AllowHtml]
        public string ReplyContent { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
    }
}