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
        #endregion
        #region supplier schedule
        public List<MDMSVC.DC_Supplier_Schedule_RS> GetSupplierSchedule(MDMSVC.DC_Supplier_Schedule_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_Schedular_Get"], RQParams, typeof(MDMSVC.DC_Supplier_Schedule_RQ), typeof(List<MDMSVC.DC_Supplier_Schedule_RS>), out result);

            return result as List<DC_Supplier_Schedule_RS>;
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