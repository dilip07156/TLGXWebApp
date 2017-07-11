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
                ReportViewersupplierwise.Visible = false;
                supplierwisedata.Visible = true;
                //allsupplierdata.Visible = true;
                fillsuppliers();

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
           
            Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
            string SupplierID = ddlSupplierName.SelectedValue;
            if (SupplierID == "0")
            {
               
                var DataSet1 = MapSvc.GetsupplierwiseSummaryReport();
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptAllSupplierReport.rdlc"; 
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }
            else
            {
                var DataSet1 = MapSvc.GetsupplierwiseUnmappedSummaryReport(SupplierID);
                ReportDataSource rds1 = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptSupplierwiseReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds1);
                //Populate Report Paramater for passing current date (month)
                //  ReportParameter p1 = new ReportParameter("ReportParameter1", SupplierID);
                // ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.DataBind(); // Added
                ReportViewersupplierwise.LocalReport.Refresh();

            }
           
        }
    }
}