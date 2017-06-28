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
    public partial class healthandsafety : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "HealthAndSafety";

        protected void GetLookUpValues()
        {
            DropDownList ddlHSCategory = (DropDownList)frmHealthAndSafety.FindControl("ddlHSCategory");
            DropDownList ddlHSName = (DropDownList)frmHealthAndSafety.FindControl("ddlHSName");

            ddlHSCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "HealthAndSafetyCategory").MasterAttributeValues;
            ddlHSCategory.DataTextField = "AttributeValue";
            ddlHSCategory.DataValueField = "MasterAttributeValue_Id";
            ddlHSCategory.DataBind();
            
                       
            ddlHSName.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "Name").MasterAttributeValues;
            ddlHSName.DataTextField = "AttributeValue";
            ddlHSName.DataValueField = "MasterAttributeValue_Id";
            ddlHSName.DataBind();
        }

        protected void GetHealthAndSafetyDetails()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdHealthAndSafety.DataSource = AccSvc.GetHealthAndSafetyDetails(Accomodation_ID, Guid.Empty);
            grdHealthAndSafety.DataBind();

            GetLookUpValues();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetHealthAndSafetyDetails();
            }
        }

        protected void frmHealthAndSafety_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlHSCategory = (DropDownList)frmHealthAndSafety.FindControl("ddlHSCategory");
            DropDownList ddlHSName = (DropDownList)frmHealthAndSafety.FindControl("ddlHSName");
            TextBox txtHSValue = (TextBox)frmHealthAndSafety.FindControl("txtHSValue");
            TextBox txtRemarks = (TextBox)frmHealthAndSafety.FindControl("txtRemarks");

            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_HealthAndSafety newObj = new MDMSVC.DC_Accommodation_HealthAndSafety
                {
                    Accommodation_HealthAndSafety_Id = Guid.NewGuid(),
                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Edit_Date = DateTime.Now, //using edit date to carry last updated date onto UI so we need to set it here, breaking design pattern

                    Description = txtHSValue.Text.Trim(),
                    Category = ddlHSCategory.SelectedItem.Text.Trim(),
                    Name = ddlHSName.SelectedItem.Text.Trim(),
                    Remarks = txtRemarks.Text.Trim(),
                    IsActive = true
                };
                if (AccSvc.AddHealthAndSafety(newObj))
                {
                    frmHealthAndSafety.ChangeMode(FormViewMode.Insert);
                    frmHealthAndSafety.DataBind();
                    GetHealthAndSafetyDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "H/S has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            }


            if (e.CommandName.ToString() == "Modify")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdHealthAndSafety.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetHealthAndSafetyDetails(Accomodation_ID, myRow_Id);

                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accommodation_HealthAndSafety newObj = new MDMSVC.DC_Accommodation_HealthAndSafety
                    {
                        Accommodation_HealthAndSafety_Id = myRow_Id,
                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        Edit_Date = DateTime.Now, // using edit date to carry last updated date onto UI so we need to set it here, breaking design pattern

                        Description = txtHSValue.Text.Trim(),
                        Category = ddlHSCategory.SelectedItem.Text.Trim(),
                        Name = ddlHSName.SelectedItem.Text.Trim(),
                        Remarks = txtRemarks.Text.Trim(),
                        IsActive = true
                    };

                    if (AccSvc.UpdateHealthAndSafety(newObj))
                    {
                        frmHealthAndSafety.ChangeMode(FormViewMode.Insert);
                        frmHealthAndSafety.DataBind();
                        GetHealthAndSafetyDetails();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "H/S has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            }
        }

        protected void grdHealthAndSafety_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);

                frmHealthAndSafety.ChangeMode(FormViewMode.Edit);
                frmHealthAndSafety.DataSource = AccSvc.GetHealthAndSafetyDetails(Accomodation_ID, myRow_Id);
                frmHealthAndSafety.DataBind();

                GetLookUpValues();

                DropDownList ddlHSCategory = (DropDownList)frmHealthAndSafety.FindControl("ddlHSCategory");
                DropDownList ddlHSName = (DropDownList)frmHealthAndSafety.FindControl("ddlHSName");

                MDMSVC.DC_Accommodation_HealthAndSafety rowView = (MDMSVC.DC_Accommodation_HealthAndSafety)frmHealthAndSafety.DataItem;

                if (ddlHSCategory.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlHSCategory.SelectedIndex = ddlHSCategory.Items.IndexOf(ddlHSCategory.Items.FindByText(rowView.Category.ToString()));
                }

                if (ddlHSName.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlHSName.SelectedIndex = ddlHSName.Items.IndexOf(ddlHSName.Items.FindByText(rowView.Name.ToString()));
                }

            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_HealthAndSafety newObj = new MDMSVC.DC_Accommodation_HealthAndSafety
                {
                    Accommodation_HealthAndSafety_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHealthAndSafety(newObj))
                {
                    GetHealthAndSafetyDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "H/S has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_HealthAndSafety newObj = new MDMSVC.DC_Accommodation_HealthAndSafety
                {
                    Accommodation_HealthAndSafety_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHealthAndSafety(newObj))
                {
                    GetHealthAndSafetyDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "H/S has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void grdHealthAndSafety_RowDataBound(object sender, GridViewRowEventArgs e)
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