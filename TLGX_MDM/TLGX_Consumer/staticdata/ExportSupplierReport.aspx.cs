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
                fillSupplier(ddlSupplierName);
            }
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnExportSuppilerCsv);
        }

        public void getData(Guid Supplier_id, bool isMDM)
        {
            var res = MapSvc.GetSupplierDataForExport(Supplier_id, isMDM);
            gvSupplier.DataSource = res;
            gvSupplier.DataBind();
        }

        private void fillAccomodationPriority(DropDownList ddl)
        {
            lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
            MDMSVC.DC_M_masterattributelists list = LookupAtrributes.GetAllAttributeAndValuesByFOR("Accommodation", "Priority");

            try
            {
                list.MasterAttributeValues = list.MasterAttributeValues.OrderBy(x => Convert.ToInt32(x.AttributeValue)).ToArray();
            }
            catch (Exception ex)
            {

            }
            ddl.Items.Clear();
            ddl.DataSource = list.MasterAttributeValues;
            ddl.DataValueField = "AttributeValue";
            ddl.DataTextField = "OTA_CodeTableValue";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--ALL--", "0"));
        }

        private void fillSupplier(DropDownList ddl)
        {
            ddl.DataSource = _objMasterSVC.GetSupplier(new DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue, StatusCode = "ACTIVE" });
            ddl.DataValueField = "Supplier_Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExportSupplierCsv_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename = SupplierMappingReport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvSupplier.Columns.Count; k++)
            {
                //add separator
                sb.Append(gvSupplier.Columns[k].HeaderText + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < gvSupplier.Rows.Count; i++)
            {
                for (int k = 0; k < gvSupplier.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(gvSupplier.Rows[i].Cells[k].Text.Replace("&nbsp;", "")  + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            bool isMdm = chkIsMDMDataOnly.Checked;
            if (ddlSupplierName.SelectedValue == "0")
            {
                getData(Guid.Empty, isMdm);
            }
            else
            {
                getData(Guid.Parse(ddlSupplierName.SelectedValue), isMdm);
            }
        }
    }
}