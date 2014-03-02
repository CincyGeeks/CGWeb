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
    
    public partial class ForumThread
    {
        public ForumThread()
        {
            this.ForumReplies = new HashSet<ForumReply>();
        }
    
        public System.Guid ThreadId { get; set; }
        public System.Guid ForumTopic { get; set; }
        public string ThreadTitle { get; set; }
        public string ThreadContent { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<bool> IsSticky { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual ForumTopic ParentForumTopic { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<ForumReply> ForumReplies { get; set; }
    }
}
