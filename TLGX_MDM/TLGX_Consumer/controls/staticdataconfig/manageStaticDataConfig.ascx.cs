using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using System.Web.UI.HtmlControls;


namespace TLGX_Consumer.controls.staticdataconfig
{
    public partial class manageStaticDataConfig1 : System.Web.UI.UserControl
    {
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();
        Controller.MappingSVCs mappingsvc = new Controller.MappingSVCs();
        public static string AttributeOptionFor = "MappingFileConfig";
        public static string AttributeOptionForType = "MappingConfigAttributeTypes";
        public static int PageIndex = 0;
        public static Guid Config_Id = Guid.Empty;
        public static Guid SelectedSupplierImportAttributeValue_Id = Guid.Empty;

        public static bool IsMapping = false;
        public static int configresultCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Config_Id = new Guid(Request.QueryString["Config_Id"]);
                fillsearchcontrolmasters();
                if (Config_Id != Guid.Empty)
                {
                    fillconfigdata();
                    fillmappingattributes();
                }
            }
        }

        private void fillmappingattributes()
        {
            if (Config_Id != Guid.Empty)
            {
                MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                RQ.SupplierImportAttribute_Id = Config_Id;
                RQ.PageNo = PageIndex;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

                var res = mappingsvc.GetStaticDataMappingAttributeValues(RQ);
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        grdMappingAttrValues.VirtualItemCount = res[0].TotalRecords;
                        lblTotalUploadConfig.Text = res[0].TotalRecords.ToString();
                        configresultCount = Convert.ToInt32(res[0].TotalRecords);
                    }
                    else
                    {
                        lblTotalUploadConfig.Text = "0";
                        configresultCount = 0;
                    }
                }
                else
                {
                    lblTotalUploadConfig.Text = "0";
                    configresultCount = 0;
                }
                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                //grdMappingAttrValues.DataSource = (from a in res orderby a.EDIT_DATE select a).ToList();
                grdMappingAttrValues.DataSource = (from a in res orderby a.CREATE_DATE descending select a).ToList();
                grdMappingAttrValues.PageIndex = PageIndex;
                grdMappingAttrValues.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                grdMappingAttrValues.DataBind();
                dvMsg.Style.Add("display", "block");
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Data was populated sucessfully", BootstrapAlertType.Success);
            }
        }

        private void fillconfigdata()
        {
            if (Config_Id != Guid.Empty)
            {
                MDMSVC.DC_SupplierImportAttributes_RQ RQ = new MDMSVC.DC_SupplierImportAttributes_RQ();
                RQ.SupplierImportAttribute_Id = Config_Id;
                RQ.PageNo = 0;
                RQ.PageSize = int.MaxValue;
                var res = mappingsvc.GetStaticDataMappingAttributes(RQ);
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        ddlFor.SelectedIndex = ddlFor.Items.IndexOf(ddlFor.Items.FindByText(res[0].For));
                        if (res[0].For.ToLower() == "mapping")
                            IsMapping = true;
                        else if (res[0].For.ToLower() == "matching")
                            IsMapping = false;
                        ddlSupplierName.SelectedIndex = ddlSupplierName.Items.IndexOf(ddlSupplierName.Items.FindByValue(res[0].Supplier_Id.ToString()));
                        ddlEntity.SelectedIndex = ddlEntity.Items.IndexOf(ddlEntity.Items.FindByText(res[0].Entity));
                        ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(res[0].Status));
                    }
                }
            }
        }

        private void fillsearchcontrolmasters()
        {
            fillfor(ddlFor);
            fillsuppliers(ddlSupplierName);
            fillentity(ddlEntity);
            fillstatus(ddlStatus);
        }

        private void fillfor(DropDownList ddl)
        {
            fillattributes(AttributeOptionFor, "AttributeFor", ddl);
        }

        private void fillstatus(DropDownList ddl)
        {
            fillattributes("SystemStatus", "Status", ddl);
        }

        private void fillentity(DropDownList ddl)
        {
            fillattributes(AttributeOptionFor, "MappingEntity", ddl);
        }

        private void fillsuppliers(DropDownList ddl)
        {
            MDMSVC.DC_Supplier_Search_RQ RQ = new MDMSVC.DC_Supplier_Search_RQ();

            RQ.PageNo = 0;
            RQ.PageSize = int.MaxValue;
            var supres = mastersvc.GetSupplier(RQ);

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
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = masterfor;
            RQ.Name = attributename;
            var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
            //Apply business logic 
            if (resvalues != null && resvalues.Count > 0)
            {
                if (ddl.ID == "ddlAttributeType")
                {
                    //If for == matching then show only two options into type dropdown match and map 
                    if (!IsMapping)
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() == "mapping" || x.AttributeValue.ToLower() == "matching" select x).ToList();
                    else
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "match" select x).ToList();
                }

            }
            ddl.DataSource = resvalues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Config_Id != Guid.Empty)
            {
                List<MDMSVC.DC_SupplierImportAttributes> _lst = new List<MDMSVC.DC_SupplierImportAttributes>();
                MDMSVC.DC_SupplierImportAttributes newObj = new MDMSVC.DC_SupplierImportAttributes
                {
                    SupplierImportAttribute_Id = Config_Id,
                    Supplier_Id = ddlFor.SelectedItem.Text.Trim().ToUpper() == "MATCHING" ? Guid.Empty : Guid.Parse(ddlSupplierName.SelectedItem.Value),
                    Entity = ddlEntity.SelectedItem.Text,
                    Status = ddlStatus.SelectedItem.Text,
                    EDIT_DATE = DateTime.Now,
                    EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name,
                    For = ddlFor.SelectedItem.Text
                };

                _lst.Add(newObj);

                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                dc = mappingsvc.UpdateStaticDataMappingAttribute(_lst);
                if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Warning);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);
                }
            }
        }

        protected void grdMappingAttrValues_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMappingAttrValues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRow_Id = Guid.Parse(grdMappingAttrValues.DataKeys[index].Values[0].ToString());

                if (e.CommandName.ToString() == "Select")
                {
                    List<MDMSVC.DC_SupplierImportAttributeValues> lstobj = new List<MDMSVC.DC_SupplierImportAttributeValues>();
                    MDMSVC.DC_SupplierImportAttributeValues obj = new MDMSVC.DC_SupplierImportAttributeValues();
                    MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                    SelectedSupplierImportAttributeValue_Id = myRow_Id;
                    RQ.SupplierImportAttributeValue_Id = myRow_Id;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    var res = mappingsvc.GetStaticDataMappingAttributeValues(RQ);

                    frmAddConfig.Visible = true;
                    frmAddConfig.ChangeMode(FormViewMode.Edit);
                    frmAddConfig.DataSource = res;
                    frmAddConfig.DataBind();
                    if (res != null && res.Count > 0)
                    {


                        hdnFlag.Value = "false";
                        DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
                        DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
                        TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
                        DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");

                        TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
                        TextBox txtPriority = (TextBox)frmAddConfig.FindControl("txtPriority"); //New Field added for Priority in Modal
                        HtmlTextArea txtDescription = (HtmlTextArea)frmAddConfig.FindControl("txtDescription");//New Field added for Description in Modal
                        DropDownList ddlAddStatus = (DropDownList)frmAddConfig.FindControl("ddlAddStatus");
                        System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
                        System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");


                        //Take data And Bind Dropdown and set it
                        if (!string.IsNullOrWhiteSpace(res[0].AttributeValue))
                        {
                            var resultForAttribute = mastersvc.GetAllAttributeAndValuesByParentAttributeValue(new MDMSVC.DC_MasterAttribute() { ParentAttributeValue_Id = res[0].AttributeValue_ID });
                            if (resultForAttribute != null && resultForAttribute.Count > 0)
                            {
                                HideShowAttributeNameControls(true);
                                ddlAttributeName.Items.Clear();
                                ddlAttributeName.DataSource = resultForAttribute;
                                ddlAttributeName.DataTextField = "AttributeValue";
                                ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                                ddlAttributeName.DataBind();
                                ddlAttributeName.Items.Insert(0, new ListItem("---Select---", "0"));

                                if (ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()) != null)
                                    ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()).Selected = true;
                            }
                            else
                            {
                                ddlAttributeName.Visible = false;
                                txtAttributeName.Visible = true;
                                txtAttributeName.Text = Convert.ToString(res[0].AttributeName);
                            }

                        }


                        fillstatus(ddlAddStatus);
                        fillattributes(AttributeOptionForType, "AttributeType", ddlAttributeType);
                        ddlAttributeType.SelectedIndex = ddlAttributeType.Items.IndexOf(ddlAttributeType.Items.FindByText(res[0].AttributeType));
                        ddlAddStatus.SelectedIndex = ddlAddStatus.Items.IndexOf(ddlAddStatus.Items.FindByText(res[0].STATUS));
                        txtAttributeName.Text = res[0].AttributeName.ToString();
                        txtPriority.Text = res[0].Priority.ToString();
                        if (res[0].Description != null)
                            txtDescription.InnerText = res[0].Description.ToString();
                        bool isFound = false;
                        if (ddlAttributeType.SelectedItem.Value != "0")
                        {
                            MDMSVC.DC_MasterAttribute RQAttr = new MDMSVC.DC_MasterAttribute();
                            RQAttr.MasterFor = AttributeOptionForType;
                            RQAttr.Name = "AttributeValues";
                            RQAttr.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                            var resvalues = mastersvc.GetAllAttributeAndValues(RQAttr);
                            if (resvalues != null)
                            {
                                if (resvalues.Count > 0)
                                {
                                    isFound = true;
                                    ddlAttributeValue.Items.Clear();
                                    ddlAttributeValue.DataSource = resvalues;
                                    ddlAttributeValue.DataTextField = "AttributeValue";
                                    ddlAttributeValue.DataValueField = "MasterAttributeValue_Id";
                                    ddlAttributeValue.DataBind();
                                    ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));
                                    dvtxtAttributeValue.Visible = false;
                                    dvddlAttributeValue.Visible = true;
                                    ddlAttributeValue.SelectedIndex = ddlAttributeValue.Items.IndexOf(ddlAttributeValue.Items.FindByText(res[0].AttributeValue.ToString()));
                                }
                            }
                        }
                        if (!isFound)
                        {
                            txtAttributeValue.Text = res[0].AttributeValue.ToString();
                            dvtxtAttributeValue.Visible = true;
                            dvddlAttributeValue.Visible = false;
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showManageModal();", true);
                }
                else if (e.CommandName.ToString() == "SoftDelete")
                {
                    List<MDMSVC.DC_SupplierImportAttributeValues> RQ = new List<MDMSVC.DC_SupplierImportAttributeValues>();

                    MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                    {
                        SupplierImportAttributeValue_Id = myRow_Id,
                        AttributeType = grdMappingAttrValues.Rows[index].Cells[0].Text,
                        AttributeName = grdMappingAttrValues.Rows[index].Cells[1].Text,
                        AttributeValue = grdMappingAttrValues.Rows[index].Cells[2].Text,
                        STATUS = "INACTIVE",
                        EDIT_DATE = DateTime.Now,
                        EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name
                    };
                    if (grdMappingAttrValues.DataKeys[index].Values[1] != null)
                    {
                        newObj.SupplierImportAttribute_Id = Guid.Parse(grdMappingAttrValues.DataKeys[index].Values[1].ToString());
                    }
                    RQ.Add(newObj);
                    MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                    dc = mappingsvc.UpdateStaticDataMappingAttributeValue(RQ);
                    if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                    { }
                    else
                    {
                        fillconfigdata();
                    }
                }
                else if (e.CommandName.ToString() == "UnDelete")
                {

                    List<MDMSVC.DC_SupplierImportAttributeValues> RQ = new List<MDMSVC.DC_SupplierImportAttributeValues>();

                    MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                    {
                        SupplierImportAttributeValue_Id = myRow_Id,
                        AttributeType = grdMappingAttrValues.Rows[index].Cells[0].Text,
                        AttributeName = grdMappingAttrValues.Rows[index].Cells[1].Text,
                        AttributeValue = grdMappingAttrValues.Rows[index].Cells[2].Text,
                        STATUS = "ACTIVE",
                        EDIT_DATE = DateTime.Now,
                        EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name
                    };
                    if (grdMappingAttrValues.DataKeys[index].Values[1] != null)
                    {
                        newObj.SupplierImportAttribute_Id = Guid.Parse(grdMappingAttrValues.DataKeys[index].Values[1].ToString());
                    }
                    RQ.Add(newObj);
                    MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                    dc = mappingsvc.UpdateStaticDataMappingAttributeValue(RQ);
                    if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                    { }
                    else
                    {
                        fillconfigdata();
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void grdMappingAttrValues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            fillmappingattributes();
        }

        protected void grdMappingAttrValues_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void frmAddConfig_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");

            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");

            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            TextBox txtPriority = (TextBox)frmAddConfig.FindControl("txtPriority"); //New Field added for priority
            HtmlTextArea txtDescription = (HtmlTextArea)frmAddConfig.FindControl("txtDescription");//New Field added for Description
            DropDownList ddlAddStatus = (DropDownList)frmAddConfig.FindControl("ddlAddStatus");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");

            if (e.CommandName == "Add")
            {
                string strAttributeName = string.Empty;
                Guid? AttributeValue_id = Guid.Empty;
                AttributeValue_id = Guid.Parse(ddlAttributeValue.SelectedValue);
                if (ddlAttributeName.Visible)
                    strAttributeName = ddlAttributeName.SelectedItem.ToString();
                else
                    strAttributeName = txtAttributeName.Text;
                MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                {
                    SupplierImportAttributeValue_Id = Guid.NewGuid(),
                    SupplierImportAttribute_Id = Config_Id,
                    AttributeType = ddlAttributeType.SelectedItem.Text,
                    AttributeName = strAttributeName,
                    AttributeValue_ID = AttributeValue_id,
                    Priority = Convert.ToInt32(txtPriority.Text),
                    Description = txtDescription.InnerText,
                    STATUS = "ACTIVE",
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };
                if (dvtxtAttributeValue.Visible)
                    newObj.AttributeValue = txtAttributeValue.Text;
                else
                    newObj.AttributeValue = ddlAttributeValue.SelectedItem.Text;

                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                dc = mappingsvc.AddStaticDataMappingAttributeValue(newObj);
                if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                {
                    hdnFlag.Value = "false";
                }
                else //if (dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    hdnFlag.Value = "true";
                    //PageIndex = 0;
                    fillmappingattributes();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);

                }
            }
            else if (e.CommandName == "Save")
            {
                List<MDMSVC.DC_SupplierImportAttributeValues> lstNewObj = new List<MDMSVC.DC_SupplierImportAttributeValues>();
                string strAttributeName = string.Empty;
                Guid? AttributeValue_id = Guid.Empty;
                AttributeValue_id = Guid.Parse(ddlAttributeValue.SelectedValue);
                if (ddlAttributeName.Visible)
                    strAttributeName = ddlAttributeName.SelectedItem.ToString();
                else
                    strAttributeName = txtAttributeName.Text;

                MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                {
                    SupplierImportAttributeValue_Id = SelectedSupplierImportAttributeValue_Id,
                    SupplierImportAttribute_Id = Config_Id,
                    AttributeType = ddlAttributeType.SelectedItem.Text,
                    AttributeName = strAttributeName,
                    AttributeValue_ID = AttributeValue_id,
                    STATUS = ddlAddStatus.SelectedItem.Text,
                    Priority = Convert.ToInt32(txtPriority.Text),
                    Description = txtDescription.InnerText,
                    EDIT_DATE = DateTime.Now,
                    EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name
                };
                if (dvtxtAttributeValue.Visible)
                    newObj.AttributeValue = txtAttributeValue.Text;
                else
                    newObj.AttributeValue = ddlAttributeValue.SelectedItem.Text;
                lstNewObj.Add(newObj);
                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                dc = mappingsvc.UpdateStaticDataMappingAttributeValue(lstNewObj);
                if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                {
                    hdnFlag.Value = "false";
                }
                else //if (dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    hdnFlag.Value = "true";
                    fillmappingattributes();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);

                }
            }
            else if (e.CommandName == "Reset")
            {
                ddlAttributeType.SelectedIndex = 0;
                txtAttributeName.Text = "";
                ddlAttributeValue.SelectedIndex = 0;
                txtAttributeValue.Text = "";
                txtPriority.Text = "";
                txtDescription.InnerText = "";
                ddlAddStatus.SelectedIndex = 0;
                dvtxtAttributeValue.Visible = true;
                dvddlAttributeValue.Visible = false;
                hdnFlag.Value = "false";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_SupplierImportAttributeValues> lstobj = new List<MDMSVC.DC_SupplierImportAttributeValues>();
            MDMSVC.DC_SupplierImportAttributeValues obj = new MDMSVC.DC_SupplierImportAttributeValues();
            lstobj.Add(obj);
            frmAddConfig.Visible = true;
            frmAddConfig.ChangeMode(FormViewMode.Insert);
            frmAddConfig.DataSource = lstobj;
            frmAddConfig.DataBind();
            hdnFlag.Value = "false";
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            fillattributes(AttributeOptionForType, "AttributeType", ddlAttributeType);
            dvddlAttributeValue.Visible = false;
            dvtxtAttributeValue.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showManageModal();", true);
        }

        protected void ddlAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            bool isFound = false;
            if (ddlAttributeType.SelectedItem.Value != "0")
            {
                MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
                RQ.MasterFor = AttributeOptionForType;
                RQ.Name = "AttributeValues";
                RQ.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
                if (resvalues != null)
                {
                    if (resvalues.Count > 0)
                    {
                        isFound = true;
                        ddlAttributeValue.Items.Clear();
                        ddlAttributeValue.DataSource = resvalues;
                        ddlAttributeValue.DataTextField = "AttributeValue";
                        ddlAttributeValue.DataValueField = "MasterAttributeValue_Id";
                        ddlAttributeValue.DataBind();
                        ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));
                        dvtxtAttributeValue.Visible = false;
                        dvddlAttributeValue.Visible = true;
                    }
                }
            }

            if (!isFound)
            {
                dvtxtAttributeValue.Visible = true;
                dvddlAttributeValue.Visible = false;
            }
        }

        protected void ddlAttributeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            if (ddlAttributeValue.SelectedItem.Value != "0")
            {
                var resvalues = mastersvc.GetAllAttributeAndValuesByParentAttributeValue(new MDMSVC.DC_MasterAttribute() { ParentAttributeValue_Id = Guid.Parse(ddlAttributeValue.SelectedValue) });
                if (resvalues != null && resvalues.Count > 0)
                {
                    HideShowAttributeNameControls(true);
                    ddlAttributeName.Items.Clear();
                    ddlAttributeName.DataSource = resvalues;
                    ddlAttributeName.DataTextField = "AttributeValue";
                    ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                    ddlAttributeName.DataBind();
                    ddlAttributeName.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                else
                {
                    HideShowAttributeNameControls(false);
                }

            }
        }

        protected void HideShowAttributeNameControls(bool blnFlag)
        {
            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            ddlAttributeName.Visible = blnFlag;
            txtAttributeName.Visible = !blnFlag;
        }
        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmappingattributes();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlFor.SelectedIndex = 0;
            ddlSupplierName.SelectedIndex = 0;
            ddlEntity.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlShowEntries.SelectedIndex = 0;
            grdMappingAttrValues.DataSource = null;
            grdMappingAttrValues.DataBind();
        }


        public void bussinessLogic()
        {

            //1. Only FileDetails will show in dropdown when there is no config

        }



    }
}