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

    public class ToFillDDL : IHttpHandler
    {

        Controller.MasterDataSVCs msterdata = new Controller.MasterDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            //Get ForWhich Dropdown List
            var EntityType = context.Request.QueryString["EntityType"];

            if (EntityType.ToLower().Trim() == "activity")
            {
                var CountryName = context.Request.QueryString["countryname"];
                var CityName = context.Request.QueryString["cityname"];
                var res = msterdata.GetActivityByCountryCity(CountryName, CityName);
                if (res != null)
                    context.Response.Write(new JavaScriptSerializer().Serialize(res));
            }
            if (EntityType.ToLower().Trim() == "country")
            {
                var res = msterdata.GetMasterCountryDataList();
                if (res != null)
                    context.Response.Write(new JavaScriptSerializer().Serialize(res));
            }
            if (EntityType.ToLower().Trim() == "acco")
            {
                Controller.AccomodationSVC accoSVc = new Controller.AccomodationSVC();
                var CountryName = context.Request.QueryString["countryname"];
                var CityName = context.Request.QueryString["cityname"];
                MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
                RQParams.ProductCategory = "Accommodation";

                if (!string.IsNullOrWhiteSpace(CountryName))
                    RQParams.Country = CountryName.ToString();
                if (!string.IsNullOrWhiteSpace(CityName))
                    if (CityName != "&NBSP;")
                        RQParams.City = CityName.ToString();

                RQParams.PageNo = 0;
                RQParams.PageSize = int.MaxValue;
                var res = accoSVc.SearchHotels(RQParams);
                if (res != null)
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
