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
            Boolean result;

            DateTime Fromdate = new DateTime();
            DateTime ToDate = new DateTime();

            if (!DateTime.TryParse(txtFrom.Text.Trim(), out Fromdate) || !DateTime.TryParse(txtTo.Text.Trim(), out ToDate))
            {
                nulldate.InnerHtml = "Please select valid FROM and To date !!";
                result = false;
            }

            else
            {
                Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                TimeSpan diff = ToDate - Fromdate;
                int days = diff.Days;
                if (days < 0)
                {
                    errorrange.InnerHtml = "Please select FROM date greater than To date !!";
                    result = false;
                }
                 else if (days > 90)
                {
                    errorrange.InnerHtml = "Date Range between FROM date and TO date should not be more than 90 days!!";
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return result;

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
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
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
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                var DataSet1 = MapSvc.getStatisticforStatusReport(parm);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "hotels/rptStatusreport.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
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
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                var DataSet1 = MapSvc.getStatisticforUpdateReport(parm);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "hotels/rptUpdateReport.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}