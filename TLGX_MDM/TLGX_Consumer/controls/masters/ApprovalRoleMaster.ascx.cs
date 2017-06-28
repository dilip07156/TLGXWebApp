using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.masters
{
    public partial class ApprovalRoleMaster : System.Web.UI.UserControl
    {
        //needs postback handling
        protected void Page_Load(object sender, EventArgs e)
        {

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var approvalRoleMasterData = (from s in context.m_Approval_RoleMaster
                                   select new Models.DynamicWorkflow.ApprovalRoleMaster
                                   {                 
                                       Appr_Role_ID = s.Appr_Role_ID,
                                       Description = s.Description,
                                       Role_Name = s.Role_Name,
                                       Status = s.Status,
                                       CREATE_DATE= s.CREATE_DATE,
                                       CREATE_USER = s.CREATE_USER,
                                       UPDATE_DATE = s.UPDATE_DATE,
                                       UPDATE_USER = s.UPDATE_USER
                                   }
                                  ).ToList();

                grdApprovalRoleMasters.DataSource = approvalRoleMasterData;
                grdApprovalRoleMasters.DataBind();

            }

        }

        //performs the insert but no rebind added
        protected void btnAddApprovalRoleMaster_Click(object sender, EventArgs e)
        {

            // get the textbox value and use it to create a new record in Project
            TextBox txtApprovalRoleName = (TextBox)frmApprovalRoleMaster.FindControl("txtRoleName");
            TextBox txtDescription = (TextBox)frmApprovalRoleMaster.FindControl("txtDescription");
            DropDownList ddlStatus = (DropDownList)frmApprovalRoleMaster.FindControl("ddlStatus");

            using (Models.TLGX_MAPPEREntities1 myEntity = new Models.TLGX_MAPPEREntities1())
            {
                Models.m_Approval_RoleMaster myApprovalRoleMaster = new Models.m_Approval_RoleMaster()
                {
                    Appr_Role_ID = Guid.NewGuid(),
                    Role_Name = txtApprovalRoleName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Status = ddlStatus.SelectedValue.ToString(),
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };

                myEntity.m_Approval_RoleMaster.Add(myApprovalRoleMaster);
                myEntity.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab 

            }
        }

        protected void grdApprovalRoleMasters_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid myRowId = Guid.Parse(grdApprovalRoleMasters.SelectedDataKey.Value.ToString());

            frmApprovalRoleMaster.ChangeMode(FormViewMode.Edit);

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var myApprovalRoleMaster = context.m_Approval_RoleMaster.Find(myRowId);
                var list = new List<Models.m_Approval_RoleMaster> { myApprovalRoleMaster };

                frmApprovalRoleMaster.DataSource = list;
                frmApprovalRoleMaster.DataBind();

            }
        }

        protected void btnAddNewApprovalRoleMaster_Click(object sender, EventArgs e)
        {
            frmApprovalRoleMaster.ChangeMode(FormViewMode.Insert);
        }

        // performs the update, but no rebind added
        protected void frmApprovalRoleMaster_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {

            string myDataKey = e.Keys["Appr_Role_ID"].ToString();
            Guid myRow_Id = Guid.Parse(myDataKey);

            TextBox txtRoleName = (TextBox)frmApprovalRoleMaster.FindControl("txtRoleName");
            TextBox txtDescription = (TextBox)frmApprovalRoleMaster.FindControl("txtDescription");
            DropDownList ddlStatus = (DropDownList)frmApprovalRoleMaster.FindControl("ddlStatus");

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {

                var result = (from apr in context.m_Approval_RoleMaster
                              where apr.Appr_Role_ID == myRow_Id
                              select apr).First();

                result.UPDATE_DATE = DateTime.Now;
                result.UPDATE_USER = System.Web.HttpContext.Current.User.Identity.Name;
                result.Role_Name = txtRoleName.Text.Trim();
                result.Description = txtDescription.Text.Trim();
                result.Status = ddlStatus.SelectedValue.ToString();

                context.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab
                
             

            }


        }
    }
}