using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.workflow
{
    public partial class workflowmessagecomposer : System.Web.UI.UserControl
    {

        // you need to handle the postback here dude
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var workflowMessageData = (from s in context.m_WorkFlowMessage
                                              select new Models.DynamicWorkflow.WorkFlowMessage
                                              {
                                                 WorkFlowMessage_Id = s.m_WorkFlowMessage_Id,
                                                 Cc = s.Cc,
                                                 Create_Date = s.Create_Date,
                                                 Create_User = s.Create_User,
                                                 Edit_Date = s.Edit_Date,
                                                 Edit_User = s.Edit_User,
                                                 From = s.From,
                                                 Subject = s.Subject,
                                                 Text = s.Text,
                                                 To = s.To
                                              }
                                  ).ToList();

                grdWorkFlowMessage.DataSource = workflowMessageData;
                grdWorkFlowMessage.DataBind();
            }
        }

        protected void btnAddNewWorkFlowMessage_Click(object sender, EventArgs e)
        {
            frmWorkFlowMessage.ChangeMode(FormViewMode.Insert);
        }

        protected void grdWorkFlowMessage_SelectedIndexChanged(object sender, EventArgs e)
        {

            Guid myRowId = Guid.Parse(grdWorkFlowMessage.SelectedDataKey.Value.ToString());

            frmWorkFlowMessage.ChangeMode(FormViewMode.Edit);

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var myWorkflowMessage = context.m_WorkFlowMessage.Find(myRowId);
                var list = new List<Models.m_WorkFlowMessage> { myWorkflowMessage };

                frmWorkFlowMessage.DataSource = list;
                frmWorkFlowMessage.DataBind();
            }
        }



        protected void frmWorkFlowMessage_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            string myDataKey = e.Keys["m_WorkFlowMessage_Id"].ToString();
            Guid myRow_Id = Guid.Parse(myDataKey);

            // get the textbox value and use it to create a new record in Project
            TextBox txtSubject = (TextBox)frmWorkFlowMessage.FindControl("txtSubject");
            TextBox txtTo = (TextBox)frmWorkFlowMessage.FindControl("txtTo");
            TextBox txtFrom = (TextBox)frmWorkFlowMessage.FindControl("txtFrom");
            TextBox txtCC = (TextBox)frmWorkFlowMessage.FindControl("txtCC");
            TextBox txtMessage = (TextBox)frmWorkFlowMessage.FindControl("txtMessage");


            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {

                var result = (from apr in context.m_WorkFlowMessage
                              where apr.m_WorkFlowMessage_Id== myRow_Id
                              select apr).First();

                result.m_WorkFlowMessage_Id = myRow_Id;
                result.Subject = txtSubject.Text.Trim();
                result.From = txtFrom.Text.Trim();
                result.To = txtTo.Text.Trim();
                result.Text = txtMessage.Text.Trim();
                result.Edit_Date = DateTime.Now;
                result.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

                context.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab


            }
            }

        protected void btnAddWorkFlowMessage_Click(object sender, EventArgs e)
        {

            // get the textbox value and use it to create a new record in Project
            TextBox txtSubject = (TextBox)frmWorkFlowMessage.FindControl("txtSubject");
            TextBox txtTo = (TextBox)frmWorkFlowMessage.FindControl("txtTo");
            TextBox txtFrom = (TextBox)frmWorkFlowMessage.FindControl("txtFrom");
            TextBox txtCC = (TextBox)frmWorkFlowMessage.FindControl("txtCC");
            TextBox txtMessage = (TextBox)frmWorkFlowMessage.FindControl("txtMessage");

            using (Models.TLGX_MAPPEREntities1 myEntity = new Models.TLGX_MAPPEREntities1())
            {
                Models.m_WorkFlowMessage myWorkflowMessage = new Models.m_WorkFlowMessage()
                {
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    To = txtTo.Text.Trim(),
                    From = txtFrom.Text.Trim(),
                    Cc = txtCC.Text.Trim(),
                    Text = txtMessage.Text.Trim(),
                    m_WorkFlowMessage_Id = Guid.NewGuid(),
                    Subject = txtSubject.Text.Trim()
                };

                myEntity.m_WorkFlowMessage.Add(myWorkflowMessage);
                myEntity.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab 

            }

        }
    }
}