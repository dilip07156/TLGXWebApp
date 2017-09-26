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

namespace TLGX_Consumer.controls.activity.ManageActivityMaster
{
    public partial class ProductInformation : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMasterData = new MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        Controller.ActivitySVC activitySVC = new ActivitySVC();
        public Guid Activity_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<MDMSVC.DC_Activity> lst = new List<MDMSVC.DC_Activity>();
                lst.Add(new MDMSVC.DC_Activity() { });
                frmProductInfo.DataSource = lst;
                frmProductInfo.DataBind();

                DropDownList ddlProductCategory = (DropDownList)frmProductInfo.FindControl("ddlProductCategory");
                DropDownList ddlProductCatSubType = (DropDownList)frmProductInfo.FindControl("ddlProductCatSubType");
                DropDownList ddlProductType = (DropDownList)frmProductInfo.FindControl("ddlProductType");
                DropDownList ddlCountry = (DropDownList)frmProductInfo.FindControl("ddlCountry");
                DropDownList ddlModeOfTransport = (DropDownList)frmProductInfo.FindControl("ddlModeOfTransport");
                DropDownList ddlCompanyRating = (DropDownList)frmProductInfo.FindControl("ddlCompanyRating");
                DropDownList ddlProductRating = (DropDownList)frmProductInfo.FindControl("ddlProductRating");
                DropDownList ddlAffiliation = (DropDownList)frmProductInfo.FindControl("ddlAffiliation");
                TextBox txtActivityID = (TextBox)frmProductInfo.FindControl("txtActivityID");

