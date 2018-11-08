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
using TLGX_Consumer.App_Code;
using System.Text;

namespace TLGX_Consumer.controls.activity
{
    public partial class SearchActivityMasterControl : System.Web.UI.UserControl
    {
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        Controller.ActivitySVC activitySVC = new ActivitySVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMasters();
                //Bind Page Controls
                if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri.Contains("ManageActivityFlavour"))
                {
                    SetControls();
                }

            }
        }
        private void SetControls()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.UrlReferrer.AbsoluteUri.Contains("ManageActivityFlavour"))))
                {
                    #region Get Value from query string
                    string ProductName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ProdN"]);
                    string CountryName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CoN"]);
                    string CountryID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CoID"]);
                    string CityName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CiN"]);
                    string CityID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CiID"]);
                    string SupplierID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["SuID"]);
                    string ActivityFlavourStatus = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["AFS"]);
                    string ProductCategorySubType = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PCST"]);
                    string ProductCategorySubTypeID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PCSTID"]);
                    string ProductType = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PT"]);
                    string ProductTypeID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PTID"]);
                    string ProductSubType = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PST"]);
                    string ProductSubTypeID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PSTID"]);

                    string SupplierProductSubType = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["SPST"]);
                    string SupplierCountryName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["SCoN"]);
                    string SupplierCityName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["SCiN"]);

                    string NoOperatingSchedule = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["NOS"]);
                    string NoPhysicalIntensity = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["NPI"]);
                    string NoSession = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["NSe"]);
                    string NoSpecial = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["NSp"]);
                    string NoSuitableFor = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["NSF"]);
                    string PageNo = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PN"]);
                    string PageSize = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PS"]);
                    //IType
                    string OnlyMedia = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["OMed"]);
                    string InterestType = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["IT"]);
                    string InterestTypeID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ITID"]);


                    #endregion
                    int pageno = 0;
                    int pagesize = 5;

                    if (!string.IsNullOrWhiteSpace(ProductName))
                        txtProductName.Text = ProductName;
                    if (!string.IsNullOrWhiteSpace(CountryName))
                    {
                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(CountryName));
                        if (!string.IsNullOrWhiteSpace(CountryID))
                            ClearAndFillCity(CountryID);
                    }
                    if (!string.IsNullOrWhiteSpace(CityName))
                        ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(CityName));


                    if (!string.IsNullOrWhiteSpace(SupplierID))
                    {
                        ddlSupplier.SelectedIndex = ddlSupplier.Items.IndexOf(ddlSupplier.Items.FindByValue(SupplierID));
                        FillSupplierProductsubType(ddlSupplier.SelectedValue);

                    }

                    if (!string.IsNullOrWhiteSpace(ActivityFlavourStatus))
                        ddlActivityFlavourStatus.SelectedIndex = ddlActivityFlavourStatus.Items.IndexOf(ddlActivityFlavourStatus.Items.FindByText(ActivityFlavourStatus));

                    if (!string.IsNullOrWhiteSpace(ProductCategorySubType))
                    {
                        ddlProductCategorySubType.SelectedIndex = ddlProductCategorySubType.Items.IndexOf(ddlProductCategorySubType.Items.FindByText(ProductCategorySubType));
                        if (!string.IsNullOrWhiteSpace(ProductCategorySubTypeID))
                            ClearFillProductType(ProductCategorySubTypeID);

                    }
                    if (!string.IsNullOrWhiteSpace(ProductType))
                    {
                        ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText(ProductType));
                        if (!string.IsNullOrWhiteSpace(ProductTypeID))
                            ClearFillProductSubType(ddlProductType.SelectedValue);
                    }
                    if (!string.IsNullOrWhiteSpace(ProductSubType))
                    {
                        ddlProductSubType.SelectedIndex = ddlProductSubType.Items.IndexOf(ddlProductSubType.Items.FindByText(ProductSubType));
                    }

                    if (!string.IsNullOrWhiteSpace(SupplierCountryName))
                        txtSupplierCountryName.Text = SupplierCountryName;

                    if (!string.IsNullOrWhiteSpace(SupplierCityName))
                        txtSupplierCityName.Text = SupplierCityName;



                    if (!string.IsNullOrWhiteSpace(SupplierProductSubType))
                    {
                        ddlSupplierProductSupType.SelectedIndex = ddlSupplierProductSupType.Items.IndexOf(ddlSupplierProductSupType.Items.FindByValue(SupplierProductSubType));
                    }


                    if (!string.IsNullOrWhiteSpace(NoOperatingSchedule))
                        chkNoOperatingSchedule.Checked = Convert.ToBoolean(NoOperatingSchedule);
                    if (!string.IsNullOrWhiteSpace(NoPhysicalIntensity))
                        chkNoPhysicalIntensity.Checked = Convert.ToBoolean(NoPhysicalIntensity);
                    if (!string.IsNullOrWhiteSpace(NoSession))
                        chkNoDuration.Checked = Convert.ToBoolean(NoSession);
                    if (!string.IsNullOrWhiteSpace(NoSpecial))
                        chkNoSpecial.Checked = Convert.ToBoolean(NoSpecial);
                    if (!string.IsNullOrWhiteSpace(NoSuitableFor))
                        chkNoSuitableFor.Checked = Convert.ToBoolean(NoSuitableFor);
                    //IType
                    if (!string.IsNullOrWhiteSpace(OnlyMedia))
                        chkNoSuitableFor.Checked = Convert.ToBoolean(OnlyMedia);
                    
                    if (!string.IsNullOrWhiteSpace(InterestType))
                    {
                        ddlInterestType.SelectedIndex = ddlInterestType.Items.IndexOf(ddlInterestType.Items.FindByText(InterestType));
                        if (!string.IsNullOrWhiteSpace(InterestTypeID))
                            fillproductcategorysubtype(ddlProductCategorySubType, InterestTypeID);
                    }
                    //end

                    if (!string.IsNullOrWhiteSpace(PageNo))
                        pageno = Convert.ToInt32(PageNo);
                    if (!string.IsNullOrWhiteSpace(PageSize))
                        pagesize = Convert.ToInt32(PageSize);

                    searchActivityMaster(pageno, pagesize);


                }
            }
            catch
            {

                throw;
            }
        }
        private void LoadMasters()
        {
            fillcoutries();
            fillInterestTypeData();
            //fillproductcategorysubtype(ddlProductCategorySubType);
            fillSupplierList(ddlSupplier);
            fillActivityFlavourStatusMaster(ddlActivityFlavourStatus);
            //fillstatusdropdown(ddlStatus);
        }
        private void fillcoutries()
        {
            ddlCountry.Items.Clear();
            var countryList = masterSVc.GetAllCountries();
            ddlCountry.DataSource = countryList;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            InsertDefaultValuesInDDL(ddlCountry);
        }
        private void fillcities(Guid Country_Id)
        {
            var CityList = masterSVc.GetMasterCityData(Country_Id.ToString());
            if (CityList != null)
            {
                if (CityList.Count() > 0)
                {
                    ddlCity.DataSource = CityList;
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataValueField = "City_Id";
                    ddlCity.DataBind();
                }
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAndFillCity(ddlCountry.SelectedValue);
        }
        public void ClearAndFillCity(string SelectedValue)
        {
            Guid CountryId;
            ddlCity.Items.Clear();
            if (Guid.TryParse(SelectedValue, out CountryId))
            {
                fillcities(CountryId);
            }
            InsertDefaultValuesInDDL(ddlCity);
            ddlCity.Focus();
        }
        private void fillproductcategorysubtype(DropDownList ddl, string SelectedVal)
        {
            try
            {
                Guid gSelectedVal;
                ddl.Items.Clear();
                var result = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
                if (Guid.TryParse(SelectedVal, out gSelectedVal))
                {
                    ddl.DataSource = result.Where(w => w.ParentAttributeValue_Id == gSelectedVal).Select(s => s);
                    ddl.DataTextField = "AttributeValue";
                    ddl.DataValueField = "MasterAttributeValue_Id";
                }
                else
                    ddl.DataSource =null;
                ddl.DataBind();
                InsertDefaultValuesInDDL(ddl);
            }
            catch
            {
                throw;
            }
        }
        private void fillActivityFlavourStatusMaster(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Status").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
            }
            catch
            {
                throw;
            }
        }
        protected void ddlProductCategorySubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearFillProductType(ddlProductCategorySubType.SelectedValue);
        }
        public void ClearFillProductType(string SelectedValue)
        {
            Guid gSubCategory;
            ddlProductType.Items.Clear();
            ddlProductSubType.Items.Clear();

            if (Guid.TryParse(SelectedValue, out gSubCategory))
            {
                fillProductType(ddlProductType, gSubCategory);

                InsertDefaultValuesInDDL(ddlProductSubType);
            }
            else
            {
                InsertDefaultValuesInDDL(ddlProductType);

                InsertDefaultValuesInDDL(ddlProductSubType);
            }
        }
        private void fillProductType(DropDownList ddl, Guid SubCategory)
        {
            ddl.Items.Clear();
            var result = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            ddl.DataSource = result.Where(w => w.ParentAttributeValue_Id == SubCategory).Select(s => s);
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            InsertDefaultValuesInDDL(ddl);
        }
        private void fillproductsubtype(DropDownList ddl, Guid ProductType)
        {
            ddl.Items.Clear();
            var result = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductSubType").MasterAttributeValues;
            ddl.DataSource = result.Where(w => w.ParentAttributeValue_Id == ProductType).Select(s => s);
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            InsertDefaultValuesInDDL(ddl);
        }
        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearFillProductSubType(ddlProductType.SelectedValue);
        }
        public void ClearFillProductSubType(string selectedValue)
        {
            Guid gProductType;
            ddlProductSubType.Items.Clear();

            if (Guid.TryParse(selectedValue, out gProductType))
            {
                fillproductsubtype(ddlProductSubType, gProductType);
            }
            else
            {
                InsertDefaultValuesInDDL(ddlProductSubType);
            }
        }
        //private void fillstatusdropdown(DropDownList ddl)
        //{
        //    ddl.Items.Clear();
        //    ddl.DataSource = masterSVc.GetAllStatuses();
        //    ddl.DataTextField = "Status_Name";
        //    ddl.DataValueField = "Status_Short";
        //    ddl.DataBind();
        //    ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
        //    ddl.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText("ACTIVE"));
        //}
        private void fillSupplierList(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = masterSVc.GetSupplierByEntity(new MDMSVC.DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue, EntityType="Activities" });
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Supplier_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
        }
        private void searchActivityMaster(int pageindex, int pagesize)
        {
            try
            {
                MDMSVC.DC_Activity_Flavour_RQ _objSearch = new MDMSVC.DC_Activity_Flavour_RQ();

                _objSearch.PageNo = pageindex;
                _objSearch.PageSize = pagesize;

                if (ddlCountry.SelectedIndex > 1)
                {
                    _objSearch.Country_Id = Guid.Parse(ddlCountry.SelectedValue);
                    _objSearch.Country = ddlCountry.SelectedItem.Text;
                }
                else
                {
                    if (ddlCountry.SelectedItem.Text.Contains("UNMAPPED"))
                        _objSearch.Country = ddlCountry.SelectedItem.Text;
                }

                if (ddlCity.SelectedIndex > 1)
                {
                    _objSearch.City_Id = Guid.Parse(ddlCity.SelectedValue);
                    _objSearch.City = ddlCity.SelectedItem.Text;
                }
                else
                {
                    if (ddlCity.SelectedItem.Text.Contains("UNMAPPED"))
                        _objSearch.City = ddlCity.SelectedItem.Text;
                }

                if (ddlSupplier.SelectedIndex != 0)
                    _objSearch.Supplier_Id = Guid.Parse(ddlSupplier.SelectedValue);

                if (ddlActivityFlavourStatus.SelectedValue != "0")
                    _objSearch.Activity_Status = ddlActivityFlavourStatus.SelectedItem.Text;

                if (!string.IsNullOrWhiteSpace(txtProductName.Text))
                    _objSearch.ProductName = txtProductName.Text;
                //IType
                _objSearch.InterestType = ddlInterestType.SelectedItem.Text;
                if (ddlInterestType.SelectedIndex > 1)
                {
                    _objSearch.InterestTypeId = Guid.Parse(ddlInterestType.SelectedValue);
                }

                _objSearch.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text;
                if (ddlProductCategorySubType.SelectedIndex > 1)
                {
                    _objSearch.ProductCategorySubTypeId = Guid.Parse(ddlProductCategorySubType.SelectedValue);
                }

                _objSearch.ProductType = ddlProductType.SelectedItem.Text;
                if (ddlProductType.SelectedIndex > 1)
                {
                    _objSearch.ProductTypeId = Guid.Parse(ddlProductType.SelectedValue);
                }

                _objSearch.ProductNameSubType = ddlProductSubType.SelectedItem.Text;
                if (ddlProductSubType.SelectedIndex > 1)
                {
                    _objSearch.ProductNameSubTypeId = Guid.Parse(ddlProductSubType.SelectedValue);
                }
                
                if (ddlSupplierProductSupType.SelectedIndex > 0)
                {
                    _objSearch.SupplierProductNameSubType = ddlSupplierProductSupType.SelectedValue;
                }
                if (!string.IsNullOrWhiteSpace(txtSupplierCountryName.Text))
                    _objSearch.SupplierCountryName = txtSupplierCountryName.Text;
                if (!string.IsNullOrWhiteSpace(txtSupplierCityName.Text))
                    _objSearch.SupplierCityName = txtSupplierCityName.Text;

                _objSearch.NoOpsSchedule = chkNoOperatingSchedule.Checked;
                _objSearch.NoPhysicalIntensity = chkNoPhysicalIntensity.Checked;
                _objSearch.NoSession = chkNoDuration.Checked;
                _objSearch.NoSpecials = chkNoSpecial.Checked;
                _objSearch.NoSuitableFor = chkNoSuitableFor.Checked;
                _objSearch.OnlyMedia = chkOnlyMedia.Checked;

                var res = activitySVC.GetActivityFlavour(_objSearch);
                if (res != null && res.Count != 0)
                {
                    if (res.Count > 0)
                    {
                        gvActivitySearch.VirtualItemCount = Convert.ToInt32(res[0].TotalRecords);
                        lblTotalRecords.Text = res[0].TotalRecords.ToString();
                        txtGoToPageNo.Text = Convert.ToString(pageindex + 1);
                        //int pagecount = 0;
                        //if (Convert.ToInt32(res[0].TotalRecords) <= pagesize)
                        //{
                        //    pagecount = 1;
                        //}
                        //else
                        //{
                        //    pagecount = (Convert.ToInt32(res[0].TotalRecords) % pagesize) > 0 ? (Convert.ToInt32(res[0].TotalRecords) % pagesize) + 1 : (Convert.ToInt32(res[0].TotalRecords) % pagesize);
                        //}
                        gvActivitySearch.DataSource = res;
                        gvActivitySearch.PageIndex = pageindex;   //(pageindex > pagecount ? pagecount : pageindex);
                        gvActivitySearch.PageSize = pagesize;
                        gvActivitySearch.DataBind();
                        lblTotalPageCount.Text = Convert.ToString(gvActivitySearch.PageCount);
                    }
                }
                else
                {
                    gvActivitySearch.DataSource = null;
                    gvActivitySearch.DataBind();
                    lblTotalRecords.Text = string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }
        private void resetControls()
        {
            chkNoSuitableFor.Checked = false;
            chkNoPhysicalIntensity.Checked = false;
            chkNoOperatingSchedule.Checked = false;
            chkNoDuration.Checked = false;
            chkNoSpecial.Checked = false;
            //IType
            chkOnlyMedia.Checked = false;
            ddlInterestType.SelectedIndex = 0;
            ddlProductCategorySubType.Items.Clear();
            InsertDefaultValuesInDDL(ddlProductCategorySubType);
            //end
            ddlCountry.SelectedIndex = 0;

            ddlCity.Items.Clear();
            InsertDefaultValuesInDDL(ddlCity);

            ddlProductType.Items.Clear();
            InsertDefaultValuesInDDL(ddlProductType);

            ddlProductSubType.Items.Clear();
            InsertDefaultValuesInDDL(ddlProductSubType);

            //ddlStatus.SelectedIndex = 0;
            ddlActivityFlavourStatus.SelectedIndex = 0;

            ddlSupplier.SelectedIndex = 0;

            ddlPageSize.SelectedIndex = 0;

            txtProductName.Text = string.Empty;
            txtSupplierCityName.Text = string.Empty;
            txtSupplierCountryName.Text = string.Empty;

            FillSupplierProductsubType("0");

            lblTotalRecords.Text = string.Empty;
            gvActivitySearch.DataSource = null;
            gvActivitySearch.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchActivityMaster(0, Convert.ToInt32(ddlPageSize.SelectedValue));
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetControls();
        }
        protected void gvActivitySearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            searchActivityMaster(e.NewPageIndex, Convert.ToInt32(ddlPageSize.SelectedValue));
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchActivityMaster(0, Convert.ToInt32(ddlPageSize.SelectedValue));
        }
        private void InsertDefaultValuesInDDL(DropDownList ddl)
        {
            ddl.Items.Insert(0, new ListItem("-ALL UNMAPPED-", "1"));
            ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
        }
        protected void gvActivitySearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Get Hyperlink 
                //System.Web.UI.Control hlf = e.Row.Cells[10];

                if (e.Row.Cells[9].Text.ToUpper() == "REVIEW COMPLETED")
                {
                    e.Row.CssClass = "alert alert-success";
                }
                else if (e.Row.Cells[9].Text.ToUpper() == "UNDER REVIEW")
                {
                    e.Row.CssClass = "alert alert-warning";
                }
                else if (e.Row.Cells[9].Text.ToUpper() == "NOT YET REVIEWED")
                {
                    e.Row.CssClass = "alert alert-danger";
                }
            }

        }
        protected void gvActivitySearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    //Create Query string

                    string strQueryString = GetQueryString(myRow_Id.ToString(), ((GridView)sender).PageIndex.ToString());
                    Response.Redirect(strQueryString, true);
                    //End Here


                }
            }
            catch (Exception)
            {


                throw;
            }
        }

        public string GetQueryString(string myRow_Id, string strpageindex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("~/activity/ManageActivityFlavour.aspx?Activity_Flavour_Id=" + myRow_Id);
            if (!string.IsNullOrWhiteSpace(txtProductName.Text))
                sb.Append("&ProdN=" + HttpUtility.UrlEncode(txtProductName.Text));
            if (ddlCountry.SelectedIndex > 1)
            {
                sb.Append("&CoID=" + HttpUtility.UrlEncode(ddlCountry.SelectedValue));
                sb.Append("&CoN=" + HttpUtility.UrlEncode(ddlCountry.SelectedItem.Text));
            }
            else
            {
                if (ddlCountry.SelectedItem.Text.Contains("UNMAPPED"))
                    sb.Append("&CoN=" + HttpUtility.UrlEncode(ddlCountry.SelectedItem.Text));
            }

            if (ddlCity.SelectedIndex > 1)
            {
                sb.Append("&CiID=" + HttpUtility.UrlEncode(ddlCity.SelectedValue));
                sb.Append("&CiN=" + HttpUtility.UrlEncode(ddlCity.SelectedItem.Text));
            }
            else
            {
                if (ddlCity.SelectedItem.Text.Contains("UNMAPPED"))
                    sb.Append("&CiN=" + HttpUtility.UrlEncode(ddlCity.SelectedItem.Text));
            }

            if (ddlSupplier.SelectedIndex != 0)
                sb.Append("&SuID=" + HttpUtility.UrlEncode(ddlSupplier.SelectedValue));

            if (ddlActivityFlavourStatus.SelectedValue != "0")
                sb.Append("&AFS=" + HttpUtility.UrlEncode(ddlActivityFlavourStatus.SelectedItem.Text));

            sb.Append("&PCST=" + HttpUtility.UrlEncode(ddlProductCategorySubType.SelectedItem.Text));
            if (ddlProductCategorySubType.SelectedIndex > 1)
            {
                sb.Append("&PCSTID=" + HttpUtility.UrlEncode(ddlProductCategorySubType.SelectedValue));
            }

            sb.Append("&PT=" + HttpUtility.UrlEncode(ddlProductType.SelectedItem.Text));
            if (ddlProductType.SelectedIndex > 1)
            {
                sb.Append("&PTID=" + HttpUtility.UrlEncode(ddlProductType.SelectedValue));
            }
            sb.Append("&PST=" + HttpUtility.UrlEncode(ddlProductSubType.SelectedItem.Text));
            if (ddlProductSubType.SelectedIndex > 1)
            {
                sb.Append("&PSTID=" + HttpUtility.UrlEncode(ddlProductSubType.SelectedValue));
            }
            if(ddlSupplierProductSupType.SelectedIndex >  0)
            {
                sb.Append("&SPST=" + HttpUtility.UrlEncode(ddlSupplierProductSupType.SelectedValue)); 
            }
            if (!string.IsNullOrWhiteSpace(txtSupplierCountryName.Text))
                sb.Append("&SCoN=" + HttpUtility.UrlEncode(txtSupplierCountryName.Text));
            if (!string.IsNullOrWhiteSpace(txtSupplierCityName.Text))
                sb.Append("&SCiN=" + HttpUtility.UrlEncode(txtSupplierCityName.Text));

            sb.Append("&NOS=" + HttpUtility.UrlEncode(Convert.ToString(chkNoOperatingSchedule.Checked)));
            sb.Append("&NPI=" + HttpUtility.UrlEncode(Convert.ToString(chkNoPhysicalIntensity.Checked)));
            sb.Append("&NSe=" + HttpUtility.UrlEncode(Convert.ToString(chkNoDuration.Checked)));
            sb.Append("&NSp=" + HttpUtility.UrlEncode(Convert.ToString(chkNoSpecial.Checked)));
            sb.Append("&NSF=" + HttpUtility.UrlEncode(Convert.ToString(chkNoSuitableFor.Checked)));
            //IType
            sb.Append("&OMed=" + HttpUtility.UrlEncode(Convert.ToString(chkNoSuitableFor.Checked)));

            sb.Append("&IT=" + HttpUtility.UrlEncode(ddlInterestType.SelectedItem.Text));
            if (ddlInterestType.SelectedIndex > 1)
            {
                sb.Append("&ITID=" + HttpUtility.UrlEncode(ddlInterestType.SelectedValue));
            }


            string pageindex = strpageindex;
            sb.Append("&PN=" + HttpUtility.UrlEncode(pageindex));
            sb.Append("&PS=" + HttpUtility.UrlEncode(Convert.ToString(ddlPageSize.SelectedValue)));


            return sb.ToString();
        }

        protected void btnGoToPagNo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(lblTotalPageCount.Text))
                {
                    int intGoToPageNo = Convert.ToInt32(txtGoToPageNo.Text);
                    if (intGoToPageNo > 0)
                    {
                        if (intGoToPageNo <= Convert.ToInt32(lblTotalPageCount.Text))
                        {
                            int pageindex = Convert.ToInt32(txtGoToPageNo.Text) - 1;
                            searchActivityMaster(pageindex, Convert.ToInt32(ddlPageSize.SelectedValue));
                        }
                    }
                }
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSupplierProductsubType(ddlSupplier.SelectedValue);
        }
        public void FillSupplierProductsubType(string Supplier_Id)
        {
            ddlSupplierProductSupType.Items.Clear();
            if (Supplier_Id != "0")
            {
                var CategoryTypes = activitySVC.GetSupplierProductSubType(new MDMSVC.DC_Supplier_DDL { Supplier_Id = Guid.Parse(Supplier_Id) });
                ddlSupplierProductSupType.DataSource = CategoryTypes;
                ddlSupplierProductSupType.DataTextField = "SupProdSubType";
                ddlSupplierProductSupType.DataValueField = "SupProdSubTypeCode";
            }
            ddlSupplierProductSupType.DataBind();
            ddlSupplierProductSupType.Items.Insert(0, new ListItem("--All--", "0"));
        }
        //IType
        private void fillInterestTypeData()
        {
            try
            {
                ddlInterestType.Items.Clear();
                var result = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "InterestType").MasterAttributeValues;
                ddlInterestType.DataSource = result;
                ddlInterestType.DataTextField = "AttributeValue";
                ddlInterestType.DataValueField = "MasterAttributeValue_Id";
                ddlInterestType.DataBind();
                InsertDefaultValuesInDDL(ddlInterestType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlInterestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillproductcategorysubtype(ddlProductCategorySubType, ddlInterestType.SelectedValue);
        }
        //end

        //protected void frmVwNewActivity_ItemCommand(object sender, FormViewCommandEventArgs e)
        //{
        //    TextBox txtProductName = (TextBox)frmVwNewActivity.FindControl("txtProductName");
        //    DropDownList ddlCategorySubType = (DropDownList)frmVwNewActivity.FindControl("frmddlCategorySubType");
        //    DropDownList ddlProductType = (DropDownList)frmVwNewActivity.FindControl("frmddlProductType");
        //    DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
        //    DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");
        //    divMsgAlertActivity.Visible = true;

        //    if (e.CommandName.ToString() == "ResetActivity")
        //    {
        //        resetPopupControls();
        //    }
        //    if (e.CommandName.ToString() == "AddActivity")
        //    {
        //        MDMSVC.DC_Activity _obj = new MDMSVC.DC_Activity();

        //        _obj.Activity_Id = Guid.NewGuid();
        //        _obj.Product_Name = txtProductName.Text;
        //        _obj.ProductCategorySubType = ddlCategorySubType.SelectedItem.Text;
        //        _obj.ProductType = ddlProductType.SelectedItem.Text;
        //        _obj.Country_Id = Guid.Parse(ddlCountry.SelectedValue);
        //        _obj.Country = ddlCountry.SelectedItem.Text;
        //        _obj.City_Id = Guid.Parse(ddlCity.SelectedValue);
        //        _obj.City = ddlCity.SelectedItem.Text;

        //        MDMSVC.DC_Message _msg = activitySVC.AddUpdateActivity(_obj);
        //        if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
        //        {
        //            BootstrapAlert.BootstrapAlertMessage(divMsgAlertActivity, _msg.StatusMessage, BootstrapAlertType.Success);
        //        }
        //        else if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Duplicate)
        //        {
        //            MDMSVC.DC_Activity_Search_RQ _objDup = new MDMSVC.DC_Activity_Search_RQ();
        //            _objDup.Name = txtProductName.Text;
        //            var res = activitySVC.ActivitySearch(_objDup);
        //            if (res != null && res.Count != 0)
        //            {
        //                if (res.Count > 0)
        //                {
        //                    grdSearchResults.VirtualItemCount = Convert.ToInt32(res[0].TotalRecord);
        //                    grdSearchResults.DataSource = res;
        //                    grdSearchResults.PageIndex = 0;
        //                    grdSearchResults.PageSize = 1;
        //                    grdSearchResults.DataBind();
        //                    grdSearchResults.Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                grdSearchResults.DataSource = null;
        //                grdSearchResults.DataBind();
        //            }

        //            divMsgAlertActivity.Visible = true;
        //            BootstrapAlert.BootstrapAlertMessage(divMsgAlertActivity, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
        //        }
        //        else
        //        {
        //            divMsgAlertActivity.Visible = true;
        //            BootstrapAlert.BootstrapAlertMessage(divMsgAlertActivity, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
        //        }
        //        //frmVwNewActivity.ChangeMode(FormViewMode.Insert);
        //    }
        //}

        //private void resetPopupControls()
        //{
        //    DropDownList ddlCategorySubType = (DropDownList)frmVwNewActivity.FindControl("frmddlCategorySubType");
        //    DropDownList ddlProductType = (DropDownList)frmVwNewActivity.FindControl("frmddlProductType");
        //    DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
        //    DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");
        //    TextBox txtProductName = (TextBox)frmVwNewActivity.FindControl("txtProductName");

        //    txtProductName.Text = string.Empty;
        //    ddlCategorySubType.SelectedIndex = 0;
        //    ddlProductType.SelectedIndex = 0;
        //    ddlCountry.SelectedIndex = 0;
        //    ddlCity.Items.Clear();
        //    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
        //    divMsgAlertActivity.Visible = false;
        //    grdSearchResults.Visible = false;
        //}
        //protected void btnNewActivity_Click(object sender, EventArgs e)
        //{
        //    DropDownList ddlCategorySubType = (DropDownList)frmVwNewActivity.FindControl("frmddlCategorySubType");
        //    DropDownList ddlProductType = (DropDownList)frmVwNewActivity.FindControl("frmddlProductType");
        //    DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
        //    DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");
        //    TextBox txtProductName = (TextBox)frmVwNewActivity.FindControl("txtProductName");

        //    txtProductName.Text = string.Empty;
        //    divMsgAlertActivity.Visible = false;
        //    grdSearchResults.Visible = false;
        //    ddlCity.Items.Clear();
        //    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));

        //    fillproductcategorysubtype(ddlCategorySubType);
        //    fillProductType(ddlProductType);

        //}

        //protected void frmddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
        //    DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");

        //    if (ddlCountry.SelectedValue != "")
        //    {
        //        fillcitydropdown("search", ddlCountry.SelectedValue, ddlCity);
        //    }
        //    else
        //    {
        //        fillcitydropdown("search", "", ddlCity);
        //    }
        //    ddlCity.Focus();
        //}
    }
}