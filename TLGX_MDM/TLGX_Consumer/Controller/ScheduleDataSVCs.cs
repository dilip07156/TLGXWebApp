using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;
using TLGX_Consumer.Models;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.Controller
{
    public class ScheduleDataSVCs
    {
        #region Supplier
        public List<MDMSVC.DC_Supplier_Schedule> GetSchedule(string Supplier_ID)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Supplier_GetSchedule"], Supplier_ID), typeof(List<MDMSVC.DC_Supplier_Schedule>), out result);
            return result as List<DC_Supplier_Schedule>;
        }
        public DC_Message AddUpdateSchedule(MDMSVC.DC_Supplier_Schedule RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_AddUpdateSchedule"], RQParams, typeof(MDMSVC.DC_Supplier_Schedule), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public List<SupplierScheduledTask> GetScheduleTaskByRoll(MDMSVC.DC_SupplierScheduledTaskRQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_ScheduleTask"], RQParams, typeof(MDMSVC.DC_SupplierScheduledTaskRQ), typeof(List<MDMSVC.SupplierScheduledTask>), out result);
            return result as List<SupplierScheduledTask>;
        }

        #endregion


    }
}