using CGDataEntities;
using CincyGeeksWebsite.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Data
{
    public static class ForumReplyDataExtensions
    {
        public static ForumReplyModel ConvertToThreadReplyModel(this ForumReply reply, bool returnContent)
        {
            return new ForumReplyModel()
            {
                CreatedBy = reply.UserProfile.UserName,
                CreatedById = reply.UserProfile.UserId,
                CreatedOn = reply.CreatedOn.ToShortDateString() + " " + reply.CreatedOn.ToShortTimeString(),
                ModifiedOn = reply.ModifiedOn.HasValue ? reply.ModifiedOn.Value.ToShortDateString() + " " + reply.ModifiedOn.Value.ToShortTimeString() : "",
                ReplyContent = returnContent ? reply.ReplyContent : "",
                ReplyId = reply.ReplyId
            };
        }
    }
}