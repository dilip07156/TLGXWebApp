using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.hotel
{
    public partial class AddNew : System.Web.UI.UserControl
    {
        MasterDataDAL masterdata = new MasterDataDAL();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MappingSVCs mapperSvc = new Controller.MappingSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        List<MDMSVC.DC_Accomodation_Search_RS> res = new List<MDMSVC.DC_Accomodation_Search_RS>();
        public static string AttributeOptionFor = "HotelInfo";
        public static string ParentPageName = "";
        MasterDataSVCs _objMasterData = new MasterDataSVCs();
        public static bool InsertFrom = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setParentPage();
                fillcountrydropdown();
                fillproductcaterogy();
                fillstarrating();
            }
        }

        private void setParentPage()
        {
            var a = this.Parent;
            string vPath = "";
            Type typo = this.BindingContainer.GetType();
            if (typo.FullName == "System.Web.UI.WebControls.FormView")
            {
                vPath = ((System.Web.UI.WebControls.FormView)this.BindingContainer).Page.AppRelativeVirtualPath;
            }
            else
            {
                //((TLGX_Consumer.staticdata.searchAccommodationProductMapping)this.BindingContainer).searchAccoMapping.dtCountrMappingDetail
                vPath = ((System.Web.UI.TemplateControl)this.BindingContainer).AppRelativeVirtualPath;
            }

            string dir = this.NamingContainer.BindingContainer.AppRelativeTemplateSourceDirectory;

            ParentPageName = vPath.Replace(dir, "");

            if (ParentPageName == "searchAccoMapping.ascx" || ParentPageName == "search.aspx")
            {
                InsertFrom = true;
                dvKeyFacts.Visible = false;
                btnReset.Visible = false;
                txtHotelName.Enabled = false;
                dvGridExist.Visible = false;
                dvAddProductCategorySubType.Visible = false;
                dvMainMapDv.Attributes.Add("class", "col-lg-5");
                dvvlsSumm.Attributes.Add("class", "col-lg-9");
                dvExistingRecords.Attributes.Add("class", "col-lg-9");
                map.Style.Add("height", "240px");
            }
            else
            {
                InsertFrom = false;
                dvKeyFacts.Visible = true;
                btnReset.Visible = true;
                txtHotelName.Enabled = true;
                dvGridExist.Visible = false;
                dvAddProductCategorySubType.Visible = true;
                dvMainMapDv.Attributes.Add("class", "col-lg-8");
                dvvlsSumm.Attributes.Add("class", "col-lg-12");
                dvExistingRecords.Attributes.Add("class", "col-lg-12");
                map.Style.Add("height", "360px");
            }
        }
        private void fillcountrydropdown()
        {
            //var resSet = masterdata.GetMasterCountryData("");
            //ddlAddCountry.DataSource = resSet;
            //ddlAddCountry.DataValueField = "Country_ID";
            //ddlAddCountry.DataTextField = "Name";
            //ddlAddCountry.DataBind();

            var result = _objMasterData.GetAllCountries();
            ddlAddCountry.DataSource = result;
            ddlAddCountry.DataValueField = "Country_Id";
            ddlAddCountry.DataTextField = "Country_Name";
            ddlAddCountry.DataBind();


        }

        private void fillStatedropdown(string Country_ID)
        {
            try
            {
                ddlAddState.Items.Clear();
                var result = _objMasterData.GetStatesByCountry(Country_ID);
                if (Country_ID != "")
                {
                    ddlAddState.DataSource = result;
                    ddlAddState.DataTextField = "State_Name";
                    ddlAddState.DataValueField = "State_Id";
                    ddlAddState.DataBind();
                    ddlAddState.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = "0" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fillcitydropdown(string Country_ID)
        {
            ddlAddCity.Items.Clear();
            //var resSet = masterdata.GetMasterCityData(new Guid(Country_ID)); 
            var result = _objMasterData.GetMasterCityData(Country_ID);
            if (Country_ID != "")
            {
                ddlAddCity.DataSource = result;
                ddlAddCity.DataValueField = "City_Id";
                ddlAddCity.DataTextField = "Name";
                ddlAddCity.DataBind();
                ddlAddCity.Items.Insert(0, new ListItem("--ALL--", ""));
            }
        }

        private void fillproductcaterogy()
        {
            //fillAttributeValues("ddlAddProductCategorySubType", "ProductCategorySubType");
            ddlAddProductCategorySubType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "ProductCategorySubType").MasterAttributeValues;
            ddlAddProductCategorySubType.DataTextField = "AttributeValue";
            ddlAddProductCategorySubType.DataValueField = "MasterAttributeValue_Id";
            ddlAddProductCategorySubType.DataBind();
            ddlAddProductCategorySubType.SelectedIndex = ddlAddProductCategorySubType.Items.IndexOf(ddlAddProductCategorySubType.Items.FindByText("Hotel"));
        }

        private void fillstarrating()
        {
            //fillAttributeValues("ddlAddProductCategorySubType", "ProductCategorySubType");
            ddlStarRating.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "Stars").MasterAttributeValues;
            ddlStarRating.DataTextField = "AttributeValue";
            ddlStarRating.DataValueField = "MasterAttributeValue_Id";
            ddlStarRating.DataBind();
        }

        private string CheckForDuplicate()
        {
            string ret = "";

            //DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
            //DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
            //DropDownList ddlProductCategorySubType = (DropDownList)frmHotelOverview.FindControl("ddlProductCategorySubType");
            //TextBox txtHotelName = (TextBox)frmHotelOverview.FindControl("txtHotelName");
            //TextBox txtCommonHotelId = (TextBox)frmHotelOverview.FindControl("txtCommonHotelId");
            //TextBox txtPostalCode = (TextBox)frmHotelOverview.FindControl("txtPostalCode");
            //TextBox txtHotelID = (TextBox)frmHotelOverview.FindControl("txtHotelID");
            RQParams.ProductCategory = "Accommodation";
            RQParams.ProductCategorySubType = ddlAddProductCategorySubType.SelectedItem.Text;
            RQParams.Status = "ACTIVE";
            RQParams.PageNo = 0;
            RQParams.PageSize = int.MaxValue;
            if (txtAddCityPlaceId.Text.Length != 0)
            {
                RQParams.Google_Place_Id = txtAddCityPlaceId.Text;
                res = AccSvc.SearchHotels(RQParams);

                if ((res == null) || ((res != null) && (res.Count == 0)))
                {
                    RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
                    RQParams.ProductCategory = "Accommodation";
                    RQParams.ProductCategorySubType = ddlAddProductCategorySubType.SelectedItem.Text;
                    RQParams.Status = "ACTIVE";
                    RQParams.PageNo = 0;
                    RQParams.PageSize = int.MaxValue;
                    if (txtHotelName.Text.Length != 0)
                        RQParams.HotelName = txtHotelName.Text;
                    if (ddlAddCountry.SelectedItem.Text != "---ALL---")
                        RQParams.Country = ddlAddCountry.SelectedItem.Text;
                    if (ddlAddCity.SelectedItem.Text != "---ALL---")
                        RQParams.City = ddlAddCity.SelectedItem.Text;
                    res = AccSvc.SearchHotels(RQParams);
                }
                else
                {
                    ret = "placeid";
                }
            }
            else
            {
                RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
                RQParams.ProductCategory = "Accommodation";
                RQParams.ProductCategorySubType = ddlAddProductCategorySubType.SelectedItem.Text;
                RQParams.Status = "ACTIVE";
                RQParams.PageNo = 0;
                RQParams.PageSize = int.MaxValue;
                if (txtHotelName.Text.Length != 0)
                    RQParams.HotelName = txtHotelName.Text;
                if (ddlAddCountry.SelectedItem.Text != "---ALL---")
                    RQParams.Country = ddlAddCountry.SelectedItem.Text;
                if (ddlAddCity.SelectedItem.Text != "---ALL---")
                    RQParams.City = ddlAddCity.SelectedItem.Text;
                res = AccSvc.SearchHotels(RQParams);
            }

            if (ret == "")
            {
                if (res != null)
                {
                    //if (txtAddCityPlaceId.Text.Length != 0)
                    //{
                    //    if (res.Count > 0)
                    //    {
                    //        ret = "placeid";
                    //    }
                    //}
                    //else
                    //{
                    foreach (MDMSVC.DC_Accomodation_Search_RS rs in res)
                    {
                        if (rs.Country.ToUpper() == ddlAddCountry.SelectedItem.Text.ToUpper())
                        {
                            if (rs.City.ToUpper() == ddlAddCity.SelectedItem.Text.ToUpper())
                            {
                                if (!string.IsNullOrWhiteSpace(rs.PostalCode))
                                {
                                    if (rs.PostalCode.ToUpper() == txtPostalCode.Text.ToUpper())
                                    {
                                        if (rs.HotelName.TrimStart().TrimEnd().ToUpper().Replace("HOTEL", "").Replace("'", "").Replace("-", "").Replace(" ", "") == txtHotelName.Text.TrimStart().TrimEnd().ToUpper().Replace("HOTEL", "").Replace("'", "").Replace("-", "").Replace(" ", ""))
                                        {
                                            ret = "names";
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (rs.HotelName.TrimStart().TrimEnd().ToUpper().Replace("HOTEL", "").Replace("'", "").Replace("-", "").Replace(" ", "") == txtHotelName.Text.TrimStart().TrimEnd().ToUpper().Replace("HOTEL", "").Replace("'", "").Replace("-", "").Replace(" ", ""))
                                    {
                                        ret = "names";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //}
                }
            }
            return ret;
        }

        public void fillHotelSearchGrid(int pageIndex, int pageSize)
        {
            grdSearchResults.DataSource = res;
            if (res != null)
            {
                grdSearchResults.VirtualItemCount = res[0].TotalRecords;
            }
            grdSearchResults.PageIndex = pageIndex;
            grdSearchResults.PageSize = pageSize;
            grdSearchResults.DataBind();
        }

        protected void gvGridExist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                string myHotelName = "";
                string myCountryName = "";
                string myCityName = "";
                Guid myRow_Id = Guid.Parse(gvGridExist.DataKeys[index].Values[0].ToString());
                if (e.CommandName == "Select")
                {
                    myCountryName = gvGridExist.Rows[index].Cells[1].Text;
                    myCityName = gvGridExist.Rows[index].Cells[2].Text;
                    myHotelName = gvGridExist.Rows[index].Cells[3].Text;
                    MDMSVC.DC_Accomodation OverviewData = new MDMSVC.DC_Accomodation();
                    OverviewData.Accommodation_Id = myRow_Id;
                    OverviewData.ProductCategory = "Accommodation";
                    if (!string.IsNullOrEmpty(txtAddCityPlaceId.Text))
                        OverviewData.Google_Place_Id = txtAddCityPlaceId.Text.ToString();
                    if (!string.IsNullOrEmpty(hdnLat.Value))
                        OverviewData.Latitude = hdnLat.Value.ToString();
                    if (!string.IsNullOrEmpty(hdnLng.Value))
                        OverviewData.Longitude = hdnLng.Value.ToString();
                    OverviewData.Edit_Date = DateTime.Now;
                    OverviewData.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    OverviewData.IsActive = true;
                    if (AccSvc.UpdateHotelGeoDetail(OverviewData))
                    {
                        DropDownList ddlSystemCountryName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCountryName");
                        DropDownList ddlSystemCityName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCityName");
                        DropDownList ddlSystemProductName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemProductName");
                        TextBox txtSystemProductCode = (TextBox)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("txtSystemProductCode");
                        Button btnAddProduct = (Button)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("btnAddProduct");


                        ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByText(myCountryName));
                        ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillcities(ddlSystemCityName, ddlSystemCountryName);
                        //ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByText(myCityName));
                        ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(myCityName, StringComparison.InvariantCultureIgnoreCase)));

                        ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);
                        ddlSystemProductName.SelectedIndex = ddlSystemProductName.Items.IndexOf(ddlSystemProductName.Items.FindByText(myHotelName));
                        txtSystemProductCode.Text = masterdata.GetCodeById("product", Guid.Parse(ddlSystemProductName.SelectedItem.Value));
                        btnAddProduct.Visible = false;
                        this.Parent.Visible = false;
                        gvGridExist.DataSource = null;
                        gvGridExist.DataBind();
                    }
                }
                else if (e.CommandName == "OpenDeactivate")
                {
                    LinkButton btnDeactivate = (LinkButton)row.Cells[7].Controls[1];
                    DropDownList ddlExistingHotels = (DropDownList)row.Cells[7].Controls[3];
                    TextBox txtDeactiveRemark = (TextBox)row.Cells[7].Controls[5];
                    LinkButton btnProceedDeactivate = (LinkButton)row.Cells[7].Controls[7];
                    LinkButton btnCancel = (LinkButton)row.Cells[7].Controls[9];

                    if (btnDeactivate != null)
                        btnDeactivate.Visible = false;
                    if (ddlExistingHotels != null)
                        ddlExistingHotels.Visible = true;
                    if (txtDeactiveRemark != null)
                        txtDeactiveRemark.Visible = true;
                    if (btnProceedDeactivate != null)
                        btnProceedDeactivate.Visible = true;
                    if (btnCancel != null)
                        btnCancel.Visible = true;
                }
                else if (e.CommandName == "CancelDeactivate")
                {
                    LinkButton btnDeactivate = (LinkButton)row.Cells[7].Controls[1];
                    DropDownList ddlExistingHotels = (DropDownList)row.Cells[7].Controls[3];
                    TextBox txtDeactiveRemark = (TextBox)row.Cells[7].Controls[5];
                    LinkButton btnProceedDeactivate = (LinkButton)row.Cells[7].Controls[7];
                    LinkButton btnCancel = (LinkButton)row.Cells[7].Controls[9];

                    if (btnDeactivate != null)
                        btnDeactivate.Visible = true;
                    if (ddlExistingHotels != null)
                        ddlExistingHotels.Visible = false;
                    if (txtDeactiveRemark != null)
                        txtDeactiveRemark.Visible = false;
                    if (btnProceedDeactivate != null)
                        btnProceedDeactivate.Visible = false;
                    if (btnCancel != null)
                        btnCancel.Visible = false;
                }
                else if (e.CommandName == "ProceedDeactivate")
                {
                    LinkButton btnDeactivate = (LinkButton)row.Cells[7].Controls[1];
                    DropDownList ddlExistingHotels = (DropDownList)row.Cells[7].Controls[3];
                    TextBox txtDeactiveRemark = (TextBox)row.Cells[7].Controls[5];
                    LinkButton btnProceedDeactivate = (LinkButton)row.Cells[7].Controls[7];
                    LinkButton btnCancel = (LinkButton)row.Cells[7].Controls[9];

                    if (ddlExistingHotels.SelectedItem.Value == "0")
                    {
                        Guid NewHotel_Id = Guid.NewGuid();
                        ShiftMapping(myRow_Id, NewHotel_Id, txtDeactiveRemark.Text);
                        if (AddHotel(NewHotel_Id)) { if (DeactivateHotel(myRow_Id, txtDeactiveRemark.Text.Trim())) { } }

                    }
                    else
                    {
                        ShiftMapping(myRow_Id, Guid.Parse(ddlExistingHotels.SelectedItem.Value), txtDeactiveRemark.Text.Trim());
                        if (DeactivateHotel(myRow_Id, txtDeactiveRemark.Text.Trim())) { loadExistingProductGrid(); }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public bool DeactivateHotel(Guid Acco_Id, string Remark)
        {
            bool ret = false;
            MDMSVC.DC_Accomodation_UpdateStatus_RQ obj = new MDMSVC.DC_Accomodation_UpdateStatus_RQ();
            obj.Accommodation_Id = Acco_Id;
            obj.Status = false;
            obj.Edit_Date = DateTime.Now;
            obj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
            if (AccSvc.UpdateHotelDetailStatus(obj))
            {
                MDMSVC.DC_Accommodation_Status AS = new MDMSVC.DC_Accommodation_Status();
                AS.Accommodation_Status_Id = Guid.NewGuid();
                AS.Accommodation_Id = Acco_Id;
                AS.CompanyMarket = "All";
                AS.DeactivationReason = Remark;
                AS.From = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                AS.To = DateTime.ParseExact("31/12/2099", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                AS.Status = "INACTIVE";
                AS.IsActive = true;
                AS.Edit_Date = DateTime.Now;
                AS.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

                if (AccSvc.AddHotelStatus(AS))
                { ret = true; }
            }

            return ret;
        }
        public void ShiftMapping(Guid From, Guid To, string Remark)
        {
            MDMSVC.DC_Mapping_ShiftMapping_RQ obj = new MDMSVC.DC_Mapping_ShiftMapping_RQ();
            obj.Accommodation_From_Id = From;
            obj.Accommodation_To_Id = To;
            obj.Edit_Date = DateTime.Now;
            obj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
            obj.Remarks = Remark;
            if (mapperSvc.ShiftAccommodationMappings(obj))
            {

            }
        }

        public void UpdateHotel()
        {
            //bool ret = false;

            MDMSVC.DC_Accomodation OverviewData = new MDMSVC.DC_Accomodation();
            OverviewData.Accommodation_Id = new Guid(res[0].AccomodationId);
            OverviewData.ProductCategory = "Accommodation";
            if (!string.IsNullOrEmpty(txtAddCityPlaceId.Text))
                OverviewData.Google_Place_Id = txtAddCityPlaceId.Text.ToString();
            if (!string.IsNullOrEmpty(hdnLat.Value))
                OverviewData.Latitude = hdnLat.Value.ToString();
            if (!string.IsNullOrEmpty(hdnLng.Value))
                OverviewData.Longitude = hdnLng.Value.ToString();
            OverviewData.Edit_Date = DateTime.Now;
            OverviewData.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
            OverviewData.IsActive = true;
            if (AccSvc.UpdateHotelGeoDetail(OverviewData))
            {
                //ret = true;
            }
            DropDownList ddlSystemCountryName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCityName");
            DropDownList ddlSystemProductName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemProductName");
            TextBox txtSystemProductCode = (TextBox)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("txtSystemProductCode");
            Button btnAddProduct = (Button)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("btnAddProduct");

            ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByValue(ddlAddCountry.SelectedItem.Value));
            ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillcities(ddlSystemCityName, ddlSystemCountryName);
            ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(ddlAddCity.SelectedItem.Value));

            ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);

            if (res.Count == 1)
            {
                ddlSystemProductName.SelectedIndex = ddlSystemProductName.Items.IndexOf(ddlSystemProductName.Items.FindByText(res[0].HotelName.ToString()));
                txtSystemProductCode.Text = res[0].CompanyHotelId;
            }
            btnAddProduct.Visible = false;
            this.Parent.Visible = false;

            // return ret;
        }

        public bool AddHotel(Guid NewHotel_Id)
        {
            bool ret = false;
            MDMSVC.DC_Accomodation OverviewData = new MDMSVC.DC_Accomodation();

            if (NewHotel_Id == Guid.Empty)
                NewHotel_Id = Guid.NewGuid();
            OverviewData.Accommodation_Id = NewHotel_Id;
            string redirectURL = "~/hotels/manage.aspx?Hotel_Id=" + OverviewData.Accommodation_Id;
            OverviewData.ProductCategory = "Accommodation";
            if (ddlAddProductCategorySubType.SelectedIndex != 0)
                OverviewData.ProductCategorySubType = ddlAddProductCategorySubType.SelectedItem.Text;
            if (ddlAddCountry.SelectedIndex != 0)
                OverviewData.Country = ddlAddCountry.SelectedItem.Text;
            if (ddlAddState.SelectedIndex != 0)
                OverviewData.State_Name = ddlAddState.SelectedItem.Text;
            if (ddlAddCity.SelectedIndex != 0)
                OverviewData.City = ddlAddCity.SelectedItem.Text;
            if (ddlStarRating.SelectedIndex != 0)
                OverviewData.HotelRating = ddlStarRating.SelectedItem.Text;

            if (!string.IsNullOrEmpty(txtCheckinTime.Text))
                OverviewData.CheckInTime = txtCheckinTime.Text.ToString();
            if (!string.IsNullOrEmpty(txtCheckOut.Text))
                OverviewData.CheckOutTime = txtCheckOut.Text.ToString();
            if (!string.IsNullOrEmpty(txtHotelName.Text))
                OverviewData.HotelName = txtHotelName.Text.ToString();
            if (!string.IsNullOrEmpty(txtPostalCode.Text))
                OverviewData.PostalCode = txtPostalCode.Text.ToString();
            if (!string.IsNullOrEmpty(txtStreet.Text))
                OverviewData.StreetName = txtStreet.Text.ToString();
            if (!string.IsNullOrEmpty(txtStreet2.Text))
                OverviewData.StreetNumber = txtStreet2.Text.ToString();
            if (!string.IsNullOrEmpty(txtAddCityPlaceId.Text))
                OverviewData.Google_Place_Id = txtAddCityPlaceId.Text.ToString();
            if (!string.IsNullOrEmpty(hdnLat.Value))
                OverviewData.Latitude = hdnLat.Value.ToString();
            if (!string.IsNullOrEmpty(hdnLng.Value))
                OverviewData.Longitude = hdnLng.Value.ToString();
            OverviewData.Create_Date = DateTime.Now;
            OverviewData.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
            OverviewData.IsActive = true;
            OverviewData.InsertFrom = InsertFrom;

            if (AccSvc.AddHotelDetail(OverviewData))
            {
                ret = true;
                if (ParentPageName == "searchAccoMapping.ascx" || ParentPageName == "search.aspx")
                {
                    DropDownList ddlSystemCountryName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCountryName");
                    DropDownList ddlSystemCityName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCityName");
                    DropDownList ddlSystemProductName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemProductName");
                    TextBox txtSystemProductCode = (TextBox)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("txtSystemProductCode");
                    Button btnAddProduct = (Button)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("btnAddProduct");

                    ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByValue(ddlAddCountry.SelectedItem.Value));
                    ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillcities(ddlSystemCityName, ddlSystemCountryName);
                    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(ddlAddCity.SelectedItem.Value));

                    ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);
                    ddlSystemProductName.SelectedIndex = ddlSystemProductName.Items.IndexOf(ddlSystemProductName.Items.FindByText(txtHotelName.Text.ToString()));
                    txtSystemProductCode.Text = masterdata.GetCodeById("product", Guid.Parse(ddlSystemProductName.SelectedItem.Value));
                    btnAddProduct.Visible = false;
                    this.Parent.Visible = false;
                }
                else
                    Response.Redirect(redirectURL);
            }
            return ret;
        }

        public void loadExistingProductGrid()
        {
            bool showgrid = false;
            string ret = CheckForDuplicate();
            if (ret != "")
            {
                dvGridExist.Visible = false;
                if (ParentPageName == "searchAccoMapping.ascx" || ParentPageName == "search.aspx")
                {
                    if (ret == "placeid")
                    {
                        showgrid = true;
                    }
                    else
                    {
                        if ((res != null) && (res.Count > 0))
                        {
                            showgrid = true;
                        }
                    }
                    if (showgrid)
                    {
                        dvGridExist.Visible = true;
                        gvGridExist.DataSource = res;
                        if (res != null)
                        {
                            gvGridExist.VirtualItemCount = res[0].TotalRecords;
                        }
                        gvGridExist.PageIndex = 0;
                        gvGridExist.PageSize = int.MaxValue;
                        gvGridExist.DataBind();
                    }
                }
                else
                {
                    //fillHotelSearchGrid(0, 5);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
            }
        }

        protected void btnMapLookup_Command(object sender, CommandEventArgs e)
        {
            loadExistingProductGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "map", "callmap();", true);
        }
        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            bool showgrid = false;
            bool IsToAdd = false;
            string ret = CheckForDuplicate();
            if (ret != "")
            {
                dvGridExist.Visible = false;
                if (ParentPageName == "searchAccoMapping.ascx" || ParentPageName == "search.aspx")
                {
                    //if (ret == "placeid" || ((res != null) && (res.Count == 1)))
                    //{
                    //    DropDownList ddlSystemCountryName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCountryName");
                    //    DropDownList ddlSystemCityName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemCityName");
                    //    DropDownList ddlSystemProductName = (DropDownList)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("ddlSystemProductName");
                    //    TextBox txtSystemProductCode = (TextBox)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("txtSystemProductCode");
                    //    Button btnAddProduct = (Button)((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).frmEditProductMap.FindControl("btnAddProduct");

                    //    ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByValue(ddlAddCountry.SelectedItem.Value));
                    //    ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillcities(ddlSystemCityName, ddlSystemCountryName);
                    //    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(ddlAddCity.SelectedItem.Value));

                    //    ((TLGX_Consumer.controls.staticdata.AccoMap)this.Parent.NamingContainer).fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);

                    //    if (res.Count == 1)
                    //    {
                    //        ddlSystemProductName.SelectedIndex = ddlSystemProductName.Items.IndexOf(ddlSystemProductName.Items.FindByText(res[0].HotelName.ToString()));
                    //        txtSystemProductCode.Text = res[0].CompanyHotelId;
                    //    }
                    //    btnAddProduct.Visible = false;
                    //    this.Parent.Visible = false;
                    //}
                    //else
                    //{
                    if ((res != null) && (res.Count > 0))
                        showgrid = true;
                    else
                        IsToAdd = true;
                    //}
                    if (showgrid)
                    {
                        dvGridExist.Visible = true;
                        gvGridExist.DataSource = res;
                        if (res != null)
                        {
                            gvGridExist.VirtualItemCount = res[0].TotalRecords;
                        }
                        gvGridExist.PageIndex = 0;
                        gvGridExist.PageSize = int.MaxValue;
                        gvGridExist.DataBind();
                    }
                    else
                    {
                        if (IsToAdd)
                        {
                            UpdateHotel();
                        }
                    }
                }
                else
                {
                    fillHotelSearchGrid(0, 5);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
            }
            else
            {
                Guid NewHotel_Id = Guid.NewGuid();
                if (AddHotel(NewHotel_Id))
                {

                }
            }
        }

        protected void ddlAddCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAddCountry.SelectedValue != "")
            {
                fillStatedropdown(ddlAddCountry.SelectedValue);
            }
        }

        protected void ddlAddState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlAddState.SelectedIndex==0)
            {
                ddlAddCity.Items.Clear();
                fillcitydropdown(ddlAddCountry.SelectedValue);
            }
            else
            {
                Guid ddlstate_id = Guid.Parse(ddlAddState.SelectedValue);
                var result = _objMasterData.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() {State_Id=ddlstate_id });

                if (result != null)
                {
                    ddlAddCity.Items.Clear();

                    ddlAddCity.DataSource = result;
                    ddlAddCity.DataValueField = "City_Id";
                    ddlAddCity.DataTextField = "Name";
                    ddlAddCity.DataBind();
                    ddlAddCity.Items.Insert(0, new ListItem("--ALL--", ""));
                }
                else
                {
                    fillcitydropdown(ddlAddCountry.SelectedValue);
                }
            }
        }
        protected void btnAddNew_Command1(object sender, CommandEventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlAddProductCategorySubType.SelectedIndex = 0;
            ddlAddCountry.SelectedIndex = 0;
            ddlAddCity.SelectedIndex = 0;
            ddlStarRating.SelectedIndex = 0;
            grdSearchResults.DataSource = null;
            grdSearchResults.DataBind();
            ddlAddState.SelectedIndex = 0;
        }

        protected void gvGridExist_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            foreach (GridViewRow row in myGridView.Rows)
            {
                LinkButton btnDeactivate = (LinkButton)row.Cells[7].Controls[1];
                DropDownList ddlExistingHotels = (DropDownList)row.Cells[7].Controls[3];
                TextBox txtDeactiveRemark = (TextBox)row.Cells[7].Controls[5];
                LinkButton btnProceedDeactivate = (LinkButton)row.Cells[7].Controls[7];
                LinkButton btnCancel = (LinkButton)row.Cells[7].Controls[9];

                if (btnDeactivate != null)
                    btnDeactivate.Visible = true;
                if (ddlExistingHotels != null)
                {
                    ddlExistingHotels.Visible = false;
                    ddlExistingHotels.DataSource = res;
                    ddlExistingHotels.DataValueField = "AccomodationId";
                    ddlExistingHotels.DataTextField = "HotelNameWithCode";
                    ddlExistingHotels.DataBind();
                }
                if (txtDeactiveRemark != null)
                    txtDeactiveRemark.Visible = false;
                if (btnProceedDeactivate != null)
                    btnProceedDeactivate.Visible = false;
                if (btnCancel != null)
                    btnCancel.Visible = false;
            }

        }
    }
}