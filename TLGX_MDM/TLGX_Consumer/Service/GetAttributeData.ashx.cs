using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for GetAttributeData
    /// </summary>
    public class GetAttributeData : IHttpHandler
    {
        Controller.MappingSVCs _mapping = new Controller.MappingSVCs();
        public void ProcessRequest(HttpContext context)
        {
            var acco_SupplierRoomTypeMapping_Id = context.Request.QueryString["acco_SupplierRoomTypeMapping_Id"];
            var result = _mapping.GetAttributeForAccomodationSupplierRoomTypeMapping(acco_SupplierRoomTypeMapping_Id);
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