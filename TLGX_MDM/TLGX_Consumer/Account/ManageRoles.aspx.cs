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


namespace TLGX_Consumer.Account
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
    public partial class ManageRoles : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(Models.ApplicationDbContext.Create());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var loggedInUser = User.Identity;
            var context = Models.ApplicationDbContext.Create();
           
            if (!IsPostBack)
            {
                if (HasPassword(manager))
                {
                    if (manager.IsInRole(loggedInUser.GetUserId(), "Administrator"))
                    {
                        //var userRoles = Roles.GetRolesForUser(User.Identity.GetUserId());
                        var userRoles = roleManager.Roles;
                        DataTable dtUsers = Models.ConversionClass.CreateDataTable(manager.Users.ToList());
                        ddlUsers.DataSource = dtUsers;
                        ddlUsers.DataValueField = "Id";
                        ddlUsers.DataTextField = "UserName";
                        ddlUsers.DataBind();
                        BindRolesDetails();
                        //BindUserRoles(userRoles);
                        //BindRolesDetails(userRoles);
                        dvDisplayRoles.Visible = false;
                        dvAssignRole.Visible = true;
                        dvCreateRole.Visible = true;
                    }
                    else
                    {
                        //List<string> userRoles;
                        List<string> roles;
                        List<string> users;

                        roles = (from r in roleManager.Roles select r.Name).ToList();

                        var userStore = new UserStore<Models.ApplicationUser>(context);
                        var userManager = new UserManager<Models.ApplicationUser>(userStore);

                        users = (from u in userManager.Users select u.UserName).ToList();

                        var user = userManager.FindByName(loggedInUser.GetUserName());
                        if (user == null)
                            throw new Exception("User not found!");

                        var userRoleIds = (from r in user.Roles select r.RoleId);
                        var userRoles = (from id in userRoleIds
                                     let r = roleManager.FindById(id)
                                     select new CustomRole {
                                       Name = r.Name
                                     }).ToList();
                        DataTable dtRoles = Models.ConversionClass.CreateDataTable(userRoles);
                        BindUserRoles(dtRoles);
                        dvDisplayRoles.Visible = true;
                        dvAssignRole.Visible = false;
                        dvCreateRole.Visible = false;
                    }
                }
            }
        }

        // This Method is used to bind user roles
        protected void BindUserRoles(DataTable userroles)
        {
            gvUserRoles.DataSource = userroles;
            gvUserRoles.DataMember = "Name";
            gvUserRoles.DataBind();
            lblUserRoleCount.Text = userroles.Rows.Count.ToString();
            gvUserRoles.DataBind();
        }
        
        protected void BindRolesDetails()
        {
            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            
            //var AllRoles = Roles.GetAllRoles();
            var AllRoles = roleManager.Roles.ToList();
            //txtRole.Text = AllRoles.Length.ToString();
            gvRoles.DataSource = AllRoles;
            gvRoles.DataMember = "Name";
            gvRoles.DataBind();
        }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }
        // This Button Click event is used to Create new Role
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            string roleName = txtRole.Text.Trim();
            
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
                context.SaveChanges();
                //Roles.CreateRole(roleName);
                lblResult.Text = roleName + " Role Created Successfully";
                lblResult.ForeColor = Color.Green;
                txtRole.Text = string.Empty;
                //var userRoles = Roles.GetRolesForUser(User.Identity.GetUserId());
                //BindUserRoles(userRoles);
                BindRolesDetails();
            }
            else
            {
                txtRole.Text = string.Empty;
                lblResult.Text = roleName + " Role already exists";
                lblResult.ForeColor = Color.Red;
            }
        }
               
        // This RowDeleting event is used to delete Roles
        protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            Label lableRole = (Label)gvRoles.Rows[e.RowIndex].FindControl("lblRole");
            var role = roleManager.FindByName(lableRole.Text);
            roleManager.Delete(role);
            //Roles.DeleteRole(lableRole.Text, false);
            lblResult.ForeColor = Color.Green;
            lblResult.Text = lableRole.Text + " Role Deleted Successfully";
           // var userRoles = Roles.GetRolesForUser(User.Identity.GetUserId());
            //BindUserRoles(userRoles);
            BindRolesDetails();
        }
        
        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var userRoles = Roles.GetRolesForUser(ddlUsers.SelectedItem.Text);
            BindRolesDetails();
        }

        protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            List<string> userRoles;
            List<string> roles;
            List<string> users;

            roles = (from r in roleManager.Roles select r.Name).ToList();

            var userStore = new UserStore<Models.ApplicationUser>(context);
            var userManager = new UserManager<Models.ApplicationUser>(userStore);

            users = (from u in userManager.Users select u.UserName).ToList();

            var user = userManager.FindByName(ddlUsers.SelectedItem.Text);
            if (user == null)
                throw new Exception("User not found!");

            var userRoleIds = (from r in user.Roles select r.RoleId);
            userRoles = (from id in userRoleIds
                         let r = roleManager.FindById(id)
                         select r.Name).ToList();

            //var userRoles = Roles.GetRolesForUser(ddlUsers.SelectedItem.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkAddRole = (CheckBox)e.Row.FindControl("chkAddRole");
                Label lblRole = (Label)e.Row.FindControl("lblRole");
                if (userRoles.Contains(lblRole.Text))
                {
                    chkAddRole.Checked = true;
                }
                else
                {
                    chkAddRole.Checked = false;
                }
            }
        }

        protected void btnAssignRole_Click(object sender, EventArgs e)
        {
            var context = Models.ApplicationDbContext.Create();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            int i = 0;
            for (i=0; i < gvRoles.Rows.Count; i++)
            {
                CheckBox chkAddRole = (CheckBox)gvRoles.Rows[i].Cells[2].FindControl("chkAddRole");
                Label lblRole = (Label)gvRoles.Rows[i].Cells[1].FindControl("lblRole");
                var user = manager.FindByName(ddlUsers.SelectedItem.Text);
                if (user == null)
                    throw new Exception("User not found!");

                var role = roleManager.FindByName(lblRole.Text);
                if (role == null)
                    throw new Exception("Role not found!");

                if (chkAddRole.Checked)
                {                    
                    if (!manager.IsInRole(user.Id, role.Name))
                    {
                        manager.AddToRole(user.Id, role.Name);
                        context.SaveChanges();
                    }
                    //if (!Roles.IsUserInRole(ddlUsers.SelectedItem.Text, lblRole.Text))
                    //{
                    //    Roles.AddUserToRole(ddlUsers.SelectedItem.Text, lblRole.Text);
                    //}
                    
                }
                else
                {
                    if (manager.IsInRole(user.Id, role.Name))
                    {
                        manager.RemoveFromRole(user.Id, role.Name);
                        context.SaveChanges();
                    }
                    //if (Roles.IsUserInRole(ddlUsers.SelectedItem.Text, lblRole.Text))
                    //{
                    //    Roles.RemoveUserFromRole(ddlUsers.SelectedItem.Text, lblRole.Text);
                    //}
                }
            }
        }
    }
}