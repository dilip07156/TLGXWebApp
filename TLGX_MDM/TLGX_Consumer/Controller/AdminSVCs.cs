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
    public class AdminSVCs
    {
        public bool IsRoleAuthorizedForUrl(MDMSVC.DC_RoleAuthorizedForUrl RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_AuthURI"], RQ, typeof(MDMSVC.DC_RoleAuthorizedForUrl), typeof(bool), out result);
            return (bool)result;
        }

        public List<MDMSVC.DC_EntityType> GetEntityType()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(ConfigurationManager.AppSettings["Admin_GetEntityType"], typeof(List<MDMSVC.DC_EntityType>), out result);
            return result as List<DC_EntityType>;
        }
        public List<MDMSVC.DC_EntityDetails> GetEntity(MDMSVC.DC_EntityDetails ED)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_GetEntity"], ED, typeof(MDMSVC.DC_EntityDetails), typeof(List<MDMSVC.DC_EntityDetails>), out result);
            return result as List<DC_EntityDetails>;
        }

        public List<MDMSVC.DC_Roles> GetRoleWithEntity(int PageNumber, int PageSize)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Admin_Roles_Get_WithEntity"], PageNumber, PageSize), typeof(List<DC_Roles>), out result);
            return result as List<DC_Roles>;
        }
        public List<MDMSVC.DC_Roles> GetAllRoleByEntityType(MDMSVC.DC_Roles rol)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_Roles_ByEntityType"], rol, typeof(MDMSVC.DC_Roles), typeof(List<MDMSVC.DC_Roles>), out result);
            return result as List<DC_Roles>;
        }
        public List<MDMSVC.DC_Roles> GetAllRole(int PageNumber, int PageSize, string applicationid = "0")
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Admin_Roles_Get"], applicationid, PageNumber, PageSize), typeof(List<DC_Roles>), out result);
            return result as List<DC_Roles>;
        }

        public bool IsRoleExist(MDMSVC.DC_Roles rol)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_Roles_IsRoleExist"], rol, typeof(MDMSVC.DC_Roles), typeof(bool), out result);
            return (bool)result;
        }
        public bool AddUpdateRoleEntityType(MDMSVC.DC_Roles rol)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_Roles_AddEntityWithRole"], rol, typeof(MDMSVC.DC_Roles), typeof(bool), out result);
            return (bool)result;
        }
        public DC_Message AddUpdateUserEntity(MDMSVC.DC_UserEntity UE)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_UserEntity"], UE, typeof(MDMSVC.DC_UserEntity), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        public DC_Message UserSoftDelete(MDMSVC.DC_UserDetails UD)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_User_SoftDelete"], UD, typeof(MDMSVC.DC_UserDetails), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        public DC_UserEntity GetUserEntityDetails(DC_UserEntity UE)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_GetUserEntityDetails"], UE, typeof(MDMSVC.DC_UserEntity), typeof(MDMSVC.DC_UserEntity), out result);
            return result as DC_UserEntity;
        }
        public List<MDMSVC.DC_UserDetails> GetAllUsers(int PageNumber, int PageSize,string ApplicationId)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Admin_User_Get_All"], PageNumber, PageSize,ApplicationId), typeof(List<DC_UserDetails>), out result);
            return result as List<DC_UserDetails>;
        }
        public List<MDMSVC.DC_ApplicationMgmt> GetAllApplication(int PageNumber, int PageSize)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Admin_Application_Get"], PageNumber, PageSize), typeof(List<DC_ApplicationMgmt>), out result);
            return result as List<DC_ApplicationMgmt>;
        }

        public string GetApplicationName(string username)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Admin_Application_GetByUser"], username), typeof(string), out result);
            return result as string;
        }
        public MDMSVC.DC_Message AddUpdateApplication(MDMSVC.DC_ApplicationMgmt apmgmt)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Admin_Application_AddUpdate"], apmgmt, typeof(MDMSVC.DC_ApplicationMgmt), typeof(MDMSVC.DC_Message), out result);
            return result as MDMSVC.DC_Message;
        }

    }
}