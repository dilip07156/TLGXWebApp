using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata.hotels
{
    public partial class AccommodationMappingSupplierVSupplier : System.Web.UI.Page
    {
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewerAccommodationMappingSupplierVSupplier.Visible = false;

            if (!IsPostBack)
            {

                LoadMasters();
            }

        }

        protected void GetAccommodationSupplierVSuplierReport(TLGX_Consumer.MDMSVC.DC_SupplerVSupplier_Report_RQ dC_SupplerVSupplier_Report_RQ)
        {

            report.Visible = true;
            List<TLGX_Consumer.MDMSVC.DC_SupplierAccoMappingExportDataReport> DsSupplierAccoMappingExportDataReport = new List<TLGX_Consumer.MDMSVC.DC_SupplierAccoMappingExportDataReport>();
            if (dC_SupplerVSupplier_Report_RQ != null)
            {
                DsSupplierAccoMappingExportDataReport = MapSvc.AccomodationMappingReport(dC_SupplerVSupplier_Report_RQ);
            }

            ReportDataSource rds = new ReportDataSource("DsAccommodationMappingSupplierVSupplier", DsSupplierAccoMappingExportDataReport);
            // ReportViewer ReportViewerActivityProductDetails = new ReportViewer();
            ReportViewerAccommodationMappingSupplierVSupplier.Visible = true;
            ReportViewerAccommodationMappingSupplierVSupplier.LocalReport.DataSources.Clear();
            ReportViewerAccommodationMappingSupplierVSupplier.LocalReport.ReportPath = Server.MapPath("~/staticdata/hotels/AccommodationMappingSupplierVSupplier.rdlc");
            ReportViewerAccommodationMappingSupplierVSupplier.LocalReport.DataSources.Add(rds);
            ReportViewerAccommodationMappingSupplierVSupplier.Visible = true;
            ReportViewerAccommodationMappingSupplierVSupplier.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            ReportViewerAccommodationMappingSupplierVSupplier.DataBind();
            ReportViewerAccommodationMappingSupplierVSupplier.LocalReport.Refresh();
        }

        private void fillSupplierList(DropDownList ddl)
        {

            MDMSVC.DC_Supplier_Search_RQ RQ = new MDMSVC.DC_Supplier_Search_RQ();
            RQ.EntityType = "Accommodation";
            RQ.StatusCode = "ACTIVE";
            ddl.DataSource = _objMasterSVC.GetSupplierByEntity(RQ);
            ddl.DataValueField = "Supplier_Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();

        }


        private void fillSupplierListToListBox(ListBox ddl)
        {


            MDMSVC.DC_Supplier_Search_RQ RQ = new MDMSVC.DC_Supplier_Search_RQ();
            List<MDMSVC.DC_Supplier_DDL> listSupplier = new List<MDMSVC.DC_Supplier_DDL>();


            RQ.EntityType = "Accommodation";
            RQ.StatusCode = "ACTIVE";
            listSupplier = _objMasterSVC.GetSupplierByEntity(RQ);

            var itemToRemove = listSupplier.Single(r => r.Supplier_Id == new Guid(ddlSupplierName.SelectedValue));
            listSupplier.Remove(itemToRemove);
            //ddl.DataSource = _objMasterSVC.GetSupplierByEntity(RQ);
            ddl.Items.Clear();
            ddl.DataSource = listSupplier;

            ddl.DataValueField = "Supplier_Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();

        }

        private void LoadMasters()
        {
            fillSupplierList(ddlSupplierName);
            //fillSupplierListToListBox(ddlCompareSupplier1);
            //fillSupplierList(ddlCompareSupplier);

        }

        protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSupplierName.SelectedIndex != 0 )
            { 
            fillSupplierListToListBox(ddlCompareSupplier1);
            }
            else
            {
                ddlCompareSupplier1.Items.Clear();
            }
        }


        private List<Guid> GetSelectedList(ListBox lst)
        {
            List<Guid> strList = new List<Guid>();
            if (lst.Items.Count > 0)
            {
                foreach (ListItem item in lst.Items)
                {
                    if (item.Selected)
                    {
                        strList.Add(new Guid(item.Value));
                    }
                }
                return strList;
            }
            else
            {
                return strList;
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {

            if (ddlSupplierName.SelectedValue == "0" || ddlCompareSupplier1.SelectedIndex == -1)
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "Please select valid Supplier & Compare Supplier before view report.", BootstrapAlertType.Danger);
                return;
            }


            List<Guid> selectedSupplier = GetSelectedList(ddlCompareSupplier1);

            TLGX_Consumer.MDMSVC.DC_SupplerVSupplier_Report_RQ dC_SupplerVSupplier_Report_RQ1 = new MDMSVC.DC_SupplerVSupplier_Report_RQ();

            if (ddlSupplierName.SelectedIndex != 0)
            {
                Guid SourSupplierName = new Guid(ddlSupplierName.SelectedValue);

                dC_SupplerVSupplier_Report_RQ1.Accommodation_Source_Id = SourSupplierName;

                //dC_SupplerVSupplier_Report_RQ1.Compare_WithSupplier_Ids = new Guid[] { new Guid("773498FA-4F94-41AA-B5C2-EC28C8B8698D"), new Guid("DBD23ED5-A2AA-4A59-9265-6B198D6C8BFD") };
                dC_SupplerVSupplier_Report_RQ1.Compare_WithSupplier_Ids = selectedSupplier.ToArray();

                
                GetAccommodationSupplierVSuplierReport(dC_SupplerVSupplier_Report_RQ1);
            }
        }

    }
}