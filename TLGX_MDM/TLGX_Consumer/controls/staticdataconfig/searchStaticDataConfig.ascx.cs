using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.staticdataconfig
{
    public partial class searchStaticDataConfig : System.Web.UI.UserControl
    {
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();
        Controller.MappingSVCs mappingsvc = new Controller.MappingSVCs();
        public static string AttributeOptionFor = "MappingFileConfig";
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
            fillfor(ddlFor);
            fillsuppliers(ddlSupplierName);
            fillentity(ddlEntity);
            fillstatus();
        }

        private void fillfor(DropDownList ddl)
        {
            fillattributes(AttributeOptionFor, "AttributeFor", ddl);
            //Setting default Mapping while adding config
            if (ddl.ID.ToLower() == "ddladdfor")
                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(Convert.ToString("MAPPING")));
        }

        private void fillstatus()
        {
            fillattributes("SystemStatus", "Status", ddlStatus);
        }
        private void fillentity(DropDownList ddl)
        {
            fillattributes(AttributeOptionFor, "MappingEntity", ddl);
        }
        private void fillsuppliers(DropDownList ddl)
        {
            var supres = mastersvc.GetSupplierMasterData();

            if (supres != null && supres.Count > 0)
            {
                ddl.DataSource = supres;
                ddl.DataValueField = "Supplier_Id";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
        }

        public void fillattributes(string masterfor, string attributename, DropDownList ddl)
        {
            ddl.Items.Clear();
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

        protected void btnAddConfig_Click(object sender, EventArgs e)
        {
            dvMsg.Visible = false;
            fillfor(ddlAddFor);
            fillsuppliers(ddlAddSupplier);
            fillentity(ddlAddEntity);
            hdnFlag.Value = "false";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            fillconfigdata();
        }

        private void fillconfigdata()
        {
            MDMSVC.DC_SupplierImportAttributes_RQ RQ = new MDMSVC.DC_SupplierImportAttributes_RQ();

            if (ddlSupplierName.SelectedItem.Value != "0")
                RQ.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value);
            if (ddlEntity.SelectedItem.Value != "0")
                RQ.Entity = ddlEntity.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Value != "0")
                RQ.Status = ddlStatus.SelectedItem.Text;
            if (ddlFor.SelectedItem.Value != "0")
                RQ.For = ddlFor.SelectedItem.Text;
            RQ.PageNo = PageIndex;
            RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

            var res = mappingsvc.GetStaticDataMappingAttributes(RQ);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdMappingConfig.VirtualItemCount = res[0].TotalRecords;
                    lblTotalUploadConfig.Text = res[0].TotalRecords.ToString();
                }
                else
                    lblTotalUploadConfig.Text = "0";
            }
            else
                lblTotalUploadConfig.Text = "0";
            grdMappingConfig.DataSource = res;
            grdMappingConfig.PageIndex = PageIndex;
            grdMappingConfig.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            grdMappingConfig.DataBind();
            //dvMsg.Style.Add("display", "block");
            //BootstrapAlert.BootstrapAlertMessage(dvMsg, "Search Completed", BootstrapAlertType.Success);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            ddlFor.SelectedIndex = 0;
            ddlSupplierName.SelectedIndex = 0;
            ddlEntity.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlShowEntries.SelectedIndex = 0;
            grdMappingConfig.DataSource = null;
            grdMappingConfig.DataBind();
            lblTotalUploadConfig.Text = "0";
        }

        protected void grdMappingConfig_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMappingConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRow_Id = Guid.Parse(grdMappingConfig.DataKeys[index].Values[0].ToString());

                if (e.CommandName.ToString() == "SoftDelete")
                {
                    List<MDMSVC.DC_SupplierImportAttributes> RQ = new List<MDMSVC.DC_SupplierImportAttributes>();

                    MDMSVC.DC_SupplierImportAttributes newObj = new MDMSVC.DC_SupplierImportAttributes
                    {
                        SupplierImportAttribute_Id = myRow_Id,
                        For=grdMappingConfig.Rows[index].Cells[0].Text,
                        Entity = grdMappingConfig.Rows[index].Cells[1].Text,
                        Status = "INACTIVE",
                        EDIT_DATE = DateTime.Now,
                        EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name
                    };
                    if (grdMappingConfig.DataKeys[index].Values[1] != null)
                    {
                        newObj.Supplier_Id = Guid.Parse(grdMappingConfig.DataKeys[index].Values[1].ToString());
                    }
                    RQ.Add(newObj);
                    MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                    dc = mappingsvc.UpdateStaticDataMappingAttribute(RQ);
                    if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                    {
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Duplicate);
                        fillconfigdata();
                    }
                    else
                    {
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been deleted Successfully", BootstrapAlertType.Success);
                        fillconfigdata();
                    }
                }
                else if (e.CommandName.ToString() == "UnDelete")
                {

                    List<MDMSVC.DC_SupplierImportAttributes> RQ = new List<MDMSVC.DC_SupplierImportAttributes>();

                    MDMSVC.DC_SupplierImportAttributes newObj = new MDMSVC.DC_SupplierImportAttributes
                    {
                        SupplierImportAttribute_Id = myRow_Id,
                        For=grdMappingConfig.Rows[index].Cells[0].Text,
                        Entity = grdMappingConfig.Rows[index].Cells[1].Text,
                        Status = "ACTIVE",
                        EDIT_DATE = DateTime.Now,
                        EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name
                    };
                    if (grdMappingConfig.DataKeys[index].Values[1] != null)
                    {
                        newObj.Supplier_Id = Guid.Parse(grdMappingConfig.DataKeys[index].Values[1].ToString());
                    }
                    RQ.Add(newObj);
                    MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                    dc = mappingsvc.UpdateStaticDataMappingAttribute(RQ);
                    if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                    {
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Duplicate);
                        fillconfigdata();
                    }
                    else
                    {
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been un deleted Successfully", BootstrapAlertType.Success);
                        fillconfigdata();
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void grdMappingConfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            fillconfigdata();
        }

        protected void grdMappingConfig_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillconfigdata();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            MDMSVC.DC_SupplierImportAttributes newObj = new MDMSVC.DC_SupplierImportAttributes
            {
                SupplierImportAttribute_Id = Guid.NewGuid(),
                Supplier_Id = ddlAddFor.SelectedItem.Text.Trim().ToUpper() == "MATCHING" ? Guid.Empty : Guid.Parse(ddlAddSupplier.SelectedItem.Value),
                Entity = ddlAddEntity.SelectedItem.Text,
                Status = "ACTIVE",
                CREATE_DATE = DateTime.Now,
                CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name,
                For = ddlAddFor.SelectedItem.Text
            };

            MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
            dc = mappingsvc.AddStaticDataMappingAttribute(newObj);
            if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
            {
                BootstrapAlert.BootstrapAlertMessage(dvModalMsg, dc.StatusMessage, BootstrapAlertType.Duplicate);
                resetModalControls();
                hdnFlag.Value = "false";
            }
            else
            {
                //Added code to fill filter and show added data.
                ddlSupplierName.SelectedIndex = ddlAddSupplier.Items.IndexOf(ddlSupplierName.Items.FindByValue(Convert.ToString(newObj.Supplier_Id)));
                ddlFor.SelectedIndex = ddlAddFor.Items.IndexOf(ddlAddFor.Items.FindByText(Convert.ToString(newObj.For)));
                ddlEntity.SelectedIndex = ddlAddEntity.Items.IndexOf(ddlAddEntity.Items.FindByText(Convert.ToString(newObj.Entity)));
                fillconfigdata();
                dvMsg.Visible = true;
                BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);
                resetModalControls();
                hdnFlag.Value = "true";
            }
        }

        private void resetModalControls()
        {
            ddlAddFor.SelectedIndex = 0;
            ddlAddSupplier.SelectedIndex = 0;
            ddlAddEntity.SelectedIndex = 0;
        }
        protected void btnResetAdd_Click(object sender, EventArgs e)
        {
            resetModalControls();
            hdnFlag.Value = "false";
        }
    }
}