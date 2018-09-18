using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for AddUpdateMappingDetails
    /// </summary>
    public class AddUpdateMappingDetails : IHttpHandler
    {
        Controller.MappingSVCs MappingSVC = new Controller.MappingSVCs();
        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "application/json";
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = System.Web.HttpUtility.UrlDecode(strJson);
            List<MDMSVC.DC_Accommodation_SupplierRoomTypeMapping_Values> objRoomMappingValues = JsonConvert.DeserializeObject<List<MDMSVC.DC_Accommodation_SupplierRoomTypeMapping_Values>>(strDecodedJson);
            var result = MappingSVC.UpdateAccomodationSupplierRoomTypeMapping(objRoomMappingValues);
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