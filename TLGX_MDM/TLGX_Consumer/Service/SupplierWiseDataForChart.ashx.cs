using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Script.Serialization;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for CityAutoComplete
    /// </summary>
   
    public class SupplierWiseDataForChart : IHttpHandler
    {

        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        public void ProcessRequest(HttpContext context)
        {
            var Supplier_Id = context.Request.QueryString["Supplier_Id"];
            var PriorityId = context.Request.QueryString["PriorityId"];
            var ProductCategory = context.Request.QueryString["ProductCategory"];
            string IsMDM = context.Request.QueryString["IsMDM"];
            var res = MapSvc.GetMappingStatistics(Supplier_Id, PriorityId, ProductCategory, IsMDM);
            context.Response.Write(new JavaScriptSerializer().Serialize(res));
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
