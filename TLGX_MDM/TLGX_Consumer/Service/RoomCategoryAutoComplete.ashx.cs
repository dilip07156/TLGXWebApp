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

    public class RoomCategoryAutoComplete : IHttpHandler
    {
        Controller.AccomodationSVC Acco = new Controller.AccomodationSVC();
        MDMSVC.DC_RoomCategoryMaster_RQ RQ = new MDMSVC.DC_RoomCategoryMaster_RQ();
        public void ProcessRequest(HttpContext context)
        {
            var PrefixText = context.Request.QueryString["term"];
            var type = context.Request.QueryString["type"];
            var acco_id = context.Request.QueryString["acco_id"];

            if (type != null && type == "fillcategory")
            {
                RQ = new MDMSVC.DC_RoomCategoryMaster_RQ();
                if (acco_id != "")
                {
                    var res = Acco.GetRoomDetails_RoomCategory(Guid.Parse(acco_id));
                    context.Response.Write(new JavaScriptSerializer().Serialize(res));
                }
            }
            if (type != null && type == "fillcategorywithdetails")
            {
                RQ = new MDMSVC.DC_RoomCategoryMaster_RQ();
                if (acco_id != "")
                {
                    var res = Acco.GetRoomDetails_RoomCategoryWithDetails(Guid.Parse(acco_id));
                    context.Response.Write(new JavaScriptSerializer().Serialize(res));
                }
            }
            else
            {
                RQ = new MDMSVC.DC_RoomCategoryMaster_RQ();
                if (PrefixText != "")
                    RQ.RoomCategory = PrefixText;
                var res = Acco.GetRoomCategoryMaster(RQ);
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
