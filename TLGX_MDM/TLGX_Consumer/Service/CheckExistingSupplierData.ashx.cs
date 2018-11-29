using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for CheckExistingSupplierData
    /// </summary>
    public class CheckExistingSupplierData : IHttpHandler
    {
        Controller.ScheduleDataSVCs ScheduleDataSVCs = new Controller.ScheduleDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = System.Web.HttpUtility.UrlDecode(strJson);
            List<MDMSVC.DC_Supplier_Schedule_RQ> obj = JsonConvert.DeserializeObject<List<MDMSVC.DC_Supplier_Schedule_RQ>> (strDecodedJson);
                      
            var result = ScheduleDataSVCs.CheckSupplierScheduleData(obj.FirstOrDefault());
            context.Response.Write(new JavaScriptSerializer().Serialize(result));
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