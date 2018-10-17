using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;
using System.Globalization;
using TLGX_Consumer.App_Code;
using System.Web.UI.HtmlControls;

namespace TLGX_Consumer.controls.hotel
{
    public partial class overview : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MasterDataDAL MasterData = new MasterDataDAL();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        public static string AttributeOptionFor = "HotelInfo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                GetHotelDetails();
                fillmasterdata();
            }
        }

        private void fillmasterdata()
        {
            fillcoutries();
            fillsubburbs();
            fillstates();
            fillcities();
            fillarea();
            filllocation();

            fillcompanyrating();
            fillstarratings();

            fillchain();
            fillbrands();
            fillaffiliation();
            fillproductcaterogy();
            selectattributevalues();
        }

        private void selectattributevalues()
        {
            MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmHotelOverview.DataItem;
            DropDownList ddlSuburbs = (DropDownList)frmHotelOverview.FindControl("ddlSuburbs");
            DropDownList ddlChain = (DropDownList)frmHotelOverview.FindControl("ddlChain");

            DropDownList ddlStarRating = (DropDownList)frmHotelOverview.FindControl("ddlStarRating");
            DropDownList ddlCompanyRating = (DropDownList)frmHotelOverview.FindControl("ddlCompanyRating");
            DropDownList ddlBrand = (DropDownList)frmHotelOverview.FindControl("ddlBrand");
            DropDownList ddlAffiliation = (DropDownList)frmHotelOverview.FindControl("ddlAffiliation");
            DropDownList ddlProductCategorySubType = (DropDownList)frmHotelOverview.FindControl("ddlProductCategorySubType");

            if (rowView.HotelRating != null)
            {
                ddlStarRating.SelectedIndex = ddlStarRating.Items.IndexOf(ddlStarRating.Items.FindByText(rowView.HotelRating.ToString()));
            }
            if (rowView.CompanyRating != null)
            {
                ddlCompanyRating.SelectedIndex = ddlCompanyRating.Items.IndexOf(ddlCompanyRating.Items.FindByText(rowView.CompanyRating.ToString()));
            }
            if (rowView.SuburbDowntown != null)
            {
                ddlSuburbs.SelectedIndex = ddlSuburbs.Items.IndexOf(ddlSuburbs.Items.FindByText(rowView.SuburbDowntown.ToString()));
            }
            if (rowView.Chain != null)
            {
                ddlChain.SelectedIndex = ddlChain.Items.IndexOf(ddlChain.Items.FindByText(rowView.Chain.ToString()));
            }
            if (rowView.Brand != null)
            {
                ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(ddlBrand.Items.FindByText(rowView.Brand.ToString()));
            }
            if (rowView.Affiliation != null)
            {
                ddlAffiliation.SelectedIndex = ddlAffiliation.Items.IndexOf(ddlAffiliation.Items.FindByText(rowView.Affiliation.ToString()));
            }
            if (rowView.ProductCategorySubType != null)
            {
                ddlProductCategorySubType.SelectedIndex = ddlProductCategorySubType.Items.IndexOf(ddlProductCategorySubType.Items.FindByText(rowView.ProductCategorySubType.ToString()));
            }
        }

        private void fillAttributeValues(string Contol_Name, string Attribute_Name)
        {
            DropDownList ddl = (DropDownList)frmHotelOverview.FindControl(Contol_Name);
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, Attribute_Name).MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }

        private void fillproductcaterogy()
        {
            fillAttributeValues("ddlProductCategorySubType", "ProductCategorySubType");
        }

        private void fillaffiliation()
        {
            fillAttributeValues("ddlAffiliation", "Affiliations");
        }

        private void fillbrands()
        {
            fillAttributeValues("ddlBrand", "Brand");
        }

        private void fillchain()
        {
            fillAttributeValues("ddlChain", "Chain");
        }

        private void fillstarratings()
        {
            fillAttributeValues("ddlStarRating", "Stars");
        }

        private void fillcompanyrating()
        {
            fillAttributeValues("ddlCompanyRating", "CompanyRating");
        }

        private void fillsubburbs()
        {
            fillAttributeValues("ddlSuburbs", "SuburbsDowntown");
        }

        private void filllocation()
        {
            DropDownList ddlLocation = (DropDownList)frmHotelOverview.FindControl("ddlLocation");
            DropDownList ddlArea = (DropDownList)frmHotelOverview.FindControl("ddlArea");
            if (ddlArea.SelectedItem.Text != "-Select-")
            {
                ddlLocation.Items.Clear();
                //  ddlLocation.DataSource = MasterData.GetMasterCityAreaLocationData(new Guid(ddlArea.SelectedItem.Value));
                ddlLocation.DataSource = masterSVc.GetMasterCityAreaLocationData(ddlArea.SelectedValue);

                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "CityAreaLocation_Id";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmHotelOverview.DataItem;
                if (ddlLocation.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    if (rowView != null)
                    {
                        if (!string.IsNullOrEmpty(rowView.Location))
                        {
                            ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(rowView.Location.ToString()));
                        }
                    }
                }
            }
        }

        private void fillarea()
        {
            DropDownList ddlArea = (DropDownList)frmHotelOverview.FindControl("ddlArea");
            DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");

            if (ddlCity.SelectedItem.Text != "-Select-")
            {
                ddlArea.Items.Clear();
                //ddlArea.DataSource = MasterData.GetMasterCityAreaData(new Guid(ddlCity.SelectedItem.Value));
                ddlArea.DataSource = masterSVc.GetMasterCityAreaData(ddlCity.SelectedValue);
                ddlArea.DataTextField = "Name";
                ddlArea.DataValueField = "CityArea_Id";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmHotelOverview.DataItem;
                if (ddlArea.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    if (rowView != null)
                    {
                        if (!string.IsNullOrEmpty(rowView.Area))
                        {
                            ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByText(rowView.Area.ToString()));
                        }
                    }
                }
            }
        }

        private void fillstates()
        {
            DropDownList ddlState = (DropDownList)frmHotelOverview.FindControl("ddlState");
            DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
            if (ddlCountry.SelectedItem.Text != "-Select-")
            {
                ddlState.Items.Clear();
                //ddlState.DataSource = MasterData.GetMasterStateData(new Guid(ddlCountry.SelectedItem.Value));
                ddlState.DataSource = masterSVc.GetMasterStateData(ddlCountry.SelectedValue);
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateCode";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmHotelOverview.DataItem;
                if (rowView != null)
                {
                    if (ddlState.Items.Count > 1 && rowView.State_Name != null) // needs to be 1 to handle the "Select" value
                    {
                        ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByText(rowView.State_Name.ToString()));
                    }
                }
            }
        }

        private void fillcities()
        {
            DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
            DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
            //ddlCity.DataSource = MasterData.GetMasterCityDetail(MasterData.GetIDByName("Country", ddlCountry.SelectedItem.Text));
            if (ddlCountry.SelectedItem.Text != "-Select-")
            {
                ddlCity.Items.Clear();
                //ddlCity.DataSource = MasterData.GetMasterCityData(new Guid(ddlCountry.SelectedItem.Value));
                ddlCity.DataSource = masterSVc.GetMasterCityData(ddlCountry.SelectedValue);
                ddlCity.DataTextField = "Name";
                ddlCity.DataValueField = "City_Id";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmHotelOverview.DataItem;
                if (rowView != null)
                {
                    ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(rowView.City.ToString()));
                }
            }
        }

        private void fillcoutries()
        {
            DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
            //ddlCountry.DataSource = MasterData.GetMasterCountryData("");
            ddlCountry.DataSource = masterSVc.GetAllCountries();
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmHotelOverview.DataItem;
            if (rowView != null)
            {
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(rowView.Country.ToString()));
            }
        }

        private void GetHotelDetails()
        {
            frmHotelOverview.DataSource = AccSvc.GetHotelDetails(Accomodation_ID);
            frmHotelOverview.DataBind();
        }

        protected void frmHotelOverview_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveProduct")
            {
                if (CheckForDuplicate())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
                else
                {
                    CheckBox blnRTCompleted = (CheckBox)frmHotelOverview.FindControl("blnRTCompleted");
                CheckBox blnMysteryProduct = (CheckBox)frmHotelOverview.FindControl("blnMysteryProduct");
                DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
                DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
                DropDownList ddlProductCategorySubType = (DropDownList)frmHotelOverview.FindControl("ddlProductCategorySubType");
                DropDownList ddlAffiliation = (DropDownList)frmHotelOverview.FindControl("ddlAffiliation");
                DropDownList ddlLocation = (DropDownList)frmHotelOverview.FindControl("ddlLocation");
                DropDownList ddlArea = (DropDownList)frmHotelOverview.FindControl("ddlArea");
                DropDownList ddlState = (DropDownList)frmHotelOverview.FindControl("ddlState");
                DropDownList ddlSuburbs = (DropDownList)frmHotelOverview.FindControl("ddlSuburbs");
                DropDownList ddlChain = (DropDownList)frmHotelOverview.FindControl("ddlChain");

                DropDownList ddlStarRating = (DropDownList)frmHotelOverview.FindControl("ddlStarRating");
                DropDownList ddlCompanyRating = (DropDownList)frmHotelOverview.FindControl("ddlCompanyRating");
                DropDownList ddlBrand = (DropDownList)frmHotelOverview.FindControl("ddlBrand");
                TextBox txtHotelName = (TextBox)frmHotelOverview.FindControl("txtHotelName");
                TextBox txtCommonHotelId = (TextBox)frmHotelOverview.FindControl("txtCommonHotelId");
                TextBox txtPostalCode = (TextBox)frmHotelOverview.FindControl("txtPostalCode");
                TextBox txtHotelID = (TextBox)frmHotelOverview.FindControl("txtHotelID");
                TextBox txtAwardsReceived = (TextBox)frmHotelOverview.FindControl("txtAwardsReceived");
                TextBox txtCheckinTime = (TextBox)frmHotelOverview.FindControl("txtCheckinTime");
                TextBox txtCheckOut = (TextBox)frmHotelOverview.FindControl("txtCheckOut");
                TextBox txtCompanyHotelID = (TextBox)frmHotelOverview.FindControl("txtCompanyHotelID");
                TextBox txtCompanyName = (TextBox)frmHotelOverview.FindControl("txtCompanyName");
                CheckBox chkCompanyRecommended = (CheckBox)frmHotelOverview.FindControl("chkCompanyRecommended");
                TextBox txtDisplayName = (TextBox)frmHotelOverview.FindControl("txtDisplayName");
                TextBox txtFinanceControlId = (TextBox)frmHotelOverview.FindControl("txtFinanceControlId");

                TextBox txtInternalRemarks = (TextBox)frmHotelOverview.FindControl("txtInternalRemarks");
                TextBox txtHotelLat = (TextBox)frmHotelOverview.FindControl("txtHotelLat");
                TextBox txtHotelLon = (TextBox)frmHotelOverview.FindControl("txtHotelLon");
                TextBox txtRatingDate = (TextBox)frmHotelOverview.FindControl("txtRatingDate");
                TextBox txtStreet = (TextBox)frmHotelOverview.FindControl("txtStreet");
                TextBox txtStreet2 = (TextBox)frmHotelOverview.FindControl("txtStreet2");
                TextBox txtStreet3 = (TextBox)frmHotelOverview.FindControl("txtStreet3");
                TextBox txtStreet4 = (TextBox)frmHotelOverview.FindControl("txtStreet4");
                TextBox txtStreet5 = (TextBox)frmHotelOverview.FindControl("txtStreet5");
                TextBox txtTotalFloor = (TextBox)frmHotelOverview.FindControl("txtTotalFloor");
                TextBox txtTotalRooms = (TextBox)frmHotelOverview.FindControl("txtTotalRooms");
                TextBox txtYearBuilt = (TextBox)frmHotelOverview.FindControl("txtYearBuilt");

                MDMSVC.DC_Accomodation OverviewData = new MDMSVC.DC_Accomodation();

                OverviewData.Accommodation_Id = new Guid(txtHotelID.Text);
                OverviewData.ProductCategory = "Accommodation";
                if (ddlProductCategorySubType.SelectedIndex != 0)
                    OverviewData.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text;
                if (ddlAffiliation.SelectedIndex != 0)
                    OverviewData.Affiliation = ddlAffiliation.SelectedItem.Text;
                if (ddlArea.SelectedIndex != 0)
                    OverviewData.Area = ddlArea.SelectedItem.Text;
                if (ddlBrand.SelectedIndex != 0)
                    OverviewData.Brand = ddlBrand.SelectedItem.Text;
                if (ddlChain.SelectedIndex != 0)
                    OverviewData.Chain = ddlChain.SelectedItem.Text;
                if (ddlCountry.SelectedIndex != 0)
                    OverviewData.Country = ddlCountry.SelectedItem.Text;
                if (ddlCity.SelectedIndex != 0)
                    OverviewData.City = ddlCity.SelectedItem.Text;
                if (ddlState.SelectedIndex != 0)
                    OverviewData.State_Name = ddlState.SelectedItem.Text;
                if (ddlCompanyRating.SelectedIndex != 0)
                    OverviewData.CompanyRating = ddlCompanyRating.SelectedItem.Text;
                if (ddlStarRating.SelectedIndex != 0)
                    OverviewData.HotelRating = ddlStarRating.SelectedItem.Text;

                if (ddlSuburbs.SelectedIndex != 0)
                    OverviewData.SuburbDowntown = ddlSuburbs.SelectedItem.Text;
                if (ddlLocation.SelectedIndex != 0)
                    OverviewData.Location = ddlLocation.SelectedItem.Text;

                if (!string.IsNullOrEmpty(txtAwardsReceived.Text))
                    OverviewData.AwardsReceived = txtAwardsReceived.Text.ToString();
                if (!string.IsNullOrEmpty(txtCheckinTime.Text))
                    OverviewData.CheckInTime = txtCheckinTime.Text.ToString();
                if (!string.IsNullOrEmpty(txtCheckOut.Text))
                    OverviewData.CheckOutTime = txtCheckOut.Text.ToString();
                if (!string.IsNullOrEmpty(txtDisplayName.Text))
                    OverviewData.DisplayName = txtDisplayName.Text.ToString();

                if (!string.IsNullOrEmpty(txtHotelName.Text))
                    OverviewData.HotelName = txtHotelName.Text.ToString();
                if (!string.IsNullOrEmpty(txtInternalRemarks.Text))
                    OverviewData.InternalRemarks = txtInternalRemarks.Text.ToString();
                if (!string.IsNullOrEmpty(txtHotelLat.Text))
                    OverviewData.Latitude = txtHotelLat.Text.ToString();
                if (!string.IsNullOrEmpty(txtHotelLon.Text))
                    OverviewData.Longitude = txtHotelLon.Text.ToString();
                if (!string.IsNullOrEmpty(txtPostalCode.Text))
                    OverviewData.PostalCode = txtPostalCode.Text.ToString();
                //if (!string.IsNullOrEmpty(txtRatingDate.Text))
                //   OverviewData.RatingDate = DateTime.ParseExact(txtRatingDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //else
                OverviewData.RatingDate = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(txtStreet.Text))
                    OverviewData.StreetName = txtStreet.Text.ToString();
                if (!string.IsNullOrEmpty(txtStreet2.Text))
                    OverviewData.StreetNumber = txtStreet2.Text.ToString();
                if (!string.IsNullOrEmpty(txtStreet3.Text))
                    OverviewData.Street3 = txtStreet3.Text.ToString();
                if (!string.IsNullOrEmpty(txtStreet4.Text))
                    OverviewData.Street4 = txtStreet4.Text.ToString();
                if (!string.IsNullOrEmpty(txtStreet5.Text))
                    OverviewData.Street5 = txtStreet5.Text.ToString();
                if (!string.IsNullOrEmpty(txtTotalFloor.Text))
                    OverviewData.TotalFloors = txtTotalFloor.Text.ToString();
                if (!string.IsNullOrEmpty(txtTotalRooms.Text))
                    OverviewData.TotalRooms = txtTotalRooms.Text.ToString();
                if (!string.IsNullOrEmpty(txtYearBuilt.Text))
                    OverviewData.YearBuilt = txtYearBuilt.Text.ToString();
                if (!string.IsNullOrEmpty(txtCompanyName.Text))
                    OverviewData.CompanyName = txtCompanyName.Text.ToString();
                //if (!string.IsNullOrEmpty(txtCommonHotelId.Text))
                //    OverviewData.CommonHotelId = txtCommonHotelId.Text.ToString();

                OverviewData.CompanyRecommended = chkCompanyRecommended.Checked;

                if (!string.IsNullOrEmpty(txtFinanceControlId.Text))
                    OverviewData.FinanceControlID = Convert.ToInt32(txtFinanceControlId.Text);
                if (!string.IsNullOrEmpty(txtCompanyHotelID.Text))
                    OverviewData.CompanyHotelID = Convert.ToInt32(txtCompanyHotelID.Text.ToString());

                OverviewData.Edit_Date = DateTime.Now;
                OverviewData.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                OverviewData.IsActive = true;
                OverviewData.IsMysteryProduct = blnMysteryProduct.Checked;
                OverviewData.IsRoomMappingCompleted = blnRTCompleted.Checked;
                System.Web.UI.HtmlControls.HtmlGenericControl alert_GeoCode = (System.Web.UI.HtmlControls.HtmlGenericControl)frmHotelOverview.FindControl("alert_GeoCode");
                System.Web.UI.HtmlControls.HtmlGenericControl dvMsgContact = (System.Web.UI.HtmlControls.HtmlGenericControl)frmHotelOverview.FindControl("contacts").FindControl("dvMsgContact");
                System.Web.UI.HtmlControls.HtmlGenericControl dvMsgDynamicAttributesForHotel = (System.Web.UI.HtmlControls.HtmlGenericControl)frmHotelOverview.FindControl("DynamicAttributesForHotel").FindControl("dvMsgDynamicAttributesForHotel");


                alert_GeoCode.Style.Add(HtmlTextWriterStyle.Display, "none");
                dvMsgContact.Style.Add(HtmlTextWriterStyle.Display, "none");
                dvMsgDynamicAttributesForHotel.Style.Add(HtmlTextWriterStyle.Display, "none");
                if (AccSvc.UpdateHotelDetail(OverviewData))
                { 
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Hotel has been updated successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }

                }
            }
            else if (e.CommandName == "CancelProduct")
            {
                TextBox txtHotelName = (TextBox)this.Parent.Page.FindControl("txtHotelName");
                DropDownList ddlCountry = (DropDownList)this.Parent.Page.FindControl("ddlCountry");
            }
            else if (e.CommandName == "CheckAddress")
            {
                TextBox txtHotelName = (TextBox)frmHotelOverview.FindControl("txtHotelName");
                TextBox txtStreet = (TextBox)frmHotelOverview.FindControl("txtStreet");
                TextBox txtStreet2 = (TextBox)frmHotelOverview.FindControl("txtStreet2");
                TextBox txtStreet3 = (TextBox)frmHotelOverview.FindControl("txtStreet3");
                TextBox txtStreet4 = (TextBox)frmHotelOverview.FindControl("txtStreet4");
                TextBox txtStreet5 = (TextBox)frmHotelOverview.FindControl("txtStreet5");
                TextBox txtPostalCode = (TextBox)frmHotelOverview.FindControl("txtPostalCode");
                DropDownList ddlSuburbs = (DropDownList)frmHotelOverview.FindControl("ddlSuburbs");
                DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
                DropDownList ddlState = (DropDownList)frmHotelOverview.FindControl("ddlState");
                DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
                DropDownList ddlArea = (DropDownList)frmHotelOverview.FindControl("ddlArea");
                DropDownList ddlLocation = (DropDownList)frmHotelOverview.FindControl("ddlLocation");
                TextBox txtHotelLat = (TextBox)frmHotelOverview.FindControl("txtHotelLat");
                TextBox txtHotelLon = (TextBox)frmHotelOverview.FindControl("txtHotelLon");

                TextBox txtAddressCheck_HotelName = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_HotelName");
                TextBox txtAddressCheck_Street = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street");
                TextBox txtAddressCheck_Street2 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street2");
                TextBox txtAddressCheck_Street3 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street3");
                TextBox txtAddressCheck_Street4 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street4");
                TextBox txtAddressCheck_Street5 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street5");
                TextBox txtAddressCheck_Suburbs = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Suburbs");
                TextBox txtAddressCheck_PostalCode = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_PostalCode");
                TextBox txtAddressCheck_Country = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Country");
                TextBox txtAddressCheck_State = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_State");
                TextBox txtAddressCheck_City = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_City");
                TextBox txtAddressCheck_Area = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Area");
                TextBox txtAddressCheck_Location = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Location");
                TextBox txtAddressCheck_Lat = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Lat");
                TextBox txtAddressCheck_Lng = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Lng");

                if (!string.IsNullOrWhiteSpace(txtHotelName.Text))
                {
                    txtAddressCheck_HotelName.Text = txtHotelName.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtStreet.Text))
                {
                    txtAddressCheck_Street.Text = txtStreet.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtStreet2.Text))
                {
                    txtAddressCheck_Street2.Text = txtStreet2.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtStreet3.Text))
                {
                    txtAddressCheck_Street3.Text = txtStreet3.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtStreet4.Text))
                {
                    txtAddressCheck_Street4.Text = txtStreet4.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtStreet5.Text))
                {
                    txtAddressCheck_Street5.Text = txtStreet5.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtPostalCode.Text))
                {
                    txtAddressCheck_PostalCode.Text = txtPostalCode.Text;
                }


                if (ddlSuburbs.SelectedIndex != 0)
                {
                    txtAddressCheck_Suburbs.Text = ddlSuburbs.SelectedItem.Text;
                }

                if (ddlCountry.SelectedIndex != 0)
                {
                    txtAddressCheck_Country.Text = ddlCountry.SelectedItem.Text;
                }

                if (ddlState.SelectedIndex != 0)
                {
                    txtAddressCheck_State.Text = ddlState.SelectedItem.Text;
                }

                if (ddlCity.SelectedIndex != 0)
                {
                    txtAddressCheck_City.Text = ddlCity.SelectedItem.Text;
                }

                if (ddlArea.SelectedIndex != 0)
                {
                    txtAddressCheck_Area.Text = ddlArea.SelectedItem.Text;
                }

                if (ddlLocation.SelectedIndex != 0)
                {
                    txtAddressCheck_Location.Text = ddlLocation.SelectedItem.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtHotelLat.Text))
                {
                    txtAddressCheck_Lat.Text = txtHotelLat.Text;
                }

                if (!string.IsNullOrWhiteSpace(txtHotelLon.Text))
                {
                    txtAddressCheck_Lng.Text = txtHotelLon.Text;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAddressCheckModal();", true);
            }

            else if (e.CommandName == "GeoLookUpAddress")
            {
                TextBox txtAddressCheck_HotelName = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_HotelName");
                TextBox txtAddressCheck_Street = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street");
                TextBox txtAddressCheck_Street2 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street2");
                TextBox txtAddressCheck_Street3 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street3");
                TextBox txtAddressCheck_Street4 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street4");
                TextBox txtAddressCheck_Street5 = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Street5");
                TextBox txtAddressCheck_Suburbs = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Suburbs");
                TextBox txtAddressCheck_PostalCode = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_PostalCode");
                TextBox txtAddressCheck_Country = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Country");
                TextBox txtAddressCheck_State = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_State");
                TextBox txtAddressCheck_City = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_City");
                TextBox txtAddressCheck_Area = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Area");
                TextBox txtAddressCheck_Location = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Location");
                HtmlGenericControl alert_addresslookup = (HtmlGenericControl)frmHotelOverview.FindControl("alert_addresslookup");


                MDMSVC.DC_Address_Physical objGeo = new MDMSVC.DC_Address_Physical();

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_HotelName.Text)))
                {
                    objGeo.Street = objGeo.Street + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_HotelName.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street.Text)))
                {
                    objGeo.Street = objGeo.Street + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street2.Text)))
                {
                    objGeo.Street = objGeo.Street + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street2.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street3.Text)))
                {
                    objGeo.Street = objGeo.Street + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street3.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street4.Text)))
                {
                    objGeo.Street = objGeo.Street + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street4.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street5.Text)))
                {
                    objGeo.Street = objGeo.Street + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Street5.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_PostalCode.Text)))
                {
                    objGeo.PostalCode = System.Web.HttpUtility.HtmlDecode(txtAddressCheck_PostalCode.Text);
                }


                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Suburbs.Text)))
                {
                    objGeo.CityOrTownOrVillage = objGeo.CityOrTownOrVillage + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Suburbs.Text) + ",";
                }


                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Country.Text)))
                {
                    objGeo.Country = System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Country.Text);
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_State.Text)))
                {
                    objGeo.CountyOrState = System.Web.HttpUtility.HtmlDecode(txtAddressCheck_State.Text);
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Area.Text)))
                {
                    objGeo.CityAreaOrDistrict = objGeo.CityAreaOrDistrict + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Area.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Location.Text)))
                {
                    objGeo.CityAreaOrDistrict = objGeo.CityAreaOrDistrict + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_Location.Text) + ",";
                }

                if (!string.IsNullOrWhiteSpace(System.Web.HttpUtility.HtmlDecode(txtAddressCheck_City.Text)))
                {
                    objGeo.CityOrTownOrVillage = objGeo.CityOrTownOrVillage + System.Web.HttpUtility.HtmlDecode(txtAddressCheck_City.Text) + ",";
                }

                objGeo.Product_Id = Guid.Parse(Request.QueryString["Hotel_Id"]);
                //Get Geo Address

                Repeater repGeoTopResult = (Repeater)frmHotelOverview.FindControl("repGeoTopResult");

                List<MDMSVC.DC_GeoLocation> objListGeoAddress = new List<MDMSVC.DC_GeoLocation>();

                var result = AccSvc.GetGeoLocationByAddress(objGeo);


                string status = string.Empty;
                MessageType msgtyp = new MessageType();

                if (result != null)
                {
                    objListGeoAddress.Add(result);

                    status = result.status;

                    if (result.results != null)
                        msgtyp = MessageType.Success;
                    else
                        msgtyp = MessageType.Info;
                }
                else
                {
                    objListGeoAddress.Add(new MDMSVC.DC_GeoLocation { results = null, status = "Address Lookup failed. Please try again." });
                    status = objListGeoAddress[0].status;
                    msgtyp = MessageType.Error;
                }

                repGeoTopResult.DataSource = objListGeoAddress;
                repGeoTopResult.DataBind();

                if (msgtyp == MessageType.Error)
                    BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, status, BootstrapAlertType.Warning);
                else if (msgtyp == MessageType.Success)
                    BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, status, BootstrapAlertType.Success);
                else if (msgtyp == MessageType.Info)
                    BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, status, BootstrapAlertType.Information);

                //ShowMessage(status, msgtyp, "#alert_addresslookup");

                UpdatePanel upAddressLookUp = (UpdatePanel)frmHotelOverview.FindControl("upAddressLookUp");
                upAddressLookUp.Update();

            }

            else if (e.CommandName == "GeoLookUpLatLng")
            {
                TextBox txtAddressCheck_Lat = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Lat");
                TextBox txtAddressCheck_Lng = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Lng");

                MDMSVC.DC_Address_GeoCode objGeo = new MDMSVC.DC_Address_GeoCode();

                objGeo.Latitude = float.Parse(txtAddressCheck_Lat.Text);
                objGeo.Longitude = float.Parse(txtAddressCheck_Lng.Text);
                objGeo.Product_Id = Guid.Parse(Request.QueryString["Hotel_Id"]);

                Repeater repGeoTopResult = (Repeater)frmHotelOverview.FindControl("repGeoTopResult");

                List<MDMSVC.DC_GeoLocation> objListGeoAddress = new List<MDMSVC.DC_GeoLocation>();


                var result = AccSvc.GetGeoLocationByLatLng(objGeo);
                if (result != null)
                {
                    objListGeoAddress.Add(result);
                }
                else
                {
                    objListGeoAddress.Add(new MDMSVC.DC_GeoLocation { results = null, status = "Address Lookup failed. Please try again." });
                }

                repGeoTopResult.DataSource = objListGeoAddress;
                repGeoTopResult.DataBind();
            }

            else if (e.CommandName == "GetGeoLatLng")
            {
                TextBox txtHotelName = (TextBox)frmHotelOverview.FindControl("txtHotelName");
                TextBox txtStreet = (TextBox)frmHotelOverview.FindControl("txtStreet");
                TextBox txtStreet2 = (TextBox)frmHotelOverview.FindControl("txtStreet2");
                TextBox txtStreet3 = (TextBox)frmHotelOverview.FindControl("txtStreet3");
                TextBox txtStreet4 = (TextBox)frmHotelOverview.FindControl("txtStreet4");
                TextBox txtStreet5 = (TextBox)frmHotelOverview.FindControl("txtStreet5");
                TextBox txtPostalCode = (TextBox)frmHotelOverview.FindControl("txtPostalCode");
                DropDownList ddlSuburbs = (DropDownList)frmHotelOverview.FindControl("ddlSuburbs");
                DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
                DropDownList ddlState = (DropDownList)frmHotelOverview.FindControl("ddlState");
                DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
                DropDownList ddlArea = (DropDownList)frmHotelOverview.FindControl("ddlArea");
                DropDownList ddlLocation = (DropDownList)frmHotelOverview.FindControl("ddlLocation");

                MDMSVC.DC_Address_Physical objGeo = new MDMSVC.DC_Address_Physical();

                if (!string.IsNullOrWhiteSpace(txtHotelName.Text))
                {
                    objGeo.Street = objGeo.Street + txtHotelName.Text + ",";
                }

                if (!string.IsNullOrWhiteSpace(txtStreet.Text))
                {
                    objGeo.Street = objGeo.Street + txtStreet.Text + ",";
                }

                if (!string.IsNullOrWhiteSpace(txtStreet2.Text))
                {
                    objGeo.Street = objGeo.Street + txtStreet2.Text + ",";
                }

                if (!string.IsNullOrWhiteSpace(txtStreet3.Text))
                {
                    objGeo.Street = objGeo.Street + txtStreet3.Text + ",";
                }

                if (!string.IsNullOrWhiteSpace(txtStreet4.Text))
                {
                    objGeo.Street = objGeo.Street + txtStreet4.Text + ",";
                }

                if (!string.IsNullOrWhiteSpace(txtStreet5.Text))
                {
                    objGeo.Street = objGeo.Street + txtStreet5.Text + ",";
                }

                if (!string.IsNullOrWhiteSpace(txtPostalCode.Text))
                {
                    objGeo.PostalCode = txtPostalCode.Text;
                }

                if (ddlSuburbs.SelectedIndex != 0)
                {
                    objGeo.CityOrTownOrVillage = objGeo.CityOrTownOrVillage + ddlSuburbs.SelectedItem.Text + ",";
                }

                if (ddlCountry.SelectedIndex != 0)
                {
                    objGeo.Country = ddlCountry.SelectedItem.Text;
                }

                if (ddlState.SelectedIndex != 0)
                {
                    objGeo.CountyOrState = ddlState.SelectedItem.Text;
                }

                if (ddlArea.SelectedIndex != 0)
                {
                    objGeo.CityAreaOrDistrict = objGeo.CityAreaOrDistrict + ddlArea.SelectedItem.Text + ",";
                }

                if (ddlLocation.SelectedIndex != 0)
                {
                    objGeo.CityAreaOrDistrict = objGeo.CityAreaOrDistrict + ddlLocation.SelectedItem.Text + ",";
                }

                if (ddlCity.SelectedIndex != 0)
                {
                    objGeo.CityOrTownOrVillage = objGeo.CityOrTownOrVillage + ddlCity.SelectedItem.Text + ",";
                }

                objGeo.Product_Id = Guid.Parse(Request.QueryString["Hotel_Id"]);

                HtmlGenericControl alert_GeoCode = (HtmlGenericControl)frmHotelOverview.FindControl("alert_GeoCode");
                //Get Geo Address
                var result = AccSvc.GetGeoLocationByAddress(objGeo);
                if (result != null)
                {
                    if (result.results != null)
                    {
                        if (result.results.Length > 0)
                        {
                            TextBox txtHotelLat = (TextBox)frmHotelOverview.FindControl("txtHotelLat");
                            TextBox txtHotelLon = (TextBox)frmHotelOverview.FindControl("txtHotelLon");

                            txtHotelLat.Text = result.results[0].geometry.location.lat.ToString();
                            txtHotelLon.Text = result.results[0].geometry.location.lng.ToString();

                            alert_GeoCode.Visible = true;
                            BootstrapAlert.BootstrapAlertMessage(alert_GeoCode, result.status, BootstrapAlertType.Success);
                            //ShowMessage(result.status, MessageType.Success, "#alert_GeoCode");
                        }
                    }
                    else
                    {
                        alert_GeoCode.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(alert_GeoCode, result.status, BootstrapAlertType.Information);

                        //  ShowMessage(result.status, MessageType.Info, "#alert_GeoCode");
                    }
                }
                else
                {
                    alert_GeoCode.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(alert_GeoCode, "Lookup failed.Please try again", BootstrapAlertType.Warning);

                    //ShowMessage("Lookup failed. Please try again.", MessageType.Error, "#alert_GeoCode");
                }

            }

            else if (e.CommandName == "GetGeoAddressByLatLng")
            {
                TextBox txtHotelLat = (TextBox)frmHotelOverview.FindControl("txtHotelLat");
                TextBox txtHotelLon = (TextBox)frmHotelOverview.FindControl("txtHotelLon");
                MDMSVC.DC_Address_GeoCode objGeo = new MDMSVC.DC_Address_GeoCode();
                objGeo.Latitude = float.Parse(txtHotelLat.Text);
                objGeo.Longitude = float.Parse(txtHotelLon.Text);
                objGeo.Product_Id = Guid.Parse(Request.QueryString["Hotel_Id"]);

                Repeater repGeoTopResult = (Repeater)frmHotelOverview.FindControl("repGeoTopResult");

                List<MDMSVC.DC_GeoLocation> objListGeoAddress = new List<MDMSVC.DC_GeoLocation>();
                HtmlGenericControl alert_addresslookup = (HtmlGenericControl)frmHotelOverview.FindControl("alert_addresslookup");

                var result = AccSvc.GetGeoLocationByLatLng(objGeo);
                if (result != null)
                {
                    objListGeoAddress.Add(result);
                    if (result.results != null)
                    {
                        //ShowMessage(result.status, MessageType.Success, "#alert_addresslookup"); 
                        BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, result.status, BootstrapAlertType.Success);
                    }
                    else
                    {
                        BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, result.status, BootstrapAlertType.Information);
                        // ShowMessage(result.status, MessageType.Info, "#alert_addresslookup");
                    }
                }
                else
                {
                    objListGeoAddress.Add(new MDMSVC.DC_GeoLocation { results = null, status = "Address Lookup failed. Please try again." });
                    // ShowMessage("Lookup failed. Please try again.", MessageType.Error, "#alert_addresslookup");
                    BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, "Lookup failed. Please try again.", BootstrapAlertType.Warning);

                }

                repGeoTopResult.DataSource = objListGeoAddress;
                repGeoTopResult.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAddressCheckModal();", true);

                UpdatePanel upAddressLookUp = (UpdatePanel)frmHotelOverview.FindControl("upAddressLookUp");
                upAddressLookUp.Update();

            }

        }

        private bool CheckForDuplicate()
        {
            bool ret = false;

            DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
            DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
            DropDownList ddlProductCategorySubType = (DropDownList)frmHotelOverview.FindControl("ddlProductCategorySubType");
            TextBox txtHotelName = (TextBox)frmHotelOverview.FindControl("txtHotelName");
            TextBox txtCommonHotelId = (TextBox)frmHotelOverview.FindControl("txtCommonHotelId");
            TextBox txtPostalCode = (TextBox)frmHotelOverview.FindControl("txtPostalCode");
            TextBox txtHotelID = (TextBox)frmHotelOverview.FindControl("txtHotelID");
            RQParams.ProductCategory = "Accommodation";
            RQParams.ProductCategorySubType = ddlProductCategorySubType.SelectedItem.Text;
            RQParams.Status = "ACTIVE";

            if (txtHotelName.Text.Length != 0)
                RQParams.HotelName = txtHotelName.Text;


            //if (txtCommonHotelId.Text.Length != 0)
            //{
            //    int CompanyHotelId = 0;
            //    int.TryParse(txtCommonHotelId.Text, out CompanyHotelId);

            //    if (CompanyHotelId != 0)
            //        RQParams.CompanyHotelId = Convert.ToInt32(txtCommonHotelId.Text);
            //}

            if (ddlCountry.SelectedItem.Text != "---ALL---")
                RQParams.Country = ddlCountry.SelectedItem.Text;
            if (ddlCity.SelectedItem.Text != "---ALL---")
                RQParams.City = ddlCity.SelectedItem.Text;

            RQParams.PageNo = 0;
            RQParams.PageSize = 5;

            List<MDMSVC.DC_Accomodation_Search_RS> res = AccSvc.SearchHotels(RQParams);
            if (res != null)
            {
                foreach (MDMSVC.DC_Accomodation_Search_RS rs in res)
                {
                    if (txtHotelID.Text.ToUpper() != rs.AccomodationId.ToUpper())
                    {
                        if (rs.Country.ToUpper() == ddlCountry.SelectedItem.Text.ToUpper())
                        {
                            if (rs.City.ToUpper() == ddlCity.SelectedItem.Text.ToUpper())
                            {
                                if (rs.PostalCode.ToUpper() == txtPostalCode.Text.ToUpper())
                                {
                                    if (rs.HotelName.TrimStart().TrimEnd().ToUpper().Replace("Hotel", "").Replace("'", "").Replace("-", "") == txtHotelName.Text.TrimStart().TrimEnd().ToUpper().Replace("Hotel", "").Replace("'", "").Replace("-", ""))
                                    {
                                        ret = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        protected void repGeoAddress_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlMapGeoAddressTo = (DropDownList)e.Item.FindControl("ddlMapGeoAddressTo");

                foreach (var type in ((TLGX_Consumer.MDMSVC.Address_Components)e.Item.DataItem).types)
                {
                    if (type == "country")
                    {
                        if (ddlMapGeoAddressTo != null)
                        {
                            ddlMapGeoAddressTo.Enabled = false;
                        }
                        break;
                    }
                    else
                    {
                        if (ddlMapGeoAddressTo != null)
                        {
                            ddlMapGeoAddressTo.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "AddressComponents").MasterAttributeValues;
                            ddlMapGeoAddressTo.DataTextField = "AttributeValue";
                            ddlMapGeoAddressTo.DataValueField = "MasterAttributeValue_Id";
                            ddlMapGeoAddressTo.DataBind();
                        }
                        break;
                    }
                }
            }
        }

        protected void repGeoAddress_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAddress")
            {
                MDMSVC.DC_Country_State_City_Area_Location CSCAL = new MDMSVC.DC_Country_State_City_Area_Location();
                TextBox txtAddressCheck_Country = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Country");
                TextBox txtAddressCheck_State = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_State");
                TextBox txtAddressCheck_City = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_City");
                TextBox txtAddressCheck_Area = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Area");
                TextBox txtAddressCheck_Location = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_Location");
                HtmlGenericControl alert_addresslookup = (HtmlGenericControl)frmHotelOverview.FindControl("alert_addresslookup");

                CSCAL.Country = txtAddressCheck_Country.Text;
                CSCAL.State = txtAddressCheck_State.Text;
                CSCAL.City = txtAddressCheck_City.Text;
                CSCAL.Area = txtAddressCheck_Area.Text;
                CSCAL.Location = txtAddressCheck_Location.Text;

                Repeater repGeoAddress = (Repeater)e.Item.Parent;
                bool bDuplicateFound = false;
                string sAreaSelected = string.Empty;
                string sLocationSelected = string.Empty;

                foreach (RepeaterItem item in repGeoAddress.Items)
                {
                    if (bDuplicateFound)
                    {
                        break;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        DropDownList ddlMapGeoAddressTo = (DropDownList)item.FindControl("ddlMapGeoAddressTo");
                        Label lblAddressLongName = (Label)item.FindControl("lblAddressLongName");

                        if (ddlMapGeoAddressTo.SelectedItem.Text.ToLower().StartsWith("street"))
                        {
                            continue;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text.ToLower().StartsWith("area") && sAreaSelected == string.Empty)
                        {
                            sAreaSelected = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text.ToLower().StartsWith("location") && sLocationSelected == string.Empty)
                        {
                            sLocationSelected = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text != "-Select-")
                        {
                            foreach (RepeaterItem itemCheck in repGeoAddress.Items)
                            {
                                if ((itemCheck.ItemType == ListItemType.Item || itemCheck.ItemType == ListItemType.AlternatingItem) && item != itemCheck)
                                {
                                    DropDownList ddlMapGeoAddressToCheck = (DropDownList)itemCheck.FindControl("ddlMapGeoAddressTo");
                                    if (ddlMapGeoAddressTo.SelectedItem.Text == ddlMapGeoAddressToCheck.SelectedItem.Text)
                                    {
                                        BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, "Mapping attributes can only be selected once.", BootstrapAlertType.Warning);
                                        //ShowMessage("Mapping attributes can only be selected once.", MessageType.Warning, "#alert_addresslookup");

                                        bDuplicateFound = true;

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (bDuplicateFound)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAddressCheckModal();", true);
                    return;
                }

                TextBox txtStreet = (TextBox)frmHotelOverview.FindControl("txtStreet");
                TextBox txtStreet2 = (TextBox)frmHotelOverview.FindControl("txtStreet2");
                TextBox txtStreet3 = (TextBox)frmHotelOverview.FindControl("txtStreet3");
                TextBox txtStreet4 = (TextBox)frmHotelOverview.FindControl("txtStreet4");
                TextBox txtStreet5 = (TextBox)frmHotelOverview.FindControl("txtStreet5");
                TextBox txtPostalCode = (TextBox)frmHotelOverview.FindControl("txtPostalCode");
                DropDownList ddlSuburbs = (DropDownList)frmHotelOverview.FindControl("ddlSuburbs");
                TextBox txtHotelLat = (TextBox)frmHotelOverview.FindControl("txtHotelLat");
                TextBox txtHotelLon = (TextBox)frmHotelOverview.FindControl("txtHotelLon");
                DropDownList ddlCountry = (DropDownList)frmHotelOverview.FindControl("ddlCountry");
                DropDownList ddlState = (DropDownList)frmHotelOverview.FindControl("ddlState");
                DropDownList ddlCity = (DropDownList)frmHotelOverview.FindControl("ddlCity");
                DropDownList ddlArea = (DropDownList)frmHotelOverview.FindControl("ddlArea");
                DropDownList ddlLocation = (DropDownList)frmHotelOverview.FindControl("ddlLocation");

                if (string.IsNullOrWhiteSpace(sAreaSelected) && !string.IsNullOrWhiteSpace(sLocationSelected) && ddlArea.SelectedIndex <= 0)
                {
                    BootstrapAlert.BootstrapAlertMessage(alert_addresslookup, "To map Location, please select the Area as well.", BootstrapAlertType.Warning);
                    //ShowMessage("To map Location, please select the Area as well.", MessageType.Warning, "#alert_addresslookup");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAddressCheckModal();", true);
                    return;
                }

                for (var i = 0; i < repGeoAddress.Items.Count; i++)
                {
                    var item = repGeoAddress.Items[i];

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label lblAddressLongName = (Label)item.FindControl("lblAddressLongName");
                        DropDownList ddlMapGeoAddressTo = (DropDownList)item.FindControl("ddlMapGeoAddressTo");

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "StreetName")
                        {
                            if (i == 0)
                            {
                                txtStreet.Text = lblAddressLongName.Text;
                            }
                            else
                            {
                                txtStreet.Text = txtStreet.Text + ", " + lblAddressLongName.Text;
                            }
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Street2")
                        {
                            if (i == 0)
                            {
                                txtStreet2.Text = lblAddressLongName.Text;
                            }
                            else
                            {
                                txtStreet2.Text = txtStreet2.Text + ", " + lblAddressLongName.Text;
                            }
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Street3")
                        {
                            if (i == 0)
                            {
                                txtStreet3.Text = lblAddressLongName.Text;
                            }
                            else
                            {
                                txtStreet3.Text = txtStreet3.Text + ", " + lblAddressLongName.Text;
                            }
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Street4")
                        {
                            if (i == 0)
                            {
                                txtStreet4.Text = lblAddressLongName.Text;
                            }
                            else
                            {
                                txtStreet4.Text = txtStreet4.Text + ", " + lblAddressLongName.Text;
                            }
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Street5")
                        {
                            if (i == 0)
                            {
                                txtStreet5.Text = lblAddressLongName.Text;
                            }
                            else
                            {
                                txtStreet5.Text = txtStreet5.Text + ", " + lblAddressLongName.Text;
                            }
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "PostalCode")
                        {
                            txtPostalCode.Text = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Country")
                        {
                            CSCAL.Country = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "City")
                        {
                            CSCAL.City = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "State")
                        {
                            CSCAL.State = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Area")
                        {
                            CSCAL.Area = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Location")
                        {
                            CSCAL.Location = lblAddressLongName.Text;
                        }

                        if (ddlMapGeoAddressTo.SelectedItem.Text == "Suburbs")
                        {

                            if (ddlSuburbs.Items.FindByText(lblAddressLongName.Text) != null)
                            {
                                ddlSuburbs.ClearSelection();
                                ddlSuburbs.Items.FindByText(lblAddressLongName.Text).Selected = true;
                            }
                        }

                    }
                }

                RepeaterItem objLocation = (RepeaterItem)e.Item.Parent.Parent;
                Label lblAddressCheck_Latitude = (Label)objLocation.FindControl("lblAddressCheck_Latitude");
                Label lblAddressCheck_Longitude = (Label)objLocation.FindControl("lblAddressCheck_Longitude");

                txtHotelLat.Text = lblAddressCheck_Latitude.Text;
                txtHotelLon.Text = lblAddressCheck_Longitude.Text;


                if (AccSvc.UpdateAdressHeirarchy(CSCAL))
                {

                    fillcoutries();

                    if (ddlCountry.Items.FindByText(CSCAL.Country) != null)
                    {
                        ddlCountry.ClearSelection();
                        ddlCountry.Items.FindByText(CSCAL.Country).Selected = true;
                    }

                    fillstates();

                    if (ddlState.Items.FindByText(CSCAL.State) != null)
                    {
                        ddlState.ClearSelection();
                        ddlState.Items.FindByText(CSCAL.State).Selected = true;
                    }

                    fillcities();
                    if (ddlCity.Items.FindByText(CSCAL.City) != null)
                    {
                        ddlCity.ClearSelection();
                        ddlCity.Items.FindByText(CSCAL.City).Selected = true;
                    }

                    fillarea();

                    if (ddlArea.Items.FindByText(CSCAL.Area) != null)
                    {
                        ddlArea.ClearSelection();
                        ddlArea.Items.FindByText(CSCAL.Area).Selected = true;
                    }


                    filllocation();

                    if (ddlLocation.Items.FindByText(CSCAL.Location) != null)
                    {
                        ddlLocation.ClearSelection();
                        ddlLocation.Items.FindByText(CSCAL.Location).Selected = true;
                    }
                }
            }
        }

        //protected void repGeoResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Control divGeoLookupAddressMap = e.Item.FindControl("divGeoLookupAddressMap");
        //        Label lblAddressCheck_Latitude = (Label)e.Item.FindControl("lblAddressCheck_Latitude");
        //        Label lblAddressCheck_Longitude = (Label)e.Item.FindControl("lblAddressCheck_Longitude");
        //        TextBox txtAddressCheck_HotelName = (TextBox)frmHotelOverview.FindControl("txtAddressCheck_HotelName");
        //        UpdatePanel upAddressLookUp = (UpdatePanel)frmHotelOverview.FindControl("upAddressLookUp");
        //        if (divGeoLookupAddressMap != null)
        //        {
        //            ScriptManager.RegisterStartupScript(upAddressLookUp, upAddressLookUp.GetType(), "ShowMap", "initAddressCheckMap(" + lblAddressCheck_Latitude.Text + "," + lblAddressCheck_Longitude.Text + "," + "," + divGeoLookupAddressMap.ClientID + "," + txtAddressCheck_HotelName.Text + ");", true);
        //        }

        //    }
        //}

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillstates();
            fillcities();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillarea();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            filllocation();
        }

        public enum MessageType { Success, Error, Info, Warning }
        protected void ShowMessage(string Message, MessageType type, string divid)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "','" + divid + "');", true);
        }

        public void DisplayMessage(string strMessage, BootstrapAlertType MessageType)
        {
            dvMsg.Style.Add("display", "block");
            dvMsg.Attributes.Remove("class");
            string style = "";
            switch (MessageType)
            {
                case BootstrapAlertType.Plain:
                    style = "alert alert-info alert-dismissable";
                    break;
                case BootstrapAlertType.Success:
                    style = "alert alert-success alert-dismissable";
                    break;
                case BootstrapAlertType.Information:
                    style = "alert alert-info alert-dismissable";
                    break;
                case BootstrapAlertType.Warning:
                    style = "alert alert-warning alert-dismissable";
                    break;
                case BootstrapAlertType.Danger:
                    style = "alert alert-danger alert-dismissable";
                    break;
                case BootstrapAlertType.Primary:
                    style = "alert alert-info alert-dismissable";
                    break;
            }
            dvMsg.Attributes.Add("class", style);
            dvMsg.InnerHtml = "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>" + MessageType + "!</strong> <span> " + strMessage;
            //"A file with the same name already exists.<br />Your file was saved as " + fileName;

        }

    }
}