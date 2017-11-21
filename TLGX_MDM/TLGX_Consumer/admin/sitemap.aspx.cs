using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TLGX_Consumer.App_Code;
using System.Configuration;

namespace TLGX_Consumer.admin
{
    public partial class sitemap : System.Web.UI.Page
    {
        public int PageSize = 5;
        public int intPageIndex = 0;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.AdminSVCs _objAdminSVCs = new Controller.AdminSVCs();

        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindApplication();
            }
        }

        private void BindApplication()
        {
            try
            {
                var result = _objAdminSVCs.GetAllApplication(0, 0);
                if (result != null)
                {
                    ddlApplilcation.DataSource = result;
                    ddlApplilcation.DataTextField = "ApplicationName";
                    ddlApplilcation.DataValueField = "ApplicationId";
                    ddlApplilcation.DataBind();
                    ddlApplilcation.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = Guid.Empty.ToString() });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void FillPageData()
        {
            try
            {
                GetSiteMapMaster();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void GetSiteMapMaster()
        {
            string strApplication = ddlApplilcation.SelectedValue.ToString();
            if (strApplication != "00000000-0000-0000-0000-000000000000")
            {
                var result = AccSvc.GetSiteMapMaster(0, strApplication);
                divSiteMapDetails.Style.Add("display", "block");
                grdSiteMap.DataSource = result;
                grdSiteMap.DataBind();
            }
            else
            {
                divSiteMapDetails.Style.Add("display", "none");
            }

        }

        protected void BindRoles()
        {
            Controller.AdminSVCs _objAdminSVCs = new Controller.AdminSVCs();
            string strApplication = ddlApplilcation.SelectedValue.ToString();
            var result = _objAdminSVCs.GetAllRole(0, 0, strApplication);
            // var result = AccSvc.GetAllRoles();
            CheckBoxList chkListRoles = (CheckBoxList)frmSiteNode.FindControl("chkListRoles");
            chkListRoles.DataSource = result;
            chkListRoles.DataTextField = "RoleName";
            chkListRoles.DataValueField = "RoleID";
            chkListRoles.DataBind();
            _objAdminSVCs = null;
        }

        protected void BindParent()
        {
            var result = AccSvc.GetSiteMapMaster(0, Convert.ToString(ddlApplilcation.SelectedValue));
            DropDownList ddlParent = (DropDownList)frmSiteNode.FindControl("ddlParent");
            //ddlParent.DataSource = (from r in result where r.IsActive == true && r.IsSiteMapNode == true select new { r.ID, r.Title }).ToList();
            ddlParent.DataSource = (from r in result where r.IsActive == true orderby r.Title select new { r.ID, r.Title }).ToList();

            ddlParent.DataTextField = "Title";
            ddlParent.DataValueField = "ID";
            ddlParent.DataBind();
        }

        protected void grdSiteMap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName == "Select")
            {
                List<MDMSVC.DC_SiteMap> objDS = new List<MDMSVC.DC_SiteMap>();
                objDS.Add(new MDMSVC.DC_SiteMap { ID = ID });
                frmSiteNode.ChangeMode(FormViewMode.Edit);
                frmSiteNode.DataSource = objDS;
                frmSiteNode.DataBind();

                BindParent();
                BindRoles();

                DropDownList ddlParent = (DropDownList)frmSiteNode.FindControl("ddlParent");
                CheckBoxList chkListRoles = (CheckBoxList)frmSiteNode.FindControl("chkListRoles");
                TextBox txtTitle = (TextBox)frmSiteNode.FindControl("txtTitle");
                TextBox txtUrl = (TextBox)frmSiteNode.FindControl("txtUrl");
                TextBox txtDescription = (TextBox)frmSiteNode.FindControl("txtDescription");
                CheckBox chkbIsSiteMapNode = (CheckBox)frmSiteNode.FindControl("chkbIsSiteMapNode");
                HiddenField hdnSiteMapID = (HiddenField)frmSiteNode.FindControl("hdnSiteMapID");

                hdnSiteMapID.Value = System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[0].Text);
                txtTitle.Text = System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[1].Text);
                txtDescription.Text = System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[2].Text);
                txtUrl.Text = System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[3].Text);
                chkbIsSiteMapNode.Checked = Convert.ToBoolean(grdSiteMap.Rows[index].Cells[6].Text);
                ddlParent.ClearSelection();

                //Disable parent dropdown and isSiteMap
                var ApplicationName = Convert.ToString(ConfigurationManager.AppSettings["ApplicationName"]);
                var SelectedApplication = Convert.ToString(ddlApplilcation.SelectedItem);
                if (SelectedApplication == "MDM")
                {
                    ddlParent.Enabled = true;
                    chkbIsSiteMapNode.Enabled = true;
                    if (ddlParent.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[4].Text)) != null)
                    {
                        ddlParent.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[4].Text)).Selected = true;
                    }
                }
                else
                {
                    ddlParent.Enabled = false;
                    chkbIsSiteMapNode.Enabled = false;
                }

                chkListRoles.ClearSelection();
                string[] rolelist = null;
                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[5].Text)))
                {
                    rolelist = System.Web.HttpUtility.HtmlDecode(grdSiteMap.Rows[index].Cells[5].Text).Split(new char[] { ',', ';' }, 512);
                    if (rolelist.Length > 0)
                    {
                        foreach (string role in rolelist)
                        {
                            if (chkListRoles.Items.FindByText(role) != null)
                            {
                                chkListRoles.Items.FindByText(role).Selected = true;
                            }
                        }
                    }

                }


            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_SiteMap newObj = new MDMSVC.DC_SiteMap
                {
                    ID = ID,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateSiteMapNode(newObj))
                {
                    GetSiteMapMaster();
                };
            }
            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_SiteMap newObj = new MDMSVC.DC_SiteMap
                {
                    ID = ID,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateSiteMapNode(newObj))
                {
                    GetSiteMapMaster();
                };
            }
        }

        protected void frmSiteNode_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlParent = (DropDownList)frmSiteNode.FindControl("ddlParent");
            CheckBoxList chkListRoles = (CheckBoxList)frmSiteNode.FindControl("chkListRoles");
            TextBox txtTitle = (TextBox)frmSiteNode.FindControl("txtTitle");
            TextBox txtUrl = (TextBox)frmSiteNode.FindControl("txtUrl");
            TextBox txtDescription = (TextBox)frmSiteNode.FindControl("txtDescription");
            CheckBox chkbIsSiteMapNode = (CheckBox)frmSiteNode.FindControl("chkbIsSiteMapNode");
            HiddenField hdnSiteMapID = (HiddenField)frmSiteNode.FindControl("hdnSiteMapID");

           

            if (e.CommandName.ToString() == "Add")
            {
                TLGX_Consumer.MDMSVC.DC_SiteMap newObj = new MDMSVC.DC_SiteMap();
                newObj.SiteMap_ID = Guid.NewGuid();
                newObj.ID = 0;
                newObj.Create_Date = DateTime.Now;
                newObj.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                newObj.Description = txtDescription.Text;
                newObj.IsActive = true;
                newObj.IsSiteMapNode = chkbIsSiteMapNode.Checked;
                newObj.ApplicationID = Guid.Parse(ddlApplilcation.SelectedValue);
                if (ddlParent.SelectedIndex != 0)
                {
                    newObj.ParentID = int.Parse(ddlParent.SelectedValue.ToString());
                }
                else
                {
                    newObj.ParentID = 1;
                }

                List<MDMSVC.DC_SiteMap_Roles> roles = new List<MDMSVC.DC_SiteMap_Roles>();

                foreach (ListItem item in chkListRoles.Items)
                {
                    if (item.Selected)
                    {
                        roles.Add(new MDMSVC.DC_SiteMap_Roles { RoleId = Guid.Parse(item.Value), RoleName = item.Text });
                    }
                }
                newObj.Roles = roles.ToArray();
                newObj.Title = txtTitle.Text;
                newObj.Url = txtUrl.Text;
                AccSvc.AddSiteMapNode(newObj);
                newObj = null;
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "SiteMap Created Successfully.", BootstrapAlertType.Success);
            }
            else if (e.CommandName.ToString() == "Modify")
            {
                Guid mySiteMap_Id = Guid.Parse(grdSiteMap.SelectedDataKey.Value.ToString());

                TLGX_Consumer.MDMSVC.DC_SiteMap newObj = new MDMSVC.DC_SiteMap();
                newObj.ID = Convert.ToInt32(hdnSiteMapID.Value);
                newObj.SiteMap_ID = mySiteMap_Id;
                newObj.Edit_Date = DateTime.Now;
                newObj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                newObj.Description = txtDescription.Text;
                newObj.ApplicationID = Guid.Parse(ddlApplilcation.SelectedValue);

                newObj.IsActive = true;
                newObj.IsSiteMapNode = chkbIsSiteMapNode.Checked;

                if (ddlParent.SelectedIndex != 0)
                {
                    newObj.ParentID = int.Parse(ddlParent.SelectedValue.ToString());
                }

                List<MDMSVC.DC_SiteMap_Roles> roles = new List<MDMSVC.DC_SiteMap_Roles>();

                foreach (ListItem item in chkListRoles.Items)
                {
                    if (item.Selected)
                    {
                        roles.Add(new MDMSVC.DC_SiteMap_Roles { RoleId = Guid.Parse(item.Value), RoleName = item.Text });
                    }
                }

                newObj.Title = txtTitle.Text;
                newObj.Url = txtUrl.Text;

                AccSvc.UpdateSiteMapNode(newObj);
                newObj = null;

            }
            hdnFlag.Value = "true";
            frmSiteNode.ChangeMode(FormViewMode.Insert);
            frmSiteNode.DataBind();
            BootstrapAlert.BootstrapAlertMessage(dvMsg, "SiteMap Edited Successfully.", BootstrapAlertType.Success);
            GetSiteMapMaster();
            //BindParent();
            //BindRoles();
        }

        protected void grdSiteMap_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    foreach (LinkButton button in e.Row.Cells[8].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "SoftDelete")
                        {
                            button.OnClientClick = "return confirmDelete();";
                        }
                    }
                }
            }

        }

        protected void ddlApplilcation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSiteMapMaster();
        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmSiteNode.ChangeMode(FormViewMode.Insert);
            frmSiteNode.DataBind();
            DropDownList ddlParent = (DropDownList)frmSiteNode.FindControl("ddlParent");
            CheckBox chkbIsSiteMapNode = (CheckBox)frmSiteNode.FindControl("chkbIsSiteMapNode");
            var SelectedApplication = Convert.ToString(ddlApplilcation.SelectedItem);
            if (SelectedApplication == "MDM")
            {
                ddlParent.Enabled = true;
                chkbIsSiteMapNode.Enabled = true;
                BindParent();
            }
            else
            {
                ddlParent.Enabled = false;
                chkbIsSiteMapNode.Enabled = false;
            }
            BindRoles();
        }
    }
}