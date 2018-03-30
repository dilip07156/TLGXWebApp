using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for IncludeExcludeZoneHotels
    /// </summary>
    public class IncludeExcludeZoneHotels : IHttpHandler
    {
        Controller.MasterDataSVCs MapSvc = new Controller.MasterDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            MDMSVC.DC_ZoneRQ RQ = new MDMSVC.DC_ZoneRQ();
            RQ.ZoneProductMapping_Id = new Guid(context.Request.QueryString["zoneProdMapId"]);
            bool includestatus = Convert.ToBoolean(context.Request.QueryString["includestatus"]);
            if (includestatus == false)
                RQ.Included = true;
            else
                RQ.Included = false;
            var res = MapSvc.IncludeExcludeHotels(RQ);
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