using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;
using Microsoft.Reporting.WebForms;

namespace TLGX_Consumer.staticdata
{
    public partial class EzeegoHotelvsSupplierHotelReport : System.Web.UI.Page
    {
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();

        Controller.MappingSVCs mappingSVC = new Controller.MappingSVCs();

        List<string> Cities = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdown();
                RptEzeegoHotelvsSupplierHotel.Visible = false;
            }
        }

        private void LoadDropdown()
        {
            fillsuppliers();
            fillAccomodationPriority(ddlAccoPriority);
            fillRegionList();
        }

        private void fillRegionList()
        {
            ddlRegion.Items.Clear();
            //var result = masterSVc.GetAllRegions();
            ddlRegion.DataSource = masterSVc.GetAllRegions();
            ddlRegion.DataValueField = "RegionCode";
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataBind();
        }

        private void fillsuppliers()
        {

            ddlSupplierName.DataSource = masterSVc.GetSupplierByEntity(new MDMSVC.DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue, EntityType = "Accommodation" });//, CategorySubType_ID = "Apartments"
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        private void fillAccomodationPriority(ListBox ddl)
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
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            var Region = GetSelectedList(ddlRegion);
            var Country = GetSelectedList(ddlCountry);

            var City = new List<string> { };
            if (rdoIsAllCities.Checked)
            {
                City = Cities;
            }


            if (rdoIsSelectiveCities.Checked)
            {
                foreach (RepeaterItem item in repSelectedCity.Items)
                {
                    Label lblCityId = (Label)item.FindControl("lblCityId");
                    Cities.Add(lblCityId.Text);
                }

                City = Cities;
            }
            var AccoPriority = GetSelectedList(ddlAccoPriority);
            var Supplier = GetSelectedList(ddlSupplierName);
            var selectedHotelId = txtHotelNameOrHDL.Text;

            var report = new DC_EzeegoHotelVsSupplierHotelMappingReport_RQ();
            report.Country = Country.ToArray();
            report.Region = Region.ToArray();

            report.City = City.ToArray();
            report.AccoPriority = AccoPriority.ToArray();
            report.Supplier = Supplier.ToArray();
            report.selectedHotelId = selectedHotelId;

            var reportResponse = mappingSVC.EzeegoHotelVsSupplierHotelMappingReport(report);
            getData(reportResponse);
            //reportList.Add(Country);
        }

        private List<string> GetSelectedList(ListBox lst)
        {
            List<string> strList = new List<string>();
            if (lst.Items.Count > 0)
            {
                foreach (ListItem item in lst.Items)
                {
                    if (item.Selected)
                    {
                        strList.Add(item.Value);
                    }
                }
                return strList;
            }
            else
            {
                return strList;
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var RegionCode = GetSelectedList(ddlRegion);
            GetRegionwiseCountries(RegionCode);
        }

        protected void GetRegionwiseCountries(List<string> RegionCode)
        {
            var result = masterSVc.GetRegionwiseCountriesList(RegionCode);
            if (result.Count > 0)
            {
                ddlCountry.DataSource = result;
                ddlCountry.DataValueField = "Country_Id";
                ddlCountry.DataTextField = "Country_Name";
                ddlCountry.DataBind();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var CountriesList = GetSelectedList(ddlCountry);

        }


        protected void getData(List<DC_EzeegoHotelVsSupplierHotelMappingReport> response)
        {
            // HotelMappingreport.Visible = true;
            List<MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport> DsReport = new List<MDMSVC.DC_EzeegoHotelVsSupplierHotelMappingReport>();
            RptEzeegoHotelvsSupplierHotel.Visible = true;
            DsReport = response;
            
            ReportDataSource rds = new ReportDataSource("DsReport", DsReport);
            //ReportViewer RptEzeegoHotelvsSupplierHotel = new ReportViewer();
            RptEzeegoHotelvsSupplierHotel.LocalReport.DataSources.Clear();
            RptEzeegoHotelvsSupplierHotel.LocalReport.ReportPath = Server.MapPath("~/staticdata/EzeegoHotelvsSupplierHotelRDLCReport.rdlc");
            RptEzeegoHotelvsSupplierHotel.LocalReport.DataSources.Add(rds);
            RptEzeegoHotelvsSupplierHotel.Visible = true;
            RptEzeegoHotelvsSupplierHotel.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            RptEzeegoHotelvsSupplierHotel.DataBind();
            RptEzeegoHotelvsSupplierHotel.LocalReport.Refresh();
        }

        protected void rdoIsSelectiveCities_CheckedChanged(object sender, EventArgs e)
        {
            txtCityLookup.ReadOnly = false;
            Cities.Clear();
        }


        private List<string> GetAllOptionsList(ListBox lst)
        {
            List<string> strList = new List<string>();
            if (lst.Items.Count > 0)
            {
                foreach (ListItem item in lst.Items)
                {
                    strList.Add(item.Value);
                }
                return strList;
            }
            else
            {
                return strList;
            }
        }

        protected void rdoIsAllCities_CheckedChanged(object sender, EventArgs e)
        {
            txtCityLookup.Text = "";
            txtCityLookup.ReadOnly = true;
            Cities.Clear();
            repSelectedCity.DataSource = null;
            repSelectedCity.DataBind();
            var SelectedCountries = GetSelectedList(ddlCountry);
            var AllCities = masterSVc.GetAllCountrywiseCitiesList(SelectedCountries);
            Cities.Add(AllCities.Select(x => x.City_Id).ToString());
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //string CityData = txtCityLookup.Text;
            if (!string.IsNullOrEmpty(txtCityLookup.Text))
            {
                string[] _citydata = txtCityLookup.Text.Split(',');
                txtCityLookup.Text = "";
                var CityName = _citydata[0].Trim();
                var CountryName = _citydata[1].Trim();
                var citydetails = masterSVc.GetCitiesDetails(CountryName, CityName);
                if (citydetails.Count > 0)
                {
                    #region Add Data in list
                    bool bDuplicate = false;

                    List<SelectedCity> itl = new List<SelectedCity>();
                    foreach (RepeaterItem item in repSelectedCity.Items)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            LinkButton btnRemoveCity = (LinkButton)item.FindControl("btnRemoveCity");
                            Label lblCityName = (Label)item.FindControl("lblCityName");
                            Label lblCityCode = (Label)item.FindControl("lblCityCode");
                            Label lblCityId = (Label)item.FindControl("lblCityId");

                            itl.Add(new SelectedCity
                            {
                                CityName = lblCityName.Text,
                                City_Id = lblCityId.Text,
                                City_Code = lblCityCode.Text,
                            });
                            if (citydetails[0].City_Id.ToString().ToLower() == btnRemoveCity.CommandArgument.ToLower())
                            {
                                bDuplicate = true;
                            };
                        }

                    }

                    if (!bDuplicate)
                    {
                        itl.Add(new SelectedCity
                        {
                           CityName = citydetails[0].City_Name,
                           City_Code = citydetails[0].City_Code,
                           City_Id = citydetails[0].City_Id.ToString()
                        });
                    }
                    repSelectedCity.DataSource = itl;
                    repSelectedCity.DataBind();
                    #endregion
                }
            }
        }        

        protected void repSelectedCity_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveCity")
            {
                List<SelectedCity> ptl = new List<SelectedCity>();
                foreach (RepeaterItem item in repSelectedCity.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveCity = (LinkButton)item.FindControl("btnRemoveCity");
                        if (btnRemoveCity.CommandArgument.ToLower() == e.CommandArgument.ToString().ToLower())
                        {
                            continue;
                        }

                        Label lblCityName = (Label)item.FindControl("lblCityName");
                        Label lblCityCode = (Label)item.FindControl("lblCityCode");
                        Label lblCityId = (Label)item.FindControl("lblCityId");

                        ptl.Add(new SelectedCity
                        {
                            CityName = lblCityName.Text,
                            City_Id = lblCityId.Text,
                            City_Code = lblCityCode.Text,
                        });
                    }
                }

                repSelectedCity.DataSource = ptl;
                repSelectedCity.DataBind();
            }
        }
       
    }

    public class SelectedCity
    {
        public string City_Id { get; set; }
        public string City_Code { get; set; }
        public string CityName { get; set; }
    }
}