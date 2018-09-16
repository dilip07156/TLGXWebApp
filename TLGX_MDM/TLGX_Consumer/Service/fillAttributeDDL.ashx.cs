using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for fillAttributeDDL
    /// </summary>
    public class fillAttributeDDL : IHttpHandler
    {
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();

        public void ProcessRequest(HttpContext context)
        {
            var OptionFor = context.Request.QueryString["OptionFor"];
            var Attribute_Name = context.Request.QueryString["Attribute_Name"];
            var result = LookupAtrributes.GetAllAttributeAndValuesByFOR(OptionFor, Attribute_Name).MasterAttributeValues;
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