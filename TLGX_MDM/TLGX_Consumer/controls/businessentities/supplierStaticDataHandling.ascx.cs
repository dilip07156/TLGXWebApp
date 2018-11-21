using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierStaticDataHandling : System.Web.UI.UserControl
    {
        ScheduleDataSVCs _objSchedularService = new ScheduleDataSVCs();
        Controller.ScheduleDataSVCs SchSvc = new Controller.ScheduleDataSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // LoadPageData();
                LoadSupplierScheduleData();
            }
        }
        private List<DC_Supplier_Schedule> GetListOfScheduleBySupplier()
        {
            try
            {
                Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                MDMSVC.DC_Supplier_Schedule_RQ _objSearch = new MDMSVC.DC_Supplier_Schedule_RQ();
                _objSearch.Suppllier_ID = mySupplier_Id;
                var _objresult = _objSchedularService.GetSchedule(_objSearch);
                return _objresult;
            }
            catch (Exception)
            {
            }
            return null;
        }

        private void LoadSupplierScheduleData()
        {
            try
            {
                MDMSVC.DC_Supplier_Schedule_RQ _objSearch = new MDMSVC.DC_Supplier_Schedule_RQ();
                Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                _objSearch.Suppllier_ID = mySupplier_Id;
                _objSearch.PageSize = 10;
                _objSearch.PageNo = 0;
                var result = _objSchedularService.GetSupplierSchedule(_objSearch);
                grdSupplierSchedule.DataSource = result;
                //grdSupplierSchedule.PageIndex = intPageIndex;
                //grdSupplierSchedule.PageSize = intPageSize;
                grdSupplierSchedule.DataBind();
                resetControls();
                
            }

            catch (Exception)
            {

                throw;
            }

        }

        private void LoadPageDataPrevious()
        {
            //try
            //{
            //    LoadSupplierScheduleData();
            //    //Get Data for Schedule
            //    List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
            //    _objresult = GetListOfScheduleBySupplier();
            //    if (_objresult != null)
            //    {
            //        if (_objresult.Count > 0)
            //        {
            //            Guid SupplierScheduleID = _objresult[0].SupplierScheduleID;
            //            if (Convert.ToBoolean(_objresult[0].ISXMLSupplier))
            //            {
            //                string strActiveFrequencyType = _objresult[0].FrequencyTypeCode.Substring(0, 1);
            //                rdbFrequency.SelectedValue = strActiveFrequencyType;
            //                #region AllDiv display none
            //                divYearly.Attributes["class"] = "Divdisplay";
            //                divMonthly.Attributes["class"] = "Divdisplay";
            //                divWeekly.Attributes["class"] = "Divdisplay";
            //                divDaily.Attributes["class"] = "Divdisplay";
            //                divHourly.Attributes["class"] = "Divdisplay";
            //                #endregion
            //                if (strActiveFrequencyType == "Y")
            //                {
            //                    string yearval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
            //                    divYearly.Attributes["class"] = "activediv";
            //                    ddlYearMonth_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
            //                    txtMonthDay_Year.Text = Convert.ToString(_objresult[0].DateOfMonth);
            //                    txtRecurEvery_Year.Text = Convert.ToString(_objresult[0].Recur_No);
            //                    if (yearval == "M")
            //                    {
            //                        rbDay_Year.Checked = true;
            //                    }
            //                    else if (yearval == "W")
            //                    {
            //                        rbOnthe_Year.Checked = true;
            //                        ddlMonthWeekList_Year.SelectedValue = Convert.ToString(_objresult[0].DayOfWeek.ToString().IndexOf("1") + 1);
            //                        ddlMonths_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
            //                    }
            //                }
            //                if (strActiveFrequencyType == "M")
            //                {
            //                    divMonthly.Attributes["class"] = "activediv";
            //                    string monthval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
            //                    if (monthval == "D")
            //                    {
            //                        rblDays_Monthly.Checked = true;
            //                        txtDayOf_Monthly.Text = Convert.ToString(_objresult[0].DateOfMonth);
            //                        txtDayOfEvery_Monthly.Text = Convert.ToString(_objresult[0].Recur_No);
            //                    }
            //                    else if (monthval == "W")
            //                    {
            //                        rbThe_Montly.Checked = true;
            //                        ddlTheDaysOf_Month.SelectedValue = Convert.ToString(Convert.ToInt32(_objresult[0].DayOfWeek.ToString().IndexOf('1')) + 1);
            //                        ddlTheSequencyWeek_Month.SelectedValue = Convert.ToString(_objresult[0].WeekOfMonth);
            //                        txtOccurEvery_Month.Text = Convert.ToString(_objresult[0].Recur_No);
            //                    }
            //                }
            //                if (strActiveFrequencyType == "W")
            //                {
            //                    divWeekly.Attributes["class"] = "activediv";
            //                    txtRecur_Weekly.Text = Convert.ToString(_objresult[0].Recur_No);
            //                    StringBuilder sbDay = new StringBuilder(Convert.ToString(_objresult[0].DayOfWeek));
            //                    char[] days = sbDay.ToString().ToCharArray();
            //                    int i = 0;
            //                    foreach (ListItem item in chckbxWeek_Weekly.Items)
            //                    {
            //                        item.Selected = Convert.ToBoolean(Convert.ToInt32(days[i].ToString()));
            //                        i++;
            //                    }

            //                }

            //                if (strActiveFrequencyType == "D")
            //                {
            //                    divDaily.Attributes["class"] = "activediv";
            //                    txtOccur_Daily.Text = Convert.ToString(_objresult[0].Recur_No);
            //                }
            //                if (strActiveFrequencyType == "H")
            //                {
            //                    divHourly.Attributes["class"] = "activediv";
            //                    txtRecur_Hourly.Text = Convert.ToString(_objresult[0].Recur_No);
            //                }
            //                timepickerStart_Hourly.Value = Convert.ToString(_objresult[0].StartTime);
            //                timepickerEnd_Hourly.Value = Convert.ToString(_objresult[0].EndTime);
            //                ddlStatus.SelectedValue = (Convert.ToString(_objresult[0].Status));

            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void LoadPageData()
        {
            try
            {
                LoadSupplierScheduleData();
                //Get Data for Schedule
                List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
                _objresult = GetListOfScheduleBySupplier();
                if (_objresult != null)
                {
                    if (_objresult.Count > 0)
                    {
                        //Guid SupplierScheduleID = _objresult[0].SupplierScheduleID;
                        //if (Convert.ToBoolean(_objresult[0].ISXMLSupplier))
                        //{
                        //    string strActiveFrequencyType = _objresult[0].FrequencyTypeCode.Substring(0, 1);
                        //    //rdbFrequency.SelectedValue = strActiveFrequencyType;
                        //    #region AllDiv display none
                        //    //divYearly.Attributes["class"] = "Divdisplay";
                        //    //divMonthly.Attributes["class"] = "Divdisplay";
                        //    //divWeekly.Attributes["class"] = "Divdisplay";
                        //    //divDaily.Attributes["class"] = "Divdisplay";
                        //    //divHourly.Attributes["class"] = "Divdisplay";
                        //    #endregion
                        //    if (strActiveFrequencyType == "Y")
                        //    {
                        //        string yearval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                        //        divYearly.Attributes["class"] = "activediv";
                        //        ddlYearMonth_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
                        //        txtMonthDay_Year.Text = Convert.ToString(_objresult[0].DateOfMonth);
                        //        txtRecurEvery_Year.Text = Convert.ToString(_objresult[0].Recur_No);
                        //        if (yearval == "M")
                        //        {
                        //            rbDay_Year.Checked = true;
                        //        }
                        //        else if (yearval == "W")
                        //        {
                        //            rbOnthe_Year.Checked = true;
                        //            ddlMonthWeekList_Year.SelectedValue = Convert.ToString(_objresult[0].DayOfWeek.ToString().IndexOf("1") + 1);
                        //            ddlMonths_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
                        //        }
                        //    }
                        //    if (strActiveFrequencyType == "M")
                        //    {
                        //        divMonthly.Attributes["class"] = "activediv";
                        //        string monthval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                        //        if (monthval == "D")
                        //        {
                        //            rblDays_Monthly.Checked = true;
                        //            txtDayOf_Monthly.Text = Convert.ToString(_objresult[0].DateOfMonth);
                        //            txtDayOfEvery_Monthly.Text = Convert.ToString(_objresult[0].Recur_No);
                        //        }
                        //        else if (monthval == "W")
                        //        {
                        //            rbThe_Montly.Checked = true;
                        //            ddlTheDaysOf_Month.SelectedValue = Convert.ToString(Convert.ToInt32(_objresult[0].DayOfWeek.ToString().IndexOf('1')) + 1);
                        //            ddlTheSequencyWeek_Month.SelectedValue = Convert.ToString(_objresult[0].WeekOfMonth);
                        //            txtOccurEvery_Month.Text = Convert.ToString(_objresult[0].Recur_No);
                        //        }
                        //    }
                        //    if (strActiveFrequencyType == "W")
                        //    {
                        //        divWeekly.Attributes["class"] = "activediv";
                        //        txtRecur_Weekly.Text = Convert.ToString(_objresult[0].Recur_No);
                        //        StringBuilder sbDay = new StringBuilder(Convert.ToString(_objresult[0].DayOfWeek));
                        //        char[] days = sbDay.ToString().ToCharArray();
                        //        int i = 0;
                        //        foreach (ListItem item in chckbxWeek_Weekly.Items)
                        //        {
                        //            item.Selected = Convert.ToBoolean(Convert.ToInt32(days[i].ToString()));
                        //            i++;
                        //        }

                        //    }

                        //    if (strActiveFrequencyType == "D")
                        //    {
                        //        divDaily.Attributes["class"] = "activediv";
                        //        txtOccur_Daily.Text = Convert.ToString(_objresult[0].Recur_No);
                        //    }
                        //    if (strActiveFrequencyType == "H")
                        //    {
                        //        divHourly.Attributes["class"] = "activediv";
                        //        txtRecur_Hourly.Text = Convert.ToString(_objresult[0].Recur_No);
                        //    }
                        //    timepickerStart_Hourly.Value = Convert.ToString(_objresult[0].StartTime);
                        //    timepickerEnd_Hourly.Value = Convert.ToString(_objresult[0].EndTime);
                        //    ddlStatus.SelectedValue = (Convert.ToString(_objresult[0].Status));

                        //}
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save()
        {
            try
            {
                string frequencyvalue = Request.Form["ddl_period"];
                string expression = cron_expression.Value;

                List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
                _objresult = GetListOfScheduleBySupplier();
                Guid SupplierScheduleID = Guid.Empty;
                Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                MDMSVC.DC_Supplier_Schedule _objSupSch = new MDMSVC.DC_Supplier_Schedule();
                if (_objresult != null && _objresult.Count > 0)
                {
                    if (Session["SupplierScheduleId"] != null)
                    {
                        SupplierScheduleID = (Guid)(Session["SupplierScheduleId"]);
                    }
                    //mySupplier_Id = _objresult[0].Suppllier_ID;
                    _objSupSch.SupplierScheduleID = SupplierScheduleID;
                    _objSupSch.Create_User = _objSupSch.Create_User;
                    _objSupSch.Create_Date = _objSupSch.Create_Date;
                    _objSupSch.Edit_Date = DateTime.Now;
                    _objSupSch.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

                }
                if (SupplierScheduleID == Guid.Empty)
                {
                    _objSupSch.Create_Date = DateTime.Now;
                    _objSupSch.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                }

                _objSupSch.Suppllier_ID = mySupplier_Id;

                _objSupSch.ISXMLSupplier = Convert.ToBoolean(Convert.ToInt16(rdbtnIsAPIXMLSupplier.SelectedValue));
                _objSupSch.ISUpdateFrequence = Convert.ToBoolean(Convert.ToInt16(rdbIsUpdateFrequencyRequired.SelectedValue));
                StringBuilder sbFrequencyTypeCode = new StringBuilder();

                sbFrequencyTypeCode.Append(frequencyvalue);
                if (Convert.ToBoolean(Convert.ToInt16(rdbtnIsAPIXMLSupplier.SelectedValue)))
                {
                    #region Yearly
                    if (frequencyvalue == "Y")
                    {
                        //_objSupSch.Recur_No = Convert.ToInt32(txtRecurEvery_Year.Text);
                        string Yearly_type = Request.Form["yearlyType"];

                        if (Yearly_type == "byDay")
                        {
                            sbFrequencyTypeCode.Append("M");
                            _objSupSch.MonthOfYear =Convert.ToInt32(Request.Form["ddl_yearly_Month"]);
                            _objSupSch.DateOfMonth = Convert.ToInt32(Request.Form["ddl_yearly_day"]);
                        }
                        else if (Yearly_type == "byWeek")
                        {
                            sbFrequencyTypeCode.Append("W");
                            _objSupSch.WeekOfMonth = Convert.ToInt32(Request.Form["ddl_yearly_nthday"]);
                            string strDays = Convert.ToString("0000000").Remove(Convert.ToInt32(Request.Form["ddl_yearly_day_od_week"]) - 1, 1).Insert(Convert.ToInt32(Request.Form["ddl_yearly_day_od_week"]) - 1, "1");
                            //  string strDays = "00000000";
                            _objSupSch.DayOfWeek = strDays;
                            _objSupSch.MonthOfYear = Convert.ToInt32(Request.Form["ddl_yearly_month_by_week"]);
                        }
                    }
                    #endregion
                    #region Monthly
                    if (frequencyvalue == "M")
                    {
                        string Monthly_type = Request.Form["monthlyType"];
                        if (Monthly_type== "byDay")
                        {
                            sbFrequencyTypeCode.Append("D");
                            _objSupSch.DateOfMonth = Convert.ToInt32(Request.Form["ddl_monthly_day"]);
                            _objSupSch.Recur_No = Convert.ToInt32(Request.Form["ddl_monthly_monthly"]);
                        }
                        else if (Monthly_type== "byWeek")
                        {
                            sbFrequencyTypeCode.Append("W");
                            _objSupSch.WeekOfMonth = Convert.ToInt32(Request.Form["ddl_monthly_nthday"]);
                            string strDays = Convert.ToString("0000000").Remove(Convert.ToInt32(Request.Form["ddl_monthly_day_of_week"]) - 1, 1).Insert(Convert.ToInt32(Request.Form["ddl_monthly_day_of_week"]) - 1, "1");
                            _objSupSch.DayOfWeek = strDays;
                            //_objSupSch.DayOfWeek = ("1" + strDays.Substring(0, (Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1))).PadLeft(7, '0');
                            _objSupSch.Recur_No = Convert.ToInt32(Request.Form["ddl_monthly_month_by_week"]);
                        }
                    }
                    #endregion
                    #region Weekly
                    if (frequencyvalue == "W")
                    {
                        //_objSupSch.Recur_No = Convert.ToInt32(txtRecur_Weekly.Text);
                        
                        string test = Request.Form["dayOfWeek"];

                        int[] nums = test.Split(',').Select(int.Parse).ToArray();
                        //1001011  2571  1100101
                        StringBuilder sbDay = new StringBuilder("0000000");
                        foreach (int i in nums)
                        {
                            if (i != 1)
                            {
                                sbDay[Convert.ToInt32(i)-2] = '1';
                                // selected.Add(item);
                            }
                            else
                            {
                                sbDay[Convert.ToInt32(6)] = '1';
                            }
                        }  
                        _objSupSch.DayOfWeek = sbDay.ToString();
                    }
                    #endregion
                    #region Daily
                    if (frequencyvalue == "D")
                    {
                        _objSupSch.Recur_No = Convert.ToInt32(Request.Form["ddl_daily"]);
                    }
                    #endregion
                    #region Hourly
                    if (frequencyvalue == "H")
                    {
                        _objSupSch.Recur_No =Convert.ToInt32(Request.Form["ddl_hourly"]);
                    }
                    #endregion
                    _objSupSch.FrequencyTypeCode = sbFrequencyTypeCode.ToString();
                    _objSupSch.Status = Convert.ToString(ddlStatus.SelectedValue);
                    //Need To Do
                    int startHour = Convert.ToInt32(Request.Form["ddl_clock_hour"]);
                    int startminute = Convert.ToInt32(Request.Form["ddl_clock_minute"]);
                    _objSupSch.StartTime = Convert.ToString(startHour + ":" + startminute);

                    //_objSupSch.StartTime = Convert.ToString(timepickerStart_Hourly.Value);
                    //_objSupSch.EndTime = Convert.ToString(timepickerEnd_Hourly.Value);
                }
                List<string> selected = new List<string>();
                foreach (ListItem item in Checkentitysequence.Items)
                    if (item.Selected)
                    {
                        selected.Add(item.Text);
                        _objSupSch.lstEnity = selected.ToArray();
                    }
                _objSupSch.CronExpression = expression;
                MDMSVC.DC_Message _objMsg = _objSchedularService.AddUpdateSchedule(_objSupSch);
               
                if (_objMsg != null)
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, "" + _objMsg.StatusMessage, (BootstrapAlertType)_objMsg.StatusCode);
                LoadPageData();
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PreviousSave()
        {
            //if(rdbFrequency.SelectedValue == )
            //if (true)
            //{
            //List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
            //_objresult = GetListOfScheduleBySupplier();
            //Guid SupplierScheduleID = Guid.Empty;
            //Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
            //MDMSVC.DC_Supplier_Schedule _objSupSch = new MDMSVC.DC_Supplier_Schedule();
            //if (_objresult != null && _objresult.Count > 0)
            //{
            //    SupplierScheduleID = _objresult[0].SupplierScheduleID;
            //    mySupplier_Id = _objresult[0].Suppllier_ID;
            //    _objSupSch.SupplierScheduleID = SupplierScheduleID;
            //    _objSupSch.Create_User = _objSupSch.Create_User;
            //    _objSupSch.Create_Date = _objSupSch.Create_Date;
            //    _objSupSch.Edit_Date = DateTime.Now;
            //    _objSupSch.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

            //}
            //if (SupplierScheduleID == Guid.Empty)
            //{
            //    _objSupSch.Create_Date = DateTime.Now;
            //    _objSupSch.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
            //}

            //_objSupSch.Suppllier_ID = mySupplier_Id;

            //_objSupSch.ISXMLSupplier = Convert.ToBoolean(Convert.ToInt16(rdbtnIsAPIXMLSupplier.SelectedValue));
            //_objSupSch.ISUpdateFrequence = Convert.ToBoolean(Convert.ToInt16(rdbIsUpdateFrequencyRequired.SelectedValue));
            //StringBuilder sbFrequencyTypeCode = new StringBuilder();
            //sbFrequencyTypeCode.Append(rdbFrequency.SelectedValue);
            //if (Convert.ToBoolean(Convert.ToInt16(rdbtnIsAPIXMLSupplier.SelectedValue)))
            //{
            //    #region Yearly
            //    if (rdbFrequency.SelectedValue.ToString() == "Y")
            //    {
            //        _objSupSch.Recur_No = Convert.ToInt32(txtRecurEvery_Year.Text);
            //        if (rbDay_Year.Checked)
            //        {
            //            sbFrequencyTypeCode.Append("M");
            //            _objSupSch.MonthOfYear = Convert.ToInt32(ddlMonths_Year.SelectedValue);
            //            _objSupSch.DateOfMonth = Convert.ToInt32(txtMonthDay_Year.Text);
            //        }
            //        else if (rbOnthe_Year.Checked)
            //        {
            //            sbFrequencyTypeCode.Append("W");
            //            _objSupSch.WeekOfMonth = Convert.ToInt32(ddlWeek_Year.SelectedValue.ToString());
            //            string strDays = Convert.ToString("0000000").Remove(Convert.ToInt32(ddlMonthWeekList_Year.SelectedValue) - 1, 1).Insert(Convert.ToInt32(ddlMonthWeekList_Year.SelectedValue) - 1, "1");
            //            //  string strDays = "00000000";
            //            _objSupSch.DayOfWeek = strDays;
            //            _objSupSch.MonthOfYear = Convert.ToInt32(ddlMonths_Year.SelectedValue);
            //        }
            //    }
            //    #endregion
            //    #region Monthly
            //    if (rdbFrequency.SelectedValue.ToString() == "M")
            //    {
            //        if (rblDays_Monthly.Checked)
            //        {
            //            sbFrequencyTypeCode.Append("D");
            //            _objSupSch.DateOfMonth = Convert.ToInt32(txtDayOf_Monthly.Text);
            //            _objSupSch.Recur_No = Convert.ToInt32(txtDayOfEvery_Monthly.Text);
            //        }
            //        else if (rbThe_Montly.Checked)
            //        {
            //            sbFrequencyTypeCode.Append("W");
            //            _objSupSch.WeekOfMonth = Convert.ToInt32(ddlTheSequencyWeek_Month.SelectedValue);
            //            string strDays = Convert.ToString("0000000").Remove(Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1, 1).Insert(Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1, "1");
            //            _objSupSch.DayOfWeek = strDays;
            //            //_objSupSch.DayOfWeek = ("1" + strDays.Substring(0, (Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1))).PadLeft(7, '0');
            //            _objSupSch.Recur_No = Convert.ToInt32(txtOccurEvery_Month.Text);
            //        }
            //    }
            //    #endregion
            //    #region Weekly
            //    if (rdbFrequency.SelectedValue.ToString() == "W")
            //    {
            //        _objSupSch.Recur_No = Convert.ToInt32(txtRecur_Weekly.Text);

            //        StringBuilder sbDay = new StringBuilder("0000000");
            //        foreach (ListItem item in chckbxWeek_Weekly.Items)
            //            if (item.Selected)
            //            {
            //                sbDay[Convert.ToInt32(item.Value) - 1] = '1';
            //                // selected.Add(item);
            //            }
            //        _objSupSch.DayOfWeek = sbDay.ToString();
            //    }
            //    #endregion
            //    #region Daily
            //    if (rdbFrequency.SelectedValue.ToString() == "D")
            //    {
            //        _objSupSch.Recur_No = Convert.ToInt32(txtOccur_Daily.Text);
            //    }
            //    #endregion
            //    #region Hourly
            //    if (rdbFrequency.SelectedValue.ToString() == "H")
            //    {
            //        _objSupSch.Recur_No = Convert.ToInt32(txtRecur_Hourly.Text);
            //    }
            //    #endregion
            //    _objSupSch.FrequencyTypeCode = sbFrequencyTypeCode.ToString();
            //    _objSupSch.Status = Convert.ToString(ddlStatus.SelectedValue);
            //    _objSupSch.StartTime = Convert.ToString(timepickerStart_Hourly.Value);
            //    _objSupSch.EndTime = Convert.ToString(timepickerEnd_Hourly.Value);
            //}
            //List<string> selected = new List<string>();
            //foreach (ListItem item in Checkentitysequence.Items)
            //    if (item.Selected)
            //    {
            //        selected.Add(item.Text);
            //        _objSupSch.lstEnity = selected.ToArray();
            //    }
            //MDMSVC.DC_Message _objMsg = _objSchedularService.AddUpdateSchedule(_objSupSch);
            //if (_objMsg != null)
            //    BootstrapAlert.BootstrapAlertMessage(msgAlert, "" + _objMsg.StatusMessage, (BootstrapAlertType)_objMsg.StatusCode);
            //LoadPageData();
            //// }
        }


        protected void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                save();
            }
            catch (Exception ex)
            {
                throw;
            }

            supplerStaticData.Update();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetControls();
            supplerStaticData.Update();
        }

        protected void resetControls()
        {
            Checkentitysequence.ClearSelection();
            rdbtnIsAPIXMLSupplier.ClearSelection();
            Session["SupplierScheduleId"] = null;
            //rdbFrequency.ClearSelection();
            rdbIsUpdateFrequencyRequired.ClearSelection();
            //ddlTheSequencyWeek_Month.SelectedIndex = 0;
            //ddlTheDaysOf_Month.SelectedIndex = 0;
            //ddlMonths_Year.SelectedIndex = 0;
            //ddlWeek_Year.SelectedIndex = 0;
            //ddlMonthWeekList_Year.SelectedIndex = 0;
            //ddlYearMonth_Year.SelectedIndex = 0;
            //txtDayOf_Monthly.Text = String.Empty;
            //txtDayOfEvery_Monthly.Text = String.Empty;
            //txtMonthDay_Year.Text = String.Empty;
            //txtOccurEvery_Month.Text = String.Empty;
            //txtOccur_Daily.Text = String.Empty;
            //txtRecur_Hourly.Text = String.Empty;
            //chckbxWeek_Weekly.ClearSelection();

        }
        protected void rdbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(rdbFrequency.SelectedValue != "Yearly")
            //{
            //    rfvtxtRecurEvery_Year.Enabled = false;
            //}
        }

        protected void grdSupplierSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            Session["SupplierScheduleId"] = myRow_Id;
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName == "Select")
            {
                
                LoadSupplierDatabyScheduleId(myRow_Id);
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Supplier_Schedule_RQ newObj = new MDMSVC.DC_Supplier_Schedule_RQ
                {
                    SupplierScheduleID = myRow_Id,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                if (_objSchedularService.SoftDeleteDetails(newObj))
                {                   
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, "Supplier Scheduler has been deleted successfully", BootstrapAlertType.Success);
                    LoadSupplierScheduleData();
                };
            }           
        }

        private void LoadSupplierDatabyScheduleIdPrevious(Guid myRow_Id)
        {
            try
            {
                List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
                _objresult = GetListOfSupplierbyScheduleId(myRow_Id);
                //if (_objresult != null)
                //{
                //    if (_objresult.Count > 0)
                //    {
                //        Guid SupplierScheduleID = _objresult[0].SupplierScheduleID;
                //        if (Convert.ToBoolean(_objresult[0].ISXMLSupplier))
                //        {                                                       
                //            string entity = Convert.ToString(_objresult[0].Entity);                           
                //            foreach (ListItem item in Checkentitysequence.Items)
                //            {
                //                if (item.Text == entity)
                //                {
                //                    item.Selected = true;                                    
                //                }
                //            }                            
                //            string strActiveFrequencyType = _objresult[0].FrequencyTypeCode.Substring(0, 1);
                //            rdbFrequency.SelectedValue = strActiveFrequencyType;
                //            #region AllDiv display none
                //            divYearly.Attributes["class"] = "Divdisplay";
                //            divMonthly.Attributes["class"] = "Divdisplay";
                //            divWeekly.Attributes["class"] = "Divdisplay";
                //            divDaily.Attributes["class"] = "Divdisplay";
                //            divHourly.Attributes["class"] = "Divdisplay";
                //            #endregion
                //            if (strActiveFrequencyType == "Y")
                //            {
                //                string yearval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                //                divYearly.Attributes["class"] = "activediv";
                //                ddlYearMonth_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
                //                txtMonthDay_Year.Text = Convert.ToString(_objresult[0].DateOfMonth);
                //                txtRecurEvery_Year.Text = Convert.ToString(_objresult[0].Recur_No);
                //                if (yearval == "M")
                //                {
                //                    rbDay_Year.Checked = true;
                //                }
                //                else if (yearval == "W")
                //                {
                //                    rbOnthe_Year.Checked = true;
                //                    ddlMonthWeekList_Year.SelectedValue = Convert.ToString(_objresult[0].DayOfWeek.ToString().IndexOf("1") + 1);
                //                    ddlMonths_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
                //                }
                //            }
                //            if (strActiveFrequencyType == "M")
                //            {
                //                divMonthly.Attributes["class"] = "activediv";
                //                string monthval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                //                if (monthval == "D")
                //                {
                //                    rblDays_Monthly.Checked = true;
                //                    txtDayOf_Monthly.Text = Convert.ToString(_objresult[0].DateOfMonth);
                //                    txtDayOfEvery_Monthly.Text = Convert.ToString(_objresult[0].Recur_No);
                //                }
                //                else if (monthval == "W")
                //                {
                //                    rbThe_Montly.Checked = true;
                //                    ddlTheDaysOf_Month.SelectedValue = Convert.ToString(Convert.ToInt32(_objresult[0].DayOfWeek.ToString().IndexOf('1')) + 1);
                //                    ddlTheSequencyWeek_Month.SelectedValue = Convert.ToString(_objresult[0].WeekOfMonth);
                //                    txtOccurEvery_Month.Text = Convert.ToString(_objresult[0].Recur_No);
                //                }
                //            }
                //            if (strActiveFrequencyType == "W")
                //            {
                //                divWeekly.Attributes["class"] = "activediv";
                //                txtRecur_Weekly.Text = Convert.ToString(_objresult[0].Recur_No);
                //                StringBuilder sbDay = new StringBuilder(Convert.ToString(_objresult[0].DayOfWeek));
                //                char[] days = sbDay.ToString().ToCharArray();
                //                int i = 0;
                //                foreach (ListItem item in chckbxWeek_Weekly.Items)
                //                {
                //                    item.Selected = Convert.ToBoolean(Convert.ToInt32(days[i].ToString()));
                //                    i++;
                //                }

                //            }

                //            if (strActiveFrequencyType == "D")
                //            {
                //                divDaily.Attributes["class"] = "activediv";
                //                txtOccur_Daily.Text = Convert.ToString(_objresult[0].Recur_No);
                //            }
                //            if (strActiveFrequencyType == "H")
                //            {
                //                divHourly.Attributes["class"] = "activediv";
                //                txtRecur_Hourly.Text = Convert.ToString(_objresult[0].Recur_No);
                //            }
                //            timepickerStart_Hourly.Value = Convert.ToString(_objresult[0].StartTime);
                //            timepickerEnd_Hourly.Value = Convert.ToString(_objresult[0].EndTime);
                //            ddlStatus.SelectedValue = (Convert.ToString(_objresult[0].Status));

                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
                       
        }

        private void LoadSupplierDatabyScheduleId(Guid myRow_Id)
        {
            try
            {
                List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
                _objresult = GetListOfSupplierbyScheduleId(myRow_Id);
                if (_objresult != null)
                {
                    if (_objresult.Count > 0)
                    {
                        //msgAlert.Attributes.Add("style", "display:none");
                        Guid SupplierScheduleID = _objresult[0].SupplierScheduleID;
                        if (Convert.ToBoolean(_objresult[0].ISXMLSupplier))
                        {
                            string entity = Convert.ToString(_objresult[0].Entity);
                            foreach (ListItem item in Checkentitysequence.Items)
                            {
                                if (item.Text == entity)
                                {
                                    item.Selected = true;
                                }
                            }
                            Checkentitysequence.Enabled = false;

                            List<string> SchedularStartTime = new List<string>();
                            if (_objresult[0].StartTime!=null)
                                {
                                   
                                    SchedularStartTime = _objresult[0].StartTime.Split(':').ToList();
                                }
                            string strActiveFrequencyType = _objresult[0].FrequencyTypeCode.Substring(0, 1);
                            #region AllDiv display none

                            #endregion
                            if (strActiveFrequencyType == "Y")
                            {
                                string yearval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                               
                                if (yearval == "M")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','" + _objresult[0].MonthOfYear + "','" + _objresult[0].DateOfMonth + "','" + _objresult[0].Recur_No + "','0','0','" + yearval + "','"+SchedularStartTime[0]+"','"+SchedularStartTime[1]+"') ", true);
                                    
                                }
                                else if (yearval == "W")
                                {
                                    int MonthWeekListvalue = _objresult[0].DayOfWeek.ToString().IndexOf("1") + 1;
                                    

                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','" + _objresult[0].DateOfMonth + "','" + _objresult[0].WeekOfMonth + "','" + _objresult[0].Recur_No + "','" + MonthWeekListvalue + "','" + _objresult[0].MonthOfYear + "','" + yearval + "','" + SchedularStartTime[0] + "','" + SchedularStartTime[1] + "') ", true);
                                   
                                }
                            }
                            if (strActiveFrequencyType == "M")
                            {
                                string monthval = _objresult[0].FrequencyTypeCode.Substring(1, 1);                            

                                

                                if (monthval == "D")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','" + _objresult[0].DateOfMonth + "','0','" + _objresult[0].Recur_No + "','0','0','" + monthval + "','" + SchedularStartTime[0] + "','" + SchedularStartTime[1] + "') ", true);

                                   
                                }
                                else if (monthval == "W")
                                {
                                    int thedaysOf_Month = Convert.ToInt32(_objresult[0].DayOfWeek.ToString().IndexOf('1')) + 1;
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','" + thedaysOf_Month + "','" + _objresult[0].WeekOfMonth + "','"+ _objresult[0].Recur_No + "','0','0','" + monthval + "','" + SchedularStartTime[0] + "','" + SchedularStartTime[1] + "') ", true);

                                  
                                }
                            }
                            if (strActiveFrequencyType == "W")
                            {
                                
                                StringBuilder sbDay = new StringBuilder(Convert.ToString(_objresult[0].DayOfWeek));
                                
                                char[] days = sbDay.ToString().ToCharArray();
                                //List<int> test = new List<int>();
                                List<char> list = sbDay.ToString().ToList();
                                string weekdays = "";
                                weekdays = string.Join(",", list.Select(n => n.ToString()).ToArray());
                                                               
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','" + _objresult[0].MonthOfYear + "','" + _objresult[0].DateOfMonth + "','" + _objresult[0].Recur_No + "','" + 0 + "','" + _objresult[0].MonthOfYear + "','" + weekdays + "','" + SchedularStartTime[0] + "','" + SchedularStartTime[1] + "') ", true);
                               

                            }

                            if (strActiveFrequencyType == "D")
                            {
                                
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','0','0','" + _objresult[0].Recur_No + "','0','0','0','" + SchedularStartTime[0] + "','" + SchedularStartTime[1] + "') ", true);
                              
                            }
                            if (strActiveFrequencyType == "H")
                            {
                            

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "GetSupplierScheduleDetailOnEdit('" + strActiveFrequencyType + "','0','0','" + _objresult[0].Recur_No + "','0','0','0','" + SchedularStartTime[0] + "','" + SchedularStartTime[1] + "') ", true);
                               
                            }
                            //timepickerStart_Hourly.Value = Convert.ToString(_objresult[0].StartTime);
                            //timepickerEnd_Hourly.Value = Convert.ToString(_objresult[0].EndTime);
                            //ddlStatus.SelectedValue = (Convert.ToString(_objresult[0].Status));

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private List<DC_Supplier_Schedule> GetListOfSupplierbyScheduleId(Guid myRow_Id)
        {
            try
            {
                Guid mySupplier_Id = myRow_Id;
                MDMSVC.DC_Supplier_Schedule_RQ _objSearch = new MDMSVC.DC_Supplier_Schedule_RQ();
                _objSearch.SupplierScheduleID = mySupplier_Id;
                var _objresult = _objSchedularService.GetSchedule(_objSearch);
                return _objresult;
            }
            catch (Exception)
            {
            }
            return null;
        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), DateTime.Now.Ticks.ToString(), "NewSupplierSchedular();", true);
            Checkentitysequence.Enabled = true;

        }
    }
}