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

namespace TLGX_Consumer.controls.activity
{
    public partial class SearchActivityMasterControl : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMasterData = new MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        Controller.ActivitySVC activitySVC = new ActivitySVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillcountrydropdown("search");
                fillproductcaterogytype();
                fillproductcategorysubtype();
                fillProductType();
                fillproductsubtype();
                fillstatusdropdown();
                dvPageSize.Visible = false;
                gvActivitySearch.Visible = false;
            }
        }
        private void fillstatusdropdown()
        {
            ddlStatus.Items.Clear();
            ddlStatus.DataSource = _objMasterData.GetAllStatuses();
            ddlStatus.DataTextField = "Status_Name";
            ddlStatus.DataValueField = "Status_Short";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText("ACTIVE"));
        }
        private void fillcountrydropdown(string source)
        {
            var result = _objMasterData.GetAllCountries();
            if (source == "search")
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = result;
                ddlCountry.DataValueField = "Country_Id";
                ddlCountry.DataTextField = "Country_Name";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("---ALL---", ""));
            }
        }
        private void fillcitydropdown(string source, string Country_ID)
        {
            if (source == "search")
            {
                if (Country_ID != "")
                {
                    var result = _objMasterData.GetMasterCityData(Country_ID);
                    ddlCity.DataSource = result;
                    ddlCity.DataValueField = "City_Id";
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataBind();
                }
                else
                {
                    ddlCity.Items.Clear();
                }
                ddlCity.Items.Insert(0, new ListItem("---ALL---", ""));
            }
        }
        private void fillproductcaterogytype()
        {
            try
            {
                ddlProductCategoryType.Items.Clear();
                ddlProductCategoryType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
                ddlProductCategoryType.DataTextField = "AttributeValue";
                ddlProductCategoryType.DataValueField = "MasterAttributeValue_Id";
                ddlProductCategoryType.DataBind();
                ddlProductCategoryType.Items.Insert(0, new ListItem("---ALL---"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void fillproductcategorysubtype()
        {
            try
            {
                ddlProductCategorySubType.Items.Clear();
                ddlProductCategorySubType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategorySubType").MasterAttributeValues;
                ddlProductCategorySubType.DataTextField = "AttributeValue";
                ddlProductCategorySubType.DataValueField = "MasterAttributeValue_Id";
                ddlProductCategorySubType.DataBind();
                ddlProductCategorySubType.Items.Insert(0, new ListItem("---ALL---"));
            }
            catch
            {
                throw;
            }
        }
        private void fillProductType()
        {
            ddlProductType.Items.Clear();
            ddlProductType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            ddlProductType.DataTextField = "AttributeValue";
            ddlProductType.DataValueField = "MasterAttributeValue_Id";
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, new ListItem("---ALL---"));
        }
        private void fillproductsubtype()
        {
            ddlProductSubType.Items.Clear();
            ddlProductSubType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductSubType").MasterAttributeValues;
            ddlProductSubType.DataTextField = "AttributeValue";
            ddlProductSubType.DataValueField = "MasterAttributeValue_Id";
            ddlProductSubType.DataBind();
            ddlProductSubType.Items.Insert(0, new ListItem("---ALL---"));
        }
        private void searchActivityMaster(int pageindex, int pagesize)
        {
            try
            {
                MDMSVC.DC_Activity_Search_RQ _objSearch = new MDMSVC.DC_Activity_Search_RQ();

                if (ddlCountry.SelectedIndex != 0)
                    _objSearch.Country = ddlCountry.SelectedItem.Text;
                if (ddlCity.SelectedIndex != 0)
                    _objSearch.City = ddlCity.SelectedItem.Text;
                _objSearch.PageNo = pageindex;
                _objSearch.PageSize = pagesize;

                if (ddlProductCategoryType.SelectedIndex != 0)
                    _objSearch.ProductCategory = ddlProductCategoryType.SelectedItem.Text;
                if (ddlProductCategorySubType.SelectedIndex != 0)
                    _objSearch.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text;
                if (ddlProductType.SelectedIndex != 0)
                    _objSearch.ProductType = ddlProductType.SelectedItem.Text;
                if (ddlProductSubType.SelectedIndex != 0)
                    _objSearch.ProductNameSubType = ddlProductSubType.SelectedItem.Text;
                //if(ddlProductCategoryType.SelectedValue!="0")
                //    _objSearch.ProductCategory = ddlProductCategoryType.SelectedItem.Text;
                //if(ddlProductSubType.SelectedValue!="0")
                //    _objSearch.ProductSubType = ddlProductSubType.SelectedItem.Text;
                //var res = masterSVc.GetActivityMaster(_objSearch);

                var res = activitySVC.ActivitySearch(_objSearch);
                if (res != null && res.Count!=0)
                {
                    if (res.Count > 0)
                    {
                        gvActivitySearch.VirtualItemCount = Convert.ToInt32(res[0].TotalRecord);
                        lblTotalRecords.Text = res[0].TotalRecord.ToString();
                        gvActivitySearch.DataSource = res;
                        gvActivitySearch.PageIndex = pageindex;
                        gvActivitySearch.PageSize = pagesize;
                        gvActivitySearch.DataBind();
                    }
                }
                else
                {
                    gvActivitySearch.DataSource = null;
                    gvActivitySearch.DataBind();
                    lblTotalRecords.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void resetControls()
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("---ALL---", ""));
            ddlStatus.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
            ddlProductType.SelectedIndex = 0;
            ddlProductSubType.SelectedIndex = 0;
            ddlProductCategoryType.SelectedIndex = 0;
            ddlProductCategorySubType.SelectedIndex = 0;
            ddlPageSize.SelectedIndex = 0;
            txtProductName.Text = string.Empty;
            dvPageSize.Visible = false;
            gvActivitySearch.Visible = false;
            lblTotalRecords.Text = string.Empty;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchActivityMaster(0, Convert.ToInt32(ddlPageSize.SelectedValue));
            dvPageSize.Visible = true;
            gvActivitySearch.Visible = true;

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetControls();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "")
            {
                fillcitydropdown("search", ddlCountry.SelectedValue);
            }
            else
            {
                fillcitydropdown("search", "");
            }
            ddlCity.Focus();
        }

        protected void gvActivitySearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            searchActivityMaster(e.NewPageIndex, Convert.ToInt32(ddlPageSize.SelectedValue));
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchActivityMaster(0, Convert.ToInt32(ddlPageSize.SelectedValue));
        }

    }
}