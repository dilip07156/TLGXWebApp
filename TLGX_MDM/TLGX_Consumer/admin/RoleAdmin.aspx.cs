using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using System.Drawing;
using Microsoft.AspNet.Identity.EntityFramework;
using TLGX_Consumer.Models;
using TLGX_Consumer.App_Code;
using System.Configuration;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;



namespace TLGX_Consumer.admin
{
    public class CustomRole
    {
        string name;
        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
    public partial class RoleAdmin : System.Web.UI.Page
    {
        public int PageSize = 5;
        public int intPageIndex = 0;
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
            #region Variables

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(Models.ApplicationDbContext.Create());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var loggedInUser = User.Identity;
            var context = Models.ApplicationDbContext.Create();
            Controller.AdminSVCs _objAdminSVCs = new Controller.AdminSVCs();

            #endregion

            if (!IsPostBack)
            {
                BindPageData();
            }
        }

        private void BindPageData()
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

        protected void ddlApplilcation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bind Role Details
            BindRoles();
        }

        private void BindRoles()
        {
            try
            {
                Guid _ApplicaitonID = Guid.Parse(ddlApplilcation.SelectedValue);
                var result = _objAdminSVCs.GetAllRole(intPageIndex, PageSize, Convert.ToString(_ApplicaitonID));
                if (result != null)
                {
                    divRoleDetails.Style.Add("display", "block");
                    ddlShowEntries.Enabled = result.Count > 0 ? true : false;
                    gvRoles.DataSource = result;
                    gvRoles.PageIndex = intPageIndex;
                    gvRoles.PageSize = PageSize;
                    if (result != null)
                    {
                        if (result.Count > 0)
                        {
                            gvRoles.VirtualItemCount = result[0].TotalRecords;
                        }
                    }
                    gvRoles.DataBind();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            BindRoles();
        }

        protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            //Label lableRole = (Label)gvRoles.Rows[e.RowIndex].FindControl("lblRole");
            string strRole = Convert.ToString(gvRoles.Rows[e.RowIndex].Cells[0].Text);
            //Get Tagged User with this role
            var roleid = roleManager.FindByName(strRole);
            var _userlst = GetUsersInRole(roleid.Id);


            DeleteUsersInRole(Convert.ToString(roleid));
            var role = roleManager.FindByName(strRole);
            roleManager.Delete(role);
            BootstrapAlert.BootstrapAlertMessage(dvMsg, strRole + " Role deleted successfully", BootstrapAlertType.Success);
            BindRoles();
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString("N"), "HideLabel();", false);
        }

