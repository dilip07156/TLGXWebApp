using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Flavours : System.Web.UI.UserControl
    {
        public Guid Activity_Flavour_Id;
        // public Guid Activity_Id;
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        MDMSVC.DC_Activity_Flavour_RQ RQParams = new MDMSVC.DC_Activity_Flavour_RQ();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getFlavourInfo();
                fillMasterDropdowns();
            }
        }
        private void fillcoutries()
        {
           DropDownList ddlCountry = (DropDownList)frmActivityFlavour.FindControl("ddlCountry");
            ddlCountry.DataSource = masterSVc.GetAllCountries();
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            MDMSVC.DC_Accomodation rowView = (MDMSVC.DC_Accomodation)frmActivityFlavour.DataItem;
            if (rowView != null)
            {
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(rowView.Country.ToString()));
            }
        }

        private void fillcities()
        {
            DropDownList ddlCity = (DropDownList)frmActivityFlavour.FindControl("ddlCity");
            DropDownList ddlCountry = (DropDownList)frmActivityFlavour.FindControl("ddlCountry");
            if (ddlCountry.SelectedItem.Text != "-Select-")
            {
                ddlCity.Items.Clear();
                ddlCity.DataSource = masterSVc.GetMasterCityData(ddlCountry.SelectedValue);
                ddlCity.DataTextField = "Name";
                ddlCity.DataValueField = "City_Id";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                if (rowView != null)
                {
                    ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(rowView.City.ToString()));
                }
            }
        }

        private void getFlavourInfo()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            DC_Activity_Flavour_RQ RQ = new DC_Activity_Flavour_RQ();
            RQ.Activity_Flavour_Id= Activity_Flavour_Id;
            frmActivityFlavour.DataSource = AccSvc.GetActivityFlavour(RQ);
            frmActivityFlavour.DataBind();
        }
        private void fillMasterDropdowns()
        {
            fillcoutries();
            fillcities();
        }

        protected void frmHotelOverview_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void ddlProdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlProdcategorySubType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}