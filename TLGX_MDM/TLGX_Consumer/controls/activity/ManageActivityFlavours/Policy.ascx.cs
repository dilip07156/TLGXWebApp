using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Policy : System.Web.UI.UserControl
    {
        Controller.ActivitySVC activitySVC = new ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDataSource();
            }
        }

        private void BindDataSource()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            MDMSVC.DC_Activity_Policy_RQ _obj = new MDMSVC.DC_Activity_Policy_RQ();
            _obj.Activity_Id = Activity_Flavour_Id;
            
            var res = activitySVC.GetActivityPolicy(_obj);
            if(res!=null)
            {
                grdPolicy.DataSource = res;
                grdPolicy.DataBind();
            }
            else
            {
                grdPolicy.DataSource = null;
                grdPolicy.DataBind();
            }
        }
    }
}