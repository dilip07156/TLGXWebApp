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
            }
        }
        private void LoadMasters()
        {
            fillcoutries();
            fillproductcategorysubtype(ddlProductCategorySubType);
            fillSupplierList(ddlSupplier);
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
            Guid CountryId;
            ddlCity.Items.Clear();
            if (Guid.TryParse(ddlCountry.SelectedValue, out CountryId))
            {
                fillcities(CountryId);
            }
            InsertDefaultValuesInDDL(ddlCity);
            ddlCity.Focus();
        }
        private void fillproductcategorysubtype(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                InsertDefaultValuesInDDL(ddl);
            }
            catch
            {
                throw;
            }
        }
        protected void ddlProductCategorySubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid gSubCategory;
            ddlProductType.Items.Clear();
            ddlProductSubType.Items.Clear();

            if (Guid.TryParse(ddlProductCategorySubType.SelectedValue, out gSubCategory))
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
            Guid gProductType;
            ddlProductSubType.Items.Clear();

            if (Guid.TryParse(ddlProductType.SelectedValue, out gProductType))
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
            ddl.DataSource = masterSVc.GetSupplier(new MDMSVC.DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue });
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

                if (!string.IsNullOrWhiteSpace(txtProductName.Text))
                    _objSearch.ProductName = txtProductName.Text;

                _objSearch.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text;

                _objSearch.ProductType = ddlProductType.SelectedItem.Text;

                _objSearch.ProductNameSubType = ddlProductSubType.SelectedItem.Text;

                var res = activitySVC.GetActivityFlavour(_objSearch);
                if (res != null && res.Count != 0)
                {
                    if (res.Count > 0)
                    {
                        gvActivitySearch.VirtualItemCount = Convert.ToInt32(res[0].TotalRecords);
                        lblTotalRecords.Text = res[0].TotalRecords.ToString();
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
            ddlCountry.SelectedIndex = 0;

            ddlCity.Items.Clear();
            InsertDefaultValuesInDDL(ddlCity);

            ddlProductCategorySubType.SelectedIndex = 0;

            ddlProductType.Items.Clear();
            InsertDefaultValuesInDDL(ddlProductType);

            ddlProductSubType.Items.Clear();
            InsertDefaultValuesInDDL(ddlProductSubType);

            //ddlStatus.SelectedIndex = 0;

            ddlSupplier.SelectedIndex = 0;

            ddlPageSize.SelectedIndex = 0;

            txtProductName.Text = string.Empty;
            
            lblTotalRecords.Text = string.Empty;
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