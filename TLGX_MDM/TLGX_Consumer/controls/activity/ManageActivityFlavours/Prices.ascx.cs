using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Prices : System.Web.UI.UserControl
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPrices(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }
        protected void BindPrices(int pagesize, int pageno)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Prices_RQ RQ = new MDMSVC.DC_Activity_Prices_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            RQ.PageNo = pageno;
            RQ.PageSize = pagesize;
            var result = AccSvc.GetActivityPrices(RQ);
            if (result != null)
            {
                gvPricesSearch.DataSource = result;
                gvPricesSearch.DataBind();
                if (result.Count() > 0)
                {
                    lblTotalRecords.Text = Convert.ToString(result[0].Totalrecords);
                }
            }
            else
            {
                gvPricesSearch.DataSource = null;
                gvPricesSearch.DataBind();
            }
        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPrices(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), gvPricesSearch.PageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void gvPricesSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvPricesSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindPrices(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);

        }

        protected void gvPricesSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}