using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.projects
{
    public partial class manageProjects : System.Web.UI.UserControl
    {
        Models.MasterDataDAL masterData = new Models.MasterDataDAL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack)
            {

            }
            else
            {
                if (Request.QueryString["Project_Id"] != null)

                {
                    frmProject.ChangeMode(FormViewMode.Edit);
                    // use Project_Id to populate the datasource

                    Guid myProject_ID = Guid.Parse(Request.QueryString["Project_Id"]);

                    using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
                    {

                        var projectData = context.Projects.Find(myProject_ID);

                        var list = new List<Models.Project> { projectData };

                        frmProject.DataSource = list;
                        frmProject.DataBind();

                        fillStatusDropdown();
                    }




                }

                else
                {
                    //leave the datasource blank and change to insert mode
                    frmProject.ChangeMode(FormViewMode.Insert);
                }
            }

        }

        private void fillStatusDropdown()
        {
           //List<Models.Statues> statusData = masterData.getAllStatuses();
            DropDownList ddl = frmProject.FindControl("ddlProjectStatus") as DropDownList;
            MasterDataSVCs _objMasterData = new MasterDataSVCs();
            ddl.DataSource = _objMasterData.GetAllStatuses();
            ddl.DataTextField = "Status_Name";
            ddl.DataValueField = "Status_Short";
            ddl.DataBind();
        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            // get the textbox value and use it to create a new record in Project
            TextBox txtProjectName = (TextBox)frmProject.FindControl("txtProjectName");

            using (Models.TLGX_MAPPEREntities1 myEntity = new Models.TLGX_MAPPEREntities1())
            {
                Models.Project myProject = new Models.Project()
                {
                    Project_ID = Guid.NewGuid(),
                    Project_Name = txtProjectName.Text.Trim(),
                    Status = "Active",
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };

                myEntity.Projects.Add(myProject);
                myEntity.SaveChanges();
            };
            // ask rubesh of we can pull the last inserted row id from EDF
        }
    }
}