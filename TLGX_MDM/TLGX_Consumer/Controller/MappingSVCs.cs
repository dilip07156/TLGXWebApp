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
        //public List<DC_Accomodation_ProductMapping> GetProductMappingMasterData(int PageNo, int PageSize, Guid Accommodation_Id, string Status)
        //{
        //    object result = null;
        //    ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_GetProductMappingMasterURI"], PageNo, PageSize, Accommodation_Id, Status), typeof(List<DC_Accomodation_ProductMapping>), out result);
        //    return result as List<DC_Accomodation_ProductMapping>;
        //}
        public List<DC_Accomodation_ProductMapping> GetProductMappingMasterData(MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_GetProductMappingMasterURI"], RQParams, typeof(MDMSVC.DC_Mapping_ProductSupplier_Search_RQ), typeof(List<MDMSVC.DC_Accomodation_ProductMapping>), out result);
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
        public List<DC_MappingStats> GetMappingStatistics(string Supplier_Id, string PriorityId, string ProductCategory, string isMDM)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Staitistics_Get"], Supplier_Id, PriorityId, ProductCategory, isMDM), typeof(List<DC_MappingStats>), out result);
            return result as List<DC_MappingStats>;
        }
        #endregion

        #region roll_off_reports
        public List<DC_RollOffReportRule> getStatisticforRuleReport(MDMSVC.DC_RollOFParams parm)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_RuleReport"], parm, typeof(MDMSVC.DC_RollOFParams), typeof(List<MDMSVC.DC_RollOffReportRule>), out result);
            return result as List<DC_RollOffReportRule>;
        }
        public List<DC_RollOffReportStatus> getStatisticforStatusReport(MDMSVC.DC_RollOFParams parm)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_StatusReport"], parm, typeof(MDMSVC.DC_RollOFParams), typeof(List<MDMSVC.DC_RollOffReportStatus>), out result);
            return result as List<DC_RollOffReportStatus>;
        }
        public List<DC_RollOffReportUpdate> getStatisticforUpdateReport(MDMSVC.DC_RollOFParams parm)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_UpdateReport"], parm, typeof(MDMSVC.DC_RollOFParams), typeof(List<MDMSVC.DC_RollOffReportUpdate>), out result);
            return result as List<DC_RollOffReportUpdate>;
        }
        #endregion

        #region rdlc reports
        public List<DC_UnmappedCountryReport> GetsupplierwiseUnmappedCountryReport(string Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["countryUnmapped_report"], Supplier_Id), typeof(List<DC_UnmappedCountryReport>), out result);
            return result as List<DC_UnmappedCountryReport>;
        }
        public List<DC_UnmappedCityReport> GetsupplierwiseUnmappedCityReport(string Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["cityUnmapped_report"], Supplier_Id), typeof(List<DC_UnmappedCityReport>), out result);
            return result as List<DC_UnmappedCityReport>;
        }
        public List<DC_unmappedProductReport> GetsupplierwiseUnmappedProductReport(string Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["productUnmapped_report"], Supplier_Id), typeof(List<DC_unmappedProductReport>), out result);
            return result as List<DC_unmappedProductReport>;
        }
        public List<DC_unmappedActivityReport> GetsupplierwiseUnmappedActivityReport(string Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["activityUnmapped_report"], Supplier_Id), typeof(List<DC_unmappedActivityReport>), out result);
            return result as List<DC_unmappedActivityReport>;
        }
        public List<DC_supplierwisesummaryReport> GetsupplierwiseSummaryReport()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["summary_report"]), typeof(List<DC_supplierwisesummaryReport>), out result);
            return result as List<DC_supplierwisesummaryReport>;
        }
        public List<DC_supplierwiseunmappedsummaryReport> GetsupplierwiseUnmappedSummaryReport(string Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["unmapped_summary_report"], Supplier_Id), typeof(List<DC_supplierwiseunmappedsummaryReport>), out result);
            return result as List<DC_supplierwiseunmappedsummaryReport>;
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

        public List<DC_MasterAttributeValueMappingRS> Mapping_AttributeValue_Get(DC_MasterAttributeValueMapping_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_AttributeValue_Get"], RQ, RQ.GetType(), typeof(List<DC_MasterAttributeValueMappingRS>), out result);
            return result as List<DC_MasterAttributeValueMappingRS>;
        }

        public DC_MasterAttributeMappingAdd_RS Mapping_Attribute_Add(DC_MasterAttributeMapping param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Attribute_Add"], param, param.GetType(), typeof(DC_MasterAttributeMappingAdd_RS), out result);
            return result as DC_MasterAttributeMappingAdd_RS;
        }

        public DC_Message Mapping_Attribute_Update(DC_MasterAttributeMapping param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Attribute_Update"], param, param.GetType(), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public DC_Message Mapping_AttributeValue_Update(List<DC_MasterAttributeValueMapping> param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_AttributeValue_Update"], param, param.GetType(), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message Mapping_AttributeValue_Delete(DC_SupplierAttributeValues_RQ param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_AttributeValue_Delete"], param, param.GetType(), typeof(DC_Message), out result);
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
        public List<string> GetStaticDataMappingAttributeValuesForFilter(MDMSVC.DC_SupplierImportAttributeValues_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UploadStaticDataValueFilter_Get"], RQ, typeof(MDMSVC.DC_SupplierImportAttributeValues_RQ), typeof(List<string>), out result);
            return result as List<string>;
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

        #region SupplierDataImportFileDetails

        public DC_Message UpdateSupplierImportFileDeails(MDMSVC.DC_SupplierImportFileDetails RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UpdateSupplierImportFileDetails_Update"], RQ, typeof(MDMSVC.DC_SupplierImportFileDetails), typeof(DC_Message), out result);
            return result as DC_Message;
        }


        public DC_Message UpdateSupplierImportFileDetailsFromNewToUploaded(MDMSVC.DC_SupplierImportFileDetails RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UpdateSupplierImportFileDetails_UpdateUploaded"], RQ, typeof(MDMSVC.DC_SupplierImportFileDetails), typeof(DC_Message), out result);
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
        public List<DC_Acitivity_SupplierProductMapping> GetActivitySupplierProductMappingSearchForMapping(DC_Acitivity_SupplierProductMapping_Search_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_Activity_SearchForMapping"], RQ, typeof(DC_Acitivity_SupplierProductMapping_Search_RQ), typeof(List<DC_Acitivity_SupplierProductMapping>), out result);
            return result as List<DC_Acitivity_SupplierProductMapping>;
        }
        public bool IsMappedWithSupplier(string masterActivityID, string supplierID)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Mapping_Activity_IsMappedWithSupplier"], masterActivityID, supplierID), typeof(bool), out result);
            return (bool)result;
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
        public DC_Message AddUpdateKeyword(DC_Keyword param)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Keyword_AddUpdate"], param, typeof(DC_Keyword), typeof(DC_Message), out result);
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

        public DC_Message KeywordReRun(string Entity, String Table, Guid Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Keyword_ReRun"], Entity, Table, Supplier_Id), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Process Or Test Uploaded Files
        public DC_Message StaticFileUploadProcessFile(DC_SupplierImportFileDetails obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Process_Uploaded_Files"], obj, typeof(MDMSVC.DC_SupplierImportFileDetails), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region RoomType Mapping
        public List<DC_Accommodation_SupplierRoomTypeMap_SearchRS> GetAccomodationSupplierRoomTypeMapping_Search(MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_GetAccoSupplierRoomType"], RQParams, typeof(MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRQ), typeof(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS>), out result);
            return result as List<DC_Accommodation_SupplierRoomTypeMap_SearchRS>;
        }
        public DC_Message AccomodationSupplierRoomTypeMapping_UpdateMap(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update> RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["AccomodationSupplierRoomTypeMapping_UpdateMap"], RQParams, typeof(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message AccomodationSupplierRoomTypeMapping_TTFUALL(List<MDMSVC.DC_SupplierRoomType_TTFU_RQ> RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["AccomodationSupplierRoomTypeMapping_UpdateTTFU"], RQParams, typeof(List<MDMSVC.DC_SupplierRoomType_TTFU_RQ>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public DC_Message UpdateAccomodationSupplierRoomTypeMapping(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMapping_Values> RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UpdateAccomodationSupplierRoomTypeMapping"], RQParams, typeof(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMapping_Values>), typeof(DC_Message), out result);
            return result as DC_Message;
        }

        public DC_Message UpdateAccomodationSupplierRoomTypeMappingTrainingFlag(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update> RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UpdateAccomodationSupplierRoomTypeMappingTrainingFlag"], RQParams, typeof(List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update>), typeof(DC_Message), out result);
            return result as DC_Message;
        }


        public List<MDMSVC.DC_SupplierRoomTypeAttributes> GetAttributeForAccomodationSupplierRoomTypeMapping(string RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["GetAttributeForAccomodationSupplierRoomTypeMapping"], RQParams), typeof(List<MDMSVC.DC_SupplierRoomTypeAttributes>), out result);
            return result as List<MDMSVC.DC_SupplierRoomTypeAttributes>;
        }

        #endregion
        #region hotel report
        public List<DC_newHotelsReport> getNewHotelsAddedReport(MDMSVC.DC_RollOFParams parm)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_NewHotelsReport"], parm, typeof(MDMSVC.DC_RollOFParams), typeof(List<MDMSVC.DC_newHotelsReport>), out result);
            return result as List<DC_newHotelsReport>;
        }

        //GAURAV-TMAP-645
        public List<DC_SupplierAccoMappingExportDataReport> AccomodationMappingReport(MDMSVC.DC_SupplerVSupplier_Report_RQ dC_SupplerVSupplier_Report_RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["AccomodationMapping_GetSupplierVSupplierReport"], dC_SupplerVSupplier_Report_RQ, typeof(MDMSVC.DC_SupplerVSupplier_Report_RQ), typeof(List<MDMSVC.DC_SupplierAccoMappingExportDataReport>), out result);
            return result as List<DC_SupplierAccoMappingExportDataReport>;
        }
        #endregion
        #region velocity dashboard
        public List<DC_VelocityMappingStats> GetVelocityDashboard(MDMSVC.DC_VelocityReport parm)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_velocityDashboard"], parm, typeof(MDMSVC.DC_VelocityReport), typeof(List<DC_VelocityMappingStats>), out result);
            return result as List<DC_VelocityMappingStats>;
        }
        #endregion

        #region Get SupplierData ForExport
        public List<DC_SupplierExportDataReport> GetSupplierDataForExport(string AccoPriority, Guid Supplier_Id, bool isMDM, string SuppPriority)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Get_SupplierDataExport"], AccoPriority, Supplier_Id.ToString(), isMDM.ToString(), SuppPriority), typeof(List<DC_SupplierExportDataReport>), out result);
            return result as List<DC_SupplierExportDataReport>;
        }
        #endregion
        #region File Progress Dashboard
        public DC_FileProgressDashboard getFileProgressDashBoardData(string fileid)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Get_FileProgress_Dashboard"], fileid), typeof(DC_FileProgressDashboard), out result);
            return result as DC_FileProgressDashboard;
        }

        #endregion


        #region fileConfig

        #endregion
        #region pentaho
        public List<DC_PentahoApiCallLogDetails> Pentaho_SupplierApiCall_List(DC_PentahoApiCallLogDetails_RQ _obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Supplier_api_search"], _obj, typeof(DC_PentahoApiCallLogDetails_RQ), typeof(List<DC_PentahoApiCallLogDetails>), out result);
            return result as List<DC_PentahoApiCallLogDetails>;

        }
        public DC_Message Pentaho_SupplierApi_Call(Guid ApiLocationId, string CalledBy)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["supplier_newapi_add"], ApiLocationId, CalledBy), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        public List<DC_Supplier_ApiLocation> Pentaho_SupplierApiLocationId_Get(Guid SupplierId, Guid EntityId)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["get_supplier_apiLocationid"], SupplierId, EntityId), typeof(List<DC_Supplier_ApiLocation>), out result);
            return result as List<DC_Supplier_ApiLocation>;
        }
        public DC_PentahoTransStatus_TransStatus Pentaho_SupplierApiCall_ViewDetails(string PentahoCall_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["supplier_api_viewdetails"], PentahoCall_Id), typeof(DC_PentahoTransStatus_TransStatus), out result);
            return result as DC_PentahoTransStatus_TransStatus;
        }

        #endregion

        #region CIty Mapping
        public List<DC_HotelListByCityCode> GetHotelListByCityCode(MDMSVC.DC_HotelListByCityCode_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Mapping_GetHotelListByCityCode"], RQParams, typeof(MDMSVC.DC_HotelListByCityCode_RQ), typeof(List<MDMSVC.DC_HotelListByCityCode>), out result);
            return result as List<DC_HotelListByCityCode>;
        }
        #endregion

        public List<DC_EzeegoHotelVsSupplierHotelMappingReport> EzeegoHotelVsSupplierHotelMappingReport(MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_EzeegoHotelVsSupplierHotelMapping"], RQ, typeof(List<MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport_RQ>), typeof(List<MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport>), out result);
            return result as List<MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport>;
        }

        #region NewDashBoardReport
        public List<DC_NewDashBoardReportCountry_RS> GetNewDashboardReport_CountryWise(DC_NewDashBoardReport_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(string.Format(ConfigurationManager.AppSettings["Country_DashBoardReport"]), RQ, typeof(List<DC_NewDashBoardReport_RQ>), typeof(List<DC_NewDashBoardReportCountry_RS>), out result);
            return result as List<DC_NewDashBoardReportCountry_RS>;
        }

        public List<DC_NewDashBoardReportCountry_RS> GetHotelMappingReport_CityWise(DC_NewDashBoardReport_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["City_DashBoardReport"], RQ, typeof(List<DC_NewDashBoardReport_RQ>), typeof(List<MDMSVC.DC_NewDashBoardReportCountry_RS>), out result);
            return result as List<DC_NewDashBoardReportCountry_RS>;
        }
        #endregion NewDashBoardReport

        #region HotelMappingReport
        public List<DC_HotelMappingReport_RS> HotelMappingReport(MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_HotelMappingReport"], RQ, typeof(MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport_RQ), typeof(List<MDMSVC.DC_HotelMappingReport_RS>), out result);
            return result as List<MDMSVC.DC_HotelMappingReport_RS>;
        }

        #endregion

        //GAURAV_TMAP_746
        #region File Processing Check
        public DC_Message FileProcessingCheckInSupplierImportFileDetails(string SupplierId)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SupplierFileProcess_Check"], SupplierId, typeof(MDMSVC.DC_SupplierImportFileDetails), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Supplier RoomType Reset 
        //GAURAV_TMAP_746
        public DC_Message AccomodationSupplierRoomTypeMapping_Reset(List<MDMSVC.DC_SupplierRoomType_TTFU_RQ> RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["AccomodationSupplierRoomTypeMapping_Reset"], RQParams, typeof(List<MDMSVC.DC_SupplierRoomType_TTFU_RQ>), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion
    }
}