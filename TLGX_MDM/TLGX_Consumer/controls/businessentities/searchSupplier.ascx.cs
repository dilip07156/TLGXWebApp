using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class SearchSupplier : System.Web.UI.UserControl
    {
        #region Variables
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        public DataTable dtSupplierData = new DataTable();
        public int intPageIndex = 0;
        public int intPageSize = 10;
        #endregion
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  bindSupplierSearchGrid(0);
                FillPageData();
            }
        }

        private void FillPageData()
        {
            try
            {
                //Fill Product Category
                FillProductCategory();
                FillSupplierType(ddlSupplierType);
                FillStatuses(ddlStatus);
                bindSupplierSearchGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FillSupplierType(DropDownList ddlSupplierTypeForBind)
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "SupplierType" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlSupplierTypeForBind.Items.Clear();
                    ddlSupplierTypeForBind.DataSource = result;
                    ddlSupplierTypeForBind.DataTextField = "AttributeValue";
                    ddlSupplierTypeForBind.DataValueField = "MasterAttributeValue_Id";
                    ddlSupplierTypeForBind.DataBind();
                    ddlSupplierTypeForBind.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }

        }

        private void FillStatuses(DropDownList ddlstatusforBind)
        {
            var result = _objMaster.GetAllStatuses();
            if (result != null)
                if (result.Count > 0)
                {
                    ddlstatusforBind.Items.Clear();
                    ddlstatusforBind.DataSource = result;
                    ddlstatusforBind.DataTextField = "Status_Name";
                    ddlstatusforBind.DataValueField = "Status_Short";
                    ddlstatusforBind.DataBind();
                    ddlstatusforBind.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }
        }

        private void FillProductCategory()
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "ProductCategory" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlProductCategory.Items.Clear();
                    ddlProductCategory.DataSource = result;
                    ddlProductCategory.DataTextField = "AttributeValue";
                    ddlProductCategory.DataValueField = "MasterAttributeValue_Id";
                    ddlProductCategory.DataBind();
                    ddlProductCategory.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }
        }
        #endregion
        #region Methods
        protected void bindSupplierSearchGrid()
        {
            //Find Serarch Filter
            string SupplierCode = txtSupplierCode.Text.Trim();
            string SupplierName = txtSupplierName.Text.Trim();

            string SupplierType = Convert.ToString(ddlSupplierType.SelectedValue);
            string ProductCategory = Convert.ToString(ddlProductCategory.SelectedValue);
            string ProductSubCategoryType = Convert.ToString(ddlProductCategorySubType.SelectedValue);
            string ddlStatusValue = Convert.ToString(ddlStatus.SelectedValue);


            MDMSVC.DC_Supplier_Search_RQ _objSearch = new MDMSVC.DC_Supplier_Search_RQ();
            _objSearch.PageSize = intPageSize;
            _objSearch.PageNo = intPageIndex;

            if (!string.IsNullOrWhiteSpace(SupplierCode))
                _objSearch.Code = SupplierCode;
            if (!string.IsNullOrWhiteSpace(SupplierName))
                _objSearch.Name = SupplierName;
            //if (SupplierType != "0")
            //    _objSearch.t = SupplierType;
            if (ProductCategory != "0")
                _objSearch.ProductCategory_ID = ProductCategory;
            if (ProductSubCategoryType != "0")
                _objSearch.CategorySubType_ID = ProductSubCategoryType;
            if (ddlStatusValue != "0")
                _objSearch.Code = SupplierCode;
            if (!string.IsNullOrWhiteSpace(SupplierCode))
                _objSearch.Code = SupplierCode;
            divDropdownForEntries.Visible = true;
            var result = _objMaster.GetSupplier(_objSearch);
            grdSupplierList.DataSource = result;
            grdSupplierList.PageIndex = intPageIndex;
            grdSupplierList.PageSize = intPageSize;
            if (result != null)
            {
                if (result.Count > 0)
                {
                    grdSupplierList.VirtualItemCount = result[0].TotalRecords;
                }
                else
                {
                    divDropdownForEntries.Visible = false;
                }
            }
            grdSupplierList.DataBind();
        }
        #endregion
        #region Controls Events
        protected void grdSupplierList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //            (e.NewSelectedIndex);
        }

        protected void grdSupplierList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            bindSupplierSearchGrid();
        }
        #endregion



        protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid ddlProductCategoryValue = Guid.Parse(ddlProductCategory.SelectedValue);
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "ProductCategorySubType", ParentAttributeValue_Id = ddlProductCategoryValue });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlProductCategorySubType.Items.Clear();
                    ddlProductCategorySubType.DataSource = result;
                    ddlProductCategorySubType.DataTextField = "AttributeValue";
                    ddlProductCategorySubType.DataValueField = "MasterAttributeValue_Id";
                    ddlProductCategorySubType.DataBind();
                    ddlProductCategorySubType.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            intPageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            bindSupplierSearchGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindSupplierSearchGrid();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmSupplierdetail.ChangeMode(FormViewMode.Insert);
            frmSupplierdetail.DataBind();
            DropDownList ddlSupplier = (DropDownList)frmSupplierdetail.FindControl("ddlSupplierTypeCreate");
            DropDownList ddlStatus = (DropDownList)frmSupplierdetail.FindControl("ddlStatusCreate");

            FillSupplierType(ddlSupplier);

            //Bind Manager DropDown for create or Update User
            FillStatuses(ddlStatus);


        }

        protected void frmSupplierdetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
            TextBox txtSupplierNameCreate = (TextBox)frmSupplierdetail.FindControl("txtSupplierNameCreate");
            TextBox txtSupplierCodeCreate = (TextBox)frmSupplierdetail.FindControl("txtSupplierCodeCreate");
            DropDownList ddlSupplierTypeCreate = (DropDownList)frmSupplierdetail.FindControl("ddlSupplierTypeCreate");
            DropDownList ddlStatusCreate = (DropDownList)frmSupplierdetail.FindControl("ddlStatusCreate");
            if (e.CommandName == "Add")
            {
                string strSupplierType = Convert.ToString(ddlSupplierTypeCreate.SelectedItem.Text.Trim());
                string strStatus = Convert.ToString(ddlStatusCreate.SelectedItem.Text.Trim());

                _msg = _objMaster.AddUpdateSupplier(new MDMSVC.DC_Supplier()
                {
                    Name = txtSupplierNameCreate.Text.Trim(),
                    Code = txtSupplierCodeCreate.Text.Trim(),
                    SupplierType = strSupplierType,
                    StatusCode = strStatus,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierdetail.DataBind();
                    hdnFlag.Value = "true";
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    hdnFlag.Value = "false";
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }
    }
}