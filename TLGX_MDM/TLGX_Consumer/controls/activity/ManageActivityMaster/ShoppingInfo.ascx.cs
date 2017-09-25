using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityMaster
{
    public partial class ShoppingInfo : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
        }
    }
}