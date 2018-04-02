using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.geography
{
    public partial class Zone : System.Web.UI.Page
    {
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        MasterDataDAL masterdata = new MasterDataDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMasters();
                //Bind Page Controls
                if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri.Contains("ZoneCityMasterEdit"))
                {
                    SetControls();
                }

            }
        }
        private void SetControls()
        {
            try
            {
                #region Get Value from query string
                string CountryName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CoN"]);
                string CountryID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CoID"]);
                string CityName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CiN"]);
                string CityID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CiID"]);
                string ZoneType = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ZoType"]);
                string ZoneStatus = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["Status"]);
                string PageNo = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PN"]);
                string PageSize = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PS"]);
                #endregion
                int pageno = 0;
                int pagesize = 10;

                //if (!string.IsNullOrWhiteSpace(CountryID))
                //  txtProductName.Text = ProductName;
                if (!string.IsNullOrWhiteSpace(CountryID))
                {
                    ddlMasterCountry.SelectedIndex = ddlMasterCountry.Items.IndexOf(ddlMasterCountry.Items.FindByText(CountryName));
                    if (!string.IsNullOrWhiteSpace(CountryID))
                        fillcities(ddlMasterCity, ddlMasterCountry);
                }
                if (!string.IsNullOrWhiteSpace(CityName))
                    ddlMasterCity.SelectedIndex = ddlMasterCity.Items.IndexOf(ddlMasterCity.Items.FindByText(CityName));


                if (!string.IsNullOrWhiteSpace(ZoneType))
                    ddlZoneType.SelectedIndex = ddlZoneType.Items.IndexOf(ddlZoneType.Items.FindByText(ZoneType));

                if (!string.IsNullOrWhiteSpace(ZoneStatus))
                    ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(ZoneStatus));

                if (!string.IsNullOrWhiteSpace(PageNo))
                    pageno = Convert.ToInt32(PageNo);
                if (!string.IsNullOrWhiteSpace(PageSize))
                    pagesize = Convert.ToInt32(PageSize);

                fillMasterSearchData(pageno, pagesize);

            }
            catch (Exception) { }
        }
        private void LoadMasters()
        {
            fillcountries(ddlMasterCountry);
            fillcountries(ddlMasterCountryAddModal);
            BindZoneType(ddlAddZoneType);
            BindZoneType(ddlZoneType);
            BindZoneRadius(ddlAddHotelIncludeRange);
        }
        private void fillcountries(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = masterSVc.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
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
            ddl.Items.Insert(0, new ListItem("--ALL--", "0"));
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
                    ddl.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }
        }
        private void BindZoneRadius(DropDownList ddl)
        {
            ddl.Items.Clear();
            var result = masterSVc.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "Zone", Name = "ZoneRadius" });
            result = (from a in result select a).OrderBy(s => s.AttributeValue).ToList();
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
        private void fillMasterSearchData(int pageindex, int pagesize)
        {
            try
            {
                MDMSVC.DC_ZoneRQ R = new MDMSVC.DC_ZoneRQ();
                R.PageNo = pageindex;
                R.PageSize = pagesize;
                if (ddlMasterCountry.SelectedItem.Value != "0")
                    R.Country_id = new Guid(ddlMasterCountry.SelectedValue);
                if (ddlMasterCity.SelectedItem.Value != "0")
                    R.City_id = new Guid(ddlMasterCity.SelectedValue);
                if (ddlZoneType.SelectedItem.Value != "0")
                    R.Zone_Type = ddlZoneType.SelectedItem.Text;
                if (ddlStatus.SelectedItem.Value != "0")
                    R.Status = ddlStatus.SelectedItem.Text;

                var res = masterSVc.SearchZone(R);

                if (res != null )
                {
                    if (res.Count > 0)
                    {
                        grdZoneSearch.VirtualItemCount = res[0].TotalRecords ?? 0;
                        lblTotalCount.Text = res[0].TotalRecords.ToString();
                    }

                    grdZoneSearch.DataSource = res;
                    grdZoneSearch.PageIndex = pageindex;
                    grdZoneSearch.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                    grdZoneSearch.DataBind();
                }
                else
                {
                    lblTotalCount.Text = string.Empty;
                    grdZoneSearch.DataSource = null;
                    grdZoneSearch.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        protected void ddlMasterCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities(ddlMasterCity, ddlMasterCountry);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillMasterSearchData(0, Convert.ToInt32(ddlShowEntries.SelectedValue));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetControls();
        }
        private void resetControls()
        {
            ddlMasterCountry.SelectedIndex = 0;
            ddlMasterCity.Items.Clear();
            ddlMasterCity.Items.Add(new ListItem("---Select---", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlZoneType.SelectedIndex = 0;
            ddlShowEntries.SelectedIndex = 0;
            lblTotalCount.Text = string.Empty;

            grdZoneSearch.DataSource = null;
            grdZoneSearch.DataBind();
        }

        protected void btnCancelZoneMaster_Click(object sender, EventArgs e)
        {
            ddlMasterCountryAddModal.SelectedIndex = 0;
            ddlMasterCityAddModal.Items.Clear();
            ddlMasterCityAddModal.Items.Add(new ListItem("---Select---", "0"));
           // ddlAddStatus.SelectedIndex = 0;
            ddlZoneType.SelectedIndex = 0;
            txtAddZoneName.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
        }

        protected void btnSaveZoneMaster_Click(object sender, EventArgs e)
        {
            MDMSVC.DC_ZoneRQ param = new MDMSVC.DC_ZoneRQ();
            var Zone_id= Guid.NewGuid();
            param.Action = "ADD";
            param.Zone_Radius = 4.0;
            param.Zone_id = Zone_id;
            param.Create_Date = DateTime.Now;
            param.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
            param.CountryName = ddlMasterCountryAddModal.SelectedItem.Text;
            if (ddlMasterCityAddModal.SelectedItem.Value != "0")
                param.City_id = new Guid(ddlMasterCityAddModal.SelectedValue);

            if (ddlAddZoneType.SelectedItem.Value != "0")
                param.Zone_Type = ddlAddZoneType.SelectedItem.Text;

            if (ddlAddHotelIncludeRange.SelectedItem.Value != "0")
                param.Zone_Radius = Convert.ToDouble(ddlAddHotelIncludeRange.SelectedValue);
            else param.Zone_Radius = 4;

            param.Zone_Name = txtAddZoneName.Text;
            param.Latitude = txtLatitude.Text;
            param.Longitude = txtLongitude.Text;
            var result = masterSVc.AddzoneMaster(param);
            if (result != null)
            {
                if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgAdd, "Zone has been added successfully", BootstrapAlertType.Success);
                    var addHotels = masterSVc.InsertZoneHotelsInTable(param);
                    string strQueryString = GetQueryString(Zone_id.ToString(), "0");
                    Response.Redirect(strQueryString, true);
                }
                   
                else
                    BootstrapAlert.BootstrapAlertMessage(dvmsgAdd, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            }
        }
     
        protected void ddlMasterCountryAddModal_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities(ddlMasterCityAddModal, ddlMasterCountryAddModal);
        }

        protected void grdZoneSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillMasterSearchData(e.NewPageIndex, Convert.ToInt32(ddlShowEntries.SelectedValue));
        }

        protected void grdZoneSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    Guid myRowId = Guid.Parse(e.CommandArgument.ToString());
                    //create Query String
                    string strQueryString = GetQueryString(myRowId.ToString(), ((GridView)sender).PageIndex.ToString());
                    Response.Redirect(strQueryString, true);
                    //end Query string
                }
                if (e.CommandName == "SoftDelete")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    Guid myRowId = Guid.Parse(e.CommandArgument.ToString());
                    MDMSVC.DC_ZoneRQ RQ = new MDMSVC.DC_ZoneRQ();
                    RQ.Zone_id = myRowId;
                    RQ.Action = "ZoneMaster";
                    RQ.Edit_Date = DateTime.Now;
                    RQ.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Status = "Inactive";
                    RQ.IsActive = false;
                    var result = masterSVc.DeactivateOrActivateZones(RQ);
                    if (result != null)
                    {
                        if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                        {
                            fillMasterSearchData(0, Convert.ToInt32(ddlShowEntries.SelectedValue));
                            BootstrapAlert.BootstrapAlertMessage(dvMsgDeleted, "Zone has been deleted successfully", BootstrapAlertType.Success);
                        }
                            
                        else
                            BootstrapAlert.BootstrapAlertMessage(dvMsgDeleted, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                    }
                }
                if (e.CommandName == "UnDelete")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    Guid myRowId = Guid.Parse(e.CommandArgument.ToString());
                    MDMSVC.DC_ZoneRQ p = new MDMSVC.DC_ZoneRQ();
                    p.Zone_id = myRowId;
                    p.Action = "ZoneMaster";
                    p.Status = "Active";
                    p.IsActive = true;
                    p.Edit_Date = DateTime.Now;
                    p.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    var result = masterSVc.DeactivateOrActivateZones(p);
                    if (result != null)
                    {
                        if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                        {
                            fillMasterSearchData(0, Convert.ToInt32(ddlShowEntries.SelectedValue));
                            BootstrapAlert.BootstrapAlertMessage(dvMsgDeleted, "Zone has been Undeleted successfully", BootstrapAlertType.Success);
                        }
                            
                        else
                            BootstrapAlert.BootstrapAlertMessage(dvMsgDeleted, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                    }
                }
            }
            catch(Exception )
            {
                throw;
            }
           
        }

        public string GetQueryString(string myRow_Id, string strpageindex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("~/geography/ZoneCityMasterEdit.aspx?Zone_Id=" + myRow_Id);
            
            if (ddlMasterCountry.SelectedValue !="0")
            {
                sb.Append("&CoID=" + HttpUtility.UrlEncode(ddlMasterCountry.SelectedValue));
                sb.Append("&CoN=" + HttpUtility.UrlEncode(ddlMasterCountry.SelectedItem.Text));
            }
           
            if (ddlMasterCity.SelectedValue != "0")
            {
                sb.Append("&CiID=" + HttpUtility.UrlEncode(ddlMasterCity.SelectedValue));
                sb.Append("&CiN=" + HttpUtility.UrlEncode(ddlMasterCity.SelectedItem.Text));
            }
            
            if (ddlZoneType.SelectedValue != "0")
                sb.Append("&ZoType=" + HttpUtility.UrlEncode(ddlZoneType.SelectedItem.Text));

            if (ddlStatus.SelectedValue != "0")
                sb.Append("&Status=" + HttpUtility.UrlEncode(ddlStatus.SelectedItem.Text));

          
            string pageindex = strpageindex;
            sb.Append("&PN=" + HttpUtility.UrlEncode(pageindex));
            sb.Append("&PS=" + HttpUtility.UrlEncode(Convert.ToString(ddlShowEntries.SelectedValue)));


            return sb.ToString();
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillMasterSearchData(0, Convert.ToInt32(ddlShowEntries.SelectedValue));
        }

        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            ddlMasterCountryAddModal.SelectedIndex = 0;
            ddlMasterCityAddModal.Items.Clear();
            ddlMasterCityAddModal.Items.Add(new ListItem("---Select---", "0"));
            ddlAddZoneType.SelectedIndex = 0;
            txtLatitude.Text = string.Empty;
            txtLongitude.Text = string.Empty;
            txtAddZoneName.Text = string.Empty;
            dvmsgAdd.Style.Add("display", "none");
            dvLatLongMap.Style.Add("display", "none");
        }

        protected void grdZoneSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btndelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }
    }
}