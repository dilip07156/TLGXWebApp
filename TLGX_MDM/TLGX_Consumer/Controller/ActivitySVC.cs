﻿using System;
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
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateActivity"], RQParams, typeof(MDMSVC.DC_Activity), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        public DC_Message AddUpdateProductInfo(MDMSVC.DC_Activity RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateProductInfo"], RQParams, typeof(MDMSVC.DC_Activity), typeof(DC_Message), out result);
            return result as DC_Message;
        }


        #region Activity Contacts
        public List<DC_Activity_Contact> GetActivityContactDetails(Guid Activity_Id, Guid DataKey_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Activity_ContactsURI"], Activity_Id, DataKey_Id), typeof(List<DC_Activity_Contact>), out result);
            return result as List<DC_Activity_Contact>;
        }
        public bool AddActivityContactsDetails(MDMSVC.DC_Activity_Contact AC)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddContactsURI"], AC, typeof(DC_Activity_Contact), typeof(bool), out result);
            return (bool)result;
        }
        public bool UpdateActivityContactDetails(MDMSVC.DC_Activity_Contact AC)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_UpdateContactsURI"], AC, typeof(DC_Activity_Contact), typeof(bool), out result);
            return (bool)result;
        }
        
        public string GetLegacyProductId(Guid Activity_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Activity_GetLegacyProductId"], Activity_Id), typeof(string), out result);
            return result as string;
        }
        #endregion
    }
}