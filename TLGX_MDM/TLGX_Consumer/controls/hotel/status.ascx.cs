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

namespace TLGX_Consumer.controls.hotel
{
    public partial class status : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();
        MasterDataSVCs _objMasterData = new MasterDataSVCs();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "HotelStatus";

        #region binding controls

        protected void GetHotelStatusDetails()
        {
            GetMasterData();

            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdStatusList.DataSource = AccSvc.GetHotelStatusDetails(Accomodation_ID, Guid.Empty); // send EMPTY GUID to get all 
            grdStatusList.DataBind();
        }

        protected void GetMasterData()
        {
            DropDownList ddlCompanyMarket = (DropDownList)frmAccommodationStatus.FindControl("ddlCompanyMarket");

            ddlCompanyMarket.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "CompanyMarket").MasterAttributeValues;
            ddlCompanyMarket.DataTextField = "AttributeValue";
            ddlCompanyMarket.DataValueField = "MasterAttributeValue_Id";
            ddlCompanyMarket.DataBind();

            DropDownList ddlStatus = (DropDownList)frmAccommodationStatus.FindControl("ddlStatus");

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
                GetHotelStatusDetails();
            }
        }

        #endregion

        #region formview controls

        // handles the activity on the formview containing the status detail record
        protected void frmAccommodationStatus_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlCompanyMarket = (DropDownList)frmAccommodationStatus.FindControl("ddlCompanyMarket");
            DropDownList ddlStatus = (DropDownList)frmAccommodationStatus.FindControl("ddlStatus");
            TextBox txtDeactivationReason = (TextBox)frmAccommodationStatus.FindControl("txtDeactivationReason");
            TextBox txtFrom = (TextBox)frmAccommodationStatus.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmAccommodationStatus.FindControl("txtTo");

            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Status newObj = new MDMSVC.DC_Accommodation_Status
                {
                    Accommodation_Status_Id = Guid.NewGuid(),
                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    CompanyMarket = ddlCompanyMarket.SelectedItem.Text.Trim(),
                    DeactivationReason = txtDeactivationReason.Text.Trim(),
                    From = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    To = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Status = ddlStatus.SelectedItem.Text.Trim(),
                    IsActive = true,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.AddHotelStatus(newObj))
                {
                    frmAccommodationStatus.DataBind();
                    GetHotelStatusDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Status has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);

            }

            else if (e.CommandName.ToString() == "Save")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdStatusList.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetHotelStatusDetails(Accomodation_ID, myRow_Id);


                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accommodation_Status newObj = new MDMSVC.DC_Accommodation_Status
                    {
                        Accommodation_Id = Accomodation_ID,
                        Accommodation_Status_Id = myRow_Id,
                        CompanyMarket = ddlCompanyMarket.SelectedItem.Text.Trim(),
                        DeactivationReason = txtDeactivationReason.Text.Trim(),
                        From = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        To = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        Status = ddlStatus.SelectedItem.Text.Trim(),
                        IsActive = true,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                    };

                    if (AccSvc.UpdateHotelStatus(newObj))
                    {
                        Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                        frmAccommodationStatus.ChangeMode(FormViewMode.Insert);
                        frmAccommodationStatus.DataBind();
                        GetHotelStatusDetails();
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
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);

                frmAccommodationStatus.ChangeMode(FormViewMode.Edit);
                frmAccommodationStatus.DataSource = AccSvc.GetHotelStatusDetails(Accomodation_ID, myRow_Id);
                frmAccommodationStatus.DataBind();

                DropDownList ddlCompanyMarket = (DropDownList)frmAccommodationStatus.FindControl("ddlCompanyMarket");
                DropDownList ddlStatus = (DropDownList)frmAccommodationStatus.FindControl("ddlStatus");

                GetMasterData();

                MDMSVC.DC_Accommodation_Status rowView = (MDMSVC.DC_Accommodation_Status)frmAccommodationStatus.DataItem;

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
                TLGX_Consumer.MDMSVC.DC_Accommodation_Status newObj = new MDMSVC.DC_Accommodation_Status
                {
                    Accommodation_Status_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelStatus(newObj))
                {
                    GetHotelStatusDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Status has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Status newObj = new MDMSVC.DC_Accommodation_Status
                {
                    Accommodation_Status_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelStatus(newObj))
                {
                    GetHotelStatusDetails();
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