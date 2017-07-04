using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for HotelNameAutoComplete
    /// </summary>
    public class GetCodeById_Service : IHttpHandler
    {
        MasterDataSVCs _obj = new MasterDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            var PrefixText = context.Request.QueryString["state_id"];
            var res = _obj.GetCodeById("state", PrefixText);
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