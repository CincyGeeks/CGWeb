using CGDataEntities;
using CincyGeeksWebsite.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Data
{
    public static class ForumTopicDataExtensions
    {
        public static ForumTopicModel ConvertToForumTopicModel(this ForumTopic topic)
        {
            return new ForumTopicModel()
            {
                CreatedBy = topic.UserProfile.UserName,
                CreatedOn = topic.CreatedOn.ToShortDateString(),
                TopicDescription = topic.TopicDescription,
                TopicId = topic.TopicId,
                TopicTitle = topic.TopicTitle
            };
        }
    }
}