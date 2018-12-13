using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TLGX_Consumer.MDMSVC;

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
            RQ.UserName = strUserName;
            List<Supplier_Task_Notifications> notificationslist = new List<Supplier_Task_Notifications>();
            notificationslist= MapSvc.GetScheduleNotificationTaskLog(RQ);
            var bellNotificationCount = (from a in notificationslist
                                         where a.NotificationType == "API" && a.Status_Message=="Error"
                                         select a).FirstOrDefault().Notification_Count;
            var bullhornNotificationCount = (from a in notificationslist
                                             where a.NotificationType == "File" && a.Status_Message != "Completed"
                                             select a.Notification_Count).Sum();

            //var response = MapSvc.GetScheduleNotificationTaskLog(RQ);
            var data = new { bellNotification = bellNotificationCount, bullhornNotification = bullhornNotificationCount };
            context.Response.Write(new JavaScriptSerializer().Serialize(data));
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