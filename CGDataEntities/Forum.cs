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
    
    public partial class Forum
    {
        public Forum()
        {
            this.ForumTopics = new HashSet<ForumTopic>();
        }
    
        public System.Guid ForumId { get; set; }
        public string ForumTitle { get; set; }
        public bool IsPublic { get; set; }
    
        public virtual ICollection<ForumTopic> ForumTopics { get; set; }
    }
}
