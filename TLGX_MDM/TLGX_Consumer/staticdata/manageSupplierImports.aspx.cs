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
                fillsuppliers();

            }
        }



        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = objMasterDataDAL.GetSupplierMasterData();
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
            supplierwisedata.Style.Add("display", "none");
            string SupplierID = ddlSupplierName.SelectedValue;
            Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
            var DataSet1 = MapSvc.GetsupplierwiseUnmappedCountryReport(SupplierID);
            var DataSet2 = MapSvc.GetsupplierwiseUnmappedCityReport(SupplierID);
            var DataSet3 = MapSvc.GetsupplierwiseUnmappedProductReport(SupplierID);
            var DataSet4 = MapSvc.GetsupplierwiseUnmappedActivityReport(SupplierID);
            var DataSet5 = MapSvc.GetsupplierwiseSummaryReport(SupplierID);
            ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);

            ReportDataSource rds2 = new ReportDataSource("DataSet2", DataSet2);
            ReportDataSource rds3 = new ReportDataSource("DataSet3", DataSet3);
            ReportDataSource rds4 = new ReportDataSource("DataSet4", DataSet4);
            ReportDataSource rds5 = new ReportDataSource("DataSet5", DataSet5);
            ReportViewersupplierwise.LocalReport.DataSources.Clear();

            ReportViewersupplierwise.LocalReport.DataSources.Add(rds);

            ReportViewersupplierwise.LocalReport.DataSources.Add(rds2);
            ReportViewersupplierwise.LocalReport.DataSources.Add(rds3);
            ReportViewersupplierwise.LocalReport.DataSources.Add(rds4);
            ReportViewersupplierwise.LocalReport.DataSources.Add(rds5);
            //ReportDataSource datasource = new ReportDataSource("DataSet1", DataSet1);
            //ReportViewer1.LocalReport.DataSources.Clear();
            // ReportViewer1.LocalReport.DataSources.Add(datasource);
            //Populate Report Paramater for passing current date (month)
            //  ReportParameter p1 = new ReportParameter("ReportParameter1", SupplierID);
            // ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
            ReportViewersupplierwise.Visible = true;
            ReportViewersupplierwise.DataBind(); // Added
            ReportViewersupplierwise.LocalReport.Refresh();
        }
    }
}