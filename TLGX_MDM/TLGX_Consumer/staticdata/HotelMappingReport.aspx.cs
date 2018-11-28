using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.staticdata
{
    public partial class HotelMappingReport : System.Web.UI.Page
    {
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();

        Controller.MappingSVCs mappingSVC = new Controller.MappingSVCs();

        List<string> Cities = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdown();
                RptHotelMapping.Visible = false;
            }
        }

        private void LoadDropdown()
        {
            fillRegionList();
            fillPriorityList();
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

        //GAURAV_TMAP_876
        private void fillPriorityList()
        {
            ddlPriorities.Items.Clear();
            ddlPriorities.DataSource = masterSVc.GetPrioritiesOfAccommodationMaster();
            ddlPriorities.DataValueField = "Value";
            ddlPriorities.DataTextField = "Name";
            ddlPriorities.DataBind();
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
            //var SelectedCountries = GetSelectedList(ddlCountry);
            //var AllCities = masterSVc.GetAllCountrywiseCitiesList(SelectedCountries);
            //Cities.Add(AllCities.Select(x => x.City_Id).ToString());
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

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            var selectedRegions = GetSelectedList(ddlRegion);
            var Region = ddlRegion.Items.Count == selectedRegions.Count ? new List<string> { } : selectedRegions;

            var selectedCountries = GetSelectedList(ddlCountry);
            var Country = ddlCountry.Items.Count == selectedCountries.Count ? new List<string>{ } : selectedCountries;

            //GAURAV_TMAP_874
            var selectedPriorities = GetSelectedList(ddlPriorities);
            var Priority = selectedPriorities.Count == 0 ? new List<string> { } : selectedPriorities;

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
            //var AccoPriority = GetSelectedList(ddlAccoPriority);
            
           
            var report = new DC_EzeegoHotelVsSupplierHotelMappingReport_RQ();
            report.Country = Country.ToArray();
            report.Region = Region.ToArray();
            report.AccoPriority = Priority.ToArray();
            report.City = City.ToArray();
            //report.AccoPriority = AccoPriority.ToArray();
            var reportResponse = mappingSVC.HotelMappingReport(report);
            getHotelReportData(reportResponse);
        }

        protected void getHotelReportData(List<DC_HotelMappingReport_RS> response)
        {
            HotelMappingreport.Visible = true;
            List<MDMSVC.DC_HotelMappingReport_RS> DsHotelReport = new List<MDMSVC.DC_HotelMappingReport_RS>();
            RptHotelMapping.Visible = true;
            DsHotelReport = response;

            ReportDataSource rds = new ReportDataSource("DsHotelReport", DsHotelReport);
            RptHotelMapping.LocalReport.DataSources.Clear();
            RptHotelMapping.LocalReport.ReportPath = Server.MapPath("~/staticdata/HotelMappingRDLCReport.rdlc");
            RptHotelMapping.LocalReport.DataSources.Add(rds);
            RptHotelMapping.Visible = true;
            RptHotelMapping.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            RptHotelMapping.DataBind();
            RptHotelMapping.LocalReport.Refresh();
        }
    }

    
}