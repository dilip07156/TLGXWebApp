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
    
    public partial class m_WorkflowSteps
    {
        public System.Guid WorkflowStep_ID { get; set; }
        public Nullable<System.Guid> Workflow_ID { get; set; }
        public Nullable<System.Guid> Activity_Master_ID { get; set; }
        public Nullable<System.Guid> Appr_status_id { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public string Step_Name { get; set; }
        public string Short_Description { get; set; }
        public Nullable<int> Order_Of_Execution { get; set; }
        public Nullable<System.Guid> m_WorkFlowMessage_Id { get; set; }
    }
}
