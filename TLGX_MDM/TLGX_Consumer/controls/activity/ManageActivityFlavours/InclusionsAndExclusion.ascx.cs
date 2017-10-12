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
            }
        }
        protected void BindAddInclusionControls()
        {
            fillInclusionFor(ddlInclusionFor);
            fillInclusionType(ddlInclusionType);
        }
        protected void ResetControls()
        {
            chkIsInclusion.Checked = false;
            ddlInclusionFor.SelectedIndex = 0;
            ddlInclusionType.SelectedIndex = 0;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;
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
        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions();
            {
                newObj.Activity_Flavour_Id = Activity_Flavour_Id;
                if(chkIsInclusion.Checked)
                    newObj.IsInclusion = true;
                else
                    newObj.IsInclusion = false;
                newObj.InclusionFor = ddlInclusionFor.SelectedItem.Text;
                newObj.InclusionType = ddlInclusionType.SelectedItem.Text;
                newObj.InclusionName = txtName.Text;
                newObj.InclusionDescription = txtDescription.Text;
                newObj.InclusionFrom = DateTime.Parse(txtFrom.Text);
                newObj.InclusionTo = DateTime.Parse(txtTo.Text);
            }

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
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void btnAddNewInclusion_Click(object sender, EventArgs e)
        {
            divMsgAlertIncExc.Visible = false;
        }
    }
}