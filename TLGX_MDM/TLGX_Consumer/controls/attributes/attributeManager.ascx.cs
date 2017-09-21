using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.controls.staticdata;

namespace TLGX_Consumer.controls.attributes
{
    public partial class attributeManager : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMst = new MasterDataSVCs();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        public int intPageSize = 10;
        public int intPageIndex = 0;
        public int intPageSizeAttributeValue = 5;
        public int intPageNoAttributeValue = 0;
        public static Guid _MasterAttribute_Id = Guid.Empty;
        public static Guid _MasterAttributeValue_Id = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                FillPageData();
                BindSuppliers(ddlSuppliers);
                BindSystemStatus(ddlMappingStatus);
                BindSuppliers(ddlmresultsupplierlist);
                ddlmresultsupplierlist.Items.RemoveAt(0);
                ddlmresultsupplierlist.Items.Insert(0, new ListItem("--ALL --", "0"));

            }
        }

        public void FillPageData()
        {
            FillMasterFor(ddlMasterForSearch);
            PopulateAttibuteMaster();
        }

        private void FillMasterFor(DropDownList DDL)
        {
            var result = _objMst.GetAttributesMasterForValues();
            if (result != null)
            {
                DDL.Items.Clear();
                DDL.DataValueField = "ParentAttribute_Id";
                DDL.DataTextField = "ParentAttributeName";
                DDL.DataSource = result;
            }
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("---ALL---", ""));


        }

        private void PopulateAttibuteMaster()
        {
            MDMSVC.DC_M_masterattribute _objSearch = new MDMSVC.DC_M_masterattribute();

            if (!String.IsNullOrWhiteSpace(txtAttributeNameSearch.Text))
                _objSearch.Name = txtAttributeNameSearch.Text.Trim();

            if (!String.IsNullOrWhiteSpace(txtOTATableCodeSearch.Text))
                _objSearch.OTA_CodeTableCode = txtOTATableCodeSearch.Text.Trim();

            if (!String.IsNullOrWhiteSpace(txtOTATableNameSearch.Text))
                _objSearch.OTA_CodeTableName = txtOTATableNameSearch.Text.Trim();

            if (!String.IsNullOrWhiteSpace(ddlParentSearch.SelectedValue))
                _objSearch.ParentAttribute_Id = Guid.Parse(ddlParentSearch.SelectedValue);

            if (ddlMasterForSearch.SelectedValue.Trim() != String.Empty)
                _objSearch.MasterFor = ddlMasterForSearch.SelectedItem.Text.Trim();

            _objSearch.IsActive = ddlStatus.SelectedValue;

            _objSearch.PageNo = intPageIndex;
            _objSearch.PageSize = intPageSize;

            var result = _objMst.GetMasterAttributes(_objSearch);
            grdMasterAttributeList.DataSource = result;
            grdMasterAttributeList.PageIndex = intPageIndex;
            grdMasterAttributeList.PageSize = intPageSize;
            if (result != null)
            {
                if (result.Count > 0)
                {
                    grdMasterAttributeList.VirtualItemCount = result[0].TotalRecords;
                }
            }
            grdMasterAttributeList.DataBind();
        }
        private void PopulateParentAttribute(DropDownList ddl, string MasterFor)
        {
            var result = _objMst.GetParentAttributes(MasterFor);
            if (result != null)
            {
                ddl.Items.Clear();
                ddl.DataValueField = "ParentAttribute_Id";
                ddl.DataTextField = "ParentAttributeName";
                ddl.DataSource = result;
            }
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---", ""));
        }

        //private void PopulateParentAttribute()
        //{
        //    var result = _objMst.GetParentAttributes();
        //    ddlParent.DataSource = result;
        //    if (result != null)
        //    {
        //        ddlParent.DataValueField = "ParentArrtribute_Id";
        //        ddlParent.DataTextField = "ParentAttributeName";
        //    }
        //    ddlParent.DataBind();
        //    ddlParent.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = Guid.Empty.ToString() });

        //    //using (Models.lookupAttributeDAL obj = new Models.lookupAttributeDAL())
        //    //{
        //    //    var data = obj.GetParentAttributes();

        //    //    ddlParent.DataSource = data;
        //    //    ddlParent.DataValueField = "ParentArrtribute_Id";
        //    //    ddlParent.DataTextField = "ParentAttributeName";
        //    //    ddlParent.DataBind();

        //    //    ddlParent.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = Guid.Empty.ToString() });
        //    //}
        //}

        //private void PopulateAttributeFor()
        //{
        //    var result = _objMst.GetAttributesForValues();

        //    ddlMasterFor.DataSource = result;
        //    if (result != null)
        //    {
        //        ddlMasterFor.DataValueField = "ParentAttributeName";
        //        ddlMasterFor.DataTextField = "ParentAttributeName";
        //    }
        //    ddlMasterFor.DataBind();

        //    ddlMasterFor.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = Guid.Empty.ToString() });

        //    //using (Models.lookupAttributeDAL obj = new Models.lookupAttributeDAL())
        //    //{
        //    //    var data = obj.GetAttributesForValues();

        //    //    ddlMasterFor.DataSource = data;
        //    //    ddlMasterFor.DataValueField = "ParentAttributeName";
        //    //    ddlMasterFor.DataTextField = "ParentAttributeName";
        //    //    ddlMasterFor.DataBind();

        //    //    ddlMasterFor.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = Guid.Empty.ToString() });
        //    //}
        //}

        protected void lnkAddNewAttribute_Click(object sender, EventArgs e)
        {
            //    //Models.lookupAttributes.masterattribute obj = new Models.lookupAttributes.masterattribute();

            //    MDMSVC.DC_M_masterattribute obj = new MDMSVC.DC_M_masterattribute();
            //    obj.Name = txtNewAttribute.Text;
            //    obj.IsActive = chkActiveMaster.Checked ? "Y" : "N";
            //    obj.MasterFor = ddlMasterFor.SelectedItem.Text;
            //    obj.OTA_CodeTableCode = txtOTATableCode.Text;
            //    obj.OTA_CodeTableName = txtOTATableName.Text;

            //    if (!(Guid.Parse(ddlParent.SelectedValue) == Guid.Empty))
            //    {
            //        obj.ParentAttribute_Id = Guid.Parse(ddlParent.SelectedValue);
            //    }

            //    if (lnkAddNewAttribute.CommandName == "Add")
            //    {
            //        obj.MasterAttribute_Id = Guid.NewGuid();
            //        obj.Action = "Save";
            //        var result = _objMst.SaveMasterAttribute(obj);
            //        if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
            //        {
            //            // resets the form values to blank so a new one can be added
            //            txtNewAttribute.Text = "";
            //            chkActiveMaster.Checked = true;
            //            ddlMasterFor.SelectedIndex = 0;
            //            txtOTATableCode.Text = "";
            //            txtOTATableName.Text = "";

            //            // rebinds the grid above within update panel
            //            PopulateAttibuteMaster();
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeDetail, result.StatusMessage, (BootstrapAlertType)result.StatusCode);

            //            //Response.Redirect(Request.Url.AbsoluteUri);
            //        }
            //        else
            //        {
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeDetail, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            //        }

            //        //using (Models.lookupAttributeDAL objIns = new Models.lookupAttributeDAL())
            //        //{
            //        //    if (objIns.SaveMasterAttribute(obj, Models.lookupAttributeDAL.operation.Save))
            //        //    {
            //        //        // resets the form values to blank so a new one can be added
            //        //        txtNewAttribute.Text = "";
            //        //        chkActiveMaster.Checked = true;
            //        //        ddlMasterFor.SelectedIndex = 0;
            //        //        txtOTATableCode.Text = "";
            //        //        txtOTATableName.Text = "";

            //        //        // rebinds the grid above within update panel
            //        //        PopulateAttibuteMaster();

            //        //        //Response.Redirect(Request.Url.AbsoluteUri);
            //        //    }
            //        //}
            //    }
            //    else if (lnkAddNewAttribute.CommandName == "Update")
            //    {
            //        obj.MasterAttribute_Id = Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString());
            //        obj.Action = "Update";
            //        var result = _objMst.SaveMasterAttribute(obj);
            //        if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
            //        {


            //            //    using (Models.lookupAttributeDAL objIns = new Models.lookupAttributeDAL())
            //            //{
            //            //    if (objIns.SaveMasterAttribute(obj, Models.lookupAttributeDAL.operation.Update))
            //            //    {

            //            // resets the form values to blank so a new one can be added
            //            txtNewAttribute.Text = "";
            //            chkActiveMaster.Checked = true;
            //            ddlMasterFor.SelectedIndex = 0;
            //            txtOTATableCode.Text = "";
            //            txtOTATableName.Text = "";
            //            lnkAddNewAttribute.Text = "Add New Attribute";
            //            lnkAddNewAttribute.CommandName = "Add";
            //            // rebinds the grid above within update panel
            //            PopulateAttibuteMaster();
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeDetail, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            //            // Response.Redirect(Request.Url.AbsoluteUri);
            //        }
            //        else
            //        {
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeDetail, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            //        }
            //        //}
            //    }

        }

        //private void ShowHideAliasGrid(bool Show)
        //{
        //    System.Web.UI.HtmlControls.HtmlGenericControl divAttrVal = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAttributedetail.FindControl("divAttrVal");
        //    if (Show)
        //    {
        //        divAttrVal.Visible = true;
        //    }
        //    else
        //    {
        //        divAttrVal.Visible = false;
        //    }

        //}

        protected void grdMasterAttributeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdMasterAttributeList.Rows)
            {
                if (row.RowIndex == grdMasterAttributeList.SelectedIndex)
                {
                    row.BackColor = System.Drawing.Color.DarkTurquoise;
                }
                else
                {
                    row.BackColor = System.Drawing.Color.Transparent;
                }
            }

            BindAttributeValues(Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString()));
        }

        void BindAttributeValues(Guid MasterAttribute_Id)
        {
            _MasterAttribute_Id = MasterAttribute_Id;
            var result = _objMst.GetAttributeDetails(MasterAttribute_Id.ToString());
            List<MDMSVC.DC_M_masterattribute> ma = new List<MDMSVC.DC_M_masterattribute>();
            ma.Add(result);
            frmAttributedetail.ChangeMode(FormViewMode.Edit);
            frmAttributedetail.DataSource = ma;
            frmAttributedetail.DataBind();

            TextBox txtNewAttribute = (TextBox)frmAttributedetail.FindControl("txtNewAttribute");
            TextBox txtOTATableCode = (TextBox)frmAttributedetail.FindControl("txtOTATableCode");
            TextBox txtOTATableName = (TextBox)frmAttributedetail.FindControl("txtOTATableName");
            DropDownList ddlMasterFor = (DropDownList)frmAttributedetail.FindControl("ddlMasterFor");
            DropDownList ddlParent = (DropDownList)frmAttributedetail.FindControl("ddlParent");
            CheckBox chkActiveMaster = (CheckBox)frmAttributedetail.FindControl("chkActiveMaster");
            LinkButton lnkAddNewAttribute = (LinkButton)frmAttributedetail.FindControl("lnkAddNewAttribute");
            FillMasterFor(ddlMasterFor);
            //  ShowHideAliasGrid(true);
            GridView grdAttributeValues = (GridView)frmAttributedetail.FindControl("grdAttributeValues");

            DropDownList ddlParentAttrValue = (DropDownList)frmAttributedetail.FindControl("ddlParentAttrValue");


            var resultAttributes = _objMst.GetAttributeValues(MasterAttribute_Id.ToString(), Convert.ToString(intPageSizeAttributeValue), Convert.ToString(intPageNoAttributeValue));
            grdAttributeValues.DataSource = resultAttributes;
            grdAttributeValues.PageIndex = intPageNoAttributeValue;
            grdAttributeValues.PageSize = intPageSizeAttributeValue;
            if (resultAttributes != null)
            {
                if (resultAttributes.Count > 0)
                {
                    grdAttributeValues.VirtualItemCount = resultAttributes[0].TotalCount;
                }
            }

            grdAttributeValues.DataBind();
            if (!string.IsNullOrWhiteSpace(result.MasterFor))
            {
                if (ddlMasterFor.Items.FindByText(System.Web.HttpUtility.HtmlDecode(result.MasterFor)) != null)
                {
                    ddlMasterFor.ClearSelection();
                    ddlMasterFor.SelectedValue = ddlMasterFor.Items.FindByText(System.Web.HttpUtility.HtmlDecode(result.MasterFor)).Value;
                }
            }
            else
            {
                ddlMasterFor.ClearSelection();
                ddlMasterFor.SelectedIndex = 0;
            }

            ddlParent.Items.Clear();
            ddlParent.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = "0" });

            ddlParentAttrValue.Items.Clear();
            ddlParentAttrValue.Items.Insert(0, new ListItem { Selected = true, Text = "-Select-", Value = "0" });

            if (ddlMasterFor.SelectedIndex != 0)
            {
                var MasterAttrValues = _objMst.GetParentAttributes(ddlMasterFor.SelectedItem.Text);
                MasterAttrValues = (from x in MasterAttrValues where x.ParentAttributeName != result.Name select x).ToList();

                if (MasterAttrValues != null)
                {
                    ddlParent.DataSource = MasterAttrValues;
                    ddlParent.DataValueField = "ParentAttribute_Id";
                    ddlParent.DataTextField = "ParentAttributeName";
                    ddlParent.DataBind();
                }

                if (result.ParentAttribute_Id != null)
                {
                    if (ddlParent.Items.FindByValue(result.ParentAttribute_Id.ToString()) != null)
                    {
                        ddlParent.SelectedValue = result.ParentAttribute_Id.ToString();
                    }
                }

                if (ddlParent.SelectedIndex != 0)
                {
                    var ParentAttrValues = _objMst.GetAttributeValues(Convert.ToString(ddlParent.SelectedValue), "0", "0");
                    if (ParentAttrValues != null)
                    {
                        ddlParentAttrValue.DataSource = ParentAttrValues;
                        ddlParentAttrValue.DataValueField = "MasterAttributeValue_Id";
                        ddlParentAttrValue.DataTextField = "AttributeValue";
                        ddlParentAttrValue.DataBind();
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(result.ParentAttribute_Id.ToString()))
            {
                if (ddlParent.Items.FindByValue(result.ParentAttribute_Id.ToString()) != null)
                {
                    ddlParent.ClearSelection();
                    ddlParent.SelectedValue = result.ParentAttribute_Id.ToString();
                }
            }
            else
            {
                ddlParent.ClearSelection();
                ddlParent.SelectedIndex = 0;
            }

            txtNewAttribute.Text = result.Name;
            txtOTATableCode.Text = result.OTA_CodeTableCode;
            txtOTATableName.Text = result.OTA_CodeTableName;
            chkActiveMaster.Checked = result.IsActive == "Y" ? true : false;

        }

        protected void btnCreateAlias_Click(object sender, EventArgs e)
        {
            //    MDMSVC.DC_M_masterattributevalue obj = new MDMSVC.DC_M_masterattributevalue();



            //    //Models.lookupAttributes.masterattributevalue obj = new Models.lookupAttributes.masterattributevalue();

            //    obj.MasterAttribute_Id = Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString());
            //    obj.AttributeValue = txtValueName.Text;
            //    obj.OTA_CodeTableValue = txtOTATableValue.Text;
            //    obj.IsActive = chkActiveValue.Checked ? "Y" : "N";

            //    if (ddlParentAttrValue.SelectedIndex != 0)
            //    {
            //        obj.ParentAttributeValue_Id = Guid.Parse(ddlParentAttrValue.SelectedValue);
            //    }


            //    if (btnCreateAlias.CommandName == "Add")
            //    {
            //        obj.MasterAttributeValue_Id = Guid.NewGuid();
            //        obj.Action = "Save";
            //        var _msg = _objMst.SaveAttributeValue(obj);
            //        if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
            //        {

            //            //using (Models.lookupAttributeDAL objIns = new Models.lookupAttributeDAL())
            //            //{
            //            //    if (objIns.SaveAttributeValue(obj, Models.lookupAttributeDAL.operation.Save))
            //            //    {
            //            //reset values for new addition
            //            txtValueName.Text = "";
            //            txtOTATableValue.Text = "";
            //            chkActiveValue.Checked = true;
            //            ddlParentAttrValue.SelectedIndex = 0;

            //            // update the grid within the update panel
            //            BindAttributeValues(Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString()));

            //            // removed to use update panel for better user experience
            //            //Response.Redirect(Request.Url.AbsoluteUri);
            //            //}
            //            // hdnFlagCity.Value = "true";
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeValue, _msg.StatusMessage, BootstrapAlertType.Success);
            //        }
            //        else
            //        {
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeValue, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            //        }
            //    }
            //    else if (btnCreateAlias.CommandName == "Update")
            //    {
            //        obj.MasterAttributeValue_Id = Guid.Parse(grdAttributeValues.SelectedDataKey.Value.ToString());
            //        obj.Action = "Save";
            //        var _msg = _objMst.SaveAttributeValue(obj);
            //        if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
            //        {
            //            //    using (Models.lookupAttributeDAL objIns = new Models.lookupAttributeDAL())
            //            //{
            //            //    if (objIns.SaveAttributeValue(obj, Models.lookupAttributeDAL.operation.Update))
            //            //    {
            //            //reset values for new addition
            //            txtValueName.Text = "";
            //            txtOTATableValue.Text = "";
            //            chkActiveValue.Checked = true;
            //            btnCreateAlias.CommandName = "Add";
            //            btnCreateAlias.Text = "Add New Value";
            //            ddlParentAttrValue.SelectedIndex = 0;

            //            // update the grid within the update panel
            //            BindAttributeValues(Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString()));


            //            // removed to use update panel for better user experience
            //            // Response.Redirect(Request.Url.AbsoluteUri);
            //            //}
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeValue, _msg.StatusMessage, BootstrapAlertType.Success);
            //        }
            //        else
            //        {
            //            BootstrapAlert.BootstrapAlertMessage(msgAlertAttributeValue, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
            //        }
            //    }

        }

        protected void grdMasterAttributeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                frmAttributedetail.ChangeMode(FormViewMode.Edit);
                //   frmAttributedetail.DataSource
                TextBox txtNewAttribute = (TextBox)frmAttributedetail.FindControl("txtNewAttribute");
                TextBox txtOTATableCode = (TextBox)frmAttributedetail.FindControl("txtOTATableCode");
                TextBox txtOTATableName = (TextBox)frmAttributedetail.FindControl("txtOTATableName");
                DropDownList ddlMasterFor = (DropDownList)frmAttributedetail.FindControl("ddlMasterFor");
                DropDownList ddlParent = (DropDownList)frmAttributedetail.FindControl("ddlParent");
                CheckBox chkActiveMaster = (CheckBox)frmAttributedetail.FindControl("chkActiveMaster");
                LinkButton lnkAddNewAttribute = (LinkButton)frmAttributedetail.FindControl("lnkAddNewAttribute");

                int index = row.RowIndex;

                txtNewAttribute.Text = System.Web.HttpUtility.HtmlDecode(grdMasterAttributeList.Rows[index].Cells[0].Text);

                bool _isEnable = (txtNewAttribute.Text.Trim().ToLower() == "systemconfig" ? true : false);
                if (_isEnable)
                {
                    txtNewAttribute.Enabled = _isEnable;
                    ddlMasterFor.Enabled = _isEnable;
                    txtOTATableCode.Enabled = _isEnable;
                    txtOTATableName.Enabled = _isEnable;
                    ddlParent.Enabled = _isEnable;
                    lnkAddNewAttribute.Enabled = _isEnable;
                }

            }
            if (e.CommandName.ToString() == "Mapping")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                var attributeid = e.CommandArgument.ToString();
                var systemattributename = grdMasterAttributeList.Rows[index].Cells[0].Text;
                sysAttrName.InnerText = systemattributename;
                addupdatemsg.Style.Add("display", "none");
                btnSave.Text = "Add";
                txtSupplierAttributeName.Text = "";
                ddlSuppliers.Enabled = true;
                ddlSuppliers.SelectedIndex = 0;
                ddlMappingStatus.SelectedIndex = ddlMappingStatus.Items.IndexOf(ddlMappingStatus.Items.FindByText("MAPPED"));
                hiddenfield.Value = attributeid;
                MDMSVC.DC_MasterAttributeMapping_RQ RQ = new MDMSVC.DC_MasterAttributeMapping_RQ();
                grdMappingAttrVal.DataSource = null;
                grdMappingAttrVal.DataBind();
                if (attributeid != null)
                    fillSearchGrid(0);
                else
                {
                    grdSearchResults.DataSource = null;
                    grdSearchResults.DataBind();
                }

            }
        }
        protected void grdAttributeValues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                GridView grdAttributeValues = (GridView)frmAttributedetail.FindControl("grdAttributeValues");
                DropDownList ddlParentAttrValue = (DropDownList)frmAttributedetail.FindControl("ddlParentAttrValue");
                TextBox txtValueName = (TextBox)frmAttributedetail.FindControl("txtValueName");
                TextBox txtOTATableValue = (TextBox)frmAttributedetail.FindControl("txtOTATableValue");
                CheckBox chkActiveValue = (CheckBox)frmAttributedetail.FindControl("chkActiveValue");

                txtValueName.Text = System.Web.HttpUtility.HtmlDecode(grdAttributeValues.Rows[index].Cells[0].Text);

                if (ddlParentAttrValue.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdAttributeValues.Rows[index].Cells[1].Text)) != null)
                {
                    ddlParentAttrValue.SelectedValue = ddlParentAttrValue.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdAttributeValues.Rows[index].Cells[1].Text)).Value;
                }
                else
                {
                    ddlParentAttrValue.ClearSelection();
                    ddlParentAttrValue.SelectedIndex = 0;
                }
                txtOTATableValue.Text = System.Web.HttpUtility.HtmlDecode(grdAttributeValues.Rows[index].Cells[2].Text);
                chkActiveValue.Checked = grdAttributeValues.Rows[index].Cells[3].Text == "Y" ? true : false;


            }


        }

        protected void lnkButtonResetForm_Click(object sender, EventArgs e)
        {

        }

        protected void grdAttributeValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView grdAttributeValues = (GridView)frmAttributedetail.FindControl("grdAttributeValues");
            _MasterAttributeValue_Id = Guid.Parse(grdAttributeValues.SelectedDataKey.Value.ToString());
            LinkButton btnCreateAlias = (LinkButton)frmAttributedetail.FindControl("btnCreateAlias");
            btnCreateAlias.CommandName = "UpdateAttributeValue";
            btnCreateAlias.Text = "Update Attribute";
            foreach (GridViewRow row in grdAttributeValues.Rows)
            {
                if (row.RowIndex == grdAttributeValues.SelectedIndex)
                {
                    row.BackColor = System.Drawing.Color.DarkTurquoise;
                }
                else
                {
                    row.BackColor = System.Drawing.Color.Transparent;
                }
            }

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            intPageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            PopulateAttibuteMaster();
        }

        protected void grdMasterAttributeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = Convert.ToInt32(e.NewPageIndex);
            intPageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            PopulateAttibuteMaster();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateAttibuteMaster();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtAttributeNameSearch.Text = string.Empty;
            txtOTATableCodeSearch.Text = string.Empty;
            txtOTATableNameSearch.Text = string.Empty;
            ddlMasterForSearch.SelectedIndex = 0;
            ddlParentSearch.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            grdMasterAttributeList.DataSource = null;
            grdMasterAttributeList.DataBind();
        }
        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmAttributedetail.ChangeMode(FormViewMode.Insert);
            frmAttributedetail.DataBind();

            DropDownList ddlMasterFor = (DropDownList)frmAttributedetail.FindControl("ddlMasterFor");

            FillMasterFor(ddlMasterFor);


        }

        protected void ddlMasterForSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateParentAttribute(ddlParentSearch, ddlMasterForSearch.SelectedItem.Text);
        }

        protected void ddlMasterFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlParent = (DropDownList)frmAttributedetail.FindControl("ddlParent");
            DropDownList ddlMasterFor = (DropDownList)frmAttributedetail.FindControl("ddlMasterFor");
            PopulateParentAttribute(ddlParent, ddlMasterFor.SelectedItem.Text);

        }

        protected void frmAttributedetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            #region Get Controls
            //Getting all controls here
            TextBox txtNewAttribute = (TextBox)frmAttributedetail.FindControl("txtNewAttribute");
            TextBox txtOTATableCode = (TextBox)frmAttributedetail.FindControl("txtOTATableCode");
            TextBox txtOTATableName = (TextBox)frmAttributedetail.FindControl("txtOTATableName");
            DropDownList ddlMasterFor = (DropDownList)frmAttributedetail.FindControl("ddlMasterFor");
            DropDownList ddlParent = (DropDownList)frmAttributedetail.FindControl("ddlParent");
            CheckBox chkActiveMaster = (CheckBox)frmAttributedetail.FindControl("chkActiveMaster");
            System.Web.UI.HtmlControls.HtmlGenericControl msgAlertAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAttributedetail.FindControl("msgAlertAttributeValue");
            LinkButton btnCreateAlias = (LinkButton)frmAttributedetail.FindControl("btnCreateAlias");
            #endregion




            if (e.CommandName == "Add")
            {
                MDMSVC.DC_M_masterattribute obj = new MDMSVC.DC_M_masterattribute();


                obj.Name = txtNewAttribute.Text;
                obj.IsActive = chkActiveMaster.Checked ? "Y" : "N";
                obj.MasterFor = ddlMasterFor.SelectedItem.Text;
                obj.OTA_CodeTableCode = txtOTATableCode.Text;
                obj.OTA_CodeTableName = txtOTATableName.Text;

                if (!String.IsNullOrWhiteSpace(ddlParent.SelectedValue))
                {
                    obj.ParentAttribute_Id = Guid.Parse(ddlParent.SelectedValue);
                }
                obj.MasterAttribute_Id = Guid.NewGuid();
                obj.Action = "Save";
                var result = _objMst.SaveMasterAttribute(obj);
                if (result.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    // resets the form values to blank so a new one can be added
                    txtNewAttribute.Text = "";
                    chkActiveMaster.Checked = true;
                    ddlMasterFor.SelectedIndex = 0;
                    txtOTATableCode.Text = "";
                    txtOTATableName.Text = "";

                    // rebinds the grid above within update panel
                    PopulateAttibuteMaster();
                    hdnFlag.Value = "add";
                    BootstrapAlert.BootstrapAlertMessage(msgAlertStatus, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                }
            }
            else if (e.CommandName == "Edit")
            {
                Guid gMasterAttributeId = Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString());

                MDMSVC.DC_M_masterattribute _objEdit = new MDMSVC.DC_M_masterattribute();
                _objEdit.MasterAttribute_Id = gMasterAttributeId;
                _objEdit.Name = txtNewAttribute.Text;
                _objEdit.MasterFor = ddlMasterFor.SelectedItem.Text;
                if (ddlParent.SelectedIndex != 0)
                {
                    _objEdit.ParentAttribute_Id = Guid.Parse(ddlParent.SelectedValue);
                }
                _objEdit.OTA_CodeTableCode = txtOTATableCode.Text;
                _objEdit.OTA_CodeTableName = txtOTATableName.Text;
                _objEdit.Action = "Update";
                _objEdit.IsActive = chkActiveMaster.Checked ? "Y" : "N";
                var _msg = _objMst.SaveMasterAttribute(_objEdit);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    txtNewAttribute.Text = "";
                    txtOTATableName.Text = "";
                    chkActiveMaster.Checked = true;
                    txtOTATableCode.Text = "";
                    ddlMasterFor.SelectedIndex = 0;
                    ddlParent.SelectedIndex = 0;
                    PopulateAttibuteMaster();
                    hdnFlag.Value = "edit";
                    BootstrapAlert.BootstrapAlertMessage(msgAlertStatus, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
            else if (e.CommandName == "AddAttributeValue")
            {
                Guid gMasterAttributeId = Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString());

                MDMSVC.DC_M_masterattributevalue objAdd = new MDMSVC.DC_M_masterattributevalue();
                objAdd.MasterAttributeValue_Id = Guid.NewGuid();
                objAdd.Action = "Save";
                CheckBox chkActiveValue = (CheckBox)frmAttributedetail.FindControl("chkActiveValue");
                DropDownList ddlParentAttrValue = (DropDownList)frmAttributedetail.FindControl("ddlParentAttrValue");
                TextBox txtValueName = (TextBox)frmAttributedetail.FindControl("txtValueName");
                TextBox txtOTATableValue = (TextBox)frmAttributedetail.FindControl("txtOTATableValue");

                objAdd.AttributeValue = txtValueName.Text;
                objAdd.OTA_CodeTableValue = txtOTATableValue.Text;
                objAdd.IsActive = chkActiveValue.Checked ? "Y" : "N";
                objAdd.MasterAttribute_Id = gMasterAttributeId;

                if (ddlParentAttrValue.SelectedIndex != 0)
                {
                    objAdd.ParentAttributeValue_Id = Guid.Parse(ddlParentAttrValue.SelectedValue);
                }

                var _msg = _objMst.SaveAttributeValue(objAdd);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    // update the grid within the update panel
                    BindAttributeValues(gMasterAttributeId);

                    BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
            else if (e.CommandName == "UpdateAttributeValue")
            {
                Guid gMasterAttributeId = Guid.Parse(grdMasterAttributeList.SelectedDataKey.Value.ToString());

                MDMSVC.DC_M_masterattributevalue objEdit = new MDMSVC.DC_M_masterattributevalue();

                objEdit.MasterAttributeValue_Id = _MasterAttributeValue_Id;
                CheckBox chkActiveValue = (CheckBox)frmAttributedetail.FindControl("chkActiveValue");
                DropDownList ddlParentAttrValue = (DropDownList)frmAttributedetail.FindControl("ddlParentAttrValue");
                TextBox txtValueName = (TextBox)frmAttributedetail.FindControl("txtValueName");
                TextBox txtOTATableValue = (TextBox)frmAttributedetail.FindControl("txtOTATableValue");

                objEdit.AttributeValue = txtValueName.Text;
                objEdit.OTA_CodeTableValue = txtOTATableValue.Text;
                objEdit.IsActive = chkActiveValue.Checked ? "Y" : "N";
                objEdit.MasterAttribute_Id = gMasterAttributeId;
                objEdit.Action = "Update";
                if (ddlParentAttrValue.SelectedIndex != 0)
                {
                    objEdit.ParentAttributeValue_Id = Guid.Parse(ddlParentAttrValue.SelectedValue);
                }

                var _msg = _objMst.SaveAttributeValue(objEdit);
                if (_msg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    btnCreateAlias.CommandName = "AddAttributeValue";
                    btnCreateAlias.Text = "Add New Value";
                    ddlParentAttrValue.SelectedIndex = 0;

                    BindAttributeValues(gMasterAttributeId);

                    BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }


        protected void grdAttributeValues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageNoAttributeValue = Convert.ToInt32(e.NewPageIndex);
            PopulateAttibuteValue();

        }

        private void PopulateAttibuteValue()
        {
            GridView grdAttributeValues = (GridView)frmAttributedetail.FindControl("grdAttributeValues");
            var resultAttributes = _objMst.GetAttributeValues(_MasterAttribute_Id.ToString(), Convert.ToString(intPageSizeAttributeValue), Convert.ToString(intPageNoAttributeValue));
            grdAttributeValues.DataSource = resultAttributes;
            grdAttributeValues.PageIndex = intPageNoAttributeValue;
            grdAttributeValues.PageSize = intPageSizeAttributeValue;
            if (resultAttributes != null)
            {
                if (resultAttributes.Count > 0)
                {
                    grdAttributeValues.VirtualItemCount = resultAttributes[0].TotalCount;
                }
            }
            grdAttributeValues.DataBind();

        }
        public void fillSearchGrid(int pageindex)
        {
            if (!string.IsNullOrWhiteSpace(hiddenfield.Value))
            {
                Guid attributeid = Guid.Parse(hiddenfield.Value);
                MDMSVC.DC_MasterAttributeMapping_RQ RQ = new MDMSVC.DC_MasterAttributeMapping_RQ();
                if (ddlmresultsupplierlist.SelectedIndex != 0)
                {
                    RQ.Supplier_Id = Guid.Parse(ddlmresultsupplierlist.SelectedValue);
                }
                RQ.MasterAttributeType_Id = attributeid;
                RQ.PageSize = int.Parse(ddlpagesize.SelectedItem.Text);
                RQ.PageNo = pageindex;
                var searchResult = MapSvc.Mapping_Attribute_Search(RQ);
                if (searchResult != null && searchResult.Count > 0)
                {
                    lblTotalCount.Text = searchResult[0].TotalRecords.ToString();
                    grdSearchResults.DataSource = searchResult;
                    grdSearchResults.VirtualItemCount = searchResult[0].TotalRecords;
                    grdSearchResults.PageSize = RQ.PageSize;
                    grdSearchResults.PageIndex = RQ.PageNo;
                    grdSearchResults.DataBind();
                }
                else
                {
                    grdSearchResults.DataSource = null;
                    grdSearchResults.DataBind();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            addupdatemsg.Style.Add("display", "none");
            msgdelundel.Style.Add("display", "none");
            msgupdateall.Style.Add("display", "none");
            var id = hiddenfield.Value;
            var a = ddlSuppliers.SelectedItem.Value;
            var san = txtSupplierAttributeName.Text;

            if (btnSave.Text == "Update")
            {
                if (a == "0" || san == "")
                {
                    BootstrapAlert.BootstrapAlertMessage(addupdatemsg, "Please select both  Supplier Name and Supplier Attribute Type..!! ", BootstrapAlertType.Warning);
                }
                else
                {
                    Guid MasterAttributeMappingId = Guid.Parse(hdn_MasterAttributeMapping_Id.Value);

                    MDMSVC.DC_MasterAttributeMapping newObj1 = new MDMSVC.DC_MasterAttributeMapping
                    {
                        MasterAttributeMapping_Id = MasterAttributeMappingId,
                        SupplierMasterAttribute = txtSupplierAttributeName.Text,
                        Supplier_Id = Guid.Parse(ddlSuppliers.SelectedValue),
                        SystemMasterAttribute_Id = Guid.Parse(id),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        Create_Date = DateTime.Now,
                        Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true,
                        Status = ddlMappingStatus.SelectedItem.Text
                    };
                    var result1 = MapSvc.Mapping_Attribute_Update(newObj1);
                    BootstrapAlert.BootstrapAlertMessage(addupdatemsg, result1.StatusMessage, (BootstrapAlertType)(result1.StatusCode));
                    fillSearchGrid(0);
                }
            }
            else
            {
                if (a == "0" || san == "")
                {
                    BootstrapAlert.BootstrapAlertMessage(addupdatemsg, "Please select both  Supplier Name and Supplier Attribute Type..!! ", BootstrapAlertType.Warning);
                }
                else
                {
                    MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                    {
                        MasterAttributeMapping_Id = Guid.NewGuid(),
                        SupplierMasterAttribute = txtSupplierAttributeName.Text,
                        Supplier_Id = Guid.Parse(ddlSuppliers.SelectedValue),
                        SystemMasterAttribute_Id = Guid.Parse(id),
                        Create_Date = DateTime.Now,
                        Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true,
                        Status = "MAPPED"
                    };
                    var result = MapSvc.Mapping_Attribute_Add(newObj);
                    BootstrapAlert.BootstrapAlertMessage(addupdatemsg, result.Message.StatusMessage, (BootstrapAlertType)(result.Message.StatusCode));
                    fillSearchGrid(0);
                    if (result.AttributeMapping_Id != null)
                    {
                        hdn_MasterAttributeMapping_Id.Value = result.AttributeMapping_Id.ToString();
                        GridViewRow loopRow;
                        for (int i = 0; i < grdSearchResults.Rows.Count; i++)
                        {
                            loopRow = grdSearchResults.Rows[i];
                            if (((LinkButton)loopRow.FindControl("btnEdit")).CommandArgument.ToString().ToLower() == result.AttributeMapping_Id.ToString().ToLower())
                            {
                                loopRow.BackColor = System.Drawing.Color.DarkTurquoise;
                            }
                            else
                            {
                                loopRow.BackColor = System.Drawing.Color.Transparent;
                            }
                        }
                        fillsupplierAttrvalues(0);
                        btnSave.Text = "Update";
                    }
                }
            }

        }

        private void UpdateAllAttrValues()
        {
            msgdelundel.Style.Add("display", "none");
            msgupdateall.Style.Add("display", "none");
            var PARAM = new List<MDMSVC.DC_MasterAttributeValueMapping>();
            Guid MasterAttributeMappingId = Guid.Parse(hdn_MasterAttributeMapping_Id.Value);
            GridViewRow row;
            for (int i = 0; i < grdMappingAttrVal.Rows.Count; i++)
            {
                row = grdMappingAttrVal.Rows[i];
                LinkButton btnSelect = (LinkButton)row.FindControl("btnSelect");
                Guid myRow_Id = Guid.Parse(btnSelect.CommandArgument.ToString());
                CheckBox chkAttrValIsActive = (CheckBox)row.FindControl("chkAttrValIsActive");
                TextBox txtSupplierAttributeValue = (TextBox)row.FindControl("txtSupplierAttributeValue");
                Label SystemMasterAttributeValueId = (Label)row.FindControl("lblSystemMasterAttributeValueId");

                PARAM.Add(new MDMSVC.DC_MasterAttributeValueMapping
                {
                    MasterAttributeMapping_Id = MasterAttributeMappingId,
                    MasterAttributeValueMapping_Id = myRow_Id,
                    IsActive = chkAttrValIsActive.Checked,
                    SupplierMasterAttributeValue = txtSupplierAttributeValue.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    SystemMasterAttributeValue_Id = Guid.Parse(SystemMasterAttributeValueId.Text)
                });

            }

            var result = MapSvc.Mapping_AttributeValue_Update(PARAM);
            BootstrapAlert.BootstrapAlertMessage(msgupdateall, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));
        }

        private void BindSuppliers(DropDownList ddl)
        {
            Controller.MasterDataSVCs mdObj = new Controller.MasterDataSVCs();
            MDMSVC.DC_Supplier_Search_RQ RQParam = new MDMSVC.DC_Supplier_Search_RQ();
            RQParam.StatusCode = "ACTIVE";
            RQParam.PageNo = 0;
            RQParam.PageSize = int.MaxValue;
            var result = mdObj.GetSupplier(RQParam);
            ddl.DataSource = result;
            ddl.DataValueField = "Supplier_Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.RemoveAt(0);
            ddl.Items.Insert(0, new ListItem("--Select --", "0"));
            result = null;
            mdObj = null;
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

        protected void grdSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillSearchGrid(e.NewPageIndex);
        }

        protected void grdSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToString() == "Editing")
            {
                Guid masterattributemappingid = Guid.Parse(e.CommandArgument.ToString());
                addupdatemsg.Style.Add("display", "none");
                ddlSuppliers.Enabled = false;
                btnSave.Text = "Update";
                hdn_MasterAttributeMapping_Id.Value = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                GridViewRow loopRow;
                for (int i = 0; i < grdSearchResults.Rows.Count; i++)
                {
                    loopRow = grdSearchResults.Rows[i];
                    if (((LinkButton)loopRow.FindControl("btnEdit")).CommandArgument.ToString().ToLower() == e.CommandArgument.ToString().ToLower())
                    {
                        loopRow.BackColor = System.Drawing.Color.DarkTurquoise;
                    }
                    else
                    {
                        loopRow.BackColor = System.Drawing.Color.Transparent;
                    }
                }

                fillSystemAttriDropdown();
                //update supplier attributes mapping
                ddlSuppliers.SelectedIndex = ddlSuppliers.Items.IndexOf(ddlSuppliers.Items.FindByText(grdSearchResults.Rows[index].Cells[0].Text));
                txtSupplierAttributeName.Text = grdSearchResults.Rows[index].Cells[1].Text;
                ddlMappingStatus.SelectedIndex = ddlMappingStatus.Items.IndexOf(ddlMappingStatus.Items.FindByText(grdSearchResults.Rows[index].Cells[2].Text));
                //fill supplier attribute value mapping
                fillsupplierAttrvalues(0);

            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                Guid masterattributemappingid = Guid.Parse(e.CommandArgument.ToString());
                addupdatemsg.Style.Add("display", "none");
                MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                {
                    MasterAttributeMapping_Id = masterattributemappingid,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = false,
                };

                var result = MapSvc.Mapping_Attribute_Update(newObj);
                BootstrapAlert.BootstrapAlertMessage(msgdelundel, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));

                fillSearchGrid(grdSearchResults.PageIndex);

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                Guid masterattributemappingid = Guid.Parse(e.CommandArgument.ToString());
                addupdatemsg.Style.Add("display", "none");
                MDMSVC.DC_MasterAttributeMapping newObj = new MDMSVC.DC_MasterAttributeMapping
                {
                    MasterAttributeMapping_Id = masterattributemappingid,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                };

                var result = MapSvc.Mapping_Attribute_Update(newObj);
                BootstrapAlert.BootstrapAlertMessage(msgdelundel, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));

                fillSearchGrid(grdSearchResults.PageIndex);

            }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hdn_MasterAttributeMapping_Id.Value = string.Empty;
            ddlsystemAttrVal.Items.Clear();
            lblTotalCountMappAttrVal.Text = "";
            addupdatemsg.Style.Add("display", "none");
            msgdelundel.Style.Add("display", "none");
            msgupdateall.Style.Add("display", "none");
            ddlSuppliers.Enabled = true;
            ddlSuppliers.SelectedIndex = 0;
            ddlStatus.SelectedIndex = ddlMappingStatus.Items.IndexOf(ddlMappingStatus.Items.FindByText("MAPPED"));
            txtSupplierAttributeName.Text = string.Empty;
            btnSave.Text = "Add";
            grdMappingAttrVal.DataSource = null;
            grdMappingAttrVal.DataBind();
            GridViewRow loopRow;
            for (int i = 0; i < grdSearchResults.Rows.Count; i++)
            {
                loopRow = grdSearchResults.Rows[i];
                loopRow.BackColor = System.Drawing.Color.Transparent;
            }
        }

        protected void grdMappingAttrVal_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToString() == "EditVal")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                CheckBox chkAttrValIsActive = (CheckBox)row.FindControl("chkAttrValIsActive");
                TextBox txtSupplierAttributeValue = (TextBox)row.FindControl("txtSupplierAttributeValue");
                Label SystemMasterAttributeValueId = (Label)row.FindControl("lblSystemMasterAttributeValueId");
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                addupdatemsg.Style.Add("display", "none");
                msgdelundel.Style.Add("display", "none");
                msgupdateall.Style.Add("display", "none");
                Guid MasterAttributeMappingId = Guid.Parse(hdn_MasterAttributeMapping_Id.Value);
                Guid MasterAttributeValueMappingId = Guid.Parse(e.CommandArgument.ToString());
                var isssActive = chkAttrValIsActive.Checked;
                MDMSVC.DC_MasterAttributeValueMapping newObj = new MDMSVC.DC_MasterAttributeValueMapping
                {
                    MasterAttributeMapping_Id = MasterAttributeMappingId,
                    MasterAttributeValueMapping_Id = MasterAttributeValueMappingId,
                    IsActive = chkAttrValIsActive.Checked,
                    SupplierMasterAttributeValue = txtSupplierAttributeValue.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    SystemMasterAttributeValue_Id = Guid.Parse(SystemMasterAttributeValueId.Text)
                };

                var RQ = new List<MDMSVC.DC_MasterAttributeValueMapping>();
                RQ.Add(newObj);

                var result = MapSvc.Mapping_AttributeValue_Update(RQ);
                BootstrapAlert.BootstrapAlertMessage(msgupdateall, result.StatusMessage, (BootstrapAlertType)(result.StatusCode));
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSearchGrid(0);
        }

        protected void ddlmresultsupplierlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSearchGrid(0);
        }

        protected void grdMappingAttrVal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateAllAttrValues();
            fillsupplierAttrvalues(e.NewPageIndex);
        }

        protected void fillsupplierAttrvalues(int pageno)
        {

            if (!string.IsNullOrWhiteSpace(hdn_MasterAttributeMapping_Id.Value))
            {
                Guid masterattributemappingid = Guid.Parse(hdn_MasterAttributeMapping_Id.Value);
                MDMSVC.DC_MasterAttributeValueMapping_RQ RQ = new MDMSVC.DC_MasterAttributeValueMapping_RQ();

                if (ddlsystemAttrVal.SelectedIndex != 0)
                {
                    RQ.SystemMasterAttributeValue_Id = Guid.Parse(ddlsystemAttrVal.SelectedItem.Value);
                }

                RQ.MasterAttributeMapping_Id = masterattributemappingid;
                RQ.PageSize = int.Parse(ddlpagesizeforAttrVal.SelectedItem.Text);
                RQ.PageNo = pageno;
                var searchResult = MapSvc.Mapping_AttributeValue_Get(RQ);
                if (searchResult != null && searchResult.Count > 0)
                {
                    lblTotalCountMappAttrVal.Text = searchResult[0].TotalRecords.ToString();
                    grdMappingAttrVal.DataSource = searchResult;
                    grdMappingAttrVal.VirtualItemCount = searchResult[0].TotalRecords;
                    grdMappingAttrVal.PageSize = RQ.PageSize;
                    grdMappingAttrVal.PageIndex = RQ.PageNo;
                    grdMappingAttrVal.DataBind();
                }
            }
            else
            {
                grdMappingAttrVal.DataSource = null;
                grdMappingAttrVal.DataBind();
            }
        }

        protected void ddlpagesizeforAttrVal_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillsupplierAttrvalues(0);
        }

        protected void fillSystemAttriDropdown()
        {
            ddlsystemAttrVal.Items.Clear();
            if (!string.IsNullOrWhiteSpace(hdn_MasterAttributeMapping_Id.Value ))
            {
                Guid masterattributemappingid = Guid.Parse(hdn_MasterAttributeMapping_Id.Value);
                MDMSVC.DC_MasterAttributeValueMapping_RQ RQ = new MDMSVC.DC_MasterAttributeValueMapping_RQ();
                RQ.MasterAttributeMapping_Id = masterattributemappingid;
                RQ.PageSize = int.MaxValue;
                RQ.PageNo = 0;
                var searchResult = MapSvc.Mapping_AttributeValue_Get(RQ);
                ddlsystemAttrVal.DataSource = searchResult;
                ddlsystemAttrVal.DataTextField = "SystemMasterAttributeValue";
                ddlsystemAttrVal.DataValueField = "SystemMasterAttributeValue_Id";
                ddlsystemAttrVal.DataBind();
            }
            ddlsystemAttrVal.Items.Insert(0, new ListItem("--ALL--", "0"));
        }

        protected void ddlsystemAttrVal_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillsupplierAttrvalues(0);
        }
        
        protected void btnUpdateAllValues_Click(object sender, EventArgs e)
        {
            addupdatemsg.Style.Add("display", "none");
            msgdelundel.Style.Add("display", "none");
            msgupdateall.Style.Add("display", "none");
            UpdateAllAttrValues();
        }
    }
}