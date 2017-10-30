using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Exclusion : System.Web.UI.UserControl
    {
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindExclusions();
            }
        }
        protected void BindExclusions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Inclusions_RQ _obj = new MDMSVC.DC_Activity_Inclusions_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = ActSVC.GetActivityInclusions(_obj);
            if (result != null)
            {
                List<MDMSVC.DC_Activity_Inclusions> res = new List<MDMSVC.DC_Activity_Inclusions>();
                if (res != null)
                {
                    foreach (MDMSVC.DC_Activity_Inclusions rs in result)
                    {
                        if (rs.IsInclusion != true)
                        {
                            res.Add(rs);
                        }

                    }
                    gvActInclusionSearch.DataSource = res;
                    gvActInclusionSearch.DataBind();
                    lblTotalRecords.Text = Convert.ToString(res.Count);
                }
                else
                {
                    gvActInclusionSearch.DataSource = null;
                    gvActInclusionSearch.DataBind();
                    divDropdownForEntries.Visible = false;
                }
            }
            else
            {
                gvActInclusionSearch.DataSource = null;
                gvActInclusionSearch.DataBind();
                divDropdownForEntries.Visible = false;
            }

            fillExclusionFor(ddlInclusionFor);
            fillExclusionType(ddlInclusionType);
        }

        protected void fillExclusionFor(DropDownList ddl)
        {
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Policy_Type").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }

        protected void fillExclusionType(DropDownList ddl)
        {
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Activity_Policy_Type").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }
        protected void gvActInclusionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvActInclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            if (e.CommandName.ToString() == "Select")
            {
                divMsgAlertExc.Style.Add("display", "none");
                MDMSVC.DC_Activity_Inclusions_RQ RQ = new MDMSVC.DC_Activity_Inclusions_RQ();
                RQ.Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                RQ.Activity_Inclusions_Id = myRow_Id;
                frmInclusionDetails.ChangeMode(FormViewMode.Edit);
                frmInclusionDetails.DataSource = ActSVC.GetActivityInclusions(RQ);
                frmInclusionDetails.DataBind();

                MDMSVC.DC_Activity_Inclusions rowView = (MDMSVC.DC_Activity_Inclusions)frmInclusionDetails.DataItem;

                //GetLookUpValues("Form", rowView.DescriptionType.ToString());

                //DropDownList ddlType = (DropDownList)frmInclusionDetails.FindControl("ddlType");
                //DropDownList ddlDescriptionType = (DropDownList)frmInclusionDetails.FindControl("ddlDescriptionType");
                
                //if (ddlDescriptionSubType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                //{
                //    ddlDescriptionSubType.SelectedIndex = ddlDescriptionSubType.Items.IndexOf(ddlDescriptionSubType.Items.FindByText(rowView.DescriptionSubType.ToString()));
                //}

                //if (ddlDescriptionType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                //{
                //    ddlDescriptionType.SelectedIndex = ddlDescriptionType.Items.IndexOf(ddlDescriptionType.Items.FindByText(rowView.DescriptionType.ToString()));
                //}
                //TextBox txtFrom = (TextBox)frmDescription.FindControl("txtFrom");
                //TextBox txtTo = (TextBox)frmDescription.FindControl("txtTo");
                //txtFrom.Text = System.Web.HttpUtility.HtmlDecode(gvDescriptionSearch.Rows[index].Cells[0].Text);
                //txtTo.Text = System.Web.HttpUtility.HtmlDecode(gvDescriptionSearch.Rows[index].Cells[1].Text);
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions
                {
                    Activity_Inclusions_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityInclusions(newObj);
                BootstrapAlert.BootstrapAlertMessage(divMsgAlertExc, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindExclusions();
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Inclusions newObj = new MDMSVC.DC_Activity_Inclusions
                {
                    Activity_Inclusions_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = ActSVC.AddUpdateActivityInclusions(newObj);
                BootstrapAlert.BootstrapAlertMessage(divMsgAlertExc, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindExclusions();

            }
        }

        protected void frmInclusionDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Editing")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRow_Id = Guid.Parse(gvActInclusionSearch.DataKeys[index].Values[0].ToString());

                MDMSVC.DC_Activity_InclusionDetails_RQ newObj = new MDMSVC.DC_Activity_InclusionDetails_RQ();
                newObj.Activity_Inclusion_Id = myRow_Id;
                newObj.Activity_Flavour_Id = Activity_Flavour_Id;

                var result = ActSVC.GetActivityInclusionDetails(newObj);
                if (result.Count > 0 && result != null)
                {
                    TLGX_Consumer.MDMSVC.DC_Activity_InclusionsDetails _newObj = new MDMSVC.DC_Activity_InclusionsDetails
                    {
                        Activity_Inclusion_Id = myRow_Id,
                        Activity_Flavour_Id = Activity_Flavour_Id,
                        EditDate = DateTime.Now,
                        EditUser = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = chkIsInclusion.Checked
                    };
                    var res1 = ActSVC.AddUpdateInclusionDetails(_newObj);
                    BootstrapAlert.BootstrapAlertMessage(divMsgAlertExc, res1.StatusMessage, (BootstrapAlertType)res1.StatusCode);
                    BindExclusions();
                }
                hdnFlag.Value = "true";
            }
        }

        protected void gvActInclusionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                    btnEdit.Enabled = false;
                    btnEdit.Attributes.Remove("OnClientClick");
                }
                else
                {
                    e.Row.Font.Strikeout = false;
                    btnEdit.Enabled = true;
                    btnEdit.Attributes.Add("OnClientClick", "showEditModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }

        }
    }
}

