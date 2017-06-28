using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.hotel
{
    public partial class route : System.Web.UI.UserControl
    {
        public Guid Accommodation_Id;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        MasterDataDAL MasterData = new MasterDataDAL();
        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "HowToReach";
        
        #region Data Binding controls

        protected void BindRoutes()
        {
            Accommodation_Id = new Guid(Request.QueryString["Hotel_Id"]);
            grdRoutes.DataSource = AccSvc.GetHowToReachDetails(Accommodation_Id, Guid.Empty);
            grdRoutes.DataBind();
        }


        protected void GetLookupData()
        {

            DropDownList ddlFrom = (DropDownList)frmRouote.FindControl("ddlFrom");
            ddlFrom.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "From").MasterAttributeValues;
            ddlFrom.DataTextField = "AttributeValue";
            ddlFrom.DataValueField = "MasterAttributeValue_Id";
            ddlFrom.DataBind();

            DropDownList ddlModeOfTransport = (DropDownList)frmRouote.FindControl("ddlModeOfTransport");
            ddlModeOfTransport.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "ModeOfTransport").MasterAttributeValues;
            ddlModeOfTransport.DataTextField = "AttributeValue";
            ddlModeOfTransport.DataValueField = "MasterAttributeValue_Id";
            ddlModeOfTransport.DataBind();

            DropDownList ddlTransportType = (DropDownList)frmRouote.FindControl("ddlTransportType");
            ddlTransportType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "TransportType").MasterAttributeValues;
            ddlTransportType.DataTextField = "AttributeValue";
            ddlTransportType.DataValueField = "MasterAttributeValue_Id";
            ddlTransportType.DataBind();


            DropDownList ddlNameOfPlace = (DropDownList)frmRouote.FindControl("ddlNameOfPlace");
            ddlNameOfPlace.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "NameOfPlace").MasterAttributeValues;
            ddlNameOfPlace.DataTextField = "AttributeValue";
            ddlNameOfPlace.DataValueField = "MasterAttributeValue_Id";
            ddlNameOfPlace.DataBind();

        }


        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoutes();
                GetLookupData();
            }
        }


        #region formview actions
        protected void frmRouote_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            DropDownList ddlFrom = (DropDownList)frmRouote.FindControl("ddlFrom");
            DropDownList ddlModeOfTransport = (DropDownList)frmRouote.FindControl("ddlModeOfTransport");
            DropDownList ddlNameOfPlace = (DropDownList)frmRouote.FindControl("ddlNameOfPlace");
            DropDownList ddlTransportType = (DropDownList)frmRouote.FindControl("ddlTransportType");
            TextBox txtDistanceFromProperty = (TextBox)frmRouote.FindControl("txtDistanceFromProperty");
            TextBox txtDescription = (TextBox)frmRouote.FindControl("txtDescription");
            TextBox txtApproximateDuration = (TextBox)frmRouote.FindControl("txtApproximateDuration");
            TextBox txtValidFrom = (TextBox)frmRouote.FindControl("txtValidFrom");
            TextBox txtValidTo = (TextBox)frmRouote.FindControl("txtValidTo");
            TextBox txtDrivingDirection = (TextBox)frmRouote.FindControl("txtDrivingDirection");



            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RouteInfo newObj = new MDMSVC.DC_Accommodation_RouteInfo
                {

                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Accommodation_Route_Id = Guid.NewGuid(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Description = txtDescription.Text.Trim(),
                    ApproximateDuration = txtApproximateDuration.Text.Trim(),
                    DistanceFromProperty = txtDistanceFromProperty.Text.Trim(),
                    DrivingDirection = txtDrivingDirection.Text.Trim(),
                    FromPlace = ddlFrom.SelectedItem.ToString(),
                    ModeOfTransport = ddlModeOfTransport.SelectedItem.ToString(),
                    TransportType = ddlTransportType.SelectedItem.ToString(),
                    ValidFrom = DateTime.ParseExact(txtValidFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ValidTo = DateTime.ParseExact(txtValidTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    NameOfPlace = ddlNameOfPlace.SelectedItem.Text.Trim(),
                    IsActive = true

                };

                if (AccSvc.AddHowToReach(newObj))
                {
                    frmRouote.DataBind();
                    BindRoutes();
                    GetLookupData();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Route Data has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);

            }

            else if (e.CommandName.ToString() == "Save")
            {
                Accommodation_Id = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdRoutes.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetHowToReachDetails(Accommodation_Id, myRow_Id);


                if (result.Count > 0)
                {

                    TLGX_Consumer.MDMSVC.DC_Accommodation_RouteInfo newObj = new MDMSVC.DC_Accommodation_RouteInfo

                    {

                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Accommodation_Route_Id = myRow_Id,
                        Create_Date = DateTime.Now,
                        Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                        Description = txtDescription.Text.Trim(),
                        ApproximateDuration = txtApproximateDuration.Text.Trim(),
                        DistanceFromProperty = txtDistanceFromProperty.Text.Trim(),
                        DrivingDirection = txtDrivingDirection.Text.Trim(),
                        FromPlace = ddlFrom.SelectedItem.ToString(),
                        ModeOfTransport = ddlModeOfTransport.SelectedItem.ToString(),
                        TransportType = ddlTransportType.SelectedItem.ToString(),
                        ValidFrom = DateTime.ParseExact(txtValidFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        ValidTo = DateTime.ParseExact(txtValidTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        NameOfPlace = ddlNameOfPlace.SelectedItem.Text.Trim(),
                        IsActive = true

                    };

                    if (AccSvc.UpdateHowToReach(newObj))
                    {

                        Accommodation_Id = new Guid(Request.QueryString["Hotel_Id"]);
                        frmRouote.ChangeMode(FormViewMode.Insert);
                        frmRouote.DataBind();
                        BindRoutes();
                        GetLookupData();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Route Data has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);


                }
            }
        }
        #endregion

        #region gridview actions





        #endregion

        protected void grdRoutes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]);

                frmRouote.ChangeMode(FormViewMode.Edit);
                frmRouote.DataSource = AccSvc.GetHowToReachDetails(Accommodation_Id, myRow_Id);
                frmRouote.DataBind();
                
                GetLookupData();
                
                MDMSVC.DC_Accommodation_RouteInfo rowView = (MDMSVC.DC_Accommodation_RouteInfo)frmRouote.DataItem;

                DropDownList ddlFrom = (DropDownList)frmRouote.FindControl("ddlFrom");
                DropDownList ddlModeOfTransport = (DropDownList)frmRouote.FindControl("ddlModeOfTransport");
                DropDownList ddlNameOfPlace = (DropDownList)frmRouote.FindControl("ddlNameOfPlace");
                DropDownList ddlTransportType = (DropDownList)frmRouote.FindControl("ddlTransportType");

                ddlFrom.Items.FindByText(rowView.FromPlace.ToString()).Selected = true;
                ddlModeOfTransport.Items.FindByText(rowView.ModeOfTransport.ToString()).Selected = true;
                ddlNameOfPlace.Items.FindByText(rowView.NameOfPlace.ToString()).Selected = true;
                ddlTransportType.Items.FindByText(rowView.TransportType.ToString()).Selected = true;
                
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RouteInfo newObj = new MDMSVC.DC_Accommodation_RouteInfo
                {
                    Accommodation_Route_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHowToReach(newObj))
                {
                    BindRoutes();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Route Data has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RouteInfo newObj = new MDMSVC.DC_Accommodation_RouteInfo
                {
                    Accommodation_Route_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHowToReach(newObj))
                {
                    BindRoutes();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Route Data has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void grdRoutes_RowDataBound(object sender, GridViewRowEventArgs e)
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

