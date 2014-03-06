using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum
{
    public class ViewThreadModel
    {
        public ForumTopicModel ParentTopic;
        public ForumThreadModel CurrentThread;
        public List<ForumReplyModel> Replies;
        public int CurrentPage;
        public int MaxPages;
    }
}