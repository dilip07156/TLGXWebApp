using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.geography
{
    public partial class countryStateMgmt : System.Web.UI.UserControl
    {
        public DataTable dtCityyMaster = new DataTable();
        public DataTable dtStateMaster = new DataTable();
        MasterDataDAL objMasterDataDAL = new MasterDataDAL();
        MasterDataSVCs _objMaster = new MasterDataSVCs();

        public int pagesizeCity = 10;
        public int intpageindexCity = 0;

        public int pagesizeState = 10;
        public int intpageindexState = 0;

        public static string Countryid { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            Countryid = Convert.ToString(Request.QueryString["Country_Id"]);

            if (!IsPostBack)
            {
                if (Countryid != null)
                {
                    string created = Convert.ToString(Request.QueryString["Created"]);
                    if (Convert.ToBoolean(created))
                    {
                        BootstrapAlert.BootstrapAlertMessage(dvMsgCountry, "Country has been added successfully", BootstrapAlertType.Success);
                        hdnFlagCountry.Value = "false";
                    }
                    Guid newGuid = Guid.Parse(Countryid);
                    FillPageData(newGuid);
                }
                else
                {
                }


            }
        }

        private void FillPageData(Guid newGuid)
        {
            try
            {
                fillCountryDetails(newGuid);
                fillgvCityyList(newGuid);
                fillgrdStateList(newGuid);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void fillCountryDetails(Guid newGuid)
        {
            try
            {
                var result = _objMaster.GetCountryMasterData(new MDMSVC.DC_Country_Search_RQ() { Country_Id = newGuid });
                if (result != null)
                {
                    lblCountryName.InnerText = Convert.ToString(result[0].Name);
                    txtCountryName.Text = Convert.ToString(result[0].Name);
                    txtCountryCode.Text = Convert.ToString(result[0].Code);
                    ddlStatus.SelectedValue = Convert.ToString(result[0].Status);
                    txt_ISOofficial_name_en.Text = Convert.ToString(result[0].ISOofficial_name_en);
                    txt_ISO3166_1_Alpha_2.Text = Convert.ToString(result[0].ISO3166_1_Alpha_2);
                    txt_ISO3166_1_Alpha_3.Text = Convert.ToString(result[0].ISO3166_1_Alpha_3);
                    txt_ISO3166_1_M49.Text = Convert.ToString(result[0].ISO3166_1_M49);
                    txt_ISO3166_1_ITU.Text = Convert.ToString(result[0].ISO3166_1_ITU);
                    txt_MARC.Text = Convert.ToString(result[0].MARC);
                    txt_WMO.Text = Convert.ToString(result[0].WMO);
                    txt_DS.Text = Convert.ToString(result[0].DS);
                    txt_Dial.Text = Convert.ToString(result[0].Dial);
                    txt_ISO4217_currency_alphabetic_code.Text = Convert.ToString(result[0].ISO4217_currency_alphabetic_code);
                    txt_ISO4217_currency_country_name.Text = Convert.ToString(result[0].ISO4217_currency_country_name);
                    txt_ISO4217_currency_minor_unit.Text = Convert.ToString(result[0].ISO4217_currency_minor_unit);
                    txt_ISO4217_currency_name.Text = Convert.ToString(result[0].ISO4217_currency_name);
                    txt_ISO4217_currency_numeric_code.Text = Convert.ToString(result[0].ISO4217_currency_numeric_code);
                    txt_ISO3166_1_Capital.Text = Convert.ToString(result[0].ISO3166_1_Capital);
                    txt_ISO3166_1_Continent.Text = Convert.ToString(result[0].ISO3166_1_Continent);
                    txt_ISO3166_1_TLD.Text = Convert.ToString(result[0].ISO3166_1_TLD);
                    txt_ISO3166_1_Languages.Text = Convert.ToString(result[0].ISO3166_1_Languages);
                    txt_ISO3166_1_Geoname_ID.Text = Convert.ToString(result[0].ISO3166_1_Geoname_ID);
                    txt_ISO3166_1_EDGAR.Text = Convert.ToString(result[0].ISO3166_1_EDGAR);
                    txt_GooglePlaceID.Text = Convert.ToString(result[0].GooglePlaceID);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void fillgvCityyList(Guid CountryID)
        {
            try
            {
                var result = _objMaster.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { Country_Id = CountryID, PageNo = intpageindexCity, PageSize = pagesizeCity });
                grdCityList.PageIndex = intpageindexCity;
                grdCityList.PageSize = pagesizeCity;
                grdCityList.DataSource = result;
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        grdCityList.VirtualItemCount = result[0].TotalRecords;
                    }
                }
                grdCityList.DataBind();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void fillgrdStateList(Guid CountryID)
        {

            try
            {
                var result = _objMaster.GetStatesMaster(new MDMSVC.DC_State_Search_RQ() { Country_Id = CountryID, PageNo = intpageindexState, PageSize = pagesizeState });
                grdStateList.PageIndex = intpageindexState;
                grdStateList.DataSource = result;
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        grdStateList.PageSize = pagesizeState;
                        grdStateList.VirtualItemCount = result[0].TotalRecords;
                    }
                }

                grdStateList.DataBind();
            }
            catch (Exception)
            {

                throw;
            }




        }

        protected void frmStateUpdate_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            #region Get Controls 
            Guid myCountryRow_Id = Guid.Parse(Countryid);
            TextBox txtStateName = (TextBox)frmStateUpdate.FindControl("txtStateName");
            TextBox txtStateCode = (TextBox)frmStateUpdate.FindControl("txtStateCode");
            TextBox txtStateLocalName = (TextBox)frmStateUpdate.FindControl("txtLocalLanguage");

            #endregion

            if (e.CommandName.ToString() == "Save")
            {
                Guid myStateRow_Id = Guid.Parse(grdStateList.SelectedDataKey.Value.ToString());
                MDMSVC.DC_Master_State _objstateMaster = new DC_Master_State();
                _objstateMaster.State_Id = myStateRow_Id;
                _objstateMaster.State_Name = txtStateName.Text.Trim();
                _objstateMaster.State_Code = txtStateCode.Text.Trim();
                _objstateMaster.StateName_LocalLanguage = txtStateLocalName.Text.Trim();
                _objstateMaster.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                _objstateMaster.Edit_Date = DateTime.Now;
                _objstateMaster.Country_Id = Guid.Parse(Countryid);
                DC_Message _msg = new DC_Message();
                _msg = _objMaster.UpdateStatesMaster(_objstateMaster);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    frmStateUpdate.ChangeMode(FormViewMode.Insert);
                    frmStateUpdate.DataBind();
                    hdnFlagState.Value = "true";
                    BootstrapAlert.BootstrapAlertMessage(dvMsgState, "State Details has been updated successfully", BootstrapAlertType.Success);
                    fillgrdStateList(Guid.Parse(Countryid));
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(msgAlertState, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
            else if (e.CommandName == "Add")
            {
                MDMSVC.DC_Master_State _objstateMaster = new DC_Master_State();
                _objstateMaster.State_Name = txtStateName.Text.Trim();
                _objstateMaster.State_Code = txtStateCode.Text.Trim();
                _objstateMaster.StateName_LocalLanguage = txtStateLocalName.Text.Trim();
                _objstateMaster.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                _objstateMaster.Create_Date = DateTime.Now;
                _objstateMaster.Country_Id = Guid.Parse(Countryid);
                DC_Message _msg = _objMaster.AddStatesMaster(_objstateMaster);
                if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
                {
                    frmStateUpdate.ChangeMode(FormViewMode.Insert);
                    frmStateUpdate.DataBind();
                    hdnFlagState.Value = "true";
                    BootstrapAlert.BootstrapAlertMessage(dvMsgState, "State Details has been added successfully", BootstrapAlertType.Success);
                    fillgrdStateList(Guid.Parse(Countryid));

                }
                else
                    BootstrapAlert.BootstrapAlertMessage(msgAlertState, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }
        }

        protected void grdStateList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                frmStateUpdate.ChangeMode(FormViewMode.Edit);
                var resultState = _objMaster.GetStatesMaster(new DC_State_Search_RQ() { State_Id = myRow_Id });
                frmStateUpdate.DataSource = resultState;
                frmStateUpdate.DataBind();
                //Getting controls
                TextBox txtStateName = (TextBox)frmStateUpdate.FindControl("txtStateName");
                TextBox txtStateCode = (TextBox)frmStateUpdate.FindControl("txtStateCode");
                TextBox txtStateLocalName = (TextBox)frmStateUpdate.FindControl("txtLocalLanguage");

                if (resultState != null)
                {
                    txtStateName.Text = (resultState[0].State_Name);
                    txtStateCode.Text = (resultState[0].State_Code);
                    txtStateLocalName.Text = (resultState[0].StateName_LocalLanguage);
                }
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "javascript:showCountryMappingModal();", true);
        }

        protected void frmvwCity_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            #region Get Controls 
            Guid myCountryRow_Id = Guid.Parse(Countryid);
            TextBox txtCityName = (TextBox)frmvwCity.FindControl("txtCityName");
            TextBox txtCityCode = (TextBox)frmvwCity.FindControl("txtCityCode");
            DropDownList ddlState = (DropDownList)frmvwCity.FindControl("ddlStates");
            DropDownList ddlStatus = (DropDownList)frmvwCity.FindControl("ddlStatus");


            #endregion

            if (e.CommandName.ToString() == "Save")
            {
                Guid myCityRow_Id = Guid.Parse(grdStateList.SelectedDataKey.Value.ToString());
                var status = Convert.ToString(ddlStatus.SelectedValue);
                var stateid = Guid.Parse(Convert.ToString(ddlState.SelectedValue).Trim());

                MDMSVC.DC_City _objCityMaster = new DC_City();
                _objCityMaster.City_Id = myCityRow_Id;
                _objCityMaster.Name = Convert.ToString(txtCityName.Text).Trim();
                _objCityMaster.Code = Convert.ToString(txtCityCode.Text).Trim();
                _objCityMaster.State_Id = stateid;
                //Get Country Details
                var resultCountry = _objMaster.GetCountryMasterData(new DC_Country_Search_RQ() { Country_Id = Guid.Parse(Countryid) });
                if (resultCountry != null)
                {
                    _objCityMaster.Country_Id = Guid.Parse(Countryid);
                    _objCityMaster.CountryName = Convert.ToString(resultCountry[0].Name).Trim();
                    _objCityMaster.Google_PlaceId = Convert.ToString(resultCountry[0].GooglePlaceID).Trim();
                }
                //Get State detils
                var resultState = _objMaster.GetStatesMaster(new DC_State_Search_RQ() { State_Id = stateid });
                if (resultState != null)
                {
                    _objCityMaster.State_Id = stateid;
                    _objCityMaster.StateName = Convert.ToString(resultState[0].State_Name).Trim();
                    _objCityMaster.StateCode = Convert.ToString(resultState[0].State_Code).Trim();
                }
                _objCityMaster.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                _objCityMaster.Edit_Date = DateTime.Now;
                DC_Message _msg = _objMaster.AddCityMasterData(_objCityMaster);
                if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
                {
                    frmStateUpdate.ChangeMode(FormViewMode.Insert);
                    frmStateUpdate.DataBind();
                    fillgvCityyList(Guid.Parse(Countryid));
                    hdnFlagCity.Value = "true";
                    BootstrapAlert.BootstrapAlertMessage(dvMsgCountry, _msg.StatusMessage, BootstrapAlertType.Success);

                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgCountry, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
            else if (e.CommandName == "Add")
            {
                var stateid = Guid.Parse(Convert.ToString(ddlState.SelectedValue).Trim());
                var status = Convert.ToString(ddlStatus.SelectedValue);
                MDMSVC.DC_City _objCityMaster = new DC_City();
                _objCityMaster.City_Id = Guid.NewGuid();
                _objCityMaster.Name = Convert.ToString(txtCityName.Text).Trim();
                _objCityMaster.Code = Convert.ToString(txtCityCode.Text).Trim();
                _objCityMaster.State_Id = stateid;
                //Get Country Details
                var resultCountry = _objMaster.GetCountryMasterData(new DC_Country_Search_RQ() { Country_Id = Guid.Parse(Countryid) });
                if (resultCountry != null)
                {
                    _objCityMaster.Country_Id = Guid.Parse(Countryid);
                    _objCityMaster.CountryName = Convert.ToString(resultCountry[0].Name).Trim();
                    _objCityMaster.Google_PlaceId = Convert.ToString(resultCountry[0].GooglePlaceID);
                }
                //Get State detils
                var resultState = _objMaster.GetStatesMaster(new DC_State_Search_RQ() { State_Id = stateid });
                if (resultState != null)
                {
                    _objCityMaster.State_Id = stateid;
                    _objCityMaster.StateName = Convert.ToString(resultState[0].State_Name).Trim();
                    _objCityMaster.StateCode = Convert.ToString(resultState[0].State_Code).Trim();
                }
                _objCityMaster.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                _objCityMaster.Create_Date = DateTime.Now;
                _objCityMaster.Status = status;
                DC_Message _msg = _objMaster.AddCityMasterData(_objCityMaster);
                if (_msg.StatusCode == ReadOnlyMessageStatusCode.Success)
                {
                    frmStateUpdate.ChangeMode(FormViewMode.Insert);
                    frmStateUpdate.DataBind();
                    fillgvCityyList(Guid.Parse(Countryid));
                    hdnFlagCity.Value = "true";
                    BootstrapAlert.BootstrapAlertMessage(dvMsgCity, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgCityAlert, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }

        protected void grdStateList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intpageindexState = Convert.ToInt32(e.NewPageIndex);
            pagesizeState = Convert.ToInt32(ddlShowEntriesState.SelectedValue);
            fillgrdStateList(Guid.Parse(Countryid));
        }

        protected void grdCityList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intpageindexCity = Convert.ToInt32(e.NewPageIndex);
            pagesizeCity = Convert.ToInt32(ddlShowEntriesCity.SelectedValue);

            fillgvCityyList(Guid.Parse(Countryid));
        }

        protected void btnUpdateCountry_Click(object sender, EventArgs e)
        {
            #region Fetching Data from screen to update country
            DC_Country _objCountry = new DC_Country();
            _objCountry.Create_Date = DateTime.Now;
            _objCountry.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
            _objCountry.Country_Id = Guid.Parse(Countryid);
            _objCountry.Name = Convert.ToString(txtCountryName.Text);
            _objCountry.Code = Convert.ToString(txtCountryCode.Text);
            _objCountry.Status = Convert.ToString(ddlStatus.SelectedValue);
            _objCountry.ISOofficial_name_en = Convert.ToString(txt_ISOofficial_name_en.Text);
            _objCountry.ISO3166_1_Alpha_2 = Convert.ToString(txt_ISO3166_1_Alpha_2.Text);
            _objCountry.ISO3166_1_Alpha_3 = Convert.ToString(txt_ISO3166_1_Alpha_3.Text);
            _objCountry.ISO3166_1_M49 = Convert.ToString(txt_ISO3166_1_M49.Text);
            _objCountry.ISO3166_1_ITU = Convert.ToString(txt_ISO3166_1_ITU.Text);
            _objCountry.MARC = Convert.ToString(txt_MARC.Text);
            _objCountry.WMO = Convert.ToString(txt_WMO.Text);
            _objCountry.DS = Convert.ToString(txt_DS.Text);
            _objCountry.Dial = Convert.ToString(txt_Dial.Text);
            _objCountry.ISO4217_currency_alphabetic_code = Convert.ToString(txt_ISO4217_currency_alphabetic_code.Text);
            _objCountry.ISO4217_currency_country_name = Convert.ToString(txt_ISO4217_currency_country_name.Text);
            _objCountry.ISO4217_currency_minor_unit = Convert.ToString(txt_ISO4217_currency_minor_unit.Text);
            _objCountry.ISO4217_currency_name = Convert.ToString(txt_ISO4217_currency_name.Text);
            _objCountry.ISO4217_currency_numeric_code = Convert.ToString(txt_ISO4217_currency_numeric_code.Text);
            _objCountry.ISO3166_1_Capital = Convert.ToString(txt_ISO3166_1_Capital.Text);
            _objCountry.ISO3166_1_Continent = Convert.ToString(txt_ISO3166_1_Continent.Text);
            _objCountry.ISO3166_1_TLD = Convert.ToString(txt_ISO3166_1_TLD.Text);
            _objCountry.ISO3166_1_Languages = Convert.ToString(txt_ISO3166_1_Languages.Text);
            _objCountry.ISO3166_1_Geoname_ID = Convert.ToString(txt_ISO3166_1_Geoname_ID.Text);
            _objCountry.ISO3166_1_EDGAR = Convert.ToString(txt_ISO3166_1_EDGAR.Text);
            _objCountry.GooglePlaceID = Convert.ToString(txt_GooglePlaceID.Text);
            #endregion

            _objCountry.Edit_Date = DateTime.Now;
            _objCountry.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
            _objCountry.Action = "Update";
            MasterDataSVCs _objMaster = new MasterDataSVCs();
            DC_Message _msg = new DC_Message();
            _msg = _objMaster.AddUpdateCountryMaster(_objCountry);
            if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsgCountry, "Country has been updated successfully", BootstrapAlertType.Success);
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsgCountry, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }


        }

        protected void btnNewCity_Click(object sender, EventArgs e)
        {
            hdnFlagCity.Value = "false";
            frmvwCity.ChangeMode(FormViewMode.Insert);
            frmvwCity.DataBind();
            fillStateList();
        }

        private void fillStateList()
        {
            try
            {
                DC_Roles _objRole = new DC_Roles();
                DropDownList ddlStates = (DropDownList)frmvwCity.FindControl("ddlStates");
                var result = _objMaster.GetStatesByCountry(Convert.ToString(Countryid));
                //DataTable roles = Models.ConversionClass.CreateDataTable(roleManager.Roles.ToList());
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

        protected void btnNewState_Click(object sender, EventArgs e)
        {
            frmStateUpdate.ChangeMode(FormViewMode.Insert);
            frmStateUpdate.DataBind();
        }

        protected void ddlShowEntriesState_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagesizeState = Convert.ToInt32(ddlShowEntriesState.SelectedValue);
            fillgrdStateList(Guid.Parse(Countryid));

        }

        protected void ddlShowEntriesCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagesizeCity = Convert.ToInt32(ddlShowEntriesCity.SelectedValue);
            fillgvCityyList(Guid.Parse(Countryid));
        }
    }
}