                if (Request.QueryString["Activity_Id"] != null)
                {
                    Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                    txtActivityID.Text = Convert.ToString(Activity_Id);
                }
                else
                {
                    txtActivityID.Text = null;
                }
                fillcountrydropdown("search", ddlCountry);
                fillproductcaterogytype(ddlProductCategory);
                fillproductcategorysubtype(ddlProductCatSubType);
                fillProductType(ddlProductType);
                fillModeOftransport(ddlModeOfTransport);
                fillCompanyRating(ddlCompanyRating);
                fillAffiliation(ddlAffiliation);
                fillProductRating(ddlProductRating);
                dvPageSize.Visible = false;
                gvProductInfo.Visible = false;
            }
        }

        private void fillProductRating(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Product_Rating").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private void fillAffiliation(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Affilliation").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private void fillCompanyRating(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Company_Rating").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private void fillModeOftransport(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Mode_Of_Transport").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void fillcountrydropdown(string source, DropDownList ddl)
        {
            var result = _objMasterData.GetAllCountries();
            if (source == "search")
            {
                //ddl.Items.Clear();
                ddl.DataSource = result;
                ddl.DataValueField = "Country_Id";
                ddl.DataTextField = "Country_Name";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
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
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
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
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
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
                ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
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
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = (DropDownList)frmProductInfo.FindControl("ddlCountry");
            DropDownList ddlCity = (DropDownList)frmProductInfo.FindControl("ddlCity");
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
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //searchActivityMaster(0, Convert.ToInt32(ddlPageSize.SelectedValue));
        }
        protected void frmProductInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtActivityID = (TextBox)frmProductInfo.FindControl("txtActivityID");
            TextBox txtProductName = (TextBox)frmProductInfo.FindControl("txtProductName");
            TextBox txtCommPID = (TextBox)frmProductInfo.FindControl("txtCommPID");
            TextBox txtCompanyProductID = (TextBox)frmProductInfo.FindControl("txtCompanyProductID");
            TextBox txtFinanceControlID = (TextBox)frmProductInfo.FindControl("txtFinanceControlID");
            TextBox txtDisplayName = (TextBox)frmProductInfo.FindControl("txtDisplayName");
            DropDownList ddlProductCategory = (DropDownList)frmProductInfo.FindControl("ddlProductCategory");
            DropDownList ddlProductCatSubType = (DropDownList)frmProductInfo.FindControl("ddlProductCatSubType");
            DropDownList ddlProductType = (DropDownList)frmProductInfo.FindControl("ddlProductType");
            DropDownList ddlCountry = (DropDownList)frmProductInfo.FindControl("ddlCountry");
            DropDownList ddlCity = (DropDownList)frmProductInfo.FindControl("ddlCity");
            DropDownList ddlAffiliation = (DropDownList)frmProductInfo.FindControl("ddlAffiliation");
            DropDownList ddlProductRating = (DropDownList)frmProductInfo.FindControl("ddlProductRating");
            DropDownList ddlCompanyRating = (DropDownList)frmProductInfo.FindControl("ddlCompanyRating");
            DropDownList ddlModeOfTransport = (DropDownList)frmProductInfo.FindControl("ddlModeOfTransport");
            dvMsg.Visible = true;


            if (e.CommandName.ToString() == "SaveProduct")
            {
                MDMSVC.DC_Activity _obj = new MDMSVC.DC_Activity();

                if (Request.QueryString["Activity_Id"] != null)
                {
                    _obj.Activity_Id = new Guid(txtActivityID.Text);
                    _obj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    _obj.Create_User= System.Web.HttpContext.Current.User.Identity.Name;
                }
                if (txtProductName.Text != string.Empty)
                    _obj.Product_Name = txtProductName.Text;
                if (txtCommPID.Text != string.Empty)
                    _obj.CommonProductID = Convert.ToInt32(txtCommPID.Text);
                if (txtCompanyProductID.Text != string.Empty)
                    _obj.CompanyProductID = Convert.ToInt32(txtCompanyProductID.Text);
                if (txtFinanceControlID.Text != string.Empty)
                    _obj.FinanceProductID = Convert.ToInt32(txtFinanceControlID.Text);
                if (txtDisplayName.Text != string.Empty)
                    _obj.Display_Name = txtDisplayName.Text;
                if (ddlProductCategory.SelectedIndex != 0)
                    _obj.ProductCategory = ddlProductCategory.SelectedItem.Text;
                if (ddlProductCatSubType.SelectedIndex != 0)
                    _obj.ProductCategorySubType = ddlProductCatSubType.SelectedItem.Text;
                if (ddlProductType.SelectedIndex != 0)
                    _obj.ProductType = ddlProductType.SelectedItem.Text;
                if (ddlCountry.SelectedIndex != 0)
                {
                    _obj.Country = ddlCountry.SelectedItem.Text;
                    _obj.Country_Id = Guid.Parse(ddlCountry.SelectedValue);
                }
                if (ddlCity.SelectedIndex != 0)
                {
                    _obj.City = ddlCity.SelectedItem.Text;
                    _obj.City_Id = Guid.Parse(ddlCity.SelectedValue);
                }
                if (ddlAffiliation.SelectedIndex != 0)
                    _obj.Affiliation = ddlAffiliation.SelectedItem.Text;
                if (ddlProductRating.SelectedIndex != 0)
                    _obj.ProductRating = Convert.ToInt32(ddlProductRating.SelectedItem.Text);
                if (ddlCompanyRating.SelectedIndex != 0)
                    _obj.CompanyRating = Convert.ToInt32(ddlCompanyRating.SelectedItem.Text);
                if (ddlModeOfTransport.SelectedIndex != 0)
                    _obj.Mode_Of_Transport = ddlModeOfTransport.SelectedItem.Text;

                MDMSVC.DC_Message _msg = activitySVC.AddUpdateProductInfo(_obj);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Duplicate)
                {
                    MDMSVC.DC_Activity_Search_RQ _objDup = new MDMSVC.DC_Activity_Search_RQ();
                    _objDup.Name = txtProductName.Text;
                    var res = activitySVC.ActivitySearch(_objDup);
                    if (res != null && res.Count != 0)
                    {
                        if (res.Count > 0)
                        {
                            grdSearchResults.VirtualItemCount = Convert.ToInt32(res[0].TotalRecord);
                            grdSearchResults.DataSource = res;
                            grdSearchResults.PageIndex = 0;
                            grdSearchResults.PageSize = 1;
                            grdSearchResults.DataBind();
                            grdSearchResults.Visible = true;
                        }
                    }
                    else
                    {
                        grdSearchResults.DataSource = null;
                        grdSearchResults.DataBind();
                    }

                    dvMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
                else
                {
                    dvMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
                //frmVwNewActivity.ChangeMode(FormViewMode.Insert);
            }
        }


        //private void searchActivityMaster(int pageindex, int pagesize)
        //{
        //    try
        //    {
        //        MDMSVC.DC_Activity_Search_RQ _objSearch = new MDMSVC.DC_Activity_Search_RQ();

        //        if (ddlCountry.SelectedIndex != 0)
        //            _objSearch.Country = ddlCountry.SelectedItem.Text;
        //        if (ddlCity.SelectedIndex != 0)
        //            _objSearch.City = ddlCity.SelectedItem.Text;
        //        _objSearch.PageNo = pageindex;
        //        _objSearch.PageSize = pagesize;

        //        if (ddlProductCategoryType.SelectedIndex != 0)
        //            _objSearch.ProductCategory = ddlProductCategoryType.SelectedItem.Text;
        //        if (ddlProductCategorySubType.SelectedIndex != 0)
        //            _objSearch.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text;
        //        if (ddlProductType.SelectedIndex != 0)
        //            _objSearch.ProductType = ddlProductType.SelectedItem.Text;
        //        if (ddlProductSubType.SelectedIndex != 0)
        //            _objSearch.ProductNameSubType = ddlProductSubType.SelectedItem.Text;
        //        //if(ddlProductCategoryType.SelectedValue!="0")
        //        //    _objSearch.ProductCategory = ddlProductCategoryType.SelectedItem.Text;
        //        //if(ddlProductSubType.SelectedValue!="0")
        //        //    _objSearch.ProductSubType = ddlProductSubType.SelectedItem.Text;
        //        //var res = masterSVc.GetActivityMaster(_objSearch);

        //        var res = activitySVC.ActivitySearch(_objSearch);
        //        if (res != null && res.Count != 0)
        //        {
        //            if (res.Count > 0)
        //            {
        //                gvActivitySearch.VirtualItemCount = Convert.ToInt32(res[0].TotalRecord);
        //                lblTotalRecords.Text = res[0].TotalRecord.ToString();
        //                gvActivitySearch.DataSource = res;
        //                gvActivitySearch.PageIndex = pageindex;
        //                gvActivitySearch.PageSize = pagesize;
        //                gvActivitySearch.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            gvActivitySearch.DataSource = null;
        //            gvActivitySearch.DataBind();
        //            lblTotalRecords.Text = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
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
        //    fillcountrydropdown("search", ddlCountry);
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
