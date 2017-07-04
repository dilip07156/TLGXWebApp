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
        }

        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = objMasterDataDAL.GetSupplierMasterData();
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }
        protected void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            string SupplierID = ddlSupplierName.SelectedValue;
            Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
             var DataSet1 =MapSvc.GetsupplierwiseUnmappedCountryReport(SupplierID);
            var DataSet2 = MapSvc.GetsupplierwiseUnmappedCityReport(SupplierID);
            var DataSet3 = MapSvc.GetsupplierwiseUnmappedProductReport(SupplierID);
            var DataSet4 = MapSvc.GetsupplierwiseUnmappedActivityReport(SupplierID);

            ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);

            ReportDataSource rds2 = new ReportDataSource("DataSet2", DataSet2);
            ReportDataSource rds3 = new ReportDataSource("DataSet3", DataSet3);
            ReportDataSource rds4 = new ReportDataSource("DataSet4",DataSet4);
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportViewer1.LocalReport.DataSources.Add(rds);

            ReportViewer1.LocalReport.DataSources.Add(rds2);
            ReportViewer1.LocalReport.DataSources.Add(rds3);
            ReportViewer1.LocalReport.DataSources.Add(rds4);
            //ReportDataSource datasource = new ReportDataSource("DataSet1", DataSet1);
            //ReportViewer1.LocalReport.DataSources.Clear();
           // ReportViewer1.LocalReport.DataSources.Add(datasource);
            //Populate Report Paramater for passing current date (month)
          //  ReportParameter p1 = new ReportParameter("ReportParameter1", SupplierID);
           // ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
            ReportViewer1.Visible = true;
            ReportViewer1.DataBind(); // Added
            ReportViewer1.LocalReport.Refresh();
        }

        //protected void btnRuleCsv_Click(object sender, EventArgs e)
        //{
        //    MDMSVC.DC_RollOFParams parm = new MDMSVC.DC_RollOFParams();
        //    parm.Fromdate = fromDate.Value.ToString();
        //    parm.ToDate = toDate.Value.ToString();
        //    MappingSVCs _objmapping = new MappingSVCs();
        //    var res = _objmapping.getStatisticforRuleReport(parm);

        //    if (res != null && res.Count > 0)
        //    {
        //        //Writeing CSV file
        //        StringBuilder sb = new StringBuilder();

        //        string csv = string.Empty;
        //        List<string> lstFileHeader = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Get_RuleReport"]).Split(',').ToList();

        //        foreach (var item in res[0].GetType().GetProperties())
        //        {
        //            if (lstFileHeader.Contains(item.Name))
        //                csv += item.Name + ',';
        //        }
        //       sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6}", "Hotel Id", "Hotel Name", "Rule Name", "Description", "Internal Flag", "Last Updated Date", "Last Updated By"));
        //        sb.Append(string.Format("{0}", csv) + Environment.NewLine);

        //        foreach (var item in res)
        //        {
        //            sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6}", Convert.ToString(item.Hotelid),("\""+ Convert.ToString(item.Hotelname)+ "\""), ("\"" + Convert.ToString(item.RuleName) + "\""), ("\"" + Convert.ToString(item.Description) + "\""), Convert.ToString(item.Internal_Flag), Convert.ToString(item.LastupdateDate), Convert.ToString(item.LastupdatedBy)));
        //            sb.Append(Environment.NewLine);
        //        }

        //        byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());
        //        sb = null;
        //        if (bytes != null)
        //        {
        //            //Download the CSV file.
        //            var response = HttpContext.Current.Response;
        //            response.Clear();
        //            response.ContentType = "text/csv";
        //            response.AddHeader("Content-Length", bytes.Length.ToString());
        //            string filename = "RollOff_RuleReport";
        //            response.AddHeader("Content-disposition", "attachment; filename=\"" + filename + ".csv" + "\"");
        //            response.BinaryWrite(bytes);
        //            response.Flush();
        //            response.End();
        //        }
        //    }
        //}
    }
}