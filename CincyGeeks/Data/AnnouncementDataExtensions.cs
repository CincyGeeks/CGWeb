using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CGDataEntities;
using CincyGeeksWebsite.Models.Shared;

namespace CincyGeeksWebsite.Data
{
    public static class AnnouncementDataExtensions
    {
        public static AnnouncementPartialModel BuildAnnouncementPartialModel(this Announcement announce)
        {
            return new AnnouncementPartialModel()
            {
                Content = announce.Content,
                CreatedBy = announce.CreatedUserProfile.UserName,
                CreatedOn = announce.CreatedDate.ToShortDateString(),
                ModifiedBy = announce.ModifiedUserProfile != null ? announce.ModifiedUserProfile.UserName : "",
                ModifiedOn = announce.ModifiedDate.HasValue ? announce.ModifiedDate.Value.ToShortDateString() : "",
                RoleRestriction = announce.RestrictToRole.HasValue ? announce.RoleRestriction.RoleName : "",
                Title = announce.Title
            };
        }
    }
}