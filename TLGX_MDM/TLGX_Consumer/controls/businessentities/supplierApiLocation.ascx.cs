using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierApiLocation : System.Web.UI.UserControl
    {
        #region Variables
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static Guid mySupplier_Id = Guid.Empty;

        #endregion
        #region Page Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                LoadEntities();
                LoadStatus();
                LoadApiLocations();
            }
        }
        #endregion


        private void LoadEntities()
        {
            ddlSupplierApiLocEntity.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("MappingFileConfig", "MappingEntity").MasterAttributeValues;
            ddlSupplierApiLocEntity.DataTextField = "AttributeValue";
            ddlSupplierApiLocEntity.DataValueField = "MasterAttributeValue_Id";
            ddlSupplierApiLocEntity.DataBind();
        }

        private void LoadStatus()
        {
            ddlSupplierApiLocStatus.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("SystemStatus", "Status").MasterAttributeValues;
            ddlSupplierApiLocStatus.DataTextField = "AttributeValue";
            ddlSupplierApiLocStatus.DataValueField = "MasterAttributeValue_Id";
            ddlSupplierApiLocStatus.DataBind();
        }

        private List<MDMSVC.DC_Supplier_ApiLocation> GetApiLocations(Guid? RowId)
        {
            MDMSVC.DC_Supplier_ApiLocation RQ = new MDMSVC.DC_Supplier_ApiLocation();
            if (RowId != null)
            {
                RQ.ApiLocation_Id = RowId ?? Guid.Empty;
            }
            RQ.Supplier_Id = mySupplier_Id;

            return _objMaster.Supplier_ApiLoc_Get(RQ);
        }

        private void LoadApiLocations()
        {
            gvSupplierApiLoc.DataSource = GetApiLocations(null);
            gvSupplierApiLoc.DataBind();
        }

        protected void gvSupplierApiLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "Select")
            {
                var result = GetApiLocations(myRow_Id);
                if (result.Count > 0)
                {
                    txtSupplierApiLocEndPoint.Text = result[0].ApiEndPoint;

                    ddlSupplierApiLocEntity.ClearSelection();
                    if (ddlSupplierApiLocEntity.Items.FindByValue(result[0].Entity_Id.ToString()) != null)
                    {
                        ddlSupplierApiLocEntity.Items.FindByValue(result[0].Entity_Id.ToString()).Selected = true;
                    }
                    else
                    {
                        ddlSupplierApiLocEntity.SelectedIndex = 0;
                    }

                    ddlSupplierApiLocStatus.ClearSelection();
                    if (ddlSupplierApiLocStatus.Items.FindByText(result[0].Status) != null)
                    {
                        ddlSupplierApiLocStatus.Items.FindByText(result[0].Status).Selected = true;
                    }
                    else
                    {
                        ddlSupplierApiLocStatus.SelectedIndex = 0;
                    }


                    lnkButtonAddUpdate.Text = "Modify";
                    lnkButtonAddUpdate.CommandName = "Modify";
                    lnkButtonAddUpdate.CommandArgument = myRow_Id.ToString();
                }
            }
        }

        protected void lnkButtonAddUpdate_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).CommandName == "Add")
            {
                var _msg = _objMaster.Supplier_ApiLoc_Add(new MDMSVC.DC_Supplier_ApiLocation
                {
                    ApiEndPoint = txtSupplierApiLocEndPoint.Text,
                    ApiLocation_Id = Guid.NewGuid(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Entity_Id = Guid.Parse(ddlSupplierApiLocEntity.SelectedValue),
                    Status = ddlSupplierApiLocStatus.SelectedItem.Text,
                    Supplier_Id = mySupplier_Id
                });
                ClearControls();
                LoadApiLocations();
                BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }
            else if (((LinkButton)sender).CommandName == "Modify")
            {
                Guid myRow_Id = Guid.Parse(((LinkButton)sender).CommandArgument);
                var _msg = _objMaster.Supplier_ApiLoc_Update(new MDMSVC.DC_Supplier_ApiLocation
                {
                    ApiEndPoint = txtSupplierApiLocEndPoint.Text,
                    ApiLocation_Id = myRow_Id,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Entity_Id = Guid.Parse(ddlSupplierApiLocEntity.SelectedValue),
                    Status = ddlSupplierApiLocStatus.SelectedItem.Text,
                    Supplier_Id = mySupplier_Id
                });
                ClearControls();
                LoadApiLocations();
                BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            }
        }

        protected void lnkButtonReset_Click(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            ClearControls();
        }

        private void ClearControls()
        {
            ddlSupplierApiLocEntity.ClearSelection();
            ddlSupplierApiLocEntity.SelectedIndex = 0;
            ddlSupplierApiLocStatus.ClearSelection();
            ddlSupplierApiLocStatus.SelectedIndex = 0;
            txtSupplierApiLocEndPoint.Text = string.Empty;
            lnkButtonAddUpdate.CommandArgument = string.Empty;
            lnkButtonAddUpdate.CommandName = "Add";
            lnkButtonAddUpdate.Text = "Add";
        }
    }
}