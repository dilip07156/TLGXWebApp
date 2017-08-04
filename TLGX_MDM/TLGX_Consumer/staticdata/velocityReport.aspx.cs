using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata
{
    public partial class velocityReport : System.Web.UI.Page
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MDMSVC.DC_RollOFParams parm = new MDMSVC.DC_RollOFParams();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();

        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();
                getData(true);

            }


        }
        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }
        private void getData(bool IsPageLoad)
        {
            string SupplierID = ddlSupplierName.SelectedValue;
            if (SupplierID == "0")
            {
                SupplierID = "00000000-0000-0000-0000-000000000000";
            }

            parm.SupplierID = Guid.Parse(SupplierID);

            if (IsPageLoad)
            {
                DateTime d = DateTime.Today;
                d = d.AddMonths(-1);
                parm.Fromdate = d.ToString("dd-MMM-yyyy");
                parm.ToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            }

            var res = MapSvc.GetVelocityDashboard(parm);


            //lstusers.DataSource = res;
            //lstusers.DataBind();
            var iNodes = 0;
            bool blnIsCityDataExist = false;
            bool blnIsCountryDataExist = false;
            bool blnProductDataExist = false;
            bool blnActivityDataExist = false;
            bool blnHotelroomDataExist = false;
            
            for (; iNodes < res[0].MappingStatsFor.Length; iNodes++)
            {
                if (res[0].MappingStatsFor[iNodes].MappingFor == "Country")
                {
                    var resultDataForCountry = res[0].MappingStatsFor[iNodes].MappingData;
                     var UnmappedCountry = res[0].MappingStatsFor[iNodes].Unmappeddata;
                    var estimate = res[0].MappingStatsFor[iNodes].Estimate;
                    string.Format("{0:n0}", UnmappedCountry);
                    gvcountry.DataSource = resultDataForCountry;
                    gvcountry.DataBind();
                    lblcountry.Text = Convert.ToString(UnmappedCountry);
                    lblcountryestimate.Text = Convert.ToString(estimate);
                    blnIsCountryDataExist = true;
                    

                }
                else if (res[0].MappingStatsFor[iNodes].MappingFor == "City")
                {
                    var resultDataForCity = res[0].MappingStatsFor[iNodes].MappingData;
                    var UnmappedCity = res[0].MappingStatsFor[iNodes].Unmappeddata;
                    string.Format("{0:n0}", UnmappedCity);
                    gvcity.DataSource = resultDataForCity;
                    gvcity.DataBind();
                    lblcity.Text = Convert.ToString(UnmappedCity);
                    var estimate = res[0].MappingStatsFor[iNodes].Estimate;
                    lblcityestimate.Text = Convert.ToString(estimate);
                    blnIsCityDataExist = true;
                }
                else if (res[0].MappingStatsFor[iNodes].MappingFor == "Product")
                {
                    var resultDataForProduct = res[0].MappingStatsFor[iNodes].MappingData;
                    var UnmappedProduct = res[0].MappingStatsFor[iNodes].Unmappeddata;
                    string.Format("{0:n0}", UnmappedProduct);
                    gvproduct.DataSource = resultDataForProduct;
                    gvproduct.DataBind();
                    lblproduct.Text = Convert.ToString(UnmappedProduct);
                    var estimate = res[0].MappingStatsFor[iNodes].Estimate;
                    lblproductestimate.Text = Convert.ToString(estimate);
                    blnProductDataExist = true;
                }
                else if (res[0].MappingStatsFor[iNodes].MappingFor == "Activity")
                {
                    var resultDataForActivity = res[0].MappingStatsFor[iNodes].MappingData;
                   var  UnmappedActivity = res[0].MappingStatsFor[iNodes].Unmappeddata;
                    string.Format("{0:n0}", UnmappedActivity);
                    gvactivity.DataSource = resultDataForActivity;
                    gvactivity.DataBind();
                    lblactivity.Text = Convert.ToString(UnmappedActivity);
                    var estimate = res[0].MappingStatsFor[iNodes].Estimate;
                    lblactivityestimate.Text = Convert.ToString(estimate);
                    blnActivityDataExist = true;
                }
                else if (res[0].MappingStatsFor[iNodes].MappingFor == "HotelRoom")
                {
                    var resultDataForhotelRoom = res[0].MappingStatsFor[iNodes].MappingData;
                    var UnmappedHotelroom = res[0].MappingStatsFor[iNodes].Unmappeddata;
                    string.Format("{0:n0}", UnmappedHotelroom);
                    gvroomtype.DataSource = resultDataForhotelRoom;
                    gvroomtype.DataBind();
                    lblhotelroom.Text = Convert.ToString(UnmappedHotelroom);
                    var estimate = res[0].MappingStatsFor[iNodes].Estimate;
                    lblhotelroomestimate.Text = Convert.ToString(estimate);
                    blnHotelroomDataExist = true;
                }
            }
            //Assigning nothing to gridview
            if (!blnHotelroomDataExist)
            {
                gvroomtype.DataSource = null;
                gvroomtype.DataBind();
            }

        }

        protected void btnViewStatus_Click(object sender, EventArgs e)
        {
            getData(false);
        }
        private int getEstimatedate(DateTime fromdate,DateTime todate,int total,int unmapped)
        {
            int ans = 0;
            var days = (todate - fromdate).TotalDays;
            var perday = (total / days);
             ans = Convert.ToInt32(unmapped / perday);
            return ans;

        }
    }


}