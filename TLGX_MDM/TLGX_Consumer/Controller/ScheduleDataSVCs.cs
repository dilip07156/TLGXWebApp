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
        public List<MDMSVC.DC_Supplier_Schedule> GetSchedule(MDMSVC.DC_Supplier_Schedule_RQ RQParams)
        {
            object result = null;
            //ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Supplier_GetSchedule"], RQParams), typeof(List<MDMSVC.DC_Supplier_Schedule>), out result);
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_GetSchedule"], RQParams, typeof(MDMSVC.DC_Supplier_Schedule_RQ), typeof(List<MDMSVC.DC_Supplier_Schedule>), out result);

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

        public List<Supplier_Task_Notifications> GetScheduleNotificationTaskLog(MDMSVC.DC_SupplierScheduledTaskRQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_ScheduleTask_notification"],RQParams, typeof(MDMSVC.DC_SupplierScheduledTaskRQ), typeof(List<MDMSVC.Supplier_Task_Notifications>), out result);
            return result as List<Supplier_Task_Notifications>;
        }

        public DC_Message UpdateTaskLog(MDMSVC.DC_SupplierScheduledTaskRQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_UpdateTaskLog"], RQParams, typeof(MDMSVC.DC_SupplierScheduledTaskRQ), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public List<MDMSVC.Supplier_Task_Logs> GetScheduleTaskLogList(string Supplier_ID)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Supplier_Scheduler_GetTaskLogs"], Supplier_ID), typeof(List<MDMSVC.Supplier_Task_Logs>), out result);
            return result as List<Supplier_Task_Logs>;
        }

        #endregion
        #region supplier schedule
        public List<MDMSVC.DC_Supplier_Schedule> GetSupplierSchedule(MDMSVC.DC_Supplier_Schedule_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_GetSchedule"], RQParams, typeof(MDMSVC.DC_Supplier_Schedule_RQ), typeof(List<MDMSVC.DC_Supplier_Schedule>), out result);

            return result as List<DC_Supplier_Schedule>;
        }

        public bool SoftDeleteDetails(MDMSVC.DC_Supplier_Schedule_RQ RQParams)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_Schedular_SoftDelete"], RQParams, typeof(MDMSVC.DC_Supplier_Schedule_RQ), typeof(bool), out result);
            return (bool)result;
        }

        public bool CheckSupplierScheduleData(MDMSVC.DC_Supplier_Schedule_RQ RQParams)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Check_Supplier_Schedular_data"], RQParams, typeof(List<MDMSVC.DC_Supplier_Schedule_RQ>), typeof(bool), out result);
            return (bool)result;
        }
        #endregion
    }
}