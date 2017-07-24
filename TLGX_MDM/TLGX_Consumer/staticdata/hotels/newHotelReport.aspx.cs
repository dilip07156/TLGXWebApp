﻿using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata.hotels
{
    public partial class newHotelReport : System.Web.UI.Page
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
                errorrange.InnerHtml = "Please select valid From and To date !!";
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errordiv.Visible = false;
            ReportViewer1.Visible = false;
        }
        protected void btnviewreport_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;
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
                var DataSet1 = MapSvc.getNewHotelsAddedReport(parm);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "staticdata/hotels/rptNewhotelsReport.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}