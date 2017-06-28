using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;
using System.Text;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class searchActivityMapping : System.Web.UI.UserControl
    {
        #region Variables
        MasterDataDAL masterdata = new MasterDataDAL();
        public static string AttributeOptionFor = "Activity";
        public static string AttributeOptionForMappingStatus = "ProductSupplierMapping";

        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MappingSVCs _mapping = new MappingSVCs();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();

        public int PageIndex = 0;
        public int PageIndexgrdActivitySearchResults = 0;
        public bool _blnDataExist = false;

        MDMSVC.DC_Activity_Search_RQ _objfilter = new DC_Activity_Search_RQ();
        public static int intActivityPageIndex = 0;
        public static int TotalCountActivityByProduct = 0;

        public static string strCountry = null;
        public static string strCity = null;
        public static Guid ActivitySupplierProductMapping_Id = Guid.Empty;
        public static string Supplier_ID = null;
        public static Guid Activity_Id = Guid.Empty;

        public static string strProductSubType = null;
        public static string strProductName = null;
        public static string strProductByKeyWord = null;


        public static string SupplierCityForBind = null;
        public static string SupplierCountryForBind = null;


        public static string SupplierKeyWorForSearch = null;
        public static string SupplierCityForSearch = null;
        public static string SupplierCountryForSearch = null;
        public static Guid Supplier_IDByProductSearch = Guid.Empty;

        public static int intSupplierActivityfvPageNo;
        public static int TotalCountSupplierActivityByProduct = 0;


        #endregion
        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPageData();
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
            }
        }

        private void FillPageData()
        {
            try
            {
                fillsuppliers();
                fillcountries(ddlSystemCountryName);
                fillmappingstatus(ddlMappingStatus);
                fillcountries(ddlSystemCountryActivityByProduct);
                fillCKISProductType();
                fillActivityStatus(ddlProdStatusActivityByProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Tab 1
        #region Button Click Event
        protected void btnActivitySearch_Click(object sender, EventArgs e)
        {
            bool blnDataExist = false;
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            SearchActivityMappingData(ref blnDataExist);
            if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                btnMapSelected.Visible = blnDataExist;
                btnMapAll.Visible = blnDataExist;
            }
            else if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                if (ddlSystemCountryName.SelectedValue == "0" && ddlSystemCityName.SelectedValue == "0")
                {
                    btnMapSelected.Visible = false;
                    btnMapAll.Visible = false;
                }
                else
                {
                    btnMapSelected.Visible = blnDataExist;
                    btnMapAll.Visible = blnDataExist;
                }
            }
            else
            {
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
            }
        }

        private void SearchActivityMappingData(ref bool blnDataExist)
        {
            try
            {
                MDMSVC.DC_Acitivity_SupplierProductMapping_Search_RQ _objSearch = new MDMSVC.DC_Acitivity_SupplierProductMapping_Search_RQ();
                if (ddlSupplierName.SelectedValue != "0")
                    _objSearch.SupplierName = ddlSupplierName.SelectedItem.Text;
                if (ddlSystemCountryName.SelectedValue != "0")
                    _objSearch.SupplierCountryName = ddlSystemCountryName.SelectedItem.Text;
                if (ddlSystemCityName.SelectedValue != "0")
                    _objSearch.SupplierCityName = ddlSystemCityName.SelectedItem.Text;
                if (ddlMappingStatus.SelectedValue != "0")
                    _objSearch.MappingStatus = ddlMappingStatus.SelectedItem.Text;

                _objSearch.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
                if (!string.IsNullOrWhiteSpace(txtSuppCountryName.Text))
                    _objSearch.SupplierCountryName = txtSuppCountryName.Text;
                if (!string.IsNullOrWhiteSpace(txtSuppCityName.Text))
                    _objSearch.SupplierCityName = txtSuppCityName.Text;
                if (!string.IsNullOrWhiteSpace(txtSuppProductName.Text))
                    _objSearch.SupplierProductName = txtSuppProductName.Text;
                if (!string.IsNullOrWhiteSpace(txtKeyWordBySupplier.Text))
                    _objSearch.KeyWord = txtKeyWordBySupplier.Text;

                _objSearch.PageNo = PageIndex;
                var res = _mapping.GetActivitySupplierProductMappingSearch(_objSearch);
                grdActivitySearchResults.Visible = true;
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        blnDataExist = true;
                        grdActivitySearchResults.VirtualItemCount = res[0].TotalCount;
                        lblActivityCount.Text = res[0].TotalCount.ToString();
                    }
                    else
                    {
                        lblActivityCount.Text = "0";
                        blnDataExist = false;
                    }
                }
                else
                {
                    lblActivityCount.Text = "0";
                    blnDataExist = false;
                }
                grdActivitySearchResults.DataSource = res;
                grdActivitySearchResults.PageIndex = PageIndex;
                //if (via == "supplier")
                grdActivitySearchResults.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
                //else
                //    grdActivitySearchResults.PageSize = Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Text);
                //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
                grdActivitySearchResults.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnActivityReset_Click(object sender, EventArgs e)
        {
            //Clear/Reset all filters
            Resetfilters();
        }

        private void Resetfilters()
        {
            try
            {
                divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
                ddlSupplierName.SelectedIndex = 0;
                ddlSystemCountryName.SelectedIndex = 0;
                ddlPageSize.SelectedIndex = 0;
                ddlMappingStatus.SelectedIndex = 0;
                ddlSystemCityName.Items.Clear();
                ddlSystemCityName.Items.Add(new ListItem("---ALL---", "0"));
                grdActivitySearchResults.DataSource = null;
                grdActivitySearchResults.DataBind();
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
                lblActivityCount.Text = "0";
                txtSuppCountryName.Text = string.Empty;
                txtSuppCityName.Text = string.Empty;
                txtSuppProductName.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Method
        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = masterdata.GetSupplierMasterData();
            ddlSupplierName.DataSource = masterSVc.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }
        private void fillcountries(DropDownList ddl)
        {
            ddl.DataSource = masterSVc.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
        }
        private void fillmappingstatus(DropDownList ddl)
        {
            fillAttributeValues(ddl, "MappingStatus", AttributeOptionForMappingStatus);
        }
        private void fillAttributeValues(DropDownList ddl, string Attribute_Name, string OptionFor)
        {
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(OptionFor, Attribute_Name).MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }
        public void fillcities(DropDownList ddl, DropDownList ddlp)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {
                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddl.DataSource = masterSVc.GetMasterCityData(ddlp.SelectedItem.Value);
                ddl.DataValueField = "City_ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        private void BindFilterData(string SupplierCountryName, string SupplierCityName)
        {
            fillActivityFilterCKISType();
            fillActivityFilterCountry(SupplierCountryName, SupplierCityName);



        }
        private void fillActivityFilterCountry(string SupplierCountryName, string SupplierCityName)
        {
            try
            {
                ddlActivityFilterCountry.DataSource = masterSVc.GetAllCountries();
                ddlActivityFilterCountry.DataValueField = "Country_Id";
                ddlActivityFilterCountry.DataTextField = "Country_Name";
                ddlActivityFilterCountry.DataBind();
                ddlActivityFilterCountry.SelectedIndex = ddlActivityFilterCountry.Items.IndexOf(ddlActivityFilterCountry.Items.FindByText(System.Web.HttpUtility.HtmlDecode(SupplierCountryName)));
                ddlActivityFilterCountry.Enabled = false;

                //Bind City 
                ddlActivityFilterCity.DataSource = masterSVc.GetMasterCityData(ddlActivityFilterCountry.SelectedItem.Value);
                ddlActivityFilterCity.DataValueField = "City_ID";
                ddlActivityFilterCity.DataTextField = "Name";
                ddlActivityFilterCity.DataBind();
                ddlActivityFilterCity.SelectedIndex = ddlActivityFilterCity.Items.IndexOf(ddlActivityFilterCity.Items.FindByText(System.Web.HttpUtility.HtmlDecode(SupplierCityName)));

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void fillActivityFilterCKISType()
        {
            try
            {
                ddlActivityFilterCKISType.Items.Clear();
                ddlActivityFilterCKISType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "CKISType").MasterAttributeValues;
                ddlActivityFilterCKISType.DataTextField = "AttributeValue";
                ddlActivityFilterCKISType.DataValueField = "MasterAttributeValue_Id";
                ddlActivityFilterCKISType.DataBind();
                ddlActivityFilterCKISType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void BindActivityDeatils()
        {
            _objfilter.PageNo = intActivityPageIndex;
            _objfilter.PageSize = 1;
            _objfilter.City = strCity;
            _objfilter.Country = strCountry;
            _objfilter.Name = strProductName;
            _objfilter.Keyword = strProductByKeyWord;
            if (!string.IsNullOrWhiteSpace(Supplier_ID))
                _objfilter.Supplier_Id = Supplier_ID;
            _objfilter.ProductSubType = strProductSubType;
            lblActivityNo.Text = Convert.ToString(intActivityPageIndex + 1);
            ActivityFormView.DataSource = null;
            ActivityFormView.DataBind();
            var activityBasic = masterSVc.GetActivityMasterBySupplier(_objfilter);
            if (activityBasic != null && activityBasic.Count > 0)
            {
                if (activityBasic[0].TotalRecord > 1)
                    btnNext.Enabled = true;
                TotalCountActivityByProduct = Convert.ToInt32(activityBasic[0].TotalRecord);
                lblTotalCountActivity.Text = Convert.ToString(TotalCountActivityByProduct);
                lblProductName.Text = Convert.ToString(activityBasic[0].Product_Name);
                ActivityFormView.DataSource = activityBasic;
                ActivityFormView.DataBind();

                if (Convert.ToString(activityBasic[0].Activity_Id) != null)
                {
                    List<MDMSVC.DC_Activity_Content> result = masterSVc.GetActivityContentMaster(new MDMSVC.DC_Activity_Content() { Activity_Id = activityBasic[0].Activity_Id });
                    if (result != null)
                    {
                        Repeater rptInclusions = (Repeater)ActivityFormView.FindControl("rptInclusions");
                        if (rptInclusions != null)
                        {
                            rptInclusions.DataSource = (from a in result where a.Content_Type.ToUpper() == "INCLUSION" select a);
                            rptInclusions.DataBind();
                        }
                        Repeater rptExclusion = (Repeater)ActivityFormView.FindControl("rptExclusion");
                        if (rptExclusion != null)
                        {
                            rptExclusion.DataSource = (from a in result where a.Content_Type.ToUpper() == "EXCLUSION" select a);
                            rptExclusion.DataBind();
                        }
                        Repeater rptNotes = (Repeater)ActivityFormView.FindControl("rptNotes");
                        if (rptNotes != null)
                        {
                            rptNotes.DataSource = (from a in result where a.Content_Type.ToUpper() == "NOTES" select a);
                            rptNotes.DataBind();
                        }

                    }
                }
                fillMappedActivityGrid(ActivitySupplierProductMapping_Id);
            }
            else
            {
                btnNext.Enabled = false;
                btnNext.Enabled = false;
                lblActivityNo.Text = "0";
                TotalCountActivityByProduct = 0;
                lblTotalCountActivity.Text = "0";
                lblProductName.Text = string.Empty;
                ActivityFormView.DataSource = null;
                ActivityFormView.DataBind();
            }
        }
        private void fillMappedActivityGrid(Guid activitySupplierProductMapping_Id)
        {
            try
            {
                var res = _mapping.GetActivitySupplierProductMappingSearch(new DC_Acitivity_SupplierProductMapping_Search_RQ() { ActivitySupplierProductMappling_Id = activitySupplierProductMapping_Id, PageSize = 5, MappingStatus = "MAPPED" });
                if (res != null && res.Count > 0)
                {
                    // divMapped.Style.Add(HtmlTextWriterStyle.Display, "block");
                    grdActivityMapped.DataSource = res;
                    grdActivityMapped.DataBind();
                }
                else
                {
                    //divMapped.Style.Add(HtmlTextWriterStyle.Display, "none");
                    grdActivityMapped.DataSource = null;
                    grdActivityMapped.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Controls Events
        protected void ddlSystemCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities(ddlSystemCityName, ddlSystemCountryName);

        }
        protected void grdActivitySearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            PageIndex = e.NewPageIndex;
            SearchActivityMappingData(ref _blnDataExist);
        }
        protected void grdActivitySearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                var myGridView = (GridView)sender;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int index = e.Row.RowIndex;
                    string myCountrName = string.Empty;
                    if (myGridView.DataKeys[index].Values[3] != null)
                        myCountrName = myGridView.DataKeys[index].Values[3].ToString();
                    DropDownList ddl = e.Row.Cells[10].Controls[1] as DropDownList;
                    if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                    {
                        if (ddl != null)
                        {
                            var myActivtiyid = Convert.ToString(myGridView.DataKeys[index].Values[4]);
                            var myActivity = Convert.ToString(myGridView.DataKeys[index].Values[5]);
                            if (!string.IsNullOrWhiteSpace(myActivtiyid) && !string.IsNullOrWhiteSpace(myActivity))
                            {
                                ddl.Items.Add(new ListItem(myActivity, myActivtiyid));
                                if (ddl.Items.FindByValue(myActivtiyid) != null)
                                    ddl.Items.FindByValue(myActivtiyid).Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void grdActivitySearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Select")
                {
                    Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    ActivitySupplierProductMapping_Id = myRow_Id;

                    var result = _mapping.GetActivitySupplierProductMappingById(myRow_Id);
                    if (result != null)
                    {
                        StringBuilder sbSupplierNameWithCode = new StringBuilder();
                        sbSupplierNameWithCode.Append(Convert.ToString(result.SupplierName));
                        string strSupplierCode = Convert.ToString(result.SupplierCode);
                        if (!string.IsNullOrWhiteSpace(strSupplierCode))
                            sbSupplierNameWithCode.Append(" (" + strSupplierCode + ")");
                        lblSupplierName.Text = sbSupplierNameWithCode.ToString();
                        lblSupplierProductCode.Text = Convert.ToString(result.SuplierProductCode);
                        lblSupplierProductType.Text = Convert.ToString(result.SupplierProductType);
                        lblSupplierCountryName.Text = Convert.ToString(result.SupplierCountryName) + (!string.IsNullOrWhiteSpace(result.SupplierCountryCode) ? "(" + Convert.ToString(result.SupplierCountryCode) + ")" : string.Empty);
                        lblSupplierCityName.Text = Convert.ToString(result.SupplierCityName) + (!string.IsNullOrWhiteSpace(result.SupplierCityCode) ? "(" + Convert.ToString(result.SupplierCityCode) + ")" : string.Empty);
                        lblSupplierProductName.Text = Convert.ToString(result.SupplierProductName);
                        lblAddress.Text = Convert.ToString(result.Address);
                        lblSupplierProDuration.Text = Convert.ToString(result.Duration);
                        lblDeparturePoint.Text = Convert.ToString(result.DeparturePoint);
                        lblDepartureTime.Text = Convert.ToString(result.DepartureTime);
                        lblCurrency.Text = Convert.ToString(result.Currency);
                        lblProductValidFor.Text = Convert.ToString(result.ProductValidFor);
                        lblLatitude.Text = Convert.ToString(result.Latitude);
                        lblLongitude.Text = Convert.ToString(result.Longitude);
                        lblTheme.Text = Convert.ToString(result.Theme);

                        lblInclusions.Text = Convert.ToString(result.Inclusions);
                        lblExclusions.Text = Convert.ToString(result.Exclusions);
                        lblIntroduction.Text = Convert.ToString(result.Introduction);
                        lblConditions.Text = Convert.ToString(result.Conditions);
                        lblAdditionalInformation.Text = Convert.ToString(result.AdditionalInformation);
                        lblProductDescription.Text = Convert.ToString(result.ProductDescription);

                        intActivityPageIndex = 0;
                        strProductSubType = strCountry = strCity = strProductName = strProductByKeyWord = null;

                        //Getting Data To Fetch activity from activity master table 
                        Supplier_ID = Convert.ToString(grdActivitySearchResults.DataKeys[index].Values[1]);

                        if (!string.IsNullOrWhiteSpace(result.SupplierCountryName))
                        {
                            _objfilter.Country = Convert.ToString(result.SupplierCountryName);
                            strCountry = Convert.ToString(result.SupplierCountryName);
                        }
                        if (!string.IsNullOrWhiteSpace(result.SupplierCityName))
                        {
                            _objfilter.City = Convert.ToString(result.SupplierCityName);
                            strCity = Convert.ToString(result.SupplierCityName);
                        }
                        ClearActivityFilter();
                        txtActivityFilterName.Text = string.Empty;
                        BindActivityDeatils();
                        BindFilterData(result.SupplierCountryName, result.SupplierCityName);

                        //List<MDMSVC.DC_Activity_Content> result = mastersvc.GetActivityContentMaster(new MDMSVC.DC_Activity_Content() { Activity_Id = myRow_Id });


                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearActivityFilter()
        {
            txtActivityFilterName.Text = string.Empty;
            ddlActivityFilterCountry.ClearSelection();
            ddlActivityFilterCKISType.ClearSelection();
            ddlActivityFilterCity.Items.Clear();
            ddlActivityFilterCity.Items.Insert(0, new ListItem("---ALL---", "0"));

        }

        protected void grdActivitySearchResults_DataBinding(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                myGridView.Columns[9].Visible = true;
                myGridView.Columns[10].Visible = false;
                myGridView.Columns[13].Visible = true;
            }
            else if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED" && ddlSystemCityName.SelectedValue != "0")
            {
                myGridView.Columns[9].Visible = false;
                myGridView.Columns[10].Visible = true;
                myGridView.Columns[13].Visible = true;

            }
            else
            {
                myGridView.Columns[9].Visible = true;
                myGridView.Columns[10].Visible = false;
                myGridView.Columns[11].Visible = true;
                myGridView.Columns[13].Visible = false;

            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (intActivityPageIndex >= 0)
            {
                intActivityPageIndex = intActivityPageIndex - 1;
                if (intActivityPageIndex == 0)
                    btnPrevious.Enabled = false;
                else
                    btnPrevious.Enabled = true;
                BindActivityDeatils();
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (intActivityPageIndex < TotalCountActivityByProduct)
            {
                intActivityPageIndex = intActivityPageIndex + 1;
                btnPrevious.Enabled = true;
                if (intActivityPageIndex == (TotalCountActivityByProduct - 1))
                    btnNext.Enabled = false;


                BindActivityDeatils();
            }
        }
        protected void btnMapActivityMap_Click(object sender, EventArgs e)
        {
            HiddenField hdnActivityId = (HiddenField)ActivityFormView.FindControl("hdnActivityId");
            Guid Activity_Id = Guid.Empty;
            if (hdnActivityId != null)
                Activity_Id = Guid.Parse(hdnActivityId.Value);
            MDMSVC.DC_Message _msg = new DC_Message();
            List<DC_Acitivity_SupplierProductMapping> _lstDC_Acitivity_SupplierProductMapping = new List<DC_Acitivity_SupplierProductMapping>();
            _lstDC_Acitivity_SupplierProductMapping.Add(new DC_Acitivity_SupplierProductMapping()
            {
                ActivitySupplierProductMapping_Id = ActivitySupplierProductMapping_Id,
                Activity_ID = Activity_Id,
                MappingStatus = "MAPPED",
                Edit_Date = DateTime.Today,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name
            });
            _msg = _mapping.UpdateActivitySupplierProductMapping(_lstDC_Acitivity_SupplierProductMapping);
            if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsg, _msg.StatusMessage, BootstrapAlertType.Success);
                fillMappedActivityGrid(ActivitySupplierProductMapping_Id);
                BindActivityDeatils();
                SearchActivityMappingData(ref _blnDataExist);
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsg, "Error occured. Please try later. ", BootstrapAlertType.Success);
            }

        }
        protected void btnActivityFilter_Click(object sender, EventArgs e)
        {
            //Get Filter Deatils
            strProductSubType = strCountry = strCity = strProductName = strProductByKeyWord = null;
            intActivityPageIndex = 0;
            if (ddlActivityFilterCKISType.SelectedValue != "0")
                strProductSubType = ddlActivityFilterCKISType.SelectedItem.Text;
            if (ddlActivityFilterCountry.SelectedValue != "0")
                strCountry = ddlActivityFilterCountry.SelectedItem.Text;
            if (ddlActivityFilterCity.SelectedValue != "0")
                strCity = ddlActivityFilterCity.SelectedItem.Text;
            if (!string.IsNullOrWhiteSpace(txtActivityFilterName.Text))
                strProductName = txtActivityFilterName.Text;
            if (!string.IsNullOrWhiteSpace(txtfilterActivityByKeyWord.Text))
                strProductByKeyWord = txtfilterActivityByKeyWord.Text;
            BindActivityDeatils();
        }

        protected void ddlActivityFilterCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActivityFilterCity.Items.Clear();
            if (ddlActivityFilterCountry.SelectedItem.Value != "0")
            {
                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddlActivityFilterCity.DataSource = masterSVc.GetMasterCityData(ddlActivityFilterCountry.SelectedItem.Value);
                ddlActivityFilterCity.DataValueField = "City_ID";
                ddlActivityFilterCity.DataTextField = "Name";
                ddlActivityFilterCity.DataBind();
            }
            ddlActivityFilterCity.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void grdActivityMapped_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unmap")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                ActivitySupplierProductMapping_Id = myRow_Id;

                MDMSVC.DC_Message _msg = new DC_Message();
                List<DC_Acitivity_SupplierProductMapping> _lstDC_Acitivity_SupplierProductMapping = new List<DC_Acitivity_SupplierProductMapping>();
                _lstDC_Acitivity_SupplierProductMapping.Add(new DC_Acitivity_SupplierProductMapping()
                {
                    ActivitySupplierProductMapping_Id = ActivitySupplierProductMapping_Id,
                    MappingStatus = "UNMAPPED",
                    Edit_Date = DateTime.Today,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                _msg = _mapping.UpdateActivitySupplierProductMapping(_lstDC_Acitivity_SupplierProductMapping);
                if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgUnMapped, _msg.StatusMessage, BootstrapAlertType.Success);
                    fillMappedActivityGrid(ActivitySupplierProductMapping_Id);
                    BindActivityDeatils();
                    SearchActivityMappingData(ref _blnDataExist);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgUnMapped, "Error occured. Please try later. ", BootstrapAlertType.Success);
                }
            }
        }

        protected void btnMapSelected_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_Acitivity_SupplierProductMapping> RQ = new List<MDMSVC.DC_Acitivity_SupplierProductMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid Activity_ID = Guid.Empty;
            MDMSVC.DC_Message _msg = new DC_Message();
            foreach (GridViewRow row in grdActivitySearchResults.Rows)
            {
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    HtmlInputCheckBox chk = row.Cells[14].Controls[1] as HtmlInputCheckBox;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        Activity_ID = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdActivitySearchResults.DataKeys[index].Values[0].ToString());
                        if (grdActivitySearchResults.DataKeys[index].Values[5] != null)
                            Activity_ID = Guid.Parse(grdActivitySearchResults.DataKeys[index].Values[5].ToString());
                    }
                }
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    HtmlInputCheckBox chk = row.Cells[13].Controls[1] as HtmlInputCheckBox;
                    DropDownList ddl = row.Cells[10].Controls[1] as DropDownList;
                    HiddenField hdnActivityId = row.Cells[15].Controls[1] as HiddenField; //Set value from ajax changes
                    if (chk != null && chk.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(hdnActivityId.Value) && hdnActivityId.Value != "0")
                        {
                            if (Activity_ID != null)
                                Activity_ID = Guid.Parse(hdnActivityId.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                Activity_ID = Guid.Parse(ddl.SelectedItem.Value);
                        }
                        myRow_Id = Guid.Empty;
                        int index = row.RowIndex;

                        myRow_Id = Guid.Parse(grdActivitySearchResults.DataKeys[index].Values[0].ToString());
                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Acitivity_SupplierProductMapping param = new MDMSVC.DC_Acitivity_SupplierProductMapping();
                    param.ActivitySupplierProductMapping_Id = myRow_Id;

                    if (Activity_ID != null)
                        param.Activity_ID = Activity_ID;
                    param.MappingStatus = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    _msg = _mapping.UpdateActivitySupplierProductMapping(RQ); ;
                    myRow_Id = Guid.Empty;
                    Activity_ID = Guid.Empty;
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, "Activities has been mapped successfully", BootstrapAlertType.Success);
                }
            }

            SearchActivityMappingData(ref _blnDataExist);
        }

        protected void btnMapAll_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_Acitivity_SupplierProductMapping> RQ = new List<MDMSVC.DC_Acitivity_SupplierProductMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid Activity_ID = Guid.Empty;
            MDMSVC.DC_Message _msg = new DC_Message();
            foreach (GridViewRow row in grdActivitySearchResults.Rows)
            {
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {

                    myRow_Id = Guid.Empty;
                    Activity_ID = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdActivitySearchResults.DataKeys[index].Values[0].ToString());
                    if (grdActivitySearchResults.DataKeys[index].Values[5] != null)
                        Activity_ID = Guid.Parse(grdActivitySearchResults.DataKeys[index].Values[5].ToString());
                }
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    DropDownList ddl = row.Cells[10].Controls[1] as DropDownList;
                    HiddenField hdnActivityId = row.Cells[15].Controls[1] as HiddenField; //Set value from ajax changes
                    if (ddl.SelectedItem.Value != "0" || !string.IsNullOrWhiteSpace(hdnActivityId.Value))
                    {
                        if (!string.IsNullOrWhiteSpace(hdnActivityId.Value))
                        {
                            if (hdnActivityId.Value == "0")
                                continue;
                            if (hdnActivityId != null)
                                Activity_ID = Guid.Parse(hdnActivityId.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                Activity_ID = Guid.Parse(ddl.SelectedItem.Value);
                        }
                        myRow_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdActivitySearchResults.DataKeys[index].Values[0].ToString());

                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Acitivity_SupplierProductMapping param = new MDMSVC.DC_Acitivity_SupplierProductMapping();
                    param.ActivitySupplierProductMapping_Id = myRow_Id;

                    if (Activity_ID != null)
                        param.Activity_ID = Activity_ID;
                    param.MappingStatus = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    _msg = _mapping.UpdateActivitySupplierProductMapping(RQ); ;
                    myRow_Id = Guid.Empty;
                    Activity_ID = Guid.Empty;
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, "Activities has been mapped successfully", BootstrapAlertType.Success);
                }
            }
            SearchActivityMappingData(ref _blnDataExist);
        }




        #endregion

        #endregion

        #region Tab 2
        #region Method 
        private void fillCKISProductType()
        {
            fillAttributeValues(ddlCKISProductTypeActivityByProduct, "CKISType", AttributeOptionFor);
        }
        private void fillActivityStatus(DropDownList ddlProdStatusActivityByProduct)
        {
            fillAttributeValues(ddlProdStatusActivityByProduct, "Status", "SystemStatus");
        }
        private void SearchActivityData(ref bool blnDataExist)
        {
            try
            {
                MDMSVC.DC_Activity_Search_RQ _objSearch = new MDMSVC.DC_Activity_Search_RQ();

                //For Binding
                if (ddlSystemCountryActivityByProduct.SelectedValue != "0")
                    _objSearch.Country = ddlSystemCountryActivityByProduct.SelectedItem.Text;
                if (ddlSystemCityActivityByProduct.SelectedValue != "0")
                    _objSearch.City = ddlSystemCityActivityByProduct.SelectedItem.Text;
                if (ddlProdStatusActivityByProduct.SelectedValue != "0")
                    _objSearch.Status = ddlProdStatusActivityByProduct.SelectedItem.Text;
                if (ddlCKISProductTypeActivityByProduct.SelectedValue != "0")
                    _objSearch.ProductSubType = ddlCKISProductTypeActivityByProduct.SelectedItem.Text;
                _objSearch.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
                if (!string.IsNullOrWhiteSpace(txtCKISProductNameActivityByProduct.Text))
                    _objSearch.Name = txtCKISProductNameActivityByProduct.Text;
                if (!string.IsNullOrWhiteSpace(txtKeyWordBySupplier.Text))
                    _objSearch.Keyword = txtKeyWordBySupplier.Text;

                _objSearch.PageSize = Convert.ToInt32(ddlPageSizeActivityByProduct.SelectedValue);
                _objSearch.PageNo = PageIndexgrdActivitySearchResults;
                var res = masterSVc.GetActivityMaster(_objSearch);
                grdActivitySearchByProduct.Visible = true;
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        blnDataExist = true;
                        grdActivitySearchByProduct.VirtualItemCount = res[0].TotalRecord;
                        lblCKISProductMasters.Text = res[0].TotalRecord.ToString();
                    }
                    else
                    {
                        lblCKISProductMasters.Text = "0";
                        blnDataExist = false;
                    }
                }
                else
                {
                    lblCKISProductMasters.Text = "0";
                    blnDataExist = false;
                }
                grdActivitySearchByProduct.DataSource = res;
                grdActivitySearchByProduct.PageIndex = PageIndex;
                grdActivitySearchByProduct.PageSize = Convert.ToInt32(ddlPageSizeActivityByProduct.SelectedValue);
                grdActivitySearchByProduct.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //private void fillSupplierActivityFormView()
        //{
        //    DC_Acitivity_SupplierProductMapping_Search_RQ _objFilter = new DC_Acitivity_SupplierProductMapping_Search_RQ();
        //    _objFilter.SupplierCountryName = SupplierCityForBind;
        //    _objFilter.SupplierCityName = SupplierCountryForBind;
        //    var result = _mapping.GetActivitySupplierProductMappingSearch(_objFilter);
        //    if (result != null && result.Count > 0)
        //    {
        //        if (result.Count > 1)
        //            btnNext.Enabled = true;
        //        TotalCountActivityByProduct = Convert.ToInt32(result[0].TotalCount);
        //        lblTotalCountActivity.Text = Convert.ToString(TotalCountActivityByProduct);
        //        frmVwMasterActivityDetails.DataSource = result;
        //        frmVwMasterActivityDetails.DataBind();

        //    }
        //    else
        //    {

        //    }

        //}
        public void BindMasterActivityDetails(Guid ActivityID)
        {
            frmVwMasterActivityDetails.DataSource = null;
            frmVwMasterActivityDetails.DataBind();
            var res = masterSVc.GetActivityMaster(new MDMSVC.DC_Activity_Search_RQ() { Activity_Id = ActivityID, PageNo = 0, PageSize = 1 });
            if (res != null && res.Count > 0)
            {
                frmVwMasterActivityDetails.PageIndex = 0;
                frmVwMasterActivityDetails.DataSource = res;
                frmVwMasterActivityDetails.DataBind();
                SupplierCityForBind = res[0].City;
                SupplierCountryForBind = res[0].Country;

                if (Convert.ToString(res[0].Activity_Id) != null)
                {
                    List<MDMSVC.DC_Activity_Content> result = masterSVc.GetActivityContentMaster(new MDMSVC.DC_Activity_Content() { Activity_Id = res[0].Activity_Id });
                    if (result != null)
                    {
                        Repeater rptInclusions = (Repeater)frmVwMasterActivityDetails.FindControl("rptInclusions");
                        if (rptInclusions != null)
                        {
                            rptInclusions.DataSource = (from a in result where a.Content_Type.ToUpper() == "INCLUSION" select a);
                            rptInclusions.DataBind();
                        }
                        Repeater rptExclusion = (Repeater)frmVwMasterActivityDetails.FindControl("rptExclusion");
                        if (rptExclusion != null)
                        {
                            rptExclusion.DataSource = (from a in result where a.Content_Type.ToUpper() == "EXCLUSION" select a);
                            rptExclusion.DataBind();
                        }
                        Repeater rptNotes = (Repeater)frmVwMasterActivityDetails.FindControl("rptNotes");
                        if (rptNotes != null)
                        {
                            rptNotes.DataSource = (from a in result where a.Content_Type.ToUpper() == "NOTES" select a);
                            rptNotes.DataBind();
                        }

                    }
                }
                // fillSupplierActivityFormView();
            }
            else
            {
                frmVwMasterActivityDetails.DataSource = null;
                frmVwMasterActivityDetails.DataBind();
            }
        }
        #endregion
        #region Control Event
        protected void btnSearchActivityByProduct_Click(object sender, EventArgs e)
        {
            bool blnDataExist = false;
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            SearchActivityData(ref blnDataExist);
        }
        protected void btnResetActivityByProduct_Click(object sender, EventArgs e)
        {
            Tab2ResetControls();
        }
        protected void btnSearchSupplierForMapping_Click(object sender, EventArgs e)
        {
            if (ddlSupplierFilterforMappingByProduct.SelectedValue != "0")
            {
                Supplier_IDByProductSearch = Guid.Parse(ddlSupplierFilterforMappingByProduct.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(txtCountryFileterByProductSupplier.Text))
            {
                SupplierCountryForSearch = txtCountryFileterByProductSupplier.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(txtCityFileterByProductSupplier.Text))
            {
                SupplierCityForSearch = txtCityFileterByProductSupplier.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(txtKeywordFilterByProducntSupplier.Text))
            {
                SupplierKeyWorForSearch = txtKeywordFilterByProducntSupplier.Text.Trim();
            }
            SupplierCountryForBind = null;
            SupplierCityForBind = null;
            intSupplierActivityfvPageNo = 0;
            BindSupplierActivityDetails();
        }

        protected void btnPreviousByProduct_Click(object sender, EventArgs e)
        {
            if (intSupplierActivityfvPageNo >= 0)
            {
                intSupplierActivityfvPageNo = intSupplierActivityfvPageNo - 1;
                if (intSupplierActivityfvPageNo == 0)
                    btnPreviousByProduct.Enabled = false;
                else
                    btnPreviousByProduct.Enabled = true;
                BindSupplierActivityDetails();
            }
        }

        protected void btnNextByProduct_Click(object sender, EventArgs e)
        {
            if (intSupplierActivityfvPageNo < TotalCountSupplierActivityByProduct)
            {
                intSupplierActivityfvPageNo = intSupplierActivityfvPageNo + 1;
                btnPreviousByProduct.Enabled = true;
                BindSupplierActivityDetails();
                if (intSupplierActivityfvPageNo == (TotalCountSupplierActivityByProduct - 1))
                    btnNextByProduct.Enabled = false;
            }
        }

        protected void btnMapActivityByProduct_Click(object sender, EventArgs e)
        {
            HiddenField hdnActivitySupplierProductMapping_Id = (HiddenField)frmvwSupplierActivtiy.FindControl("hdnActivitySupplierProductMapping_Id");
            Guid ActivitySupplierProductMapping_Id = Guid.Empty;
            if (hdnActivitySupplierProductMapping_Id != null)
                ActivitySupplierProductMapping_Id = Guid.Parse(hdnActivitySupplierProductMapping_Id.Value);
            MDMSVC.DC_Message _msg = new DC_Message();
            List<DC_Acitivity_SupplierProductMapping> _lstDC_Acitivity_SupplierProductMapping = new List<DC_Acitivity_SupplierProductMapping>();
            _lstDC_Acitivity_SupplierProductMapping.Add(new DC_Acitivity_SupplierProductMapping()
            {
                ActivitySupplierProductMapping_Id = ActivitySupplierProductMapping_Id,
                Activity_ID = Activity_Id,
                MappingStatus = "MAPPED",
                Edit_Date = DateTime.Today,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name
            });
            _msg = _mapping.UpdateActivitySupplierProductMapping(_lstDC_Acitivity_SupplierProductMapping);
            if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgByProductMapped, _msg.StatusMessage, BootstrapAlertType.Success);
                BindMappedSupplierActivityForByProduct(Activity_Id);
                BindSupplierActivityDetails();
                SearchActivityData(ref _blnDataExist);
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgByProductMapped, "Error occured. Please try later. ", BootstrapAlertType.Success);
            }
        }

        private void Tab2ResetControls()
        {
            try
            {
                ddlSystemCountryActivityByProduct.SelectedIndex = 0;
                ddlCKISProductTypeActivityByProduct.SelectedIndex = 0;
                ddlProdStatusActivityByProduct.SelectedIndex = 0;
                ddlSystemCityActivityByProduct.Items.Clear();
                ddlSystemCityActivityByProduct.Items.Add(new ListItem("---ALL---", "0"));
                grdActivitySearchByProduct.DataSource = null;
                grdActivitySearchByProduct.DataBind();
                ddlPageSizeActivityByProduct.SelectedIndex = 0;
                lblCKISProductMasters.Text = "0";
                txtCKISProductNameActivityByProduct.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void ddlPageSizeActivityByProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool blnFlag = false;
            SearchActivityData(ref blnFlag);
        }

        protected void grdActivitySearchByProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndexgrdActivitySearchResults = e.NewPageIndex;
            bool blnFlag = false;
            SearchActivityData(ref blnFlag);
        }

        protected void grdActivitySearchByProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    Activity_Id = myRow_Id;
                    intSupplierActivityfvPageNo = 0;
                    //Get Selected Master Activity
                    BindMasterActivityDetails(myRow_Id);
                    //Bind Dropdown Supplier for filter  
                    BindSupplierActivityDropDown();
                    //Bind Supplier Activity Details With respect to Country and City

                    Supplier_IDByProductSearch = Guid.Empty;
                    SupplierCountryForSearch = null;
                    SupplierCityForSearch = null;
                    SupplierKeyWorForSearch = null;

                    BindSupplierActivityDetails();
                    //Bind Mapped Data With Respect to selected master activity
                    BindMappedSupplierActivityForByProduct(myRow_Id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void grdvwMappedActivityByProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unmap")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                ActivitySupplierProductMapping_Id = myRow_Id;

                MDMSVC.DC_Message _msg = new DC_Message();
                List<DC_Acitivity_SupplierProductMapping> _lstDC_Acitivity_SupplierProductMapping = new List<DC_Acitivity_SupplierProductMapping>();
                _lstDC_Acitivity_SupplierProductMapping.Add(new DC_Acitivity_SupplierProductMapping()
                {
                    ActivitySupplierProductMapping_Id = ActivitySupplierProductMapping_Id,
                    MappingStatus = "UNMAPPED",
                    Edit_Date = DateTime.Today,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                _msg = _mapping.UpdateActivitySupplierProductMapping(_lstDC_Acitivity_SupplierProductMapping);
                if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgByProductUnMapped, _msg.StatusMessage, BootstrapAlertType.Success);
                    BindMappedSupplierActivityForByProduct(Activity_Id);
                    BindSupplierActivityDetails();
                    SearchActivityData(ref _blnDataExist);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgByProductUnMapped, "Error occured. Please try later. ", BootstrapAlertType.Success);
                }
            }
        }
        private void BindMappedSupplierActivityForByProduct(Guid myRow_Id)
        {
            var result = _mapping.GetActivitySupplierProductMappingSearch(new DC_Acitivity_SupplierProductMapping_Search_RQ() { Activity_ID = myRow_Id });
            if (result != null && result.Count > 0)
            {
                grdvwMappedActivityByProduct.DataSource = result;
                grdvwMappedActivityByProduct.DataBind();
            }
            else
            {
                grdvwMappedActivityByProduct.DataSource = null;
                grdvwMappedActivityByProduct.DataBind();
            }
        }

        private void BindSupplierActivityDetails()
        {
            try
            {
                //Get selected Activity details to find supplier activity list
                DC_Acitivity_SupplierProductMapping_Search_RQ _objSearch = new DC_Acitivity_SupplierProductMapping_Search_RQ();
                _objSearch.PageSize = 1;
                _objSearch.PageNo = intSupplierActivityfvPageNo;
                lblActivityNoByProduct.Text = Convert.ToString(intSupplierActivityfvPageNo + 1);
                if (!string.IsNullOrWhiteSpace(SupplierCountryForBind))
                    _objSearch.SupplierCountryName = SupplierCountryForBind;
                if (!string.IsNullOrWhiteSpace(SupplierCityForBind))
                    _objSearch.SupplierCityName = SupplierCityForBind;

                //For search
                if (Supplier_IDByProductSearch != Guid.Empty)
                    _objSearch.Supplier_ID = Supplier_IDByProductSearch;
                if (!string.IsNullOrWhiteSpace(SupplierCountryForSearch))
                    _objSearch.SystemCountryName = SupplierCountryForSearch;
                if (!string.IsNullOrWhiteSpace(SupplierCityForSearch))
                    _objSearch.SystemCityName = SupplierCityForSearch;
                if (!string.IsNullOrWhiteSpace(SupplierKeyWorForSearch))
                    _objSearch.KeyWord = SupplierKeyWorForSearch;



                _objSearch.StatusExcept = "MAPPED";
                frmvwSupplierActivtiy.DataSource = null;
                frmvwSupplierActivtiy.DataBind();
                var result = _mapping.GetActivitySupplierProductMappingSearch(_objSearch);
                if (result != null && result.Count > 0)
                {
                    if (result[0].TotalCount > 1)
                        btnNextByProduct.Enabled = true;
                    TotalCountSupplierActivityByProduct = Convert.ToInt32(result[0].TotalCount);
                    lblTotalCountActivityByProduct.Text = Convert.ToString(TotalCountSupplierActivityByProduct);
                    frmvwSupplierActivtiy.DataSource = result;
                    frmvwSupplierActivtiy.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BindSupplierActivityDropDown()
        {
            try
            {
                //Get selected Activity details to find supplier activity list
                DC_Acitivity_SupplierProductMapping_Search_RQ _objSearch = new DC_Acitivity_SupplierProductMapping_Search_RQ();
                if (!string.IsNullOrWhiteSpace(SupplierCountryForBind))
                    _objSearch.SystemCountryName = SupplierCountryForBind;
                if (!string.IsNullOrWhiteSpace(SupplierCityForBind))
                    _objSearch.SystemCityName = SupplierCityForBind;

                var result_temp = _mapping.GetActivitySupplierProductMappingSearchForDDL(_objSearch);
                ddlSupplierFilterforMappingByProduct.Items.Clear();
                if (result_temp != null && result_temp.Count > 0)
                {
                    var result = result_temp.Select(m => new { m.Supplier_ID, m.SupplierName }).Distinct().ToList();
                    ddlSupplierFilterforMappingByProduct.DataSource = result;
                    ddlSupplierFilterforMappingByProduct.DataTextField = "SupplierName";
                    ddlSupplierFilterforMappingByProduct.DataValueField = "Supplier_ID";
                    ddlSupplierFilterforMappingByProduct.DataBind();
                }
                else
                {
                    ddlSupplierFilterforMappingByProduct.DataSource = null;
                    ddlSupplierFilterforMappingByProduct.DataBind();
                }
                ddlSupplierFilterforMappingByProduct.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlSystemCountryActivityByProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSystemCityActivityByProduct.Items.Clear();
            if (ddlSystemCountryActivityByProduct.SelectedItem.Value != "0")
            {
                ddlSystemCityActivityByProduct.DataSource = masterSVc.GetMasterCityData(ddlSystemCountryActivityByProduct.SelectedItem.Value);
                ddlSystemCityActivityByProduct.DataValueField = "City_ID";
                ddlSystemCityActivityByProduct.DataTextField = "Name";
                ddlSystemCityActivityByProduct.DataBind();
            }
            ddlSystemCityActivityByProduct.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        #endregion

        #endregion


    }
}