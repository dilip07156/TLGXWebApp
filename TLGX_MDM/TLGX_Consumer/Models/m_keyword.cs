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
    
    public partial class m_keyword
    {
        public System.Guid Keyword_Id { get; set; }
        public string Keyword { get; set; }
        public Nullable<bool> Missing { get; set; }
        public Nullable<bool> Extra { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Create_User { get; set; }
        public Nullable<System.DateTime> Edit_Date { get; set; }
        public string Edit_User { get; set; }
        public string Status { get; set; }
        public Nullable<bool> Attribute { get; set; }
        public Nullable<int> Sequence { get; set; }
        public string Icon { get; set; }
        public string EntityFor { get; set; }
        public string AttributeType { get; set; }
        public string AttributeLevel { get; set; }
        public string AttributeSubLevel { get; set; }
        public string AttributeSubLevelValue { get; set; }
    }
}
