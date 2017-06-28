using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.geography
{
    public partial class portSearch : System.Web.UI.UserControl
    {
        #region Variable 
        Controller.MasterDataSVCs _serviceMaster = new Controller.MasterDataSVCs();
        public int intPageIndex = 0;
        public int intPageSize = 10;

        #endregion
        #region PageMethods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPageData();
            }
        }
        #endregion
        #region Data Fetching and Binding
        private void FillPageData()
        {
            try
            {
                fillSystemCountry();
                //fillSystemCity();
                fillMappingStatus();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void fillMappingStatus()
        {
            var resultMappingStatus = _serviceMaster.GetAllAttributeAndValuesByFOR(new MDMSVC.DC_MasterAttribute() { MasterFor = "ProductSupplierMapping" });
            if (resultMappingStatus != null)
            {
                if (resultMappingStatus.Count > 0)
                {
                    ddlStatus.DataSource = resultMappingStatus;
                    ddlStatus.DataValueField = "MasterAttributeValue_Id";
                    ddlStatus.DataTextField = "AttributeValue";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new ListItem { Text = "- ALL -", Value = "0" });

                }
            }
        }

        private void fillSystemCountry()
        {
            try
            {
                var result = _serviceMaster.GetCountryMasterData(new MDMSVC.DC_Country_Search_RQ() { });
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        ddlMasterCountry.Items.Clear();
                        ddlMasterCountry.DataSource = result;
                        ddlMasterCountry.DataValueField = "Country_Id";
                        ddlMasterCountry.DataTextField = "Name";
                        ddlMasterCountry.DataBind();
                        ddlMasterCountry.Items.Insert(0, new ListItem { Text = "- ALL -", Value = "0" });

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #region Control Action 
        protected void ddlMasterCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strSelectedCountry = Convert.ToString(ddlMasterCountry.SelectedValue);
                var resultCity = _serviceMaster.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { Country_Id = Guid.Parse(strSelectedCountry) });
                if (resultCity != null)
                {
                    if (resultCity.Count > 0)
                    {
                        ddlMasterCity.Items.Clear();

                        ddlMasterCity.DataSource = resultCity;
                        ddlMasterCity.DataValueField = "City_Id";
                        ddlMasterCity.DataTextField = "Name";
                        ddlMasterCity.DataBind();
                        ddlMasterCity.Items.Insert(0, new ListItem { Text = "- ALL -", Value = "0" });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// To search port 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPortDetails();
        }
        /// <summary>
        /// To reset the page controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPageControls();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearPageControls()
        {
            ddlMasterCountry.ClearSelection();
            ddlMasterCity.Items.Clear();
            ddlMasterCity.Items.Insert(0, new ListItem { Selected = true, Text = "-All-", Value = "0" });
            ddlStatus.ClearSelection();
            ddlShowEntries.ClearSelection();
        }

        protected void grdPortList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            SearchPortDetails();
        }
        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            intPageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            SearchPortDetails();
        }

        private void SearchPortDetails()
        {
            try
            {
                var _ddlCountryMaster = Convert.ToString(ddlMasterCountry.SelectedValue) == "0" ? String.Empty : Convert.ToString(ddlMasterCountry.SelectedValue);
                var _ddlMasterCity = Convert.ToString(ddlMasterCity.SelectedValue) == "0" ? String.Empty : Convert.ToString(ddlMasterCity.SelectedValue);
                var _ddlStatus = Convert.ToString(ddlStatus.SelectedValue) == "0" ? String.Empty : Convert.ToString(ddlStatus.SelectedValue);
                var _ddlEntriesCount = Convert.ToString(ddlShowEntries.SelectedValue);
                var lblPortCountry = Convert.ToString(txtSuppCountry.Text) == String.Empty ? String.Empty : Convert.ToString(txtSuppCountry.Text);

                MDMSVC.DC_PortMaster_RQ _obj = new MDMSVC.DC_PortMaster_RQ();
                if (!string.IsNullOrWhiteSpace(_ddlCountryMaster))
                    _obj.Country_Id = Guid.Parse(_ddlCountryMaster);
                if (!string.IsNullOrWhiteSpace(_ddlMasterCity))
                    _obj.City_Id = Guid.Parse(_ddlMasterCity);
                if (!string.IsNullOrWhiteSpace(_ddlStatus))
                    _obj.Mapping_Status = _ddlStatus;
                if (!string.IsNullOrWhiteSpace(_ddlEntriesCount))
                    _obj.PageSize = Convert.ToInt32(_ddlEntriesCount);
                else
                    _obj.PageSize = 10;
                if (!string.IsNullOrWhiteSpace(lblPortCountry))
                    _obj.Port_Country_Name = lblPortCountry;
                _obj.PageNo = intPageIndex;


                var result = _serviceMaster.PortMasterSeach(_obj);
                grdPortList.PageIndex = intPageIndex;
                grdPortList.DataSource = result;
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        grdPortList.PageSize = intPageSize;
                        grdPortList.VirtualItemCount = result[0].TotalRecords;
                    }
                }
                grdPortList.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grdPortList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        #endregion


        protected void btnNewPort_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmPortdetail.ChangeMode(FormViewMode.Insert);
            frmPortdetail.DataBind();

            fillSystemCountryForNew();


        }

        private void fillSystemCountryForNew()
        {
            try
            {
                var result = _serviceMaster.GetCountryMasterData(new MDMSVC.DC_Country_Search_RQ() { });
                DropDownList ddlCountryEdit = (DropDownList)frmPortdetail.FindControl("ddlCountryEdit");
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        ddlCountryEdit.Items.Clear();

                        ddlCountryEdit.DataSource = result;
                        ddlCountryEdit.DataValueField = "Country_Id";
                        ddlCountryEdit.DataTextField = "Name";
                        ddlCountryEdit.DataBind();
                        ddlMasterCountry.Items.Insert(0, new ListItem { Text = "- ALL -", Value = "0" });

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlCountryEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountryEdit = (DropDownList)frmPortdetail.FindControl("ddlCountryEdit");
            DropDownList ddlStateEdit = (DropDownList)frmPortdetail.FindControl("ddlStateEdit");
            string strSelectedCountry = Convert.ToString(ddlCountryEdit.SelectedValue);
            var resultState = _serviceMaster.GetStateMasterData(new MDMSVC.DC_State_Search_RQ() { Country_Id = Guid.Parse(strSelectedCountry) });
            if (resultState != null)
            {
                if (resultState.Count > 0)
                {
                    ddlStateEdit.Items.Clear();
                    ddlStateEdit.DataSource = resultState;
                    ddlStateEdit.DataValueField = "State_Id";
                    ddlStateEdit.DataTextField = "State_Name";
                    ddlStateEdit.DataBind();
                    ddlStateEdit.Items.Insert(0, new ListItem { Text = "- ALL -", Value = "0" });
                }
            }
            BindCity(Guid.Parse(strSelectedCountry));

        }
        private void BindCity(Guid? Country_Id)
        {
            var resultCity = _serviceMaster.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { Country_Id = Country_Id });
            DropDownList ddlCityEdit = (DropDownList)frmPortdetail.FindControl("ddlCityEdit");

            if (resultCity != null)
            {
                if (resultCity.Count > 0)
                {
                    ddlCityEdit.DataSource = resultCity;
                    ddlCityEdit.DataValueField = "City_Id";
                    ddlCityEdit.DataTextField = "Name";
                    ddlCityEdit.DataBind();
                    ddlCityEdit.Items.Insert(0, new ListItem { Text = "- ALL -", Value = "0" });

                }
            }
        }

        protected void frmPortdetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            #region Get Controls
            //Getting all controls here
            TextBox txtoag_portname = (TextBox)frmPortdetail.FindControl("txtoag_portname");
            TextBox txtOAG_loc = (TextBox)frmPortdetail.FindControl("txtOAG_loc");
            TextBox txtOAG_multicity = (TextBox)frmPortdetail.FindControl("txtOAG_multicity");
            TextBox txtoag_inactive = (TextBox)frmPortdetail.FindControl("txtoag_inactive");
            TextBox txtMappingStatus = (TextBox)frmPortdetail.FindControl("txtMappingStatus");
            TextBox txtoag_ctryname = (TextBox)frmPortdetail.FindControl("txtoag_ctryname");
            TextBox txtoag_state = (TextBox)frmPortdetail.FindControl("txtoag_state");

            TextBox txtoag_substate = (TextBox)frmPortdetail.FindControl("txtoag_substate");
            TextBox txtoag_timediv = (TextBox)frmPortdetail.FindControl("txtoag_timediv");
            TextBox txtoag_lat = (TextBox)frmPortdetail.FindControl("txtoag_lat");
            TextBox txtOAG_type = (TextBox)frmPortdetail.FindControl("txtOAG_type");

            TextBox txtOAG_subtype = (TextBox)frmPortdetail.FindControl("txtOAG_subtype");
            TextBox txtoag_name = (TextBox)frmPortdetail.FindControl("txtoag_name");
            TextBox txtoag_ctry = (TextBox)frmPortdetail.FindControl("txtoag_ctry");
            TextBox txtoag_subctry = (TextBox)frmPortdetail.FindControl("txtoag_subctry");
            TextBox txtoag_lon = (TextBox)frmPortdetail.FindControl("txtoag_lon");


            DropDownList ddlCountryEdit = (DropDownList)frmPortdetail.FindControl("ddlCountryEdit");
            DropDownList ddlStateEdit = (DropDownList)frmPortdetail.FindControl("ddlStateEdit");
            DropDownList ddlCityEdit = (DropDownList)frmPortdetail.FindControl("ddlCityEdit");

            #endregion
            if (e.CommandName == "Add")
            {
                MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
                MDMSVC.DC_PortMaster _newObj = new MDMSVC.DC_PortMaster();
                _newObj.Port_Id = Guid.NewGuid();
                _newObj.OAG_loc = Convert.ToString(txtOAG_loc.Text);
                _newObj.OAG_multicity = Convert.ToString(txtOAG_multicity.Text);
                _newObj.OAG_type = Convert.ToString(txtoag_lon.Text);
                _newObj.Oag_inactive = Convert.ToString(txtoag_inactive.Text);
                _newObj.MappingStatus = Convert.ToString(txtMappingStatus.Text);
                _newObj.Oag_ctryname = Convert.ToString(txtoag_ctryname.Text);
                _newObj.Oag_state = Convert.ToString(txtoag_state.Text);
                _newObj.Oag_substate = Convert.ToString(txtoag_substate.Text);
                _newObj.Oag_timediv = Convert.ToString(txtoag_timediv.Text);
                _newObj.Oag_lat = Convert.ToString(txtoag_lat.Text);
                _newObj.OAG_type = Convert.ToString(txtOAG_type.Text);
                _newObj.OAG_subtype = Convert.ToString(txtOAG_subtype.Text);
                _newObj.Oag_name = Convert.ToString(txtoag_name.Text);
                _newObj.Oag_portname = Convert.ToString(txtoag_portname.Text);
                _newObj.Oag_ctry = Convert.ToString(txtoag_ctry.Text);
                _newObj.Oag_subctry = Convert.ToString(txtoag_subctry.Text);
                _newObj.City_Id = Guid.Parse(ddlCityEdit.SelectedValue);
                _newObj.Country_Id = Guid.Parse(ddlCountryEdit.SelectedValue);
                _newObj.State_Id = Guid.Parse(ddlStateEdit.SelectedValue);
                _msg = _serviceMaster.AddPortMaster(_newObj);
                hdnFlag.Value = "true";
                BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }
        }
    }
}