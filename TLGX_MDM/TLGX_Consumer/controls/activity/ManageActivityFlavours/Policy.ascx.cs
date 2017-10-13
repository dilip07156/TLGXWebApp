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
        Controller.ActivitySVC ActSVC = new ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDataSource();
            }
        }

        private void BindDataSource()
        {
            //Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            MDMSVC.DC_Activity_Policy_RQ _obj = new MDMSVC.DC_Activity_Policy_RQ();
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;
            
            var res = ActSVC.GetActivityPolicy(_obj);
            if(res!=null)
            {
                grdPolicy.DataSource = res;
                grdPolicy.DataBind();
            }
            else
            {
                grdPolicy.DataSource = null;
                grdPolicy.DataBind();
            }
        }

        private void ResetControls()
        {
            chkIsActive.Checked = false;
            chkIsAllow.Checked = false;
            ddlInclusionType.SelectedIndex = 0;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
        //    Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

        //    MDMSVC.DC_Activity_Policy newObj = new MDMSVC.DC_Activity_Policy();
        //    {
        //        newObj.Activity_Flavour_Id = Activity_Flavour_Id;
        //        if (chkIsActive.Checked)
        //            newObj = true;
        //        else
        //            newObj.IsInclusion = false;
        //        if()
        //        newObj.InclusionFor = ddlInclusionFor.SelectedItem.Text;
        //        newObj.InclusionType = ddlInclusionType.SelectedItem.Text;
        //        newObj.InclusionName = txtName.Text;
        //        newObj.InclusionDescription = txtDescription.Text;
        //        newObj.InclusionFrom = DateTime.Parse(txtFrom.Text);
        //        newObj.InclusionTo = DateTime.Parse(txtTo.Text);
        //        newObj.IsActive = true;
        //    }

        //    MDMSVC.DC_Message _msg = ActSVC.AddUpdateActivityInclusions(newObj);
        //    if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
        //    {
        //        divMsgAlertIncExc.Visible = true;
        //        BootstrapAlert.BootstrapAlertMessage(divMsgAlertIncExc, _msg.StatusMessage, BootstrapAlertType.Success);
        //    }
        //    else
        //    {
        //        divMsgAlertIncExc.Visible = true;
        //        BootstrapAlert.BootstrapAlertMessage(divMsgAlertIncExc, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
        //    }


        //    ResetControls();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}