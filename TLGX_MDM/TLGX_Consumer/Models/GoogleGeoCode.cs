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
    
    public partial class GoogleGeoCode
    {
        public System.Guid GoogleGeoCode_Id { get; set; }
        public string JobType { get; set; }
        public string Input { get; set; }
        public string OutPut { get; set; }
        public Nullable<System.Guid> City_Id { get; set; }
        public string gCountryName { get; set; }
        public string gCountryCode { get; set; }
        public string gCityName { get; set; }
        public string gCityCode { get; set; }
        public string gStateName { get; set; }
        public string gStateCode { get; set; }
        public Nullable<System.Guid> State_Id { get; set; }
        public string g_adminarea_level2_name { get; set; }
        public string g_adminarea_level2_code { get; set; }
        public string administrative_area_level_3_name { get; set; }
        public string administrative_area_level_3_code { get; set; }
        public string ExitPoint { get; set; }
        public Nullable<System.Guid> Product_Id { get; set; }
    }
}
