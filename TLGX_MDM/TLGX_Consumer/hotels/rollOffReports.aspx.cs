using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;
using System.Configuration;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace TLGX_Consumer.hotels
{
    public partial class rollOffReports : System.Web.UI.Page
    {

        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MDMSVC.DC_RollOFParams parm = new MDMSVC.DC_RollOFParams();
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
            errordiv.Visible = false;
        }
        protected Boolean validatedate()
        {
            DateTime Fromdate = new DateTime();
            DateTime ToDate = new DateTime();

            try
            {
                string fd = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                Fromdate = Convert.ToDateTime(fd);
                string td = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                ToDate = Convert.ToDateTime(td);

                TimeSpan diff = ToDate - Fromdate;
                int days = diff.Days;
                if (days < 0)
                {
                    errorrange.InnerHtml = "Please select TO date greater than FROM date !!";
                    return false;
                }
                else if (days > 90)
                {
                    errorrange.InnerHtml = "Date Range between FROM date and TO date should not be more than 90 days!!";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                nulldate.InnerHtml = "Please select valid From and To date !!";
                return false;
            }
        }


        protected void btnRuleCsv_Click(object sender, EventArgs e)
        {
            var res = validatedate();
            if (res == false)
            {
                errordiv.Visible = true;
                ReportViewer1.Visible = false;
            }
            else
            {
                ReportViewer1.Visible = true;
                errordiv.Visible = false;
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                //parm.Fromdate = fromDate.Value.ToString();
                // parm.ToDate = toDate.Value.ToString();
                var DataSet1 = MapSvc.getStatisticforRuleReport(parm);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "hotels/rptRuleReport.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }

        }

        protected void btnStatusCsv_Click(object sender, EventArgs e)
        {
            var res = validatedate();
            if (res == false)
            {
                errordiv.Visible = true;
                ReportViewer1.Visible = false;
            }
            else
            {
                ReportViewer1.Visible = true;
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                var DataSet1 = MapSvc.getStatisticforStatusReport(parm);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "hotels/rptStatusreport.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void btnUpdateCsv_Click(object sender, EventArgs e)
        {
            var res = validatedate();
            if (res == false)
            {
                errordiv.Visible = true;
                ReportViewer1.Visible = false;
            }
            else
            {
                ReportViewer1.Visible = true;
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                var DataSet1 = MapSvc.getStatisticforUpdateReport(parm);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "hotels/rptUpdateReport.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}