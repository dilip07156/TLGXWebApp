using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Inclusions : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInclusions();
            }
        }
        protected void BindInclusions()

        {
            Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
            gvActInclusionSearch.DataSource = null;
            gvActInclusionSearch.DataBind();

        }

        protected void gvActInclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvActInclusionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}