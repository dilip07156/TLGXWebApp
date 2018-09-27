using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.staticdata.activity
{
    public partial class ActivitiesProductDetailsReport : System.Web.UI.Page
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {

                    LoadMasters();
                    getData(new DC_ActivityCountStats { SupplierID = ddlSupplierName.SelectedValue, CountryID = ddlCountry.SelectedValue, CityID = ddlCity.SelectedValue });
                }

            }
        }
        private void LoadMasters()
        {
            fillSupplierList(ddlSupplierName);
            fillcountries();
        }

        private void fillSupplierList(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = _objMasterSVC.GetSupplierByEntity(new MDMSVC.DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue, EntityType = "Activities" });
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Supplier_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
        }

        private void fillcountries()
        {
            ddlCountry.Items.Clear();
            var countryList = _objMasterSVC.GetAllCountries();
            ddlCountry.DataSource = countryList;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            InsertDefaultValuesInDDL(ddlCountry);
            ddlCountry.Items.Insert(1, new ListItem("-ALL Countries-", "1"));
        }

        private void InsertDefaultValuesInDDL(DropDownList ddl)
        {
            ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAndFillCity(ddlCountry.SelectedValue);
        }

        public void ClearAndFillCity(string SelectedValue)
        {
            Guid CountryId;
            ddlCity.Items.Clear();
            if (Guid.TryParse(SelectedValue, out CountryId))
            {
                fillcities(CountryId);
            }
            InsertDefaultValuesInDDL(ddlCity);
            ddlCity.Focus();
            ddlCity.Items.Insert(1, new ListItem("-ALL Cities-", "1"));
        }

        private void fillcities(Guid Country_Id)
        {
            var CityList = _objMasterSVC.GetMasterCityData(Country_Id.ToString());
            if (CityList != null)
            {
                if (CityList.Count() > 0)
                {
                    ddlCity.DataSource = CityList;
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataValueField = "City_Id";
                    ddlCity.DataBind();
                }
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            getData(new DC_ActivityCountStats { SupplierID = ddlSupplierName.SelectedValue, CountryID = ddlCountry.SelectedValue, CityID = ddlCity.SelectedValue });
        }

        protected void getData(DC_ActivityCountStats ActivityCountRequest)
        {
            report.Visible = true;
            var DsActivitiesProductDetails = AccSvc.GetActivitiesProductDetailsReport(ActivityCountRequest);

                ReportDataSource rds = new ReportDataSource("DsActivitiesProductDetails", DsActivitiesProductDetails);
                // ReportViewer ReportViewerActivityProductDetails = new ReportViewer();
                ReportViewerActivityProductDetails.LocalReport.DataSources.Clear();
                ReportViewerActivityProductDetails.LocalReport.ReportPath = Server.MapPath("~/staticdata/activity/ActivitiesProductDetailsRDLCReport.rdlc");
                ReportViewerActivityProductDetails.LocalReport.DataSources.Add(rds);
                ReportViewerActivityProductDetails.Visible = true;
                ReportViewerActivityProductDetails.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewerActivityProductDetails.DataBind();
                ReportViewerActivityProductDetails.LocalReport.Refresh();
        }
    }
}