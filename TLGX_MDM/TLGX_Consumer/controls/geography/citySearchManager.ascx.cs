using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TLGX_Consumer;
using TLGX_Consumer.Models;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;
using System.Text;
using System.Configuration;

namespace TLGX_Consumer.controls.geography
{
    public partial class citySearchManager : System.Web.UI.UserControl
    {
        public DataTable dtCityDetail = new DataTable();                // for use in the main form view


        public DataTable dtCityArea = new DataTable();                   // for use in the CityArea grid
        public DataTable dtCityAreaDetail = new DataTable();            // for use in the CityArea grid

        public DataTable dtCityAreaLocation = new DataTable();
        public DataTable dtCityAreaLocationDetail = new DataTable();

        public DataTable dtCountryMaster = new DataTable();
        public DataTable dtCityyMaster = new DataTable();
        MasterDataSVCs _objMasterData = new MasterDataSVCs();
        public int intPageSize = 0;
        public Guid CountryID = Guid.Empty;
        public static Guid refCountryId = Guid.Empty;



        MasterDataDAL objMasterDataDAL = new MasterDataDAL();

        public Guid? CityState_id { get; set; }

        private void fillgvCountryList()
        {
            var result = _objMasterData.GetAllCountries();
            ddlCountry.DataSource = result;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
        }

        private void fillgvCityyList(Guid CountryID, int pageindex)
        {
            intPageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            string CityName = string.Empty;
            string _Key = string.Empty;
            string _Rank = string.Empty;
            string _Priority = string.Empty;

            if (!string.IsNullOrWhiteSpace(txtCityName.Text))
            {
                CityName = txtCityName.Text;
            }

            if (!string.IsNullOrWhiteSpace(ddlKey.SelectedValue.Replace("0", ""))) { _Key = ddlKey.SelectedValue; }

            if (!string.IsNullOrWhiteSpace(ddlRank.SelectedValue.Replace("0", ""))) { _Rank = ddlRank.SelectedValue; }

            if (!string.IsNullOrWhiteSpace(ddlPriority.SelectedValue.Replace("0", ""))) { _Priority = ddlPriority.SelectedValue; }

            var result = _objMasterData.GetCityMasterData(new MDMSVC.DC_City_Search_RQ()
            { Country_Id = CountryID, City_Name = CityName, Key = _Key, Rank = _Rank, Priority = _Priority, Status = "ACTIVE", PageNo = pageindex, PageSize = intPageSize });

            grdCityList.DataSource = result;
            if (result != null && result.Count > 0)
            {
                cityResult.Style.Add(HtmlTextWriterStyle.Display, "block");
                grdCityList.VirtualItemCount = result[0].TotalRecords;
            }
            grdCityList.PageIndex = pageindex;
            grdCityList.PageSize = intPageSize;
            grdCityList.DataBind();
        }




        //used tofill the header city detail form
        //private void fillCityForm(string City_Id, ref Guid refcountryID)
        //{

        //    var result = _objMasterData.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { City_Id = Guid.Parse(City_Id) });
        //    if (result != null && result.Count > 0)
        //    {
        //        refcountryID = result[0].Country_Id;
        //        CityState_id = result[0].State_Id;
        //    }
        //    frmCityMaster.DataSource = result;
        //    frmCityMaster.DataBind();
        //}

        //// used to fill the city area read form
        //private void fillCityAreaForm(string CityArea_Id)
        //{

        //    var result = _objMasterData.GetMasterCityAreaDetail(CityArea_Id);
        //    frmCityArea.ChangeMode(FormViewMode.Edit);
        //    frmCityArea.DataSource = result;
        //    frmCityArea.DataBind();

        //}

        //private void fillCityAreaLocationDetailForm(string CityAreaLocation_Id)
        //{

        //    var result = _objMasterData.GetMasterCityAreaLocationDetail(CityAreaLocation_Id);
        //    frmCityAreaLocation.ChangeMode(FormViewMode.Edit);
        //    frmCityAreaLocation.DataSource = result;
        //    frmCityAreaLocation.DataBind();


        //}

        //// used to fill city area grid
        //private void fillCityArea(string City_Id)
        //{
        //    var result = _objMasterData.GetMasterCityAreaData(City_Id);
        //    grdCityAreas.DataSource = result;
        //    grdCityAreas.DataBind();
        //}

        //private void fillCityAreaLocation(string CityArea_Id)
        //{

        //    var result = _objMasterData.GetMasterCityAreaLocationData(CityArea_Id);
        //    grdCityAreaLocation.DataSource = result;
        //    grdCityAreaLocation.DataBind();
        //}

        //private void fillStateByCountryId(string Country_Id)
        //{
        //    DropDownList ddlState = (DropDownList)frmCityMaster.FindControl("ddlState");

