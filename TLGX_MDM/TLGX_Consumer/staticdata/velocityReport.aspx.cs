﻿using System;
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
        MDMSVC.DC_VelocityReport parm = new MDMSVC.DC_VelocityReport();
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
            if (ddlDateOptions.SelectedValue == "6")
                dvSpecificDate.Style.Add("display", "block");
            else
                dvSpecificDate.Style.Add("display", "none");
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
                calculateDateRange(0);
            }
            else
            {
                calculateDateRange(Convert.ToInt32(ddlDateOptions.SelectedValue));
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
            if (res != null)
            {
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
                        var UnmappedActivity = res[0].MappingStatsFor[iNodes].Unmappeddata;
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
            }
            //Assigning nothing to gridview
            if (!blnHotelroomDataExist)
            {
                gvroomtype.DataSource = null;
                gvroomtype.DataBind();
            }
            if (!blnActivityDataExist)
            {
                gvactivity.DataSource = null;
                gvactivity.DataBind();
            }
            if (!blnIsCityDataExist)
            {
                gvcity.DataSource = null;
                gvcity.DataBind();
            }
            if (!blnIsCountryDataExist)
            {
                gvcountry.DataSource = null;
                gvcountry.DataBind();
            }
            if (!blnProductDataExist)
            {
                gvproduct.DataSource = null;
                gvproduct.DataBind();
            }
        }
        
        protected void btnViewStatus_Click(object sender, EventArgs e)
        {
                getData(false);
        }

        protected void calculateDateRange(int rangeValue)
        {
            DateTime presentDate = DateTime.Today;
            //yesterday
            if (rangeValue == 0)
            {
                var Dateyesterday = presentDate.AddDays(-1);
                parm.Fromdate = Dateyesterday;
                parm.ToDate = Dateyesterday;
            }
            //today
            else if(rangeValue == 1)
            {
                parm.Fromdate = presentDate;
                parm.ToDate = presentDate;
            }
            //thisweek
            else if (rangeValue == 2)
            {
                parm.Fromdate= DateTime.Now.FirstDayOfWeek();
                parm.ToDate = DateTime.Now.LastDayOfWeek();
            }
            //lastWeek
            else if (rangeValue == 3)
            {
                parm.Fromdate = DateTime.Now.FirstDayOfPreviousWeek();
                parm.ToDate = DateTime.Now.LastDayOfPreviousWeek();
            }
            //thisMonth
            else if(rangeValue == 4)
            {
                parm.Fromdate = DateTime.Now.FirstDayOfMonth();
                parm.ToDate = presentDate;
            }
            //LastMonth
            else if (rangeValue == 5)
            {
                parm.Fromdate = DateTime.Now.FirstDayOfPreviousMonth();
                parm.ToDate = DateTime.Now.LastDayOfPreviousMonth();
            }
            else
            {
                parm.Fromdate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                parm.ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

        }
        
    }



    //Days
    public static partial class DateTimeExtensions
    {
        //CurrentWeek
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }
        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }
        //previousWeek
        public static DateTime FirstDayOfPreviousWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(-7);
        }
        public static DateTime LastDayOfPreviousWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(-7).AddDays(6);
        }
        //CurrentMonth
        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }
        //PreviousMonth
        public static DateTime FirstDayOfPreviousMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(-1);
        }
        public static DateTime LastDayOfPreviousMonth(this DateTime dt)
        {
            return dt.FirstDayOfPreviousMonth().AddMonths(1).AddDays(-1);
        }
    }


}