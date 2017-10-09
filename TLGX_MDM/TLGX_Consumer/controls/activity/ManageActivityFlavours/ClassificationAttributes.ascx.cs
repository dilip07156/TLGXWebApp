using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ClassificationAttributes : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
        }

        protected void gvActivityCASearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvActivityCASearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvActivityCASearch.DataSource = null;
            gvActivityCASearch.DataBind();
        }
    }
}