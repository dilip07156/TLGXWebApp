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
    /// Summary description for HotelNameAutoComplete
    /// </summary>
    public class HotelNameAutoComplete : IHttpHandler
    {

        MasterDataDAL masterdata = new MasterDataDAL();
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        public DataTable dtCountryMappingSearchResults = new DataTable();
        public DataTable dtCountrMappingDetail = new DataTable();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        MasterDataDAL masters = new MasterDataDAL();
        public static string AttributeOptionFor = "ProductSupplierMapping";
        public static string AttributeOptionFor1 = "HotelInfo";
        public static string SortBy = "";
        public static string SortEx = "";
        public static string via = "";
        public static int PageIndex = 0;
        public void ProcessRequest(HttpContext context)
        {
            var PrefixText = context.Request.QueryString["term"];
            var Country = context.Request.QueryString["country"];
            var City = context.Request.QueryString["city"];
            var Chain = context.Request.QueryString["chain"];
            var Brand = context.Request.QueryString["brand"];
            // var prefixText = context.Items["key"];

            RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
            RQParams.ProductCategory = "Accommodation";
            RQParams.ProductCategorySubType = "Hotel"; //txtProdCategoryt.Text;
            //RQParams.Status = ddlProductMappingStatus.SelectedItem.Text == "ACTIVE" ? true : false;
            //RQParams.Status = true;
            RQParams.Status = "ACTIVE";
            if (PrefixText != "")
                RQParams.HotelName = PrefixText;
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
            if (!string.IsNullOrWhiteSpace(Chain))
            {
                if (Chain.IndexOf("-") == -1)
                    RQParams.Chain = Chain;
            }
            if (!string.IsNullOrWhiteSpace(Brand))
            {
                if (Brand.IndexOf("-") == -1)
                    RQParams.Brand = Brand;
            }

            RQParams.PageNo = PageIndex;
            RQParams.PageSize = 500;
            var res = AccSvc.GetAccomodationNames(RQParams);

            //var ret = new List<string>();
            //ret.Add("one");
            //ret.Add("two");

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("one,two");
            context.Response.Write(new JavaScriptSerializer().Serialize(res));
        }

        
        

        [System.Web.Script.Services.ScriptMethodAttribute(), System.Web.Services.WebMethodAttribute()]
        public string[] GetHotelList(string prefixText, int count, string contextKey)
        {
            var ret = new List<string>();
            ret.Add("one");
            ret.Add("two");
            return ret.ToArray();
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