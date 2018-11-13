using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierStaticDataHandling : System.Web.UI.UserControl
    {
        ScheduleDataSVCs _objSchedularService = new ScheduleDataSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
            }
        }
        private List<DC_Supplier_Schedule> GetListOfScheduleBySupplier()
        {
            try
            {
                Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                var _objresult = _objSchedularService.GetSchedule(Convert.ToString(mySupplier_Id));
                return _objresult;
            }
            catch (Exception)
            {
            }
            return null;
        }
        private void LoadPageData()
        {
            try
            {
                //Get Data for Schedule
                List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
                _objresult = GetListOfScheduleBySupplier();
                if (_objresult != null)
                {
                    if (_objresult.Count > 0)
                    {
                        Guid SupplierScheduleID = _objresult[0].SupplierScheduleID;
                        if (Convert.ToBoolean(_objresult[0].ISXMLSupplier))
                        {
                            string strActiveFrequencyType = _objresult[0].FrequencyTypeCode.Substring(0, 1);
                            rdbFrequency.SelectedValue = strActiveFrequencyType;
                            #region AllDiv display none
                            divYearly.Attributes["class"] = "Divdisplay";
                            divMonthly.Attributes["class"] = "Divdisplay";
                            divWeekly.Attributes["class"] = "Divdisplay";
                            divDaily.Attributes["class"] = "Divdisplay";
                            divHourly.Attributes["class"] = "Divdisplay";
                            #endregion
                            if (strActiveFrequencyType == "Y")
                            {
                                string yearval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                                divYearly.Attributes["class"] = "activediv";
                                ddlYearMonth_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
                                txtMonthDay_Year.Text = Convert.ToString(_objresult[0].DateOfMonth);
                                txtRecurEvery_Year.Text = Convert.ToString(_objresult[0].Recur_No);
                                if (yearval == "M")
                                {
                                    rbDay_Year.Checked = true;
                                }
                                else if (yearval == "W")
                                {
                                    rbOnthe_Year.Checked = true;
                                    ddlMonthWeekList_Year.SelectedValue = Convert.ToString(_objresult[0].DayOfWeek.ToString().IndexOf("1") + 1);
                                    ddlMonths_Year.SelectedValue = Convert.ToString(_objresult[0].MonthOfYear);
                                }
                            }
                            if (strActiveFrequencyType == "M")
                            {
                                divMonthly.Attributes["class"] = "activediv";
                                string monthval = _objresult[0].FrequencyTypeCode.Substring(1, 1);
                                if (monthval == "D")
                                {
                                    rblDays_Monthly.Checked = true;
                                    txtDayOf_Monthly.Text = Convert.ToString(_objresult[0].DateOfMonth);
                                    txtDayOfEvery_Monthly.Text = Convert.ToString(_objresult[0].Recur_No);
                                }
                                else if (monthval == "W")
                                {
                                    rbThe_Montly.Checked = true;
                                    ddlTheDaysOf_Month.SelectedValue = Convert.ToString(Convert.ToInt32(_objresult[0].DayOfWeek.ToString().IndexOf('1')) + 1);
                                    ddlTheSequencyWeek_Month.SelectedValue = Convert.ToString(_objresult[0].WeekOfMonth);
                                    txtOccurEvery_Month.Text = Convert.ToString(_objresult[0].Recur_No);
                                }
                            }
                            if (strActiveFrequencyType == "W")
                            {
                                divWeekly.Attributes["class"] = "activediv";
                                txtRecur_Weekly.Text = Convert.ToString(_objresult[0].Recur_No);
                                StringBuilder sbDay = new StringBuilder(Convert.ToString(_objresult[0].DayOfWeek));
                                char[] days = sbDay.ToString().ToCharArray();
                                int i = 0;
                                foreach (ListItem item in chckbxWeek_Weekly.Items)
                                {
                                    item.Selected = Convert.ToBoolean(Convert.ToInt32(days[i].ToString()));
                                    i++;
                                }

                            }

                            if (strActiveFrequencyType == "D")
                            {
                                divDaily.Attributes["class"] = "activediv";
                                txtOccur_Daily.Text = Convert.ToString(_objresult[0].Recur_No);
                            }
                            if (strActiveFrequencyType == "H")
                            {
                                divHourly.Attributes["class"] = "activediv";
                                txtRecur_Hourly.Text = Convert.ToString(_objresult[0].Recur_No);
                            }
                            timepickerStart_Hourly.Value = Convert.ToString(_objresult[0].StartTime);
                            timepickerEnd_Hourly.Value = Convert.ToString(_objresult[0].EndTime);
                            ddlStatus.SelectedValue = (Convert.ToString(_objresult[0].Status));

                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        protected void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                //if(rdbFrequency.SelectedValue == )
                //if (true)
                //{
                List<MDMSVC.DC_Supplier_Schedule> _objresult = new List<MDMSVC.DC_Supplier_Schedule>();
                _objresult = GetListOfScheduleBySupplier();
                Guid SupplierScheduleID = Guid.Empty;
                Guid mySupplier_Id = Guid.Empty;
                if (_objresult != null && _objresult.Count > 0)
                {
                    SupplierScheduleID = _objresult[0].SupplierScheduleID;
                    mySupplier_Id = _objresult[0].Suppllier_ID;
                    MDMSVC.DC_Supplier_Schedule _objSupSch = new MDMSVC.DC_Supplier_Schedule();
                    if (SupplierScheduleID == Guid.Empty)
                    {
                        _objSupSch.Create_Date = DateTime.Now;
                        _objSupSch.Create_User = System.Web.HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        _objSupSch.Create_User = _objSupSch.Create_User;
                        _objSupSch.Create_Date = _objSupSch.Create_Date;
                        _objSupSch.Edit_Date = DateTime.Now;
                        _objSupSch.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

                    }
                    _objSupSch.SupplierScheduleID = SupplierScheduleID;
                    _objSupSch.Suppllier_ID = mySupplier_Id;

                    _objSupSch.ISXMLSupplier = Convert.ToBoolean(Convert.ToInt16(rdbtnIsAPIXMLSupplier.SelectedValue));
                    _objSupSch.ISUpdateFrequence = Convert.ToBoolean(Convert.ToInt16(rdbIsUpdateFrequencyRequired.SelectedValue));
                    StringBuilder sbFrequencyTypeCode = new StringBuilder();
                    sbFrequencyTypeCode.Append(rdbFrequency.SelectedValue);
                    if (Convert.ToBoolean(Convert.ToInt16(rdbtnIsAPIXMLSupplier.SelectedValue)))
                    {
                        #region Yearly
                        if (rdbFrequency.SelectedValue.ToString() == "Y")
                        {
                            _objSupSch.Recur_No = Convert.ToInt32(txtRecurEvery_Year.Text);
                            if (rbDay_Year.Checked)
                            {
                                sbFrequencyTypeCode.Append("M");
                                _objSupSch.MonthOfYear = Convert.ToInt32(ddlMonths_Year.SelectedValue);
                                _objSupSch.DateOfMonth = Convert.ToInt32(txtMonthDay_Year.Text);
                            }
                            else if (rbOnthe_Year.Checked)
                            {
                                sbFrequencyTypeCode.Append("W");
                                _objSupSch.WeekOfMonth = Convert.ToInt32(ddlWeek_Year.SelectedValue.ToString());
                                string strDays = Convert.ToString("0000000").Remove(Convert.ToInt32(ddlMonthWeekList_Year.SelectedValue) - 1, 1).Insert(Convert.ToInt32(ddlMonthWeekList_Year.SelectedValue) - 1, "1");
                                //  string strDays = "00000000";
                                _objSupSch.DayOfWeek = strDays;
                                _objSupSch.MonthOfYear = Convert.ToInt32(ddlMonths_Year.SelectedValue);
                            }
                        }
                        #endregion
                        #region Monthly
                        if (rdbFrequency.SelectedValue.ToString() == "M")
                        {
                            if (rblDays_Monthly.Checked)
                            {
                                sbFrequencyTypeCode.Append("D");
                                _objSupSch.DateOfMonth = Convert.ToInt32(txtDayOf_Monthly.Text);
                                _objSupSch.Recur_No = Convert.ToInt32(txtDayOfEvery_Monthly.Text);
                            }
                            else if (rbThe_Montly.Checked)
                            {
                                sbFrequencyTypeCode.Append("W");
                                _objSupSch.WeekOfMonth = Convert.ToInt32(ddlTheSequencyWeek_Month.SelectedValue);
                                string strDays = Convert.ToString("0000000").Remove(Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1, 1).Insert(Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1, "1");
                                _objSupSch.DayOfWeek = strDays;
                                //_objSupSch.DayOfWeek = ("1" + strDays.Substring(0, (Convert.ToInt32(ddlTheDaysOf_Month.SelectedValue) - 1))).PadLeft(7, '0');
                                _objSupSch.Recur_No = Convert.ToInt32(txtOccurEvery_Month.Text);
                            }
                        }
                        #endregion
                        #region Weekly
                        if (rdbFrequency.SelectedValue.ToString() == "W")
                        {
                            _objSupSch.Recur_No = Convert.ToInt32(txtRecur_Weekly.Text);
                            List<ListItem> selected = new List<ListItem>();
                            StringBuilder sbDay = new StringBuilder("0000000");
                            foreach (ListItem item in chckbxWeek_Weekly.Items)
                                if (item.Selected)
                                {
                                    sbDay[Convert.ToInt32(item.Value) - 1] = '1';
                                    // selected.Add(item);
                                }
                            _objSupSch.DayOfWeek = sbDay.ToString();
                        }
                        #endregion
                        #region Daily
                        if (rdbFrequency.SelectedValue.ToString() == "D")
                        {
                            _objSupSch.Recur_No = Convert.ToInt32(txtOccur_Daily.Text);
                        }
                        #endregion
                        #region Hourly
                        if (rdbFrequency.SelectedValue.ToString() == "H")
                        {
                            _objSupSch.Recur_No = Convert.ToInt32(txtRecur_Hourly.Text);
                        }
                        #endregion
                        _objSupSch.FrequencyTypeCode = sbFrequencyTypeCode.ToString();
                        _objSupSch.Status = Convert.ToString(ddlStatus.SelectedValue);
                        _objSupSch.StartTime = Convert.ToString(timepickerStart_Hourly.Value);
                        _objSupSch.EndTime = Convert.ToString(timepickerEnd_Hourly.Value);
                    }
                    MDMSVC.DC_Message _objMsg = _objSchedularService.AddUpdateSchedule(_objSupSch);
                    if (_objMsg != null)
                        BootstrapAlert.BootstrapAlertMessage(msgAlert, "" + _objMsg.StatusMessage, (BootstrapAlertType)_objMsg.StatusCode);
                    LoadPageData();
                    // }
                }

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
            rdbtnIsAPIXMLSupplier.ClearSelection();
            rdbFrequency.ClearSelection();
            rdbIsUpdateFrequencyRequired.ClearSelection();
            ddlTheSequencyWeek_Month.SelectedIndex = 0;
            ddlTheDaysOf_Month.SelectedIndex = 0;
            ddlMonths_Year.SelectedIndex = 0;
            ddlWeek_Year.SelectedIndex = 0;
            ddlMonthWeekList_Year.SelectedIndex = 0;
            ddlYearMonth_Year.SelectedIndex = 0;
            txtDayOf_Monthly.Text = String.Empty;
            txtDayOfEvery_Monthly.Text = String.Empty;
            txtMonthDay_Year.Text = String.Empty;
            txtOccurEvery_Month.Text = String.Empty;
            txtOccur_Daily.Text = String.Empty;
            txtRecur_Hourly.Text = String.Empty;
            chckbxWeek_Weekly.ClearSelection();

        }
        protected void rdbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(rdbFrequency.SelectedValue != "Yearly")
            //{
            //    rfvtxtRecurEvery_Year.Enabled = false;
            //}
        }
    }
}