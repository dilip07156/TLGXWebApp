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
    /// Summary description for UpdateSupplierRoomTypeTrainingFlag
    /// </summary>
    public class UpdateSupplierRoomTypeTrainingFlag : IHttpHandler
    {
        Controller.MappingSVCs _mapping = new Controller.MappingSVCs();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = System.Web.HttpUtility.UrlDecode(strJson);
            List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update> obj = JsonConvert.DeserializeObject<List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update>>(strDecodedJson);
            var result = _mapping.UpdateAccomodationSupplierRoomTypeMappingTrainingFlag(obj);
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