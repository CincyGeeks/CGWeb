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
    
    public partial class ForumReply
    {
        public System.Guid ReplyId { get; set; }
        public System.Guid ParentThreadId { get; set; }
        public string ReplyContent { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual ForumThread ForumThread { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