        //private void BindEntityType()
        //{
        //    AdminSVCs _obj = new AdminSVCs();
        //    DropDownList ddlEntityType = (DropDownList)frmRoledetail.FindControl("ddlEntityType");
        //    ddlEntityType.DataSource = _obj.GetEntityType();
        //    ddlEntityType.DataValueField = "EnityTypeID";
        //    ddlEntityType.DataTextField = "EntityTypeName";
        //    ddlEntityType.DataBind();
        //    ddlEntityType.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = "0" });
        //}
        private bool IsRoleExists(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            string strapplicationID = Convert.ToString(ddlApplilcation.SelectedValue);
            var role = roleManager.FindByName(roleName);
            bool blnFlag = false;
            try
            {
                if (role != null)
                {
                    DC_Roles _rol = new DC_Roles();
                    _rol.RoleID = role.Id;
                    _rol.ApplicationID = Guid.Parse(strapplicationID);
                    blnFlag = _objAdminSVCs.IsRoleExist(_rol);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return blnFlag;
        }

        private bool UpdateRoleEntityTagging(IdentityRole roleid)
        {
            DC_Roles _obj = new DC_Roles();
          //  DropDownList ddlEntityType = (DropDownList)frmRoledetail.FindControl("ddlEntityType");
            _obj.RoleID = roleid.Id;
           // _obj.EntityTypeID = Convert.ToString(ddlEntityType.SelectedValue);
            _obj.ApplicationID = Guid.Parse(ddlApplilcation.SelectedValue);
            bool blnflag = _objAdminSVCs.AddUpdateRoleEntityType(_obj);
            return blnflag;
        }


        public IQueryable<ApplicationUser> GetUsersInRole(string roleid)
        {
            var context = Models.ApplicationDbContext.Create();
            return from user in context.Users
                   where user.Roles.Any(r => r.RoleId == roleid)
                   select user;
        }
        public void DeleteUsersInRole(string roleid)
        {
            var context = Models.ApplicationDbContext.Create();
            var result = (from user in context.Users where user.Roles.Any(r => r.RoleId == roleid) select user);
            foreach (var item in result)
            {
                context.Users.Remove(item);
            }

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            BindRoles();
        }

        protected void frmRoledetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

           // DropDownList ddlEntityType = (DropDownList)frmRoledetail.FindControl("ddlEntityType");
            TextBox txtRoleName = (TextBox)frmRoledetail.FindControl("txtRoleName");
            Label lblApplication = (Label)frmRoledetail.FindControl("lblApplication");

            string roleName = txtRoleName.Text.Trim();
            if (e.CommandName == "Add")
            {
                if (!IsRoleExists(roleName))
                {
                    roleManager.Create(new IdentityRole(roleName));
                    context.SaveChanges();
                    var roleid = roleManager.FindByName(roleName);
                    if (UpdateRoleEntityTagging(roleid))
                    {
                        BindRoles();
                        hdnFlag.Value = "true";
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, roleName + " Role created successfully", BootstrapAlertType.Success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString("N"), "HideLabel();", false);
                    }
                    else
                    {
                        //If issue come for tagging role will automatic deleted.
                        roleManager.Delete(roleid);
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, roleName + " tagging has some issue. Please try later ", BootstrapAlertType.Warning);
                    }
                }
                else
                {

                    BootstrapAlert.BootstrapAlertMessage(msgAlert, roleName + " Role already exist !", BootstrapAlertType.Information);
                }
            }


        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Guid ID = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                MDMSVC.DC_Roles _obj = new MDMSVC.DC_Roles();
                _obj.RoleID = Convert.ToString(ID);
                _obj.RoleName = System.Web.HttpUtility.HtmlDecode(gvRoles.Rows[index].Cells[0].Text);
                //Label lblEntityType = (Label)gvRoles.Rows[index].Cells[1].FindControl("lblEntityType");
                //_obj.EntityType = System.Web.HttpUtility.HtmlDecode(lblEntityType.Text);
                //Label lblEntityTypeID = (Label)gvRoles.Rows[index].Cells[1].FindControl("lblEntityTypeID");
                //_obj.EntityTypeID = System.Web.HttpUtility.HtmlDecode(lblEntityTypeID.Text);

                List<MDMSVC.DC_Roles> rl = new List<MDMSVC.DC_Roles>();
                rl.Add(_obj);
                frmRoledetail.ChangeMode(FormViewMode.Edit);
                frmRoledetail.DataSource = rl;
                frmRoledetail.DataBind();
                TextBox txtRoleName = (TextBox)frmRoledetail.FindControl("txtRoleName");
                Label lblApplication = (Label)frmRoledetail.FindControl("lblApplication");
                // DropDownList ddlEntityType = (DropDownList)frmRoledetail.FindControl("ddlEntityType");

                lblApplication.Text = Convert.ToString(ddlApplilcation.SelectedItem);
                txtRoleName.Text = _obj.RoleName;
                //BindEntityType();
                // ddlEntityType.SelectedValue = _obj.EntityTypeID;
            }
        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {

            hdnFlag.Value = "false";
            frmRoledetail.ChangeMode(FormViewMode.Insert);
            frmRoledetail.DataBind();
            Label lblApplication = (Label)frmRoledetail.FindControl("lblApplication");
            lblApplication.Text = Convert.ToString(ddlApplilcation.SelectedItem);
            //BindEntityType();
        }

        protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (LinkButton button in e.Row.Cells[1].Controls.OfType<LinkButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.OnClientClick = "return confirmDelete();";
                    }
                }
            }
        }
    }
}