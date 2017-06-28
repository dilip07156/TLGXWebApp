using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.projects
{
    public partial class manageMasterTeam : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

            }
            else
            {
                if (Request.QueryString["Team_Id"] != null)
                {

                    frmMasterTeam.ChangeMode(FormViewMode.Edit);
                    // use Team_Id to populate the datasource

                    Guid myTeam_Id = Guid.Parse(Request.QueryString["Team_Id"]);            
                    using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
                    {
                        var Team = context.m_Teams.Find(myTeam_Id);
                        var list = new List<Models.m_Teams> { Team };

                        frmMasterTeam.DataSource = list;
                        frmMasterTeam.DataBind();
                    }

                }

                else
                {
                    //leave the datasource blank and change to insert mode
                    frmMasterTeam.ChangeMode(FormViewMode.Insert);
                }
            }



        }

        protected void btnAddTeam_Click(object sender, EventArgs e)
        {

            // get the textbox value and use it to create a new record in Project
            TextBox txtTeamname = (TextBox)frmMasterTeam.FindControl("txtTeamname");

            using (Models.TLGX_MAPPEREntities1 myEntity = new Models.TLGX_MAPPEREntities1())
            {
                Models.m_Teams myTeam = new Models.m_Teams()
                {
                    Team_ID = Guid.NewGuid(),
                    Team_Name = txtTeamname.Text.Trim(),
                    Status = "Active",
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };

                myEntity.m_Teams.Add(myTeam);
                myEntity.SaveChanges();


                // ask rubesh of we can pull the last inserted row id from EDF

            };

        }
    }
}