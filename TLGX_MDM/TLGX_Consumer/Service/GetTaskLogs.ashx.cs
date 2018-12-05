using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for GetTaskLogs
    /// </summary>
    public class GetTaskLogs : IHttpHandler
    {
        Controller.ScheduleDataSVCs Schedule = new Controller.ScheduleDataSVCs();
        public void ProcessRequest(HttpContext context)
        {
            var TaskId = context.Request.QueryString["TaskId"];
            
            if (TaskId != "")
            {
                var res = Schedule.GetScheduleTaskLogList(Convert.ToString(TaskId));
                context.Response.Write(new JavaScriptSerializer().Serialize(res));
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