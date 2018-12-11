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
            //string RedirectFromAlert = string.Empty;
            RQ.UserName = strUserName;
            //RQ.PageNo = 1;
            //RQ.Notification = "Both";
            //var resultboth = MapSvc.GetScheduleTaskByRoll(RQ);
            //RQ.RedirectFrom = "Alert";
            //RQ.Notification = "Alert";
            //var Alertlog = MapSvc.GetScheduleTaskByRoll(RQ);
            //List<int> response = new List<int>();

            //int Alertlogcount = (Alertlog != null ? Alertlog.Count() : 0);
            //response.Add(Alertlogcount);
            //int logCount = (resultboth != null ? resultboth.Count() : 0);
            //response.Add(logCount);

            var response = MapSvc.GetScheduleNotificationTaskLog(RQ);

            context.Response.Write(new JavaScriptSerializer().Serialize(response));
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