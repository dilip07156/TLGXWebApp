using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata
{
    public partial class CountryWiseDashBoardReport : System.Web.UI.Page
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();

        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (!_obj.IsRoleAuthorizedForUrl())
            {
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
        }

        protected void ShowReport()
        {
            var DataSet1 = MapSvc.GetNewDashboardReport_CountryWise();
            ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
            CountryReportViewer.LocalReport.DataSources.Clear();
            CountryReportViewer.LocalReport.ReportPath = "staticdata/HotelMappingCountryReport.rdlc";
            CountryReportViewer.LocalReport.DataSources.Add(rds);
            CountryReportViewer.Visible = true;
            CountryReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            CountryReportViewer.DataBind();
            CountryReportViewer.LocalReport.Refresh();
        }
    }
}