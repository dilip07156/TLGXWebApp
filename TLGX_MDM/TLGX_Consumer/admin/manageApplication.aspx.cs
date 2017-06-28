using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.admin
{
    public partial class manageApplication : System.Web.UI.Page
    {
        #region Variable
        public int PageSize = 5;
        public int intPageIndex = 0;
        Controller.AdminSVCs _objAdminSVCs = new Controller.AdminSVCs();
        MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindPageData();
        }

        protected void BindPageData()
        {
            try
            {
                BindApplicationDetails(intPageIndex);
            }
            catch
            {
                throw;
            }
        }

        private void BindApplicationDetails(int PageIndex)
        {
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            var result = _objAdminSVCs.GetAllApplication(PageIndex, PageSize);
            grdListOfApplication.DataSource = result;
            grdListOfApplication.PageIndex = PageIndex;
            grdListOfApplication.PageSize = PageSize;
            if (result != null)
            {
                if (result.Count > 0)
                {
                    grdListOfApplication.VirtualItemCount = result[0].TotalRecords;
                }
            }
            //gvRoles.VirtualItemCount = 
            grdListOfApplication.DataBind();
        }

        protected void grdListOfApplication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int ID = int.Parse(e.CommandArgument.ToString());


            if (e.CommandName == "Select")
            {
                Guid ID = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                MDMSVC.DC_ApplicationMgmt _obj = new MDMSVC.DC_ApplicationMgmt();
                _obj.ApplicationId = ID;
                _obj.ApplicationName = System.Web.HttpUtility.HtmlDecode(grdListOfApplication.Rows[index].Cells[1].Text);
                _obj.Description = System.Web.HttpUtility.HtmlDecode(grdListOfApplication.Rows[index].Cells[2].Text);
                List<MDMSVC.DC_ApplicationMgmt> apmgmt = new List<MDMSVC.DC_ApplicationMgmt>();
                apmgmt.Add(_obj);
                frmApplicationdetail.ChangeMode(FormViewMode.Edit);
                frmApplicationdetail.DataSource = apmgmt;
                frmApplicationdetail.DataBind();
                TextBox txtApplicationName = (TextBox)frmApplicationdetail.FindControl("txtApplicationName");
                TextBox txtDescription = (TextBox)frmApplicationdetail.FindControl("txtDescription");
                txtApplicationName.Text = _obj.ApplicationName;
                txtDescription.Text = _obj.Description;
            }
        }

        protected void grdListOfApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            BindApplicationDetails(intPageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            BindApplicationDetails(intPageIndex);
        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmApplicationdetail.ChangeMode(FormViewMode.Insert);
            frmApplicationdetail.DataBind();
        }

        protected void frmApplicationdetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            // get the textbox value and use it to create a new record in Project
            TextBox txtApplicationName = (TextBox)frmApplicationdetail.FindControl("txtApplicationName");
            TextBox txtDescription = (TextBox)frmApplicationdetail.FindControl("txtDescription");
            MDMSVC.DC_ApplicationMgmt _obj = new MDMSVC.DC_ApplicationMgmt();
            if (e.CommandName == "Edit")
            {
                Guid myRow_Id = Guid.Parse(grdListOfApplication.SelectedDataKey.Value.ToString());
                _obj.ApplicationId = myRow_Id;
                _obj.ApplicationName = txtApplicationName.Text;
                _obj.Description = txtDescription.Text;
            }
            else if (e.CommandName == "Add")
            {
                _obj.ApplicationId = Guid.NewGuid();
                _obj.ApplicationName = txtApplicationName.Text;
                _obj.Description = txtDescription.Text;
            }
            _msg = _objAdminSVCs.AddUpdateApplication(_obj);
            if ((BootstrapAlertType)Convert.ToInt32(_msg.StatusCode) == BootstrapAlertType.Warning)
            {
                hdnFlag.Value = "false";
                BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(_msg.StatusCode));
            }
            else
            {
                frmApplicationdetail.ChangeMode(FormViewMode.Insert);
                frmApplicationdetail.DataBind();
                msgAlert.Attributes.Add("display", "none");
                hdnFlag.Value = "true";
                BindApplicationDetails(intPageIndex);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(_msg.StatusCode));
            }
        }
    }
}