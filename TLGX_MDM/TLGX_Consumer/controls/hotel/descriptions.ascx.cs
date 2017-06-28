using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using AjaxControlToolkit;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.hotel
{
    public partial class descriptions : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "Descriptions";

        protected void GetDescriptionType()
        {

            DropDownList ddlDescriptionType = (DropDownList)frmDescriptionDetail.FindControl("ddlDescriptionType");


            ddlDescriptionType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionType").MasterAttributeValues;
            ddlDescriptionType.DataTextField = "AttributeValue";
            ddlDescriptionType.DataValueField = "MasterAttributeValue_Id";
            ddlDescriptionType.DataBind();
        }



        protected void GetDescriptionDetails()
        {



            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdDescriptionList.DataSource = AccSvc.GetHotelDescriptionDetails(Accomodation_ID, Guid.Empty);
            grdDescriptionList.DataBind();

            GetDescriptionType();

        }





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetDescriptionDetails();



            }
        }


        // needs client side handling of DATE FIELDS
        protected void frmDescriptionDetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlDescriptionType = (DropDownList)frmDescriptionDetail.FindControl("ddlDescriptionType");
            TextBox txtDescription = (TextBox)frmDescriptionDetail.FindControl("txtDescription");
            TextBox txtFrom = (TextBox)frmDescriptionDetail.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmDescriptionDetail.FindControl("txtTo");



            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Descriptions newObj = new MDMSVC.DC_Accommodation_Descriptions
                {

                    Accommodation_Description_Id = Guid.NewGuid(),
                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Description = txtDescription.Text.Trim(),
                    DescriptionType = ddlDescriptionType.SelectedItem.ToString(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    IsActive = true
                };
                if (AccSvc.AddAccommodationDescriptionDetail(newObj))
                {
                    GetDescriptionDetails();
                    frmDescriptionDetail.DataBind();
                    frmDescriptionDetail.ChangeMode(FormViewMode.Insert);
                    GetDescriptionType();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Description has been added successfully", BootstrapAlertType.Success);

                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            }


            if (e.CommandName.ToString() == "Modify")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdDescriptionList.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetHotelDescriptionDetails(Accomodation_ID, myRow_Id);

                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accommodation_Descriptions newObj = new MDMSVC.DC_Accommodation_Descriptions
                    {
                        Accommodation_Description_Id = myRow_Id,
                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Description = txtDescription.Text.Trim(),
                        DescriptionType = ddlDescriptionType.SelectedItem.ToString(),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    };

                    if (AccSvc.UpdateHotelDescriptions(newObj))
                    {

                        GetDescriptionDetails();
                        Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                        frmDescriptionDetail.ChangeMode(FormViewMode.Insert);
                        frmDescriptionDetail.DataBind();
                        GetDescriptionType();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Description has been Updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            };
        }

        protected void grdDescriptionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //int index = Convert.ToInt32(e.CommandArgument);

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                //Guid myRow_Id = Guid.Parse(grdDescriptionList.DataKeys[index]["Accommodation_Description_Id"].ToString());
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);
                
                frmDescriptionDetail.ChangeMode(FormViewMode.Edit);
                frmDescriptionDetail.DataSource = AccSvc.GetHotelDescriptionDetails(Accomodation_ID, myRow_Id);
                frmDescriptionDetail.DataBind();
                
                GetDescriptionType();

                DropDownList ddlDescriptionType = (DropDownList)frmDescriptionDetail.FindControl("ddlDescriptionType");

                if (ddlDescriptionType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    MDMSVC.DC_Accommodation_Descriptions rowView = (MDMSVC.DC_Accommodation_Descriptions)frmDescriptionDetail.DataItem;
                    ddlDescriptionType.SelectedIndex = ddlDescriptionType.Items.IndexOf(ddlDescriptionType.Items.FindByText(rowView.DescriptionType.ToString()));
                }

            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Descriptions newObj = new MDMSVC.DC_Accommodation_Descriptions
                {

                    Accommodation_Description_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelDescriptions(newObj))
                {
                    GetDescriptionDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Description has been deleted successfully", BootstrapAlertType.Success);
                };

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Descriptions newObj = new MDMSVC.DC_Accommodation_Descriptions
                {
                    Accommodation_Description_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelDescriptions(newObj))
                {
                    GetDescriptionDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Description has been retrived successfully", BootstrapAlertType.Success);
                };

            }


        }

        protected void grdDescriptionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.DataItem != null)
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