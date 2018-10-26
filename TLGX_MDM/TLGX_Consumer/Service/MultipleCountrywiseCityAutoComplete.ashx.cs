using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for MultipleCountrywiseCityAutoComplete
    /// </summary>
    public class MultipleCountrywiseCityAutoComplete : IHttpHandler
    {
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var javascriptserializer = new JavaScriptSerializer();
            javascriptserializer.MaxJsonLength = Int32.MaxValue;
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = HttpUtility.UrlDecode(strJson);
            var CountryIdList = JsonConvert.DeserializeObject<MDMSVC.DC_CitywithMultipleCountry_Search_RQ> (strDecodedJson);
            var result = masterSVc.GetCountrywiseCitiesList(CountryIdList);
            context.Response.Write(javascriptserializer.Serialize(result));
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