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
   
    public class CityFillForDDL : IHttpHandler
    {

        Controller.MasterDataSVCs msterdata = new Controller.MasterDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            var Country_Id = context.Request.QueryString["country_Id"];
            var res = msterdata.GetMasterCityData(Country_Id);
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
