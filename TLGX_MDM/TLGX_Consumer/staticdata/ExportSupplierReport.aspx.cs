using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.staticdata
{
    public partial class ExportSupplierReport : System.Web.UI.Page
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MDMSVC.DC_MappingStats parm = new MDMSVC.DC_MappingStats();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillAccomodationPriority(ddlAccoPriority);
                fillSupplier(ddlSupplierName, ddlSupplierPriority);
                ExportSupplierDetailsReport.Visible = false;
                //getData("0", Guid.Empty, false,"0");
            }
        }

        public void getData(string Priority, Guid Supplier_id, bool isMDM, string SuppPriority)
        {
            ExportSupplierDetailsReport.Visible = true;

            List<DC_SupplierExportDataReport> DsExportSupplierReport = new List<DC_SupplierExportDataReport>();
            DsExportSupplierReport = MapSvc.GetSupplierDataForExport(Priority, Supplier_id, isMDM, SuppPriority);


            ReportDataSource rds = new ReportDataSource("DsExportSupplierReport", DsExportSupplierReport);
            // ReportViewer ExportSupplierReport = new ReportViewer();
            ExportSupplierDetailsReport.LocalReport.DataSources.Clear();
            ExportSupplierDetailsReport.LocalReport.ReportPath = Server.MapPath("~/staticdata/ExportSupplierRDLCReport.rdlc");
            ExportSupplierDetailsReport.LocalReport.DataSources.Add(rds);
            ExportSupplierDetailsReport.Visible = true;
            ExportSupplierDetailsReport.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            ExportSupplierDetailsReport.DataBind();
            ExportSupplierDetailsReport.LocalReport.Refresh();

        }

        private void fillAccomodationPriority(DropDownList ddl)
        {
            lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
            MDMSVC.DC_M_masterattributelists list = LookupAtrributes.GetAllAttributeAndValuesByFOR("Accommodation", "Priority");

            try
            {
                list.MasterAttributeValues = list.MasterAttributeValues.OrderBy(x => Convert.ToInt32(x.AttributeValue)).ToArray();
            }
            catch
            {

            }
            ddl.Items.Clear();
            ddl.DataSource = list.MasterAttributeValues;
            ddl.DataValueField = "AttributeValue";
            ddl.DataTextField = "OTA_CodeTableValue";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--ALL--", "0"));
        }

        private void fillSupplier(DropDownList ddl, DropDownList ddlSupplierPriority)
        {
            var result = _objMasterSVC.GetSupplier(new DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue, StatusCode = "ACTIVE" });
            ddl.DataSource = result;
            ddl.DataValueField = "Supplier_Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();

            ddlSupplierPriority.DataSource = (from r in result where r.Priority != null orderby r.Priority select new { Priority = r.Priority }).Distinct().ToList(); ;
            ddlSupplierPriority.DataValueField = "Priority";
            ddlSupplierPriority.DataTextField = "Priority";
            ddlSupplierPriority.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            bool isMdm = chkIsMDMDataOnly.Checked;
            string AccoPriority = ddlAccoPriority.SelectedValue;
            string SuppPriority = ddlSupplierPriority.SelectedValue;

            if (ddlSupplierName.SelectedValue == "0")
            {
                getData(AccoPriority, Guid.Empty, isMdm, SuppPriority);
            }
            else
            {
                getData(AccoPriority, Guid.Parse(ddlSupplierName.SelectedValue), isMdm, SuppPriority);
            }
        }
    }
}