using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ucNonOperatingDays : System.Web.UI.UserControl
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getNonOperatingDays(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }

        private void getNonOperatingDays(int pagesize, int pageno)
        {
            try
            {
                
                Guid? Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                if (Activity_Flavour_Id != Guid.Empty)
                {
                    Guid guidActivity_Flavour_Id = Activity_Flavour_Id ?? Guid.Empty;
                    var result = AccSvc.GetActivityNonOperatingDays(guidActivity_Flavour_Id, pagesize, pageno);

                    if (result != null)
                    {
                        gvNonOperatingData.DataSource = result;
                        if (result.Count() > 0)
                        {
                            gvNonOperatingData.VirtualItemCount = result[0].TotalRecords ?? 0;
                            gvNonOperatingData.PageIndex = pageno;
                            gvNonOperatingData.PageSize = pagesize;
                        }
                        gvNonOperatingData.DataBind();

                    }
                    else
                    {
                        gvNonOperatingData.DataSource = null;
                        gvNonOperatingData.DataBind();
                    }
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool DateDifference(string FromDate, string EndDate)
        {
            DateTime fromdate = Convert.ToDateTime(FromDate);
            DateTime todate = Convert.ToDateTime(EndDate);
            if (fromdate <= todate)
            {
                //TimeSpan daycount = todate.Subtract(fromdate);
                //int dacount1 = Convert.ToInt32(daycount.Days) + 1;
                return false;
            }
            else
                return true;
        }

        protected void addNonOperatingDate_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            //string AddDays = btn.CommandArgument;

            string FromDate = txtFrom.Text;
            string EndDate = txtTo.Text;

            if (string.IsNullOrWhiteSpace(FromDate))
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please Select From Date", BootstrapAlertType.Danger);
                txtFrom.Focus();
            }
            else if (string.IsNullOrWhiteSpace(EndDate))
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please Select To Date", BootstrapAlertType.Danger);
                txtTo.Focus();
            }
            else if (DateDifference(FromDate, EndDate))
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "To date should be less than From date", BootstrapAlertType.Danger);
            }
            else
            {
                dvMsg.Style.Add("display", "none");
                var nonOperatingDays = new DC_Activity_OperatingDays();
                Guid ActivityDaysOfOperationId = Guid.NewGuid();

                nonOperatingDays.Activity_DaysOfOperation_Id = ActivityDaysOfOperationId;
                nonOperatingDays.Activity_Flavor_ID = new Guid(Request.QueryString["Activity_Flavour_Id"]); ;
                nonOperatingDays.FromDate = Convert.ToDateTime(FromDate);
                nonOperatingDays.EndDate = Convert.ToDateTime(EndDate);
                nonOperatingDays.IsOperatingDays = false;
                nonOperatingDays.IsActive = false;
                nonOperatingDays.CreateUser = System.Web.HttpContext.Current.User.Identity.Name;
                nonOperatingDays.EditUser = System.Web.HttpContext.Current.User.Identity.Name;
                List<DC_Activity_OperatingDays> nonOperatingDaysList = new List<DC_Activity_OperatingDays>();

                nonOperatingDaysList.Add(nonOperatingDays);
                if (nonOperatingDays.Activity_Flavor_ID != Guid.Empty)
                {
                    var result = AccSvc.AddUpdateActivityNonOperatingDays(nonOperatingDaysList);
                    BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, result.StatusMessage, BootstrapAlertType.Success);
                    getNonOperatingDays(gvNonOperatingData.PageSize, gvNonOperatingData.PageIndex);

                }
            }
        }

        protected void deleteNonOperatingDate_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);

            //string RemoveNonOperatingDays = btn.CommandArgument;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string pk = gvNonOperatingData.DataKeys[row.RowIndex].Values[0].ToString();
            gvNonOperatingData.PageSize = Convert.ToInt16(ddlShowEntries.SelectedValue);
            Guid ActivityDaysOfOperationId = Guid.Parse(pk);

            if (ActivityDaysOfOperationId != Guid.Empty)
            {
                var result = AccSvc.DeleteActivityNonOperatingDays(ActivityDaysOfOperationId);
                BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, result.StatusMessage, BootstrapAlertType.Success);
                getNonOperatingDays(gvNonOperatingData.PageSize, gvNonOperatingData.PageIndex);
            }

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            getNonOperatingDays(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), gvNonOperatingData.PageIndex);
        }

        protected void gvNonOperatingData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            dvMsgAlert.Style.Add("display", "none");
            getNonOperatingDays(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }

    }
}