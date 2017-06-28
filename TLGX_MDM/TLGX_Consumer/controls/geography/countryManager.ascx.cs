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
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.geography
{
    public partial class countrymapper : System.Web.UI.UserControl
    {
        #region Variable
        public DataSet dsMasters = new DataSet();
        public DataTable dtCountryMaster = new DataTable();
        public DataTable dtCityyMaster = new DataTable();
        public DataTable dtStateMaster = new DataTable();
        MasterDataDAL objMasterDataDAL = new MasterDataDAL();
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        public int PageSize = 10;
        public int intPageIndex = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dsMasters = objMasterDataDAL.GetMasterData();
                fillgvCountryList();

            }
        }

        private void fillgvCountryList()
        {
            DC_Country_Search_RQ _objSearch = new DC_Country_Search_RQ();
            _objSearch.PageNo = intPageIndex;
            _objSearch.PageSize = PageSize;
            var result = _objMaster.GetCountryMasterData(_objSearch);
            divEntries.Style.Add(HtmlTextWriterStyle.Display, "block");
            grdCurrentCountryList.DataSource = result;
            grdCurrentCountryList.PageIndex = intPageIndex;
            grdCurrentCountryList.PageSize = PageSize;
            if (result != null)
            {
                if (result.Count > 0)
                {
                    grdCurrentCountryList.VirtualItemCount = result[0].TotalRecords;
                    if (result[0].TotalRecords < 11)
                        divEntries.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
                else
                    divEntries.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            grdCurrentCountryList.DataBind();
        }

        //private void fillgrdStateList(Guid CountryID, int pageindex)
        //{
        //    dtStateMaster = objMasterDataDAL.GetMasterStateData(CountryID);
        //  //  grdStateList.PageIndex = pageindex;
        //    grdStateList.DataSource = dtStateMaster;
        //    grdStateList.DataBind();


        //    ddlStates.DataSource = dtStateMaster;
        //    ddlStates.DataTextField = "StateName";
        //    ddlStates.DataValueField = "State_Id";
        //    ddlStates.DataBind();


        //}

        //private void fillgvCityyList(Guid CountryID, int pageindex)
        //{
        //    dtCityyMaster = objMasterDataDAL.GetMasterCityData(CountryID);
        //    grdCityList.PageIndex = pageindex;
        //    grdCityList.DataSource = dtCityyMaster;
        //    grdCityList.DataBind();
        //}

        //protected void grdCountryList_SelectedIndexChanged(object sender,  EventArgs e)
        //{
        //    Guid newGuid = new Guid(grdCountryList.SelectedDataKey.Value.ToString());
        //    dvChildtables.Visible=true;
        //    fillgvCityyList(newGuid, 0);
        //    fillgrdStateList(newGuid, 0);

        //    supplierCountryMapping.Country_Id = newGuid;
        //    supplierCountryMapping.SupplierCountryMappingMode = MasterDataDAL.SupplierDataMode.AllSupplierSingleCountry;
        //    supplierCountryMapping.bindSupplierCountryMapping(0);

        //}

        //protected void grdCountryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    intPageIndex = Convert.ToInt32(e.NewPageIndex);
        //    fillgvCountryList();
        //}

        //protected void grdStateList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    Guid newGuid = new Guid(grdCountryList.SelectedDataKey.Value.ToString());
        //    fillgrdStateList(newGuid,e.NewPageIndex);
        //}

        //protected void grdCityList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    Guid newGuid = new Guid(grdCountryList.SelectedDataKey.Value.ToString());
        //    fillgvCityyList(newGuid, e.NewPageIndex);
        //}

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // filters search results list based on text field
            DC_Country_Search_RQ _objSearch = new DC_Country_Search_RQ();
            _objSearch.Country_Name = txtCountryNameSearch.Text.Trim();
            _objSearch.PageNo = intPageIndex;
            _objSearch.PageSize = PageSize;
            grdCurrentCountryList.PageIndex = intPageIndex;
            var result = _objMaster.GetCountryMasterData(_objSearch);
            divEntries.Style.Add(HtmlTextWriterStyle.Display, "block");
            grdCurrentCountryList.DataSource = result;
            grdCurrentCountryList.PageIndex = intPageIndex;
            grdCurrentCountryList.PageSize = PageSize;
            if (result != null)
            {
                if (result.Count > 0)
                {
                    grdCurrentCountryList.VirtualItemCount = result[0].TotalRecords;
                    if (result[0].TotalRecords < 11)
                        divEntries.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
                else
                    divEntries.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            grdCurrentCountryList.DataBind();

        }

        protected void grdCurrentCountryList_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void grdCurrentCountryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            fillgvCountryList();
        }

        protected void frmCountrydetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DC_Message _msg = new DC_Message();
            try
            {

                //Getting all controls here
                TextBox txtCountryName = (TextBox)frmCountrydetail.FindControl("txtCountryName");
                TextBox txtCountryCode = (TextBox)frmCountrydetail.FindControl("txtCountryCode");
                DropDownList ddlStatus = (DropDownList)frmCountrydetail.FindControl("ddlStatus");

                TextBox txt_ISOofficial_name_en = (TextBox)frmCountrydetail.FindControl("txt_ISOofficial_name_en");
                TextBox txt_ISO3166_1_Alpha_2 = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_Alpha_2");
                TextBox txt_ISO3166_1_Alpha_3 = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_Alpha_3");
                TextBox txt_ISO3166_1_M49 = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_M49");
                TextBox txt_ISO3166_1_ITU = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_ITU");

                TextBox txt_MARC = (TextBox)frmCountrydetail.FindControl("txt_MARC");
                TextBox txt_WMO = (TextBox)frmCountrydetail.FindControl("txt_WMO");
                TextBox txt_DS = (TextBox)frmCountrydetail.FindControl("txt_DS");
                TextBox txt_Dial = (TextBox)frmCountrydetail.FindControl("txt_Dial");

                TextBox txt_ISO4217_currency_alphabetic_code = (TextBox)frmCountrydetail.FindControl("txt_ISO4217_currency_alphabetic_code");
                TextBox txt_ISO4217_currency_country_name = (TextBox)frmCountrydetail.FindControl("txt_ISO4217_currency_country_name");
                TextBox txt_ISO4217_currency_minor_unit = (TextBox)frmCountrydetail.FindControl("txt_ISO4217_currency_minor_unit");
                TextBox txt_ISO4217_currency_name = (TextBox)frmCountrydetail.FindControl("txt_ISO4217_currency_name");
                TextBox txt_ISO4217_currency_numeric_code = (TextBox)frmCountrydetail.FindControl("txt_ISO4217_currency_numeric_code");
                TextBox txt_ISO3166_1_Capital = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_Capital");
                TextBox txt_ISO3166_1_Continent = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_Continent");
                TextBox txt_ISO3166_1_TLD = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_TLD");
                TextBox txt_ISO3166_1_Languages = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_Languages");
                TextBox txt_ISO3166_1_Geoname_ID = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_Geoname_ID");
                TextBox txt_ISO3166_1_EDGAR = (TextBox)frmCountrydetail.FindControl("txt_ISO3166_1_EDGAR");
                TextBox txt_GooglePlaceID = (TextBox)frmCountrydetail.FindControl("txt_GooglePlaceID");
                Guid _countryid = Guid.NewGuid();
                if (e.CommandName.ToString() == "Add")
                {
                    DC_Country _objCountry = new DC_Country();
                    _objCountry.Create_Date = DateTime.Now;
                    _objCountry.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                    _objCountry.Country_Id = _countryid;
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
                    _objCountry.Action = "Save";
                    _msg = _objMaster.AddUpdateCountryMaster(_objCountry);
                    if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                    {
                        Response.Redirect("~/geography/CountryStateMgmt?Country_Id=" + _countryid + "&Created=true");
                    }
                    else
                    {
                        BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode); 
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            fillgvCountryList();
        }
    }

}
