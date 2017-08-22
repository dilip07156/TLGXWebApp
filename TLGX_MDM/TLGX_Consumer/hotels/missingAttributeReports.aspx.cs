using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace TLGX_Consumer.hotels
{
    public partial class missingAttributeReports : System.Web.UI.Page
    {
        MDMSVC.DC_Accomodation_Search_RQ param = new MDMSVC.DC_Accomodation_Search_RQ();
        Controller.AccomodationSVC objAcco = new Controller.AccomodationSVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            errordiv.Visible = false;
            rvMissingAttributeReport.Visible = false;
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            DateTime Fromdate = new DateTime();
            DateTime ToDate = new DateTime();
            string fd = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            Fromdate = Convert.ToDateTime(fd);
            string td = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            ToDate = (Convert.ToDateTime(td)).AddDays(1);
            //rvMissingAttributeReport.Visible = true;
            //var res = validatedate();
            //if (res == false)
            //{
            //    errordiv.Visible = true;
            //    rvMissingAttributeReport.Visible = false;
            //}
            //else
            //{
            rvMissingAttributeReport.Visible = true;
            errordiv.Visible = false;
            param.FromDate = Fromdate;
            param.ToDate = ToDate;
            var res = objAcco.GetAccomodationMissingAttributeDetails(param);

            if (res != null)
            {
                ReportDataSource rds = new ReportDataSource("DataSet1", res);
                rvMissingAttributeReport.LocalReport.DataSources.Clear();
                rvMissingAttributeReport.LocalReport.ReportPath = "hotels/rptMissingAttribute.rdlc";
                rvMissingAttributeReport.LocalReport.DataSources.Add(rds);
                rvMissingAttributeReport.Visible = true;
                rvMissingAttributeReport.DataBind();
                rvMissingAttributeReport.LocalReport.Refresh();
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
            }
        }


        //protected Boolean validatedate()
        //{
        //    DateTime Fromdate = new DateTime();
        //    DateTime ToDate = new DateTime();

        //    try
        //    {
        //        string fd = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
        //        Fromdate = Convert.ToDateTime(fd);
        //        string td = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
        //        ToDate = Convert.ToDateTime(td);

        //        TimeSpan diff = ToDate - Fromdate;
        //        int days = diff.Days;
        //        if (days < 0)
        //        {
        //            errorrange.InnerHtml = "Please select TO date greater than FROM date !!";
        //            return false;
        //        }
        //        else if (days > 90)
        //        {
        //            errorrange.InnerHtml = "Date Range between FROM date and TO date should not be more than 90 days!!";
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        errorrange.InnerHtml = "Please select valid From and To date !!";
        //        return false;
        //    }
        //}

    }
}