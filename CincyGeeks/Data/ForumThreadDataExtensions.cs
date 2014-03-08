using CGDataEntities;
using CincyGeeksWebsite.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Data
{
    public static class ForumThreadDataExtensions
    {
        public static ForumThreadModel ConvertToForumThreadModel(this ForumThread thread, bool includeContent)
        {
            return new ForumThreadModel()
            {
                CreatedBy = thread.UserProfile.UserName,
                CreatedById = thread.UserProfile.UserId,
                IsSticky = thread.IsSticky,
                CreatedOn = thread.CreatedOn.ToShortDateString() + thread.CreatedOn.ToShortTimeString(),
                ModifiedOn = thread.ModifiedOn.HasValue ? thread.ModifiedOn.Value.ToShortDateString() + thread.ModifiedOn.Value.ToShortTimeString() : "",
                ThreadId = thread.ThreadId,
                ThreadTitle = thread.ThreadTitle,
                ThreadContent = includeContent ? thread.ThreadContent : ""
            };
        }
    }
}