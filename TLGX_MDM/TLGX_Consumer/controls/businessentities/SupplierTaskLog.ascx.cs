using System;
using System.Collections.Generic;
using System.Globalization;
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
        string RedirectFromAlert = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                fillsuppliers();
                fillentities();
                if (Request.QueryString["param"] != null)
                {
                    RedirectFromAlert = Request.QueryString["RedirectFromAlert"].ToString();
                    
                }
                fillgriddata(RedirectFromAlert,0,0);
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
                   
                    ddlEntity.DataSource = result;
                    ddlEntity.DataTextField = "AttributeValue";
                    ddlEntity.DataValueField = "MasterAttributeValue_Id";
                    ddlEntity.DataBind();

                }
        }

        protected void btnSearch_Click(object sender, EventArgs args)
        {
            fillgriddata(RedirectFromAlert,0,0);
        }

        private void fillgriddata(string RedirectFromAlert,int PageIndex,int PageSize)
        {
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            string strUserName = System.Web.HttpContext.Current.User.Identity.Name;
            MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
            RQ.UserName = System.Web.HttpContext.Current.User.Identity.Name;

            //if (!string.IsNullOrWhiteSpace(RedirectFromAlert))
            //{
                RQ.RedirectFrom = RedirectFromAlert;
            if (txtFrom.Text != string.Empty)

                RQ.FromDate = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", null); // DateTime.ParseExact(txtFrom.Text.Trim(), "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(txtFrom.Text.ToString("yyyy-MM-dd"));
            if (txtTo.Text != string.Empty)
                RQ.ToDate = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", null);// Convert.ToDateTime(txtTo.Text);
            if (ddlSupplierName.SelectedItem.Value != "0")
                    RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
                if (ddlEntity.SelectedItem.Value != "0")
                    RQ.Entity = ddlEntity.SelectedItem.Text;
                if (ddlstatus.SelectedItem.Value != "0")
                    RQ.Status = ddlstatus.SelectedValue;


                RQ.PageNo = PageIndex;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                //RQ.SortBy = (SortBy + " " + SortEx).Trim();
            //}

            var res = _objScheduleDataSVCs.GetScheduleTaskByRoll(RQ);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdSupplierScheduleTask.VirtualItemCount = res[0].TotalRecord;
                    lblTotalCount.Text = res[0].TotalRecord.ToString();
                }
                else
                    lblTotalCount.Text = "0";
            }
            else
                lblTotalCount.Text = "0";
            //if (res[0].LogType == "Log")
            //{
            //    grdSupplierScheduleTask.Columns[7].Visible = false;
            //    grdSupplierScheduleTask.Columns[6].Visible = false;
            //}
            grdSupplierScheduleTask.DataSource = res;
            grdSupplierScheduleTask.PageIndex = PageIndex;
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


        protected void grdSupplierScheduleTask_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            
           
        }

        protected void grdSupplierScheduleTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var myGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                string logtype = string.Empty;
                if (myGridView.DataKeys[index].Values[1] != null)
                    logtype = myGridView.DataKeys[index].Values[1].ToString();
                                  

                if (logtype == "Log")
                {
                    LinkButton btnupload = e.Row.FindControl("btnupload") as LinkButton;
                    LinkButton btnTaskComp = e.Row.FindControl("btnTaskComp") as LinkButton;
                    if (btnupload != null)
                    {
                        btnupload.Visible = false;
                    }
                    if (btnTaskComp != null)
                    {
                        btnTaskComp.Visible = false;
                    }
                    //myGridView.Columns[7].Visible = true;
                    //myGridView.Columns[6].Visible = true;
                }
                else
                {
                    //myGridView.Columns[7].Visible = false;
                    //myGridView.Columns[6].Visible = false;
                }
            }

        }


        private void LoadDownloadData(int pagesize, int pageno)
        {
            fillgriddata(RedirectFromAlert,pageno,pagesize);
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