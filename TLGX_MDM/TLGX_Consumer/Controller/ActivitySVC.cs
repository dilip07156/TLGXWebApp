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
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateActivity"], RQParams, typeof(MDMSVC.DC_Activity), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region "Activity Product Info"
        public DC_Message AddUpdateProductInfo(MDMSVC.DC_Activity RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateProductInfo"], RQParams, typeof(MDMSVC.DC_Activity), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

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
        
        public int? GetLegacyProductId(Guid Activity_Flavour_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Activity_GetLegacyProductId"], Activity_Flavour_Id), typeof(int), out result);
            return result as int?;
        }
        #endregion

        #region "Activity Status"
        public List<DC_Activity_Status> GetActivityStatusDetails(Guid Activity_Flavour_Id, Guid DataKey_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Activity_StatusURI"], Activity_Flavour_Id, DataKey_Id), typeof(List<DC_Activity_Status>), out result);
            return result as List<DC_Activity_Status>;
        }

        public bool AddActivityStatus(MDMSVC.DC_Activity_Status AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddStatusURI"], AF, typeof(DC_Activity_Status), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateActivityStatus(MDMSVC.DC_Activity_Status AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_UpdateStatusURI"], AF, typeof(DC_Activity_Status), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region "Activity Policy"
        public List<DC_Activity_Policy> GetActivityPolicy(DC_Activity_Policy_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_GetActivityPolicy"], RQParams, typeof(MDMSVC.DC_Activity_Policy_RQ), typeof(List<DC_Activity_Policy>), out result);
            return result as List<DC_Activity_Policy>;
        }
        public DC_Message AddUpdateActivityPolicy(MDMSVC.DC_Activity_Policy RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddActivityPolicy"], RQParams, typeof(MDMSVC.DC_Activity_Policy), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region "Activity Inclusion"
        public List<DC_Activity_Inclusions> GetActivityInclusions(DC_Activity_Inclusions_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_GetActivityInclusion"], RQParams, typeof(DC_Activity_Inclusions_RQ), typeof(List<DC_Activity_Inclusions>), out result);
            return result as List<DC_Activity_Inclusions>;
        }
        public DC_Message AddUpdateActivityInclusions(MDMSVC.DC_Activity_Inclusions RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateActivityInclusions"], RQParams, typeof(MDMSVC.DC_Activity_Inclusions), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region "Activity Inclusion Details"
        public List<DC_Activity_InclusionsDetails> GetActivityInclusionDetails(DC_Activity_InclusionDetails_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_GetActivityInclusionDetails"], RQParams, typeof(DC_Activity_InclusionDetails_RQ), typeof(List<DC_Activity_Inclusions>), out result);
            return result as List<DC_Activity_InclusionsDetails>;
        }

        public DC_Message AddUpdateInclusionDetails(MDMSVC.DC_Activity_InclusionsDetails RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateActivityInclusionDetails"], RQParams, typeof(MDMSVC.DC_Activity_InclusionsDetails), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Activity Flavour
        public List<MDMSVC.DC_Activity_Flavour> GetActivityFlavour(MDMSVC.DC_Activity_Flavour_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["get_Activity_Flavour"], RQ, typeof(DC_Activity_Flavour_RQ), typeof(List<DC_Activity_Flavour>), out result);
            return result as  List <MDMSVC.DC_Activity_Flavour> ;

        }
        public DC_Message AddUpdateActivityFlavour(MDMSVC.DC_Activity_Flavour RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["addUpdate_Activity_Flavour"], RQ, typeof(MDMSVC.DC_Activity_Flavour), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        #endregion

        #region Activity Classification Attributes
        public List<MDMSVC.DC_Activity_ClassificationAttributes> GetActivityClasificationAttributes(MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["get_Activity_ClasificationAttributes"], RQ, typeof(DC_Activity_ClassificationAttributes_RQ), typeof(List<DC_Activity_ClassificationAttributes>), out result);
            return result as List<MDMSVC.DC_Activity_ClassificationAttributes>;

        }

        public bool AddActivityClassifiationAttributes(MDMSVC.DC_Activity_ClassificationAttributes AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Add_Activity_ClasificationAttributes"], AF, typeof(DC_Activity_ClassificationAttributes), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateActivityClassifiationAttributes(MDMSVC.DC_Activity_ClassificationAttributes AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Update_Activity_ClasificationAttributes"], AF, typeof(DC_Activity_ClassificationAttributes), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Activity Media
        public List<MDMSVC.DC_Activity_Media> GetActivityMedia(MDMSVC.DC_Activity_Media_Search_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["get_Activity_Media"], RQ, typeof(DC_Activity_Media_Search_RQ), typeof(List<DC_Activity_Media>), out result);
            return result as List<MDMSVC.DC_Activity_Media>;
        }
        
        public DC_Message AddUpdateActivityMedia(MDMSVC.DC_Activity_Media RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["addUpdate_Activity_Media"], RQ, typeof(MDMSVC.DC_Activity_Media), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        #endregion

        #region Activity Description
        public List<MDMSVC.DC_Activity_Descriptions> GetActivityDescription(MDMSVC.DC_Activity_Descriptions_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["get_Activity_Description"], RQ, typeof(DC_Activity_Descriptions_RQ), typeof(List<DC_Activity_Descriptions>), out result);
            return result as List<MDMSVC.DC_Activity_Descriptions>;
        }

        public DC_Message AddUpdateActivityDescription(MDMSVC.DC_Activity_Descriptions RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["addUpdate_Activity_Description"], RQ, typeof(MDMSVC.DC_Activity_Descriptions), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        #endregion

        #region Activity Review And Score
        public List<MDMSVC.DC_Activity_ReviewsAndScores> GetActReviewsAndScores(MDMSVC.DC_Activity_ReviewsAndScores_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_GetActivityReviewsAndScores"], RQ, typeof(DC_Activity_ReviewsAndScores_RQ), typeof(List<DC_Activity_ReviewsAndScores>), out result);
            return result as List<MDMSVC.DC_Activity_ReviewsAndScores>;
        }

        public DC_Message AddUpdateActReviewsNScores(MDMSVC.DC_Activity_ReviewsAndScores RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Activity_AddUpdateActivityReviewsNScores"], RQ, typeof(DC_Activity_ReviewsAndScores), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        #endregion

        #region Activity Prices
        public List<MDMSVC.DC_Activity_Prices> GetActivityPrices(MDMSVC.DC_Activity_Prices_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["get_Activity_Prices"], RQ, typeof(DC_Activity_Prices_RQ), typeof(List<DC_Activity_Prices>), out result);
            return result as List<MDMSVC.DC_Activity_Prices>;
        }

        public DC_Message AddUpdateActivityPrices(MDMSVC.DC_Activity_Prices RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["addUpdate_Activity_Prices"], RQ, typeof(DC_Activity_Prices), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        #endregion

    }
}