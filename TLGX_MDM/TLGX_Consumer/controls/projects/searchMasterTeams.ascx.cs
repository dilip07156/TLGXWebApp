using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.projects
{
    public partial class searchMasterTeams : System.Web.UI.UserControl
    {
        MasterDataDAL masterdata = new MasterDataDAL();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "HotelInfo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillstatus();
                fillteams();
            }

            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {

                var teamData = (from s in context.m_Teams
                                   select new Models.dataProject.Teams
                                   {

                                       Team_Id = s.Team_ID,
                                       Team_Name = s.Team_Name,
                                       Status = s.Status,
                                       CREATE_DATE = s.CREATE_DATE,
                                       CREATE_USER = s.CREATE_USER,
                                       UPDATE_DATE= s.UPDATE_DATE,
                                       UPDATE_USER = s.UPDATE_USER
                                   }
                                  ).ToList();

                grdTeamList.DataSource = teamData;
                grdTeamList.DataBind();

            }



        }

        private void fillteams()
        {
            
        }

        private void fillstatus()
        {
            DropDownList ddlStatus = (DropDownList)frmTeamMasters.FindControl("ddlStatus");
            //ddlStatus.DataSource = masterdata.getAllStatuses();
            MasterDataSVCs _objMasterData = new MasterDataSVCs();
            ddlStatus.DataSource = _objMasterData.GetAllStatuses();
            ddlStatus.DataTextField = "Status_Name";
            ddlStatus.DataValueField = "Status_Short";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("---ALL---", ""));
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText("ACTIVE"));
        }

        protected void frmTeamMasters_DataBinding(object sender, EventArgs e)
        {

        }

        protected void frmTeamMasters_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }
    }
}