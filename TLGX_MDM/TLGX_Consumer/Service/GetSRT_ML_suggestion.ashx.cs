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
    /// Summary description for CountryAutoComplete
    /// </summary>

    public class GetSRT_ML_suggestion : IHttpHandler
    {

        Controller.AccomodationSVC Acco = new Controller.AccomodationSVC();
        public void ProcessRequest(HttpContext context)
        {
            var Accommodation_SupplierRoomTypeMapping_Id = context.Request.QueryString["acco_SupplierRoomTypeMapping_Id"];
            //RQ = new MDMSVC.DC_RoomCategoryMaster_RQ();
            if (Accommodation_SupplierRoomTypeMapping_Id != "")
            {
                var res = Acco.GetRTM_ML_Suggestions(Convert.ToString(Accommodation_SupplierRoomTypeMapping_Id));
                context.Response.Write(new JavaScriptSerializer().Serialize(res));
            }
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
