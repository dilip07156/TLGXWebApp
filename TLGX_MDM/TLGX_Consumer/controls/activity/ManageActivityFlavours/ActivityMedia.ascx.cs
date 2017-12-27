using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ActivityMedia : System.Web.UI.UserControl
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMedia(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }
        protected void BindMedia(int pagesize,int pageno)
        {
           Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Media_Search_RQ RQ = new DC_Activity_Media_Search_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            RQ.PageNo = pageno;
            RQ.PageSize = pagesize;
            var result = AccSvc.GetActivityMedia(RQ);
            if (result != null)
            {
                gvActMediaSearch.DataSource = result;
                gvActMediaSearch.DataBind();
                if (result.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(result[0].TotalRecords);
                }
            }
            else
            {
                gvActMediaSearch.DataSource = null;
                gvActMediaSearch.DataBind();
            }

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMedia(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {
            frmMedia.ChangeMode(FormViewMode.Insert);
            frmMedia.DataBind();
            //GetLookUpValues("Page", "");
        }

        
        protected void gvActMediaSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindMedia(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }
        protected void gvActMediaSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                MDMSVC.DC_Activity_Media_Search_RQ RQ = new DC_Activity_Media_Search_RQ();
                RQ.Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                RQ.Activity_Media_Id = myRow_Id;
                frmMedia.ChangeMode(FormViewMode.Edit);
                frmMedia.DataSource = AccSvc.GetActivityMedia(RQ);
                frmMedia.DataBind();
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Media newObj = new MDMSVC.DC_Activity_Media
                {

                    Activity_Media_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = AccSvc.AddUpdateActivityMedia(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindMedia(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Media newObj = new MDMSVC.DC_Activity_Media
                {
                    Activity_Media_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = AccSvc.AddUpdateActivityMedia(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindMedia(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0); 

            }
        }

        protected void gvActMediaSearch_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    btnSelect.Attributes.Add("OnClientClick", "showMediaModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void frmMedia_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }
    }
}