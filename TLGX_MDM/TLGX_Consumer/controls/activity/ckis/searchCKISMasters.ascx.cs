using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ckis
{
    public partial class searchCKISMasters : System.Web.UI.UserControl
    {
        //TLGX_Consumer.Scripts.Styles.BootstrapStyles style = new Scripts.Styles.BootstrapStyles();
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();
        public static string AttributeOptionFor = "Activity";
        public static int PageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsearchcontrolmasters();
            }
        }

        private void fillsearchcontrolmasters()
        {
            fillcountries();
            fillckistype();
            fillactivitytype();
            fillstatus();
        }

        private void fillstatus()
        {
            fillattributes("SystemStatus", "Status", ddlStatus);
        }

        private void fillactivitytype()
        {
            fillattributes(AttributeOptionFor, "CKISActivityType", ddlCKISActivityType);
        }

        private void fillckistype()
        {
            fillattributes(AttributeOptionFor, "CKISType", ddlCKISType);
        }

        public void fillattributes(string masterfor, string attributename, DropDownList ddl)
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = masterfor;
            RQ.Name = attributename;
            var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
            ddl.DataSource = resvalues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        private void fillcountries()
        {
            MDMSVC.DC_Country_Search_RQ RQ = new MDMSVC.DC_Country_Search_RQ();
            RQ.PageNo = 0;
            RQ.PageSize = int.MaxValue;
            var result = mastersvc.GetCountryMasterData(RQ);
            ddlCountry.DataSource = result;
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
        }
        private void fillcities()
        {
            MDMSVC.DC_City_Search_RQ RQ = new MDMSVC.DC_City_Search_RQ();
            RQ.Country_Id = Guid.Parse(ddlCountry.SelectedValue);
            RQ.PageNo = 0;
            RQ.PageSize = int.MaxValue;
            var result = mastersvc.GetCityMasterData(RQ);
            ddlCity.DataSource = result;
            ddlCity.DataTextField = "Name";
            ddlCity.DataValueField = "City_Id";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
                fillcities();
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            ddlCity.Focus();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlCountry.SelectedIndex = 0;
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlCKISType.SelectedIndex = 0;
            ddlCKISActivityType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlPageSize.SelectedIndex = 0;
            txtHotelName.Text = "";
            grdCKISData.DataSource = null;
            grdCKISData.DataBind();
            lblTotalCount.Text = "0";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            fillmasterdata();
        }

        private void fillmasterdata()
        {
            List<MDMSVC.DC_Activity> res = new List<MDMSVC.DC_Activity>();
            searchactivity("", Guid.Empty, out res);
            grdCKISData.DataSource = res;
            if (res != null && res.Count > 0)
            {
                grdCKISData.VirtualItemCount = Convert.ToInt32(res[0].TotalRecord);
                lblTotalCount.Text = res[0].TotalRecord.ToString();
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Search Completed", BootstrapAlertType.Success);
            }
            else
            {
                lblTotalCount.Text = "0";
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "No Record Found", BootstrapAlertType.Warning);
            }
            grdCKISData.PageIndex = PageIndex;
            grdCKISData.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
            grdCKISData.DataBind();
        }

        public void searchactivity(string mode, Guid Activity_Id, out List<MDMSVC.DC_Activity> res)
        {
            MDMSVC.DC_Activity_Search_RQ RQParams = new MDMSVC.DC_Activity_Search_RQ();
            RQParams.PageNo = PageIndex;
            RQParams.PageSize = int.Parse(ddlPageSize.SelectedItem.Value);

            if (mode == "")
            {
                if (ddlCountry.SelectedValue != "0")
                    RQParams.Country = ddlCountry.SelectedItem.Text;
                if (ddlCity.SelectedValue != "0")
                    RQParams.City = ddlCity.SelectedItem.Text;
                if (ddlCKISType.SelectedValue != "0")
                    RQParams.ProductCategorySubType = ddlCKISType.SelectedItem.Text;
                if (ddlCKISActivityType.SelectedValue != "0")
                    RQParams.ProductCategory = ddlCKISActivityType.SelectedItem.Text;
                if (!string.IsNullOrEmpty(txtHotelName.Text))
                    RQParams.Name = txtHotelName.Text;
                if (ddlStatus.SelectedValue != "0")
                    RQParams.Status = ddlStatus.SelectedItem.Text;
            }
            else if (mode.ToUpper() == "BYID")
            {
                if (Activity_Id != Guid.Empty)
                    RQParams.Activity_Id = Activity_Id;
            }
            res = mastersvc.GetActivityMaster(RQParams);
        }

        protected void grdCKISData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdCKISData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            fillmasterdata();
        }

        protected void grdCKISData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                if (e.CommandName == "Manage")
                {
                    dvMsg.Style.Add("display", "none");
                    List<MDMSVC.DC_Activity> res = new List<MDMSVC.DC_Activity>();
                    searchactivity("BYID", myRow_Id, out res);
                    frmCKISMasterData.ChangeMode(FormViewMode.Edit);
                    frmCKISMasterData.DataSource = res;
                    frmCKISMasterData.DataBind();

                    List<MDMSVC.DC_Activity_Content> result = mastersvc.GetActivityContentMaster(new MDMSVC.DC_Activity_Content() { Activity_Id = myRow_Id });
                    if (result != null)
                    {
                        Repeater rptInclusions = (Repeater)frmCKISMasterData.FindControl("rptInclusions");
                        if (rptInclusions != null)
                        {
                            rptInclusions.DataSource = (from a in result where a.Content_Type.ToUpper() == "INCLUSION" select a);
                            rptInclusions.DataBind();
                        }
                        Repeater rptExclusion = (Repeater)frmCKISMasterData.FindControl("rptExclusion");
                        if (rptExclusion != null)
                        {
                            rptExclusion.DataSource = (from a in result where a.Content_Type.ToUpper() == "EXCLUSION" select a);
                            rptExclusion.DataBind();
                        }
                        Repeater rptNotes = (Repeater)frmCKISMasterData.FindControl("rptNotes");
                        if (rptNotes != null)
                        {
                            rptNotes.DataSource = (from a in result where a.Content_Type.ToUpper() == "NOTES" select a);
                            rptNotes.DataBind();
                        }
                    }
                    hdnFlag.Value = "false";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "javascript:showManageModal();", true);
                }
            }
            catch
            {
            }
        }

        protected void grdCKISData_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdCKISData_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdCKISData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void frmCKISMasterData_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }
    }
}