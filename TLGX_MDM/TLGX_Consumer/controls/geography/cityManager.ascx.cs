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

namespace TLGX_Consumer.controls.geography
{
    public partial class cityMapper : System.Web.UI.UserControl
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
            //dtCountryMaster = objMasterDataDAL.GetMasterCountryData("");
            //ddlCountry.DataSource = dtCountryMaster;
            //ddlCountry.DataTextField = "Name";
            //ddlCountry.DataValueField = "Country_Id";

            var result = _objMasterData.GetAllCountries();
            ddlCountry.DataSource = result;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
        }

        private void fillgvCityyList(Guid CountryID, int pageindex)
        {
            //dtCityyMaster = objMasterDataDAL.GetMasterCityData(CountryID);
            intPageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            string CityName = string.Empty;
            if (!string.IsNullOrWhiteSpace(txtCityName.Text))
            {
                CityName = txtCityName.Text;
            }
            var result = _objMasterData.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { Country_Id = CountryID, City_Name = CityName, Status = "ACTIVE", PageNo = pageindex, PageSize = intPageSize });
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




        // used tofill the header city detail form 
        private void fillCityForm(string City_Id, ref Guid refcountryID)
        {
            //dtCityDetail = objMasterDataDAL.GetMasterCityDetail(Guid.Parse(City_Id));
            var result = _objMasterData.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { City_Id = Guid.Parse(City_Id) });
            if (result != null && result.Count > 0)
            {
                refcountryID = result[0].Country_Id;
                CityState_id = result[0].State_Id;
            }
            frmCityMaster.DataSource = result;
            frmCityMaster.DataBind();
        }

        // used to fill the city area read form
        private void fillCityAreaForm(string CityArea_Id)
        {
            //dtCityAreaDetail = objMasterDataDAL.GetMasterCityAreaDetail(Guid.Parse(CityArea_Id));
            var result = _objMasterData.GetMasterCityAreaDetail(CityArea_Id);
            frmCityArea.ChangeMode(FormViewMode.Edit);
            frmCityArea.DataSource = result;
            frmCityArea.DataBind();

        }

        private void fillCityAreaLocationDetailForm(string CityAreaLocation_Id)
        {
            //dtCityAreaLocationDetail = objMasterDataDAL.GetMasterCityAreaLocationDetail(Guid.Parse(CityAreaLocation_Id));
            var result = _objMasterData.GetMasterCityAreaLocationDetail(CityAreaLocation_Id);
            frmCityAreaLocation.ChangeMode(FormViewMode.Edit);
            frmCityAreaLocation.DataSource = result;
            frmCityAreaLocation.DataBind();


        }

        // used to fill city area grid
        private void fillCityArea(string City_Id)
        {
            // dtCityArea = objMasterDataDAL.GetMasterCityAreaData(Guid.Parse(City_Id));
            var result = _objMasterData.GetMasterCityAreaData(City_Id);
            grdCityAreas.DataSource = result;
            grdCityAreas.DataBind();
        }

        private void fillCityAreaLocation(string CityArea_Id)
        {
            //dtCityAreaLocation = objMasterDataDAL.GetMasterCityAreaLocationData(Guid.Parse(CityArea_Id));\
            var result = _objMasterData.GetMasterCityAreaLocationData(CityArea_Id);
            grdCityAreaLocation.DataSource = result;
            grdCityAreaLocation.DataBind();
        }

        private void fillStateByCountryId(string Country_Id)
        {
            DropDownList ddlState = (DropDownList)frmCityMaster.FindControl("ddlState");

            var result = _objMasterData.GetStatesByCountry(Country_Id);
            ddlState.DataSource = result;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Id";
            ddlState.DataBind();
            ddlState.Items.FindByValue(Convert.ToString(CityState_id)).Selected = true;
            fillStateCode();
        }
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
        private void fillStateCode()
        {
            DropDownList ddlState = (DropDownList)frmCityMaster.FindControl("ddlState");
            TextBox txtStateCode = (TextBox)frmCityMaster.FindControl("txtStateCode");

            var result = _objMasterData.GetStatesByCountry(Convert.ToString(refCountryId));
            int statecode = (ddlState.SelectedIndex) - 1;
            if (statecode >= 0)
            {
                txtStateCode.Text = result[statecode].State_Code.ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["City_Id"] != null)
                {
                    panSearchConditions.Visible = false;
                    fillCityForm(Request.QueryString["City_Id"], ref refCountryId);
                    fillCityArea((Request.QueryString["City_Id"]));
                    fillStateByCountryId(Convert.ToString(refCountryId));
                }

                else
                {
                    updatePanel1.Visible = false;
                    fillgvCountryList();
                }

            }
            else
            {
                dvMsgCity2.Visible = false;
            }
        }



        // route the formview command to the correct data action -- matt.watson@coxandkings.com -- 11022017
        protected void frmCityArea_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            TextBox txtCityAreaName = (TextBox)frmCityArea.FindControl("txtCityAreaName");
            TextBox txtCityAreaCode = (TextBox)frmCityArea.FindControl("txtCityAreaCode");

            MDMSVC.DC_CityArea _obj = new MDMSVC.DC_CityArea();
            //Models.CityAreaE obj = new Models.CityAreaE();
            //obj.City_Id = Guid.Parse(Request.QueryString["City_Id"].ToString());
            //obj.Name = txtCityAreaName.Text.Trim();
            //obj.Code = txtCityAreaCode.Text.Trim();
            _obj.City_Id = Guid.Parse(Request.QueryString["City_Id"].ToString());
            _obj.Name = txtCityAreaName.Text.Trim();
            _obj.Code = txtCityAreaCode.Text.Trim();
            if (e.CommandName.ToString() == "Add")
            {
                _obj.CityArea_Id = Guid.NewGuid();
                _obj.Option = "Save";
                _objMasterData.SaveCityArea(_obj);

                // objMasterDataDAL.SaveCityArea(obj, Models.MasterDataDAL.operation.Save);

                fillCityArea(Request.QueryString["City_Id"]);
                txtCityAreaName.Text = "";
                txtCityAreaCode.Text = "";
                frmCityArea.ChangeMode(FormViewMode.Insert);


            }
            else if (e.CommandName.ToString() == "Save")
            {
                //obj.CityArea_Id = Guid.Parse(grdCityAreas.SelectedDataKey.Value.ToString());
                //objMasterDataDAL.SaveCityArea(obj, Models.MasterDataDAL.operation.Update);
                _obj.CityArea_Id = Guid.Parse(grdCityAreas.SelectedDataKey.Value.ToString());
                _obj.Option = "UPDATE";
                _objMasterData.SaveCityArea(_obj);

                fillCityArea(Request.QueryString["City_Id"]);
                txtCityAreaName.Text = "";
                txtCityAreaCode.Text = "";
                frmCityArea.ChangeMode(FormViewMode.Insert);

            }

        }

        protected void grdCityAreas_SelectedIndexChanged(object sender, EventArgs e)
        {

            dvCityAreaLocations.Visible = true;
            frmCityAreaLocation.ChangeMode(FormViewMode.Insert);
            fillCityAreaLocation(grdCityAreas.SelectedDataKey["CityArea_Id"].ToString());
            fillCityAreaForm(grdCityAreas.SelectedDataKey["CityArea_Id"].ToString());
        }

        protected void frmCityAreaLocation_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            TextBox txtCityAreaName = (TextBox)frmCityAreaLocation.FindControl("txtCityAreaName");
            TextBox txtCityAreaCode = (TextBox)frmCityAreaLocation.FindControl("txtCityAreaCode");

            MDMSVC.DC_CityAreaLocation obj = new MDMSVC.DC_CityAreaLocation();
            obj.City_Id = Guid.Parse(Request.QueryString["City_Id"].ToString());
            obj.Name = txtCityAreaName.Text.Trim();
            obj.Code = txtCityAreaCode.Text.Trim();
            obj.CityArea_Id = Guid.Parse(grdCityAreas.SelectedDataKey.Value.ToString());


            switch (e.CommandName.ToString())
            {
                case "Add":
                    {
                        obj.CityAreaLocation_Id = Guid.NewGuid();
                        //objMasterDataDAL.SaveCityAreaLocation(obj, Models.MasterDataDAL.operation.Save);
                        obj.Option = "SAVE";
                        _objMasterData.SaveCityAreaLocation(obj);
                        fillCityAreaLocation(grdCityAreas.SelectedDataKey["CityArea_Id"].ToString());
                        txtCityAreaName.Text = "";
                        txtCityAreaCode.Text = "";
                        frmCityAreaLocation.ChangeMode(FormViewMode.Insert);
                        dvMsgCity.Visible = false;
                    };
                    break;

                case "Save":
                    {
                        obj.CityAreaLocation_Id = Guid.Parse(grdCityAreaLocation.SelectedDataKey.Value.ToString());
                        obj.Option = "UPDATE";
                        _objMasterData.SaveCityAreaLocation(obj);
                        //objMasterDataDAL.SaveCityAreaLocation(obj, Models.MasterDataDAL.operation.Update);
                        fillCityAreaLocation(grdCityAreas.SelectedDataKey["CityArea_Id"].ToString());
                        txtCityAreaName.Text = "";
                        txtCityAreaCode.Text = "";
                        frmCityAreaLocation.ChangeMode(FormViewMode.Insert);
                        dvMsgCity.Visible = false;
                    };
                    break;
            }


        }

        protected void grdCityAreaLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCityAreaLocationDetailForm(grdCityAreaLocation.SelectedDataKey["CityAreaLocation_Id"].ToString());
        }

        protected void btnresetCityAreaLocation_Click(object sender, EventArgs e)
        {
            frmCityArea.ChangeMode(FormViewMode.Insert);
            dvMsgCity.Visible = false;
        }

        protected void frmCityArea_ItemInserting(object sender, FormViewInsertEventArgs e)
        {

        }

        protected void btnGetCities_Click(object sender, EventArgs e)
        {
            CountryID = Guid.Parse(ddlCountry.SelectedValue.ToString());
            fillgvCityyList(CountryID, 0);
            if (ddlCountry.SelectedIndex != 0)
                btnNewCity.Visible = true;
            else
                btnNewCity.Visible = false;
        }

        protected void grdCityList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CountryID = new Guid(ddlCountry.SelectedValue.ToString());
            fillgvCityyList(CountryID, e.NewPageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountryID = Guid.Parse(ddlCountry.SelectedValue.ToString());
            fillgvCityyList(CountryID, 0);
            dvMsgCity2.Visible = false;
        }

        protected void frmCityMaster_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtCityCode = (TextBox)frmCityMaster.FindControl("txtCityCode");
            TextBox txtStateCode = (TextBox)frmCityMaster.FindControl("txtStateCode");
            TextBox txtCityName = (TextBox)frmCityMaster.FindControl("txtCityName");
            DropDownList ddlState = (DropDownList)frmCityMaster.FindControl("ddlState");
            dvMsgCity.Visible = true;
            if (e.CommandName.ToString() == "UpdateCityManager")
            {
                MDMSVC.DC_City _obj = new MDMSVC.DC_City();
                _obj.City_Id = Guid.Parse(Request.QueryString["City_Id"]);
                _obj.Country_Id = refCountryId;
                _obj.Name = txtCityName.Text;
                _obj.Code = txtCityCode.Text;
                _obj.State_Id = Guid.Parse(ddlState.SelectedValue);
                _obj.StateName = ddlState.SelectedItem.Text;
                _obj.StateCode = txtStateCode.Text;
                _obj.Edit_Date = DateTime.Now;
                _obj.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
                _msg = _objMasterData.UpdateCityMasterData(_obj);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgCity, "City has been updated successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgCity, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillStateCode();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedIndex == 0)
                btnNewCity.Visible = false;
        }

        protected void btnNewCity_Click(object sender, EventArgs e)
        {
            fillStateList();
        }

        protected void frmvwCity_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtCityName = (TextBox)frmvwCity.FindControl("txtCityName");
            TextBox txtCityCode = (TextBox)frmvwCity.FindControl("txtCityCode");
            DropDownList ddlStates = (DropDownList)frmvwCity.FindControl("ddlStates");
            DropDownList ddlStatus = (DropDownList)frmvwCity.FindControl("ddlStatus");
            Guid ddlCountry_Id_Value = Guid.Parse(ddlCountry.SelectedValue);

            if (e.CommandName.ToString() == "AddCity")
            {
                var stateid = Guid.Parse(Convert.ToString(ddlStates.SelectedValue).Trim());
                var status = Convert.ToString(ddlStatus.SelectedValue);
                MDMSVC.DC_City _objCityMaster = new MDMSVC.DC_City();
                _objCityMaster.City_Id = Guid.NewGuid();
                _objCityMaster.Name = Convert.ToString(txtCityName.Text).Trim();
                _objCityMaster.Code = Convert.ToString(txtCityCode.Text).Trim();
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
            ddlStates.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
    }
}