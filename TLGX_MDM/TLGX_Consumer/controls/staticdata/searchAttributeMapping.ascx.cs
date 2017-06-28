using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class searchAttributeMapping : System.Web.UI.UserControl
    {
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindSuppliers(ddlSearchSupplier);
                BindSystemAttributeTypes(ddlSearchAttributeType);
            }
        }

        private void BindSuppliers(DropDownList ddl)
        {
            Controller.MasterDataSVCs mdObj = new Controller.MasterDataSVCs();
            var result = mdObj.GetSupplier(new MDMSVC.DC_Supplier_Search_RQ());

            ddl.DataSource = result;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Supplier_Id";
            ddl.DataBind();

            result = null;
            mdObj = null;
        }

        private void BindSystemAttributeTypes(DropDownList ddl)
        {
            Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
            string AttributeOptionFor = "SupplierInfo";

            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "GenericAttributeType").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();

            LookupAtrributes = null;
        }

        private void BindSystemStatus(DropDownList ddl)
        {
            Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
            string AttributeOptionFor = "ProductSupplierMapping";

            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "MappingStatus").MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();

            LookupAtrributes = null;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlSearchAttributeType.ClearSelection();
            ddlSearchAttributeType.SelectedIndex = 0;
            ddlSearchSupplier.ClearSelection();
            ddlSearchSupplier.SelectedIndex = 0;
            grdSearchResults.DataSource = null;
            grdSearchResults.DataBind();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            msgAlert.Style.Add("display", "none");
            msgAlertHdr.Style.Add("display", "none");

            frmAttributeMapping.ChangeMode(FormViewMode.Insert);
            var dummyDataSource = new List<MDMSVC.DC_MasterAttributeMapping>();
            dummyDataSource.Add(new MDMSVC.DC_MasterAttributeMapping { MasterAttributeMapping_Id = Guid.NewGuid() });
            frmAttributeMapping.DataSource = dummyDataSource;
            frmAttributeMapping.DataBind();

            DropDownList ddlSuppliers = (DropDownList)frmAttributeMapping.FindControl("ddlSuppliers");
            TextBox txtSupplierAttributeName = (TextBox)frmAttributeMapping.FindControl("txtSupplierAttributeName");
            DropDownList ddlAttributeType = (DropDownList)frmAttributeMapping.FindControl("ddlAttributeType");

            ddlSuppliers.Items.Clear();
            ddlAttributeType.Items.Clear();

            BindSuppliers(ddlSuppliers);
            BindSystemAttributeTypes(ddlAttributeType);

            ddlSuppliers.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0", Enabled = true, Selected = true });
            ddlAttributeType.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0", Enabled = true, Selected = true });

            txtSupplierAttributeName.Text = string.Empty;

        }

        protected void frmAttributeMapping_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlSuppliers = (DropDownList)frmAttributeMapping.FindControl("ddlSuppliers");
            TextBox txtSupplierAttributeName = (TextBox)frmAttributeMapping.FindControl("txtSupplierAttributeName");
            DropDownList ddlAttributeType = (DropDownList)frmAttributeMapping.FindControl("ddlAttributeType");

            if (e.CommandName.ToString() == "Add")
            {
                MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                {
                    MasterAttributeMapping_Id = Guid.NewGuid(),
                    SupplierMasterAttribute = txtSupplierAttributeName.Text,
                    Supplier_Id = Guid.Parse(ddlSuppliers.SelectedValue),
                    SystemMasterAttribute_Id = Guid.Parse(ddlAttributeType.SelectedValue),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                    Status = "MAPPED"
                };

                var result = MapSvc.Mapping_Attribute_Add(newObj);

                BootstrapAlert.BootstrapAlertMessage(msgAlert, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));

                if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    BinfFormViewInEditMode(newObj.MasterAttributeMapping_Id);
                }
            }

            else if (e.CommandName.ToString() == "Modify")
            {
                DropDownList ddlStatus = (DropDownList)frmAttributeMapping.FindControl("ddlStatus");

                Guid myRow_Id = Guid.Parse(grdSearchResults.SelectedDataKey.Value.ToString());

                MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                {
                    MasterAttributeMapping_Id = myRow_Id,
                    SupplierMasterAttribute = txtSupplierAttributeName.Text,
                    Supplier_Id = Guid.Parse(ddlSuppliers.SelectedValue),
                    SystemMasterAttribute_Id = Guid.Parse(ddlAttributeType.SelectedValue),
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                    Status = ddlStatus.SelectedItem.Text
                };

                var result = MapSvc.Mapping_Attribute_Update(newObj);
                BootstrapAlert.BootstrapAlertMessage(msgAlert, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));

            };

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillSearchGrid(0);
        }

        protected void grdSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillSearchGrid(e.NewPageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSearchGrid(0);
        }

        public void fillSearchGrid(int pageIndex)
        {
            var SupplierId = ddlSearchSupplier.SelectedValue;
            var AttributeId = ddlSearchAttributeType.SelectedValue;
            var PageSize = ddlShowEntries.SelectedItem.Text;

            var searchResult = MapSvc.Mapping_Attribute_Search(
                new MDMSVC.DC_MasterAttributeMapping_RQ
                {
                    MasterAttributeType_Id = Guid.Parse(AttributeId),
                    Supplier_Id = Guid.Parse(SupplierId),
                    PageNo = pageIndex,
                    PageSize = int.Parse(PageSize)
                });

            grdSearchResults.DataSource = searchResult;
            grdSearchResults.DataBind();
        }

        protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }

        protected void grdSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msgAlert.Style.Add("display", "none");
            msgAlertHdr.Style.Add("display", "none");

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {
                BinfFormViewInEditMode(myRow_Id);
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                {
                    MasterAttributeMapping_Id = myRow_Id,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = false,
                };

                var result = MapSvc.Mapping_Attribute_Update(newObj);
                BootstrapAlert.BootstrapAlertMessage(msgAlertHdr, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));

                fillSearchGrid(grdSearchResults.PageIndex);

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                {
                    MasterAttributeMapping_Id = myRow_Id,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                };

                var result = MapSvc.Mapping_Attribute_Update(newObj);
                BootstrapAlert.BootstrapAlertMessage(msgAlertHdr, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));

                fillSearchGrid(grdSearchResults.PageIndex);

            }
        }

        private void BinfFormViewInEditMode(Guid myRow_Id)
        {
            frmAttributeMapping.ChangeMode(FormViewMode.Edit);

            var ListData = new List<MDMSVC.DC_MasterAttributeMapping>();
            var data = MapSvc.Mapping_Attribute_Get(myRow_Id);
            ListData.Add(data);

            frmAttributeMapping.DataSource = ListData;
            frmAttributeMapping.DataBind();

            DropDownList ddlSuppliers = (DropDownList)frmAttributeMapping.FindControl("ddlSuppliers");
            TextBox txtSupplierAttributeName = (TextBox)frmAttributeMapping.FindControl("txtSupplierAttributeName");
            DropDownList ddlAttributeType = (DropDownList)frmAttributeMapping.FindControl("ddlAttributeType");
            DropDownList ddlStatus = (DropDownList)frmAttributeMapping.FindControl("ddlStatus");

            ddlSuppliers.Items.Clear();
            ddlAttributeType.Items.Clear();
            ddlStatus.Items.Clear();

            BindSuppliers(ddlSuppliers);
            BindSystemAttributeTypes(ddlAttributeType);
            BindSystemStatus(ddlStatus);

            ddlSuppliers.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0", Enabled = true, Selected = true });
            ddlAttributeType.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0", Enabled = true, Selected = true });

            txtSupplierAttributeName.Text = data.SupplierMasterAttribute;

            if (ddlStatus.Items.FindByText(data.Status) != null)
            {
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(data.Status));
            }

            if (ddlSuppliers.Items.FindByValue(data.Supplier_Id.ToString()) != null)
            {
                ddlSuppliers.SelectedIndex = ddlSuppliers.Items.IndexOf(ddlSuppliers.Items.FindByValue(data.Supplier_Id.ToString()));
            }

            if (ddlAttributeType.Items.FindByValue(data.SystemMasterAttribute_Id.ToString()) != null)
            {
                ddlAttributeType.SelectedIndex = ddlAttributeType.Items.IndexOf(ddlAttributeType.Items.FindByValue(data.SystemMasterAttribute_Id.ToString()));
            }

            //Fill Attribute Mapping Values
            var mapattrvalData = MapSvc.Mapping_AttributeValue_Get(myRow_Id);
            GridView grdMappingAttrVal = (GridView)frmAttributeMapping.FindControl("grdMappingAttrVal");
            grdMappingAttrVal.DataSource = mapattrvalData;
            grdMappingAttrVal.DataBind();
        }

        protected void grdMappingAttrVal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            CheckBox chkAttrValIsActive = (CheckBox)row.FindControl("chkAttrValIsActive");
            TextBox txtSupplierAttributeValue = (TextBox)row.FindControl("txtSupplierAttributeValue");
            Label SystemMasterAttributeValueId = (Label)row.FindControl("lblSystemMasterAttributeValueId");

            if (e.CommandName.ToString() == "EditVal")
            {
                MDMSVC.DC_MasterAttributeValueMapping newObj = new MDMSVC.DC_MasterAttributeValueMapping
                {
                    MasterAttributeMapping_Id = Guid.Parse(frmAttributeMapping.DataKey.Value.ToString()),
                    MasterAttributeValueMapping_Id = myRow_Id,
                    IsActive = chkAttrValIsActive.Checked,
                    SupplierMasterAttributeValue = txtSupplierAttributeValue.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    SystemMasterAttributeValue_Id = Guid.Parse(SystemMasterAttributeValueId.Text)
                };

                var result = MapSvc.Mapping_AttributeValue_Update(newObj);
                BootstrapAlert.BootstrapAlertMessage(msgAlert, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));
            }
        }

    }
}
