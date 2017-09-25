using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.activity.ManageActivityMaster
{
    public partial class ProductStatus : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Id;

        MasterDataDAL MasterData = new MasterDataDAL();
        MasterDataSVCs _objMasterData = new MasterDataSVCs();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();

        #region binding controls
        protected void GetActivityStatusDetails()
        {
            GetMasterData();

            Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
            grdStatusList.DataSource = ActSVC.GetActivityStatusDetails(Activity_Id, Guid.Empty); // send EMPTY GUID to get all 
            grdStatusList.DataBind();
        }

        protected void GetMasterData()
        {
            DropDownList ddlCompanyMarket = (DropDownList)frmActivityStatus.FindControl("ddlCompanyMarket");

            ddlCompanyMarket.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "CompanyMarket").MasterAttributeValues;
            ddlCompanyMarket.DataTextField = "AttributeValue";
            ddlCompanyMarket.DataValueField = "MasterAttributeValue_Id";
            ddlCompanyMarket.DataBind();

            DropDownList ddlStatus = (DropDownList)frmActivityStatus.FindControl("ddlStatus");

            //ddlStatus.DataSource = MasterData.getAllStatuses();
            ddlStatus.DataSource = _objMasterData.GetAllStatuses();
            ddlStatus.DataTextField = "Status_Name";
            ddlStatus.DataValueField = "Status_Short";
            ddlStatus.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetActivityStatusDetails();
            }
        }
        
        protected void frmActivityStatus_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlCompanyMarket = (DropDownList)frmActivityStatus.FindControl("ddlCompanyMarket");
            DropDownList ddlStatus = (DropDownList)frmActivityStatus.FindControl("ddlStatus");
            TextBox txtDeactivationReason = (TextBox)frmActivityStatus.FindControl("txtDeactivationReason");
            TextBox txtFrom = (TextBox)frmActivityStatus.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmActivityStatus.FindControl("txtTo");

            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = Guid.NewGuid(),
                    Activity_Id = Guid.Parse(Request.QueryString["Activity_Id"]),
                    CompanyMarket = ddlCompanyMarket.SelectedItem.Text.Trim(),
                    DeactivationReason = txtDeactivationReason.Text.Trim(),
                    From = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    To = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Status = ddlStatus.SelectedItem.Text.Trim(),
                    IsActive = true,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (ActSVC.AddActivityStatus(newObj))
                {
                    frmActivityStatus.DataBind();
                    GetActivityStatusDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Status has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);

            }

            else if (e.CommandName.ToString() == "Save")
            {
                Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                Guid myRow_Id = Guid.Parse(grdStatusList.SelectedDataKey.Value.ToString());

                var result = ActSVC.GetActivityStatusDetails(Activity_Id, myRow_Id);


                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                    {
                        Activity_Id = Activity_Id,
                        Activity_Status_Id = myRow_Id,
                        CompanyMarket = ddlCompanyMarket.SelectedItem.Text.Trim(),
                        DeactivationReason = txtDeactivationReason.Text.Trim(),
                        From = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        To = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        Status = ddlStatus.SelectedItem.Text.Trim(),
                        IsActive = true,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                    };

                    if (ActSVC.UpdateActivityStatus(newObj))
                    {
                        Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                        frmActivityStatus.ChangeMode(FormViewMode.Insert);
                        frmActivityStatus.DataBind();
                        GetActivityStatusDetails();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Status has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            }
        }
        #endregion

        protected void grdStatusList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                Activity_Id = Guid.Parse(Request.QueryString["Activity_Id"]);

                frmActivityStatus.ChangeMode(FormViewMode.Edit);
                frmActivityStatus.DataSource = ActSVC.GetActivityStatusDetails(Activity_Id, myRow_Id);
                frmActivityStatus.DataBind();

                DropDownList ddlCompanyMarket = (DropDownList)frmActivityStatus.FindControl("ddlCompanyMarket");
                DropDownList ddlStatus = (DropDownList)frmActivityStatus.FindControl("ddlStatus");

                GetMasterData();

                MDMSVC.DC_Activity_Status rowView = (MDMSVC.DC_Activity_Status)frmActivityStatus.DataItem;

                if (ddlCompanyMarket.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlCompanyMarket.SelectedIndex = ddlCompanyMarket.Items.IndexOf(ddlCompanyMarket.Items.FindByText(rowView.CompanyMarket.ToString()));
                }

                if (ddlStatus.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(rowView.Status.ToString()));
                }
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (ActSVC.UpdateActivityStatus(newObj))
                {
                    GetActivityStatusDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Status has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (ActSVC.UpdateActivityStatus(newObj))
                {
                    GetActivityStatusDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Status has been retrived successfully", BootstrapAlertType.Success);
                };
            }
        }

        protected void grdStatusList_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}