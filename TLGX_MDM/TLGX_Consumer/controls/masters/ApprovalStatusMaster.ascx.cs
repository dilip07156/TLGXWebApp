using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.masters
{
    public partial class ApprovalStatusMaster : System.Web.UI.UserControl
    {
        //needs postback handling...
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var ApprovalStatusMasterData = (from s in context.m_Approval_StatusMaster
                                                select new Models.DynamicWorkflow.ApprovalStatusMaster
                                                {
                                                    Appr_status_id = s.Appr_status_id,
                                                    Object_Id = s.Object_id ?? Guid.Empty,
                                                    Object_type = s.Object_type,
                                                    Status = s.Status,
                                                    CREATE_DATE = s.CREATE_DATE,
                                                    CREATE_USER = s.CREATE_USER,
                                                    UPDATE_DATE = s.UPDATE_DATE,
                                                    UPDATE_USER = s.UPDATE_USER,
                                                    Status_hierarchy= s.Status_hierarchy ?? 0
                                                }
                                  ).ToList();

                grdApprovalStatusMasters.DataSource = ApprovalStatusMasterData;
                grdApprovalStatusMasters.DataBind();
            }


        }


        // not handling OBJECT ID at the moment as i don't know how you will impliment it
        protected void frmApprovalStatusMasters_ItemInserting(object sender, FormViewInsertEventArgs e)
        {

            // get the textbox value and use it to create a new record in Project
            TextBox txtStatus = (TextBox)frmApprovalStatusMasters.FindControl("txtStatus");
            TextBox txtObjectType = (TextBox)frmApprovalStatusMasters.FindControl("txtObjectType");
            TextBox txtHierarchy = (TextBox)frmApprovalStatusMasters.FindControl("txtHierarchy");

            using (Models.TLGX_MAPPEREntities1 myEntity = new Models.TLGX_MAPPEREntities1())
            {
                Models.m_Approval_StatusMaster myApprovalStatusMaster = new Models.m_Approval_StatusMaster()
                {
                    Appr_status_id = Guid.NewGuid(),
                    Status = txtStatus.Text.Trim(),
                    Object_type = txtObjectType.Text.Trim(),
                    Object_id = Guid.Empty, // dummying in a GUID
                    Status_hierarchy = int.Parse(txtHierarchy.Text.Trim()), // you'll need to add validation on this bit             
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };

                myEntity.m_Approval_StatusMaster.Add(myApprovalStatusMaster);
                myEntity.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab 


            }

        }

        protected void grdApprovalStatusMasters_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid myRowId = Guid.Parse(grdApprovalStatusMasters.SelectedDataKey.Value.ToString());

            frmApprovalStatusMasters.ChangeMode(FormViewMode.Edit);

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var myApprovalRoleMaster = context.m_Approval_StatusMaster.Find(myRowId);
                var list = new List<Models.m_Approval_StatusMaster> { myApprovalRoleMaster };

                frmApprovalStatusMasters.DataSource = list;
                frmApprovalStatusMasters.DataBind();
            }

        }

        // not handling OBJECT ID at the moment as i don't know how you will impliment it
        protected void frmApprovalStatusMasters_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {

            string myDataKey = e.Keys["Appr_status_id"].ToString();
            Guid myRow_Id = Guid.Parse(myDataKey);


            TextBox txtStatus = (TextBox)frmApprovalStatusMasters.FindControl("txtStatus");
            TextBox txtObjectType = (TextBox)frmApprovalStatusMasters.FindControl("txtObjectType");
            TextBox txtHierarchy = (TextBox)frmApprovalStatusMasters.FindControl("txtHierarchy");


            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {

                var result = (from apr in context.m_Approval_StatusMaster
                              where apr.Appr_status_id == myRow_Id
                              select apr).First();

                {
                    result.Status = txtStatus.Text.Trim();
                    result.Object_type = txtObjectType.Text.Trim();
                    result.Object_id = Guid.Empty;                      // dummying in a GUID as i'm not sure what's happening
                    result.UPDATE_DATE = DateTime.Now;
                    result.UPDATE_USER = System.Web.HttpContext.Current.User.Identity.Name;
                    result.Status_hierarchy = int.Parse(txtHierarchy.Text.Trim());
                }

                context.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab


            }
        }
    }
}