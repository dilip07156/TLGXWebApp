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
    public partial class updates : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "HotelUpdates";


        #region Binding Calls
        
        protected void GetLookUpData()
        {
            DropDownList ddlHotelUpdateSource = (DropDownList)frmHotelUpdate.FindControl("ddlHotelUpdateSource");

            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = AttributeOptionFor;
            RQ.Name = "UpdateSource";
            var updates = mastersvc.GetAllAttributeAndValues(RQ);

            //ddlHotelUpdateSource.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "UpdateSource").MasterAttributeValues;
            ddlHotelUpdateSource.DataSource = updates;
            ddlHotelUpdateSource.DataTextField = "AttributeValue";
            ddlHotelUpdateSource.DataValueField = "MasterAttributeValue_Id";
            ddlHotelUpdateSource.DataBind();
        }
      
        protected void GetHotelUpdateDetails()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdHotelupdates.DataSource = AccSvc.GetHotelUpdateDetails(Accomodation_ID, Guid.Empty);
            grdHotelupdates.DataBind();

         }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetHotelUpdateDetails();

                GetLookUpData();
            }
        }
        
        #endregion

        protected void frmHotelUpdate_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            DropDownList ddlHotelUpdateSource = (DropDownList)frmHotelUpdate.FindControl("ddlHotelUpdateSource");
            TextBox txtDescription = (TextBox)frmHotelUpdate.FindControl("txtDescription");
            TextBox txtFrom = (TextBox)frmHotelUpdate.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmHotelUpdate.FindControl("txtTo");
            CheckBox chkIsInternal = (CheckBox)frmHotelUpdate.FindControl("chkIsInternal");
            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_HotelUpdates newObj = new MDMSVC.DC_Accommodation_HotelUpdates
                {

                    Accommodation_HotelUpdates_Id = Guid.NewGuid(),
                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Description = txtDescription.Text.Trim(),


                    Source = ddlHotelUpdateSource.SelectedItem.Text.Trim(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,


                    FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    IsActive = true
                };
                if (chkIsInternal.Checked)
                    newObj.IsInternal = true;
                else
                    newObj.IsInternal = false;

                if (AccSvc.AddHotelUpdate(newObj))
                {
                    frmHotelUpdate.ChangeMode(FormViewMode.Insert);
                    frmHotelUpdate.DataBind();
                    GetHotelUpdateDetails();

                    GetLookUpData();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Updates has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            }


            if (e.CommandName.ToString() == "Modify")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdHotelupdates.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetHotelUpdateDetails(Accomodation_ID, myRow_Id);

                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accommodation_HotelUpdates newObj = new MDMSVC.DC_Accommodation_HotelUpdates
                    {
                        Accommodation_HotelUpdates_Id = myRow_Id,
                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Description = txtDescription.Text.Trim(),
                        
                        Source = ddlHotelUpdateSource.SelectedItem.Text.Trim(),
                        Edit_Date = DateTime.Now,
                        Edit_User= System.Web.HttpContext.Current.User.Identity.Name,

                        FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        IsActive = true

                    };
                    if (chkIsInternal.Checked)
                        newObj.IsInternal = true;
                    else
                        newObj.IsInternal = false;
                    if (AccSvc.UpdateHotelUpdate(newObj))
                    {
                        Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                        frmHotelUpdate.ChangeMode(FormViewMode.Insert);
                        frmHotelUpdate.DataBind();
                        GetHotelUpdateDetails();
                        GetLookUpData();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Updates has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            };

        }

        protected void grdHotelupdates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);

                frmHotelUpdate.ChangeMode(FormViewMode.Edit);
                frmHotelUpdate.DataSource = AccSvc.GetHotelUpdateDetails(Accomodation_ID, myRow_Id);
                frmHotelUpdate.DataBind();
                
                GetHotelUpdateDetails();
                GetLookUpData();

                DropDownList ddlHotelUpdateSource = (DropDownList)frmHotelUpdate.FindControl("ddlHotelUpdateSource");
                CheckBox chkIsInternal = (CheckBox)frmHotelUpdate.FindControl("chkIsInternal");

                MDMSVC.DC_Accommodation_HotelUpdates rowView = (MDMSVC.DC_Accommodation_HotelUpdates)frmHotelUpdate.DataItem;

                if (ddlHotelUpdateSource.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlHotelUpdateSource.SelectedIndex = ddlHotelUpdateSource.Items.IndexOf(ddlHotelUpdateSource.Items.FindByText(rowView.Source.ToString()));
                }
                chkIsInternal.Checked = rowView.IsInternal;

            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_HotelUpdates newObj = new MDMSVC.DC_Accommodation_HotelUpdates
                {
                    Accommodation_HotelUpdates_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelUpdate(newObj))
                {
                    GetHotelUpdateDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Updates has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_HotelUpdates newObj = new MDMSVC.DC_Accommodation_HotelUpdates
                {
                    Accommodation_HotelUpdates_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelUpdate(newObj))
                {
                    GetHotelUpdateDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Updates has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void grdHotelupdates_RowDataBound(object sender, GridViewRowEventArgs e)
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