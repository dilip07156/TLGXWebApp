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
           
        }

        protected void btnRuleCsv_Click(object sender, EventArgs e)
        {
          
            parm.Fromdate = fromDate.Value.ToString();
            parm.ToDate = toDate.Value.ToString();
            var DataSet1 = MapSvc.getStatisticforRuleReport(parm);
            ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
            ReportViewer1.LocalReport.DataSources.Clear();
           // ReportViewer1.LocalReport.ReportPath = "rptRuleReport.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.Visible = true;
            ReportViewer1.DataBind(); 
            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnStatusCsv_Click(object sender, EventArgs e)
        {
            parm.Fromdate = fromDate.Value.ToString();
            parm.ToDate = toDate.Value.ToString();
            var DataSet1 = MapSvc.getStatisticforStatusReport(parm);
            ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.ReportPath = "hotels/rptStatusreport.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.Visible = true;
            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnUpdateCsv_Click(object sender, EventArgs e)
        {
            parm.Fromdate = fromDate.Value.ToString();
            parm.ToDate = toDate.Value.ToString();
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