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
   
    public class CityAutoComplete : IHttpHandler
    {

        Controller.MasterDataSVCs msterdata = new Controller.MasterDataSVCs();
        MDMSVC.DC_City_Search_RQ RQ = new MDMSVC.DC_City_Search_RQ();
        public void ProcessRequest(HttpContext context)
        {
            var PrefixText = context.Request.QueryString["term"];
            var Country = context.Request.QueryString["country"];
            RQ = new MDMSVC.DC_City_Search_RQ();
            if (Country != "")
                RQ.Country_Name = Country;
            if (PrefixText != "")
                RQ.City_Name = PrefixText;
            var res = msterdata.GetCityNameList(RQ);
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
