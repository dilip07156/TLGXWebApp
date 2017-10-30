using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ReviewsNScores : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new ActivitySVC();
        public Guid Activity_Flavour_Id;
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDataSource();
            }
        }
        private void BindDataSource()
        {
            MDMSVC.DC_Activity_ReviewsAndScores_RQ _obj = new MDMSVC.DC_Activity_ReviewsAndScores_RQ();
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;

            var res = ActSVC.GetActReviewsAndScores(_obj);
            if (res != null)
            {
                DropDownList ddlReviewType = (DropDownList)frmRevAndScore.FindControl("ddlReviewType");
                DropDownList ddlReviewSource = (DropDownList)frmRevAndScore.FindControl("ddlReviewSource");
                DropDownList ddlReviewScore = (DropDownList)frmRevAndScore.FindControl("ddlReviewScore");

                grdRevAndScore.DataSource = res;
                grdRevAndScore.DataBind();
                lblTotalRecords.Text = Convert.ToString(res[0].Totalrecords);
                fillReviewType(ddlReviewType);
                fillReviewSource(ddlReviewSource);
                fillReviewScore(ddlReviewScore);
            }
            else
            {
                grdRevAndScore.DataSource = null;
                grdRevAndScore.DataBind();
                //divDropdownForEntries.Visible = false;
            }
        }
        protected void fillReviewType(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityReviewType").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        protected void fillReviewSource(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityReviewSource").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        protected void fillReviewScore(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Review_Score").MasterAttributeValues;
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private void ResetControls()
        {
            CheckBox chkIsActive = (CheckBox)frmRevAndScore.FindControl("chkIsActive");
            CheckBox chkIsCustomerReview = (CheckBox)frmRevAndScore.FindControl("chkIsCustomerReview");
            TextBox txtReviewName = (TextBox)frmRevAndScore.FindControl("txtReviewName");
            TextBox txtReviewDescription = (TextBox)frmRevAndScore.FindControl("txtReviewDescription");
            DropDownList ddlReviewType = (DropDownList)frmRevAndScore.FindControl("ddlReviewType");
            DropDownList ddlReviewSource = (DropDownList)frmRevAndScore.FindControl("ddlReviewSource");

            chkIsActive.Checked = false;
            chkIsCustomerReview.Checked = false;
            ddlReviewType.SelectedIndex = 0;
            ddlReviewSource.SelectedItem.Text=string.Empty;
            txtReviewName.Text = string.Empty;
            txtReviewDescription.Text = string.Empty;
        }
        protected void frmRevAndScore_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            CheckBox chkIsActive = (CheckBox)frmRevAndScore.FindControl("chkIsActive");
            CheckBox chkIsCustomerReview = (CheckBox)frmRevAndScore.FindControl("chkIsCustomerReview");
            TextBox txtReviewName = (TextBox)frmRevAndScore.FindControl("txtReviewName");
            TextBox txtReviewDescription = (TextBox)frmRevAndScore.FindControl("txtReviewDescription");
            DropDownList ddlReviewType = (DropDownList)frmRevAndScore.FindControl("ddlReviewType");
            DropDownList ddlReviewSource = (DropDownList)frmRevAndScore.FindControl("ddlReviewSource");
            DropDownList ddlReviewScore = (DropDownList)frmRevAndScore.FindControl("ddlReviewScore");

            if (e.CommandName.ToString() == "Add")
            {
                MDMSVC.DC_Activity_ReviewsAndScores newObj = new MDMSVC.DC_Activity_ReviewsAndScores
                {
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    Activity_ReviewsAndScores_Id = Guid.NewGuid(),
                    IsCustomerReview = chkIsCustomerReview.Checked,
                    IsActive = chkIsActive.Checked,
                    Review_Title = txtReviewName.Text,
                    Review_Description = txtReviewDescription.Text,
                    Review_Type = ddlReviewType.SelectedItem.Text,
                    Review_Source = ddlReviewSource.SelectedItem.Text,
                    Review_Score = Convert.ToDecimal(ddlReviewScore.SelectedItem.Text)
                };
                var result = ActSVC.AddUpdateActReviewsNScores(newObj);
                BootstrapAlert.BootstrapAlertMessage(divMsgAlertRevAndScore, result.StatusMessage, (BootstrapAlertType)result.StatusCode);

                frmRevAndScore.ChangeMode(FormViewMode.Insert);
                frmRevAndScore.DataBind();

                ResetControls();
            }
            if (e.CommandName.ToString() == "Update")
            {
                MDMSVC.DC_Activity_ReviewsAndScores newObj = new MDMSVC.DC_Activity_ReviewsAndScores();
                {
                    newObj.Activity_Flavour_Id = Activity_Flavour_Id;
                    if (chkIsCustomerReview.Checked)
                        newObj.IsCustomerReview = true;
                    else
                        newObj.IsCustomerReview = false;
                    if (chkIsActive.Checked)
                        newObj.IsActive = true;
                    else
                        newObj.IsActive = false;
                    newObj.Review_Type = ddlReviewType.SelectedItem.Text;
                    newObj.Review_Source = ddlReviewSource.SelectedItem.Text;
                    newObj.Review_Title = txtReviewName.Text;
                    newObj.Review_Description = txtReviewDescription.Text;
                }

                MDMSVC.DC_Message _msg = ActSVC.AddUpdateActReviewsNScores(newObj);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    divMsgAlertRevAndScore.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertRevAndScore, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    divMsgAlertRevAndScore.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertRevAndScore, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }

                ResetControls();
            }
            if (e.CommandName.ToString() == "Reset")
            {
                ResetControls();
            }
        }

        protected void grdRevAndScore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            DropDownList ddlReviewType = (DropDownList)frmRevAndScore.FindControl("ddlReviewType");
            DropDownList ddlReviewSource = (DropDownList)frmRevAndScore.FindControl("ddlReviewSource");
            DropDownList ddlReviewScore = (DropDownList)frmRevAndScore.FindControl("ddlReviewScore");

            if (e.CommandName.ToString() == "Select")
            {
                frmRevAndScore.ChangeMode(FormViewMode.Edit);
                dvMsg.Style.Add("display", "none");
                MDMSVC.DC_Activity_ReviewsAndScores_RQ RQ = new MDMSVC.DC_Activity_ReviewsAndScores_RQ();
                RQ.Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                RQ.Activity_ReviewsAndScores_Id = myRow_Id;
                var res = ActSVC.GetActReviewsAndScores(RQ);
                frmRevAndScore.DataSource = res; 
                frmRevAndScore.DataBind();
                
                fillReviewType(ddlReviewType);
                fillReviewSource(ddlReviewSource);
                fillReviewScore(ddlReviewScore);

                if (ddlReviewType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlReviewType.SelectedIndex = ddlReviewType.Items.IndexOf(ddlReviewType.Items.FindByText(res[0].Review_Type.ToString()));
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_ReviewsAndScores newObj = new MDMSVC.DC_Activity_ReviewsAndScores
                {
                    Activity_ReviewsAndScores_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActReviewsNScores(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindDataSource();
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_ReviewsAndScores newObj = new MDMSVC.DC_Activity_ReviewsAndScores
                {
                    Activity_ReviewsAndScores_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActReviewsNScores(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindDataSource();

            }
        }

        protected void grdRevAndScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                    btnSelect.Enabled = false;
                    btnSelect.Attributes.Remove("OnClientClick");
                }
                else
                {
                    e.Row.Font.Strikeout = false;
                    btnSelect.Enabled = true;
                    btnSelect.Attributes.Add("OnClientClick", "showAddNewRevAndScoreModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataSource();
        }
    }
}