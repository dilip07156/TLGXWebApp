using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.staticdata
{
    public partial class HotelMappingCityWiseReport : System.Web.UI.Page
    {
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();

        Controller.MappingSVCs mappingSVC = new Controller.MappingSVCs();

        List<string> Cities = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadDropdown();
            }
        }
        private void LoadDropdown()
        {
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
        
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            var SelectedRegions = GetSelectedList(ddlRegion);
            var Region = ddlRegion.Items.Count == SelectedRegions.Count ? new List<string> { } : SelectedRegions;
            var SelectedCountries = GetSelectedList(ddlCountry);
            var Country = ddlCountry.Items.Count == SelectedCountries.Count ? new List<string> { } : SelectedCountries;
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
            
            var report = new MDMSVC.DC_NewDashBoardReport_RQ();
            report.Country = Country.ToArray();
            report.Region = Region.ToArray();
            report.City = City.ToArray();
            // Bind data to Report and Show report 
            var reportResponse = mappingSVC.GetHotelMappingReport_CityWise(report);
                ReportDataSource rds = new ReportDataSource("DataSet1", reportResponse);
                CityReportViewer.LocalReport.DataSources.Clear();
                CityReportViewer.LocalReport.ReportPath = "staticdata/HotelMappingCityReport.rdlc";
                CityReportViewer.LocalReport.DataSources.Add(rds);
                CityReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                CityReportViewer.DataBind();
                CityReportViewer.LocalReport.Refresh();
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


        //protected void getData(List<MDMSVC.DC_NewDashBoardReportCountry_RS> response)
        //{
        //     HotelMappingCityreport.Visible = true;
        //    List<MDMSVC.DC_NewDashBoardReportCountry_RS> DsReport = new List<MDMSVC.DC_NewDashBoardReportCountry_RS>();
        //    DsReport = response;
        //    ReportDataSource rds = new ReportDataSource("DsReport", DsReport);
        //    CityReportViewer.LocalReport.DataSources.Clear();
        //    CityReportViewer.LocalReport.ReportPath = "staticdata/HotelMappingCityReport.rdlc";
        //    CityReportViewer.LocalReport.DataSources.Add(rds);
        //    CityReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
        //    CityReportViewer.DataBind();
        //    CityReportViewer.LocalReport.Refresh();
        //}

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

                    List<CityReportSelectedCity> itl = new List<CityReportSelectedCity>();
                    foreach (RepeaterItem item in repSelectedCity.Items)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            LinkButton btnRemoveCity = (LinkButton)item.FindControl("btnRemoveCity");
                            Label lblCityName = (Label)item.FindControl("lblCityName");
                            Label lblCityCode = (Label)item.FindControl("lblCityCode");
                            Label lblCityId = (Label)item.FindControl("lblCityId");

                            itl.Add(new CityReportSelectedCity
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
                        itl.Add(new CityReportSelectedCity
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
                List<CityReportSelectedCity> ptl = new List<CityReportSelectedCity>();
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

                        ptl.Add(new CityReportSelectedCity
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

    public class CityReportSelectedCity
    {
        public string City_Id { get; set; }
        public string City_Code { get; set; }
        public string CityName { get; set; }
    }
}