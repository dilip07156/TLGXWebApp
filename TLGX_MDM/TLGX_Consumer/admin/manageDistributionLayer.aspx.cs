using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.admin
{
    public partial class manageDistributionLayer : System.Web.UI.Page
    {
        Controller.MasterDataSVCs MasterSvc = new Controller.MasterDataSVCs();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));
            
                GetUpdatedDistributionLog();                       
        }

        //For refreshing distribution log 
        protected void GetUpdatedDistributionLog()
        {
            MDMSVC.DC_RefreshDistributionDataLog RQ = new MDMSVC.DC_RefreshDistributionDataLog();
            var res = MasterSvc.GetRefreshDistributionLog(RQ);

            LastUpdatedCountryMaster.Text = (res.Where(x => x.Element == "Country" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();                  
            LastUpdatedCountryMapping.Text = (res.Where(x => x.Element == "Country" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();                    
            LastUpdatedCityMapping.Text = ( res.Where(x => x.Element == "City" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();            
            LastUpdatedCityMaster.Text = (res.Where(x => x.Element == "City" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedHotelMapping.Text = (res.Where(x => x.Element == "Hotel" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedActivityMapping.Text = (res.Where(x => x.Element == "Activities" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedSupplierMapping.Text = ( res.Where(x => x.Element == "Supplier" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedPortMaster.Text= (res.Where(x => x.Element == "Port" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedStateMaster.Text = (res.Where(x => x.Element == "State" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
        }

        protected void btnRefreshCountryMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCountryMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
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
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Port Master Sync failed.", BootstrapAlertType.Danger);
            }
        }
    }
}