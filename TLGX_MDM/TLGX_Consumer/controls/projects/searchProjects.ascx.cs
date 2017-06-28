using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.projects
{
    public partial class searchProjects : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillProjectDetails(0);
            }
        }

        private void fillProjectDetails(int pageindex)
        {
            dataProjectDAL dataProject = new dataProjectDAL();
            List<Models.dataProject.Project> projectData = dataProject.getAllProjects();
            grdProjectList.DataSource = projectData;
            grdProjectList.PageIndex = pageindex;
            grdProjectList.DataBind();
        }

        protected void grdProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillProjectDetails(e.NewPageIndex);
        }
    }
}