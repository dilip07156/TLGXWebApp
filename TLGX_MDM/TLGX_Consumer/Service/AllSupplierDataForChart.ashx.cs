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
   
    public class AllSupplierDataForChart : IHttpHandler
    {

        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        public void ProcessRequest(HttpContext context)
        {
            var PriorityId = context.Request.QueryString["PriorityId"];
            var res = MapSvc.GetMappingStatisticsForSuppliers(PriorityId);
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
