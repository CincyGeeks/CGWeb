using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ForumTopicRequestModel
    {
        public Guid TopicId { get; set; }
        public int CurrentPage { get; set; }
    }
}