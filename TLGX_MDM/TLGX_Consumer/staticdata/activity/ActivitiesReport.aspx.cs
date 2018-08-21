using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.staticdata.activity
{
    public partial class ActivitiesReport : System.Web.UI.Page
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData(ddlReportType.SelectedIndex);
            }

        }
        protected void btnViewReport_Click(object sender, EventArgs e)
        {

            if (ddlReportType.SelectedValue == "0")
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, "Please select report type from dropdown", BootstrapAlertType.Warning);

            }
            else
            {
                getData((ddlReportType.SelectedIndex));
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            report.Visible = false;
            dvMsgAlert.Style.Add("display", "none");

        }
        

        public void getData(int ReportType)
        {
            report.Visible = true;
            var DsActivityCount = AccSvc.GetActivitiesReport(ReportType);
            ReportDataSource rds = new ReportDataSource("DsActivityCount", DsActivityCount);
            //ReportViewer ReportVieweractivity = new ReportViewer();
            ReportVieweractivity.LocalReport.DataSources.Clear();
            ReportVieweractivity.LocalReport.ReportPath = Server.MapPath("~/staticdata/activity/ActivitiesCountReport.rdlc");//"staticdata/activity/ActivitiesCountReport.rdlc";//;
            ReportVieweractivity.LocalReport.DataSources.Add(rds);
            ReportVieweractivity.Visible = true;
            ReportVieweractivity.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            ReportVieweractivity.DataBind();
            ReportVieweractivity.LocalReport.Refresh();
        }
    }
}