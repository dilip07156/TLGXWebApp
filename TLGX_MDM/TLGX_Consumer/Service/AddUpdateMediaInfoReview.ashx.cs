using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for AddUpdateMediaInfoReview
    /// </summary>
    public class AddUpdateMediaInfoReview : IHttpHandler
    {

        MDMSVC.DC_Activity_Media_Search_RQ RQParams = new MDMSVC.DC_Activity_Media_Search_RQ();
        ActivitySVC _Objmaster = new ActivitySVC();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            string strDecodedJson = System.Web.HttpUtility.UrlDecode(strJson);
            List<MDMSVC.DC_Activity_MediaReview> obj = JsonConvert.DeserializeObject<List<MDMSVC.DC_Activity_MediaReview>>(strDecodedJson);
            var result = _Objmaster.AddUpdateActivityMediaReview(obj.FirstOrDefault());
            context.Response.Write(new JavaScriptSerializer().Serialize(result));
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