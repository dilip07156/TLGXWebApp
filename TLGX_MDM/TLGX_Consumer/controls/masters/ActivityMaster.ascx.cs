using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.masters
{
    public partial class ActivityMaster : System.Web.UI.UserControl
    {


        //needs postback handling
        protected void Page_Load(object sender, EventArgs e)
        {

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var activityMasterData = (from s in context.m_Activity_Master
                                          select new Models.DynamicWorkflow.ActivityMaster
                                          {
                                              Activity_Master_Id = s.Activity_Master_ID,
                                              Activity_Class_Name = s.Activity_Class_Name,
                                              Activity_Method_Name = s.Activity_Method_Name,
                                              Activity_Name = s.Activity_Name,
                                              Description = s.Description,
                                              Status = s.Status,
                                              Status_Message = s.Status_Message
                                          }
                                  ).ToList();

                grdActivityMasters.DataSource = activityMasterData;
                grdActivityMasters.DataBind();

            }
        }


        // binds formview to gridview selected item
        protected void grdActivityMasters_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid myRowId = Guid.Parse(grdActivityMasters.SelectedDataKey.Value.ToString());

            frmActivityMaster.ChangeMode(FormViewMode.Edit);

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var myActivityMaster = context.m_Activity_Master.Find(myRowId);
                var list = new List<Models.m_Activity_Master> { myActivityMaster };

                frmActivityMaster.DataSource = list;
                frmActivityMaster.DataBind();
            }
        }


        //  runs the insert on a new 
        protected void btnAddActivityMaster_Click(object sender, EventArgs e)
        {

            // get the textbox value and use it to create a new record in Project
            TextBox txtActivityName = (TextBox)frmActivityMaster.FindControl("txtActivityName");
            TextBox txtActivityClassName = (TextBox)frmActivityMaster.FindControl("txtActivityClassName");
            TextBox txtActivityMethodName = (TextBox)frmActivityMaster.FindControl("txtActivityMethodName");
            TextBox txtStatus = (TextBox)frmActivityMaster.FindControl("txtStatus");
            TextBox txtStatusMessage = (TextBox)frmActivityMaster.FindControl("txtStatusMessage");
            TextBox txtDescription = (TextBox)frmActivityMaster.FindControl("txtDescription");


            using (Models.TLGX_MAPPEREntities1 myEntity = new Models.TLGX_MAPPEREntities1())
            {
                Models.m_Activity_Master myActivityMaster= new Models.m_Activity_Master()
                {
                    Activity_Master_ID = Guid.NewGuid(),
                    Activity_Name = txtActivityName.Text.Trim(),
                    Activity_Class_Name = txtActivityClassName.Text.Trim(),
                    Activity_Method_Name = txtActivityMethodName.Text.Trim(),
                    Status = txtStatus.Text.Trim(),
                    Status_Message = txtStatusMessage.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };

                myEntity.m_Activity_Master.Add(myActivityMaster);
                myEntity.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab 


            }
        }

        protected void frmActivityMaster_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {


            string myDataKey = e.Keys["Activity_Master_ID"].ToString();
            Guid myRow_Id = Guid.Parse(myDataKey);

            TextBox txtActivityName = (TextBox)frmActivityMaster.FindControl("txtActivityName");
            TextBox txtActivityClassName = (TextBox)frmActivityMaster.FindControl("txtActivityClassName");
            TextBox txtActivityMethodName = (TextBox)frmActivityMaster.FindControl("txtActivityMethodName");
            TextBox txtStatus = (TextBox)frmActivityMaster.FindControl("txtStatus");
            TextBox txtStatusMessage = (TextBox)frmActivityMaster.FindControl("txtStatusMessage");
            TextBox txtDescription = (TextBox)frmActivityMaster.FindControl("txtDescription");


            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {

                var result = (from apr in context.m_Activity_Master
                              where apr.Activity_Master_ID == myRow_Id
                              select apr).First();

                result.UPDATE_DATE = DateTime.Now;
                result.UPDATE_USER = System.Web.HttpContext.Current.User.Identity.Name;

                result.Activity_Master_ID = myRow_Id;
                result.Activity_Name = txtActivityName.Text.Trim();
                result.Activity_Class_Name = txtActivityClassName.Text.Trim();
                result.Activity_Method_Name = txtActivityMethodName.Text.Trim();
                result.Status = txtStatus.Text.Trim();
                result.Status_Message = txtStatusMessage.Text.Trim();
                result.Description = txtDescription.Text.Trim();

                context.SaveChanges();

                // refresh page however you like, but you'll need to set focus to the Appproval Role Master tab



            } }

        //show-hide the insert mode
        protected void btnAddNewActivityMasterMaster_Click(object sender, EventArgs e)
        {
            frmActivityMaster.ChangeMode(FormViewMode.Insert);
        }
    }
}