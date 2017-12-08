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

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Policy : System.Web.UI.UserControl
    {
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        Controller.ActivitySVC ActSVC = new ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            DropDownList ddlPolicyType = (DropDownList)frmPolicy.FindControl("ddlPolicyType");
            BindDataSource();
            fillPolicyType(ddlPolicyType);
        }
        private void BindDataSource()
        {
            DropDownList ddlPolicyType = (DropDownList)frmPolicy.FindControl("ddlPolicyType");

            MDMSVC.DC_Activity_Policy_RQ _obj = new MDMSVC.DC_Activity_Policy_RQ();
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;

            var res = ActSVC.GetActivityPolicy(_obj);
            if (res != null)
            {

                grdPolicy.DataSource = res;
                grdPolicy.DataBind();

                if (res.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(res[0].Totalrecords);
                }

            }
            else
            {
                grdPolicy.DataSource = null;
                grdPolicy.DataBind();
                //divDropdownForEntries.Visible = false;
            }

        }
        private void fillPolicyType(DropDownList ddl)
        {
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Policy_Type").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }

        private void ResetControls()
        {
            CheckBox chkIsActive = (CheckBox)frmPolicy.FindControl("chkIsActive");
            CheckBox chkIsAllow = (CheckBox)frmPolicy.FindControl("chkIsAllow");
            TextBox txtName = (TextBox)frmPolicy.FindControl("txtName");
            TextBox txtDescription = (TextBox)frmPolicy.FindControl("txtDescription");
            DropDownList ddlPolicyType = (DropDownList)frmPolicy.FindControl("ddlPolicyType");

            chkIsActive.Checked = false;
            chkIsAllow.Checked = false;
            ddlPolicyType.SelectedIndex = 0;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
        //    CheckBox chkIsAllow = (CheckBox)frmPolicy.FindControl("chkIsAllow");
        //    TextBox txtName = (TextBox)frmPolicy.FindControl("txtName");
        //    TextBox txtDescription = (TextBox)frmPolicy.FindControl("txtDescription");

        //    MDMSVC.DC_Activity_Policy newObj = new MDMSVC.DC_Activity_Policy();
        //    {
        //        newObj.Activity_Flavour_Id = Activity_Flavour_Id;
        //        if (chkIsAllow.Checked)
        //            newObj.AllowedYN = true;
        //        else
        //            newObj.AllowedYN = false;
        //        newObj.IsActive = true;
        //        newObj.PolicyName = txtName.Text;
        //        newObj.PolicyDescription = txtDescription.Text;
        //    }

        //    MDMSVC.DC_Message _msg = ActSVC.AddUpdateActivityPolicy(newObj);
        //    if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
        //    {
        //        dvMsg.Visible = true;
        //        BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
        //    }
        //    else
        //    {
        //        dvMsg.Visible = true;
        //        BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
        //    }


        //    ResetControls();
        //}

        protected void grdPolicy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                MDMSVC.DC_Activity_Policy_RQ RQ = new MDMSVC.DC_Activity_Policy_RQ();
                RQ.Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                RQ.Activity_Policy_Id = myRow_Id;
                frmPolicy.ChangeMode(FormViewMode.Edit);
                frmPolicy.DataSource = ActSVC.GetActivityPolicy(RQ);
                frmPolicy.DataBind();

                MDMSVC.DC_Activity_Policy rowView = (MDMSVC.DC_Activity_Policy)frmPolicy.DataItem;

                DropDownList ddlPolicyType = (DropDownList)frmPolicy.FindControl("ddlPolicyType");

                fillPolicyType(ddlPolicyType);

                if (ddlPolicyType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlPolicyType.SelectedIndex = ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText(rowView.Policy_Type.ToString()));
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Policy newObj = new MDMSVC.DC_Activity_Policy
                {
                    Activity_Policy_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityPolicy(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindDataSource();
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Policy newObj = new MDMSVC.DC_Activity_Policy
                {
                    Activity_Policy_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityPolicy(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindDataSource();

            }
        }

        protected void grdPolicy_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                    btnSelect.Enabled = false;
                    btnSelect.Attributes.Remove("OnClientClick");
                }
                else
                {
                    e.Row.Font.Strikeout = false;
                    btnSelect.Enabled = true;
                    btnSelect.Attributes.Add("OnClientClick", "showAddNewPolicyModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void frmPolicy_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            DropDownList ddlPolicyType = (DropDownList)frmPolicy.FindControl("ddlPolicyType");
            TextBox txtName = (TextBox)frmPolicy.FindControl("txtName");
            TextBox txtDescription = (TextBox)frmPolicy.FindControl("txtDescription");
            CheckBox chkIsAllow = (CheckBox)frmPolicy.FindControl("chkIsAllow");
            CheckBox chkIsActive = (CheckBox)frmPolicy.FindControl("chkIsActive");

            if (e.CommandName.ToString() == "Add")
            {
                MDMSVC.DC_Activity_Policy newObj = new MDMSVC.DC_Activity_Policy
                {
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    Activity_Policy_Id = Guid.NewGuid(),
                    AllowedYN = true,
                    IsActive = true,
                    PolicyName = txtName.Text,
                    PolicyDescription = txtDescription.Text,
                    Policy_Type = ddlPolicyType.SelectedItem.Text
                };

                MDMSVC.DC_Message _msg = ActSVC.AddUpdateActivityPolicy(newObj);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    divMsgAlertPolicy.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertPolicy, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    divMsgAlertPolicy.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertPolicy, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }

                ResetControls();
                BindDataSource();
            }
            if (e.CommandName.ToString() == "Update")
            {
                Guid myRowId = Guid.Parse(frmPolicy.DataKey.Value.ToString());
                MDMSVC.DC_Activity_Policy newObj = new MDMSVC.DC_Activity_Policy();
                {
                    newObj.Activity_Policy_Id = myRowId;
                    newObj.Activity_Flavour_Id = Activity_Flavour_Id;
                    if (chkIsAllow.Checked)
                        newObj.AllowedYN = true;
                    else
                        newObj.AllowedYN = false;
                    if (chkIsActive.Checked)
                        newObj.IsActive = true;
                    else
                        newObj.IsActive = false;
                    newObj.Policy_Type = ddlPolicyType.SelectedItem.Text;
                    newObj.PolicyName = txtName.Text;
                    newObj.PolicyDescription = txtDescription.Text;
                }

                MDMSVC.DC_Message _msg = ActSVC.AddUpdateActivityPolicy(newObj);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    divMsgAlertPolicy.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertPolicy, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    divMsgAlertPolicy.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertPolicy, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }

                ResetControls();
                BindDataSource();
            }
            if (e.CommandName.ToString() == "Reset")
            {
                ResetControls();
            }
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataSource();
        }

    }
}