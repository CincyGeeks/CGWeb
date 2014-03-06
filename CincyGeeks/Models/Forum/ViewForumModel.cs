using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ViewForumModel
    {
        public Guid ForumId;
        public string ForumTitle;
        public bool IsPublic;
        public List<ForumTopicModel> ForumTopics;
    }
}