﻿using System;
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
                fillcountrydropdown("search", ddlCountry);
                fillproductcaterogytype(ddlProductCategoryType);
                fillproductcategorysubtype(ddlProductCategorySubType);
                fillProductType(ddlProductType);
                fillproductsubtype(ddlProductSubType);
                fillstatusdropdown(ddlStatus);
                dvPageSize.Visible = false;
                gvActivitySearch.Visible = false;
            }
        }
        private void fillstatusdropdown(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = _objMasterData.GetAllStatuses();
            ddl.DataTextField = "Status_Name";
            ddl.DataValueField = "Status_Short";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddl.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText("ACTIVE"));
        }
        private void fillcountrydropdown(string source, DropDownList ddl)
        {
            var result = _objMasterData.GetAllCountries();
            if (source == "search")
            {
                ddl.Items.Clear();
                ddl.DataSource = result;
                ddl.DataValueField = "Country_Id";
                ddl.DataTextField = "Country_Name";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---", ""));
            }
        }
        private void fillcitydropdown(string source, string Country_ID, DropDownList ddl)
        {
            if (source == "search")
            {
                if (Country_ID != "")
                {
                    var result = _objMasterData.GetMasterCityData(Country_ID);
                    ddl.DataSource = result;
                    ddl.DataValueField = "City_Id";
                    ddl.DataTextField = "Name";
                    ddl.DataBind();
                }
                else
                {
                    ddl.Items.Clear();
                }
                ddl.Items.Insert(0, new ListItem("---ALL---", ""));
            }
        }
        private void fillproductcaterogytype(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void fillproductcategorysubtype(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategorySubType").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---"));
            }
            catch
            {
                throw;
            }
        }
        private void fillProductType(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---"));
        }
        private void fillproductsubtype(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductSubType").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---"));
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
                fillcitydropdown("search", ddlCountry.SelectedValue, ddlCity);
            }
            else
            {
                fillcitydropdown("search", "", ddlCity);
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
        
        protected void frmVwNewActivity_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtProductName = (TextBox)frmVwNewActivity.FindControl("txtProductName");
            DropDownList ddlCategorySubType = (DropDownList)frmVwNewActivity.FindControl("frmddlCategorySubType");
            DropDownList ddlProductType = (DropDownList)frmVwNewActivity.FindControl("frmddlProductType");
            DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
            DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");

            if (e.CommandName == "ResetActivity")
            {
                txtProductName.Text = string.Empty;
                ddlCategorySubType.SelectedIndex = 0;
                ddlProductType.SelectedIndex = 0;
                ddlCountry.SelectedIndex = 0;
                ddlCity.SelectedIndex = 0;
            }
            if(e.CommandName == "AddActivity")
            {

            }
        }

        protected void btnNewActivity_Click(object sender, EventArgs e)
        {
            DropDownList ddlCategorySubType = (DropDownList)frmVwNewActivity.FindControl("frmddlCategorySubType");
            DropDownList ddlProductType = (DropDownList)frmVwNewActivity.FindControl("frmddlProductType");
            DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
            DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");

            fillproductcategorysubtype(ddlCategorySubType);
            fillProductType(ddlProductType);
            fillcountrydropdown("search", ddlCountry);
        }

        protected void frmddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = (DropDownList)frmVwNewActivity.FindControl("frmddlCountry");
            DropDownList ddlCity = (DropDownList)frmVwNewActivity.FindControl("frmddlCity");

            if (ddlCountry.SelectedValue != "")
            {
                fillcitydropdown("search", ddlCountry.SelectedValue, ddlCity);
            }
            else
            {
                fillcitydropdown("search", "", ddlCity);
            }
            ddlCity.Focus();
        }
    }
}