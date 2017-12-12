﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Inclusion : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInclusions();
            }
        }
        protected void BindInclusions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Inclusions_RQ _obj = new MDMSVC.DC_Activity_Inclusions_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = ActSVC.GetActivityInclusions(_obj);
            if (result != null)
            {
                List<MDMSVC.DC_Activity_Inclusions> res = result.Where(w => w.IsInclusion == true).Select(s => s).ToList();

                gvActInclusionSearch.DataSource = res;
                gvActInclusionSearch.DataBind();

                if (res.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(res[0].TotalRecords);
                }

            }
            else
            {
                gvActInclusionSearch.DataSource = null;
                gvActInclusionSearch.DataBind();
                divDropdownForEntries.Visible = false;
            }
        }
        protected void gvActInclusionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvActInclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Editing")
            {
                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //int index = row.RowIndex;

                //Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

                //bool isinclusion = false;

                //if (gvActInclusionSearch.Rows[index].Cells[4].Text == "true")
                //    isinclusion = true;

                //List<MDMSVC.DC_Activity_Inclusions> newObj = new List<MDMSVC.DC_Activity_Inclusions>();
                //newObj.Add(new MDMSVC.DC_Activity_Inclusions
                //{
                //    Activity_Inclusions_Id = myRow_Id,
                //    Activity_Flavour_Id = Activity_Flavour_Id,
                //    InclusionType = gvActInclusionSearch.Rows[index].Cells[0].Text,
                //    InclusionName = gvActInclusionSearch.Rows[index].Cells[1].Text,
                //    InclusionDescription = gvActInclusionSearch.Rows[index].Cells[3].Text,
                //    IsInclusion = isinclusion
                //});

                //if (!string.IsNullOrEmpty(gvActInclusionSearch.Rows[index].Cells[0].Text))
                //    newObj[0].InclusionType = gvActInclusionSearch.Rows[index].Cells[0].Text;

                //frmInclusion.ChangeMode(FormViewMode.Edit);
                //frmInclusion.DataSource = newObj;
                //frmInclusion.DataBind();

                //TextBox txtName = (TextBox)frmInclusion.FindControl("txtMAxAdults");
                //TextBox txtDescription = (TextBox)frmInclusion.FindControl("txtPassengers");

                //DropDownList ddlInclusionFor = (DropDownList)frmInclusion.FindControl("ddlInclusionFor");
                //DropDownList ddlInclusionType = (DropDownList)frmInclusion.FindControl("ddlInclusionType");

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
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindInclusions();
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
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindInclusions();

            }
        }

        protected void frmInclusionDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

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
                    btnEdit.Attributes.Add("OnClientClick", "showAddEditModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void frmInclusion_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Add")
            {
                hdnFlag.Value = "true";
            }
            if (e.CommandName.ToString() == "Update")
            {
                hdnFlag.Value = "true";
            }
            if (e.CommandName.ToString() == "Reset")
            {
                //hdnFlag.Value = "true";
            }
        }

        //protected void btnNewActivity_Click(object sender, EventArgs e)
        //{
        //    frmInclusion.ChangeMode(FormViewMode.Insert);
        //    frmInclusion.DataBind();
        //}
    }
}