        //    var result = _objMasterData.GetStatesByCountry(Country_Id);
        //    ddlState.DataSource = result;
        //    ddlState.DataTextField = "State_Name";
        //    ddlState.DataValueField = "State_Id";
        //    ddlState.DataBind();
        //    if (CityState_id != null)
        //        ddlState.Items.FindByValue(Convert.ToString(CityState_id)).Selected = true;
        //    fillStateCode();
        //}
        private void fillStateList()
        {
            try
            {
                DropDownList ddlStates = (DropDownList)frmvwCity.FindControl("ddlStates");

                var result = _objMasterData.GetStatesByCountry(ddlCountry.SelectedValue);
                ddlStates.DataSource = result;
                ddlStates.DataTextField = "State_Name";
                ddlStates.DataValueField = "State_Id";
                ddlStates.DataBind();
                ddlStates.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = "0" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        //private void fillStateCode()
        //{
        //    DropDownList ddlState = (DropDownList)frmCityMaster.FindControl("ddlState");
        //    TextBox txtStateCode = (TextBox)frmCityMaster.FindControl("txtStateCode");


        //    var result = _objMasterData.GetStateNameAndCode(new MDMSVC.DC_State_Master_DDL_RQ
        //    {
        //        State_ID = Convert.ToString(ddlState.SelectedValue)
        //    });
        //    if (result != null)
        //    {
        //        txtStateCode.Text = Convert.ToString(result.StateCode);
        //    }
        //}



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri.Contains("city"))
                {
                    fillgvCountryList();
                    SetControls();
                }
                else
                {
                    fillgvCountryList();
                }


                //Bind Page Controls


            }
            else
            {

                dvMsgCity2.Visible = false;
            }
        }

        /// <summary>
        /// Added by / On / TMAP ID : Varun Phadke / 8 Oct 2018 / T691
        /// Desc     : 3 Additionsl filters for Key, Rank, Priority
        /// </summary>
        private void fillDropdowns()
        {
            ddlKey.Items.Clear(); ddlRank.Items.Clear(); ddlPriority.Items.Clear();

            ddlKey.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlRank.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlPriority.Items.Insert(0, new ListItem("---ALL---", "0"));


            CountryID = Guid.Parse(Convert.ToString(ddlCountry.SelectedValue));

            var result = _objMasterData.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { Country_Id = CountryID, City_Name = "", Status = "ACTIVE", PageNo = 0, PageSize = 5000 });
            divEntries.Style.Add(HtmlTextWriterStyle.Display, "block");

            ddlKey.DataSource = result.Select(o => new { o.Key }).Where(y => y.Key != null && y.Key != "").Distinct();
            ddlKey.DataValueField = "Key";
            ddlKey.DataTextField = "Key";
            ddlKey.DataBind();

            ddlRank.DataSource = result.Select(o => new { o.Rank }).Where(y => y.Rank != null && y.Rank != "").Distinct();
            ddlRank.DataValueField = "Rank";
            ddlRank.DataTextField = "Rank";
            ddlRank.DataBind();

