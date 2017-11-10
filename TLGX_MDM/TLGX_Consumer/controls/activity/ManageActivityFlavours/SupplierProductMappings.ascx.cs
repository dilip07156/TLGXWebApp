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
    public partial class SupplierProductMappings : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new ActivitySVC();
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
            MDMSVC.DC_Activity_SupplierProductMapping_RQ _obj = new MDMSVC.DC_Activity_SupplierProductMapping_RQ();
            _obj.Activity_ID = Activity_Flavour_Id;
            var res = ActSVC.GetActivitySupplierProductMapping(_obj);
            if (res.Count > 0 || res != null)
            {
                grdSupplierProductMapping.DataSource = res;
                grdSupplierProductMapping.DataBind();
            }
            else
            {
                grdSupplierProductMapping.DataSource = null;
                grdSupplierProductMapping.DataBind();
            }
        }
        protected void grdSupplierProductMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdSupplierProductMapping_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void frmSupplierProductMapping_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}