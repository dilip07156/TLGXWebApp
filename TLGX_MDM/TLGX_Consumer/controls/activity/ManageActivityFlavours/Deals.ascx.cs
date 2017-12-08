using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Deals : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSvc = new Controller.ActivitySVC();
        //Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataSource(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }
        private void BindDataSource(int pagesize, int pageno)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Deals_RQ _obj = new MDMSVC.DC_Activity_Deals_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;

            var res = ActSvc.GetActivityDeals(_obj);
            if (res != null)
            {
                gvDealsSearch.DataSource = res;
                gvDealsSearch.DataBind();
                if (res.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(res[0].TotalRecords);
                }
            }
            else
            {
                gvDealsSearch.DataSource = null;
                gvDealsSearch.DataBind();
            }
        }
        protected void gvDealsSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDealsSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDealsSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}