//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CGDataEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Announcement
    {
        public int AnnouncementId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public Nullable<int> RestrictToRole { get; set; }
    
        public virtual UserProfile CreatedUserProfile { get; set; }
        public virtual UserProfile ModifiedUserProfile { get; set; }
        public virtual webpages_Roles RoleRestriction { get; set; }
    }
}
