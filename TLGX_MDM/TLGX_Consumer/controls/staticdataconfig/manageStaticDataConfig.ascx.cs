﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Text;
using System.Dynamic;

namespace TLGX_Consumer.controls.staticdataconfig
{
    public partial class manageStaticDataConfig1 : System.Web.UI.UserControl
    {
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();
        Controller.MappingSVCs mappingsvc = new Controller.MappingSVCs();
        //public static string AttributeOptionFor = "MappingFileConfig";
        //public static string AttributeOptionForType = "MappingConfigAttributeTypes";
        //public static int PageIndex = 0;
        //public static Guid Config_Id = Guid.Empty;


        //public static Guid SelectedSupplierImportAttributeValue_Id = Guid.Empty;
        //public static bool IsMapping = false;
        //public static int configresultCount = 0;
        //public static int intNumberOfColumnHasBeenAdded = 0;

        public static bool IsFileDetailsFileFormatCsvTxt = false;
        public static bool IsFileDetailsFileFormatXPath = false;
        public static bool IsFileDetailsFileFormatJPath = false;




        public static string _stgTable = string.Empty;
        public static string _masterTable = string.Empty;
        public static string _mappingTable = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PageIndex = 0;
                Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
                fillsearchcontrolmasters();
                if (Config_Id != Guid.Empty)
                {
                    fillconfigdata(Config_Id);
                    fillmappingattributes(0, Config_Id);
                    fillattributeFilters(Config_Id);
                }
            }
        }



        private void fillattributeFilters(Guid Config_Id)
        {
            if (Config_Id != Guid.Empty)
            {
                //Fill Filter for attribute type and priority
                var attributes = mappingsvc.GetStaticDataMappingAttributeValuesForFilter(new MDMSVC.DC_SupplierImportAttributeValues_RQ() { For = "attributetype", SupplierImportAttribute_Id = Config_Id });
                if (attributes != null && attributes.Count > 0)
                {
                    ddlFilterAttributeType.Items.Clear();
                    ddlFilterAttributeType.DataSource = attributes;
                    ddlFilterAttributeType.DataBind();
                    ddlFilterAttributeType.Items.Insert(0, new ListItem("All", "-1"));

                }

                var prioritys = mappingsvc.GetStaticDataMappingAttributeValuesForFilter(new MDMSVC.DC_SupplierImportAttributeValues_RQ() { For = "priority", SupplierImportAttribute_Id = Config_Id });
                if (prioritys != null && prioritys.Count > 0)
                {
                    ddlFilterPriority.Items.Clear();
                    ddlFilterPriority.DataSource = prioritys;
                    ddlFilterPriority.DataBind();
                    ddlFilterPriority.Items.Insert(0, new ListItem("All", "-1"));

                }
            }
        }

        private void setNoOfColumnsForMapType(int PageIndex, Guid Config_Id)
        {
            if (Config_Id != Guid.Empty)
            {
                MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                RQ.SupplierImportAttribute_Id = Config_Id;
                RQ.PageNo = PageIndex;

                if (ddlFilterPriority.SelectedItem.Value != "-1")
                    RQ.Priority = Convert.ToInt32(ddlFilterPriority.SelectedItem.Text); //intFilterPriority;
                else
                    RQ.Priority = -1;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

                var res = mappingsvc.GetStaticDataMappingAttributeValues(RQ);
                if (res != null)
                {
                    if (res.Count > 0)
                    {

                        //Set Value for IsNumberOfColumnHasBeenAdded
                        //var resNumberOfColumns = (from x in res where x.AttributeValue.ToLower() == "numberofcolumns" select x).ToList();
                        //if (resNumberOfColumns != null && resNumberOfColumns.Count > 0)
                        //{
                        //    if ((resNumberOfColumns[0].AttributeName) != null && (resNumberOfColumns[0].AttributeName) != string.Empty)
                        //        intNumberOfColumnHasBeenAdded = Convert.ToInt32(resNumberOfColumns[0].AttributeName);
                        //}
                        //else
                        //    intNumberOfColumnHasBeenAdded = 0;
                    }
                    else
                    {
                        lblTotalUploadConfig.Text = "0";
                        lblconfigresultCount.Text = "0";
                    }
                }
                else
                {
                    lblTotalUploadConfig.Text = "0";
                    lblconfigresultCount.Text = "0";
                }

            }
        }

        private int intNumberOfColumnHasBeenAdded()
        {
            int ColumnCount = 0;
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);

            if (Config_Id != Guid.Empty)
            {
                MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                RQ.SupplierImportAttribute_Id = Config_Id;
                RQ.PageNo = 0;

                if (ddlFilterPriority.SelectedItem.Value != "-1")
                    RQ.Priority = Convert.ToInt32(ddlFilterPriority.SelectedItem.Text); //intFilterPriority;
                else
                    RQ.Priority = -1;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

                var res = mappingsvc.GetStaticDataMappingAttributeValues(RQ);

                if (res != null)
                {
                    if (res.Count > 0)
                    {

                        //Set Value for IsNumberOfColumnHasBeenAdded
                        var resNumberOfColumns = (from x in res where x.AttributeValue.ToLower() == "numberofcolumns" select x).ToList();
                        if (resNumberOfColumns != null && resNumberOfColumns.Count > 0)
                        {
                            if ((resNumberOfColumns[0].AttributeName) != null && (resNumberOfColumns[0].AttributeName) != string.Empty)
                                ColumnCount = Convert.ToInt32(resNumberOfColumns[0].AttributeName);
                        }
                        else
                            ColumnCount = 0;
                    }
                }
            }

            return ColumnCount;
        }

        private void fillmappingattributes(int PageIndex, Guid Config_Id)
        {
            if (Config_Id != Guid.Empty)
            {
                MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                RQ.SupplierImportAttribute_Id = Config_Id;
                RQ.PageNo = PageIndex;

                if (ddlFilterPriority.SelectedItem.Value != "-1")
                    RQ.Priority = Convert.ToInt32(ddlFilterPriority.SelectedItem.Text); //intFilterPriority;
                else
                    RQ.Priority = -1;
                if (ddlFilterAttributeType.SelectedItem.Value != "-1")
                    RQ.AttributeType = ddlFilterAttributeType.SelectedItem.Text; //strFilterAttributeType;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

                var res = mappingsvc.GetStaticDataMappingAttributeValues(RQ);

                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        grdMappingAttrValues.VirtualItemCount = res[0].TotalRecords;
                        lblTotalUploadConfig.Text = res[0].TotalRecords.ToString();
                        lblconfigresultCount.Text = Convert.ToInt32(res[0].TotalRecords).ToString();

                        //Set Valur For File Type
                        var resAttributeFileformate = (from x in res where x.AttributeValue.ToLower() == "format" select x).ToList();
                        if (resAttributeFileformate != null && resAttributeFileformate.Count > 0)
                        {
                            var AttTypeName = (from x in resAttributeFileformate select x.AttributeName).FirstOrDefault();

                            if (AttTypeName.ToLower() == "csv" || AttTypeName.ToLower() == "text")
                                IsFileDetailsFileFormatCsvTxt = true;
                            else if (AttTypeName.ToLower() == "json")
                                IsFileDetailsFileFormatJPath = true;
                            else if (AttTypeName.ToLower() == "xml")
                                IsFileDetailsFileFormatXPath = true;
                        }
                        else
                            IsFileDetailsFileFormatCsvTxt = false;
                        //Set Value for IsNumberOfColumnHasBeenAdded
                        var resNumberOfColumns = (from x in res where x.AttributeValue.ToLower() == "numberofcolumns" select x).ToList();
                        //if (resNumberOfColumns != null && resNumberOfColumns.Count > 0)
                        //{
                        //    if ((resNumberOfColumns[0].AttributeName) != null && (resNumberOfColumns[0].AttributeName) != string.Empty)
                        //        intNumberOfColumnHasBeenAdded = Convert.ToInt32(resNumberOfColumns[0].AttributeName);
                        //}
                        //else
                        //    intNumberOfColumnHasBeenAdded = 0;
                    }
                    else
                    {
                        lblTotalUploadConfig.Text = "0";
                        lblconfigresultCount.Text = "0";
                    }
                }
                else
                {
                    lblTotalUploadConfig.Text = "0";
                    lblconfigresultCount.Text = "0";
                }
                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                grdMappingAttrValues.DataSource = (from a in res orderby a.CREATE_DATE descending select a).ToList();
                grdMappingAttrValues.PageIndex = PageIndex;
                grdMappingAttrValues.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                grdMappingAttrValues.DataBind();
                dvMsg.Style.Add("display", "block");
                //BootstrapAlert.BootstrapAlertMessage(dvMsg, "Data was populated sucessfully", BootstrapAlertType.Success);
            }
        }

        private dynamic IsMapping(string Type)
        {
            dynamic obj = new ExpandoObject();
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);

            if (Config_Id != Guid.Empty)
            {
                MDMSVC.DC_SupplierImportAttributes_RQ RQ = new MDMSVC.DC_SupplierImportAttributes_RQ();
                RQ.SupplierImportAttribute_Id = Config_Id;
                RQ.PageNo = 0;
                RQ.PageSize = int.MaxValue;

                var res = mappingsvc.GetStaticDataMappingAttributes(RQ);

                switch (Type)
                {
                    case "bool":
                        if (res != null)
                        {
                            if (res.Count > 0)
                            {
                                if (res[0].For.ToLower() == "mapping")
                                    obj = true;
                                else if (res[0].For.ToLower() == "matching")
                                    obj = false;
                            }
                        }
                        break;
                    case "_stgTable":
                        if (res != null) { if (res.Count > 0) { obj = Convert.ToString(res[0].STG_Table); } else { obj = ""; } } else { obj = ""; }
                        break;
                    case "_masterTable":
                        if (res != null) { if (res.Count > 0) { obj = Convert.ToString(res[0].Master_Table); } else { obj = ""; } } else { obj = ""; }
                        break;
                    case "_mappingTable":
                        if (res != null) { if (res.Count > 0) { obj = Convert.ToString(res[0].Mapping_Table); } else { obj = ""; } } else { obj = ""; }
                        break;
                    default:
                        break;
                }


            }

            return obj;
        }

        private void fillconfigdata(Guid Config_Id)
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
                        
                        //Set mappind,master and staging table name
                        _mappingTable = Convert.ToString(res[0].Mapping_Table);
                        _masterTable = Convert.ToString(res[0].Master_Table);
                        _stgTable = Convert.ToString(res[0].STG_Table);

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

        private void EnableDisableValidation(RequiredFieldValidator rqfv)
        {
            if (rqfv != null)
                rqfv.Enabled = true;
        }
        private void fillfor(DropDownList ddl)
        {
            fillattributes("MappingFileConfig", "AttributeFor", ddl);
        }

        private void fillstatus(DropDownList ddl)
        {
            fillattributes("SystemStatus", "Status", ddl);
        }

        private void fillentity(DropDownList ddl)
        {
            fillattributes("MappingFileConfig", "MappingEntity", ddl);
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
                    if (!IsMapping("bool"))
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() == "map" || x.AttributeValue.ToLower() == "match" select x).ToList();
                    else if (IsMapping("bool"))
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "match" select x).ToList();


                    //If no file config is added, show only "File Details" opens in File drop down
                    if (Convert.ToInt32(lblconfigresultCount.Text) == 0 && IsMapping("bool"))
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() == "filedetails" select x).ToList();

                    //Distinct & Filter option only available when number of column has been added
                    if (Convert.ToInt32(lblconfigresultCount.Text) > 0 && intNumberOfColumnHasBeenAdded() == 0)
                    {
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "filter" select x).ToList();
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "distinct" select x).ToList();
                    }

                    //For Xpath and Jpath option available only
                    if (intNumberOfColumnHasBeenAdded() > 0)
                    {
                        if (IsFileDetailsFileFormatXPath || IsFileDetailsFileFormatJPath)
                        {
                            if (IsFileDetailsFileFormatXPath)
                                resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "jpath" select x).ToList();
                            else if (IsFileDetailsFileFormatJPath)
                                resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "xpath" select x).ToList();
                        }
                        else
                            resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "xpath" && x.AttributeValue.ToLower() != "jpath" select x).ToList();
                    }
                }

            }
            ddl.DataSource = resvalues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        public void fillComparisonOperators(string masterfor, string attributename, DropDownList ddl)
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = masterfor;
            RQ.Name = attributename;
            var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
            ddl.Items.Clear();
            ddl.DataSource = resvalues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "AttributeValue";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
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
                    dvMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Warning);
                    // PageIndex = 0;
                    fillmappingattributes(0, Config_Id);
                }
                else
                {
                    dvMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);
                }
            }
        }

        protected void grdMappingAttrValues_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMappingAttrValues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRow_Id = Guid.Parse(grdMappingAttrValues.DataKeys[index].Values[0].ToString());

                if (e.CommandName.ToString() == "Select")
                {
                    int intNoOfColumn = intNumberOfColumnHasBeenAdded();

                    dvModalMsg.Visible = false;
                    List<MDMSVC.DC_SupplierImportAttributeValues> lstobj = new List<MDMSVC.DC_SupplierImportAttributeValues>();
                    MDMSVC.DC_SupplierImportAttributeValues obj = new MDMSVC.DC_SupplierImportAttributeValues();
                    MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                    lblSelectedSupplierImportAttributeValue_Id.Text = Convert.ToString(myRow_Id);
                    RQ.SupplierImportAttributeValue_Id = myRow_Id;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    var res = mappingsvc.GetStaticDataMappingAttributeValues(RQ);

                    frmAddConfig.Visible = true;
                    frmAddConfig.ChangeMode(FormViewMode.Edit);
                    frmAddConfig.DataSource = res;
                    frmAddConfig.DataBind();
                    hdnFlag.Value = "false";

                    if (res != null && res.Count > 0)
                    {

                        #region Get All Controls
                        hdnFlag.Value = "false";
                        DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
                        DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
                        TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
                        DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
                        RequiredFieldValidator rqfvddlAttributeValuedll = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValuedll");
                        RequiredFieldValidator rqfvddlAttributeValue = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValue");
                        RequiredFieldValidator rqfvddlAttributeValueFrom = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueFrom");
                        RequiredFieldValidator rqfvddlAttributeValueTo = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueTo");
                        RequiredFieldValidator rqfvddlAttributeValueFilter = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueFilter");

                        TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
                        TextBox txtPriority = (TextBox)frmAddConfig.FindControl("txtPriority"); //New Field added for Priority in Modal
                        HtmlTextArea txtDescription = (HtmlTextArea)frmAddConfig.FindControl("txtDescription");//New Field added for Description in Modal
                        DropDownList ddlAddStatus = (DropDownList)frmAddConfig.FindControl("ddlAddStatus");
                        System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
                        System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
                        System.Web.UI.HtmlControls.HtmlGenericControl dvAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvAttributeValue");
                        FilteredTextBoxExtender axfte_txtAttributeName = (FilteredTextBoxExtender)frmAddConfig.FindControl("axfte_txtAttributeName");
                        HtmlGenericControl dvtxtPriority = (HtmlGenericControl)frmAddConfig.FindControl("dvtxtPriority");
                        HtmlGenericControl dvValueForFilter = (HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");
                        HtmlGenericControl divReplaceValue = (HtmlGenericControl)frmAddConfig.FindControl("divReplaceValue");

                        HiddenField hdnddlAttributeTableName = (HiddenField)frmAddConfig.FindControl("hdnddlAttributeTableName");
                        HiddenField hdnddlAttributeTableValueName = (HiddenField)frmAddConfig.FindControl("hdnddlAttributeTableValueName");
                        HiddenField hdnIsReplaceWith = (HiddenField)frmAddConfig.FindControl("hdnIsReplaceWith");
                        TextBox txtReplaceFrom = (TextBox)frmAddConfig.FindControl("txtReplaceFrom");
                        TextBox txtReplaceTo = (TextBox)frmAddConfig.FindControl("txtReplaceTo");

                        //matchBy
                        System.Web.UI.HtmlControls.HtmlGenericControl dvMatchByColumnOrValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvMatchByColumnOrValue");
                        DropDownList ddlComparisonValue = (DropDownList)frmAddConfig.FindControl("ddlComparisonValue");
                        RadioButton rdoIsMatchByColumn = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByColumn");
                        RadioButton rdoIsMatchByValue = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByValue");
                        dvMatchByColumnOrValue.Visible = false;
                        ddlComparisonValue.Visible = false;
                        //end
                        #endregion //Get All Controls

                        #region Priority 
                        if (IsMapping("bool"))
                            dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "none");
                        else
                            dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "block");
                        txtPriority.Text = res[0].Priority.ToString();


                        #endregion //END Priority
                        //Fill & Set attribute Type
                        #region Attribute Type
                        fillattributes("MappingConfigAttributeTypes", "AttributeType", ddlAttributeType);
                        ddlAttributeType.SelectedIndex = ddlAttributeType.Items.IndexOf(ddlAttributeType.Items.FindByText(res[0].AttributeType));

                        #endregion //End Attribute Type
                        //Fill and Set Attribute Name 
                        #region Attribute Name
                        txtAttributeName.Visible = ddlAttributeName.Visible = false;

                        if (ddlAttributeType.SelectedItem.Value != "0")
                        {
                            #region For Distinct and Filter AttributeType
                            if (ddlAttributeType.SelectedItem.Text.ToLower() == "distinct" || ddlAttributeType.SelectedItem.Text.ToLower() == "filter")
                            {


                                ddlAttributeName.Visible = true;

                                if (intNoOfColumn > 0)
                                {
                                    ddlAttributeName.Items.Clear();
                                    ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                                    for (int i = --intNoOfColumn; i >= 0; i--)
                                        ddlAttributeName.Items.Insert(1, new ListItem(Convert.ToString(i), Convert.ToString(i)));

                                    if (ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()) != null)
                                        ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()).Selected = true;
                                }

                            }
                            else if (ddlAttributeType.SelectedItem.Text.ToLower() == "map" || ddlAttributeType.SelectedItem.Text.ToLower() == "match")
                            {
                                List<string> lstAttributeName = new List<string>();
                                if (!IsMapping("bool") && res[0].AttributeType.ToLower() == "map")
                                {
                                    lstAttributeName = mastersvc.GetListOfColumnNamesByTable(_mappingTable);
                                    hdnddlAttributeTableName.Value = _mappingTable;
                                }
                                else if (IsMapping("bool") && res[0].AttributeType.ToLower() == "map")
                                {
                                    if (intNoOfColumn > 0)
                                    {
                                        ddlAttributeName.Items.Clear();
                                        for (int i = --intNoOfColumn; i >= 0; i--)
                                            lstAttributeName.Add(i.ToString());

                                    }
                                    hdnddlAttributeTableName.Value = "0";
                                }
                                else if (!IsMapping("bool") && res[0].AttributeType.ToLower() == "match")
                                {
                                    lstAttributeName = mastersvc.GetListOfColumnNamesByTable(_mappingTable);
                                    hdnddlAttributeTableName.Value = _mappingTable;

                                }

                                if (lstAttributeName != null)
                                {
                                    ddlAttributeName.Visible = true;
                                    ddlAttributeName.Items.Clear();
                                    ddlAttributeName.DataSource = lstAttributeName;
                                    ddlAttributeName.DataBind();
                                    ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                                    string strAttribute = string.Empty;
                                    if (res[0].AttributeName.ToString().Split('.').Length > 1)
                                        strAttribute = res[0].AttributeName.ToString().Split('.')[1];
                                    else
                                        strAttribute = res[0].AttributeName.ToString();

                                    ddlAttributeName.SelectedIndex = ddlAttributeName.Items.IndexOf(ddlAttributeName.Items.FindByText(strAttribute));
                                }

                            }
                            else if (ddlAttributeType.SelectedItem.Text.ToLower() == "jpath" || ddlAttributeType.SelectedItem.Text.ToLower() == "xpath")
                            {
                                ddlAttributeName.Visible = true;
                                if (intNoOfColumn > 0)
                                {
                                    ddlAttributeName.Items.Clear();
                                    for (int i = --intNoOfColumn; i >= 0; i--)
                                        ddlAttributeName.Items.Insert(0, new ListItem(Convert.ToString(i), Convert.ToString(i)));

                                    ddlAttributeName.Items.Insert(0, new ListItem("SchemaLocation", "SchemaLocation"));

                                    if (ddlAttributeName.Items.FindByText(res[0].AttributeValue.ToString()) != null)
                                        ddlAttributeName.Items.FindByText(res[0].AttributeValue.ToString()).Selected = true;
                                }
                            }
                            else if (IsMapping("bool") && ddlAttributeType.SelectedItem.Text.ToLower() == "keyword")
                            {
                                ddlAttributeName.Visible = true;
                                var lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_mappingTable);
                                hdnddlAttributeTableValueName.Value = _mappingTable;
                                hdnddlAttributeTableName.Value = _mappingTable;
                                if (lstAttributeValue != null)
                                {
                                    ddlAttributeName.Visible = true;
                                    ddlAttributeName.Items.Clear();
                                    ddlAttributeName.DataSource = lstAttributeValue;
                                    ddlAttributeName.DataBind();
                                    ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                                    ddlAttributeValue.Visible = true;
                                    ddlAttributeValue.Items.Clear();
                                    ddlAttributeValue.DataSource = lstAttributeValue;
                                    ddlAttributeValue.DataBind();
                                    ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));
                                    EnableDisableValidation(rqfvddlAttributeValuedll);

                                    string strAttributeName = string.Empty;
                                    string strAttributeVal = string.Empty;
                                    if (res[0].AttributeName.ToString().Split('.').Length > 1)
                                        strAttributeName = res[0].AttributeName.ToString().Split('.')[1];
                                    else
                                        strAttributeName = res[0].AttributeName.ToString();
                                    if (res[0].AttributeValue.ToString().Split('.').Length > 1)
                                        strAttributeVal = res[0].AttributeValue.ToString().Split('.')[1];
                                    else
                                        strAttributeVal = res[0].AttributeValue.ToString();

                                    ddlAttributeName.SelectedIndex = ddlAttributeName.Items.IndexOf(ddlAttributeName.Items.FindByText(strAttributeName));
                                    ddlAttributeValue.SelectedIndex = ddlAttributeValue.Items.IndexOf(ddlAttributeValue.Items.FindByText(strAttributeVal));
                                }

                            }
                            #endregion
                            else
                            {
                                MDMSVC.DC_MasterAttribute RQAttr = new MDMSVC.DC_MasterAttribute();
                                RQAttr.MasterFor = "MappingConfigAttributeTypes";
                                RQAttr.Name = "AttributeValues";
                                RQAttr.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                                var resvalues = mastersvc.GetAllAttributeAndValues(RQAttr);
                                if (resvalues != null && resvalues.Count > 0)
                                {
                                    ddlAttributeName.Visible = true;
                                    
                                    ddlAttributeName.Items.Clear();
                                    ddlAttributeName.DataSource = resvalues;
                                    ddlAttributeName.DataTextField = "AttributeValue";
                                    ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                                    ddlAttributeName.DataBind();
                                    ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                                    ddlAttributeName.SelectedIndex = ddlAttributeName.Items.IndexOf(ddlAttributeName.Items.FindByText(res[0].AttributeValue.ToString()));
                                }
                                else
                                    txtAttributeName.Visible = true;
                            }
                        }


                        #endregion //End Attribute Name
                        //Fill and Set Attribute Value
                        #region Fill & Set Attribute Value
                        string strAttributeValue = Convert.ToString(res[0].AttributeValue);
                        ddlAttributeValue.Visible = txtAttributeValue.Visible = false;
                        divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "none");
                        if (!string.IsNullOrWhiteSpace(strAttributeValue))
                        {
                            #region For Filter
                            if (res[0].AttributeType.ToLower() == "filter")
                            {
                                dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");
                                //rqfvddlAttributeValueFilter.Enabled = true;
                                //rqfvddlAttributeValue.Enabled = false;
                                //EnableDisableValidation(rqfvddlAttributeValueFilter);

                                StringBuilder sbinnerhtml = new StringBuilder();
                                string[] strArrayAttributeValue = strAttributeValue.Split(',');
                                sbinnerhtml.Append(dvValueForFilter.InnerHtml);
                                for (int i = 0; i < strArrayAttributeValue.Count(); i++)
                                {

                                    //sbinnerhtml.Append("<div class='con inner-addon right-addon'><i id='btnAddValue' style='cursor: pointer' class='btnRemove glyphicon glyphicon-minus'></i>");
                                    //string strID = "txt" + i.ToString();
                                    //sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control' value=" + strArrayAttributeValue[i] + " /></div>");
                                    sbinnerhtml.Append("<div class='con'>");
                                    string strID = "txt" + i.ToString();
                                    // sbinnerhtml.Append("<div class='con inner-addon right-addon'><i id='btnAddValue' style='cursor: pointer' class='btnRemove glyphicon glyphicon-minus'></i>");
                                    sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control col-md-8 inputTypeForFilter' value=" + strArrayAttributeValue[i] + " />");
                                    sbinnerhtml.Append("<div class='input-group-btn col-md-4' style='padding-left: 0px !important;'><button class='btn btn-default btnRemove' id='btnAddValue' type='button'>");
                                    sbinnerhtml.Append("<i class='glyphicon glyphicon-minus'></i></button>");
                                    sbinnerhtml.Append("</div>");
                                    sbinnerhtml.Append("</div>");
                                }
                                dvValueForFilter.InnerHtml = sbinnerhtml.ToString();
                            }
                            #endregion //End  For Filter
                            else if (ddlAttributeType.SelectedItem.Text.ToLower() == "map" || ddlAttributeType.SelectedItem.Text.ToLower() == "match")
                            {
                                fillComparisonOperators("MappingConfigAttributeTypes", "ComparisonOperators", ddlComparisonValue);
                                List<string> lstAttributeValue = null;
                                if (IsMapping("bool") && res[0].AttributeType.ToLower() == "map")
                                {
                                    //lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_stgTable);
                                    //hdnddlAttributeTableValueName.Value = _stgTable;
                                    hdnddlAttributeTableValueName.Value = IsMapping("_stgTable");
                                    lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(hdnddlAttributeTableValueName.Value);



                                }
                                else if (!IsMapping("bool") && res[0].AttributeType.ToLower() == "map")
                                {
                                    //lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_stgTable);
                                    //hdnddlAttributeTableValueName.Value = _stgTable;
                                    hdnddlAttributeTableValueName.Value = IsMapping("_stgTable");
                                    lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(hdnddlAttributeTableValueName.Value);

                                }

                                else if (!IsMapping("bool") && res[0].AttributeType.ToLower() == "match")
                                {
                                    #region y match By
                                    if (ddlComparisonValue.Items.FindByText(res[0].Comparison) != null)
                                    {
                                        ddlComparisonValue.SelectedIndex = ddlComparisonValue.Items.IndexOf(ddlComparisonValue.Items.FindByText(res[0].Comparison));
                                    }

                                    dvMatchByColumnOrValue.Visible = true;

                                    if (res[0].AttributeValueType != null)
                                    {
                                        if (res[0].AttributeValueType.ToUpper() == "VALUE")
                                        {
                                            rdoIsMatchByColumn.Checked = false;
                                            rdoIsMatchByValue.Checked = true;
                                            txtAttributeValue.Text = res[0].AttributeValue;
                                            //ddlAttributeValue.Style.Add("display", "none");
                                            ddlAttributeValue.Visible = false;
                                            txtAttributeValue.Visible = true;
                                            ddlComparisonValue.Visible = true;

                                        }
                                        else
                                        {
                                            rdoIsMatchByColumn.Checked = true;
                                        }
                                    }
                                    #endregion
                                    lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_masterTable);
                                    hdnddlAttributeTableValueName.Value = _masterTable;

                                }
                                if (lstAttributeValue != null)
                                {
                                    //y match By
                                    if (res[0].AttributeValueType.ToUpper() == "VALUE")
                                    {
                                        ddlAttributeValue.Items.Clear();
                                        ddlAttributeValue.DataSource = lstAttributeValue;
                                        ddlAttributeValue.DataBind();
                                        ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));
                                    }
                                    //end y match By
                                    else
                                    {
                                        ddlAttributeValue.Visible = true;
                                        ddlAttributeValue.Items.Clear();
                                        ddlAttributeValue.DataSource = lstAttributeValue;
                                        ddlAttributeValue.DataBind();
                                        ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));
                                        ddlAttributeValue.SelectedIndex = ddlAttributeValue.Items.IndexOf(ddlAttributeValue.Items.FindByText(res[0].AttributeValue.ToString().Split('.')[1]));

                                        EnableDisableValidation(rqfvddlAttributeValuedll);
                                    }

                                }
                            }
                            else if (ddlAttributeType.SelectedItem.Text.ToLower() == "format")
                            {
                                if (ddlAttributeName.SelectedItem.Text.ToLower() != "replace")
                                {
                                    divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "none");
                                    hdnIsReplaceWith.Value = Convert.ToString(false);
                                    txtAttributeValue.Visible = true;
                                    txtAttributeValue.Text = res[0].AttributeName;
                                }
                                else
                                {
                                    divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "block");
                                    hdnIsReplaceWith.Value = Convert.ToString(true);
                                    var data = Convert.ToString(res[0].AttributeName).Split(new string[] { "\",\"" }, StringSplitOptions.None);
                                    if (!string.IsNullOrWhiteSpace(res[0].AttributeName) && data.Length > 1)
                                    {
                                        txtReplaceFrom.Text = data[0].TrimStart('"');
                                        txtReplaceTo.Text = data[1].TrimEnd('"');
                                    }
                                }
                            }
                            else if (ddlAttributeType.SelectedItem.Text.ToLower() == "keyword")
                            {
                                divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "none");
                                txtAttributeValue.Visible = false;
                                ddlAttributeValue.Visible = true;
                            }
                            else
                            {
                                if (res[0].AttributeValue_ID.HasValue)
                                {
                                    var resultForAttribute = mastersvc.GetAllAttributeAndValuesByParentAttributeValue(new MDMSVC.DC_MasterAttribute() { ParentAttributeValue_Id = res[0].AttributeValue_ID });
                                    if (resultForAttribute != null && resultForAttribute.Count > 0)
                                    {
                                        ddlAttributeValue.Visible = true;
                                        ddlAttributeValue.Items.Clear();
                                        ddlAttributeValue.DataSource = resultForAttribute;
                                        ddlAttributeValue.DataTextField = "AttributeValue";
                                        ddlAttributeValue.DataValueField = "MasterAttributeValue_Id";
                                        ddlAttributeValue.DataBind();
                                        ddlAttributeValue.Items.Insert(0, new ListItem("---Select---", "0"));
                                        EnableDisableValidation(rqfvddlAttributeValuedll);

                                        if (ddlAttributeValue.Items.FindByText(res[0].AttributeName.ToString()) != null)
                                            ddlAttributeValue.Items.FindByText(res[0].AttributeName.ToString()).Selected = true;
                                    }

                                }
                                if (ddlAttributeValue.Visible != true)
                                {
                                    txtAttributeValue.Visible = true;
                                    if (res[0].AttributeValue.ToLower() == "numberofcolumns")
                                        axfte_txtAttributeName.Enabled = true;
                                    else
                                        axfte_txtAttributeName.Enabled = false;

                                    txtAttributeValue.Text = res[0].AttributeName.ToString();
                                }
                            }
                        }

                        #endregion // End Fill & Set Attribute Value
                        //Fill & Set Status
                        #region Status 
                        fillstatus(ddlAddStatus);
                        ddlAddStatus.SelectedIndex = ddlAddStatus.Items.IndexOf(ddlAddStatus.Items.FindByText(res[0].STATUS));

                        #endregion //End Status
                        //Set 
                        #region Description 
                        if (res[0].Description != null)
                            txtDescription.InnerText = res[0].Description.ToString();
                        #endregion // End Description 

                        #region Hide 'Value' Section
                        string valAttributeType = ddlAttributeType.SelectedItem.Text.ToLower();
                        if (valAttributeType == "decode" || valAttributeType == "encode" || valAttributeType == "distinct" || valAttributeType == "filter")
                        {
                            if (valAttributeType == "filter")
                            {
                                if (divReplaceValue.Visible)
                                    divReplaceValue.Visible = false;
                            }
                            else
                            {
                                if (dvAttributeValue.Visible)
                                    dvAttributeValue.Visible = false;
                            }

                        }
                        else
                        {
                            if (!dvAttributeValue.Visible)
                            {
                                dvAttributeValue.Visible = true;
                                dvAttributeValue.Style.Add(HtmlTextWriterStyle.Display, "block");
                            }
                        }
                        #endregion

                    }
                    dvMsg.Visible = false;
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
                        Priority = Convert.ToInt32(string.IsNullOrEmpty(grdMappingAttrValues.Rows[index].Cells[4].Text.Trim()) ? "0" : grdMappingAttrValues.Rows[index].Cells[4].Text.Trim()),
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
                    {
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Duplicate);
                    }
                    else
                    {
                        fillconfigdata(Config_Id);
                        fillmappingattributes(0, Config_Id);
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Mapping Attribute Value has been deleted successfully", BootstrapAlertType.Success);
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
                        Priority = Convert.ToInt32(string.IsNullOrEmpty(grdMappingAttrValues.Rows[index].Cells[4].Text.Trim()) ? "0" : grdMappingAttrValues.Rows[index].Cells[4].Text.Trim()),
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
                    {
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Duplicate);
                    }
                    else
                    {
                        fillconfigdata(Config_Id);
                        fillmappingattributes(0, Config_Id);
                        dvMsg.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Mapping Attribute Value has been retrived successfully", BootstrapAlertType.Success);
                    }
                }
            }
            catch
            {
            }
        }

        protected void grdMappingAttrValues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //PageIndex = e.NewPageIndex;
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
            fillmappingattributes(e.NewPageIndex, Config_Id);
            dvMsg.Visible = false;
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
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);

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
            System.Web.UI.HtmlControls.HtmlGenericControl dvValueForFilter = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");
            System.Web.UI.HtmlControls.HtmlGenericControl divReplaceValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("divReplaceValue");
            HiddenField hdnValueWithCommaSeprated = (HiddenField)frmAddConfig.FindControl("hdnValueWithCommaSeprated");
            HiddenField hdnddlAttributeTableName = (HiddenField)frmAddConfig.FindControl("hdnddlAttributeTableName");
            HiddenField hdnddlAttributeTableValueName = (HiddenField)frmAddConfig.FindControl("hdnddlAttributeTableValueName");
            HiddenField hdnIsReplaceWith = (HiddenField)frmAddConfig.FindControl("hdnIsReplaceWith");

            TextBox txtReplaceFrom = (TextBox)frmAddConfig.FindControl("txtReplaceFrom"); //New Field added for priority
            TextBox txtReplaceTo = (TextBox)frmAddConfig.FindControl("txtReplaceTo"); //New Field added for priority
                                                                                      //MatchBy
            RadioButton rdoIsMatchByColumn = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByColumn");
            RadioButton rdoIsMatchByValue = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByValue");
            DropDownList ddlComparisonValue = (DropDownList)frmAddConfig.FindControl("ddlComparisonValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvMatchByColumnOrValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvMatchByColumnOrValue");
            //End
            if (e.CommandName == "Add")
            {
                string strAttributeValue = string.Empty;
                string strAttributeName = string.Empty;
                Guid AttributeValue_id = Guid.Empty;

                string strAttributeValueType = string.Empty;
                string strcomparisonValue = string.Empty;

                bool blnIsGuid = Guid.TryParse(ddlAttributeName.SelectedValue, out AttributeValue_id);
                if (blnIsGuid && ddlAttributeName.Visible)
                {
                    AttributeValue_id = Guid.Parse(ddlAttributeName.SelectedValue);
                }
                else
                    AttributeValue_id = Guid.Empty;

                if (ddlAttributeName.Visible)
                {
                    if (hdnddlAttributeTableName != null && !string.IsNullOrEmpty(hdnddlAttributeTableName.Value))
                    {
                        if (!string.IsNullOrEmpty(hdnddlAttributeTableName.Value) && hdnddlAttributeTableName.Value != "0")
                            strAttributeName = hdnddlAttributeTableName.Value + "." + ddlAttributeName.SelectedItem.ToString();
                        else
                            strAttributeName = ddlAttributeName.SelectedItem.ToString();
                    }
                    else
                        strAttributeValue = ddlAttributeName.SelectedItem.ToString();
                }
                else
                    strAttributeValue = txtAttributeName.Text;


                if (ddlAttributeValue.Visible)
                {
                    if (hdnddlAttributeTableValueName != null && !string.IsNullOrEmpty(hdnddlAttributeTableValueName.Value))
                        strAttributeValue = hdnddlAttributeTableValueName.Value + "." + ddlAttributeValue.SelectedItem.Text;
                    else
                        strAttributeName = ddlAttributeValue.SelectedItem.Text;
                }
                else if (txtAttributeValue.Visible)
                    strAttributeName = txtAttributeValue.Text;
                else if (Convert.ToBoolean(hdnIsReplaceWith.Value))
                {
                    string strReplace = "\"" + txtReplaceFrom.Text + "\",\"" + txtReplaceTo.Text + "\"";
                    strAttributeName = strReplace;
                }
                else
                    strAttributeName = hdnValueWithCommaSeprated.Value != string.Empty ? Convert.ToString(hdnValueWithCommaSeprated.Value).TrimEnd(',') : string.Empty;


                //For filter 
                string strAttributeType = Convert.ToString(ddlAttributeType.SelectedItem.Text).ToLower();
                if (strAttributeType == "filter" || strAttributeType == "distinct")
                {
                    string strtemp = strAttributeName;
                    strAttributeName = strAttributeValue;
                    strAttributeValue = strtemp;
                }
                //match by
                if (dvMatchByColumnOrValue.Visible)
                {
                    if (rdoIsMatchByValue.Checked)
                    {
                        strAttributeValueType = rdoIsMatchByValue.Text.ToUpper();
                        if (ddlComparisonValue.SelectedValue != "0")
                            strcomparisonValue = ddlComparisonValue.SelectedValue;
                        strAttributeValue = txtAttributeValue.Text;
                    }
                    else
                    {
                        strAttributeValueType = rdoIsMatchByColumn.Text.ToUpper();
                        strcomparisonValue = "=";
                    }
                    if (hdnddlAttributeTableName != null && !string.IsNullOrEmpty(hdnddlAttributeTableName.Value))
                    {
                        if (!string.IsNullOrEmpty(hdnddlAttributeTableName.Value) && hdnddlAttributeTableName.Value != "0")
                            strAttributeName = hdnddlAttributeTableName.Value + "." + ddlAttributeName.SelectedItem.ToString();
                        else
                            strAttributeName = ddlAttributeName.SelectedItem.ToString();
                    }
                }

                MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                {
                    SupplierImportAttributeValue_Id = Guid.NewGuid(),
                    SupplierImportAttribute_Id = Config_Id,
                    AttributeType = ddlAttributeType.SelectedItem.Text,
                    AttributeName = strAttributeName,
                    AttributeValue = strAttributeValue,
                    AttributeValue_ID = AttributeValue_id,
                    Priority = !string.IsNullOrWhiteSpace(txtPriority.Text) ? Convert.ToInt32(txtPriority.Text) : 0,
                    Description = txtDescription.InnerText,
                    STATUS = "ACTIVE",
                    CREATE_DATE = DateTime.Now,
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name,
                    Comparison = strcomparisonValue,
                    AttributeValueType = strAttributeValueType

                };


                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                dc = mappingsvc.AddStaticDataMappingAttributeValue(newObj);
                if (!(dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success))
                {
                    hdnFlag.Value = "false";
                    dvModalMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvModalMsg, dc.StatusMessage, BootstrapAlertType.Duplicate);
                }
                else //if (dc.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    hdnFlag.Value = "true";
                    //PageIndex = 0;
                    fillmappingattributes(0, Config_Id);
                    fillattributeFilters(Config_Id);
                    dvMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);
                }

            }
            else if (e.CommandName == "Save")
            {
                List<MDMSVC.DC_SupplierImportAttributeValues> lstNewObj = new List<MDMSVC.DC_SupplierImportAttributeValues>();

                string strAttributeValue = string.Empty;
                string strAttributeName = string.Empty;
                Guid AttributeValue_id = Guid.Empty;

                string strAttributeValueType = string.Empty;
                string strcomparisonValue = string.Empty;

                bool blnIsGuid = Guid.TryParse(ddlAttributeName.SelectedValue, out AttributeValue_id);
                if (blnIsGuid && ddlAttributeName.Visible)
                {
                    AttributeValue_id = Guid.Parse(ddlAttributeName.SelectedValue);
                }
                else
                    AttributeValue_id = Guid.Empty;

                if (ddlAttributeName.Visible)
                {
                    if (hdnddlAttributeTableName != null && !string.IsNullOrEmpty(hdnddlAttributeTableName.Value))
                    {
                        if (!string.IsNullOrEmpty(hdnddlAttributeTableName.Value) && hdnddlAttributeTableName.Value != "0")
                            strAttributeName = hdnddlAttributeTableName.Value + "." + ddlAttributeName.SelectedItem.ToString();
                        else
                            strAttributeName = ddlAttributeName.SelectedItem.ToString();
                    }
                    else
                        strAttributeValue = ddlAttributeName.SelectedItem.ToString();
                }
                else
                    strAttributeValue = txtAttributeName.Text;


                if (ddlAttributeValue.Visible)
                {
                    if (hdnddlAttributeTableValueName != null && !string.IsNullOrEmpty(hdnddlAttributeTableValueName.Value))
                        strAttributeValue = hdnddlAttributeTableValueName.Value + "." + ddlAttributeValue.SelectedItem.Text;
                    else
                        strAttributeName = ddlAttributeValue.SelectedItem.Text;
                }
                else if (txtAttributeValue.Visible)
                    strAttributeName = txtAttributeValue.Text;
                else if (!string.IsNullOrWhiteSpace(hdnIsReplaceWith.Value) && Convert.ToBoolean(hdnIsReplaceWith.Value))
                {
                    string strReplace = "\"" + txtReplaceFrom.Text + "\",\"" + txtReplaceTo.Text + "\"";
                    strAttributeName = strReplace;
                }
                else
                    strAttributeName = hdnValueWithCommaSeprated.Value != string.Empty ? Convert.ToString(hdnValueWithCommaSeprated.Value).TrimEnd(',') : string.Empty;


                //For filter 
                string strAttributeType = Convert.ToString(ddlAttributeType.SelectedItem.Text).ToLower();
                if (strAttributeType == "filter" || strAttributeType == "distinct")
                {
                    string strtemp = strAttributeName;
                    strAttributeName = strAttributeValue;
                    strAttributeValue = strtemp;
                }

                // y match by
                if (dvMatchByColumnOrValue.Visible)
                {
                    if (rdoIsMatchByValue.Checked)
                    {
                        strAttributeValueType = rdoIsMatchByValue.Text.ToUpper();
                        if (ddlComparisonValue.SelectedValue != "0")
                            strcomparisonValue = ddlComparisonValue.SelectedValue;
                        strAttributeValue = txtAttributeValue.Text;
                    }
                    else
                    {
                        strAttributeValueType = rdoIsMatchByColumn.Text.ToUpper();
                        strcomparisonValue = "=";
                    }
                    if (hdnddlAttributeTableName != null && !string.IsNullOrEmpty(hdnddlAttributeTableName.Value))
                    {
                        if (!string.IsNullOrEmpty(hdnddlAttributeTableName.Value) && hdnddlAttributeTableName.Value != "0")
                            strAttributeName = hdnddlAttributeTableName.Value + "." + ddlAttributeName.SelectedItem.ToString();
                        else
                            strAttributeName = ddlAttributeName.SelectedItem.ToString();
                    }
                }
                //end

                MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                {
                    SupplierImportAttributeValue_Id = Guid.Parse(lblSelectedSupplierImportAttributeValue_Id.Text),
                    SupplierImportAttribute_Id = Config_Id,
                    AttributeType = ddlAttributeType.SelectedItem.Text,
                    AttributeName = strAttributeName,
                    AttributeValue = strAttributeValue,
                    AttributeValue_ID = AttributeValue_id,
                    STATUS = ddlAddStatus.SelectedItem.Text,
                    Priority = Convert.ToInt32(txtPriority.Text),
                    Description = txtDescription.InnerText,
                    EDIT_DATE = DateTime.Now,
                    EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name,
                    AttributeValueType = strAttributeValueType,
                    Comparison = strcomparisonValue
                };

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
                    fillmappingattributes(0, Config_Id);
                    fillattributeFilters(Config_Id);
                    dvMsg.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, dc.StatusMessage, BootstrapAlertType.Success);

                }
            }
            else if (e.CommandName == "ResetAdd")
            {
                dvModalMsg.Visible = false;
                ddlAttributeType.SelectedIndex = 0;
                ddlAttributeName.Items.Clear();
                ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                txtAttributeName.Text = "";
                ddlAttributeValue.Items.Clear();
                ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", ""));
                ddlAttributeValue.Visible = true;
                txtAttributeValue.Text = "";
                txtAttributeValue.Visible = false;
                txtPriority.Text = "";
                txtPriority.Visible = false;
                txtDescription.InnerText = "";
                dvValueForFilter.Visible = false;
                divReplaceValue.Visible = false;
                //dvtxtAttributeValue.Visible = true;
                //dvddlAttributeValue.Visible = false;
                hdnFlag.Value = "false";
            }
            else if (e.CommandName == "ResetUpdate")
            {
                dvModalMsg.Visible = false;
                ddlAddStatus.SelectedIndex = 0;
                ddlAttributeType.SelectedIndex = 0;
                ddlAttributeName.Items.Clear();
                ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                ddlAttributeValue.Items.Clear();
                ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", ""));
                txtAttributeValue.Text = "";
                txtPriority.Text = "";
                txtDescription.InnerText = "";
                //dvtxtAttributeValue.Visible = true;
                //dvddlAttributeValue.Visible = false;
                hdnFlag.Value = "false";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            dvModalMsg.Visible = false;
            dvMsg.Visible = false;
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
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtPriority = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtPriority");
            System.Web.UI.HtmlControls.HtmlGenericControl dvMatchByColumnOrValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvMatchByColumnOrValue");
            fillattributes("MappingConfigAttributeTypes", "AttributeType", ddlAttributeType);

            DropDownList ddlComparisonValue = (DropDownList)frmAddConfig.FindControl("ddlComparisonValue");
            fillComparisonOperators("MappingConfigAttributeTypes", "ComparisonOperators", ddlComparisonValue);
            //dvddlAttributeValue.Visible = false;
            //dvtxtAttributeValue.Visible = true;

            //dvMatchByColumnOrValue.Style.Add(HtmlTextWriterStyle.Display, "none");
            dvMatchByColumnOrValue.Visible = false;

            if (IsMapping("bool"))
                dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "none");
            else
                dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "block");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showManageModal();", true);
        }

        protected void ddlAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region All Controls 
            int intNoOfColumn = intNumberOfColumnHasBeenAdded();

            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
            HiddenField hdnddlAttributeTableName = (HiddenField)frmAddConfig.FindControl("hdnddlAttributeTableName");
            HiddenField hdnddlAttributeTableValueName = (HiddenField)frmAddConfig.FindControl("hdnddlAttributeTableValueName");
            HiddenField hdnIsReplaceWith = (HiddenField)frmAddConfig.FindControl("hdnIsReplaceWith");
            RequiredFieldValidator rqfvddlAttributeValuedll = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValuedll");
            RequiredFieldValidator rqfvddlAttributeValue = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValue");
            RequiredFieldValidator rqfvddlAttributeValueFrom = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueFrom");
            RequiredFieldValidator rqfvddlAttributeValueTo = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueTo");
            RequiredFieldValidator rqfvddlAttributeValueFilter = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueFilter");
            RequiredFieldValidator vddlAttributeName = (RequiredFieldValidator)frmAddConfig.FindControl("vddlAttributeName");

            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvValueForFilter = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");
            System.Web.UI.HtmlControls.HtmlGenericControl dvAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl divReplaceValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("divReplaceValue");
            //matchBy
            System.Web.UI.HtmlControls.HtmlGenericControl dvMatchByColumnOrValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvMatchByColumnOrValue");
            DropDownList ddlComparisonValue = (DropDownList)frmAddConfig.FindControl("ddlComparisonValue");
            RadioButton rdoIsMatchByColumn = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByColumn");
            RadioButton rdoIsMatchByValue = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByValue");
            #endregion //End All Controls

            ddlComparisonValue.Visible = false;
            //dvMatchByColumnOrValue.Style.Add(HtmlTextWriterStyle.Display, "none");
            dvMatchByColumnOrValue.Visible = false;
            #region Set Attribute Name 
            if (ddlAttributeType.SelectedItem.Value != "0")
            {
                txtAttributeName.Visible = ddlAttributeName.Visible = txtAttributeValue.Visible = ddlAttributeValue.Visible = false;
                dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "none");
                string strAttributeType = ddlAttributeType.SelectedItem.Text.ToLower();

                if (strAttributeType == "filter" || strAttributeType == "distinct")
                {
                    ddlAttributeName.Visible = true;
                    if (intNoOfColumn > 0)
                    {
                        ddlAttributeName.Items.Clear();
                        ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                        for (int i = --intNoOfColumn; i >= 0; i--)
                            ddlAttributeName.Items.Insert(1, new ListItem(Convert.ToString(i), Convert.ToString(i)));
                    }
                    if (strAttributeType == "distinct")
                        txtAttributeValue.Visible = true;
                    else
                    {
                        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");

                        EnableDisableValidation(rqfvddlAttributeValueFilter);
                    }
                }
                else if (IsMapping("bool") && strAttributeType == "map")
                {
                    ddlAttributeName.Visible = true;
                    if (intNoOfColumn > 0)
                    {
                        ddlAttributeName.Items.Clear();
                        ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                        for (int i = --intNoOfColumn; i >= 0; i--)
                            ddlAttributeName.Items.Insert(1, new ListItem(Convert.ToString(i), Convert.ToString(i)));
                    }
                    hdnddlAttributeTableName.Value = "0";
                    //set Attribute Value STG Table 

                    //hdnddlAttributeTableValueName.Value = _stgTable;
                    //var lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_stgTable);
                    hdnddlAttributeTableValueName.Value = IsMapping("_stgTable");
                    var lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(hdnddlAttributeTableValueName.Value);

                    if (lstAttributeValue != null)
                    {
                        ddlAttributeValue.Visible = true;
                        ddlAttributeValue.Items.Clear();
                        ddlAttributeValue.DataSource = lstAttributeValue;
                        ddlAttributeValue.DataBind();
                        ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));
                        rqfvddlAttributeValuedll.Enabled = true;
                        rqfvddlAttributeValue.Enabled = false;
                        rqfvddlAttributeValueFrom.Enabled = false;
                        rqfvddlAttributeValueTo.Enabled = false;
                        rqfvddlAttributeValueFilter.Enabled = false;
                    }
                }
                else if (IsMapping("bool") && strAttributeType == "keyword")
                {
                    ddlAttributeName.Visible = true;
                    var lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_mappingTable);
                    hdnddlAttributeTableValueName.Value = _mappingTable;
                    hdnddlAttributeTableName.Value = _mappingTable;
                    if (lstAttributeValue != null)
                    {
                        ddlAttributeName.Visible = true;
                        ddlAttributeName.Items.Clear();
                        ddlAttributeName.DataSource = lstAttributeValue;
                        ddlAttributeName.DataBind();
                        ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                        ddlAttributeValue.Visible = true;
                        ddlAttributeValue.Items.Clear();
                        ddlAttributeValue.DataSource = lstAttributeValue;
                        ddlAttributeValue.DataBind();
                        ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));

                        EnableDisableValidation(rqfvddlAttributeValuedll);
                    }

                }
                else if (strAttributeType == "encode")
                {
                    EnableDisableValidation(vddlAttributeName);
                }
                else if (IsMapping("bool") && (strAttributeType == "jpath" || strAttributeType == "xpath"))
                {
                    ddlAttributeName.Visible = true;
                    if (intNoOfColumn > 0)
                    {
                        ddlAttributeName.Items.Clear();

                        for (int i = --intNoOfColumn; i >= 0; i--)
                            ddlAttributeName.Items.Insert(0, new ListItem(Convert.ToString(i), Convert.ToString(i)));

                        ddlAttributeName.Items.Insert(0, new ListItem("SchemaLocation", "SchemaLocation"));

                    }
                    txtAttributeValue.Visible = true;

                    EnableDisableValidation(rqfvddlAttributeValue);

                }
                else if (!IsMapping("bool")) //Match & Map
                {
                    List<string> lstAttributeName = null;
                    List<string> lstAttributeValue = null;
                    if (IsMapping("bool") && strAttributeType == "map") // ddlAttributeName -- Name No of columns , ddlAttributeValue -- Stg table Columns
                    {
                        if (intNoOfColumn > 0)
                        {
                            ddlAttributeName.Items.Clear();
                            for (int i = --intNoOfColumn; i >= 0; i--)
                                lstAttributeName.Add(i.ToString());

                        }
                        hdnddlAttributeTableName.Value = string.Empty;
                        //lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_stgTable);
                        //hdnddlAttributeTableValueName.Value = _stgTable;
                        hdnddlAttributeTableValueName.Value = IsMapping("_stgTable");
                        lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(hdnddlAttributeTableValueName.Value);


                    }
                    else if (!IsMapping("bool") && strAttributeType == "map") // ddlAttributeName -- mapping table Columns , ddlAttributeValue -- Stg table Columns
                    {
                        lstAttributeName = mastersvc.GetListOfColumnNamesByTable(_mappingTable);
                        hdnddlAttributeTableName.Value = _mappingTable;
                        //hdnddlAttributeTableValueName.Value = _stgTable;
                        //lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_stgTable);

                        hdnddlAttributeTableValueName.Value = IsMapping("_stgTable");
                        lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(hdnddlAttributeTableValueName.Value);
                    }
                    else if (!IsMapping("bool") && strAttributeType == "match") // ddlAttributeName -- mapping table Columns , ddlAttributeValue -- master table Columns
                    {
                        // dvMatchByColumnOrValue.Style.Add(HtmlTextWriterStyle.Display, "block");
                        dvMatchByColumnOrValue.Visible = true;
                        rdoIsMatchByValue.Checked = false;
                        rdoIsMatchByColumn.Checked = true;
                        lstAttributeName = mastersvc.GetListOfColumnNamesByTable(_mappingTable);
                        hdnddlAttributeTableName.Value = _mappingTable;
                        lstAttributeValue = mastersvc.GetListOfColumnNamesByTable(_masterTable);
                        hdnddlAttributeTableValueName.Value = _masterTable;
                    }
                    if (lstAttributeName != null)
                    {
                        ddlAttributeName.Visible = true;
                        ddlAttributeName.Items.Clear();
                        ddlAttributeName.DataSource = lstAttributeName;
                        ddlAttributeName.DataBind();
                        ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                    }
                    //Setting Attribute value dropdown also
                    if (lstAttributeValue != null)
                    {
                        ddlAttributeValue.Visible = true;
                        ddlAttributeValue.Items.Clear();
                        ddlAttributeValue.DataSource = lstAttributeValue;
                        ddlAttributeValue.DataBind();
                        ddlAttributeValue.Items.Insert(0, new ListItem("---ALL---", "0"));

                        EnableDisableValidation(rqfvddlAttributeValuedll);
                    }
                }
                else
                {
                    MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
                    RQ.MasterFor = "MappingConfigAttributeTypes";
                    RQ.Name = "AttributeValues";
                    RQ.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                    var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
                    if (resvalues != null && resvalues.Count > 0)
                    {
                        ddlAttributeName.Visible = true;
                        // Delimiter option only available when format has been added and Selected as csc / txt
                        if (IsFileDetailsFileFormatCsvTxt)
                            resvalues = (from x in resvalues select x).ToList();
                        else
                        {
                            resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "delimiter" select x).ToList();
                            resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "textqualifier" select x).ToList();
                        }
                        
                        ddlAttributeName.Items.Clear();
                        ddlAttributeName.DataSource = resvalues;
                        ddlAttributeName.DataTextField = "AttributeValue";
                        ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                        ddlAttributeName.DataBind();
                        ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "-1"));
                        txtAttributeValue.Visible = true;
                    }
                    else
                    {
                        txtAttributeName.Visible = true;
                    }
                }
            }
            else
            {
                ddlAttributeName.Items.Clear();
            }
            #endregion


            #region Hide 'Value' Section
            string valAttributeType = ddlAttributeType.SelectedItem.Text.ToLower();
            if (valAttributeType == "decode" || valAttributeType == "encode" || valAttributeType == "distinct" || valAttributeType == "filter")
            {
                if (valAttributeType == "filter")
                {
                    if (divReplaceValue.Visible)
                        divReplaceValue.Visible = false;
                }
                else
                {
                    if (dvAttributeValue.Visible)
                        dvAttributeValue.Visible = false;
                }

            }
            else
            {
                if (!dvAttributeValue.Visible)
                {
                    dvAttributeValue.Visible = true;
                    dvAttributeValue.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
            }
            //to hide the From and To textboxes.
            if (!(ddlAttributeType.SelectedItem.Text.ToLower() == "format"))
            {
                hdnIsReplaceWith.Value = Convert.ToString(false);
                divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            #endregion
            
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
            FilteredTextBoxExtender axfte_txtAttributeName = (FilteredTextBoxExtender)frmAddConfig.FindControl("axfte_txtAttributeName");
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
                    ddlAttributeName.Items.Insert(0, new ListItem("---Select---", "-1"));
                }
                else
                {
                    HideShowAttributeNameControls(false);
                    if (ddlAttributeValue.SelectedItem.Text.ToLower() == "numberofcolumns")
                        axfte_txtAttributeName.Enabled = true;
                    else
                        axfte_txtAttributeName.Enabled = false;
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
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
            fillmappingattributes(0, Config_Id);
            dvMsg.Visible = false;
            setNoOfColumnsForMapType(0, Config_Id);
        }

        protected void ddlAttributeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            TextBox txtReplaceFrom = (TextBox)frmAddConfig.FindControl("txtReplaceFrom");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            RequiredFieldValidator rqfvddlAttributeValuedll = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValuedll");
            RequiredFieldValidator rqfvddlAttributeValue = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValue");
            RequiredFieldValidator rqfvddlAttributeValueFrom = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueFrom");
            RequiredFieldValidator rqfvddlAttributeValueTo = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueTo");
            RequiredFieldValidator rqfvddlAttributeValueFilter = (RequiredFieldValidator)frmAddConfig.FindControl("rqfvddlAttributeValueFilter");
            FilteredTextBoxExtender axfte_txtAttributeValue = (FilteredTextBoxExtender)frmAddConfig.FindControl("axfte_txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvValueForFilter = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");
            System.Web.UI.HtmlControls.HtmlGenericControl divReplaceValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("divReplaceValue");
            HiddenField hdnIsReplaceWith = (HiddenField)frmAddConfig.FindControl("hdnIsReplaceWith");

            rqfvddlAttributeValuedll.Enabled = false;
            rqfvddlAttributeValue.Enabled = false;
            rqfvddlAttributeValueFrom.Enabled = false;
            rqfvddlAttributeValueTo.Enabled = false;
            rqfvddlAttributeValueFilter.Enabled = false;

            //Resetting Default Length to reset Effects during NoOfColumns..
            txtAttributeValue.MaxLength = 8000; txtAttributeValue.Text = "";

            if (ddlAttributeType.SelectedItem.Text.ToLower() != "map" && ddlAttributeType.SelectedItem.Text.ToLower() != "match" && ddlAttributeType.SelectedItem.Text.ToLower() != "keyword")
            {
                Guid ParentAttributeValue_Id = Guid.Empty;
                ddlAttributeValue.Visible = txtAttributeValue.Visible = false;
                divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnIsReplaceWith.Value = Convert.ToString(false);
                bool blnIsGuid = Guid.TryParse(ddlAttributeName.SelectedItem.Value, out ParentAttributeValue_Id);
                if (blnIsGuid && ddlAttributeName.SelectedItem.Value != "0")
                {
                    dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "none");
                    var resvalues = mastersvc.GetAllAttributeAndValuesByParentAttributeValue(new MDMSVC.DC_MasterAttribute() { ParentAttributeValue_Id = ParentAttributeValue_Id });
                    if (resvalues != null && resvalues.Count > 0)
                    {
                        ddlAttributeValue.Visible = true;
                        ddlAttributeValue.Items.Clear();
                        ddlAttributeValue.DataSource = resvalues;
                        ddlAttributeValue.DataTextField = "AttributeValue";
                        ddlAttributeValue.DataValueField = "MasterAttributeValue_Id";
                        ddlAttributeValue.DataBind();
                        ddlAttributeValue.Items.Insert(0, new ListItem("---Select---", "0"));

                        //Since textbox for value is not visible the validatin control should be disable

                        EnableDisableValidation(rqfvddlAttributeValuedll);
                    }
                    else
                    {
                        // HideShowAttributeNameControls(false);
                        if (ddlAttributeName.SelectedItem.Text.ToLower() == "numberofcolumns")
                        {
                            ddlAttributeValue.Visible = false;
                            txtAttributeValue.Visible = true;
                            txtAttributeValue.MaxLength = 4;
                            axfte_txtAttributeValue.Enabled = true;

                            EnableDisableValidation(rqfvddlAttributeValue);
                        }

                        else if (ddlAttributeType.SelectedItem.Text.ToLower() == "format" && ddlAttributeName.SelectedItem.Text.ToLower() == "replace")
                        {
                            hdnIsReplaceWith.Value = Convert.ToString(true);
                            ddlAttributeValue.Visible = txtAttributeValue.Visible = false;
                            divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "block");

                            //rqfvddlAttributeValue.Enabled = false;

                            EnableDisableValidation(rqfvddlAttributeValueFrom);
                            EnableDisableValidation(rqfvddlAttributeValueTo);
                        }
                        else if (ddlAttributeType.SelectedItem.Text.ToLower() == "format" && ddlAttributeName.SelectedItem.Text.ToLower() == "replace all spcl chrs")
                        {
                            hdnIsReplaceWith.Value = Convert.ToString(false);
                            ddlAttributeValue.Visible = false;
                            txtAttributeValue.Visible = true;
                            divReplaceValue.Style.Add(HtmlTextWriterStyle.Display, "none");

                            //rqfvddlAttributeValueFrom.Enabled = false;
                            //rqfvddlAttributeValueTo.Enabled = false;
                            EnableDisableValidation(rqfvddlAttributeValue);
                        }
                        else
                            if (!(frmAddConfig.CurrentMode.ToString() == "Edit"))
                        {
                            axfte_txtAttributeValue.Enabled = false;
                            rqfvddlAttributeValue.Enabled = false;
                            rqfvddlAttributeValueFilter.Enabled = false;
                            //rqfvddlAttributeValueFrom.Enabled = true;
                        }
                    }

                }
                else
                {
                    if (ddlAttributeType.SelectedItem.Text.ToLower() == "xpath" || ddlAttributeType.SelectedItem.Text.ToLower() == "jpath")
                    {
                        txtAttributeValue.Visible = true;
                    }
                    else if (ddlAttributeType.SelectedItem.Text.ToLower() == "distinct")
                    {
                        txtAttributeValue.Visible = true;
                    }
                    else if (ddlAttributeName.SelectedIndex == 0)
                    {
                        txtAttributeValue.Visible = true;
                    }
                    else
                    {
                        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");

                        EnableDisableValidation(rqfvddlAttributeValueFilter);
                    }
                }
            }
        }

        protected void ddlFilterPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
            fillmappingattributes(0, Config_Id);
            dvMsg.Visible = false;
            setNoOfColumnsForMapType(0, Config_Id);
        }

        protected void ddlFilterAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid Config_Id = new Guid(Request.QueryString["Config_Id"]);
            fillmappingattributes(0, Config_Id);
            dvMsg.Visible = false;
            setNoOfColumnsForMapType(0, Config_Id);
        }

        protected void MatchBy_CheckedChanged()
        {
            RadioButton rdoIsMatchByValue = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByValue");
            RadioButton rdoIsMatchByColumn = (RadioButton)frmAddConfig.FindControl("rdoIsMatchByColumn");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            DropDownList ddlComparisonValue = (DropDownList)frmAddConfig.FindControl("ddlComparisonValue");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            if (rdoIsMatchByValue.Checked == true)
            {
                ddlAttributeValue.Visible = false;
                txtAttributeValue.Visible = true;
                ddlComparisonValue.Visible = true;
            }
            else
            {
                ddlAttributeValue.Visible = true;
                txtAttributeValue.Visible = false;
                ddlComparisonValue.Visible = false;
            }
        }

        protected void rdoIsMatchByColumn_CheckedChanged(object sender, EventArgs e)
        {
            MatchBy_CheckedChanged();
        }

        protected void rdoIsMatchByValue_CheckedChanged(object sender, EventArgs e)
        {
            MatchBy_CheckedChanged();
        }
    }
}