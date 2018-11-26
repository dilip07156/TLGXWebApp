using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class manageSupplierScheduleTask : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        ScheduleDataSVCs _objScheduleDataSVCs = new ScheduleDataSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();
                fillentities();
                if (!String.IsNullOrWhiteSpace(Request.QueryString["RedirectFromAlert"].ToString()))
                    fillgriddata(Request.QueryString["RedirectFromAlert"].ToString());

               
                //var test= Roles.GetAllRoles();
            }
        }

        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        private void fillentities()
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "mapping", Name = "MappingEntity" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlEntity.Items.Clear();
                    ddlEntity.DataSource = result;
                    ddlEntity.DataTextField = "AttributeValue";
                    ddlEntity.DataValueField = "MasterAttributeValue_Id";
                    ddlEntity.DataBind();
                    
                }
        }

        protected void btnSearch_Click(object sender,EventArgs args)
        {
            fillgriddata("");
        }

        private void fillgriddata(string RedirectFromAlert)
        {
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            string strUserName = System.Web.HttpContext.Current.User.Identity.Name;
            MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
            RQ.UserName = System.Web.HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(RedirectFromAlert))
            {
                if (dtFrom.Value != string.Empty)
                    RQ.FromDate = Convert.ToDateTime(dtFrom.Value);
                if (dtFrom.Value != string.Empty)
                    RQ.FromDate = Convert.ToDateTime(dtFrom.Value);
                if (ddlSupplierName.SelectedItem.Value != "0")
                    RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
                if (ddlEntity.SelectedItem.Value != "0")
                    RQ.Entity = ddlEntity.SelectedValue;
                if (ddlstatus.SelectedItem.Value != "0")
                    RQ.Status = ddlstatus.SelectedValue;


                //RQ.PageNo = PageIndex;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                //RQ.SortBy = (SortBy + " " + SortEx).Trim();
            }
            
            var res = _objScheduleDataSVCs.GetScheduleTaskByRoll(RQ);
            grdSupplierScheduleTask.DataSource = res;
           
            grdSupplierScheduleTask.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            grdSupplierScheduleTask.DataBind();
        }

    }
}