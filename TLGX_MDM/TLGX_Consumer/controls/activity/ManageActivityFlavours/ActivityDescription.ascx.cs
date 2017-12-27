using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ActivityDescription : System.Web.UI.UserControl
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        public static string AttributeOptionFor = "Descriptions";
        public Guid Activity_Flavour_Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDescriptionType();
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }

        }
        protected void GetDescriptionType()
        {
            ddlDescriptionType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionType").MasterAttributeValues;
            ddlDescriptionType.DataTextField = "AttributeValue";
            ddlDescriptionType.DataValueField = "MasterAttributeValue_Id";
            ddlDescriptionType.DataBind();
        }


        protected void btnNewUpload_Click(object sender, EventArgs e)
        {
            txtDescription.Text = string.Empty;
            ddlDescriptionType.ClearSelection();
            ddlDescriptionType.SelectedIndex = 0;
            hdnDescId.Value = Guid.NewGuid().ToString();
        }

        protected void BindActDescription(int pagesize, int pageno)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Descriptions_RQ RQ = new MDMSVC.DC_Activity_Descriptions_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            RQ.PageNo = pageno;
            RQ.PageSize = pagesize;
            var result = AccSvc.GetActivityDescription(RQ);
            if (result != null)
            {
                if (result.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(result[0].TotalRecords);
                    gvDescriptionSearch.VirtualItemCount = result[0].TotalRecords ?? 0;
                    gvDescriptionSearch.PageIndex = pageno;
                    gvDescriptionSearch.PageSize = pagesize;
                }

                gvDescriptionSearch.DataSource = result;
                gvDescriptionSearch.DataBind();

            }
            else
            {
                gvDescriptionSearch.DataSource = null;
                gvDescriptionSearch.DataBind();
            }
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void gvDescriptionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");

                hdnDescId.Value = myRow_Id.ToString();
                txtDescription.Text = Regex.Replace(System.Web.HttpUtility.HtmlDecode(gvDescriptionSearch.Rows[index].Cells[1].Text), "<.*?>", string.Empty); ;

                ddlDescriptionType.ClearSelection();
                if (ddlDescriptionType.Items.FindByText(gvDescriptionSearch.Rows[index].Cells[0].Text) != null)
                {
                    ddlDescriptionType.Items.FindByText(gvDescriptionSearch.Rows[index].Cells[0].Text).Selected = true;
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Descriptions newObj = new MDMSVC.DC_Activity_Descriptions
                {

                    Activity_Description_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = AccSvc.AddUpdateActivityDescription(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Descriptions newObj = new MDMSVC.DC_Activity_Descriptions
                {
                    Activity_Description_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = AccSvc.AddUpdateActivityDescription(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);

            }
        }

        protected void gvDescriptionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    btnSelect.Attributes.Add("OnClientClick", "showDescriptionModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void gvDescriptionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            MDMSVC.DC_Activity_Descriptions RQ = new MDMSVC.DC_Activity_Descriptions
            {
                Activity_Description_Id = Guid.Parse(hdnDescId.Value),
                Activity_Flavour_Id = Activity_Flavour_Id,
                DescriptionType = ddlDescriptionType.SelectedItem.ToString(),
                Description = txtDescription.Text.Trim(),
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                IsActive = true
            };
            var res = AccSvc.AddUpdateActivityDescription(RQ);
            BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
            BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }
    }
}