﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Flavours : System.Web.UI.UserControl
    {
        public Guid Activity_Flavour_Id;
        // public Guid Activity_Id;
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MDMSVC.DC_Activity_Flavour_RQ RQParams = new MDMSVC.DC_Activity_Flavour_RQ();
        //List<string> listContents = new List<string>();

        //ArrayList arraylist1 = new ArrayList();
        //ArrayList arraylist2 = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getFlavourInfo();

            }
        }
        private void getFlavourInfo()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            DC_Activity_Flavour_RQ RQ = new DC_Activity_Flavour_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = AccSvc.GetActivityFlavour(RQ);
            frmActivityFlavour.DataSource = result;
            frmActivityFlavour.DataBind();
            if (result != null)
            {
                fillMasterDropdowns();
            }
        }
        private void BindDataSource()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            Label lblSuppCountry = (Label)frmActivityFlavour.FindControl("lblSuppCountry");
            Label lblSuppCity = (Label)frmActivityFlavour.FindControl("lblSuppCity");
            Label lblSuppProductType = (Label)frmActivityFlavour.FindControl("lblSuppProductType");
            Label lblSuppProdNameSubType = (Label)frmActivityFlavour.FindControl("lblSuppProdNameSubType");
            Label lblSuppSuitableFor = (Label)frmActivityFlavour.FindControl("lblSuppSuitableFor");
            Label lblSuppPhysicalIntensity = (Label)frmActivityFlavour.FindControl("lblSuppPhysicalIntensity");
            
            MDMSVC.DC_Activity_SupplierProductMapping_RQ _obj = new MDMSVC.DC_Activity_SupplierProductMapping_RQ();
            _obj.Activity_ID = Activity_Flavour_Id;
            var result = AccSvc.GetActivitySupplierProductMapping(_obj);
            if (result.Count > 0 || result != null)
            {
                foreach (DC_Activity_SupplierProductMapping res in result)
                {
                    lblSuppCountry.Text = res.SupplierCountryName.ToString();
                    lblSuppCity.Text = res.SupplierCityName.ToString();
                    lblSuppProductType.Text = res.SupplierProductType.ToString();
                    lblSuppProdNameSubType.Text = res.SupplierProductName.ToString();
                    lblSuppSuitableFor.Text = res.SupplierType.ToString();
                    lblSuppPhysicalIntensity.Text = res.SupplierType.ToString();
                }
            }

        }
        private void fillMasterDropdowns()
        {
            fillcoutries();
            fillcities();
            fillproductcaterogysubtype();
            fillProductType();
            fillproductsubtype();
            //fillDescriptions();
            fillSuitableFor();
            fillPhysicalIntensity();
            BindDataSource();
            fillDDLSession();
            fillDurationSession();
            fillDaysOfWeek();
        }
        private void fillDurationSession()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            Label lblSuppStartTime = (Label)frmActivityFlavour.FindControl("lblSuppStartTime");
            Label lblSuppDuration = (Label)frmActivityFlavour.FindControl("lblSuppDuration");
            Label lblSuppOperatingDays = (Label)frmActivityFlavour.FindControl("lblSuppOperatingDays");
            CheckBox chkSpecificOperatingDays = (CheckBox)frmActivityFlavour.FindControl("chkSpecificOperatingDays");
            //Control dvFrm = (Control)frmActivityFlavour.FindControl("dvFrm");
            HtmlGenericControl dvFrm = (HtmlGenericControl)frmActivityFlavour.FindControl("dvFrm");
            HtmlGenericControl dvTo = (HtmlGenericControl)frmActivityFlavour.FindControl("dvTo");
            TextBox txtFrom = (TextBox)frmActivityFlavour.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmActivityFlavour.FindControl("txtTo");

            MDMSVC.DC_Activity_DaysOfWeek_RQ obj = new DC_Activity_DaysOfWeek_RQ();
            obj.Activity_Flavor_ID = Activity_Flavour_Id;
            var result = AccSvc.GetActivityDaysOfWeek(obj);

            if (result.Count > 0 || result != null)
            {
                int currentval = 0;
                foreach (DC_Activity_DaysOfWeek_RS res in result)
                {
                    currentval = currentval + 1;
                    lblSuppStartTime.Text = res.SupplierStartTime.ToString();
                    lblSuppDuration.Text = res.SupplierDuration.ToString();
                    if (res.IsOperatingDays != null && res.IsOperatingDays == true)
                    {
                        lblSuppOperatingDays.Text = Convert.ToString(res.IsOperatingDays);
                        chkSpecificOperatingDays.Checked = true;
                    }
                    else
                    {
                        lblSuppOperatingDays.Text = Convert.ToString(res.IsOperatingDays);
                        chkSpecificOperatingDays.Checked = false;
                    }
                    if (res.FromDate != null)
                    {
                        string strFrmDate = Convert.ToString(res.FromDate);
                        StringBuilder sbinnerhtml = new StringBuilder();
                        string strID = "txtFrm_" + currentval.ToString();
                        sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control col-md-8' value=" + strFrmDate + " />");
                        sbinnerhtml.Append(dvFrm.InnerHtml);
                        dvFrm.InnerHtml = Convert.ToString(sbinnerhtml);
                    }
                    if (res.ToDate != null)
                    {
                        string strToDate = Convert.ToString(res.ToDate);
                        StringBuilder sbinnerhtml = new StringBuilder();
                        string strID = "txtTo_" + currentval.ToString();
                        sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control col-md-8' value=" + strToDate + " />");
                        sbinnerhtml.Append(dvTo.InnerHtml);
                        dvTo.InnerHtml = Convert.ToString(sbinnerhtml);
                    }
                }
            }
        }
        private void fillDaysOfWeek()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            TextBox txtStartTime = (TextBox)frmActivityFlavour.FindControl("txtStartTime");
            TextBox txtDuration = (TextBox)frmActivityFlavour.FindControl("txtDuration");
            DropDownList ddlSession = (DropDownList)frmActivityFlavour.FindControl("ddlSession");
            HtmlInputCheckBox chkMon = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkMon");
            HtmlInputCheckBox chkTues = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkTues");
            HtmlInputCheckBox chkWed = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkWed");
            HtmlInputCheckBox chkThurs = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkThurs");
            HtmlInputCheckBox chkFri = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkFri");
            HtmlInputCheckBox chkSat = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkSat");
            HtmlInputCheckBox chkSun = (HtmlInputCheckBox)frmActivityFlavour.FindControl("chkSun");
            HtmlInputRadioButton rdoDaysOfWeek = (HtmlInputRadioButton)frmActivityFlavour.FindControl("rdoDaysOfWeek");
            HtmlInputRadioButton rdoMonthly = (HtmlInputRadioButton)frmActivityFlavour.FindControl("rdoDaysOfWeek");
            HtmlInputRadioButton rdoSpecificDates = (HtmlInputRadioButton)frmActivityFlavour.FindControl("rdoDaysOfWeek");
            HtmlGenericControl dvStartTime = (HtmlGenericControl)frmActivityFlavour.FindControl("dvStartTime");
            HtmlGenericControl dvDuration = (HtmlGenericControl)frmActivityFlavour.FindControl("dvDuration");
            HtmlGenericControl dvddlSession = (HtmlGenericControl)frmActivityFlavour.FindControl("dvddlSession");
            Repeater rptDaysOfWeek = (Repeater)frmActivityFlavour.FindControl("rptDaysOfWeek");

            MDMSVC.DC_Activity_DaysOfWeek_RQ obj = new DC_Activity_DaysOfWeek_RQ();
            obj.Activity_Flavor_ID = Activity_Flavour_Id;
            var result = AccSvc.GetActivityDaysOfWeek(obj);

            var resultdataforsession = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivitySession").MasterAttributeValues;

            if (result != null || result.Count > 0)
            {
                rptDaysOfWeek.DataSource = result;
                rptDaysOfWeek.DataBind();
                
                int currentval = 0;
                foreach (DC_Activity_DaysOfWeek_RS res in result)
                {
                    

                    //txtStartTime.Text = res.StartTime;
                    //txtDuration.Text = res.Duration;
                    chkSun.Checked = res.Sun != null ? Convert.ToBoolean(res.Sun) : false;
                    chkMon.Checked = res.Sun != null ? Convert.ToBoolean(res.Mon) : false;
                    chkTues.Checked = res.Sun != null ? Convert.ToBoolean(res.Tues) : false;
                    chkWed.Checked = res.Sun != null ? Convert.ToBoolean(res.Wed) : false;
                    chkThurs.Checked = res.Sun != null ? Convert.ToBoolean(res.Thur) : false;
                    chkFri.Checked = res.Sun != null ? Convert.ToBoolean(res.Fri) : false;
                    chkSat.Checked = res.Sun != null ? Convert.ToBoolean(res.Sat) : false;

                    ddlSession.SelectedIndex = ddlSession.Items.IndexOf(ddlSession.Items.FindByText(res.Session.ToString()));
                    if (res.SupplierFrequency != null)
                    {
                        if (res.SupplierFrequency.ToLower().ToString() == "daily")
                        {
                            rdoDaysOfWeek.Checked = true;
                        }
                    }

                    for (int i = 1; i < result.Count; i++)
                    {
                        currentval = currentval + 1;

                        string strStartTime = Convert.ToString(res.StartTime);
                        StringBuilder sbinnerhtmlForStartTime = new StringBuilder();
                        string strIDForStartTime = "txtStartTime_" + currentval.ToString();
                        sbinnerhtmlForStartTime.Append("<input type='text' id=" + strIDForStartTime + " class='form-control col-md-8' value=" + strStartTime + " />");
                        sbinnerhtmlForStartTime.Append(dvStartTime.InnerHtml);
                        dvStartTime.InnerHtml = Convert.ToString(sbinnerhtmlForStartTime);

                        string strDuration = Convert.ToString(res.Duration);
                        StringBuilder sbinnerhtmlForDuration = new StringBuilder();
                        string strIDForDuration = "txtDuration_" + currentval.ToString();
                        sbinnerhtmlForDuration.Append("<input type='text' id=" + strIDForDuration + " class='form-control col-md-8' value=" + strDuration + " />");
                        sbinnerhtmlForDuration.Append(dvDuration.InnerHtml);
                        dvDuration.InnerHtml = Convert.ToString(sbinnerhtmlForDuration);

                        string strDDLSession = Convert.ToString(res.Session);
                        StringBuilder sbinnerhtmlForDDLSession = new StringBuilder();
                        string strIDForDDLSession = "ddlSession_" + currentval.ToString();
                        sbinnerhtmlForDDLSession.Append("<select id=" + strIDForDDLSession + " class='form-control col-md-8' value=" + strDDLSession + " />");
                        sbinnerhtmlForDDLSession.Append(dvddlSession.InnerHtml);

                        //dvddlSession.InnerHtml = Convert.ToString(sbinnerhtmlForDDLSession);

                        //DropDownList ddlstrIDForDDLSession = (DropDownList)dvddlSession.FindControl(strIDForDDLSession);
                        //if (ddlstrIDForDDLSession != null)
                        //{
                        //    ddlstrIDForDDLSession.DataSource = resultdataforsession;
                        //    ddlstrIDForDDLSession.DataTextField = "AttributeValue";
                        //    ddlstrIDForDDLSession.DataValueField = "MasterAttributeValue_Id";
                        //    ddlSession.DataBind();
                        //    ddlSession.Items.Insert(0, new ListItem("-Select-", "0"));
                        //}
                    }
                }
            }
        }
        private void fillcoutries()
        {
            DropDownList ddlCountry = (DropDownList)frmActivityFlavour.FindControl("ddlCountry");
            var countryList = masterSVc.GetAllCountries();
            ddlCountry.DataSource = countryList;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
            if (rowView != null)
            {
                if (rowView.Country != null)
                    ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(rowView.Country.ToString()));
            }
        }
        private void fillcities()
        {
            DropDownList ddlCity = (DropDownList)frmActivityFlavour.FindControl("ddlCity");
            DropDownList ddlCountry = (DropDownList)frmActivityFlavour.FindControl("ddlCountry");
            if (ddlCountry.SelectedItem.Text != "-Select-")
            {
                ddlCity.Items.Clear();
                ddlCity.DataSource = masterSVc.GetMasterCityData(ddlCountry.SelectedValue);
                ddlCity.DataTextField = "Name";
                ddlCity.DataValueField = "City_Id";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                if (rowView != null)
                {
                    if (rowView.City != null)
                        ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(rowView.City.ToString()));
                }
            }
        }
        private void fillproductcaterogysubtype()
        {
            try
            {
                DropDownList ddlProdcategorySubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdcategorySubType");
                ddlProdcategorySubType.Items.Clear();
                ddlProdcategorySubType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
                ddlProdcategorySubType.DataTextField = "AttributeValue";
                ddlProdcategorySubType.DataValueField = "MasterAttributeValue_Id";
                ddlProdcategorySubType.DataBind();
                ddlProdcategorySubType.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                if (rowView != null)
                {
                    if (rowView.ProductCategorySubType != null)
                    {
                        ddlProdcategorySubType.SelectedIndex = ddlProdcategorySubType.Items.IndexOf(ddlProdcategorySubType.Items.FindByText(rowView.ProductCategorySubType.ToString()));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void fillProductType()
        {
            DropDownList ddlProdcategorySubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdcategorySubType");

            DropDownList ddlProductType = (DropDownList)frmActivityFlavour.FindControl("ddlProductType");
            ddlProductType.Items.Clear();
            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            if (ddlProdcategorySubType.SelectedItem.Text != "-Select-")
            {
                var result = (from s in dropdownvalues
                              where s.ParentAttributeValue_Id == new Guid(ddlProdcategorySubType.SelectedValue)
                              select s);
                ddlProductType.DataSource = result;
                ddlProductType.DataTextField = "AttributeValue";
                ddlProductType.DataValueField = "MasterAttributeValue_Id";
                ddlProductType.DataBind();
                ddlProductType.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                if (rowView != null)
                {
                    if (rowView.ProductType != null)
                    {
                        ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText(rowView.ProductType.ToString()));
                    }
                }
            }
            else
            {
                ddlProductType.DataSource = dropdownvalues;
                ddlProductType.DataTextField = "AttributeValue";
                ddlProductType.DataValueField = "MasterAttributeValue_Id";
                ddlProductType.DataBind();
            }

            //ListBox lstboxProductType = (ListBox)frmActivityFlavour.FindControl("lstboxProductType");

            //lstboxProductType.Items.Clear();
            //var res = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            //lstboxProductType.DataSource = res;
            //lstboxProductType.DataTextField = "AttributeValue";
            //lstboxProductType.DataValueField = "MasterAttributeValue_Id";
            //lstboxProductType.DataBind();
            ////lst.Items.Insert(0, new ListItem("-Select-", "0"));
            //MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
            //if (rowView != null)
            //{
            //    if (rowView.ProductType != null)
            //    {
            //        //lstboxProductType.SelectedIndex = lstboxProductType.Items.IndexOf(lstboxProductType.Items.FindByText(rowView.ProductType.ToString()));
            //    }
            //}

            //else
            //{
            //    lstboxProductType.DataSource = res;
            //    lstboxProductType.DataTextField = "AttributeValue";
            //    lstboxProductType.DataValueField = "MasterAttributeValue_Id";
            //    lstboxProductType.DataBind();
            //}
        }

        //private void fillProductTypeDynamic()
        //{
        //    StringBuilder sbinnerhtml = new StringBuilder();
        //    string[] strArrayAttributeValue = strAttributeValue.Split(',');
        //    sbinnerhtml.Append(dvValueForFilter.InnerHtml);
        //    for (int i = 0; i < strArrayAttributeValue.Count(); i++)
        //    {

        //        //sbinnerhtml.Append("<div class='con inner-addon right-addon'><i id='btnAddValue' style='cursor: pointer' class='btnRemove glyphicon glyphicon-minus'></i>");
        //        //string strID = "txt" + i.ToString();
        //        //sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control' value=" + strArrayAttributeValue[i] + " /></div>");
        //        sbinnerhtml.Append("<div class='con'>");
        //        string strID = "txt" + i.ToString();
        //        // sbinnerhtml.Append("<div class='con inner-addon right-addon'><i id='btnAddValue' style='cursor: pointer' class='btnRemove glyphicon glyphicon-minus'></i>");
        //        sbinnerhtml.Append("<input type='text' id=" + strID + " class='form-control col-md-8 inputTypeForFilter' value=" + strArrayAttributeValue[i] + " />");
        //        sbinnerhtml.Append("<div class='input-group-btn col-md-4' style='padding-left: 0px !important;'><button class='btn btn-default btnRemove' id='btnAddValue' type='button'>");
        //        sbinnerhtml.Append("<i class='glyphicon glyphicon-minus'></i></button>");
        //        sbinnerhtml.Append("</div>");
        //        sbinnerhtml.Append("</div>");
        //    }
        //}
        private void fillproductsubtype()
        {
            DropDownList ddlProductType = (DropDownList)frmActivityFlavour.FindControl("ddlProductType");
            DropDownList ddlProdNameSubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdNameSubType");
            ddlProdNameSubType.Items.Clear();
            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductSubType").MasterAttributeValues;
            if (ddlProductType.SelectedItem.Text != "-Select-")
            {
                var res = (from s in dropdownvalues
                           where s.ParentAttributeValue_Id == new Guid(ddlProductType.SelectedValue)
                           select s);
                var search = (from s in res
                              orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                              select new { AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.AttributeValue }).ToList();
                ddlProdNameSubType.DataSource = search;
                ddlProdNameSubType.DataTextField = "AttributeValue";
                ddlProdNameSubType.DataValueField = "MasterAttributeValue_Id";
                ddlProdNameSubType.DataBind();
                ddlProdNameSubType.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                if (rowView != null)
                {
                    if (rowView.ProductNameSubType != null)
                    {
                        ddlProdNameSubType.SelectedIndex = ddlProdNameSubType.Items.IndexOf(ddlProdNameSubType.Items.FindByText(rowView.ProductNameSubType.ToString()));
                    }
                }
            }
            else
            {

                var search = (from s in dropdownvalues
                              orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                              select new { AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.AttributeValue }).ToList();
                ddlProdNameSubType.DataSource = search;
                ddlProdNameSubType.DataTextField = "AttributeValue";
                ddlProdNameSubType.DataValueField = "MasterAttributeValue_Id";
                ddlProdNameSubType.DataBind();
            }

        }
        private void fillDescriptions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            TextBox txtShortDescription = (TextBox)frmActivityFlavour.FindControl("txtShortDescription");
            TextBox txtLongDescription = (TextBox)frmActivityFlavour.FindControl("txtLongDescription");
            MDMSVC.DC_Activity_Descriptions_RQ RQ = new MDMSVC.DC_Activity_Descriptions_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = AccSvc.GetActivityDescription(RQ);
            if (result != null)
            {
                foreach (TLGX_Consumer.MDMSVC.DC_Activity_Descriptions desc in result)
                {
                    if (desc.DescriptionType.ToLower().ToString() == "short")
                        txtShortDescription.Text = this.Page.Server.HtmlEncode(desc.Description);
                    else
                        txtLongDescription.Text = this.Page.Server.HtmlEncode(desc.Description);
                }
            }
            else
            {
                txtShortDescription.Text = string.Empty;
                txtShortDescription.Text = string.Empty;
            }
        }
        private void fillSuitableFor()
        {
            CheckBoxList chklstSuitableFor = (CheckBoxList)frmActivityFlavour.FindControl("chklstSuitableFor");
            var res = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "SuitableFor").MasterAttributeValues;
            if (res != null)
            {
                chklstSuitableFor.DataSource = res;
                chklstSuitableFor.DataTextField = "AttributeValue";
                chklstSuitableFor.DataValueField = "AttributeValue";
                chklstSuitableFor.DataBind();

                if (chkCheckboxSelection())
                {
                    MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ = new DC_Activity_ClassificationAttributes_RQ();
                    RQ.Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                    var result = AccSvc.GetActivityClasificationAttributes(RQ);
                    if (result != null)
                    {
                        var resCount = result.Count();
                        for (int i = 0; i < resCount; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(result[i].AttributeValue))
                            {
                                var SuitableFor = result[i].AttributeValue;
                                for (int count = 0; count < chklstSuitableFor.Items.Count; count++)
                                {
                                    if (SuitableFor.ToLower().Contains(chklstSuitableFor.Items[count].ToString().ToLower()))
                                    {
                                        chklstSuitableFor.Items[count].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                chklstSuitableFor.DataSource = null;
                chklstSuitableFor.DataBind();
            }
        }
        protected bool chkCheckboxSelection()
        {
            MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ = new DC_Activity_ClassificationAttributes_RQ();
            RQ.Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            var result = AccSvc.GetActivityClasificationAttributes(RQ);
            if (result != null)
                return true;
            else
                return false;
        }
        private void fillPhysicalIntensity()
        {
            CheckBoxList chklstPhysicalIntensity = (CheckBoxList)frmActivityFlavour.FindControl("chklstPhysicalIntensity");
            var res = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "PhysicalIntensity").MasterAttributeValues;
            if (res != null)
            {
                chklstPhysicalIntensity.DataSource = res;
                chklstPhysicalIntensity.DataTextField = "AttributeValue";
                chklstPhysicalIntensity.DataValueField = "AttributeValue";
                chklstPhysicalIntensity.DataBind();

                if (chkCheckboxSelection())
                {
                    MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ = new DC_Activity_ClassificationAttributes_RQ();
                    RQ.Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                    var result = AccSvc.GetActivityClasificationAttributes(RQ);
                    if (result != null)
                    {
                        var resCount = result.Count();
                        for (int i = 0; i < resCount; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(result[i].AttributeValue))
                            {
                                var SuitableFor = result[i].AttributeValue;
                                for (int count = 0; count < chklstPhysicalIntensity.Items.Count; count++)
                                {
                                    if (SuitableFor.ToLower().Contains(chklstPhysicalIntensity.Items[count].ToString().ToLower()))
                                    {
                                        chklstPhysicalIntensity.Items[count].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                chklstPhysicalIntensity.DataSource = null;
                chklstPhysicalIntensity.DataBind();
            }
        }
        private void fillDDLSession()
        {

            try
            {
                DropDownList ddlSession = (DropDownList)frmActivityFlavour.FindControl("ddlSession");
                ddlSession.Items.Clear();
                ddlSession.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivitySession").MasterAttributeValues;
                ddlSession.DataTextField = "AttributeValue";
                ddlSession.DataValueField = "MasterAttributeValue_Id";
                ddlSession.DataBind();
                ddlSession.Items.Insert(0, new ListItem("-Select-", "0"));
                //MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                //if (rowView != null)
                //{
                //    if (rowView.ProductCategorySubType != null)
                //    {
                //        ddlProdcategorySubType.SelectedIndex = ddlProdcategorySubType.Items.IndexOf(ddlProdcategorySubType.Items.FindByText(rowView.ProductCategorySubType.ToString()));
                //    }
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities();
        }
        protected void ddlProdcategorySubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProductType = (DropDownList)frmActivityFlavour.FindControl("ddlProductType");
            ddlProductType.Items.Clear();
            DropDownList ddlProdNameSubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdNameSubType");
            ddlProdNameSubType.Items.Clear();
            fillProductType();
        }
        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProdNameSubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdNameSubType");
            ddlProdNameSubType.Items.Clear();
            fillproductsubtype();
        }
        protected void frmActivityFlavour_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveProduct")
            {
                //name
                TextBox txtActivity_Flavour_Id = (TextBox)frmActivityFlavour.FindControl("txtActivity_Flavour_Id");
                TextBox txtProductName = (TextBox)frmActivityFlavour.FindControl("txtProductName");
                //address info

                DropDownList ddlCountryTLGX = (DropDownList)frmActivityFlavour.FindControl("ddlCountryTLGX");
                DropDownList ddlCity = (DropDownList)frmActivityFlavour.FindControl("ddlCity");
                //Classification Attributes
                DropDownList ddlProdCategory = (DropDownList)frmActivityFlavour.FindControl("ddlProdCategory");
                DropDownList ddlProdcategorySubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdcategorySubType");
                DropDownList ddlProductType = (DropDownList)frmActivityFlavour.FindControl("ddlProductType");
                DropDownList ddlProdNameSubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdNameSubType");
                //key facts

                //General info
                TextBox txtEventPlace = (TextBox)frmActivityFlavour.FindControl("txtEventPlace");

                MDMSVC.DC_Activity_Flavour FlavData = new MDMSVC.DC_Activity_Flavour();

                FlavData.Activity_Flavour_Id = new Guid(txtActivity_Flavour_Id.Text);
                FlavData.Legacy_Product_ID = AccSvc.GetLegacyProductId(Activity_Flavour_Id);
                if (ddlCountryTLGX.SelectedIndex != 0)
                {
                    FlavData.Country = ddlCountryTLGX.SelectedItem.Text;
                    FlavData.Country = ddlCountryTLGX.SelectedItem.Value;
                }
                if (ddlCity.SelectedIndex != 0)
                {
                    FlavData.City = ddlCity.SelectedItem.Text;
                    FlavData.City = ddlCity.SelectedItem.Value;
                }

                if (ddlProdCategory.SelectedIndex != 0)
                    FlavData.ProductCategory = ddlProdCategory.SelectedItem.Text;

                if (ddlProdcategorySubType.SelectedIndex != 0)
                    FlavData.ProductCategorySubType = ddlProdcategorySubType.SelectedItem.Text;

                if (ddlProductType.SelectedIndex != 0)
                    FlavData.ProductType = ddlProductType.SelectedItem.Text;

                if (ddlProdNameSubType.SelectedIndex != 0)
                    FlavData.ProductNameSubType = ddlProdNameSubType.SelectedItem.Text;

                if (!string.IsNullOrEmpty(txtProductName.Text))
                    FlavData.ProductName = txtProductName.Text.ToString();

                FlavData.Edit_Date = DateTime.Now;
                FlavData.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                var result = AccSvc.AddUpdateActivityFlavour(FlavData);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);

            }
            else if (e.CommandName == "CancelProduct")
            {
                //TextBox txtProductName = (TextBox)this.Parent.Page.FindControl("txtProductName");
                System.Web.UI.HtmlControls.HtmlTextArea txtProductName = (System.Web.UI.HtmlControls.HtmlTextArea)frmActivityFlavour.FindControl("txtProductName");
                DropDownList ddlCountryTLGX = (DropDownList)this.Parent.Page.FindControl("ddlCountryTLGX");
            }
        }
        protected void rptDaysOfWeek_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var resultdataforsession = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivitySession").MasterAttributeValues;
            DropDownList ddlSession_ = (DropDownList)e.Item.FindControl("ddlSession_");
            ddlSession_.DataSource = resultdataforsession;
            ddlSession_.DataTextField = "AttributeValue";
            ddlSession_.DataValueField = "MasterAttributeValue_Id";
            ddlSession_.DataBind();

        }
    }
}
