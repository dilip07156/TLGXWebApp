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
    
    public partial class Project_ViewBagDuration
    {
        public System.Guid Project_Step_Duration_ID { get; set; }
        public Nullable<System.Guid> ViewBag_ID { get; set; }
        public Nullable<System.Guid> Project_ID { get; set; }
        public Nullable<System.Guid> Project_Workflow_Step_ID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
    }
}
