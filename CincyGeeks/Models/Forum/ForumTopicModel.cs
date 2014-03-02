using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ForumTopicModel
    {
        public Guid TopicId;
        public string TopicTitle;
        public string TopicDescription;
        public string CreatedBy;
        public string CreatedOn;
    }
}