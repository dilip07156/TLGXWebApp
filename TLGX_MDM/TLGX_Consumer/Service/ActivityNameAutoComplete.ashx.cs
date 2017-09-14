using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Script.Serialization;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for HotelNameAutoComplete
    /// </summary>
    public class ActivityNameAutoComplete : IHttpHandler
    {

        MDMSVC.DC_Activity_Search_RQ RQParams = new MDMSVC.DC_Activity_Search_RQ();       
        MasterDataSVCs _Objmaster = new MasterDataSVCs();      
        public void ProcessRequest(HttpContext context)
       {
            var PrefixText = context.Request.QueryString["term"];
            var Country = context.Request.QueryString["country"];
            var City = context.Request.QueryString["city"];
            var ckisproducttype = context.Request.QueryString["ckisproducttype"];

            RQParams = new MDMSVC.DC_Activity_Search_RQ();
            RQParams.Status = "ACTIVE";
            if (PrefixText != "")
                RQParams.Name = PrefixText;
            if (!string.IsNullOrWhiteSpace(Country))
            {
                if (Country.IndexOf("-") == -1)
                    RQParams.Country = Country;
            }
            if (!string.IsNullOrWhiteSpace(City))
            {
                if (City.IndexOf("-") == -1)
                    RQParams.City = City;
            }
            if (!string.IsNullOrWhiteSpace(ckisproducttype))
            {
                if (ckisproducttype.IndexOf("-") == -1)
                    RQParams.ProductCategorySubType = ckisproducttype;
            }

            RQParams.PageNo = 0;
            RQParams.PageSize = 500;
            var res = _Objmaster.GetActivityNames(RQParams);

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