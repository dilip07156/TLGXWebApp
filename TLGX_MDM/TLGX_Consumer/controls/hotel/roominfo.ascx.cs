using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using TLGX_Consumer;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.hotel
{
    public partial class roominfo : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        public Guid Accomodation_RoomInfo_ID;
        public static Guid Accomodation_RoomInfo_Static_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "RoomInfo";


        #region Binding Controls
        protected void GetLookupValues()
        {

            DropDownList ddlCompanyRoomCategory = (DropDownList)frmRoomInfo.FindControl("ddlCompanyRoomCategory");

            ddlCompanyRoomCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "CompanyRoomCategory").MasterAttributeValues;
            ddlCompanyRoomCategory.DataTextField = "AttributeValue";
            ddlCompanyRoomCategory.DataValueField = "MasterAttributeValue_Id";
            ddlCompanyRoomCategory.DataBind();

            DropDownList ddlBedType = (DropDownList)frmRoomInfo.FindControl("ddlBedType");

            ddlBedType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "BedType").MasterAttributeValues;
            ddlBedType.DataTextField = "AttributeValue";
            ddlBedType.DataValueField = "MasterAttributeValue_Id";
            ddlBedType.DataBind();


            DropDownList ddlBathRoomType = (DropDownList)frmRoomInfo.FindControl("ddlBathRoomType");

            ddlBathRoomType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "BathRoomType").MasterAttributeValues;
            ddlBathRoomType.DataTextField = "AttributeValue";
            ddlBathRoomType.DataValueField = "MasterAttributeValue_Id";
            ddlBathRoomType.DataBind();

        }



        protected void GetRoomInfoDetails()
        {

            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdRoomTypes.DataSource = AccSvc.GetRoomDetails(Accomodation_ID, Guid.Empty);
            grdRoomTypes.DataBind();

            GetLookupValues();


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRoomInfoDetails();
            }
        }

        #endregion

        #region FormView Controls




        #endregion

        #region GridView Controls




        #endregion

        protected void frmRoomInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            // get form controls
            TextBox txtRoomCategory = (TextBox)frmRoomInfo.FindControl("txtRoomCategory");
            TextBox txtRoomName = (TextBox)frmRoomInfo.FindControl("txtRoomName");
            TextBox txtNumberOfRooms = (TextBox)frmRoomInfo.FindControl("txtNumberOfRooms");
            TextBox txtRoomDescription = (TextBox)frmRoomInfo.FindControl("txtRoomDescription");
            TextBox txtFloorName = (TextBox)frmRoomInfo.FindControl("txtFloorName");
            TextBox txtFloorNumber = (TextBox)frmRoomInfo.FindControl("txtFloorNumber");
            TextBox txtRoomView = (TextBox)frmRoomInfo.FindControl("txtRoomView");
            TextBox txtRoomDecor = (TextBox)frmRoomInfo.FindControl("txtRoomDecor");
            TextBox txtInterconnectRooms = (TextBox)frmRoomInfo.FindControl("txtInterconnectRooms");
            DropDownList ddlCompanyRoomCategory = (DropDownList)frmRoomInfo.FindControl("ddlCompanyRoomCategory");
            DropDownList ddlBedType = (DropDownList)frmRoomInfo.FindControl("ddlBedType");
            DropDownList ddlBathRoomType = (DropDownList)frmRoomInfo.FindControl("ddlBathRoomType");
            DropDownList ddlSmoking = (DropDownList)frmRoomInfo.FindControl("ddlSmoking");
            TextBox txtRoomSize = (TextBox)frmRoomInfo.FindControl("txtRoomSize");
            TextBox txtRoomInfo_Id = (TextBox)frmRoomInfo.FindControl("txtRoomInfo_Id");

            bool bsmoking;

            if (ddlSmoking.SelectedItem.Text == "Yes")
            { bsmoking = true; }
            else { bsmoking = false; };


            if (e.CommandName.ToString() == "Add")
            {
                Accomodation_RoomInfo_ID = Guid.NewGuid();
                txtRoomInfo_Id.Text = Accomodation_RoomInfo_ID.ToString();
                TLGX_Consumer.MDMSVC.DC_Accommodation_RoomInfo newObj = new MDMSVC.DC_Accommodation_RoomInfo
                {

                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Accommodation_RoomInfo_Id = Accomodation_RoomInfo_ID,
                    BathRoomType = ddlBathRoomType.SelectedIndex != 0 ? ddlBathRoomType.SelectedItem.Text.Trim() : string.Empty,
                    BedType = ddlBedType.SelectedIndex != 0 ? ddlBedType.SelectedItem.Text.Trim() : string.Empty,
                    Category = txtRoomCategory.Text.Trim(),
                    CompanyRoomCategory = ddlCompanyRoomCategory.SelectedIndex != 0 ? ddlCompanyRoomCategory.SelectedItem.Text.Trim() : string.Empty,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    FloorName = txtFloorName.Text.Trim(),
                    FloorNumber = txtFloorNumber.Text.Trim(),
                    RoomCategory = txtRoomCategory.Text.Trim(),
                    RoomDecor = txtRoomDecor.Text.Trim(),
                    RoomName = txtRoomName.Text.Trim(),
                    RoomSize = txtRoomSize.Text.Trim(),
                    RoomView = txtRoomView.Text.Trim(),
                    Smoking = bsmoking,
                    Description = txtRoomDescription.Text.Trim(),
                    IsActive = true

                };

                if (!string.IsNullOrWhiteSpace(txtNumberOfRooms.Text.Trim()))
                {
                    newObj.NoOfRooms = int.Parse(txtNumberOfRooms.Text.Trim());
                }

                if (!string.IsNullOrWhiteSpace(txtInterconnectRooms.Text.Trim()))
                {
                    newObj.NoOfInterconnectingRooms = int.Parse(txtInterconnectRooms.Text.Trim());
                }

                if (AccSvc.AddRoom(newObj))
                {
                    frmRoomInfo.DataBind();
                    GetRoomInfoDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "RoomInfo has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);

            }

            else if (e.CommandName.ToString() == "Save")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdRoomTypes.SelectedDataKey.Value.ToString());
                Accomodation_RoomInfo_ID = myRow_Id;
                var result = AccSvc.GetRoomDetails(Accomodation_ID, myRow_Id);
                txtRoomInfo_Id.Text = myRow_Id.ToString();

                if (result.Count > 0)
                {

                    TLGX_Consumer.MDMSVC.DC_Accommodation_RoomInfo newObj = new MDMSVC.DC_Accommodation_RoomInfo

                    {

                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Accommodation_RoomInfo_Id = myRow_Id,
                        BathRoomType = ddlBathRoomType.SelectedIndex != 0 ? ddlBathRoomType.SelectedItem.Text.Trim() : string.Empty,
                        BedType = ddlBedType.SelectedIndex != 0 ? ddlBedType.SelectedItem.Text.Trim() : string.Empty,
                        Category = txtRoomCategory.Text.Trim(),
                        CompanyRoomCategory = ddlCompanyRoomCategory.SelectedIndex !=0 ? ddlCompanyRoomCategory.SelectedItem.Text.Trim() : string.Empty,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        FloorName = txtFloorName.Text.Trim(),
                        FloorNumber = txtFloorNumber.Text.Trim(),
                        RoomCategory = txtRoomCategory.Text.Trim(),
                        RoomDecor = txtRoomDecor.Text.Trim(),
                        RoomName = txtRoomName.Text.Trim(),
                        RoomSize = txtRoomSize.Text.Trim(),
                        RoomView = txtRoomView.Text.Trim(),
                        Smoking = bsmoking,
                        Description = txtRoomDescription.Text.Trim(),
                        IsActive = true

                    };

                    if (!string.IsNullOrWhiteSpace(txtNumberOfRooms.Text.Trim()))
                    {
                        newObj.NoOfRooms = int.Parse(txtNumberOfRooms.Text.Trim());
                    }

                    if (!string.IsNullOrWhiteSpace(txtInterconnectRooms.Text.Trim()))
                    {
                        newObj.NoOfInterconnectingRooms = int.Parse(txtInterconnectRooms.Text.Trim());
                    }

                    if (AccSvc.UpdateRoom(newObj))
                    {

                        Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                        frmRoomInfo.ChangeMode(FormViewMode.Insert);
                        frmRoomInfo.DataBind();
                        GetRoomInfoDetails();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "RoomInfo has been Updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);


                }




            }
        }

        protected void grdRoomTypes_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            //int index = Convert.ToInt32(e.CommandArgument);

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {
                Accomodation_RoomInfo_Static_ID = Guid.Empty;
                dvMsg.Style.Add("display", "none");
                //Guid myRow_Id = Guid.Parse(grdRoomTypes.DataKeys[index]["Accommodation_RoomInfo_Id"].ToString());
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);
                Accomodation_RoomInfo_ID = myRow_Id;
                Accomodation_RoomInfo_Static_ID = myRow_Id;
                frmRoomInfo.ChangeMode(FormViewMode.Edit);
                frmRoomInfo.DataSource = AccSvc.GetRoomDetails(Accomodation_ID, myRow_Id);
                frmRoomInfo.DataBind();

                GetLookupValues();

                MDMSVC.DC_Accommodation_RoomInfo rowView = (MDMSVC.DC_Accommodation_RoomInfo)frmRoomInfo.DataItem;

                DropDownList ddlCompanyRoomCategory = (DropDownList)frmRoomInfo.FindControl("ddlCompanyRoomCategory");
                DropDownList ddlBedType = (DropDownList)frmRoomInfo.FindControl("ddlBedType");
                DropDownList ddlBathRoomType = (DropDownList)frmRoomInfo.FindControl("ddlBathRoomType");
                DropDownList ddlSmoking = (DropDownList)frmRoomInfo.FindControl("ddlSmoking");

                if (ddlCompanyRoomCategory.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlCompanyRoomCategory.SelectedIndex = ddlCompanyRoomCategory.Items.IndexOf(ddlCompanyRoomCategory.Items.FindByText(rowView.CompanyRoomCategory.ToString()));
                }

                if (ddlBedType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlBedType.SelectedIndex = ddlBedType.Items.IndexOf(ddlBedType.Items.FindByText(rowView.BedType.ToString()));
                }

                if (ddlBathRoomType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlBathRoomType.SelectedIndex = ddlBathRoomType.Items.IndexOf(ddlBathRoomType.Items.FindByText(rowView.BathRoomType.ToString()));
                }

                if (ddlSmoking.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlSmoking.SelectedIndex = ddlSmoking.Items.IndexOf(ddlSmoking.Items.FindByText(rowView.Smoking.ToString()));
                }


            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RoomInfo newObj = new MDMSVC.DC_Accommodation_RoomInfo
                {
                    Accommodation_RoomInfo_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateRoom(newObj))
                {
                    GetRoomInfoDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "RoomInfo has been deleted successfully", BootstrapAlertType.Success);
                };

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RoomInfo newObj = new MDMSVC.DC_Accommodation_RoomInfo
                {
                    Accommodation_RoomInfo_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateRoom(newObj))
                {
                    GetRoomInfoDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "RoomInfo has been retrived successfully", BootstrapAlertType.Success);
                };

            }
        }

        protected void frmRoomInfo_DataBound(object sender, EventArgs e)
        {

            if (frmRoomInfo.CurrentMode.Equals(FormViewMode.Edit))
            {
                RoomAmenities myRoomAmenities = (RoomAmenities)frmRoomInfo.FindControl("myRoomAmenities");
                //myRoomAmenities.Accommodation_RoomInfo_Id = Guid.Parse(grdRoomTypes.DataKeys[0]["Accommodation_RoomInfo_Id"].ToString());
                myRoomAmenities.Accommodation_RoomInfo_Id = Accomodation_RoomInfo_Static_ID;
                myRoomAmenities.GetRoomAmenityDetails();
            }
            
        }

        protected void grdRoomTypes_RowDataBound(object sender, GridViewRowEventArgs e)
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
    } }