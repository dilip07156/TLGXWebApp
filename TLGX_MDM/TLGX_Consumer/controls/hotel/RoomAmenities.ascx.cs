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
    public partial class RoomAmenities : System.Web.UI.UserControl
    {
        public Guid Accommodation_RoomInfo_Id; // needs to be set from caller page


        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "RoomAmenities";


        //RoomAmenityType

        #region Binding Code



        protected void GetLookUpValues()
        {
            DropDownList ddlFacilityCategory = (DropDownList)frmRoomAmenity.FindControl("ddlFacilityCategory");

            ddlFacilityCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "RoomAmenityType").MasterAttributeValues;
            ddlFacilityCategory.DataTextField = "AttributeValue";
            ddlFacilityCategory.DataValueField = "MasterAttributeValue_Id";
            ddlFacilityCategory.DataBind();
        }


        public void GetRoomAmenityDetails() // needs to be set from caller page
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdRoomAmenities.DataSource = AccSvc.GetRoomFacilitiesDetails(Accomodation_ID, Accommodation_RoomInfo_Id, Guid.Empty);
            grdRoomAmenities.DataBind();
            //  
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }





        #endregion

        protected void frmRoomAmenity_DataBound(object sender, EventArgs e)
        {
            GetLookUpValues();
            //TextBox txtRoomInfo_Id = (TextBox)frmRoomAmenity.FindControl("txtRoomInfo_Id");
            //txtRoomInfo_Id.Text = Accommodation_RoomInfo_Id.ToString();
        }

        protected void frmRoomAmenity_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);
            Accommodation_RoomInfo_Id = new Guid(((TextBox)(this.Parent.FindControl("txtRoomInfo_Id"))).Text);
            TextBox txtRoomInfo_Id = (TextBox)frmRoomAmenity.FindControl("txtRoomInfo_Id");
            TextBox txtAmenityName = (TextBox)frmRoomAmenity.FindControl("txtAmenityName");
            DropDownList ddlFacilityCategory = (DropDownList)frmRoomAmenity.FindControl("ddlFacilityCategory");

            
            if (e.CommandName.ToString() == "AddAmenity")
            {
                TLGX_Consumer.MDMSVC.DC_Accomodation_RoomFacilities newObj = new MDMSVC.DC_Accomodation_RoomFacilities
                {
                    Accommodation_RoomFacility_Id = Guid.NewGuid(),
                    Accommodation_Id = Accomodation_ID,
                    Accommodation_RoomInfo_Id = Accommodation_RoomInfo_Id,
                    AmenityName = txtAmenityName.Text.Trim(),
                    AmenityType = ddlFacilityCategory.SelectedItem.Text.Trim(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true

                };

                if (AccSvc.AddRoomFacilities(newObj))
                {
                    frmRoomAmenity.DataBind();
                    GetRoomAmenityDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Room Amenity has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);

            }

            else if (e.CommandName.ToString() == "UpdateAmenity")
            {
                Guid myRow_Id = Guid.Parse(grdRoomAmenities.SelectedDataKey.Value.ToString());
                var result = AccSvc.GetRoomFacilitiesDetails(Accomodation_ID, Accommodation_RoomInfo_Id, myRow_Id); // AccSvc.GetRoomDetails(Accomodation_ID, myRow_Id);
                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accomodation_RoomFacilities newObj = new MDMSVC.DC_Accomodation_RoomFacilities
                    {
                        Accommodation_RoomFacility_Id = myRow_Id,
                        Accommodation_Id = Accomodation_ID,
                        Accommodation_RoomInfo_Id = Accommodation_RoomInfo_Id,
                        AmenityName = txtAmenityName.Text.Trim(),
                        AmenityType = ddlFacilityCategory.SelectedItem.Text.Trim(),
                        Edit_Date = DateTime.Now,
                        Edit_user = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };

                    if (AccSvc.UpdateRoomFacilities(newObj))
                    {
                        Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                        frmRoomAmenity.ChangeMode(FormViewMode.Insert);
                        frmRoomAmenity.DataBind();
                        GetRoomAmenityDetails();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Room Amenity has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            }
        }

        protected void grdRoomAmenities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = row.RowIndex;

            if (e.CommandName == "Select")
            {
                dvMsg.Style.Add("display", "none");
                //int index = Convert.ToInt32(e.CommandArgument);
                //Guid myRow_Id = Guid.Parse(grdRoomAmenities.DataKeys[index].Value.ToString());
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Accommodation_RoomInfo_Id = new Guid(((TextBox)(this.Parent.FindControl("txtRoomInfo_Id"))).Text);
                List<MDMSVC.DC_Accomodation_RoomFacilities> obj = new List<MDMSVC.DC_Accomodation_RoomFacilities>();
                obj.Add(new MDMSVC.DC_Accomodation_RoomFacilities
                {
                    Accommodation_RoomFacility_Id = myRow_Id,
                    Accommodation_Id = Accomodation_ID,
                    Accommodation_RoomInfo_Id = Accommodation_RoomInfo_Id,
                    AmenityType = grdRoomAmenities.Rows[RowIndex].Cells[0].Text,
                    AmenityName = grdRoomAmenities.Rows[RowIndex].Cells[1].Text
                });
                frmRoomAmenity.ChangeMode(FormViewMode.Edit);
                frmRoomAmenity.DataSource = obj;
                frmRoomAmenity.DataBind();

                TextBox txtAmenityName = (TextBox)frmRoomAmenity.FindControl("txtAmenityName");
                DropDownList ddlFacilityCategory = (DropDownList)frmRoomAmenity.FindControl("ddlFacilityCategory");
                ddlFacilityCategory.SelectedIndex = ddlFacilityCategory.Items.IndexOf(ddlFacilityCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdRoomAmenities.Rows[RowIndex].Cells[0].Text)));
                txtAmenityName.Text = System.Web.HttpUtility.HtmlDecode(grdRoomAmenities.Rows[RowIndex].Cells[1].Text);
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accomodation_RoomFacilities newObj = new MDMSVC.DC_Accomodation_RoomFacilities
                {
                    Accommodation_RoomFacility_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_user = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateRoomFacilities (newObj))
                {
                    Accommodation_RoomInfo_Id = new Guid(((TextBox)(this.Parent.FindControl("txtRoomInfo_Id"))).Text);
                    GetRoomAmenityDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Room Amenity has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accomodation_RoomFacilities newObj = new MDMSVC.DC_Accomodation_RoomFacilities
                {
                    Accommodation_RoomFacility_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_user = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateRoomFacilities(newObj))
                {
                    Accommodation_RoomInfo_Id = new Guid(((TextBox)(this.Parent.FindControl("txtRoomInfo_Id"))).Text);
                    GetRoomAmenityDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Room Amenity has been retrived successfully", BootstrapAlertType.Success);
                };

            }
        }

        protected void grdRoomAmenities_RowDataBound(object sender, GridViewRowEventArgs e)
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