using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for HotelMappingAutoComplete
    /// </summary>
    public class HotelMappingAutoComplete : IHttpHandler
    {
        Controller.AccomodationSVC AccoSvc = new Controller.AccomodationSVC();
        MDMSVC.DC_Accomodation_AutoComplete_RQ RQ;

        public void ProcessRequest(HttpContext context)
        {
            var Source = context.Request.QueryString["source"];
            if (Source != null && Source == "autocomplete")
            {
                var PrefixText = context.Request.QueryString["term"];
                var Country = context.Request.QueryString["country"];
                var StateName = context.Request.QueryString["state"];
                RQ = new MDMSVC.DC_Accomodation_AutoComplete_RQ();
                if (!string.IsNullOrWhiteSpace(PrefixText))
                {
                    if (Convert.ToString(PrefixText).Length > 2)
                    {
                        RQ.HotelName = PrefixText.Trim().TrimStart(' ');
                        if (!string.IsNullOrWhiteSpace(Country))
                            RQ.Country = Country.Trim().TrimStart(' ');
                        if (!string.IsNullOrWhiteSpace(StateName) && StateName != "---ALL---")
                            RQ.State = StateName.Trim();
                        RQ.PageNo = 0;
                        var res = AccoSvc.SearchHotelsAutoComplete(RQ);
                        context.Response.Write(new JavaScriptSerializer().Serialize(res));

                    }
                    else
                        context.Response.Write(new JavaScriptSerializer().Serialize(null));

                }

            }
            if (Source != null && Source == "details")
            {
                var accoid = context.Request.QueryString["accoid"];
                if (accoid != null)
                {
                    var res = AccoSvc.GetAccomodationBasicInfo(Guid.Parse(accoid));
                    context.Response.Write(new JavaScriptSerializer().Serialize(res));
                }
                else
                {
                    context.Response.Write(new JavaScriptSerializer().Serialize(null));
                }
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