            ddlPriority.DataSource = result.Select(o => new { o.Priority }).Where(y => y.Priority != null && y.Priority != "").Distinct();
            ddlPriority.DataValueField = "Priority";
            ddlPriority.DataTextField = "Priority";
            ddlPriority.DataBind();


        }

        private void SetControls()
        {


            try
            {
                //if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.UrlReferrer.AbsoluteUri.Contains("ManageActivityFlavour"))))
                //{
                #region Get Value from query string
                //string ProductName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ProdN"]);
                string CountryName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CoN"]);
                string CountryID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CoID"]);
                string CityName = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CiN"]);
                string CityID = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["City_Id"]);
                string PageNo = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PN"]);
                string PageSize = Convert.ToString(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["PS"]);

                #endregion
                int pageno = 0;
                int pagesize = 5;

                if (!string.IsNullOrWhiteSpace(CityName))
                    txtCityName.Text = CityName;

                if (!string.IsNullOrWhiteSpace(PageNo))
                    pageno = Convert.ToInt32(PageNo);
                if (!string.IsNullOrWhiteSpace(PageSize))
                    pagesize = Convert.ToInt32(PageSize);


                ddlCountry.SelectedValue = CountryID;
                ddlShowEntries.SelectedValue = Convert.ToString(pagesize);
                //searchActivityMaster(pageno, pagesize);
                fillgvCityyList(Guid.Parse(CountryID), pageno);
                fillDropdowns();
                if (ddlCountry.SelectedIndex != 0)
                    btnNewCity.Visible = true;
                else
                    btnNewCity.Visible = false;

                //}
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        protected void btnGetCities_Click(object sender, EventArgs e)
        {
            CountryID = Guid.Parse(Convert.ToString(ddlCountry.SelectedValue));
            fillgvCityyList(CountryID, 0);
            if (ddlCountry.SelectedIndex != 0)
                btnNewCity.Visible = true;
            else
                btnNewCity.Visible = false;
        }

        protected void grdCityList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CountryID = new Guid(Convert.ToString(ddlCountry.SelectedValue));
            fillgvCityyList(CountryID, e.NewPageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountryID = Guid.Parse(Convert.ToString(ddlCountry.SelectedValue));
            fillgvCityyList(CountryID, 0);
            dvMsgCity2.Visible = false;
        }



        protected void btnNewCity_Click(object sender, EventArgs e)
        {
            fillStateList();
        }

        protected void frmvwCity_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtCityName = (TextBox)frmvwCity.FindControl("txtCityName");
            TextBox txtCityCode = (TextBox)frmvwCity.FindControl("txtCityCode");
            TextBox txtKey = (TextBox)frmvwCity.FindControl("txtKey");
            TextBox txtRank = (TextBox)frmvwCity.FindControl("txtRank");
            TextBox txtPriority = (TextBox)frmvwCity.FindControl("txtPriority");

            DropDownList ddlStates = (DropDownList)frmvwCity.FindControl("ddlStates");
            DropDownList ddlStatus = (DropDownList)frmvwCity.FindControl("ddlStatus");
            Guid ddlCountry_Id_Value = Guid.Parse(ddlCountry.SelectedValue);

            if (Convert.ToString(e.CommandName) == "AddCity")
            {
                var stateid = Guid.Parse(Convert.ToString(ddlStates.SelectedValue).Trim());
                var status = Convert.ToString(ddlStatus.SelectedValue);
                MDMSVC.DC_City _objCityMaster = new MDMSVC.DC_City();
                _objCityMaster.City_Id = Guid.NewGuid();
                _objCityMaster.Name = Convert.ToString(txtCityName.Text).Trim();
                _objCityMaster.Code = Convert.ToString(txtCityCode.Text).Trim();
                _objCityMaster.Key = Convert.ToString(txtKey.Text).Trim();
                _objCityMaster.Rank = Convert.ToString(txtRank.Text).Trim();
                _objCityMaster.Priority = Convert.ToString(txtPriority.Text).Trim();

                _objCityMaster.State_Id = stateid;
                //Get Country Details
                var resultCountry = _objMasterData.GetCountryMasterData(new MDMSVC.DC_Country_Search_RQ() { Country_Id = ddlCountry_Id_Value });
                if (resultCountry != null)
                {
                    _objCityMaster.Country_Id = ddlCountry_Id_Value;
                    _objCityMaster.CountryCode = resultCountry[0].Code;
                    _objCityMaster.CountryName = Convert.ToString(resultCountry[0].Name).Trim();
                    _objCityMaster.Google_PlaceId = Convert.ToString(resultCountry[0].GooglePlaceID);
                }
                //Get State detils
                var resultState = _objMasterData.GetStatesMaster(new MDMSVC.DC_State_Search_RQ() { State_Id = stateid });
                if (resultState != null)
                {
                    _objCityMaster.State_Id = stateid;
                    _objCityMaster.StateName = Convert.ToString(resultState[0].State_Name).Trim();
                    _objCityMaster.StateCode = Convert.ToString(resultState[0].State_Code).Trim();
                }
                _objCityMaster.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                _objCityMaster.Create_Date = DateTime.Now;
                _objCityMaster.Status = status;
                MDMSVC.DC_Message _msg = _objMasterData.AddCityMasterData(_objCityMaster);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    fillgvCityyList(ddlCountry_Id_Value, 0);
                    hdnFlagCity.Value = "true";
                    dvMsgCity2.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsgCity2, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    dvMsgCity2.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvmsgCityAlert, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
            txtCityName.Text = String.Empty;
            txtCityCode.Text = String.Empty;

            txtKey.Text = string.Empty;
            txtRank.Text = string.Empty;
            txtPriority.Text = string.Empty;

            ddlStates.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }

        protected void grdCityList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    //Create Query string

                    string strQueryString = GetQueryString(myRow_Id.ToString(), ((GridView)sender).PageIndex.ToString());
                    Response.Redirect(strQueryString, true);
                    //End Here


                }
            }
            catch (Exception)
            {


                throw;
            }
        }

        public string GetQueryString(string myRow_Id, string strpageindex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("~/geography/city?City_Id=" + myRow_Id);
            if (!string.IsNullOrWhiteSpace(txtCityName.Text))
                sb.Append("&CiN=" + HttpUtility.UrlEncode(txtCityName.Text));
            if (ddlCountry.SelectedIndex > 1)
            {
                sb.Append("&CoID=" + HttpUtility.UrlEncode(ddlCountry.SelectedValue));
                sb.Append("&CoN=" + HttpUtility.UrlEncode(ddlCountry.SelectedItem.Text));
            }
            else
            {
                if (ddlCountry.SelectedItem.Text.Contains("UNMAPPED"))
                    sb.Append("&CoN=" + HttpUtility.UrlEncode(ddlCountry.SelectedItem.Text));
            }


            string pageindex = strpageindex;
            sb.Append("&PN=" + HttpUtility.UrlEncode(pageindex));
            sb.Append("&PS=" + HttpUtility.UrlEncode(Convert.ToString(ddlShowEntries.SelectedValue)));


            return sb.ToString();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDropdowns();
        }
    }
}