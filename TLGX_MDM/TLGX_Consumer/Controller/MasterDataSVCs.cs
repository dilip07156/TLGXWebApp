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
    public class MasterDataSVCs
    {
        #region "Country Data"

        public List<string> GetCountryNameList(MDMSVC.DC_Country_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetCountryNameList"], RQParams, typeof(MDMSVC.DC_Country_Search_RQ), typeof(List<string>), out result);
            return result as List<string>;
        }

        public List<MDMSVC.DC_Master_Country> GetAllCountries()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetAllCountries"]), typeof(List<DC_Master_Country>), out result);
            return result as List<DC_Master_Country>;
        }

        public List<DC_Country> GetCountryMasterData(MDMSVC.DC_Country_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CountryGet"], RQParams, typeof(MDMSVC.DC_Country_Search_RQ), typeof(List<MDMSVC.DC_Country>), out result);
            return result as List<DC_Country>;
        }
        public DC_Message AddUpdateCountryMaster(MDMSVC.DC_Country RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_AddUpdateCountryMaster"], RQ, typeof(MDMSVC.DC_Country), typeof(DC_Message), out result);
            return (DC_Message)result;
        }
        public bool UpdateCountryMasterData(MDMSVC.DC_Country RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CountryUpdate"], RQ, typeof(MDMSVC.DC_Country), typeof(bool), out result);
            return (bool)result;
        }
        public bool AddCountryMasterData(MDMSVC.DC_Country RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CountryAdd"], RQ, typeof(MDMSVC.DC_Country), typeof(bool), out result);
            return (bool)result;
        }
        #endregion


        #region "City Data"

        public List<string> GetCityNameList(MDMSVC.DC_City_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetCityNameList"], RQParams, typeof(MDMSVC.DC_City_Search_RQ), typeof(List<string>), out result);
            return result as List<string>;
        }

        public List<DC_City> GetCityMasterData(MDMSVC.DC_City_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CityGet"], RQParams, typeof(MDMSVC.DC_City_Search_RQ), typeof(List<MDMSVC.DC_City>), out result);
            return result as List<DC_City>;
        }

        public List<City_AlphaPage> GetCityAlphaPaging(MDMSVC.DC_City_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CityGetAlphaPaging"], RQParams, typeof(MDMSVC.DC_City_Search_RQ), typeof(List<MDMSVC.City_AlphaPage>), out result);
            return result as List<City_AlphaPage>;
        }

        public MDMSVC.DC_Message UpdateCityMasterData(MDMSVC.DC_City RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CityUpdate"], RQ, typeof(MDMSVC.DC_City), typeof(MDMSVC.DC_Message), out result);
            return result as MDMSVC.DC_Message;
        }
        public MDMSVC.DC_Message AddCityMasterData(MDMSVC.DC_City RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_CityAdd"], RQ, typeof(MDMSVC.DC_City), typeof(MDMSVC.DC_Message), out result);
            return result as MDMSVC.DC_Message;
        }
        #endregion

        #region "State Data"
        //public List<MDMSVC.DC_Master_State> GetAllStates()
        //{

        //}
        //public List<MDMSVC.DC_Master_State> GetStatesByCountry(Guid Country_Id)
        //{

        //}
        public List<MDMSVC.DC_Master_State> GetStateMasterData(MDMSVC.DC_State_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_StateGet"], RQParams, typeof(MDMSVC.DC_State_Search_RQ), typeof(List<MDMSVC.DC_Master_State>), out result);
            return result as List<DC_Master_State>;
        }
        public List<MDMSVC.DC_Master_State> GetStatesMaster(MDMSVC.DC_State_Search_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_StatesGet"], RQ, typeof(MDMSVC.DC_State_Search_RQ), typeof(List<MDMSVC.DC_Master_State>), out result);
            return result as List<DC_Master_State>;
        }
        public List<MDMSVC.State_AlphaPage> GetStatesAlphaPaging(MDMSVC.DC_State_Search_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_StatesGetAlphaPaging"], RQ, typeof(MDMSVC.DC_State_Search_RQ), typeof(List<MDMSVC.State_AlphaPage>), out result);
            return result as List<State_AlphaPage>;
        }
        public MDMSVC.DC_Message AddStatesMaster(MDMSVC.DC_Master_State param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_StatesAdd"], param, typeof(MDMSVC.DC_Master_State), typeof(MDMSVC.DC_Message), out result);
            return result as MDMSVC.DC_Message;

        }
        public MDMSVC.DC_Message UpdateStatesMaster(MDMSVC.DC_Master_State param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_StatesUpdate"], param, typeof(MDMSVC.DC_Master_State), typeof(MDMSVC.DC_Message), out result);
            return (MDMSVC.DC_Message)result;

        }
        public List<DC_Master_State> GetStatesByCountry(string countryid)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_StatesGetByCountry"], countryid), typeof(List<DC_Master_State>), out result);
            return result as List<DC_Master_State>;
        }
        #endregion

        #region Port
        public List<MDMSVC.DC_PortMaster> PortMasterSeach(MDMSVC.DC_PortMaster_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_PortSearch"], RQParams, typeof(MDMSVC.DC_PortMaster_RQ), typeof(List<MDMSVC.DC_PortMaster>), out result);
            return result as List<MDMSVC.DC_PortMaster>;
        }
        public MDMSVC.DC_PortMaster GetPort(string pageSize, string pageNo, string Port_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_StatesGetByCountry"], pageSize, pageNo, Port_Id), typeof(DC_PortMaster), out result);
            return result as DC_PortMaster;
        }
        public MDMSVC.DC_Message UpdatePortMaster(MDMSVC.DC_PortMaster RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_PortUpdate"], RQParams, typeof(MDMSVC.DC_PortMaster), typeof(MDMSVC.DC_Message), out result);
            return result as DC_Message;
        }
        public MDMSVC.DC_Message AddPortMaster(MDMSVC.DC_PortMaster RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_PortAdd"], RQParams, typeof(MDMSVC.DC_PortMaster), typeof(MDMSVC.DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Master Attribute
        public List<MDMSVC.DC_MasterAttribute> GetAllAttributeAndValuesByFOR(MDMSVC.DC_MasterAttribute RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_AttributeGetFor"], RQParams, typeof(MDMSVC.DC_MasterAttribute), typeof(List<MDMSVC.DC_MasterAttribute>), out result);
            return result as List<DC_MasterAttribute>;
        }
        public List<MDMSVC.DC_MasterAttribute> GetAllAttributeAndValuesByParentAttributeValue(MDMSVC.DC_MasterAttribute RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_AttributeByParentAttributeValue"], RQParams, typeof(MDMSVC.DC_MasterAttribute), typeof(List<MDMSVC.DC_MasterAttribute>), out result);
            return result as List<DC_MasterAttribute>;
        }
        public List<MDMSVC.DC_MasterAttribute> GetAllAttributeAndValues(MDMSVC.DC_MasterAttribute RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_AttributeGet"], RQParams, typeof(MDMSVC.DC_MasterAttribute), typeof(List<MDMSVC.DC_MasterAttribute>), out result);
            return result as List<DC_MasterAttribute>;
        }


        public List<MDMSVC.DC_M_masterattribute> GetMasterAttributes(MDMSVC.DC_M_masterattribute _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_AttributeGetAll"], _obj, typeof(MDMSVC.DC_M_masterattribute), typeof(List<MDMSVC.DC_M_masterattribute>), out result);
            return result as List<DC_M_masterattribute>;
        }
        public List<MDMSVC.DC_M_masterparentattributes> GetParentAttributes(string MasterFor)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetParentAttributes"], MasterFor), typeof(List<DC_M_masterparentattributes>), out result);
            return result as List<DC_M_masterparentattributes>;
        }
        public List<MDMSVC.DC_M_masterparentattributes> GetAttributesForValues(string MasterAttribute_id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetAttributesForValues"], MasterAttribute_id), typeof(List<DC_M_masterparentattributes>), out result);
            return result as List<DC_M_masterparentattributes>;
        }
        public List<MDMSVC.DC_M_masterparentattributes> GetAttributesMasterForValues()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetAttributesMasterForValues"]), typeof(List<DC_M_masterparentattributes>), out result);
            return result as List<DC_M_masterparentattributes>;
        }

        public MDMSVC.DC_M_masterattribute GetAttributeDetails(string MasterAttribute_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetAttributeDetails"], MasterAttribute_Id), typeof(DC_M_masterattribute), out result);
            return result as DC_M_masterattribute;
        }
        public List<MDMSVC.DC_M_masterattributevalue> GetAttributeValues(string MasterAttribute_Id,string pagesize , string pageno)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetAttributeValuesById"], MasterAttribute_Id,pagesize,pageno), typeof(List<DC_M_masterattributevalue>), out result);
            return result as List<DC_M_masterattributevalue>;
        }

        public DC_M_masterattributelists GetListAttributeAndValuesByFOR(DC_MasterAttribute _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetListAttributeAndValuesByFOR"], _obj, typeof(MDMSVC.DC_MasterAttribute), typeof(DC_M_masterattributelists), out result);
            return result as DC_M_masterattributelists;
        }
        public DC_Message SaveMasterAttribute(MDMSVC.DC_M_masterattribute masterattrib)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_SaveMasterAttribute"], masterattrib, typeof(MDMSVC.DC_M_masterattribute), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message SaveAttributeValue(MDMSVC.DC_M_masterattributevalue RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_SaveAttributeValue"], RQParams, typeof(MDMSVC.DC_M_masterattributevalue), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        #endregion

        #region Supplier
        public List<MDMSVC.DC_Supplier> GetSupplier(MDMSVC.DC_Supplier_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_Get"], RQParams, typeof(MDMSVC.DC_Supplier_Search_RQ), typeof(List<MDMSVC.DC_Supplier>), out result);
            return result as List<DC_Supplier>;
        }
        public List<MDMSVC.DC_SupplierMarket> GetSupplierMarket(MDMSVC.DC_SupplierMarket RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierMarket_Get"], RQParams, typeof(MDMSVC.DC_SupplierMarket), typeof(List<MDMSVC.DC_SupplierMarket>), out result);
            return result as List<DC_SupplierMarket>;
        }
        public List<MDMSVC.DC_Supplier_ProductCategory> GetProductCategoryBySupplier(MDMSVC.DC_Supplier_ProductCategory RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["ProductCategoryBySupplier_Get"], RQParams, typeof(MDMSVC.DC_Supplier_ProductCategory), typeof(List<MDMSVC.DC_Supplier_ProductCategory>), out result);
            return result as List<DC_Supplier_ProductCategory>;
        }

        public DC_Message AddUpdateSupplier(MDMSVC.DC_Supplier RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_AddUpdate"], RQParams, typeof(MDMSVC.DC_Supplier), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message AddUpdateSupplierMarket(MDMSVC.DC_SupplierMarket RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierMarket_AppUpdate"], RQParams, typeof(MDMSVC.DC_SupplierMarket), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message AddUpdateSupplier_ProductCategory(MDMSVC.DC_Supplier_ProductCategory RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_ProductCategory_AddUpdate"], RQParams, typeof(MDMSVC.DC_Supplier_ProductCategory), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message SupplierMarketSoftDelete(MDMSVC.DC_SupplierMarket RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierMarket_SoftDelete"], RQParams, typeof(MDMSVC.DC_SupplierMarket), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message Supplier_ProductCategorySoftDelete(MDMSVC.DC_Supplier_ProductCategory RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_ProductCategory_SoftDelete"], RQParams, typeof(MDMSVC.DC_Supplier_ProductCategory), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Statuses
        public List<MDMSVC.DC_Statuses> GetAllStatuses()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Statuses_Get"]), typeof(List<DC_Statuses>), out result);
            return result as List<MDMSVC.DC_Statuses>;
        }

        #endregion

        #region "Activity Master"
        public List<MDMSVC.DC_Activity> GetActivityMaster(MDMSVC.DC_Activity_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetActivity"], RQParams, typeof(MDMSVC.DC_Activity_Search_RQ), typeof(List<MDMSVC.DC_Activity>), out result);
            return result as List<DC_Activity>;
        }
        public List<MDMSVC.DC_Activity> GetActivityMasterBySupplier(MDMSVC.DC_Activity_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetActivityBySupplier"], RQParams, typeof(MDMSVC.DC_Activity_Search_RQ), typeof(List<MDMSVC.DC_Activity>), out result);
            return result as List<DC_Activity>;
        }
        public List<MDMSVC.DC_Activity_Content> GetActivityContentMaster(MDMSVC.DC_Activity_Content RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetActivityContent"], RQParams, typeof(MDMSVC.DC_Activity_Content), typeof(List<MDMSVC.DC_Activity_Content>), out result);
            return result as List<DC_Activity_Content>;
        }
        public List<string> GetActivityNames(MDMSVC.DC_Activity_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetActivityNames"], RQParams, typeof(MDMSVC.DC_Activity_Search_RQ), typeof(List<string>), out result);
            return result as List<string>;
        }
        #endregion
        #region Common Funciton to Get Codes by Entity Type
        public string GetCodeById(string objName, string obj_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetCodeById"], objName, obj_Id), typeof(string), out result);
            return result as string;
        }
        public string GetRemarksForMapping(string from, Guid Mapping_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Masters_GetRemarksForMapping"], from, Mapping_Id.ToString()), typeof(string), out result);
            return result as string;
        }
        public DC_GenericMasterDetails_ByIDOrName GetDetailsByIdOrName(DC_GenericMasterDetails_ByIDOrName _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Masters_GetDetailsByIdOrName"], _obj, typeof(MDMSVC.DC_GenericMasterDetails_ByIDOrName), typeof(DC_GenericMasterDetails_ByIDOrName), out result);
            return result as DC_GenericMasterDetails_ByIDOrName;
        }
        #endregion

        #region To Fill Dropdown
        public List<MDMSVC.DC_Accomodation_DDL>GetProductByCity(DC_Accomodation_DDL _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetProductByCity"], _obj, typeof(MDMSVC.DC_Accomodation_DDL), typeof(List<MDMSVC.DC_Accomodation_DDL>), out result);
            return result as List<MDMSVC.DC_Accomodation_DDL>;
        }
        public List<MDMSVC.DC_State_Master_DDL> GetMasterStateData(string Country_id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetMasterStateData"], Country_id), typeof(List<DC_State_Master_DDL>), out result);
            return result as List<MDMSVC.DC_State_Master_DDL>;
        }
        public List<MDMSVC.DC_City_Master_DDL> GetMasterCityData(string Country_id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetMasterCityData"], Country_id), typeof(List<DC_City_Master_DDL>), out result);
            return result as List<MDMSVC.DC_City_Master_DDL>;
        }
        public List<MDMSVC.DC_CityAreaLocation> GetMasterCityAreaLocationDetail(string CityAreaLocation_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetMasterCityAreaLocationDetail"], CityAreaLocation_Id), typeof(List<DC_CityAreaLocation>), out result);
            return result as List<MDMSVC.DC_CityAreaLocation>;
        }
        public List<MDMSVC.DC_CityArea> GetMasterCityAreaDetail(string CityArea_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetMasterCityAreaDetail"], CityArea_Id), typeof(List<DC_CityArea>), out result);
            return result as List<MDMSVC.DC_CityArea>;
        }
        public List<MDMSVC.DC_Supplier_DDL> GetSupplierMasterData()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetSupplierMasterData"]), typeof(List<DC_Supplier_DDL>), out result);
            return result as List<MDMSVC.DC_Supplier_DDL>;
        }


        public List<DC_State_Master_DDL> GetStateByCity(string City_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetStateByCity"], City_Id), typeof(List<DC_State_Master_DDL>), out result);
            return result as List<MDMSVC.DC_State_Master_DDL>;

        }
        public List<DC_CountryMaster> GetCountryCodes(string Country_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetCountryCodes"], Country_Id), typeof(List<DC_CountryMaster>), out result);
            return result as List<MDMSVC.DC_CountryMaster>;

        }
        public DC_Supplier_DDL GetSupplierDataByMapping_Id(string objName, string Mapping_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_GetSupplierDataByMapping_Id"], objName, Mapping_Id), typeof(DC_Supplier_DDL), out result);
            return result as DC_Supplier_DDL;

        }
        public List<DC_CountryMaster> GetMasterCountryDataList()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetMasterCountryDataList"]), typeof(List<DC_CountryMaster>), out result);
            return result as List<MDMSVC.DC_CountryMaster>;

        }
        public List<DC_Activity_DDL>GetActivityByCountryCity(string countryname, string cityname)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_ForDropDown_GetActivityForDDL"], countryname, cityname), typeof(List<DC_Activity_DDL>), out result);
            return result as List<DC_Activity_DDL>;

        }
        #endregion

        #region City Area and Location
        public bool SaveCityAreaLocation(DC_CityAreaLocation _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Master_SaveCityAreaLocation"], _obj, typeof(MDMSVC.DC_CityAreaLocation), typeof(bool), out result);
            return (bool)result ;
        }
        public bool SaveCityArea(DC_CityArea _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Master_SaveCityArea"], _obj, typeof(MDMSVC.DC_CityArea), typeof(bool), out result);
            return (bool)result;
        }

        public List<MDMSVC.DC_CityAreaLocation> GetMasterCityAreaLocationData(string CityArea_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Master_GetMasterCityAreaLocationData"], CityArea_Id), typeof(List<DC_CityAreaLocation>), out result);
            return result as List<MDMSVC.DC_CityAreaLocation>;
        }
        public List<MDMSVC.DC_CityArea> GetMasterCityAreaData(string City_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Master_GetMasterCityAreaData"], City_Id), typeof(List<DC_CityArea>), out result);
            return result as List<MDMSVC.DC_CityArea>;
        }
        #endregion
    }
}