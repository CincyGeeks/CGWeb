using CGDataEntities;
using CincyGeeksWebsite.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Data
{
    public static class ForumDataExtensions
    {
        public static ViewForumModel ConvertToViewForumModel(this Forum forum, bool includeTopics, bool publicOnly)
        {
            ViewForumModel returnValue = new ViewForumModel()
            {
                ForumId = forum.ForumId,
                ForumTitle = forum.ForumTitle,
                IsPublic = forum.IsPublic
            };

            if (includeTopics)
            {
                returnValue.ForumTopics = new List<ForumTopicModel>();

                if (publicOnly)
                {
                    foreach (ForumTopic topic in forum.ForumTopics.Where(FT => FT.IsPublic).OrderBy(FT => FT.TopicTitle))
                        returnValue.ForumTopics.Add(topic.ConvertToForumTopicModel());
                }
                else
                {
                    foreach (ForumTopic topic in forum.ForumTopics.OrderBy(FT => FT.TopicTitle))
                        returnValue.ForumTopics.Add(topic.ConvertToForumTopicModel());
                }
            }

            return returnValue;
        }
    }
}