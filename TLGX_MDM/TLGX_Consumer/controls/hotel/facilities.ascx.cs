using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.hotel
{
    public partial class facilities : System.Web.UI.UserControl
    {

        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "FacilityInfo";

        protected void GetFacilityCategory()
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            
            RQ.MasterFor = AttributeOptionFor;
            RQ.Name = "FacilityCategory";
            var Categories = mastersvc.GetAllAttributeAndValues(RQ);

            DropDownList ddlFacilityCategory = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityCategory");
            //ddlFacilityCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "FacilityCategory").MasterAttributeValues;
            ddlFacilityCategory.DataSource = Categories;
            ddlFacilityCategory.DataTextField = "AttributeValue";
            ddlFacilityCategory.DataValueField = "MasterAttributeValue_Id";
            ddlFacilityCategory.DataBind();

        }

        protected void GetFacilityType()
        {
            DropDownList ddlFacilityCategory = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityCategory");
            DropDownList ddlFacilityType = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityType");
            ddlFacilityType.Items.Clear();

            if (ddlFacilityCategory.SelectedItem.Value != "0")
            {
                MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();

                RQ.MasterFor = AttributeOptionFor;
                RQ.Name = "FacilityType";
                RQ.ParentAttributeValue_Id = Guid.Parse(ddlFacilityCategory.SelectedItem.Value);
                var types = mastersvc.GetAllAttributeAndValues(RQ);

                //ddlFacilityType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "FacilityType").MasterAttributeValues;
                ddlFacilityType.DataSource = types;
                ddlFacilityType.DataTextField = "AttributeValue";
                ddlFacilityType.DataValueField = "MasterAttributeValue_Id";
                ddlFacilityType.DataBind();
            }
            ddlFacilityType.Items.Insert(0, new ListItem("-Select-", "0"));

        }

        // gets facility details and binds datagrid
        protected void GetFacilityDetails()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdFacilityList.DataSource = AccSvc.GetHotelFacilityDetails(Accomodation_ID, Guid.Empty);
            grdFacilityList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                GetFacilityCategory();
                GetFacilityDetails();
                //GetFacilityType();
            }

        }


        protected void frmFacilityDetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlFacilityType = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityType");
            DropDownList ddlFacilityCategory = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityCategory");
            TextBox txtFacilityDescription = (TextBox)frmFacilityDetail.FindControl("txtFacilityDescription");
            TextBox txtFacilityName = (TextBox)frmFacilityDetail.FindControl("txtFacilityName");

            if (e.CommandName.ToString() == "Add")
            {

                TLGX_Consumer.MDMSVC.DC_Accommodation_Facility newObj = new MDMSVC.DC_Accommodation_Facility
                {
                    Accommodation_Facility_Id = Guid.NewGuid(),
                    Accommodation_Id = new Guid(Request.QueryString["Hotel_Id"]),
                    FacilityCategory = ddlFacilityCategory.SelectedItem.Text.ToString(),
                    FacilityType = ddlFacilityType.SelectedItem.Text.ToString(),
                    Description = txtFacilityDescription.Text.Trim(),
                    FacilityName = txtFacilityName.Text.Trim(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true

                };

                if(AccSvc.AddHotelFacilityDetails(newObj))
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Facility has been added successfully", BootstrapAlertType.Success);
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                newObj = null;
            }

            else if (e.CommandName.ToString() == "Save")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdFacilityList.SelectedDataKey.Value.ToString());
               
                TLGX_Consumer.MDMSVC.DC_Accommodation_Facility newObj = new MDMSVC.DC_Accommodation_Facility
                {
                    Accommodation_Facility_Id = myRow_Id,
                    Accommodation_Id = Accomodation_ID,
                    FacilityCategory = ddlFacilityCategory.SelectedItem.Text.ToString(),
                    FacilityType = ddlFacilityType.SelectedItem.Text.ToString(),
                    Description = txtFacilityDescription.Text.Trim(),
                    FacilityName = txtFacilityName.Text.Trim(),
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true
                };

                if (AccSvc.UpdateHotelFacilityDetails(newObj))
                {
                    frmFacilityDetail.ChangeMode(FormViewMode.Insert);
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Facility has been updated successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            };

            frmFacilityDetail.DataBind();
            GetFacilityCategory();
            GetFacilityDetails();
            GetFacilityType();

        }

        protected void grdFacilityList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Select")
            {
                dvMsg.Style.Add("display", "none");
                //BootstrapAlert.BootstrapAlertMessage(dvMsg, "", BootstrapAlertType.Plain);
                //int index = Convert.ToInt32(e.CommandArgument);

                //Guid myRow_Id = Guid.Parse(grdFacilityList.DataKeys[index].Value.ToString());

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);

                List<MDMSVC.DC_Accommodation_Facility> obj = new List<MDMSVC.DC_Accommodation_Facility>();
                obj.Add(new MDMSVC.DC_Accommodation_Facility
                {
                    Accommodation_Facility_Id = myRow_Id,
                    Accommodation_Id = Accomodation_ID,
                    FacilityName = grdFacilityList.Rows[index].Cells[2].Text,
                    Description = grdFacilityList.Rows[index].Cells[3].Text,
                    FacilityCategory = grdFacilityList.Rows[index].Cells[0].Text,
                    FacilityType = grdFacilityList.Rows[index].Cells[1].Text
                });

                frmFacilityDetail.ChangeMode(FormViewMode.Edit);
                frmFacilityDetail.DataSource = obj;
                frmFacilityDetail.DataBind();

                DropDownList ddlFacilityType = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityType");
                DropDownList ddlFacilityCategory = (DropDownList)frmFacilityDetail.FindControl("ddlFacilityCategory");
                TextBox txtFacilityDescription = (TextBox)frmFacilityDetail.FindControl("txtFacilityDescription");
                TextBox txtFacilityName = (TextBox)frmFacilityDetail.FindControl("txtFacilityName");

                GetFacilityCategory();
                ddlFacilityCategory.ClearSelection();
                if (ddlFacilityCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdFacilityList.Rows[index].Cells[0].Text)) != null)
                    ddlFacilityCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdFacilityList.Rows[index].Cells[0].Text)).Selected = true;


                GetFacilityType();
                ddlFacilityType.ClearSelection();
                if (ddlFacilityType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdFacilityList.Rows[index].Cells[1].Text)) != null)
                    ddlFacilityType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdFacilityList.Rows[index].Cells[1].Text)).Selected = true;

                txtFacilityName.Text = System.Web.HttpUtility.HtmlDecode(grdFacilityList.Rows[index].Cells[2].Text);
                txtFacilityDescription.Text = System.Web.HttpUtility.HtmlDecode(grdFacilityList.Rows[index].Cells[3].Text);
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Facility newObj = new MDMSVC.DC_Accommodation_Facility
                {

                    Accommodation_Facility_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelFacilityDetails(newObj))
                {
                    GetFacilityDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Facility has been deleted successfully", BootstrapAlertType.Success);
                };

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Facility newObj = new MDMSVC.DC_Accommodation_Facility
                {
                    Accommodation_Facility_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelFacilityDetails(newObj))
                {
                    GetFacilityDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Facility has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void grdFacilityList_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ddlFacilityCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFacilityType();
        }
    }
}