﻿using System;
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
            if (!IsPostBack)
            {
                fillsuppliers();   
            }
            supplierwisedata.Visible = true;
            report.Visible = false;
            if (ddlSupplierName.SelectedValue == "0")
            {
                allsupplierdata.Visible = true;
            }
            else
            {
                allsupplierdata.Visible = false;
            }
        }



        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
            supplierwisedata.Visible = false;
            allsupplierdata.Visible = false;
            report.Visible = true;
            if (ddlSupplierName.SelectedValue == "0")
            {
                var DataSet1 = MapSvc.GetsupplierwiseSummaryReport();
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptAllSupplierReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }
            else
            {
                string supplierid = ddlSupplierName.SelectedValue;
                var DataSet1 = MapSvc.GetsupplierwiseUnmappedSummaryReport(supplierid);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptSupplierwiseReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }


        }

        public void InsertFileRecord(string Entity)
        {
            Guid SupplierImportFile_Id = Guid.NewGuid();
            MappingSVCs _objMappingSVCs = new MappingSVCs();
            MDMSVC.DC_SupplierImportFileDetails _objFileDetails = new MDMSVC.DC_SupplierImportFileDetails();
            _objFileDetails.SupplierImportFile_Id = SupplierImportFile_Id;
            _objFileDetails.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedValue);
            _objFileDetails.Entity = Entity;
            _objFileDetails.OriginalFilePath = "";
            _objFileDetails.SavedFilePath = "";
            _objFileDetails.STATUS = "UPLOADED";
            _objFileDetails.Mode = "RE_RUN";
            _objFileDetails.CREATE_DATE = DateTime.Now;
            _objFileDetails.CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name;
            MDMSVC.DC_Message _objMsg = _objMappingSVCs.SaveSupplierStaticFileDetails(_objFileDetails);
            //file Process logic
            MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails();
            MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
            RQ.SupplierImportFile_Id = SupplierImportFile_Id;
            RQ.PageNo = 0;
            RQ.PageSize = int.MaxValue;
            var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQ);
            obj.SupplierImportFile_Id = res[0].SupplierImportFile_Id;
            obj.Supplier_Id = res[0].Supplier_Id;
            obj.Supplier = res[0].Supplier;
            obj.SavedFilePath = res[0].SavedFilePath;
            obj.PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name;
            obj.Entity = res[0].Entity;
            obj.STATUS = res[0].STATUS;
            obj.Mode = res[0].Mode;
            var result = _objMappingSVCs.StaticFileUploadProcessFile(obj);
            //end
            //view File  details
            ClientScript.RegisterStartupScript(this.GetType(), "showDetailsModal", "showDetailsModal('" + SupplierImportFile_Id + "');", true);
            // Response.Redirect("~/staticdata/files/upload.aspx");
        }

        protected void btnCityReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("City");
            }
        }
        protected void btnCountryReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("Country");
            }
        }

        protected void btnHotelReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("Hotel");
            }
        }

        protected void btnRoomTypeReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("RoomType");
            }
        }

        protected void btnActivityReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("Activity");
            }
        } 
    }
}