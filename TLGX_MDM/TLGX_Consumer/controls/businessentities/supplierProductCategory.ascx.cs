using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierProductCategory : System.Web.UI.UserControl
    {
        #region Variables
        public static Guid mySupplier_Id = Guid.Empty;
        MasterDataSVCs _objMaster = new MasterDataSVCs();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                BindPageData();
            }
            //var businessEntityDay = new businessEntityDAL();
            //grdSupplierProductCategory.DataSource = businessEntityDay.GetSupplierProductTypes(mySupplier_Id);
            //grdSupplierProductCategory.DataBind();

        }

        private void BindPageData()
        {
            try
            {
                frmSupplierProductCategory.ChangeMode(FormViewMode.Insert);
                DropDownList ddlproductCategoryBind = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategory");
                DropDownList ddlProductCategorySubTypeBind = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategorySubType");
                BindProductCategory(ddlproductCategoryBind);
                BindSupplierProductCategoryGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BindSupplierProductCategoryGrid()
        {
            var result = _objMaster.GetProductCategoryBySupplier(new MDMSVC.DC_Supplier_ProductCategory() { Supplier_Id = mySupplier_Id });
            grdSupplierProductCategory.DataSource = result;
            grdSupplierProductCategory.DataBind();
        }


        private void BindProductCategory(DropDownList ddlProductCategoryBind)
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "ProductCategory" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlProductCategoryBind.Items.Clear();
                    ddlProductCategoryBind.DataSource = result;
                    ddlProductCategoryBind.DataTextField = "AttributeValue";
                    ddlProductCategoryBind.DataValueField = "MasterAttributeValue_Id";
                    ddlProductCategoryBind.DataBind();
                    ddlProductCategoryBind.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                }
        }

        protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlproductCategoryBind = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategory");
            DropDownList ddlProductCategorySubTypeBind = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategorySubType");
            BindProductCategorySubType(ddlProductCategorySubTypeBind, ddlproductCategoryBind.SelectedValue.ToString());
            supplierProduct.Update();
            dvMsg.Visible = false;
        }

        private void BindProductCategorySubType(DropDownList ddlProductCategorySubTypeBind, string val)
        {
            Guid ddlProductCategoryValue = Guid.Parse(val);
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "ProductCategorySubType", ParentAttributeValue_Id = ddlProductCategoryValue });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlProductCategorySubTypeBind.Items.Clear();
                    ddlProductCategorySubTypeBind.DataSource = result;
                    ddlProductCategorySubTypeBind.DataTextField = "AttributeValue";
                    ddlProductCategorySubTypeBind.DataValueField = "MasterAttributeValue_Id";
                    ddlProductCategorySubTypeBind.DataBind();
                    ddlProductCategorySubTypeBind.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }
        }

        protected void frmSupplierProductCategory_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
            DropDownList ddlProductCategory = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategory");
            DropDownList ddlProductCategorySubType = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategorySubType");
            HtmlInputCheckBox chckbIsDefaultSupplier = (HtmlInputCheckBox)frmSupplierProductCategory.FindControl("chckbIsDefaultSupplier");
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
            dvMsg.Visible = true;
            if (e.CommandName.ToString() == "Add")
            {
                MDMSVC.DC_Supplier_ProductCategory newObj = new MDMSVC.DC_Supplier_ProductCategory
                {
                    ProductCategory = ddlProductCategory.SelectedItem.Text,
                    ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text,
                    Create_Date = DateTime.Now,
                    Supplier_Id = mySupplier_Id,
                    IsDefaultSupplier = chckbIsDefaultSupplier.Checked,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                    Status = "ACT"

                };
                _msg = _objMaster.AddUpdateSupplier_ProductCategory(newObj);
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierProductCategory.ChangeMode(FormViewMode.Insert);
                    frmSupplierProductCategory.DataBind();
                    BindPageData();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }


            if (e.CommandName.ToString() == "Modify")
            {

                Guid myRow_Id = Guid.Parse(grdSupplierProductCategory.SelectedDataKey.Value.ToString());

                MDMSVC.DC_Supplier_ProductCategory newObj = new MDMSVC.DC_Supplier_ProductCategory
                {
                    Supplier_ProductCategory_Id = myRow_Id,
                    Supplier_Id = mySupplier_Id,
                    ProductCategory = ddlProductCategory.SelectedItem.Text,
                    ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text,
                    IsDefaultSupplier = chckbIsDefaultSupplier.Checked,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                    Status = "ACT"
                };

                _msg = _objMaster.AddUpdateSupplier_ProductCategory(newObj);
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierProductCategory.ChangeMode(FormViewMode.Insert);
                    frmSupplierProductCategory.DataBind();
                    BindPageData();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }

        protected void grdSupplierProductCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }

        protected void grdSupplierProductCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                frmSupplierProductCategory.ChangeMode(FormViewMode.Edit);
                var result = _objMaster.GetProductCategoryBySupplier(new MDMSVC.DC_Supplier_ProductCategory() { Supplier_ProductCategory_Id = myRow_Id });
                frmSupplierProductCategory.DataSource = result;
                frmSupplierProductCategory.DataBind();
                DropDownList ddlProductCategory = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategory");
                DropDownList ddlProductCategorySubType = (DropDownList)frmSupplierProductCategory.FindControl("ddlProductCategorySubType");
                HtmlInputCheckBox chckbIsDefaultSupplier = (HtmlInputCheckBox)frmSupplierProductCategory.FindControl("chckbIsDefaultSupplier");
                BindProductCategory(ddlProductCategory);

                if (result != null)
                    if (result.Count > 0)
                    {
                        if (ddlProductCategory != null)
                            ddlProductCategory.Items.FindByText(result[0].ProductCategory).Selected = true;
                        BindProductCategorySubType(ddlProductCategorySubType, ddlProductCategory.SelectedValue.ToString());
                        if (ddlProductCategorySubType != null)
                            ddlProductCategorySubType.Items.FindByText(result[0].ProductCategorySubType).Selected = true;
                        if (chckbIsDefaultSupplier != null)
                            chckbIsDefaultSupplier.Checked = Convert.ToBoolean(result[0].IsDefaultSupplier);
                    }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                _msg = _objMaster.Supplier_ProductCategorySoftDelete(new MDMSVC.DC_Supplier_ProductCategory()
                {
                    Supplier_ProductCategory_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierProductCategory.ChangeMode(FormViewMode.Insert);
                    frmSupplierProductCategory.DataBind();
                    BindSupplierProductCategoryGrid();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Product Category has been deleted successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
                BindPageData();
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                _msg = _objMaster.Supplier_ProductCategorySoftDelete(new MDMSVC.DC_Supplier_ProductCategory()
                {
                    Supplier_ProductCategory_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierProductCategory.ChangeMode(FormViewMode.Insert);
                    frmSupplierProductCategory.DataBind();
                    BindSupplierProductCategoryGrid();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Product Category has been retrived successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
                BindPageData();
            }
        }
    }
}