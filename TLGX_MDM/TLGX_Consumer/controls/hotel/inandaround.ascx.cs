using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.hotel
{
    public partial class inandaround : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "Landmark";
        
        #region DataBinding

        protected void GetLookupValues()
        {

            DropDownList ddlPlaceCategory = (DropDownList)frmLandmark.FindControl("ddlPlaceCategory");
            DropDownList ddlUnitOfMeasure = (DropDownList)frmLandmark.FindControl("ddlUnitOfMeasure");

            ddlPlaceCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "PlaceCategory").MasterAttributeValues;
            ddlPlaceCategory.DataTextField = "AttributeValue";
            ddlPlaceCategory.DataValueField = "MasterAttributeValue_Id";
            ddlPlaceCategory.DataBind();

            ddlUnitOfMeasure.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "UnitOfMeasurement").MasterAttributeValues;
            ddlUnitOfMeasure.DataTextField = "AttributeValue";
            ddlUnitOfMeasure.DataValueField = "MasterAttributeValue_Id";
            ddlUnitOfMeasure.DataBind();

        }


        protected void BindNearbyPlaces()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdInAndAround.DataSource = AccSvc.GetNearbyPlacesDetails(Accomodation_ID, Guid.Empty);
            grdInAndAround.DataBind();

            GetLookupValues();
            
        }


        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindNearbyPlaces();
            }


        }

        protected void frmLandmark_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            DropDownList ddlPlaceCategory = (DropDownList)frmLandmark.FindControl("ddlPlaceCategory");
            DropDownList ddlUnitOfMeasure = (DropDownList)frmLandmark.FindControl("ddlUnitOfMeasure");

            TextBox txtDescription = (TextBox)frmLandmark.FindControl("txtDescription");
            TextBox txtPlaceName = (TextBox)frmLandmark.FindControl("txtPlaceName");
            TextBox txtDistance = (TextBox)frmLandmark.FindControl("txtDistance");
            
            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_NearbyPlaces newObj = new MDMSVC.DC_Accommodation_NearbyPlaces
                {
                    Accommodation_NearbyPlace_Id = Guid.NewGuid(),
                    Accomodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Description = txtDescription.Text.Trim(),
                    DistanceFromProperty = txtDistance.Text.Trim(),
                    DistanceUnit = ddlUnitOfMeasure.SelectedItem.Text,
                    PlaceCategory = ddlPlaceCategory.SelectedItem.Text,
                    PlaceName = txtPlaceName.Text.Trim(),

                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,

                    IsActive = true

                };
                if (AccSvc.AddNearbyPlaces(newObj))
                {

                    BindNearbyPlaces();

                    frmLandmark.DataBind();
                    frmLandmark.ChangeMode(FormViewMode.Insert);

                    GetLookupValues();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "In and Around has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            }


            if (e.CommandName.ToString() == "Modify")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdInAndAround.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetNearbyPlacesDetails(Accomodation_ID, myRow_Id);

                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accommodation_NearbyPlaces newObj = new MDMSVC.DC_Accommodation_NearbyPlaces
                    {
                        Accommodation_NearbyPlace_Id = myRow_Id,
                        Accomodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Description = txtDescription.Text.Trim(),
                        DistanceFromProperty = txtDistance.Text.Trim(),
                        DistanceUnit = ddlUnitOfMeasure.SelectedItem.Text,
                        PlaceCategory = ddlPlaceCategory.SelectedItem.Text,
                        PlaceName = txtPlaceName.Text.Trim(),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };

                    if (AccSvc.UpdateNearbyPlaces(newObj))
                    {

                        BindNearbyPlaces();
                        frmLandmark.ChangeMode(FormViewMode.Insert);
                        frmLandmark.DataBind();

                        GetLookupValues();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "In and Around has been updated successfully", BootstrapAlertType.Success);

                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            };

        }

        protected void grdInAndAround_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);
                
                frmLandmark.ChangeMode(FormViewMode.Edit);
                frmLandmark.DataSource = AccSvc.GetNearbyPlacesDetails(Accomodation_ID, myRow_Id);
                frmLandmark.DataBind();
                
                GetLookupValues();

                DropDownList ddlPlaceCategory = (DropDownList)frmLandmark.FindControl("ddlPlaceCategory");
                DropDownList ddlUnitOfMeasure = (DropDownList)frmLandmark.FindControl("ddlUnitOfMeasure");

                MDMSVC.DC_Accommodation_NearbyPlaces rowView = (MDMSVC.DC_Accommodation_NearbyPlaces)frmLandmark.DataItem;

                if (ddlPlaceCategory.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                        ddlPlaceCategory.SelectedIndex = ddlPlaceCategory.Items.IndexOf(ddlPlaceCategory.Items.FindByText(rowView.PlaceCategory.ToString()));
                }


                if (ddlUnitOfMeasure.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlUnitOfMeasure.SelectedIndex = ddlUnitOfMeasure.Items.IndexOf(ddlUnitOfMeasure.Items.FindByText(rowView.DistanceUnit.ToString()));
                }

            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_NearbyPlaces newObj = new MDMSVC.DC_Accommodation_NearbyPlaces
                {
                    Accommodation_NearbyPlace_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateNearbyPlaces(newObj))
                {
                    BindNearbyPlaces();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "In and Around has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_NearbyPlaces newObj = new MDMSVC.DC_Accommodation_NearbyPlaces
                {
                    Accommodation_NearbyPlace_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateNearbyPlaces(newObj))
                {
                    BindNearbyPlaces();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "In and Around has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void grdInAndAround_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void btnAddNewLookUP_Click(object sender, EventArgs e)
        {

        }
    }
}