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
    public partial class rules : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "HotelRules";


        protected void GetLookUpValues()
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = AttributeOptionFor;
            RQ.Name = "RuleName";
            var rules = mastersvc.GetAllAttributeAndValues(RQ);

            DropDownList ddlRuleName = (DropDownList)frmRule.FindControl("ddlRuleName");
            //ddlRuleName.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "RuleName").MasterAttributeValues;
            ddlRuleName.DataSource = rules;
            ddlRuleName.DataTextField = "AttributeValue";
            ddlRuleName.DataValueField = "MasterAttributeValue_Id";
            ddlRuleName.DataBind();


        }


        protected void BindHotelRules()

        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdHOtelRUles.DataSource = AccSvc.GetHotelRuleDetails(Accomodation_ID, Guid.Empty);
            grdHOtelRUles.DataBind();

            GetLookUpValues();

        }





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindHotelRules();
            }
        }

        protected void frmRule_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            DropDownList ddlRuleName = (DropDownList)frmRule.FindControl("ddlRuleName");
            TextBox txtRuleText = (TextBox)frmRule.FindControl("txtRuleText");
            CheckBox chkIsInternal = (CheckBox)frmRule.FindControl("chkIsInternal");
            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RuleInfo newObj = new MDMSVC.DC_Accommodation_RuleInfo
                {
                    Accommodation_RuleInfo_Id = Guid.NewGuid(),
                    Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Description = txtRuleText.Text.Trim(),
                    RuleType = ddlRuleName.SelectedItem.Text.Trim(),
                    IsActive = true

                };
                if (chkIsInternal.Checked)
                    newObj.IsInternal = true;
                else
                    newObj.IsInternal = false;
                if (AccSvc.AddHotelRule(newObj))
                {

                    BindHotelRules();

                    frmRule.DataBind();
                    frmRule.ChangeMode(FormViewMode.Insert);

                    GetLookUpValues();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Rule has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            }


            if (e.CommandName.ToString() == "Modify")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdHOtelRUles.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetHotelRuleDetails(Accomodation_ID, myRow_Id);

                if (result.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Accommodation_RuleInfo newObj = new MDMSVC.DC_Accommodation_RuleInfo
                    {

                        Accommodation_RuleInfo_Id = myRow_Id,
                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        Description = txtRuleText.Text.Trim(),
                        RuleType = ddlRuleName.SelectedItem.Text.Trim(),
                        IsActive = true

                    };
                    if (chkIsInternal.Checked)
                        newObj.IsInternal = true;
                    else
                        newObj.IsInternal = false;

                    if (AccSvc.UpdateHotelRule(newObj))
                    {

                        BindHotelRules();
                        
                        frmRule.ChangeMode(FormViewMode.Insert);
                        frmRule.DataBind();

                        GetLookUpValues();
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Rule has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            };



        }

        protected void grdHOtelRUles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);

                frmRule.ChangeMode(FormViewMode.Edit);
                frmRule.DataSource = AccSvc.GetHotelRuleDetails(Accomodation_ID, myRow_Id);
                frmRule.DataBind();


                GetLookUpValues();

                DropDownList ddlRuleName = (DropDownList)frmRule.FindControl("ddlRuleName");
                CheckBox chkIsInternal = (CheckBox)frmRule.FindControl("chkIsInternal");

                MDMSVC.DC_Accommodation_RuleInfo rowView = (MDMSVC.DC_Accommodation_RuleInfo)frmRule.DataItem;

                if (ddlRuleName.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlRuleName.SelectedIndex = ddlRuleName.Items.IndexOf(ddlRuleName.Items.FindByText(rowView.RuleType.ToString()));
                }
                chkIsInternal.Checked = rowView.IsInternal;
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RuleInfo newObj = new MDMSVC.DC_Accommodation_RuleInfo
                {
                    Accommodation_RuleInfo_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelRule(newObj))
                {
                    BindHotelRules();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Rule has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_RuleInfo newObj = new MDMSVC.DC_Accommodation_RuleInfo
                {
                    Accommodation_RuleInfo_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelRule(newObj))
                {
                    BindHotelRules();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Rule has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void grdHOtelRUles_RowDataBound(object sender, GridViewRowEventArgs e)
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