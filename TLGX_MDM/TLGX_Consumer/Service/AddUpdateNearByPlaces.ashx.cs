using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Script.Serialization;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for CityAutoComplete
    /// </summary>

    public class AddUpdateNearByPlaces : IHttpHandler
    {
         
        Controller.AccomodationSVC Acco = new Controller.AccomodationSVC();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = System.Web.HttpUtility.UrlDecode(strJson);
            MDMSVC.DC_GooglePlaceNearByWithAccoID objPlaces = JsonConvert.DeserializeObject<MDMSVC.DC_GooglePlaceNearByWithAccoID>(strDecodedJson);
            objPlaces.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            var res = Acco.AddUpdatePlaces(objPlaces);
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
