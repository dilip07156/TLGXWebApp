using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for GetScheduleTaskAssigned
    /// </summary>
    public class GetScheduleTaskAssigned : IHttpHandler
    {
        Controller.ScheduleDataSVCs MapSvc = new Controller.ScheduleDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
            string strUserName = System.Web.HttpContext.Current.User.Identity.Name;
            string RedirectFromAlert = string.Empty;
            RQ.UserName = strUserName;
            RQ.PageNo = 1;         
            var resultboth = MapSvc.GetScheduleTaskByRoll(RQ);
            RQ.RedirectFrom = "Alert";
            var Alertlog = MapSvc.GetScheduleTaskByRoll(RQ);
            List<int> response = new List<int>();
            int Alertlogcount= Alertlog.Count();
            response.Add(Alertlogcount);
            int logCount = resultboth.Count();
            response.Add(logCount);
            context.Response.Write (new JavaScriptSerializer().Serialize(response));
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