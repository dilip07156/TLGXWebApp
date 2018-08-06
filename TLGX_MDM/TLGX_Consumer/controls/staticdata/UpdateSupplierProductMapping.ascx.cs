using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.Service;
using System.Net;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class UpdateSupplierProductMapping : System.Web.UI.UserControl
    {
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MasterDataSVCs _objMasterRef = new Controller.MasterDataSVCs();
        MasterDataDAL masterdata = new MasterDataDAL();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        #region ==properties
        public Guid Accommodation_ProductMapping_Id { get; set; }
        public Guid Accommodation_Id { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Street { get; set; }
        public string TelephoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Status { get; set; }
        public string CalledFrom { get; set; }
        #endregion

        public void BindData()
        {
            dvAddProduct.Visible = false;
            dvMsg.Style.Add("display", "none");
            grdMatchingProducts.DataSource = null;
            grdMatchingProducts.DataBind();
            dvMatchingRecords.Visible = false;

            List<MDMSVC.DC_Accomodation_ProductMapping> obj = new List<MDMSVC.DC_Accomodation_ProductMapping>();

            if (CalledFrom == "APM")
            {
                obj.Add(new MDMSVC.DC_Accomodation_ProductMapping
                {
                    Accommodation_ProductMapping_Id = Accommodation_ProductMapping_Id,
                    SupplierId = SupplierId,
                    SupplierName = SupplierName,
                    ProductId = ProductId,
                    ProductName = ProductName,
                    ProductType = ProductType,
                    Street = Street,
                    TelephoneNumber = TelephoneNumber,
                    CountryCode = CountryCode,
                    CountryName = CountryName,
                    CityCode = CityCode,
                    CityName = CityName,
                    Status = Status,
                    Accommodation_Id = Accommodation_Id,
                });
            }
            else if (CalledFrom == "RTM")
            {
                //get data from db via service assign to obj

                obj.Add(new MDMSVC.DC_Accomodation_ProductMapping
                {

                    Accommodation_ProductMapping_Id = Accommodation_ProductMapping_Id,
                    SupplierId = SupplierId,
                    SupplierName = SupplierName,
                    ProductId = ProductId,
                    ProductName = ProductName,
                    ProductType = ProductType,
                    Street = Street,
                    TelephoneNumber = TelephoneNumber,
                    CountryCode = CountryCode,
                    CountryName = CountryName,
                    CityCode = CityCode,
                    CityName = CityName,
                    Status = Status,
                    Accommodation_Id = Accommodation_Id,
                });

            }

            frmEditProductMap.Visible = true;
            frmEditProductMap.ChangeMode(FormViewMode.Edit);
            frmEditProductMap.DataSource = obj;
            frmEditProductMap.DataBind();

            #region GetControls
            Label lblSupplierName = (Label)frmEditProductMap.FindControl("lblSupplierName");
            Label lblSupplierCode = (Label)frmEditProductMap.FindControl("lblSupplierCode");
            Label lblSupCountryName = (Label)frmEditProductMap.FindControl("lblSupCountryName");
            Label lblSupCountryCode = (Label)frmEditProductMap.FindControl("lblSupCountryCode");
            Label lblSupStateName = (Label)frmEditProductMap.FindControl("lblSupStateName");
            Label lblSupStateCode = (Label)frmEditProductMap.FindControl("lblSupStateCode");
            Label lblCityName = (Label)frmEditProductMap.FindControl("lblCityName");
            Label lblCityCode = (Label)frmEditProductMap.FindControl("lblCityCode");
            Label lblProductName = (Label)frmEditProductMap.FindControl("lblProductName");
            Label lblProductCode = (Label)frmEditProductMap.FindControl("lblProductCode");
            Label lblProductType = (Label)frmEditProductMap.FindControl("lblProductType");
            Label lblProductAddress = (Label)frmEditProductMap.FindControl("lblProductAddress");
            Label lblHotelName_TX = (Label)frmEditProductMap.FindControl("lblHotelName_TX");

            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");

            DropDownList ddlSystemStateName = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            TextBox txtSystemStateCode = (TextBox)frmEditProductMap.FindControl("txtSystemStateCode");
            Label lblSystemStateCode = (Label)frmEditProductMap.FindControl("lblSystemStateCode");


            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");

            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            TextBox txtSystemRemark = (TextBox)frmEditProductMap.FindControl("txtSystemRemark");
            Button btnAddProduct = (Button)frmEditProductMap.FindControl("btnAddProduct");
            TextBox txtAddCityPlaceId = (TextBox)ucAddNew.FindControl("txtAddCityPlaceId");
            TextBox txtHotelName = (TextBox)ucAddNew.FindControl("txtHotelName");
            TextBox txtStreet = (TextBox)ucAddNew.FindControl("txtStreet");
            TextBox txtStreet2 = (TextBox)ucAddNew.FindControl("txtStreet2");
            TextBox txtPostalCode = (TextBox)ucAddNew.FindControl("txtPostalCode");
            DropDownList ddlAddCountry = (DropDownList)ucAddNew.FindControl("ddlAddCountry");
            DropDownList ddlAddState = (DropDownList)ucAddNew.FindControl("ddlAddState");
            DropDownList ddlAddCity = (DropDownList)ucAddNew.FindControl("ddlAddCity");


            Label lblProductTelephone = (Label)frmEditProductMap.FindControl("lblProductTelephone");
            Label lblProductLatitude = (Label)frmEditProductMap.FindControl("lblProductLatitude");
            Label lblProductLongitude = (Label)frmEditProductMap.FindControl("lblProductLongitude");
            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemProductType = (Label)frmEditProductMap.FindControl("lblSystemProductType");

            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");

            Label lblpMatchedBy = (Label)frmEditProductMap.FindControl("lblpMatchedBy");
            Label lblpMatchedByString = (Label)frmEditProductMap.FindControl("lblpMatchedByString");

            TextBox txtSearchSystemProduct = (TextBox)frmEditProductMap.FindControl("txtSearchSystemProduct");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");

            //clear Grid  (Hotel exist )from Add New screen
            GridView gvGridExist = (GridView)ucAddNew.FindControl("gvGridExist");
            gvGridExist.DataSource = null;
            gvGridExist.DataBind();
            //End
            //hide Div Exist Product on page load
            HtmlGenericControl dvGridExist = (HtmlGenericControl)ucAddNew.FindControl("dvGridExist");
            //dvGridExist.Style.Add("display", "none");
            dvGridExist.Visible = false;
            //END
            #endregion
            string street = "";
            string street2 = "";
            string street3 = "";
            string street4 = "";
            string myCountryName = "";
            string myStateName = "";
            string myCityName = "";
            List<MDMSVC.DC_Accomodation_ProductMapping> masterRoduct = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            if (Accommodation_ProductMapping_Id != null)
            {
                masterRoduct = mapperSVc.GetProductMappingMasterDataById(Accommodation_ProductMapping_Id);
            }

            #region == Fill DropDowns
            fillcountries(ddlSystemCountryName);
            fillmappingstatus(ddlStatus);
            #endregion
            //set HiddenFieldValue
            hdmApmId.Value = Accommodation_ProductMapping_Id.ToString();
            if (masterRoduct[0].SupplierName != null)
                lblSupplierName.Text = masterRoduct[0].SupplierName.ToString(); // System.Web.HttpUtility.HtmlDecode(grdAccoMaps.Rows[index].Cells[2].Text);
            if (masterRoduct[0].SupplierId != null)
            {
                lblSupplierCode.Text = "(" + masterRoduct[0].SupplierId.ToString() + ")";
            }
            lblSupCountryName.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].CountryName);
            if (System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CountryCode)) != "")
                lblSupCountryCode.Text = Convert.ToString(masterRoduct[0].CountryCode);//"(" + System.Web.HttpUtility.HtmlDecode(grdAccoMaps.Rows[index].Cells[6].Text) + ")";

            lblCityName.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CityName));
            if (System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CityCode)) != "")
                lblCityCode.Text = "(" + System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CityCode)) + ")";
            lblProductName.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].ProductName);
            lblHotelName_TX.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].HotelName_Tx);
            lblProductCode.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].ProductId);
            //Added code to set Product Type
            if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(masterRoduct[0].ProductType)))
                lblProductType.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].ProductType);

            if (masterRoduct[0].Remarks != null)
                txtSystemRemark.Text = masterRoduct[0].Remarks.ToString(); //masters.GetRemarksForMapping("product", myRow_Id);
                                                                           //State Name
            lblSupStateName.Text = Convert.ToString(masterRoduct[0].StateName);
            lblSupCountryCode.Text = "(" + Convert.ToString(masterRoduct[0].CountryCode) + ")";

            if (masterRoduct != null)
            {
                #region BindSupplierdata Section
                lblProductAddress.Text = masterRoduct[0].FullAddress;
                txtHotelName.Text = masterRoduct[0].ProductName;

                if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street))
                    street = masterRoduct[0].Street.ToString();
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street2))
                    street2 = masterRoduct[0].Street2.ToString();
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street3))
                    street3 = masterRoduct[0].Street3.ToString();
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street4))
                    street4 = masterRoduct[0].Street4.ToString();
                txtStreet.Text = street;
                txtStreet2.Text = street2 + street3 + street4;
                txtPostalCode.Text = string.IsNullOrWhiteSpace(masterRoduct[0].PostCode) ? string.Empty : masterRoduct[0].PostCode.ToString();
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].TelephoneNumber))
                    lblProductTelephone.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].TelephoneNumber));
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].Latitude))
                    lblProductLatitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].Latitude));
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].Longitude))
                    lblProductLongitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].Longitude));

                if (masterRoduct[0].MatchedBy != null)
                    lblpMatchedBy.Text = masterRoduct[0].MatchedBy.ToString();
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].MatchedByString))
                    lblpMatchedByString.Text = masterRoduct[0].MatchedByString.ToString();

                #endregion


                #region country,StateName, CityName
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemCountryName))
                { myCountryName = Convert.ToString(masterRoduct[0].SystemCountryName); }
                else
                { myCountryName = Convert.ToString(masterRoduct[0].CountryName); }
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemStateName))
                { myStateName = Convert.ToString(masterRoduct[0].SystemStateName); }
                else
                { myStateName = Convert.ToString(masterRoduct[0].StateName); }
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemCityName))
                { myCityName = Convert.ToString(masterRoduct[0].SystemCityName); }
                else
                { myCityName = Convert.ToString(masterRoduct[0].CityName); }

                #endregion

                Guid SystemCountry_Id = Guid.Empty; Guid SystemCity_Id = Guid.Empty;

                if (masterRoduct[0].Country_Id != null)
                    SystemCountry_Id = masterRoduct[0].Country_Id ?? Guid.Empty;

                if (masterRoduct[0].City_Id != null)
                    SystemCity_Id = masterRoduct[0].City_Id ?? Guid.Empty;

                #region Country Mapping data search
                if (SystemCountry_Id == null || SystemCountry_Id == Guid.Empty)
                {
                    MDMSVC.DC_CountryMappingRQ RQ = new MDMSVC.DC_CountryMappingRQ();
                    List<MDMSVC.DC_CountryMapping> lstCountry = new List<MDMSVC.DC_CountryMapping>();
                    RQ.Supplier_Id = masterRoduct[0].Supplier_Id;
                    if (!string.IsNullOrWhiteSpace(masterRoduct[0].CountryName) && Convert.ToString(masterRoduct[0].CountryName) != "null")
                        RQ.SupplierCountryName = masterRoduct[0].CountryName;
                    if (!string.IsNullOrWhiteSpace(masterRoduct[0].CountryCode) && Convert.ToString(masterRoduct[0].CountryCode) != "null")
                        RQ.SupplierCountryCode = masterRoduct[0].CountryCode;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    RQ.Status = "ALL";
                    lstCountry = mapperSVc.GetCountryMappingData(RQ);
                    if (lstCountry.Count > 0)
                        SystemCountry_Id = lstCountry[0].Country_Id ?? Guid.Empty;
                }
                #endregion
                if (!string.IsNullOrWhiteSpace(myCountryName) && (SystemCountry_Id == Guid.Empty || SystemCountry_Id == null))
                {
                    MDMSVC.DC_Country_Search_RQ RQ = new MDMSVC.DC_Country_Search_RQ();
                    List<MDMSVC.DC_Country> lstCountry = new List<MDMSVC.DC_Country>();
                    RQ.Country_Name = myCountryName.Trim();
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    lstCountry = _objMasterRef.GetCountryMasterData(RQ);
                    if (lstCountry.Count > 0)
                        SystemCountry_Id = lstCountry[0].Country_Id;
                }


                if (SystemCity_Id == null || SystemCity_Id == Guid.Empty)
                {
                    MDMSVC.DC_CityMapping_RQ RQ = new MDMSVC.DC_CityMapping_RQ();
                    List<MDMSVC.DC_CityMapping> lstCity = new List<MDMSVC.DC_CityMapping>();
                    RQ.Supplier_Id = masterRoduct[0].Supplier_Id;

                    if (SystemCountry_Id != null && SystemCountry_Id != Guid.Empty)
                        RQ.Country_Id = SystemCountry_Id;
                    else if (!string.IsNullOrWhiteSpace(masterRoduct[0].CountryName))
                        RQ.SupplierCountryName = masterRoduct[0].CountryName;

                    if (!string.IsNullOrWhiteSpace(masterRoduct[0].CityName) && Convert.ToString(masterRoduct[0].CityName) != "null")
                        RQ.SupplierCityName = masterRoduct[0].CityName;
                    if (!string.IsNullOrWhiteSpace(masterRoduct[0].CityCode) && Convert.ToString(masterRoduct[0].CityCode) != "null")
                        RQ.SupplierCityCode = masterRoduct[0].CityCode;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    RQ.Status = "ALL";
                    lstCity = mapperSVc.GetCityMappingData(RQ);
                    if (lstCity.Count > 0)
                        SystemCity_Id = lstCity[0].City_Id ?? Guid.Empty;
                }
                if (!string.IsNullOrWhiteSpace(myCityName) && (SystemCity_Id == Guid.Empty || SystemCity_Id == null))
                {
                    MDMSVC.DC_City_Search_RQ RQ = new MDMSVC.DC_City_Search_RQ();
                    List<MDMSVC.DC_City> lstCity = new List<MDMSVC.DC_City>();
                    RQ.City_Name = myCityName.Trim();
                    RQ.Country_Id = SystemCountry_Id;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    lstCity = _objMasterRef.GetCityMasterData(RQ);
                    bool foundExactMatch = false;
                    if (lstCity.Count > 1)
                    {
                        foreach (var item in lstCity)
                        {
                            if (item.Name.ToString() == myCityName.Trim())
                            {
                                SystemCity_Id = item.City_Id;
                                foundExactMatch = true;
                                break;
                            }
                        }
                    }
                    if (lstCity.Count > 0 && !foundExactMatch)
                        SystemCity_Id = lstCity[0].City_Id;

                }

                if (SystemCountry_Id != null && Guid.Parse(Convert.ToString(SystemCountry_Id)) != Guid.Empty)
                    ddlAddCountry.SelectedIndex = ddlAddCountry.Items.IndexOf(ddlAddCountry.Items.FindByValue(Convert.ToString(SystemCountry_Id.ToString())));
                if (SystemCountry_Id != null && Guid.Parse(Convert.ToString(SystemCountry_Id)) != Guid.Empty)
                    ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByValue(Convert.ToString(SystemCountry_Id.ToString())));

                //Added code for state dropdown
                var selState_ID = _objMasterRef.GetDetailsByIdOrName(new MDMSVC.DC_GenericMasterDetails_ByIDOrName()
                {
                    WhatFor = MDMSVC.DetailsWhatFor.IDByName,
                    Name = myStateName,
                    Optional1 = myCountryName,
                    ObjName = MDMSVC.EntityType.state
                });

                fillStates(ddlSystemCountryName, ddlSystemStateName);
                if (ddlSystemStateName.Items.Count > 1)
                {
                    if (!string.IsNullOrWhiteSpace(masterRoduct[0].StateName))
                    {
                        if (selState_ID != null && selState_ID.ID != null)
                        {
                            if (Guid.Parse(Convert.ToString(selState_ID.ID)) != Guid.Empty)
                                ddlSystemStateName.SelectedIndex = ddlSystemStateName.Items.IndexOf(ddlSystemStateName.Items.FindByValue(selState_ID.ID.ToString()));
                        }
                    }
                }
                fillStates(ddlAddCountry, ddlAddState);
                if (ddlAddState.Items.Count > 1)
                {
                    if (!string.IsNullOrWhiteSpace(masterRoduct[0].StateName))
                    {
                        if (selState_ID != null && selState_ID.ID != null)
                        {
                            if (Guid.Parse(Convert.ToString(selState_ID.ID)) != Guid.Empty)
                                ddlAddState.SelectedIndex = ddlAddState.Items.IndexOf(ddlAddState.Items.FindByValue(selState_ID.ID.ToString()));
                        }
                    }
                }


                fillcities(ddlAddCity, ddlAddCountry);
                if (ddlAddCity.Items.Count > 1)
                {
                    //if (!string.IsNullOrWhiteSpace(masterRoduct[0].CityName))
                    //{
                    if (SystemCity_Id != null && SystemCity_Id != Guid.Empty)
                        ddlAddCity.SelectedIndex = ddlAddCity.Items.IndexOf(ddlAddCity.Items.FindByValue(SystemCity_Id.ToString()));
                    //}
                }


                fillcities(ddlSystemCityName, ddlSystemCountryName);
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(System.Web.HttpUtility.HtmlDecode(masterRoduct[0].Status)));
                if (SystemCity_Id != null && SystemCity_Id != Guid.Empty)
                    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(SystemCity_Id.ToString()));

                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemProductName))
                {
                    string sysproductname = Convert.ToString(masterRoduct[0].SystemProductName);
                    string sysproduct_Id = Convert.ToString(masterRoduct[0].Accommodation_Id);
                    txtSearchSystemProduct.Text = sysproductname;
                    txtSystemProductCode.Text = Convert.ToString(masterRoduct[0].SystemProductCode);
                    ddlSystemProductName.Items.Insert(0, new ListItem("---ALL---", "0"));
                    ddlSystemProductName.Items.Insert(1, new ListItem(sysproductname, sysproduct_Id));
                    ddlSystemProductName.SelectedIndex = 1;
                    hdnSelSystemProduct_Id.Value = sysproduct_Id;
                }

                lblSystemProductType.Text = masterRoduct[0].SystemProductType;
                lblSystemProductAddress.Text = masterRoduct[0].SystemFullAddress;
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemTelephone))
                    lblSystemTelephone.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemTelephone));
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemLocation))
                    lblSystemLocation.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemLocation));
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemLatitude))
                    lblSystemLatitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemLatitude));
                if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemLongitude))
                    lblSystemLongitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemLongitude));

                if (ddlSystemCountryName.SelectedItem.Value != "0")
                    lblSystemCountryCode.Text = masterdata.GetCodeById("country", Guid.Parse(ddlSystemCountryName.SelectedItem.Value));
                if (lblSystemCountryCode.Text.Replace(" ", "") != "")
                    lblSystemCountryCode.Text = "(" + lblSystemCountryCode.Text + ")";
                //txtSystemCityCode.Text = masterdata.GetCodeById("city", Guid.Parse(ddlSystemCityName.SelectedItem.Value));
                if (ddlSystemCityName.SelectedItem.Value != "0")
                    lblSystemCityCode.Text = masterdata.GetCodeById("city", Guid.Parse(ddlSystemCityName.SelectedItem.Value));
                if (lblSystemCityCode.Text.Replace(" ", "") != "")
                {
                    lblSystemCityCode.Text = "(" + lblSystemCityCode.Text + ")";
                }


                Guid AccoId;
                if (Guid.TryParse(ddlSystemProductName.SelectedItem.Value, out AccoId))
                {
                    txtSystemProductCode.Text = masterdata.GetCodeById("product", Guid.Parse(ddlSystemProductName.SelectedItem.Value));
                }
                else
                {
                    txtSystemProductCode.Text = string.Empty;
                }

            }
            if (ddlSystemProductName.SelectedItem.Value == "0")
                btnAddProduct.Attributes.Add("style", "display:block");
            else
                btnAddProduct.Attributes.Add("style", "display:none");
            hdnFlag.Value = "false";
        }

        #region ==Fill Dropdowns methods
        private void fillcountries(DropDownList ddl)
        {
            try
            {
                ddl.DataSource = _objMasterRef.GetAllCountries();
                ddl.DataValueField = "Country_Id";
                ddl.DataTextField = "Country_Name";
                ddl.DataBind();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        private void fillmappingstatus(DropDownList ddl)
        {
            fillAttributeValues(ddl, "MappingStatus", "ProductSupplierMapping");
        }
        private void fillAttributeValues(DropDownList ddl, string Attribute_Name, string OptionFor)
        {
            MDMSVC.DC_M_masterattributelists list = LookupAtrributes.GetAllAttributeAndValuesByFOR(OptionFor, Attribute_Name);
            ddl.DataSource = list.MasterAttributeValues;

            if (Attribute_Name == "MatchingPriority")
            {
                ddl.DataTextField = "OTA_CodeTableValue";
                ddl.DataValueField = "AttributeValue";
                ddl.DataBind();

                if (list.MasterAttributeValues.Length > 0)
                {
                    MDMSVC.DC_M_masterattributevalue[] vals = list.MasterAttributeValues;

                    foreach (MDMSVC.DC_M_masterattributevalue val in vals)
                    {
                        if (!string.IsNullOrWhiteSpace(val.OTA_CodeTableValue) && ddl.Items.FindByValue(val.AttributeValue) != null)
                            ddl.Items.FindByValue(val.AttributeValue).Attributes.Add("title", val.AttributeValue);
                    }
                }
            }
            else
            {
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
            }

        }
        private void fillStates(DropDownList ddlSystemCountryName, DropDownList ddlSystemStateName)
        {
            ddlSystemStateName.Items.Clear();
            if (ddlSystemCountryName.SelectedItem.Value != "0")
            {
                MDMSVC.DC_State_Search_RQ RQ = new MDMSVC.DC_State_Search_RQ();
                RQ.Country_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value);

                var res = _objMasterRef.GetStatesMaster(RQ);
                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddlSystemStateName.DataSource = res;
                ddlSystemStateName.DataValueField = "State_Id";
                ddlSystemStateName.DataTextField = "State_Name";
                ddlSystemStateName.DataBind();
            }
            ddlSystemStateName.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        public void fillcities(DropDownList ddl, DropDownList ddlp)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {

                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddl.DataSource = _objMasterRef.GetMasterCityData(ddlp.SelectedItem.Value);
                ddl.DataValueField = "City_Id";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        public void fillproducts(DropDownList ddl, DropDownList ddlp, DropDownList ddlc)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {
                //ddl.DataSource = masterdata.GetProductByCity(ddlp.SelectedItem.Text, ddlc.SelectedItem.Text);
                ddl.DataSource = _objMasterRef.GetProductByCity(new MDMSVC.DC_Accomodation_DDL() { CityName = ddlp.SelectedItem.Text, CountryName = ddlc.SelectedItem.Text, IsActive = true });

                ddl.DataValueField = "Accommodation_Id";
                ddl.DataTextField = "HotelName";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        #endregion

        protected void frmEditProductMap_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            #region Getting Controls 
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Label lblSupplierName = (Label)frmEditProductMap.FindControl("lblSupplierName");
            Label lblSupplierCode = (Label)frmEditProductMap.FindControl("lblSupplierCode");
            Label lblSupCountryName = (Label)frmEditProductMap.FindControl("lblSupCountryName");
            Label lblSupCountryCode = (Label)frmEditProductMap.FindControl("lblSupCountryCode");
            Label lblCityName = (Label)frmEditProductMap.FindControl("lblCityName");
            Label lblCityCode = (Label)frmEditProductMap.FindControl("lblCityCode");
            Label lblProductName = (Label)frmEditProductMap.FindControl("lblProductName");
            Label lblSystemProductType = (Label)frmEditProductMap.FindControl("lblSystemProductType");
            Label lblProductCode = (Label)frmEditProductMap.FindControl("lblProductCode");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");
            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");


            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            DropDownList ddlSystemSystemName = (DropDownList)frmEditProductMap.FindControl("ddlSystemSystemName");


            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");
            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");

            Button btnAddProduct = (Button)frmEditProductMap.FindControl("btnAddProduct");

            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            TextBox txtSystemRemark = (TextBox)frmEditProductMap.FindControl("txtSystemRemark");
            TextBox txtSearchSystemProduct = (TextBox)frmEditProductMap.FindControl("txtSearchSystemProduct");

            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");


            //Button btnMatchedMapSelected = (Button)frmEditProductMap.FindControl("btnMatchedMapSelected");
            //Button btnMatchedMapAll = (Button)frmEditProductMap.FindControl("btnMatchedMapAll");
            HiddenField hdnIsAnyChanges = (HiddenField)this.Parent.FindControl("hdnIsAnyChanges");

            #endregion
            if (e.CommandName == "Add")
            {
                hdnIsAnyChanges.Value = "true";
                Guid myRow_Id = Accommodation_ProductMapping_Id;
                Guid? AccoId = null;
                string countryname = string.Empty;
                Guid? countryId = null;
                string cityname = string.Empty;

                if (txtSearchSystemProduct.Text != string.Empty && ((ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedItem.Value != "0") || !string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value)))
                {
                    if (ddlSystemCountryName.SelectedIndex != 0)
                    {

                        countryname = ddlSystemCountryName.SelectedItem.Value;
                        countryId = new Guid(countryname);
                        cityname = ddlSystemCityName.SelectedItem.Value;
                        if (ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedItem.Value != "0")
                        {
                            AccoId = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                        }
                        else if (!string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value))
                        {
                            AccoId = Guid.Parse(hdnSelSystemProduct_Id.Value);
                        }
                    }
                    MDMSVC.DC_Accomodation_ProductMapping newObj = new MDMSVC.DC_Accomodation_ProductMapping
                    {
                        Accommodation_ProductMapping_Id = Guid.Parse(hdmApmId.Value),
                        Accommodation_Id = AccoId,
                        SystemCountryName = countryname,
                        SystemCityName = cityname,
                        Status = ddlStatus.SelectedItem.Text,
                        Remarks = txtSystemRemark.Text,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };

                    if (!string.IsNullOrEmpty(lblSupplierName.Text))
                    {
                        newObj.SupplierName = lblSupplierName.Text;
                        newObj.SupplierId = lblSupplierCode.Text.Replace("(", "").Replace(")", "");
                    }
                    RQ.Add(newObj);
                    if (mapperSVc.UpdateProductMappingData(RQ))
                    {

                        var result = AccSvc.GetAccomodationBasicInfo(newObj.Accommodation_Id ?? Guid.Empty);
                        if (result != null)
                        {
                            txtSystemProductCode.Text = Convert.ToString(result[0].CompanyHotelID);
                            lblSystemProductAddress.Text = Convert.ToString(result[0].FullAddress);
                            lblSystemLocation.Text = Convert.ToString(result[0].Location);
                            lblSystemTelephone.Text = Convert.ToString(result[0].Telephone_Tx);
                            lblSystemLatitude.Text = Convert.ToString(result[0].Latitude);
                            lblSystemLongitude.Text = Convert.ToString(result[0].Longitude);
                            lblSystemProductType.Text = Convert.ToString(result[0].SystemProductType);

                            btnAddProduct.Attributes.Add("style", "display:none");

                        }
                        dvMsg.Style.Add("display", "block");
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been mapped successfully", BootstrapAlertType.Success);
                        if (!(ddlSystemCountryName.SelectedIndex == 0))
                        {
                            //fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
                            fillmatchingdata("", 0);
                            //Setting control value setted in javascript

                            dvMatchingRecords.Visible = true;
                            btnMatchedMapSelected.Visible = true;
                            btnMatchedMapAll.Visible = true;
                            hdnFlag.Value = "false";
                        }
                        else
                        {
                            dvMatchingRecords.Visible = false;
                            btnMatchedMapSelected.Visible = false;
                            btnMatchedMapAll.Visible = false;
                            hdnFlag.Value = "false";
                        }
                    }
                }
                else
                {
                    dvMsg.Style.Add("display", "block");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please select valid Hotel to map", BootstrapAlertType.Warning);

                }
            }
            else if (e.CommandName == "Cancel")
            {
                frmEditProductMap.ChangeMode(FormViewMode.Edit);
                frmEditProductMap.DataSource = null;
                frmEditProductMap.DataBind();
                frmEditProductMap.Visible = false;
                dvMatchingRecords.Visible = false;
                btnMatchedMapSelected.Visible = false;
                btnMatchedMapAll.Visible = false;
                //hdnPageNumber.Value = grdAccoMaps.PageIndex.ToString();
                //fillproductdata(ref isDataExist, "supplier", Convert.ToInt32(hdnPageNumber.Value ?? "0"));
            }
            else if (e.CommandName == "OpenAddProduct")
            {
                var AddHotelcalledFrom = this.Parent.Page.ToString();
                if (AddHotelcalledFrom.Contains("roomtype"))
                {
                    dvMsg.Style.Add("display", "block");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please add hotel from HotelMapping Screen", BootstrapAlertType.Warning);
                }
                else
                {
                    dvAddProduct.Style.Add("display", "block");
                    dvAddProduct.Visible = true;
                }
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "javascript:closeCityMappingModal();", true);

        }

        private void fillmatchingdata(string from, int pPageIndex)
        {
            Label lblSupCountryName = (Label)frmEditProductMap.FindControl("lblSupCountryName");
            Label lblCityName = (Label)frmEditProductMap.FindControl("lblCityName");
            Label lblProductName = (Label)frmEditProductMap.FindControl("lblProductName");
            Label lblHotelName_TX = (Label)frmEditProductMap.FindControl("lblHotelName_TX");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            dvMsg.Style.Add("display", "none");
            MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQ = new MDMSVC.DC_Mapping_ProductSupplier_Search_RQ();
            if (ddlMatchingStatus.SelectedItem.Value != "0")
                RQ.Status = ddlMatchingStatus.SelectedItem.Text;
            if (!string.IsNullOrWhiteSpace(lblSupCountryName.Text))
                RQ.CountryName = lblSupCountryName.Text;

            if (ddlSystemCountryName.SelectedItem.Value != "0")
                RQ.Country_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value);

            if (ddlSystemCityName.SelectedItem.Value != "0")
                RQ.City_Id = Guid.Parse(ddlSystemCityName.SelectedItem.Value);

            if (!string.IsNullOrWhiteSpace(lblCityName.Text))
                RQ.CityName = lblCityName.Text;

            if (!string.IsNullOrWhiteSpace(lblProductName.Text))
                RQ.SupplierProductName = lblProductName.Text;

            if (!string.IsNullOrWhiteSpace(lblHotelName_TX.Text))
                RQ.HotelName_TX = lblHotelName_TX.Text;

            RQ.Via = "CROSS";
            RQ.StatusExcept = ddlStatus.SelectedItem.Text.Trim().ToUpper();
            RQ.PageNo = pPageIndex;
            RQ.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            var res = mapperSVc.GetProductMappingData(RQ);
            grdMatchingProducts.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    if (from != "status")
                    {
                        ddlMatchingStatus.Items.Clear();
                        var disstatus = (from s in res where s.Status != null orderby s.Status select s.Status).Distinct();
                        ddlMatchingStatus.DataSource = disstatus;
                        ddlMatchingStatus.DataBind();
                        ddlMatchingStatus.Items.Insert(0, new ListItem("--ALL--", "0"));
                    }
                    grdMatchingProducts.VirtualItemCount = res[0].TotalRecords;
                    if (res[0].TotalRecords > 0)
                    {
                        lblMsg.Text = "There are (" + res[0].TotalRecords.ToString() + ") similar records in system matching with mapped combination. Do you wish to map the same? ";
                    }
                    else
                    {
                        lblMsg.Text = "No similar records found matching with updated combination";
                    }
                }
                else
                {
                    lblMsg.Text = "No similar records found matching with updated combination";
                }
            }
            else
            {
                lblMsg.Text = "No similar records found matching with updated combination";
            }
            grdMatchingProducts.PageIndex = pPageIndex;
            grdMatchingProducts.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            grdMatchingProducts.DataBind();
        }

        #region == Selected Index changed
        protected void ddlMatchingPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("", 0);
            ddlMatchingStatus.Focus();
        }
        protected void ddlMatchingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("status", 0);
        }
        protected void ddlSystemStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemStateName = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            DropDownList ddlAddState = (DropDownList)ucAddNew.FindControl("ddlAddState");
            HiddenField hdnIsJavascriptChagedValueddlSystemStateName = (HiddenField)frmEditProductMap.FindControl("hdnIsJavascriptChagedValueddlSystemStateName");
            if (hdnIsJavascriptChagedValueddlSystemStateName.Value != "true")
                ResetSystemDetails("state");
            else
                hdnIsJavascriptChagedValueddlSystemStateName.Value = "false";
            ddlAddState.SelectedIndex = ddlAddState.Items.IndexOf(ddlAddState.Items.FindByValue(ddlSystemStateName.SelectedItem.Value));

        }
        protected void ddlSystemCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region controls
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");

            DropDownList ddlSystemStateName = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            TextBox txtSystemStateCode = (TextBox)frmEditProductMap.FindControl("txtSystemStateCode");
            Label lblSystemStateCode = (Label)frmEditProductMap.FindControl("lblSystemStateCode");

            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");



            DropDownList ddlAddCountry = (DropDownList)ucAddNew.FindControl("ddlAddCountry");
            DropDownList ddlAddState = (DropDownList)ucAddNew.FindControl("ddlAddState");
            DropDownList ddlAddCity = (DropDownList)ucAddNew.FindControl("ddlAddCity");
            #endregion

            if (ddlSystemCountryName.SelectedIndex != 0)
            {
                lblSystemCountryCode.Text = masterdata.GetCodeById(MDMSVC.EntityType.country, Guid.Parse(ddlSystemCountryName.SelectedItem.Value));
                if (lblSystemCountryCode.Text.Replace(" ", "") != "")
                    lblSystemCountryCode.Text = "(" + lblSystemCountryCode.Text + ")";
                fillStates(ddlSystemCountryName, ddlSystemStateName);

                fillcities(ddlSystemCityName, ddlSystemCountryName);

                ddlAddCountry.SelectedIndex = ddlAddCountry.Items.IndexOf(ddlAddCountry.Items.FindByValue(ddlSystemCountryName.SelectedItem.Value));
                fillStates(ddlAddCountry, ddlAddState);
                fillcities(ddlAddCity, ddlAddCountry);
                ddlSystemStateName.Focus();
            }
            ddlSystemCityName.SelectedIndex = 0;
            lblSystemCountryCode.Text = string.Empty;
            txtSystemProductCode.Text = string.Empty;
            ResetSystemDetails("");
        }
        protected void ddlSystemCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");
            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            DropDownList ddlAddCountry = (DropDownList)ucAddNew.FindControl("ddlAddCountry");
            DropDownList ddlAddCity = (DropDownList)ucAddNew.FindControl("ddlAddCity");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");
            HiddenField hdnIsJavascriptChagedValueddlSystemStateName = (HiddenField)frmEditProductMap.FindControl("hdnIsJavascriptChagedValueddlSystemStateName");
            HiddenField hdnIsJavascriptChagedValueddlSystemCityName = (HiddenField)frmEditProductMap.FindControl("hdnIsJavascriptChagedValueddlSystemCityName");

            lblSystemProductAddress.Text = "";

            //txtSystemCityCode.Text = masterdata.GetCodeById("city", Guid.Parse(ddlSystemCityName.SelectedItem.Value));
            if (ddlSystemCityName.SelectedItem.Value != "0")
                lblSystemCityCode.Text = masterdata.GetCodeById(MDMSVC.EntityType.city, Guid.Parse(ddlSystemCityName.SelectedItem.Value));
            if (lblSystemCityCode.Text.Replace(" ", "") != "")
                lblSystemCityCode.Text = "(" + lblSystemCityCode.Text + ")";
            fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);
            txtSystemProductCode.Text = "";
            ddlAddCity.SelectedIndex = ddlAddCity.Items.IndexOf(ddlAddCity.Items.FindByValue(ddlSystemCityName.SelectedItem.Value));
            ddlSystemProductName.Focus();
            if (hdnIsJavascriptChagedValueddlSystemCityName.Value != "true")
                ResetSystemDetails("");
            else
                hdnIsJavascriptChagedValueddlSystemCityName.Value = "false";

        }
        protected void ddlSystemProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            Button btnAddProduct = (Button)frmEditProductMap.FindControl("btnAddProduct");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");

            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");
            if (ddlSystemProductName.SelectedItem.Value == "0")
            {
                btnAddProduct.Visible = true;
            }
            else
            {
                txtSystemProductCode.Text = masterdata.GetCodeById("product", Guid.Parse(ddlSystemProductName.SelectedItem.Value));
                btnAddProduct.Visible = false;
                AccomodationSVC _acmd = new AccomodationSVC();
                var result = _acmd.SearchHotels(new MDMSVC.DC_Accomodation_Search_RQ()
                {
                    AccomodationId = ddlSystemProductName.SelectedItem.Value,
                    PageNo = 0,
                    PageSize = 5
                });
                if (result != null && result.Count > 0)
                {
                    lblSystemProductAddress.Text = result[0].FullAddress;
                    lblSystemTelephone.Text = result[0].Telephone_Tx;
                    lblSystemLatitude.Text = result[0].Latitude;
                    lblSystemLongitude.Text = result[0].Longitude;
                    lblSystemLocation.Text = result[0].Location;
                }
            }
            txtSystemProductCode.Focus();
        }
        protected void grdMatchingProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillmatchingdata("", e.NewPageIndex);
        }
        #endregion

        #region ==On Button clicks
        protected void btnMatchedMapSelected_Click(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");
            List<MDMSVC.DC_Accomodation_ProductMapping> newObj = new List<MDMSVC.DC_Accomodation_ProductMapping>();

            //To Refresh AccoMapping Grid after closing PopUp.
            HiddenField hdnIsAnyChanges = (HiddenField)this.Parent.FindControl("hdnIsAnyChanges");
            hdnIsAnyChanges.Value = "true";
            //end

            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid? myAcco_Id = Guid.Empty;
            bool res = false;
           
            foreach (GridViewRow row in grdMatchingProducts.Rows)
            {
                HtmlInputCheckBox chk = row.Cells[12].Controls[1] as HtmlInputCheckBox;
                //   CheckBox chk = row.Cells[11].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myAcco_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[0].ToString());
                    mySupplier_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[1].ToString());
                    if (ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedValue != "0")
                        myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                    else if (!string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value))
                        myAcco_Id = Guid.Parse(hdnSelSystemProduct_Id.Value);
                    //myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Accomodation_ProductMapping param = new MDMSVC.DC_Accomodation_ProductMapping();
                    param.Accommodation_ProductMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myAcco_Id != null)
                        param.Accommodation_Id = myAcco_Id;
                    param.Status = ddlStatus.SelectedItem.Text;
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    newObj.Add(param);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myAcco_Id = Guid.Empty;
                }
            }
            if (mapperSVc.UpdateProductMappingData(newObj))
            {
                fillmatchingdata("", grdMatchingProducts.PageIndex);
                //fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
                hdnFlag.Value = "false";
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
            }
        }
        protected void btnMatchedMapAll_Click(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");

            //To Refresh AccoMapping Grid after closing PopUp.
            HiddenField hdnIsAnyChanges = (HiddenField)this.Parent.FindControl("hdnIsAnyChanges");
            hdnIsAnyChanges.Value = "true";
            //end

            List<MDMSVC.DC_Accomodation_ProductMapping> newObj = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid? myAcco_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdMatchingProducts.Rows)
            {
                myRow_Id = Guid.Empty;
                mySupplier_Id = Guid.Empty;
                myAcco_Id = Guid.Empty;
                int index = row.RowIndex;
                myRow_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[0].ToString());
                mySupplier_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[1].ToString());

                if (ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedValue != "0")
                    myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                else if (!string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value))
                    myAcco_Id = Guid.Parse(hdnSelSystemProduct_Id.Value);

                //myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Accomodation_ProductMapping param = new MDMSVC.DC_Accomodation_ProductMapping();
                    param.Accommodation_ProductMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myAcco_Id != null)
                        param.Accommodation_Id = myAcco_Id;
                    param.Status = ddlStatus.SelectedItem.Text;
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    newObj.Add(param);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myAcco_Id = Guid.Empty;
                }
            }
            if (mapperSVc.UpdateProductMappingData(newObj))
            {
                fillmatchingdata("", grdMatchingProducts.PageIndex);
                //fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
                hdnFlag.Value = "false";
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
            }

        }
        #endregion

        public void ResetSystemDetails(string byReset)
        {
            DropDownList ddlStystemCity = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            DropDownList ddlStystemState = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");
            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");
            TextBox txtSearchSystemProduct = (TextBox)frmEditProductMap.FindControl("txtSearchSystemProduct");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            HiddenField hdnSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSystemProduct_Id");
            HiddenField hdnSystemProduct = (HiddenField)frmEditProductMap.FindControl("hdnSystemProduct");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");
            lblSystemCityCode.Text = "";
            lblSystemProductAddress.Text = string.Empty;
            lblSystemCityCode.Text = string.Empty;
            lblSystemLocation.Text = string.Empty;
            lblSystemTelephone.Text = string.Empty;
            lblSystemLatitude.Text = string.Empty;
            lblSystemLongitude.Text = string.Empty;
            txtSearchSystemProduct.Text = string.Empty;
            txtSystemProductCode.Text = string.Empty;
            hdnSystemProduct_Id.Value = null;
            hdnSystemProduct.Value = null;
            hdnSelSystemProduct_Id.Value = null;
            if (byReset == "state")
            {
                ddlStystemCity.SelectedIndex = 0;
            }

        }
    }
}