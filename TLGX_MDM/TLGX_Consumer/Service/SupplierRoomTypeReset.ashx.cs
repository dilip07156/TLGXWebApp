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
    /// Summary description for SupplierRoomTypeReset
    /// </summary>
    public class SupplierRoomTypeReset : IHttpHandler
    {
        Controller.MappingSVCs _mapping = new Controller.MappingSVCs();

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "application/json";
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = System.Web.HttpUtility.UrlDecode(strJson);
            List<MDMSVC.DC_SupplierRoomType_TTFU_RQ> objPlaces = JsonConvert.DeserializeObject<List<MDMSVC.DC_SupplierRoomType_TTFU_RQ>>(strDecodedJson);
            objPlaces.ForEach(x => x.Edit_User = System.Web.HttpContext.Current.User.Identity.Name);
            var result = _mapping.AccomodationSupplierRoomTypeMapping_Reset(objPlaces);
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