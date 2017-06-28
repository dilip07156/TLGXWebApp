using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TLGX_Consumer.Models;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.Models
{
    public class MasterDataDAL : IDisposable
    {
        public void Dispose()
        {

        }

        // used to control what data is retrieved from Supplier Country and City Mapping
        public enum SupplierDataMode
        {
            AllSupplierAllCountry,
            AllSupplierSingleCountry,
            SingleSupplierAllCountry,
            SingleSupplierSingleCountry
        }

        // *********************************************************
        // classes organised into hierarchical order for neatness
        // *********************************************************


        //Need replacement
        public string GetCodeById(string objName, Guid obj_Id)
        {

            MasterDataSVCs _obj = new MasterDataSVCs();
            return _obj.GetCodeById(objName, obj_Id.ToString());
        }
        
        public string GetCodeById(MDMSVC.EntityType type, Guid obj_Id)
        {

            MasterDataSVCs _obj = new MasterDataSVCs();
            string Code = string.Empty;
            MDMSVC.DC_GenericMasterDetails_ByIDOrName _objClass = _obj.GetDetailsByIdOrName(new MDMSVC.DC_GenericMasterDetails_ByIDOrName()
            {
                ObjName = type,
                WhatFor = MDMSVC.DetailsWhatFor.CodeById,
                ID = obj_Id
            });
            if (_objClass != null)
                Code = _objClass.Code;
            return Code;
        }
        public string GetRemarksForMapping(string from, Guid Mapping_Id)
        {
            MasterDataSVCs _obj = new MasterDataSVCs();
            return _obj.GetRemarksForMapping(from, Mapping_Id);
        }
        //

        // gets Master City data for a particular Country Id
        
        public DataTable GetSupplierCountryMapping(SupplierDataMode DataMode, Guid? Supplier_Id, Guid Country_Id)
        {
            DataTable dtRet = new DataTable();
            // need a nice way of handling this mode, talk to rubesh you could probably do it with nullable guid check
            try
            {
                using (TLGX_MAPPEREntities1 myEntity = new TLGX_MAPPEREntities1())
                {
                    var MasterData = new MasterDataContract();
                    if (DataMode == SupplierDataMode.AllSupplierSingleCountry)

                    {
                        var supplierCountryMapping = (from ct in myEntity.m_CountryMapping
                                                      where ct.Country_Id == Country_Id
                                                      orderby ct.SupplierName ascending
                                                      select new CountryMappingE
                                                      {
                                                          CountryMapping_ID = ct.CountryMapping_Id,
                                                          CountryCode = ct.CountryCode,
                                                          CountryName = ct.CountryName,
                                                          Edit_Date = ct.Edit_Date,
                                                          Country_ID = ct.Country_Id,
                                                          Create_Date = ct.Create_Date,
                                                          Create_User = ct.Create_User,
                                                          Edit_User = ct.Edit_User,
                                                          Status = ct.Status,
                                                          Supplier_ID = ct.Supplier_Id ?? Guid.Empty,
                                                          Supplier_Name = ct.SupplierName

                                                      }).ToList();
                        MasterData.CountryMappingL = supplierCountryMapping;
                        dtRet = ConversionClass.CreateDataTable(supplierCountryMapping);

                    }
                    else if (DataMode == SupplierDataMode.SingleSupplierSingleCountry)
                    {

                        var supplierCountryMapping = (from ct in myEntity.m_CountryMapping
                                                      where
                                                          ct.Country_Id == Country_Id
                                                          && ct.Supplier_Id == Supplier_Id
                                                      orderby ct.CountryName ascending

                                                      select new CountryMappingE
                                                      {
                                                          CountryMapping_ID = ct.CountryMapping_Id,
                                                          CountryCode = ct.CountryCode,
                                                          CountryName = ct.CountryName,
                                                          Edit_Date = ct.Edit_Date,
                                                          Country_ID = ct.Country_Id,
                                                          Create_Date = ct.Create_Date,
                                                          Create_User = ct.Create_User,
                                                          Edit_User = ct.Edit_User,
                                                          Status = ct.Status,
                                                          Supplier_ID = ct.Supplier_Id ?? Guid.Empty,
                                                          Supplier_Name = ct.SupplierName
                                                      }).ToList();
                        MasterData.CountryMappingL = supplierCountryMapping;
                        dtRet = ConversionClass.CreateDataTable(supplierCountryMapping);

                    }

                    else if (DataMode == SupplierDataMode.SingleSupplierAllCountry)
                    {

                        var supplierCountryMapping = (from ct in myEntity.m_CountryMapping
                                                      where
                                                          ct.Supplier_Id == Supplier_Id
                                                      orderby ct.CountryName ascending

                                                      select new CountryMappingE
                                                      {
                                                          CountryMapping_ID = ct.CountryMapping_Id,
                                                          CountryCode = ct.CountryCode,
                                                          CountryName = ct.CountryName,
                                                          Edit_Date = ct.Edit_Date,
                                                          Country_ID = ct.Country_Id,
                                                          Create_Date = ct.Create_Date,
                                                          Create_User = ct.Create_User,
                                                          Edit_User = ct.Edit_User,
                                                          Status = ct.Status,
                                                          Supplier_ID = ct.Supplier_Id ?? Guid.Empty,
                                                          Supplier_Name = ct.SupplierName
                                                      }).ToList();
                        MasterData.CountryMappingL = supplierCountryMapping;
                        dtRet = ConversionClass.CreateDataTable(supplierCountryMapping);

                    }
                    else if (DataMode == SupplierDataMode.AllSupplierAllCountry)
                    {

                        var supplierCountryMapping = (from ct in myEntity.m_CountryMapping

                                                      orderby ct.CountryName ascending

                                                      select new CountryMappingE
                                                      {
                                                          CountryMapping_ID = ct.CountryMapping_Id,
                                                          CountryCode = ct.CountryCode,
                                                          CountryName = ct.CountryName,
                                                          Edit_Date = ct.Edit_Date,
                                                          Country_ID = ct.Country_Id,
                                                          Create_Date = ct.Create_Date,
                                                          Create_User = ct.Create_User,
                                                          Edit_User = ct.Edit_User,
                                                          Status = ct.Status,
                                                          Supplier_ID = ct.Supplier_Id ?? Guid.Empty,
                                                          Supplier_Name = ct.SupplierName
                                                      }).ToList();
                        MasterData.CountryMappingL = supplierCountryMapping;
                        dtRet = ConversionClass.CreateDataTable(supplierCountryMapping);

                    };

                }

                return dtRet;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}