using Microsoft.Reporting.WebForms;
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
    public partial class CountryWiseDashBoardReport : System.Web.UI.Page
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();

        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (!_obj.IsRoleAuthorizedForUrl())
            {
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdown();
                //ShowReport();
            }
        }



        private void LoadDropdown()
        {
            fillPriorityList();
            fillKeysList();
            fillRanksList();
        }





        //GAURAV_TMAP_876
        private void fillPriorityList()
        {
            ddlPriorities.Items.Clear();
            ddlPriorities.DataSource = _objMasterSVC.GetPrioritiesOfCountryMaster();
            ddlPriorities.DataValueField = "Value";
            ddlPriorities.DataTextField = "Name";
            ddlPriorities.DataBind();
        }


        private void fillKeysList()
        {
            ddlKeys.Items.Clear();
            ddlKeys.DataSource = _objMasterSVC.GetKeysOfCountryMaster();
            ddlKeys.DataValueField = "Value";
            ddlKeys.DataTextField = "Name";
            ddlKeys.DataBind();
        }

        private List<string> GetSelectedList(ListBox lst)
        {
            List<string> strList = new List<string>();
            if (lst.Items.Count > 0)
            {
                foreach (ListItem item in lst.Items)
                {
                    if (item.Selected)
                    {
                        strList.Add(item.Value);
                    }
                }
                return strList;
            }
            else
            {
                return strList;
            }
        }

        private void fillRanksList()
        {
            ddlRanks.Items.Clear();
            ddlRanks.DataSource = _objMasterSVC.GetRanksOfCountryMaster();
            ddlRanks.DataValueField = "Value";
            ddlRanks.DataTextField = "Name";
            ddlRanks.DataBind();
        }


        protected void ShowReport()
        {
            var SelectedPriorities = GetSelectedList(ddlPriorities);
            //var Priority = ddlPriorities.Items.Count == SelectedPriorities.Count ? new List<string> { } : SelectedPriorities;
            var Priority = SelectedPriorities.Count == 0 ? new List<string> { } : SelectedPriorities;


            var SelectedKyes = GetSelectedList(ddlKeys);
            //var Keys = ddlKeys.Items.Count == SelectedKyes.Count ? new List<string> { } : SelectedKyes;
            var Keys = SelectedKyes.Count == 0 ? new List<string> { } : SelectedKyes;
            var SelectedRanks = GetSelectedList(ddlRanks);
            //var Ranks = ddlRanks.Items.Count == SelectedRanks.Count ? new List<string> { } : SelectedRanks;
            var Ranks = SelectedRanks.Count == 0 ? new List<string> { } : SelectedRanks;

            var report = new MDMSVC.DC_NewDashBoardReport_RQ();
            report.Priorities = Priority.ToArray();
            report.Keys = Keys.ToArray();
            report.Ranks = Ranks.ToArray();

            var DataSet1 = MapSvc.GetNewDashboardReport_CountryWise(report);
            ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
            CountryReportViewer.LocalReport.DataSources.Clear();
            CountryReportViewer.LocalReport.ReportPath = "staticdata/HotelMappingCountryReport.rdlc";
            CountryReportViewer.LocalReport.DataSources.Add(rds);
            CountryReportViewer.Visible = true;
            CountryReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
            CountryReportViewer.DataBind();
            CountryReportViewer.LocalReport.Refresh();
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {

            ShowReport();

        }
    }
}