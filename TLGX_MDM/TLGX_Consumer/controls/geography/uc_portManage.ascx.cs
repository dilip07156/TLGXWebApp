using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.geography
{
    public partial class uc_portManage : System.Web.UI.UserControl
    {
        #region Variables
        public static string Port_ID { get; set; }
        MasterDataSVCs _objMaster = new MasterDataSVCs();

        #endregion

        #region Page Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            Port_ID = Convert.ToString(Request.QueryString["Port_Id"]);
            if (!IsPostBack)
            {
                if (Port_ID != null)
                {
                    Guid newGuid = Guid.Parse(Port_ID);
                    FillPageData(newGuid);
                }
                else
                {
                }


            }
        }
        #endregion
        #region Methods
        private void FillPageData(Guid newGuid)
        {
            try
            {
                var result = _objMaster.PortMasterSeach(new MDMSVC.DC_PortMaster_RQ() { Port_Id = newGuid, PageSize= 5 });
                if (result != null)
                    if (result.Count > 0)
                    {
                        BindCountry();
                        BindState(result[0].Country_Id);
                        BindCity(result[0].Country_Id);
                        //Fill all data
                        txtOAG_loc.Text = Convert.ToString(result[0].OAG_loc);
                        txtOAG_multicity.Text = Convert.ToString(result[0].OAG_multicity);
                        txtoag_lon.Text = Convert.ToString(result[0].Oag_lon);
                        txtoag_inactive.Text = Convert.ToString(result[0].Oag_inactive);
                        txtMappingStatus.Text = Convert.ToString(result[0].MappingStatus);
                        txtoag_ctryname.Text = Convert.ToString(result[0].Oag_ctryname);
                        txtoag_state.Text = Convert.ToString(result[0].Oag_state);
                        txtoag_substate.Text = Convert.ToString(result[0].Oag_substate);
                        txtoag_timediv.Text = Convert.ToString(result[0].Oag_timediv);
                        txtoag_lat.Text = Convert.ToString(result[0].Oag_lat);


                        txtOAG_type.Text = Convert.ToString(result[0].OAG_type);
                        txtOAG_subtype.Text = Convert.ToString(result[0].OAG_subtype);
                        txtoag_name.Text = Convert.ToString(result[0].Oag_name);
                        txtoag_portname.Text = Convert.ToString(result[0].Oag_portname);
                        txtoag_ctry.Text = Convert.ToString(result[0].Oag_ctry);
                        txtoag_subctry.Text = Convert.ToString(result[0].Oag_subctry);

                        ddlCountryEdit.SelectedValue = Convert.ToString(result[0].Country_Id);
                        ddlCityEdit.SelectedValue = Convert.ToString(result[0].City_Id);
                        ddlStateEdit.SelectedValue = Convert.ToString(result[0].State_Id);
                        //  ddlStatus.SelectedValue = Convert.ToString(result[0].stat)

                    }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BindCity(Guid? Country_Id)
        {
            var resultCity = _objMaster.GetCityMasterData(new MDMSVC.DC_City_Search_RQ() { Country_Id = Country_Id });
            if (resultCity != null)
            {
                if (resultCity.Count > 0)
                {
                    ddlCityEdit.DataSource = resultCity;
                    ddlCityEdit.DataValueField = "City_Id";
                    ddlCityEdit.DataTextField = "Name";
                    ddlCityEdit.DataBind();
                    ddlCityEdit.Items.Insert(0, new ListItem { Selected = true, Text = "- ALL -", Value = "0" });

                }
            }
        }

        private void BindState(Guid? country_Id)
        {
            var resultState = _objMaster.GetStateMasterData(new MDMSVC.DC_State_Search_RQ() { Country_Id = country_Id });

            if (resultState != null)
            {
                if (resultState.Count > 0)
                {
                    ddlStateEdit.DataSource = resultState;
                    ddlStateEdit.DataValueField = "State_Id";
                    ddlStateEdit.DataTextField = "State_Name";
                    ddlStateEdit.DataBind();
                    ddlStateEdit.Items.Insert(0, new ListItem { Selected = true, Text = "- ALL -", Value = "0" });

                }
            }
        }

        private void BindCountry()
        {
            var resultCountry = _objMaster.GetCountryMasterData(new MDMSVC.DC_Country_Search_RQ() { });
            if (resultCountry != null)
            {
                if (resultCountry.Count > 0)
                {
                    ddlCountryEdit.DataSource = resultCountry;
                    ddlCountryEdit.DataValueField = "Country_Id";
                    ddlCountryEdit.DataTextField = "Name";
                    ddlCountryEdit.DataBind();
                    ddlCountryEdit.Items.Insert(0, new ListItem { Selected = true, Text = "- ALL -", Value = "0" });

                }
            }
        }
        #endregion

        protected void btnEditPort_Click(object sender, EventArgs e)
        {
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
            MDMSVC.DC_PortMaster _newObj = new MDMSVC.DC_PortMaster();
            _newObj.Port_Id = Guid.Parse(Port_ID);
            _newObj.OAG_loc = txtOAG_loc.Text;
            _newObj.OAG_multicity = txtOAG_multicity.Text;
            _newObj.Oag_lon = txtoag_lon.Text;
            _newObj.Oag_inactive = txtoag_inactive.Text;
            _newObj.MappingStatus = txtMappingStatus.Text;
            _newObj.Oag_ctryname = txtoag_ctryname.Text;
            _newObj.Oag_state = txtoag_state.Text;
            _newObj.Oag_substate = txtoag_substate.Text;
            _newObj.Oag_timediv = txtoag_timediv.Text;
            _newObj.Oag_lat = txtoag_lat.Text;
            _newObj.OAG_type = txtOAG_type.Text;
            _newObj.OAG_subtype = txtOAG_subtype.Text;
            _newObj.Oag_name = txtoag_name.Text;
            _newObj.Oag_portname = txtoag_portname.Text;
            _newObj.Oag_ctry = txtoag_ctry.Text;
            _newObj.Oag_subctry = txtoag_subctry.Text;
            _newObj.City_Id = Guid.Parse(ddlCityEdit.SelectedValue);
            _newObj.Country_Id = Guid.Parse(ddlCountryEdit.SelectedValue);
            _newObj.State_Id = Guid.Parse(ddlStateEdit.SelectedValue);

            _msg = _objMaster.UpdatePortMaster(_newObj);
            BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
        }

        protected void btnRedirectToSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/geography/portSearch.aspx");
        }
    }
}