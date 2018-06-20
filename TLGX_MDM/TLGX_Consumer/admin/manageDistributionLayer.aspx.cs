using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.admin
{
    public partial class manageDistributionLayer : System.Web.UI.Page
    {
        Controller.MasterDataSVCs MasterSvc = new Controller.MasterDataSVCs();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

            if (!IsPostBack)
            {
                GetStaticHotelData();
                GetUpdatedDistributionLog();

            }
        }

        protected void GetStaticHotelData()
        {
            dvMsg.Style.Add("display", "none");
            MDMSVC.DC_SupplierEntity RQ = new MDMSVC.DC_SupplierEntity();
            var result = MasterSvc.GetStaticHotel(RQ);

            if (result != null)
            {
                grdSupplierEntity.DataSource = result;
                grdSupplierEntity.DataBind();

            }
            else
            {
                grdSupplierEntity.DataSource = null;
                grdSupplierEntity.DataBind();

            }


            foreach (GridViewRow rowitem in grdSupplierEntity.Rows)
            {
                if (rowitem.Cells[4].Text == "Running" || rowitem.Cells[4].Text == "Scheduled")
                {
                    foreach (GridViewRow row in grdSupplierEntity.Rows)
                    {
                        LinkButton refreshButton = (LinkButton)(row.FindControl("btnUpdate"));
                        refreshButton.Enabled = false;
                        refreshButton.BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                else
                {
                    foreach (GridViewRow row in grdSupplierEntity.Rows)
                    {
                        LinkButton refreshButton = (LinkButton)(row.FindControl("btnUpdate"));
                        refreshButton.Enabled = true;
                        refreshButton.BackColor = System.Drawing.Color.Green;
                    }

                }

            }
        }

        //For refreshing distribution log 
        protected void GetUpdatedDistributionLog()
        {
            
            MDMSVC.DC_RefreshDistributionDataLog RQ = new MDMSVC.DC_RefreshDistributionDataLog();
            var res = MasterSvc.GetRefreshDistributionLog(RQ);

            LastUpdatedCountryMaster.Text = (res.Where(x => x.Element == "Country" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedCountryMapping.Text = (res.Where(x => x.Element == "Country" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedCityMapping.Text = (res.Where(x => x.Element == "City" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedCityMaster.Text = (res.Where(x => x.Element == "City" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedHotelMapping.Text = (res.Where(x => x.Element == "Hotel" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedActivityMapping.Text = (res.Where(x => x.Element == "Activities" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedSupplierMapping.Text = (res.Where(x => x.Element == "Supplier" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedPortMaster.Text = (res.Where(x => x.Element == "Port" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedStateMaster.Text = (res.Where(x => x.Element == "State" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();

           


        }

        protected void btnRefreshCountryMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCountryMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Country Master Sync failed.", BootstrapAlertType.Danger);
            }

        }
        protected void btnRefreshCountryMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCountryMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
                            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Country Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void btnRefreshCityMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCityMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "City Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void btnRefreshCityMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCityMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "City Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void btnRefreshHotelMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshHotelMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Hotel Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void btnRefreshActivityMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshActivityMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Activity Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void btnRefreshSupplyMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshSupplyMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supply Master Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void btnRefreshPortMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshPortMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Port Master Sync failed.", BootstrapAlertType.Danger);
            }

        }

        protected void btnRefreshStateMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshStateMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Port Master Sync failed.", BootstrapAlertType.Danger);
            }
        }

        protected void grdSupplierEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                dvMsg.Style.Add("display", "none");
                Guid myRowId = Guid.Parse(e.CommandArgument.ToString());

                var res = MasterSvc.RefreshStaticHotel(Guid.Empty, myRowId);

                if (res != null)
                {
                    GetUpdatedDistributionLog();
                    GetStaticHotelData();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Staic Hotel Sync failed.", BootstrapAlertType.Danger);
                }
                
            }
        }

        protected void grdSupplierEntity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
                               
            {
                //coding for progress bar
                int intTotalCount = ((TLGX_Consumer.MDMSVC.DC_SupplierEntity)e.Row.DataItem).TotalCount ?? 0;
                int intMongoPushCount = ((TLGX_Consumer.MDMSVC.DC_SupplierEntity)e.Row.DataItem).MongoPushCount ?? 0;

                double progressWidth = intTotalCount != 0 ? Math.Round((Convert.ToDouble(intMongoPushCount) / Convert.ToDouble(intTotalCount)) * 100.0,2) : 0.0;

                //find div in row
                HtmlControl divCompleted = (HtmlControl)e.Row.FindControl("divCompleted");
                Label lblcompleted = (Label)e.Row.FindControl("lblcompleted");
                if (divCompleted != null && progressWidth > 0)
                {
                    divCompleted.Attributes.Remove("aria-valuenow");
                    divCompleted.Attributes.Add("aria-valuenow", progressWidth.ToString());

                    divCompleted.Attributes.Remove("style");
                    divCompleted.Attributes.Add("style", "width: " + progressWidth.ToString() + "%");

                    if (lblcompleted != null)
                        lblcompleted.Text = Convert.ToString(progressWidth) + "%";
                    
                }
                
            }
        }


        protected void TimerStaticData_Tick(object sender, EventArgs e)
        {
            
            foreach (GridViewRow rowitem in grdSupplierEntity.Rows)
            {
                if (rowitem.Cells[4].Text == "Running" || rowitem.Cells[4].Text == "Scheduled")
                {
                    //foreach (GridViewRow row in grdSupplierEntity.Rows)
                    //{
                        GetStaticHotelData();
                    //}
                   // break;
                }
            }


        }


    }
        
    
}