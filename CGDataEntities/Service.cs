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
    
    public partial class Service
    {
        public Service()
        {
            this.Service_Roles = new HashSet<Service_Roles>();
        }
    
        public System.Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
    
        public virtual ICollection<Service_Roles> Service_Roles { get; set; }
    }
}
