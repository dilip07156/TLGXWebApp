//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TLGX_Consumer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteMap
    {
        public System.Guid SiteMap_ID { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Roles { get; set; }
        public Nullable<int> Parent { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Create_User { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Edit_User { get; set; }
        public Nullable<bool> IsSiteMapNode { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.Guid applicationId { get; set; }
    }
}
