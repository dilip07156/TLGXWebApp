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
   
    public class GetZoneHotelsForMap : IHttpHandler
    {

        Controller.MasterDataSVCs MapSvc = new Controller.MasterDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            MDMSVC.DC_ZoneRQ RQ = new MDMSVC.DC_ZoneRQ();
            RQ.Latitude = context.Request.QueryString["Latitude"];
            RQ.Longitude = context.Request.QueryString["Longitude"];
            RQ.CountryName = context.Request.QueryString["CountryName"];
            RQ.DistanceRange = 4000;
            var res = MapSvc.SearchZoneHotels(RQ);
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
