using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ViewTopicModel
    {
        public int CurrentPage;
        public int MaxPages;
        public ViewForumModel ParentForum;
        public ForumTopicModel CurrentTopic;
        public List<ForumThreadModel> Threads; 
    }
}