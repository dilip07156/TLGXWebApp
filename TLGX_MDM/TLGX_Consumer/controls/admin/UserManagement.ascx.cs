using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;
using TLGX_Consumer.Models;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.admin
{
    public partial class UserManagement : System.Web.UI.UserControl
    {
        #region variables
        public int PageSize = 5;
        public int intPageIndex = 0;
        public ApplicationUserManager manager;
        public RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        Controller.AdminSVCs _objAdminSVCs = new Controller.AdminSVCs();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (!IsPostBack)
                BindApplications();


        }

        private void BindApplications()
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

        private void BindEntityType()
        {
            AdminSVCs _obj = new AdminSVCs();
            DropDownList ddlEntityType = (DropDownList)frmUserdetail.FindControl("ddlEntityType");
            ddlEntityType.DataSource = _obj.GetEntityType();
            ddlEntityType.DataValueField = "EnityTypeID";
            ddlEntityType.DataTextField = "EntityTypeName";
            ddlEntityType.DataBind();
        }


        protected void frmUserdetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            // get the textbox value and use it to create a new record in Project
            TextBox txtEmail = (TextBox)frmUserdetail.FindControl("txtEmail");
            DropDownList ddlManager = (DropDownList)frmUserdetail.FindControl("ddlManager");
            TextBox txtPswd = (TextBox)frmUserdetail.FindControl("txtPswd");
            if (e.CommandName == "Edit")
            {

                List<RoleDetails> _lstrole = TaggedUserRole();
                if (_lstrole.Count > 0)
                {
                    //Get Updating UserID 
                    var context = Models.ApplicationDbContext.Create();
                    using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                    {
                        var user = userManager.FindByName(txtEmail.Text);
                        user.Email = txtEmail.Text;
                        user.UserName = txtEmail.Text;
                        if (Convert.ToString(txtPswd.Text) != String.Empty)
                        {
                            userManager.RemovePassword(user.Id);
                            userManager.AddPassword(user.Id, txtPswd.Text);
                        }

                        IdentityResult result = userManager.Update(user);

                        //If user has not created 
                        if (!result.Succeeded)
                        {

                            BootstrapAlert.BootstrapAlertMessage(msgAlert, ((string[])result.Errors)[0], BootstrapAlertType.Warning);
                        }
                        else if (result.Succeeded)
                        {
                            context.SaveChanges();
                            ////For Role tagging
                            //if (result.Succeeded)
                            //{
                            //foreach (DataRow dr in dtUserRoleTagged.Rows)
                            //{
                            //    // if(dr[])
                            //    manager.AddToRole(user.Id, Convert.ToString(dr["RoleName"]));
                            //    // manager.RemoveFromRole(user.Id,)
                            //}
                            foreach (var rol in _lstrole)
                            {
                                if (rol.IsAdded)
                                    manager.AddToRole(user.Id, Convert.ToString(rol.RoleName));
                                else 
                                {
                                    var _lstExistingRole = manager.GetRoles(user.Id);
                                    foreach (var exRole in _lstExistingRole)
                                    {
                                        if(exRole == rol.RoleName)
                                            manager.RemoveFromRole(user.Id, Convert.ToString(rol.RoleName));
                                    }
                                }
                            }
                            //Update UserTagging with Entity                           
                            if (!UpdateEntityTagging(user, "update"))
                            {
                                BootstrapAlert.BootstrapAlertMessage(dvMsg, "User has been added successfully.Entity tagging try later.", BootstrapAlertType.Information);
                            }
                            frmUserdetail.ChangeMode(FormViewMode.Insert);
                            frmUserdetail.DataBind();
                            RefreshControls();
                            hdnFlag.Value = "true";
                            BootstrapAlert.BootstrapAlertMessage(dvMsg, "User has been updated successfully", BootstrapAlertType.Success);
                        }
                    }

                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, "Please select atleast one role to create user", BootstrapAlertType.Warning);
                }
            }
            else if (e.CommandName == "Add")
            {
                var userManager1 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user1 = userManager1.FindByName(txtEmail.Text);
                if (user1 == null)
                {
                    //Check User Role Selected Or Not
                    List<RoleDetails> _lstrole = TaggedUserRole();
                    if (_lstrole.Count > 0)
                    {

                        var context = Models.ApplicationDbContext.Create();
                        Models.ApplicationUser _User = new Models.ApplicationUser();
                        _User.Email = txtEmail.Text;
                        _User.UserName = txtEmail.Text;
                        _User.PasswordHash = Convert.ToString(txtPswd.GetHashCode());


                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                        IdentityResult result = manager.Create(_User, txtPswd.Text);
                        //If user has not created 
                        if (!result.Succeeded)
                        {

                            BootstrapAlert.BootstrapAlertMessage(msgAlert, ((string[])result.Errors)[0], BootstrapAlertType.Warning);
                        }
                        else if (result.Succeeded)
                        {
                            context.SaveChanges();
                            ////For Role tagging
                            //if (result.Succeeded)
                            foreach (var rol in _lstrole)
                            {
                                if (rol.IsAdded)
                                    manager.AddToRole(_User.Id, Convert.ToString(rol.RoleName));
                                else
                                {
                                    var _lstExistingRole = manager.GetRoles(_User.Id);
                                    foreach (var exRole in _lstExistingRole)
                                    {
                                        if (exRole == rol.RoleName)
                                            manager.RemoveFromRole(_User.Id, Convert.ToString(rol.RoleName));
                                    }
                                }

                            }

                            //Update UserTagging with Entity
                            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                            var user = userManager.FindByName(txtEmail.Text);
                            if (!UpdateEntityTagging(user, "add"))
                            {
                                BootstrapAlert.BootstrapAlertMessage(dvMsg, "User has been added successfully.Entity tagging try later.", BootstrapAlertType.Information);
                            }
                            RefreshControls();
                            frmUserdetail.DataBind();
                            hdnFlag.Value = "true";
                            BootstrapAlert.BootstrapAlertMessage(dvMsg, "User has been added successfully", BootstrapAlertType.Success);
                        }
                    }
                    else
                    {
                        BootstrapAlert.BootstrapAlertMessage(msgAlert, "Please select atleast one role to create user", BootstrapAlertType.Warning);
                    }
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, "User alreay exist !!", BootstrapAlertType.Warning);
                }

            }
        }

        private bool UpdateEntityTagging(ApplicationUser user, string status)
        {
            try
            {
                if (user != null)
                {
                    DropDownList ddlEntityType = (DropDownList)frmUserdetail.FindControl("ddlEntityType");
                    DropDownList ddlEntity = (DropDownList)frmUserdetail.FindControl("ddlEntity");
                    DropDownList ddlManager = (DropDownList)frmUserdetail.FindControl("ddlManager");


                    DC_UserEntity _obj = new DC_UserEntity();
                    _obj.UserID = Guid.Parse(user.Id);
                    _obj.EntityTypeID = Convert.ToInt32(ddlEntityType.SelectedValue);
                    _obj.ManagerID = Convert.ToString(ddlManager.SelectedValue);
                    _obj.ApplicationId = Guid.Parse(ddlApplilcation.SelectedValue);
                    if (ddlEntity.SelectedValue != string.Empty)
                        _obj.EntityID = Guid.Parse(ddlEntity.SelectedValue);
                    else
                        _obj.EntityID = Guid.Empty;
                    if (status == "add")
                    {
                        _obj.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                        _obj.Create_Date = DateTime.Now;
                    }
                    else if (status == "update")
                    {
                        _obj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                        _obj.Edit_Date = DateTime.Now;
                    }
                    var result = _objAdminSVCs.AddUpdateUserEntity(_obj);
                    if (result != null)
                    {
                        if (result.StatusCode == ReadOnlyMessageStatusCode.Success) { return true; }
                        else { return false; }
                    }
                    else { return false; }
                }
                else { return false; }
            }
            catch (Exception)
            { throw; }
        }
        #region Controls Binding
        /// <summary>
        /// To Bind Manager Dropdown
        /// </summary>
        /// <param name="dtUsers"></param>
        private void BindManager()
        {
            var result = _objAdminSVCs.GetAllUsers(0, 0, ddlApplilcation.SelectedValue.ToString());
            DropDownList ddlManager = (DropDownList)frmUserdetail.FindControl("ddlManager");
            ddlManager.DataSource = result;
            ddlManager.DataValueField = "Userid";
            ddlManager.DataTextField = "UserName";
            ddlManager.DataBind();
            ddlManager.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = Guid.Empty.ToString() });
        }
        /// <summary>
        /// To Bind Role Data
        /// </summary>
        /// <param name="roles"></param>
        private void BindRole()
        {
            try
            {
                DC_Roles _objRole = new DC_Roles();
                DropDownList ddlEntityType = (DropDownList)frmUserdetail.FindControl("ddlEntityType");
                var result = _objAdminSVCs.GetAllRole(0, 0, Convert.ToString(ddlApplilcation.SelectedValue));
                //DataTable roles = Models.ConversionClass.CreateDataTable(roleManager.Roles.ToList());
                grdRoles.DataSource = result;
                grdRoles.DataBind();

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// To Bind User Details
        /// </summary>
        /// <param name="dtUsers"></param>

        #endregion
        #region Control Events

        private void RefreshControls()
        {
            try
            {
                //Refresh User Grid
                BindUserDetails(intPageIndex);

                //Refresh Role Table
                BindRole();

                //Reset Controls
                ResetControls();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ResetControls()
        {
            try
            {
                TextBox txtEmail = (TextBox)frmUserdetail.FindControl("txtEmail");
                DropDownList ddlManager = (DropDownList)frmUserdetail.FindControl("ddlManager");
                TextBox txtPswd = (TextBox)frmUserdetail.FindControl("txtPswd");
                TextBox txtConfirmPswd = (TextBox)frmUserdetail.FindControl("txtConfirmPswd");
                txtEmail.Text = String.Empty;
                ddlManager.SelectedValue = "00000000-0000-0000-0000-000000000000";
                txtPswd.Text = string.Empty;
                txtConfirmPswd.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public class RoleDetails
        {
            public Guid? RoleId { get; set; }
            public string RoleName { get; set; }
            public bool IsAdded { get; set; }
        }
        private List<RoleDetails> TaggedUserRole()
        {
            List<RoleDetails> _objRolelst = new List<RoleDetails>();
            try
            {
                foreach (GridViewRow row in grdRoles.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (CheckBox)(row.Cells[2].FindControl("chkAddRole"));
                        RoleDetails _obj = new RoleDetails();
                        string Id = row.Cells[0].Text;
                        Guid? guidId = Id == string.Empty ? Guid.Empty : Guid.Parse(Id);
                        _objRolelst.Add(new RoleDetails() { RoleId = guidId, RoleName = row.Cells[1].Text, IsAdded = chkRow.Checked });
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return _objRolelst;
        }

        //protected void grdListOfUsers_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}


        #endregion

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DropDownList ddlEntityType = (DropDownList)frmUserdetail.FindControl("ddlEntityType");
                DropDownList ddlEntity = (DropDownList)frmUserdetail.FindControl("ddlEntity");

                string selectedValue = ddlEntityType.SelectedValue;
                RequiredFieldValidator rfv = (RequiredFieldValidator)frmUserdetail.FindControl("rfvddlEntity");

                // BindRole();
                //Validation Enable disable
                if (selectedValue == "1")
                {
                    ddlEntity.Items.Clear();
                    ddlEntity.Enabled = false;
                    rfv.Enabled = false;
                }
                else
                {
                    ddlEntity.Enabled = true;
                    FillEntityDDL();
                    rfv.Enabled = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FillEntityDDL()
        {
            AdminSVCs _obj = new AdminSVCs();
            DropDownList ddlEntity = (DropDownList)frmUserdetail.FindControl("ddlEntity");
            DropDownList ddlEntityType = (DropDownList)frmUserdetail.FindControl("ddlEntityType");


            DC_EntityDetails _ed = new DC_EntityDetails();
            _ed.EntityTypeID = Convert.ToInt32(ddlEntityType.SelectedValue);
            var reslut = _obj.GetEntity(_ed);
            ddlEntity.DataSource = reslut;
            ddlEntity.DataValueField = "EntityID";
            ddlEntity.DataTextField = "EntityName";
            ddlEntity.DataBind();
            ddlEntity.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = "0" });
        }

        protected void grdListOfUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            BindUserDetails(intPageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            BindUserDetails(intPageIndex);
        }

        private void BindUserDetails(int pageindex)
        {
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            var result = _objAdminSVCs.GetAllUsers(pageindex, PageSize, ddlApplilcation.SelectedValue.ToString());
            divUserDetails.Style.Add("display", "block");
            grdListOfUsers.DataSource = result;
            grdListOfUsers.PageIndex = pageindex;
            grdListOfUsers.PageSize = PageSize;
            if (result != null)
            {
                if (result.Count > 0)
                {
                    grdListOfUsers.VirtualItemCount = result[0].TotalRecords;
                }
            }

            //gvRoles.VirtualItemCount = 
            grdListOfUsers.DataBind();
        }

        protected void grdListOfUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MDMSVC.DC_Message _msg = new DC_Message();


            if (e.CommandName == "Select")
            {
                dvMsg.Style.Add("display", "none");
                frmUserdetail.ChangeMode(FormViewMode.Edit);
                Guid userId = Guid.Parse(e.CommandArgument.ToString());
                using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                {
                    var user = userManager.FindById(Convert.ToString(userId));
                    IList<string> lstroleNames = userManager.GetRoles(Convert.ToString(userId));

                    Users_Contract userC = new Users_Contract();
                    userC.Email = user.Email;
                    userC.Manager = Guid.Empty;
                    List<Users_Contract> uc = new List<Users_Contract>();
                    uc.Add(userC);

                    //Set FormView to update

                    frmUserdetail.DataSource = uc;
                    frmUserdetail.DataBind();
                    BindManager();

                    TextBox txtEmail = (TextBox)frmUserdetail.FindControl("txtEmail");
                    DropDownList ddlManager = (DropDownList)frmUserdetail.FindControl("ddlManager");
                    DropDownList ddlEntityType = (DropDownList)frmUserdetail.FindControl("ddlEntityType");
                    DropDownList ddlEntity = (DropDownList)frmUserdetail.FindControl("ddlEntity");

                    txtEmail.ReadOnly = true;
                    txtEmail.Text = user.Email;
                    //Get extra user details like managerid,entitytypeid,entityid
                    BindEntityType();
                    DC_UserEntity _userdetails = new DC_UserEntity();
                    _userdetails.UserID = Guid.Parse(user.Id);
                    AdminSVCs _obj = new AdminSVCs();
                    var result = _obj.GetUserEntityDetails(_userdetails);
                    if (result != null)
                    {

                        ddlManager.SelectedValue = Convert.ToString(result.ManagerID);
                        ddlEntityType.SelectedValue = Convert.ToString(result.EntityTypeID);
                        if (result.EntityTypeID == 1)
                        {
                            ddlEntity.Items.Clear();
                            ddlEntity.Enabled = false;
                        }
                        else
                        {
                            FillEntityDDL();
                            ddlEntity.SelectedValue = Convert.ToString(result.EntityID);
                        }
                    }

                    //Set role grid
                    BindRole();
                    foreach (GridViewRow row in grdRoles.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string strRole = Convert.ToString(row.Cells[1].Text);
                            if (lstroleNames.Contains(strRole))
                            {
                                CheckBox chkRow = (CheckBox)(row.Cells[2].FindControl("chkAddRole"));
                                chkRow.Checked = true;
                            }
                        }

                    }
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                Guid userId = Guid.Parse(e.CommandArgument.ToString());
                _msg = _objAdminSVCs.UserSoftDelete(new MDMSVC.DC_UserDetails()
                {
                    Userid = Convert.ToString(userId),
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    intPageIndex = (String.IsNullOrEmpty(grdListOfUsers.PageIndex.ToString()) ? 0 : grdListOfUsers.PageIndex);
                    RefreshControls();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "User has been inactive successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }

            }
            else if (e.CommandName.ToString() == "UnDelete")
            {
                Guid userId = Guid.Parse(e.CommandArgument.ToString());
                _msg = _objAdminSVCs.UserSoftDelete(new MDMSVC.DC_UserDetails()
                {
                    Userid = Convert.ToString(userId),
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    intPageIndex = (String.IsNullOrEmpty(grdListOfUsers.PageIndex.ToString()) ? 0 : grdListOfUsers.PageIndex);
                    RefreshControls();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "User has been retrived successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmUserdetail.ChangeMode(FormViewMode.Insert);
            frmUserdetail.DataBind();

            BindRole();

            //Bind Manager DropDown for create or Update User
            BindManager();

            //Bind EntityType
            BindEntityType();
        }

        protected void ddlApplilcation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUserDetails(intPageIndex);
        }

        protected void grdListOfUsers_RowDataBound(object sender, GridViewRowEventArgs e)
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