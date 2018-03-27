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
            var Source = context.Request.QueryString["source"];
            var StateCode = context.Request.QueryString["statecode"];
            var StateName = context.Request.QueryString["statename"];
            var City_Id = context.Request.QueryString["city_id"];

            RQ = new MDMSVC.DC_City_Search_RQ();
            if (!string.IsNullOrWhiteSpace(Country))
                RQ.Country_Name = Country.Trim().TrimStart(' ');
            if (!string.IsNullOrWhiteSpace(PrefixText)) 
                RQ.City_Name = PrefixText.Trim();
            if (!string.IsNullOrWhiteSpace(StateName)) 
                RQ.State_Name = StateName.Trim();
            if (!string.IsNullOrWhiteSpace(City_Id))
                RQ.City_Id = Guid.Parse(City_Id.Trim());

            if (Source != null)
            {
                RQ.Status = "ACTIVE";
                RQ.PageNo = 0;
                RQ.PageSize = int.MaxValue;
                var res = msterdata.GetCityMasterData(RQ);
                context.Response.Write(new JavaScriptSerializer().Serialize(res));
            }
            else
            {
                var res = msterdata.GetCityNameList(RQ);
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
