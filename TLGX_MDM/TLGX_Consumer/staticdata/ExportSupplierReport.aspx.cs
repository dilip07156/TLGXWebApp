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

                ddlSupplierName.DataSource = _objMasterSVC.GetSupplier(new DC_Supplier_Search_RQ { PageNo =0, PageSize = int.MaxValue, StatusCode = "ACTIVE" });
                ddlSupplierName.DataValueField = "Supplier_Id";
                ddlSupplierName.DataTextField = "Name";
                ddlSupplierName.DataBind();

                getData(Guid.Empty, false);
            }
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnExportSuppilerCsv);
        }
        public void getData(Guid Supplier_id, bool isMDM)
        {
            var res = MapSvc.GetSupplierDataForExport(Supplier_id, isMDM);
            gvSupplier.DataSource = res;
            gvSupplier.DataBind();
        }

        protected void gvSupplier_DataBound(object sender, EventArgs e)
        {
            //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //TableHeaderCell cell1 = new TableHeaderCell();
            //cell1.Text = "";
            //cell1.ColumnSpan = 2;
            //row.Controls.Add(cell1);

            //TableHeaderCell cell2 = new TableHeaderCell();
            //cell2.Text = "COUNTRY MAPPING";
            ////cell2.Text = TextAlign.Right.;
            //cell2.ColumnSpan = 8;
            //row.Controls.Add(cell2);

            //TableHeaderCell cell3 = new TableHeaderCell();
            //cell3.Text = "CITY MAPPING";
            //cell3.ColumnSpan = 7;
            //row.Controls.Add(cell3);

            //TableHeaderCell cell4 = new TableHeaderCell();
            //cell4.Text = "HOTEL MAPPING";
            //cell4.ColumnSpan = 8;
            //row.Controls.Add(cell4);

            //TableHeaderCell cell5 = new TableHeaderCell();
            //cell5.Text = "ROOM TYPES MAPPING";
            //cell5.ColumnSpan = 10;
            //row.Controls.Add(cell5);
            //// row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            //gvSupplier.HeaderRow.Parent.Controls.AddAt(0, row);
        }

        protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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