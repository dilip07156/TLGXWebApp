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
    
    public partial class Project_WorkflowStepsActivity
    {
        public System.Guid WorkflowStep_Acticity_ID { get; set; }
        public Nullable<System.Guid> WorkflowStep_ID { get; set; }
        public Nullable<System.Guid> ActivityMaster_ID { get; set; }
        public Nullable<int> Hierarchy { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string CREATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
    }
}
