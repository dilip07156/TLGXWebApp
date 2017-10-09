using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ActivityMedia : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Activity_Id =  new Guid( Request.QueryString["Activity_Id"]);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {

        }

        protected void gvActMediaSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvActMediaSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}