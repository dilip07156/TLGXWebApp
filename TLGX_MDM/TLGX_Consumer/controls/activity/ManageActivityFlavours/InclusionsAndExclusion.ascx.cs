using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class InclusionsAndExclusion : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMasterData = new MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAddInclusionControls();
                BindInclusionsExclusions();
            }
        }

        protected void BindInclusionsExclusions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Inclusions_RQ _obj = new MDMSVC.DC_Activity_Inclusions_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = ActSVC.GetActivityInclusions(_obj);
            if (result != null)
            {
                var resInc = result.Where(w => w.IsInclusion == true).Select(s => s).ToList();
                gvActInclusionSearch.DataSource = resInc;
                gvActInclusionSearch.DataBind();
                lblTotalRecordsInclusions.Text = Convert.ToString(resInc.Count());

                var resExe = result.Where(w => w.IsInclusion == false).Select(s => s).ToList();
                gvActExclusionSearch.DataSource = resExe;
                gvActExclusionSearch.DataBind();
                lblTotalRecordsExclusions.Text = Convert.ToString(resExe.Count());
            }
            else
            {
                gvActInclusionSearch.DataSource = null;
                gvActInclusionSearch.DataBind();
                gvActExclusionSearch.DataSource = null;
                gvActExclusionSearch.DataBind();
            }
        }
        protected void BindAddInclusionControls()
        {
            fillInclusionFor(ddlInclusionFor);
            fillInclusionType(ddlInclusionType);
        }
        protected void ResetControls()
        {
            hdnId.Value = null;
            chkIsInclusion.Checked = false;
            ddlInclusionFor.SelectedIndex = 0;
            ddlInclusionType.SelectedIndex = 0;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;
            btnAdd.Text = "Add New";
        }
        private void fillInclusionFor(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "InclusionFor").MasterAttributeValues;
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
        private void fillInclusionType(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "InclusionType").MasterAttributeValues;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions();

            if (btnAdd.Text == "Update")
            {
                newObj.Activity_Inclusions_Id = Guid.Parse(hdnId.Value);
            }

            newObj.Activity_Flavour_Id = Activity_Flavour_Id;
            if (chkIsInclusion.Checked)
                newObj.IsInclusion = true;
            else
                newObj.IsInclusion = false;
            newObj.InclusionFor = ddlInclusionFor.SelectedItem.Text;
            newObj.InclusionType = ddlInclusionType.SelectedItem.Text;
            newObj.InclusionName = txtName.Text;
            newObj.InclusionDescription = txtDescription.Text;

            if (!string.IsNullOrWhiteSpace(txtFrom.Text))
            {
                newObj.InclusionFrom = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(txtTo.Text))
            {
                newObj.InclusionTo = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            newObj.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
            newObj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

            newObj.IsActive = true;

            MDMSVC.DC_Message _msg = ActSVC.AddUpdateActivityInclusions(newObj);
            if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
            {
                divMsgAlertIncExc.Visible = true;
                BootstrapAlert.BootstrapAlertMessage(divMsgAlertIncExc, _msg.StatusMessage, BootstrapAlertType.Success);
            }
            else
            {
                divMsgAlertIncExc.Visible = true;
                BootstrapAlert.BootstrapAlertMessage(divMsgAlertIncExc, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }


            ResetControls();

            BindInclusionsExclusions();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void btnAddNewInclusion_Click(object sender, EventArgs e)
        {
            ResetControls();
            divMsgAlertIncExc.Visible = false;
            btnAdd.Text = "Add New";
        }

        protected void gvActInclusionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                    btnEdit.Enabled = false;
                    btnEdit.Attributes.Remove("OnClientClick");
                }
                else
                {
                    e.Row.Font.Strikeout = false;
                    btnEdit.Enabled = true;
                    btnEdit.Attributes.Add("OnClientClick", "showAddEditModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void gvActInclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResetControls();

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Editing")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                MDMSVC.DC_Activity_Inclusions_RQ _obj = new MDMSVC.DC_Activity_Inclusions_RQ();
                _obj.Activity_Flavour_Id = Activity_Flavour_Id;
                _obj.Activity_Inclusions_Id = myRow_Id;
                var result = ActSVC.GetActivityInclusions(_obj);
                if (result != null)
                {
                    hdnId.Value = result[0].Activity_Inclusions_Id.ToString();

                    txtDescription.Text = result[0].InclusionDescription;
                    txtName.Text = result[0].InclusionName;

                    if (result[0].InclusionFrom != null)
                        txtFrom.Text = (result[0].InclusionFrom ?? DateTime.Now).ToString("dd/MM/yyyy");

                    if (result[0].InclusionTo != null)
                        txtTo.Text = (result[0].InclusionTo ?? DateTime.Now).ToString("dd/MM/yyyy");

                    ddlInclusionFor.ClearSelection();
                    if (ddlInclusionFor.Items.FindByText(result[0].InclusionFor) != null)
                        ddlInclusionFor.Items.FindByText(result[0].InclusionFor).Selected = true;

                    ddlInclusionType.ClearSelection();
                    if (ddlInclusionType.Items.FindByText(result[0].InclusionType) != null)
                        ddlInclusionType.Items.FindByText(result[0].InclusionType).Selected = true;

                    chkIsInclusion.Checked = result[0].IsInclusion ?? false;

                    btnAdd.Text = "Update";
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions
                {
                    Activity_Inclusions_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityInclusions(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindInclusionsExclusions();
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions
                {
                    Activity_Inclusions_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityInclusions(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindInclusionsExclusions();

            }
        }

        protected void gvActExclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResetControls();

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Editing")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                MDMSVC.DC_Activity_Inclusions_RQ _obj = new MDMSVC.DC_Activity_Inclusions_RQ();
                _obj.Activity_Flavour_Id = Activity_Flavour_Id;
                _obj.Activity_Inclusions_Id = myRow_Id;
                var result = ActSVC.GetActivityInclusions(_obj);
                if (result != null)
                {
                    hdnId.Value = result[0].Activity_Inclusions_Id.ToString();
                    txtDescription.Text = result[0].InclusionDescription;
                    txtName.Text = result[0].InclusionName;

                    if (result[0].InclusionFrom != null)
                        txtFrom.Text = (result[0].InclusionFrom ?? DateTime.Now).ToString("dd/MM/yyyy");

                    if (result[0].InclusionTo != null)
                        txtTo.Text = (result[0].InclusionTo ?? DateTime.Now).ToString("dd/MM/yyyy");

                    ddlInclusionFor.ClearSelection();
                    if (ddlInclusionFor.Items.FindByText(result[0].InclusionFor) != null)
                        ddlInclusionFor.Items.FindByText(result[0].InclusionFor).Selected = true;

                    ddlInclusionType.ClearSelection();
                    if (ddlInclusionType.Items.FindByText(result[0].InclusionType) != null)
                        ddlInclusionType.Items.FindByText(result[0].InclusionType).Selected = true;

                    chkIsInclusion.Checked = result[0].IsInclusion ?? false;

                    btnAdd.Text = "Update";
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions
                {
                    Activity_Inclusions_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityInclusions(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindInclusionsExclusions();
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions
                {
                    Activity_Inclusions_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityInclusions(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindInclusionsExclusions();

            }
        }

        protected void gvActExclusionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                    btnEdit.Enabled = false;
                    btnEdit.Attributes.Remove("OnClientClick");
                }
                else
                {
                    e.Row.Font.Strikeout = false;
                    btnEdit.Enabled = true;
                    btnEdit.Attributes.Add("OnClientClick", "showAddEditModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }
    }
}