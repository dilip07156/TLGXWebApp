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
    public class MappingSVCs
    {
        #region Country Mapping
        //public List<DC_CountryMapping> GetCountryMappingData(int PageNo, int PageSize, Guid Supplier_Id, Guid Country_Id, string sts, string SortBy)
        //{
        //    object result = null;
        //    ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_GetCountryMappingURI"], PageNo, PageSize, Supplier_Id, Country_Id, sts, SortBy), typeof(List<DC_CountryMapping>), out result);
        //    return result as List<DC_CountryMapping>;
        //}

        public List<DC_CountryMapping> GetCountryMappingData(MDMSVC.DC_CountryMappingRQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_GetCountryMappingURI"], RQ, typeof(MDMSVC.DC_CountryMappingRQ), typeof(List<DC_CountryMapping>), out result);
            return result as List<DC_CountryMapping>;
        }
        public bool UpdateCountryMappingDatat(List<MDMSVC.DC_CountryMapping> MappingData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_UpdateCountryMappingURI"], MappingData, typeof(List<MDMSVC.DC_CountryMapping>), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region CIty Mapping
        public List<DC_CityMapping> GetCityMappingData(MDMSVC.DC_CityMapping_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_GetCityMappingURI"], RQParams, typeof(MDMSVC.DC_CityMapping_RQ), typeof(List<MDMSVC.DC_CityMapping>), out result);
            return result as List<DC_CityMapping>;
        }
        public bool UpdateCityMappingDatat(List<MDMSVC.DC_CityMapping> MappingData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_UpdateCityMappingURI"], MappingData, typeof(List<MDMSVC.DC_CityMapping>), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelDetail(MDMSVC.DC_CityMapping HotelData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_UpdateCityMappingURI"], HotelData, typeof(MDMSVC.DC_CityMapping), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Product Mapping

        public List<DC_Accomodation_ProductMapping> GetProductMappingMasterDataById(Guid Accommodation_ProductMapping_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_GetProductMappingByIdURI"], Accommodation_ProductMapping_Id), typeof(List<DC_Accomodation_ProductMapping>), out result);
            return result as List<DC_Accomodation_ProductMapping>;
        }
        public List<DC_Accomodation_ProductMapping> GetProductMappingMasterData(int PageNo, int PageSize, Guid Accommodation_Id, string Status)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_GetProductMappingMasterURI"], PageNo, PageSize, Accommodation_Id, Status), typeof(List<DC_Accomodation_ProductMapping>), out result);
            return result as List<DC_Accomodation_ProductMapping>;
        }

        public List<DC_Accomodation_ProductMapping> GetProductMappingData(MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_GetProductMappingURI"], RQParams, typeof(MDMSVC.DC_Mapping_ProductSupplier_Search_RQ), typeof(List<MDMSVC.DC_Accomodation_ProductMapping>), out result);
            return result as List<DC_Accomodation_ProductMapping>;
        }
        public bool UpdateProductMappingData(List<MDMSVC.DC_Accomodation_ProductMapping> MappingData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_UpdateProductMappingURI"], MappingData, typeof(List<MDMSVC.DC_Accomodation_ProductMapping>), typeof(bool), out result);
            return (bool)result;
        }
        public bool ShiftAccommodationMappings(MDMSVC.DC_Mapping_ShiftMapping_RQ MappingData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_ShiftProductMappingURI"], MappingData, typeof(MDMSVC.DC_Mapping_ShiftMapping_RQ), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Mapping Statictics
        public List<DC_MappingStats> GetMappingStatistics(string Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Staitistics_Get"], Supplier_Id), typeof(List<DC_MappingStats>), out result);
            return result as List<DC_MappingStats>;
        }
        public List<DC_MappingStatsForSuppliers> GetMappingStatisticsForSuppliers()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Staitistics_Get_AllSuppliers"]), typeof(List<DC_MappingStatsForSuppliers>), out result);
            return result as List<DC_MappingStatsForSuppliers>;
        }
        #endregion
        #region roll_off_reports
        public List<DC_RollOffReportRule> getStatisticforRuleReport(MDMSVC.DC_RollOFParams parm)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_RuleReport"], parm, typeof(MDMSVC.DC_RollOFParams), typeof(List<MDMSVC.DC_RollOffReportRule>), out result);
            return result as List<DC_RollOffReportRule>;
        }
        #endregion

        #region Attribute Mapping
        public List<DC_MasterAttributeMapping_RS> Mapping_Attribute_Search(DC_MasterAttributeMapping_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Attribute_Search"], RQ, typeof(DC_MasterAttributeMapping_RQ), typeof(List<DC_MasterAttributeMapping_RS>), out result);
            return result as List<DC_MasterAttributeMapping_RS>;
        }

        public DC_MasterAttributeMapping Mapping_Attribute_Get(Guid MasterAttributeMapping_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Attribute_Get"], MasterAttributeMapping_Id.ToString()), typeof(DC_MasterAttributeMapping), out result);
            return result as DC_MasterAttributeMapping;
        }

        public List<DC_MasterAttributeValueMapping> Mapping_AttributeValue_Get(Guid MasterAttributeMapping_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_AttributeValue_Get"], MasterAttributeMapping_Id.ToString()), typeof(List<DC_MasterAttributeValueMapping>), out result);
            return result as List<DC_MasterAttributeValueMapping>;
        }

        public DC_Message Mapping_Attribute_Add(DC_MasterAttributeMapping param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Attribute_Add"], param, typeof(DC_MasterAttributeMapping), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public DC_Message Mapping_Attribute_Update(DC_MasterAttributeMapping param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Attribute_Update"], param, typeof(DC_MasterAttributeMapping), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public DC_Message Mapping_AttributeValue_Update(DC_MasterAttributeValueMapping param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_AttributeValue_Update"], param, typeof(DC_MasterAttributeValueMapping), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region "Upload Mapping Config"
        public List<DC_SupplierImportAttributes> GetStaticDataMappingAttributes(MDMSVC.DC_SupplierImportAttributes_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticData_Get"], RQ, typeof(MDMSVC.DC_SupplierImportAttributes_RQ), typeof(List<DC_SupplierImportAttributes>), out result);
            return result as List<DC_SupplierImportAttributes>;
        }
        public DC_Message AddStaticDataMappingAttribute(MDMSVC.DC_SupplierImportAttributes RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticData_Add"], RQ, typeof(MDMSVC.DC_SupplierImportAttributes), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message UpdateStaticDataMappingAttribute(List<MDMSVC.DC_SupplierImportAttributes> RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticData_Update"], RQ, typeof(List<MDMSVC.DC_SupplierImportAttributes>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message UpdateStaticDataMappingAttributeStatus(List<MDMSVC.DC_SupplierImportAttributes> RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticData_UpdateStatus"], RQ, typeof(List<MDMSVC.DC_SupplierImportAttributes>), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public List<DC_SupplierImportAttributeValues> GetStaticDataMappingAttributeValues(MDMSVC.DC_SupplierImportAttributeValues_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticDataValue_Get"], RQ, typeof(MDMSVC.DC_SupplierImportAttributeValues_RQ), typeof(List<DC_SupplierImportAttributeValues>), out result);
            return result as List<DC_SupplierImportAttributeValues>;
        }
        public DC_Message AddStaticDataMappingAttributeValue(MDMSVC.DC_SupplierImportAttributeValues RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticDataValue_Add"], RQ, typeof(MDMSVC.DC_SupplierImportAttributeValues), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message UpdateStaticDataMappingAttributeValue(List<MDMSVC.DC_SupplierImportAttributeValues> RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticDataValue_Update"], RQ, typeof(List<MDMSVC.DC_SupplierImportAttributeValues>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message UpdateStaticDataMappingAttributeValueStatus(List<MDMSVC.DC_SupplierImportAttributeValues> RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticDataValue_UpdateStatus"], RQ, typeof(List<MDMSVC.DC_SupplierImportAttributeValues>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Supplier Static File Details

        //Save
        public DC_Message SaveSupplierStaticFileDetails(MDMSVC.DC_SupplierImportFileDetails RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierFileDetails_Save"], RQ, typeof(MDMSVC.DC_SupplierImportFileDetails), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        //Get
        public List<MDMSVC.DC_SupplierImportFileDetails> GetSupplierStaticFileDetails(MDMSVC.DC_SupplierImportFileDetails_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierFileDetails_Get"], RQ, typeof(MDMSVC.DC_SupplierImportFileDetails_RQ), typeof(List<MDMSVC.DC_SupplierImportFileDetails>), out result);
            return result as List<MDMSVC.DC_SupplierImportFileDetails>;
        }
        //Update
        public DC_Message UpdateSupplierStaticFileDetails(MDMSVC.DC_SupplierImportFileDetails RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierFileDetails_Update"], RQ, typeof(MDMSVC.DC_SupplierImportFileDetails), typeof(DC_Message), out result);
            return result as DC_Message;
        }


        public List<MDMSVC.DC_SupplierImportFile_ErrorLog> GetStaticDataUploadErrorLog(MDMSVC.DC_SupplierImportFile_ErrorLog_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierFileDetails_GetErrorLog"], RQ, typeof(MDMSVC.DC_SupplierImportFile_ErrorLog_RQ), typeof(List<MDMSVC.DC_SupplierImportFile_ErrorLog>), out result);
            return result as List<MDMSVC.DC_SupplierImportFile_ErrorLog>;
        }
        #endregion

        #region Attribute Mapping
        public List<DC_Acitivity_SupplierProductMapping> GetActivitySupplierProductMappingSearch(DC_Acitivity_SupplierProductMapping_Search_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Activity_Search"], RQ, typeof(DC_Acitivity_SupplierProductMapping_Search_RQ), typeof(List<DC_Acitivity_SupplierProductMapping>), out result);
            return result as List<DC_Acitivity_SupplierProductMapping>;
        }
        public List<DC_Acitivity_SupplierProductMapping> GetActivitySupplierProductMapping(int PageNo, int PageSize, Guid Activity_Id, string Status)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Activity_Get_ByID"], PageNo, PageSize, Activity_Id, Status), typeof(List<DC_Acitivity_SupplierProductMapping>), out result);
            return result as List<DC_Acitivity_SupplierProductMapping>;
        }
        public DC_Acitivity_SupplierProductMapping GetActivitySupplierProductMappingById(Guid ActivitySupplierProductMappling_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Activity_Get"], ActivitySupplierProductMappling_Id.ToString()), typeof(DC_Acitivity_SupplierProductMapping), out result);
            return result as DC_Acitivity_SupplierProductMapping;
        }

        public DC_Message UpdateActivitySupplierProductMapping(List<DC_Acitivity_SupplierProductMapping> param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Activity_Update"], param, typeof(List<DC_Acitivity_SupplierProductMapping>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public List<DC_Acitivity_SupplierProductMappingForDDL> GetActivitySupplierProductMappingSearchForDDL(DC_Acitivity_SupplierProductMapping_Search_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Supplier_Activity_ForDDL"], RQ, typeof(DC_Acitivity_SupplierProductMapping_Search_RQ), typeof(List<DC_Acitivity_SupplierProductMappingForDDL>), out result);
            return result as List<DC_Acitivity_SupplierProductMappingForDDL>;
        }
        #endregion

        #region Keyword Mapping
        public DC_Message SaveKeyword(List<DC_Keyword> param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Keyword_Add"], param, typeof(List<DC_Keyword>), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public DC_Message AddUpdateKeyword(List<DC_Keyword> param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Keyword_AddUpdate"], param, typeof(List<DC_Keyword>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public List<DC_Keyword> SearchKeyword(DC_Keyword_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Keyword_Get"], RQ, typeof(DC_Keyword_RQ), typeof(List<DC_Keyword>), out result);
            return result as List<DC_Keyword>;
        }
        public List<DC_keyword_alias> SearchKeywordAlias(DC_Keyword_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["KeywordAlias_Get"], RQ, typeof(DC_Keyword_RQ), typeof(List<DC_keyword_alias>), out result);
            return result as List<DC_keyword_alias>;
        }


        public DC_Message AddUpdateKeywordAlias(List<DC_keyword_alias> param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["KeywordAlias_AddUpdate"], param, typeof(List<DC_keyword_alias>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion
    }
}