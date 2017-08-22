using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Text;

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
        public static bool IsFileDetailsFileFormatCsvTxt = false;
        public static int intNumberOfColumnHasBeenAdded = 0;


        public static string _stgTable = string.Empty;
        public static string _masterTable = string.Empty;
        public static string _mappingTable = string.Empty;

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

                        //Set Valur For File Type
                        var resAttributeFileformate = (from x in res where x.AttributeValue.ToLower() == "format" select x).ToList();
                        if (resAttributeFileformate != null && resAttributeFileformate.Count > 0)
                        {
                            var IsCsvORTxt = (from x in resAttributeFileformate where x.AttributeName.ToLower() == "csv" || x.AttributeName.ToLower() == "text" select x).FirstOrDefault();
                            if (IsCsvORTxt != null)
                                IsFileDetailsFileFormatCsvTxt = true;
                            else
                                IsFileDetailsFileFormatCsvTxt = false;
                        }
                        else
                            IsFileDetailsFileFormatCsvTxt = false;
                        //Set Value for IsNumberOfColumnHasBeenAdded
                        var resNumberOfColumns = (from x in res where x.AttributeValue.ToLower() == "numberofcolumns" select x).ToList();
                        if (resNumberOfColumns != null && resNumberOfColumns.Count > 0)
                            intNumberOfColumnHasBeenAdded = Convert.ToInt32(resNumberOfColumns[0].AttributeName);
                        else
                            intNumberOfColumnHasBeenAdded = 0;





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

                        //Set mappind,master and staging table name
                        //_mappingTable = Convert.ToString(res[0].Mapping_Table);
                        //_masterTable = Convert.ToString(res[0].Master_Table);
                        //_stgTable = Convert.ToString(res[0].STG_Table);

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
                    //If no file config is added, show only "File Details" opens in File drop down
                    if (configresultCount == 0 && IsMapping)
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() == "filedetails" select x).ToList();

                    //Distinct & Filter option only available when number of column has been added
                    if (configresultCount > 0 && intNumberOfColumnHasBeenAdded == 0)
                    {
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "filter" select x).ToList();
                        resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "distinct" select x).ToList();
                    }


                    ////If for == matching then show only two options into type dropdown match and map 
                    //if (!IsMapping)
                    //    resvalues = (from x in resvalues where x.AttributeValue.ToLower() == "mapping" || x.AttributeValue.ToLower() == "matching" select x).ToList();
                    //else
                    //    resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "match" select x).ToList();
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
                    bool isFound = false;
                    if (res != null && res.Count > 0)
                    {

                        #region Get All Controls
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
                        FilteredTextBoxExtender axfte_txtAttributeName = (FilteredTextBoxExtender)frmAddConfig.FindControl("axfte_txtAttributeName");
                        HtmlGenericControl dvtxtPriority = (HtmlGenericControl)frmAddConfig.FindControl("dvtxtPriority");
                        HtmlGenericControl dvValueForFilter = (HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");

                        #endregion //Get All Controls

                        #region Priority 
                        if (IsMapping)
                            dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "none");
                        else
                            dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "block");
                        txtPriority.Text = res[0].Priority.ToString();


                        #endregion //END Priority
                        //Fill & Set attribute Type
                        #region Attribute Type
                        fillattributes(AttributeOptionForType, "AttributeType", ddlAttributeType);
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
                                int intNoOfColumn = intNumberOfColumnHasBeenAdded;
                                ddlAttributeName.Visible = true;
                                if (intNumberOfColumnHasBeenAdded > 0)
                                {
                                    ddlAttributeName.Items.Clear();
                                    for (int i = --intNoOfColumn; i >= 0; i--)
                                        ddlAttributeName.Items.Insert(0, new ListItem(Convert.ToString(i), Convert.ToString(i)));

                                    if (ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()) != null)
                                        ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()).Selected = true;
                                }

                            }
                            #endregion
                            else
                            {
                                MDMSVC.DC_MasterAttribute RQAttr = new MDMSVC.DC_MasterAttribute();
                                RQAttr.MasterFor = AttributeOptionForType;
                                RQAttr.Name = "AttributeValues";
                                RQAttr.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                                var resvalues = mastersvc.GetAllAttributeAndValues(RQAttr);
                                if (resvalues != null && resvalues.Count > 0)
                                {
                                    isFound = true;
                                    ddlAttributeName.Items.Clear();
                                    ddlAttributeName.DataSource = resvalues;
                                    ddlAttributeName.DataTextField = "AttributeValue";
                                    ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                                    ddlAttributeName.DataBind();
                                    ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "0"));
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
                        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "none");
                        if (!string.IsNullOrWhiteSpace(strAttributeValue))
                        {
                            #region For Filter
                            if (res[0].AttributeType.ToLower() == "filter")
                            {
                                dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");
                                StringBuilder sbinnerhtml = new StringBuilder();
                                string[] strArrayAttributeValue = strAttributeValue.Split(',');
                                sbinnerhtml.Append(dvValueForFilter.InnerHtml);
                                for (int i = 0; i < strArrayAttributeValue.Count(); i++)
                                {
                                    sbinnerhtml.Append("<div class='con inner-addon right-addon'><i id='btnAddValue' style='cursor: pointer' class='btnRemove glyphicon glyphicon-minus'></i>");
                                    string strID = "txt" + i.ToString();
                                    sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control' value=" + strArrayAttributeValue[i] + " /></div>");
                                }
                                dvValueForFilter.InnerHtml = sbinnerhtml.ToString();
                            }
                            #endregion //End  For Filter
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



                        //Take data And Bind Dropdown and set it
                        //if (!string.IsNullOrWhiteSpace(res[0].AttributeValue))
                        //{
                        //    //string strAttributeValue = Convert.ToString(res[0].AttributeValue);
                        //    if (!string.IsNullOrWhiteSpace(strAttributeValue) && res[0].AttributeType.ToLower() == "filter")
                        //    {
                        //        ddlAttributeName.Visible = false;
                        //        //Fill Attribute name and value to 
                        //        StringBuilder sbinnerhtml = new StringBuilder();
                        //        string[] strArrayAttributeValue = strAttributeValue.Split(',');
                        //        sbinnerhtml.Append(dvValueForFilter.InnerHtml);
                        //        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");
                        //        for (int i = 0; i < strArrayAttributeValue.Count(); i++)
                        //        {
                        //            sbinnerhtml.Append("<div class='con inner-addon right-addon'><i id='btnAddValue' style='cursor: pointer' class='btnRemove glyphicon glyphicon-minus'></i>");
                        //            string strID = "txt" + i.ToString();
                        //            sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control' value=" + strArrayAttributeValue[i] + " /></div>");
                        //        }
                        //        dvValueForFilter.InnerHtml = sbinnerhtml.ToString();
                        //        ddlAttributeValue.Visible = false;

                        //    }
                        //    else
                        //    {


                        //        var resultForAttribute = mastersvc.GetAllAttributeAndValuesByParentAttributeValue(new MDMSVC.DC_MasterAttribute() { ParentAttributeValue_Id = res[0].AttributeValue_ID });
                        //        if (resultForAttribute != null && resultForAttribute.Count > 0)
                        //        {
                        //            HideShowAttributeNameControls(true);

                        //        }
                        //        else
                        //        {
                        //            txtAttributeName.Visible = false;
                        //            if (!string.IsNullOrWhiteSpace(strAttributeValue) && res[0].AttributeType.ToLower() == "filter")
                        //            {
                        //                ddlAttributeValue.Visible = true;
                        //            }
                        //            else
                        //                ddlAttributeValue.Visible = false;


                        //            txtAttributeName.Text = Convert.ToString(res[0].AttributeName);

                        //            ddlAttributeValue.Visible = false;
                        //            txtAttributeValue.Visible = true;
                        //            txtAttributeValue.Text = res[0].AttributeName.ToString();
                        //        }
                        //    }

                        //}




                        //if (ddlAttributeType.SelectedItem.Value != "0")
                        //{
                        //    MDMSVC.DC_MasterAttribute RQAttr = new MDMSVC.DC_MasterAttribute();
                        //    RQAttr.MasterFor = AttributeOptionForType;
                        //    RQAttr.Name = "AttributeValues";
                        //    if (ddlAttributeType.SelectedItem.Text.ToLower() == "distinct" || ddlAttributeType.SelectedItem.Text.ToLower() == "filter")
                        //    {
                        //        txtAttributeName.Visible = false;
                        //        int intNoOfColumn = intNumberOfColumnHasBeenAdded;
                        //        ddlAttributeName.Visible = true;
                        //        if (intNumberOfColumnHasBeenAdded > 0)
                        //        {
                        //            ddlAttributeName.Items.Clear();

                        //            for (int i = --intNoOfColumn; i >= 0; i--)
                        //            {
                        //                string strItemTextAndValue = Convert.ToString(i);
                        //                ddlAttributeName.Items.Insert(0, new ListItem(strItemTextAndValue, strItemTextAndValue));
                        //            }
                        //            if (ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()) != null)
                        //                ddlAttributeName.Items.FindByText(res[0].AttributeName.ToString()).Selected = true;
                        //        }

                        //    }
                        //    else
                        //    {
                        //        RQAttr.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                        //        var resvalues = mastersvc.GetAllAttributeAndValues(RQAttr);
                        //        if (resvalues != null)
                        //        {
                        //            if (resvalues.Count > 0)
                        //            {
                        //                isFound = true;
                        //                ddlAttributeName.Items.Clear();
                        //                ddlAttributeName.DataSource = resvalues;
                        //                ddlAttributeName.DataTextField = "AttributeValue";
                        //                ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                        //                ddlAttributeName.DataBind();
                        //                ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "0"));

                        //                ddlAttributeName.SelectedIndex = ddlAttributeName.Items.IndexOf(ddlAttributeName.Items.FindByText(res[0].AttributeValue.ToString()));
                        //            }
                        //        }
                        //    }

                        //}

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
            System.Web.UI.HtmlControls.HtmlGenericControl dvValueForFilter = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");
            HiddenField hdnValueWithCommaSeprated = (HiddenField)frmAddConfig.FindControl("hdnValueWithCommaSeprated");


            if (e.CommandName == "Add")
            {
                string strAttributeValue = string.Empty;
                string strAttributeName = string.Empty;
                Guid AttributeValue_id = Guid.Empty;
                bool blnIsGuid = Guid.TryParse(ddlAttributeName.SelectedValue, out AttributeValue_id);
                if (blnIsGuid && ddlAttributeName.Visible)
                {
                    AttributeValue_id = Guid.Parse(ddlAttributeName.SelectedValue);
                }
                else
                    AttributeValue_id = Guid.Empty;

                if (ddlAttributeName.Visible)
                    strAttributeValue = ddlAttributeName.SelectedItem.ToString();
                else
                    strAttributeValue = txtAttributeName.Text;


                if (ddlAttributeValue.Visible)
                    strAttributeName = ddlAttributeValue.SelectedItem.Text;
                else if (txtAttributeValue.Visible)
                    strAttributeName = txtAttributeValue.Text;
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
                    CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name
                };




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

                string strAttributeValue = string.Empty;
                string strAttributeName = string.Empty;
                Guid AttributeValue_id = Guid.Empty;
                bool blnIsGuid = Guid.TryParse(ddlAttributeName.SelectedValue, out AttributeValue_id);
                if (blnIsGuid && ddlAttributeName.Visible)
                {
                    AttributeValue_id = Guid.Parse(ddlAttributeName.SelectedValue);
                }
                else
                    AttributeValue_id = Guid.Empty;

                if (ddlAttributeName.Visible)
                    strAttributeValue = ddlAttributeName.SelectedItem.ToString();
                else
                    strAttributeValue = txtAttributeName.Text;


                if (ddlAttributeValue.Visible)
                    strAttributeName = ddlAttributeValue.SelectedItem.Text;
                else if (txtAttributeValue.Visible)
                    strAttributeName = txtAttributeValue.Text;
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

                MDMSVC.DC_SupplierImportAttributeValues newObj = new MDMSVC.DC_SupplierImportAttributeValues
                {
                    SupplierImportAttributeValue_Id = SelectedSupplierImportAttributeValue_Id,
                    SupplierImportAttribute_Id = Config_Id,
                    AttributeType = ddlAttributeType.SelectedItem.Text,
                    AttributeName = strAttributeName,
                    AttributeValue = strAttributeValue,
                    AttributeValue_ID = AttributeValue_id,
                    STATUS = ddlAddStatus.SelectedItem.Text,
                    Priority = Convert.ToInt32(txtPriority.Text),
                    Description = txtDescription.InnerText,
                    EDIT_DATE = DateTime.Now,
                    EDIT_USER = System.Web.HttpContext.Current.User.Identity.Name
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
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtPriority = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtPriority");

            fillattributes(AttributeOptionForType, "AttributeType", ddlAttributeType);
            //dvddlAttributeValue.Visible = false;
            //dvtxtAttributeValue.Visible = true;
            if (IsMapping)
                dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "none");
            else
                dvtxtPriority.Style.Add(HtmlTextWriterStyle.Display, "block");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showManageModal();", true);
        }

        protected void ddlAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region All Controls 
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");

            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvValueForFilter = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");

            #endregion //End All Controls
            bool isFound = false;


            #region Set Attribute Name 
            if (ddlAttributeType.SelectedItem.Value != "0")
            {
                txtAttributeName.Visible = ddlAttributeName.Visible = txtAttributeValue.Visible = ddlAttributeValue.Visible = false;
                dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "none");
                string strAttributeType = ddlAttributeType.SelectedItem.Text.ToLower();

                if (strAttributeType == "filter" || strAttributeType == "distinct")
                {
                    ddlAttributeName.Visible = true;
                    if (intNumberOfColumnHasBeenAdded > 0)
                    {
                        int intNoOfColumn = intNumberOfColumnHasBeenAdded;
                        ddlAttributeName.Items.Clear();
                        for (int i = --intNoOfColumn; i >= 0; i--)
                            ddlAttributeName.Items.Insert(0, new ListItem(Convert.ToString(i), Convert.ToString(i)));

                    }
                    if (strAttributeType == "distinct")
                        txtAttributeValue.Visible = true;
                    else
                        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else if(IsMapping && strAttributeType == "map")
                {
                    ddlAttributeName.Visible = true;
                    if (intNumberOfColumnHasBeenAdded > 0)
                    {
                        int intNoOfColumn = intNumberOfColumnHasBeenAdded;
                        ddlAttributeName.Items.Clear();
                        for (int i = --intNoOfColumn; i >= 0; i--)
                            ddlAttributeName.Items.Insert(0, new ListItem(Convert.ToString(i), Convert.ToString(i)));

                    }
                    //set Attribute Value STG Table 
                    //var resultForValue = 


                }
                else if(!IsMapping && strAttributeType == "map")
                {

                }
                else if(!IsMapping && strAttributeType == "matching")
                {

                }
                else
                {
                    MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
                    RQ.MasterFor = AttributeOptionForType;
                    RQ.Name = "AttributeValues";
                    RQ.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
                    var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
                    if (resvalues != null &&  resvalues.Count > 0)
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
                        isFound = true;
                        ddlAttributeName.Items.Clear();
                        ddlAttributeName.DataSource = resvalues;
                        ddlAttributeName.DataTextField = "AttributeValue";
                        ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
                        ddlAttributeName.DataBind();
                        ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "0"));
                        txtAttributeValue.Visible = true;
                    }
                    else
                    {
                        txtAttributeName.Visible = true;
                    }
                }
            }
            #endregion



            //if (ddlAttributeType.SelectedItem.Value != "0")
            //{
            //    MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            //    RQ.MasterFor = AttributeOptionForType;
            //    RQ.Name = "AttributeValues";
            //    //If Distinct OR Filter
            //    if (ddlAttributeType.SelectedItem.Text.ToLower() == "distinct" || ddlAttributeType.SelectedItem.Text.ToLower() == "filter")
            //    {
            //        int intNoOfColumn = intNumberOfColumnHasBeenAdded;
            //        if (intNumberOfColumnHasBeenAdded > 0)
            //        {
            //            ddlAttributeName.Items.Clear();

            //            for (int i = --intNoOfColumn; i >= 0; i--)
            //            {
            //                string strItemTextAndValue = Convert.ToString(i);
            //                ddlAttributeName.Items.Insert(0, new ListItem(strItemTextAndValue, strItemTextAndValue));
            //            }
            //        }
            //        ddlAttributeValue.Visible = false;
            //        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");
            //    }
            //    else
            //    {
            //        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "none");
            //        RQ.ParentAttributeValue_Id = Guid.Parse(ddlAttributeType.SelectedItem.Value);
            //        var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
            //        if (resvalues != null)
            //        {
            //            if (resvalues.Count > 0)
            //            {
            //                // Delimiter option only available when format has been added and Selected as csc / txt
            //                if (IsFileDetailsFileFormatCsvTxt)
            //                    resvalues = (from x in resvalues select x).ToList();
            //                else
            //                {
            //                    resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "delimiter" select x).ToList();
            //                    resvalues = (from x in resvalues where x.AttributeValue.ToLower() != "textqualifier" select x).ToList();
            //                }
            //                isFound = true;
            //                ddlAttributeName.Items.Clear();
            //                ddlAttributeName.DataSource = resvalues;
            //                ddlAttributeName.DataTextField = "AttributeValue";
            //                ddlAttributeName.DataValueField = "MasterAttributeValue_Id";
            //                ddlAttributeName.DataBind();
            //                ddlAttributeName.Items.Insert(0, new ListItem("---ALL---", "0"));
            //                //dvtxtAttributeValue.Visible = false;
            //                //dvddlAttributeValue.Visible = true;
            //            }
            //        }
            //    }
            //    //dvtxtAttributeValue.Visible = false;
            //    //dvddlAttributeValue.Visible = true;
            //}

            //if (!isFound)
            //{
            //    dvtxtAttributeValue.Visible = true;
            //    if (ddlAttributeType.SelectedItem.Text.ToLower() == "filter")
            //    {
            //        txtAttributeValue.Visible = false;
            //        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");

            //    }
            //    else
            //    {
            //        txtAttributeValue.Visible = true;
            //        dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "none");
            //    }
            //    dvddlAttributeValue.Visible = false;
            //}
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
                    ddlAttributeName.Items.Insert(0, new ListItem("---Select---", "0"));
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

        protected void ddlAttributeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAttributeType = (DropDownList)frmAddConfig.FindControl("ddlAttributeType");
            DropDownList ddlAttributeValue = (DropDownList)frmAddConfig.FindControl("ddlAttributeValue");
            DropDownList ddlAttributeName = (DropDownList)frmAddConfig.FindControl("ddlAttributeName");
            TextBox txtAttributeName = (TextBox)frmAddConfig.FindControl("txtAttributeName");
            TextBox txtAttributeValue = (TextBox)frmAddConfig.FindControl("txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvtxtAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvtxtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvddlAttributeValue = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvddlAttributeValue");
            FilteredTextBoxExtender axfte_txtAttributeValue = (FilteredTextBoxExtender)frmAddConfig.FindControl("axfte_txtAttributeValue");
            System.Web.UI.HtmlControls.HtmlGenericControl dvValueForFilter = (System.Web.UI.HtmlControls.HtmlGenericControl)frmAddConfig.FindControl("dvValueForFilter");


            Guid ParentAttributeValue_Id = Guid.Empty;
            ddlAttributeValue.Visible = txtAttributeValue.Visible = false;
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
                }
                else
                {
                    // HideShowAttributeNameControls(false);
                    if (ddlAttributeName.SelectedItem.Text.ToLower() == "numberofcolumns")
                    {
                        ddlAttributeValue.Visible = false;
                        txtAttributeValue.Visible = true;
                        axfte_txtAttributeValue.Enabled = true;
                    }
                    else
                        axfte_txtAttributeValue.Enabled = false;
                }

            }
            else
            {
                dvValueForFilter.Style.Add(HtmlTextWriterStyle.Display, "block");
            }

        }

        protected void ddlAttributeValue_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}