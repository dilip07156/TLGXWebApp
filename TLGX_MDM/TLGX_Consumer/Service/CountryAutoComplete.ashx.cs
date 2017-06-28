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
    
    public class CountryAutoComplete : IHttpHandler
    {
        Controller.MasterDataSVCs msterdata = new Controller.MasterDataSVCs();
        MDMSVC.DC_Country_Search_RQ RQ = new MDMSVC.DC_Country_Search_RQ();
        public void ProcessRequest(HttpContext context)
        {
            var PrefixText = context.Request.QueryString["term"];
            RQ = new MDMSVC.DC_Country_Search_RQ();
            if (PrefixText != "")
                RQ.Country_Name = PrefixText;
            var res = msterdata.GetCountryNameList(RQ);
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
