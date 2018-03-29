using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.geography
{
    public partial class ZoneCityMasterEdit : System.Web.UI.Page
    {
        public Guid Zone_Id;
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        MasterDataDAL masterdata = new MasterDataDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Zone_Id = new Guid(Request.QueryString["Zone_Id"]);
            hdnZone_id.Value = Zone_Id.ToString();
            if (!IsPostBack)
            {
                LoadMasters();
                getZoneInfo(string.Empty);
            }
        }
        protected void LoadMasters()
        {
            fillcountries(ddlMasterCountryEdit);
            BindZoneType(ddlEditZoneType);
            BindZoneRadius(ddlZoneRadius); 
            BindZoneRadius(ddlShowDistance);
        }
        private void fillcountries(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = masterSVc.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
        }
        private void fillcities(DropDownList ddl, DropDownList ddlp)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {
                //ddl.DataSource = objMasterDataDAL.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddl.DataSource = masterSVc.GetMasterCityData(ddlp.SelectedItem.Value);
                ddl.DataValueField = "City_ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        private void BindZoneType(DropDownList ddl)
        {
            ddl.Items.Clear();
            var result = masterSVc.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "Zone", Name = "ZoneType" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddl.Items.Clear();
                    ddl.DataSource = result;
                    ddl.DataTextField = "AttributeValue";
                    ddl.DataValueField = "AttributeValue";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                }
        }
        private void BindZoneRadius(DropDownList ddl)
        {
            ddl.Items.Clear();
            var result = masterSVc.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "Zone", Name = "ZoneRadius" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddl.Items.Clear();
                    ddl.DataSource = result;
                    ddl.DataTextField = "AttributeValue";
                    ddl.DataValueField = "AttributeValue";
                    ddl.DataBind();
                    //ddl.Items.FindByValue("4.0").Selected = true;
                }
        }

        public void getZoneInfo(string calledfrom)
        {
            var result = masterSVc.SearchZone(new MDMSVC.DC_ZoneRQ  { Zone_id = Zone_Id });
            if(result!=null && result.Count > 0)
            {
                lblEditZoneName.Text = (from m in result select m.Zone_Name).FirstOrDefault();
                txtEditZoneName.Text = (from m in result select m.Zone_Name).FirstOrDefault();
                txtEditLatitude.Text= (from m in result select m.Latitude).FirstOrDefault();
                txtEditLongitude.Text = (from m in result select m.Longitude).FirstOrDefault();
                ddlMasterCountryEdit.SelectedIndex = ddlMasterCountryEdit.Items.IndexOf(ddlMasterCountryEdit.Items.FindByText(result[0].CountryName));
                ddlZoneRadius.SelectedIndex = ddlZoneRadius.Items.IndexOf(ddlZoneRadius.Items.FindByText((result[0].Zone_Radius).ToString()));
                ddlEditZoneType.SelectedIndex = ddlEditZoneType.Items.IndexOf(ddlEditZoneType.Items.FindByText(result[0].Zone_Type));
                ddlShowDistance.SelectedIndex = ddlShowDistance.Items.IndexOf(ddlShowDistance.Items.FindByText((result[0].Zone_Radius).ToString()));
                hdnCountryId.Value = ddlMasterCountryEdit.SelectedValue;
                fillcities(ddlMasterCityEdit, ddlMasterCountryEdit);
                //cityList
                fillgrdZoneCitiesData(Zone_Id);
                //hotelList
                fillgrdZoneHotelSearchData();
                // HotelMap()
            }
        }
        protected void fillgrdZoneCitiesData(Guid Zone_id)
        {
            MDMSVC.DC_ZoneRQ zc = new MDMSVC.DC_ZoneRQ();
            zc.Zone_id = Zone_id;
            var res = masterSVc.SearchZoneCities(zc);
            if (res != null && res.Count > 0)
            {
                grdZoneCities.DataSource = res;
                grdZoneCities.DataBind();
            }
            else
            {
                grdZoneCities.DataSource = null;
                grdZoneCities.DataBind();
            }
        }

        protected void fillgrdZoneHotelSearchData()
        {
            MDMSVC.DC_ZoneRQ zh = new MDMSVC.DC_ZoneRQ();
            var DistanceRange = Convert.ToDouble(ddlShowDistance.SelectedValue);
            zh.Zone_id = Zone_Id;
            var result = masterSVc.SearchZoneHotels(zh);
            if (result != null && result.Count > 0)
            {
                result = (from a in result where a.Distance <= DistanceRange select a).ToList();
                lblTotalCount.Text = (result.Count).ToString();
                grdZoneHotelSearch.DataSource = result;
                grdZoneHotelSearch.DataBind();
            }
            else
            {
                lblTotalCount.Text = string.Empty;
                grdZoneHotelSearch.DataSource = null;
                grdZoneHotelSearch.DataBind();
            }
        }
        protected void btnUpdateZoneMaster_Click(object sender, EventArgs e)
        {
            dvUpdateMsg.Style.Add("display", "none");
            dvmsgUpdateZone.Style.Add("display", "none");
            string lat   = txtEditLatitude.Text;
            string longg = txtEditLongitude.Text;
            double zoneRadius = Convert.ToDouble(ddlZoneRadius.SelectedValue);

            var searchZone = masterSVc.SearchZone(new MDMSVC.DC_ZoneRQ { Zone_id = Zone_Id });
            if (searchZone != null)
            {
                var ExistingZone = (from a in searchZone select a).First();
                if(ExistingZone.Latitude!= lat || ExistingZone.Longitude!= longg)
                {
                    var deletezone = masterSVc.DeleteZoneHotelsInTable(new MDMSVC.DC_ZoneRQ { Zone_id = Zone_Id });
                }
                if(ExistingZone.Zone_Radius!= zoneRadius)
                {
                    var updateZone = masterSVc.UpdateZoneHotelsInTable(new MDMSVC.DC_ZoneRQ { Zone_id = Zone_Id, Zone_Radius= zoneRadius });
                }
            }
            
            MDMSVC.DC_ZoneRQ UPdparam = new MDMSVC.DC_ZoneRQ();
            UPdparam.Action = "UPDATE";
            UPdparam.Zone_id = Zone_Id;
            UPdparam.Zone_Name = txtEditZoneName.Text;
            UPdparam.Latitude = lat;
            UPdparam.Longitude = longg;
            UPdparam.Zone_Type = ddlEditZoneType.SelectedItem.Text;
            UPdparam.Edit_Date = DateTime.Now;
            UPdparam.Edit_User= System.Web.HttpContext.Current.User.Identity.Name;
            UPdparam.CountryName = ddlMasterCountryEdit.SelectedItem.Text;
            UPdparam.Zone_Radius = zoneRadius;
            var result = masterSVc.AddzoneMaster(UPdparam);
            var addZone = masterSVc.InsertZoneHotelsInTable(UPdparam);
            getZoneInfo(string.Empty);
        }
        protected void btnAddZoneCity_Click(object sender, EventArgs e)
        {
            dvmsgUpdateZone.Style.Add("display", "none");
            MDMSVC.DC_ZoneRQ RQ = new MDMSVC.DC_ZoneRQ();
            RQ.Zone_id = Zone_Id;
            RQ.City_id = new Guid(ddlMasterCityEdit.SelectedValue);
            var result = masterSVc.AddZoneCityMapping(RQ);
            if (result != null)
            {
                if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgaddZoneCity, "City has been added successfully", BootstrapAlertType.Success);
                    fillgrdZoneCitiesData(Zone_Id);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsgaddZoneCity, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            }
        }

        protected void btnRedirectToSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/geography/Zone.aspx");
        }

        protected void ddlShowDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillgrdZoneHotelSearchData();
        }
    }
}