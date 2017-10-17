using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.hotel
{
    public partial class searchHotelascx : System.Web.UI.UserControl
    {
        MasterDataDAL masterdata = new MasterDataDAL();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MasterDataSVCs _objMasterData = new MasterDataSVCs();
        public static string AttributeOptionFor = "HotelInfo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillcountrydropdown("search");
                fillstatusdropdown();
                fillproductcaterogy();
                fillStarRating();
                dvPageSize.Visible = false;
                //dvGrid.Visible = false;
                grdSearchResults.Visible = false;
            }
        }

        private void fillStarRating()
        {
            try
            {
                ddlStarRating.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("hotelinfo", "Stars").MasterAttributeValues;
                ddlStarRating.DataTextField = "AttributeValue";
                ddlStarRating.DataValueField = "MasterAttributeValue_Id";
                ddlStarRating.DataBind();
                ddlStarRating.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void fillstatusdropdown()
        {
            //ddlStatus.DataSource = masterdata.getAllStatuses();
            ddlStatus.DataSource = _objMasterData.GetAllStatuses();
            ddlStatus.DataTextField = "Status_Name";
            ddlStatus.DataValueField = "Status_Short";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText("ACTIVE"));
        }

        private void fillcountrydropdown(string source)
        {
            //var resSet = masterdata.GetMasterCountryData("");
            var result = _objMasterData.GetAllCountries();
            if (source == "search")
            {
                ddlCountry.DataSource = result;
                ddlCountry.DataValueField = "Country_Id";
                ddlCountry.DataTextField = "Country_Name";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("---ALL---", ""));
            }
        }

        private void fillcitydropdown(string source, string Country_ID)
        {
            if (source == "search")
            {
                if (Country_ID != "")
                {
                    // var resSet = masterdata.GetMasterCityData(new Guid(Country_ID));
                    var result = _objMasterData.GetMasterCityData(Country_ID);
                    ddlCity.DataSource = result;
                    ddlCity.DataValueField = "City_Id";
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataBind();
                }
                else
                {
                    ddlCity.Items.Clear();
                }
                ddlCity.Items.Insert(0, new ListItem("---ALL---", ""));
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "")
            {
                fillcitydropdown("search", ddlCountry.SelectedValue);
            }
            else
            {
                fillcitydropdown("search", "");
            }
            ddlCity.Focus();
        }

        public void CallHotelSearch(int pageIndex, int pageSize)
        {
            RQParams.ProductCategory = "Accommodation";
            RQParams.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text; //txtProdCategoryt.Text;

            if (ddlStatus.SelectedItem.Value != "0")
            {
                if (ddlStatus.SelectedItem.Text == "ACTIVE")
                    RQParams.Status = "ACTIVE";
                else
                    RQParams.Status = "INACTIVE";
            }
            //else
            //    RQParams.Status = true;
            //if (ddlStatus.SelectedItem.Text == "ACTIVE" || ddlStatus.SelectedItem.Value == "0")
            //  RQParams.Status = ddlStatus.SelectedItem.Text == "ACTIVE" ? true : false;

            if (txtHotelName.Text.Length != 0)
                RQParams.HotelName = txtHotelName.Text.TrimStart().TrimEnd();


            if (txtCommon.Text.Length != 0)
            {
                int CompanyHotelId = 0;
                int.TryParse(txtCommon.Text, out CompanyHotelId);

                if (CompanyHotelId != 0)
                    RQParams.CompanyHotelId = Convert.ToInt32(txtCommon.Text);
            }

            if (ddlCountry.SelectedItem.Text != "---ALL---")
                RQParams.Country = ddlCountry.SelectedItem.Text;
            if (ddlCity.SelectedItem.Text != "---ALL---")
                RQParams.City = ddlCity.SelectedItem.Text;
            RQParams.Searchfrom = "hotalsearch";

            if (ddlStarRating.SelectedValue != "0")
                RQParams.Starrating = ddlStarRating.SelectedItem.Text;
            RQParams.PageNo = pageIndex;
            RQParams.PageSize = pageSize;
        }

        public void fillHotelSearchGrid(int pageIndex, int pageSize)
        {
            CallHotelSearch(pageIndex, pageSize);
            var res = AccSvc.SearchHotels(RQParams);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdSearchResults.VirtualItemCount = res[0].TotalRecords;
                    lblTotalRecords.Text = res[0].TotalRecords.ToString();
                }

                grdSearchResults.DataSource = res;
                grdSearchResults.PageIndex = pageIndex;
                grdSearchResults.PageSize = pageSize;
                grdSearchResults.DataBind();
            }
            else
            {
                grdSearchResults.DataSource = null;
                grdSearchResults.DataBind();
                lblTotalRecords.Text = String.Empty;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillHotelSearchGrid(0, Convert.ToInt32(ddlPageSize.SelectedItem.Value));
            dvPageSize.Visible = true;
            //dvGrid.Visible = true;
            grdSearchResults.Visible = true;
        }

        protected void grdSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillHotelSearchGrid(e.NewPageIndex, Convert.ToInt32(ddlPageSize.SelectedItem.Value));
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillHotelSearchGrid(0, Convert.ToInt32(ddlPageSize.SelectedItem.Value));
        }

        private void fillproductcaterogy()
        {
            //fillAttributeValues("ddlAddProductCategorySubType", "ProductCategorySubType");
            ddlProductCategorySubType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "ProductCategorySubType").MasterAttributeValues;
            ddlProductCategorySubType.DataTextField = "AttributeValue";
            ddlProductCategorySubType.DataValueField = "MasterAttributeValue_Id";
            ddlProductCategorySubType.DataBind();
            ddlProductCategorySubType.SelectedIndex = ddlProductCategorySubType.Items.IndexOf(ddlProductCategorySubType.Items.FindByText("Hotel"));
        }

        private void fillAttributeValues(string Contol_Name, string Attribute_Name)
        {

            DropDownList ddl = (DropDownList)Page.FindControl(Contol_Name);
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, Attribute_Name).MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlProductCategorySubType.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            grdSearchResults.DataSource = null;
            grdSearchResults.DataBind();
            dvPageSize.Visible = false;
            //dvGrid.Visible = false;
            grdSearchResults.Visible = false;
            lblTotalRecords.Text = String.Empty;
            ddlStarRating.SelectedIndex = 0;
        }

        protected void grdSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}