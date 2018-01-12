using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.activity
{
    public partial class ManageActivityFlavour : System.Web.UI.Page
    {
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.ActivitySVC activitySVC = new Controller.ActivitySVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillActivityStatusMaster(ddlActivity_Flavour_Status);
               // fillActivityStatusData();
            }
        }

       
        private void fillActivityStatusMaster(DropDownList ddlActivityStatus)
        {
            try
            {
                ddlActivityStatus.Items.Clear();
                ddlActivityStatus.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Status").MasterAttributeValues;
                ddlActivityStatus.DataTextField = "AttributeValue";
                ddlActivityStatus.DataValueField = "MasterAttributeValue_Id";
                ddlActivityStatus.DataBind();
                
            }
            catch
            {
                throw;
            }
        }

        protected void btnChangeActivityStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Guid Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                var result = activitySVC.AddUpdateActivityFlavoursStatus(new MDMSVC.DC_ActivityFlavoursStatus()
                {
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    Activity_Status = ddlActivity_Flavour_Status.SelectedItem.Text,
                    Activity_StatusNotes = txtActivity_Flavour_StatusNotes.Text,
                    Activity_Status_Edit_Date = DateTime.Now,
                    Activity_Status_Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                // Response.Redirect("/activity/ManageActivityFlavour?Activity_Flavour_Id=" + Activity_Flavour_Id, true);
                BootstrapAlert.BootstrapAlertMessage(dvMsgStatusUpdate, "Activity Status updated successfully", BootstrapAlertType.Success);
                Flavours.getFlavourInfo("header");
                
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnRedirectToSearch_Click(object sender, EventArgs e)
        {  
            Response.Redirect("~/activity/SearchActivityMaster.aspx");
        }

      
    }
}