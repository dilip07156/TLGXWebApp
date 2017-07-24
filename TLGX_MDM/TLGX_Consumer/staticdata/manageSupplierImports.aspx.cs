using System;
using System.Web;
using System.Web.UI;
using TLGX_Consumer.App_Code;
using System.Configuration;
using TLGX_Consumer.Controller;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;

namespace TLGX_Consumer.staticdata
{
    public partial class manageSupplierImports : System.Web.UI.Page
    {

        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();

        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();   
            }
            supplierwisedata.Visible = true;
            report.Visible = false;
            if (ddlSupplierName.SelectedValue == "0")
            {
                allsupplierdata.Visible = true;
            }
            else
            {
                allsupplierdata.Visible = false;
            }
        }



        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
            supplierwisedata.Visible = false;
            allsupplierdata.Visible = false;
            report.Visible = true;
            if (ddlSupplierName.SelectedValue == "0")
            {
                var DataSet1 = MapSvc.GetsupplierwiseSummaryReport();
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptAllSupplierReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }
            else
            {
                string supplierid = ddlSupplierName.SelectedValue;
                var DataSet1 = MapSvc.GetsupplierwiseUnmappedSummaryReport(supplierid);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptSupplierwiseReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }


        }
    }
}