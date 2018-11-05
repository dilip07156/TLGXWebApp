using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierStaticDownloadData : System.Web.UI.UserControl
    {
        public static Guid mySupplier_Id = Guid.Empty;
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        MDMSVC.DC_Supplier_StaticDataDownload RQ = new MDMSVC.DC_Supplier_StaticDataDownload();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }

        protected void gvSupplerStaticDownloadData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dvMsg.Style.Add("display", "none");
           
            if (e.CommandName.ToString() == "Select")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                RQ.SupplierCredentialsId = myRow_Id;

                var result = _objMaster.Supplier_StaticDownloadData_Get(RQ);
                if (result.Count > 0)
                {
                    ClearControls();
                    txtUrl.Text = result[0].URL == null ? string.Empty : result[0].URL;
                    txtUsername.Text = result[0].Username == null ? string.Empty : result[0].Username;
                    txtPassword.Text = result[0].Password == null ? string.Empty : result[0].Password;
                    txtDescription.InnerHtml = result[0].Description;
                }
                
                lnkButtonAddUpdate.Text = "Modify";
                lnkButtonAddUpdate.CommandName = "Modify";
                lnkButtonAddUpdate.CommandArgument = myRow_Id.ToString();
            }

            if (e.CommandName.ToString() == "SoftDelete")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                RQ.SupplierCredentialsId = myRow_Id;
                RQ.IsActive = false;
                var resultData =  _objMaster.Supplier_StaticDownloadData_AddUpdate(RQ);
                LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
            if(e.CommandName.ToString() == "UnDelete")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                RQ.SupplierCredentialsId = myRow_Id;
                RQ.IsActive = true;
                var resultData = _objMaster.Supplier_StaticDownloadData_AddUpdate(RQ);
                LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }

        protected void lnkButtonAddUpdate_Click(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            HtmlTextArea Description = (HtmlTextArea)this.FindControl("txtDescription");
            if (((LinkButton)sender).CommandName == "Add")
            {
                var _msg = _objMaster.Supplier_StaticDownloadData_AddUpdate(new MDMSVC.DC_Supplier_StaticDataDownload
                {
                    SupplierCredentialsId = Guid.NewGuid(),
                    URL = txtUrl.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Description = Description.Value.ToString(),
                    SupplierId = mySupplier_Id,
                   // CreateDate = DateTime.Now,
                    CreateUser= System.Web.HttpContext.Current.User.Identity.Name
                });
                ClearControls();
                LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }
            else if (((LinkButton)sender).CommandName == "Modify")
            {
                Guid myRow_Id = Guid.Parse(((LinkButton)sender).CommandArgument);
                var _msg = _objMaster.Supplier_StaticDownloadData_AddUpdate(new MDMSVC.DC_Supplier_StaticDataDownload
                {
                    SupplierCredentialsId = myRow_Id,
                    URL = txtUrl.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Description = Description.Value.ToString(),
                   // EditDate = DateTime.Now,
                    EditUser= System.Web.HttpContext.Current.User.Identity.Name
                });
                ClearControls();
                LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }
        }

        private void LoadDownloadData(int pagesize, int pageno)
        {
            RQ.SupplierId = mySupplier_Id;
            RQ.PageNo = pageno;
            RQ.PageSize = pagesize;
            var downloadData = _objMaster.Supplier_StaticDownloadData_Get(RQ);
            if (downloadData != null)
            {
                if (downloadData.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(downloadData[0].TotalRecords);
                    gvSupplerStaticDownloadData.VirtualItemCount = downloadData[0].TotalRecords ?? 0;
                    gvSupplerStaticDownloadData.PageIndex = pageno;
                    gvSupplerStaticDownloadData.PageSize = pagesize;
                }
                gvSupplerStaticDownloadData.DataSource = downloadData;
                gvSupplerStaticDownloadData.DataBind();
            }
        }

        protected void lnkButtonReset_Click(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            ClearControls();
        }

        private void ClearControls()
        {
            txtUrl.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtDescription.Value = string.Empty;
            lnkButtonAddUpdate.CommandArgument = string.Empty;
            lnkButtonAddUpdate.CommandName = "Add";
            lnkButtonAddUpdate.Text = "Add";
        }

        protected void gvSupplerStaticDownloadData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    foreach (LinkButton button in e.Row.Cells[5].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "SoftDelete")
                        {
                            button.OnClientClick = "return confirmDelete();";
                        }
                    }
                }
            }
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void gvSupplerStaticDownloadData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }
    }
}