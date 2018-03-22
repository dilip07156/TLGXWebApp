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
                ddlEditZoneType.SelectedIndex = ddlEditZoneType.Items.IndexOf(ddlEditZoneType.Items.FindByText(result[0].Zone_Type));

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
            zh.Latitude = txtEditLatitude.Text;
            zh.Longitude = txtEditLongitude.Text;
            zh.CountryName = ddlMasterCountryEdit.SelectedItem.Text;
            zh.DistanceRange = Convert.ToInt32(ddlShowDistance.SelectedValue);
           
            var result = masterSVc.SearchZoneHotels(zh);
            if (result != null && result.Count > 0)
            {
                grdZoneHotelSearch.DataSource = result;
                grdZoneHotelSearch.DataBind();
            }
            else
            {
                grdZoneHotelSearch.DataSource = null;
                grdZoneHotelSearch.DataBind();
            }
        }
        protected void ddlMasterCountryEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fillcities(ddlMasterCityEdit, ddlMasterCountryEdit);
        }

        protected void btnUpdateZoneMaster_Click(object sender, EventArgs e)
        {
            dvUpdateMsg.Style.Add("display", "none");
            dvmsgUpdateZone.Style.Add("display", "none");
            var a = HdnCountryChangeFlag.Value;
            MDMSVC.DC_ZoneRQ Param = new MDMSVC.DC_ZoneRQ();
            Param.Action = "UPDATE";
            Param.Zone_id = Zone_Id;
            Param.Zone_Name = txtEditZoneName.Text;
            Param.Latitude = txtEditLatitude.Text;
            Param.Longitude = txtEditLongitude.Text;
            Param.Zone_Type = ddlEditZoneType.SelectedItem.Text;
            Param.Edit_Date = DateTime.Now;
            Param.Edit_User= System.Web.HttpContext.Current.User.Identity.Name;
            if (a== "True")
            {
                var res = masterSVc.DeleteZoneCities(Param);
                var result = masterSVc.AddzoneMaster(Param);
                if (result != null)
                {
                    if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                    {
                        BootstrapAlert.BootstrapAlertMessage(dvmsgUpdateZone, "Zone has been Updated successfully", BootstrapAlertType.Success);
                        fillgrdZoneCitiesData(Zone_Id); 
                        BootstrapAlert.BootstrapAlertMessage(dvMsgaddZoneCity, "Please Add atleast One CITY for this zone", BootstrapAlertType.Warning);
                        fillcities(ddlMasterCityEdit, ddlMasterCountryEdit);
                    } 
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvmsgUpdateZone, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                }
                
            }
            else if (a == "False")
            {
                getZoneInfo(string.Empty);
            }
            else
            {
                var result = masterSVc.AddzoneMaster(Param);
            }
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