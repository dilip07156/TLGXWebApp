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
    public class ActivitySVC
    {
        #region "Activity Search"
        public List<DC_ActivitySearch_RS> ActivitySearch(MDMSVC.DC_Activity_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_Search"], RQParams, typeof(MDMSVC.DC_Activity_Search_RQ), typeof(List<MDMSVC.DC_ActivitySearch_RS>), out result);
            return result as List<DC_ActivitySearch_RS>;
        }
        #endregion

        #region "Activity Add/Update"
        public DC_Message AddUpdateActivity(MDMSVC.DC_Activity RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdate"], RQParams, typeof(MDMSVC.DC_Activity), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion
    }
}