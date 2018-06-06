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
    public class ActivityMediaInfo : IHttpHandler
    {

        MDMSVC.DC_Activity_Media_Search_RQ RQParams = new MDMSVC.DC_Activity_Media_Search_RQ();       
        ActivitySVC _Objmaster = new ActivitySVC();      
        public void ProcessRequest(HttpContext context)
       {
            var Activity_Flavour_Id = context.Request.QueryString["ActFlavID"];
            RQParams.Activity_Flavour_Id =  new Guid(Activity_Flavour_Id);
            
            var res = _Objmaster.GetActivityMedia(RQParams);

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