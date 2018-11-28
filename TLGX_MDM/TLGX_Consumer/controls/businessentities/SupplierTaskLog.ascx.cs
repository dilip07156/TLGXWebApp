using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class SupplierTaskLog : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        ScheduleDataSVCs _objScheduleDataSVCs = new ScheduleDataSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();
                fillentities();
                if (!String.IsNullOrWhiteSpace(Request.QueryString["RedirectFromAlert"].ToString()))
                    fillgriddata(Request.QueryString["RedirectFromAlert"].ToString());

            }
        }

        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        private void fillentities()
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "MappingFileConfig", Name = "MappingEntity" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlEntity.Items.Clear();
                    ddlEntity.DataSource = result;
                    ddlEntity.DataTextField = "AttributeValue";
                    ddlEntity.DataValueField = "MasterAttributeValue_Id";
                    ddlEntity.DataBind();

                }
        }

        protected void btnSearch_Click(object sender, EventArgs args)
        {
            fillgriddata(Request.QueryString["RedirectFromAlert"].ToString());
        }

        private void fillgriddata(string RedirectFromAlert)
        {
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            string strUserName = System.Web.HttpContext.Current.User.Identity.Name;
            MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
            RQ.UserName = System.Web.HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(RedirectFromAlert))
            {
                RQ.RedirectFrom = RedirectFromAlert;
                //if (dtFrom.Value != string.Empty)
                //    RQ.FromDate = Convert.ToDateTime(dtFrom.Value);
                //if (dtFrom.Value != string.Empty)
                //    RQ.FromDate = Convert.ToDateTime(dtFrom.Value);
                if (ddlSupplierName.SelectedItem.Value != "0")
                    RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
                if (ddlEntity.SelectedItem.Value != "0")
                    RQ.Entity = ddlEntity.SelectedValue;
                if (ddlstatus.SelectedItem.Value != "0")
                    RQ.Status = ddlstatus.SelectedValue;


                //RQ.PageNo = PageIndex;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                //RQ.SortBy = (SortBy + " " + SortEx).Trim();
            }

            var res = _objScheduleDataSVCs.GetScheduleTaskByRoll(RQ);
            grdSupplierScheduleTask.DataSource = res;
            grdSupplierScheduleTask.PageIndex = 0;//Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            grdSupplierScheduleTask.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            grdSupplierScheduleTask.DataBind();
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void grdSupplierScheduleTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }

       

        private void LoadDownloadData(int pagesize, int pageno)
        {
            fillgriddata(Request.QueryString["RedirectFromAlert"].ToString());
        }

        protected void grdSupplierScheduleTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToString() == "Select")
            {
                MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

                // RQ.LogId = myRow_Id;

                ////// var result = _objMaster.Supplier_StaticDownloadData_Get(RQ);
                //// if (result.Count > 0)
                //// {

                //// }

                // lnkButtonAddUpdate.Text = "Modify";
                // lnkButtonAddUpdate.CommandName = "Modify";
                // lnkButtonAddUpdate.CommandArgument = myRow_Id.ToString();
            }


        }

        protected void btnReset_Click(object sender, EventArgs args)
        {
            //fillgriddata("");
        }
    }
}