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
    public partial class manageSupplier : System.Web.UI.UserControl
    {


        // used for retrieving drop down list attribute values from masters
        MasterDataDAL MasterData = new MasterDataDAL();
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        public static string AttributeOptionFor = "SupplierInfo";
        public static Guid mySupplier_Id = Guid.Empty;
        private void BindMainSupplierData()
        {
            DropDownList ddlSupplierType = (DropDownList)frmSupplierDetail.FindControl("ddlSupplierType");

            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "SupplierType" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlSupplierType.Items.Clear();
                    ddlSupplierType.DataSource = result;
                    ddlSupplierType.DataTextField = "AttributeValue";
                    ddlSupplierType.DataValueField = "MasterAttributeValue_Id";
                    ddlSupplierType.DataBind();
                    ddlSupplierType.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                }
        }
        private void BindStatus()
        {
            DropDownList ddlStatusEdit = (DropDownList)frmSupplierDetail.FindControl("ddlStatusEdit");

            var result = _objMaster.GetAllStatuses();
            if (result != null)
                if (result.Count > 0)
                {
                    ddlStatusEdit.Items.Clear();
                    ddlStatusEdit.DataSource = result;
                    ddlStatusEdit.DataTextField = "Status_Name";
                    ddlStatusEdit.DataValueField = "Status_Short";
                    ddlStatusEdit.DataBind();
                    ddlStatusEdit.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
                mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                FillPageData(mySupplier_Id);
            }

        }

        private void FillPageData(Guid mySupplier_Id)
        {
            try
            {
                frmSupplierDetail.ChangeMode(FormViewMode.Edit);
                var result = _objMaster.GetSupplier(new MDMSVC.DC_Supplier_Search_RQ() { Supplier_Id = mySupplier_Id });
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        frmSupplierDetail.DataSource = result;
                        frmSupplierDetail.DataBind();
                        BindMainSupplierData();
                        BindStatus();
                        DropDownList ddlStatusEdit = (DropDownList)frmSupplierDetail.FindControl("ddlStatusEdit");
                        DropDownList ddlSupplierType = (DropDownList)frmSupplierDetail.FindControl("ddlSupplierType");

                        if(result[0].StatusCode==string.Empty)
                            ddlStatusEdit.Items.FindByText("--Select--").Selected = true;
                        else
                            ddlStatusEdit.Items.FindByText(result[0].StatusCode).Selected = true;
                        if (result[0].SupplierType == string.Empty)
                            ddlSupplierType.Items.FindByText("--Select--").Selected = true;
                        else
                            ddlSupplierType.Items.FindByText(result[0].SupplierType).Selected = true;
                        //ddlStatusEdit.Items.FindByText(result[0].StatusCode ?? "--Select--").Selected = true;
                        //ddlSupplierType.Items.FindByText(result[0].SupplierType ?? "--Select--").Selected = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected void frmSupplierDetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            string supplierType;
            DropDownList ddlStatusEdit = (DropDownList)frmSupplierDetail.FindControl("ddlStatusEdit");
            DropDownList ddlSupplierType = (DropDownList)frmSupplierDetail.FindControl("ddlSupplierType");
            TextBox txtNameSupplierEdit = (TextBox)frmSupplierDetail.FindControl("txtNameSupplierEdit");
            TextBox txtCodeSupplierEdit = (TextBox)frmSupplierDetail.FindControl("txtCodeSupplierEdit");
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();

            if (e.CommandName == "EditCommand")
            {

                if (ddlSupplierType.SelectedIndex == 0)
                    supplierType = string.Empty;
                else
                    supplierType = ddlSupplierType.SelectedItem.Text;

                _msg = _objMaster.AddUpdateSupplier(new MDMSVC.DC_Supplier()
                {
                    Supplier_Id = mySupplier_Id,
                    Name = txtNameSupplierEdit.Text.Trim(),
                    Code = txtCodeSupplierEdit.Text.Trim(),
                    SupplierType = supplierType,
                    StatusCode = ddlStatusEdit.SelectedItem.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name

                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierDetail.ChangeMode(FormViewMode.Edit);
                    frmSupplierDetail.DataBind();
                    BootstrapAlert.BootstrapAlertMessage(dvMsgUpdateSupplierDetails, _msg.StatusMessage, BootstrapAlertType.Success);
                    FillPageData(mySupplier_Id);
                }
                else
                {
                    hdnFlag.Value = "false";
                    BootstrapAlert.BootstrapAlertMessage(dvMsgUpdateSupplierDetails, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }

            }
        }
    }
}