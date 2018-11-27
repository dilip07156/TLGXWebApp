using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;
using TLGX_Consumer.App_Code;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class CountryMap : System.Web.UI.UserControl
    {
        MDMSVC.DC_CountryMappingRQ RQ = new MDMSVC.DC_CountryMappingRQ();
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        public DataTable dtCountryMappingSearchResults = new DataTable();
        public DataTable dtCountrMappingDetail = new DataTable();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        MasterDataDAL masters = new MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        public static string AttributeOptionFor = "ProductSupplierMapping";
        public static string SortBy = "";
        public static string SortEx = "";
        public static int PageIndex = 0;
        public static int MatchedPageIndex = 0;
        public static Guid? MappedCountry_ID = Guid.Empty;
        public static string MatchedSupplierName = "";
        public static string MatchedStatus = "";

        // this currently gets all suppliers, SupplierDataMode method will give you the control over what to get
        protected void bindSupplierCountryMappingGrid()
        {
            dtCountryMappingSearchResults = objMasterDataDAL.GetSupplierCountryMapping(Models.MasterDataDAL.SupplierDataMode.AllSupplierAllCountry, Guid.Empty, Guid.Empty);
            grdCountryMaps.DataSource = dtCountryMappingSearchResults;
            grdCountryMaps.DataBind();
        }




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                fillsuppliers();
                fillcountries(ddlMasterCountry, "");
                fillmappingstatus(ddlStatus);
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
            }
            // handle the postback
            //bindSupplierCountryMappingGrid();

        }

        private void fillmappingstatus(DropDownList ddl)
        {
            fillAttributeValues(ddl, "MappingStatus");
        }

        private void fillAttributeValues(DropDownList ddl, string Attribute_Name)
        {
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, Attribute_Name).MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
        }
        private void fillcountries(DropDownList ddl, string option)
        {
            //ddl.DataSource = objMasterDataDAL.GetMasterCountryData(option);
            //ddl.DataValueField = "Country_ID";
            //ddl.DataTextField = "Name";
            ddl.DataSource = _objMasterSVC.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
        }

        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = objMasterDataDAL.GetSupplierMasterData();
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        protected void grdCountryMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmEditCountryMap.ChangeMode(FormViewMode.Edit);

            // need to add in the code to get and bind

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SortBy = "CountryName";
            SortEx = "";
            PageIndex = 0;
            fillmappingdata();

            if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW" || ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                btnMapSelected.Visible = true;
                btnMapAll.Visible = true;
            }
            else
            {
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
            }
            // BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Records Searched successfully", BootstrapAlertType.Success);
        }

        private void fillmatchingdata()
        {
            MDMSVC.DC_CountryMappingRQ RQParam = new MDMSVC.DC_CountryMappingRQ();
            RQParam.Supplier_Id = Guid.Empty;
            RQParam.Country_Id = Guid.Empty;
            RQParam.Status = "All";
            RQParam.StatusExcept = MatchedStatus;
            RQParam.SupplierCountryName = MatchedSupplierName;
            RQParam.PageNo = MatchedPageIndex;
            RQParam.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            RQParam.SortBy = (SortBy + " " + SortEx).Trim();
            var res = mapperSVc.GetCountryMappingData(RQParam);
            grdMatchingCountry.DataSource = res; //.Where(Country => Country.Status.ToUpper()!="MAPPED");
            if (res != null && res.Count > 0)
            {
                grdMatchingCountry.VirtualItemCount = res[0].TotalRecord;
                if (res[0].TotalRecord > 0)
                {
                    lblMsg.Text = "There are (" + res[0].TotalRecord.ToString() + ") similar records in system matching with mapped combination. Do you wish to map the same? ";
                }
                else
                {
                    lblMsg.Text = "No similar records found matching with updated combination";
                }
                //lblTotalCount.Text = res[0].TotalRecord.ToString();
            }
            else
            {
                lblMsg.Text = "No similar records found matching with updated combination";
            }
            //else
            //{
            //    lblTotalCount.Text = "0";
            //}
            grdMatchingCountry.PageIndex = MatchedPageIndex;
            grdMatchingCountry.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            grdMatchingCountry.DataBind();

        }

        private void fillmappingdata()
        {
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            if (ddlSupplierName.SelectedItem.Value != "0")
                //selSupplier_ID = new Guid(ddlSupplierName.SelectedItem.Value);
                RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
            else
                RQ.Supplier_Id = Guid.Empty;
            if (ddlMasterCountry.SelectedItem.Value != "0")
                //selCountry_ID = new Guid(ddlMasterCountry.SelectedItem.Value);
                RQ.SystemCountryName = ddlMasterCountry.SelectedItem.Text;
            else
                RQ.Country_Id = Guid.Empty;

            if (ddlStatus.SelectedItem.Value == "0")
                RQ.Status = "All"; //selStatus = "ALL";
            else
                RQ.Status = ddlStatus.SelectedItem.Text;//selStatus = ddlStatus.SelectedItem.Text;
                                                        //if (txtSuppCountry.Text != "")
            RQ.SupplierCountryName = txtSuppCountry.Text;

            RQ.PageNo = PageIndex;
            RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            RQ.SortBy = (SortBy + " " + SortEx).Trim();
            //var res = mapperSVc.GetCountryMappingData(PageIndex, Convert.ToInt32(ddlShowEntries.SelectedItem.Text), selSupplier_ID, selCountry_ID, selStatus, (SortBy + " " + SortEx).Trim());
            var res = mapperSVc.GetCountryMappingData(RQ);
            grdCountryMaps.DataSource = res;
            if (res != null && res.Count > 0)
            {
                grdCountryMaps.VirtualItemCount = res[0].TotalRecord;
                lblTotalCount.Text = res[0].TotalRecord.ToString();
            }
            else
            {
                lblTotalCount.Text = "0";
            }
            grdCountryMaps.PageIndex = PageIndex;
            grdCountryMaps.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            grdCountryMaps.DataBind();
        }

        protected void grdCountryMaps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            PageIndex = e.NewPageIndex;
            fillmappingdata();
        }

        protected void grdCountryMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                if (e.CommandName == "Select")
                {
                    dvMsgForDelete.Visible = false;
                    grdMatchingCountry.DataSource = null;
                    grdMatchingCountry.DataBind();
                    dvMatchingRecords.Visible = false;
                    List<MDMSVC.DC_CountryMapping> obj = new List<MDMSVC.DC_CountryMapping>();
                    obj.Add(new MDMSVC.DC_CountryMapping
                    {
                        CountryMapping_Id = myRow_Id,
                        //MapID = Convert.ToInt32(grdCountryMaps.Rows[index].Cells[0].Text),
                        SupplierName = grdCountryMaps.Rows[index].Cells[1].Text,
                        CountryCode = grdCountryMaps.Rows[index].Cells[2].Text,
                        CountryName = grdCountryMaps.Rows[index].Cells[3].Text,
                        Code = grdCountryMaps.Rows[index].Cells[4].Text,
                        Name = grdCountryMaps.Rows[index].Cells[5].Text,
                        Status = grdCountryMaps.Rows[index].Cells[7].Text
                    });

                    //SupplierMasters sData = new SupplierMasters();
                    //sData = masters.GetSupplierDataByMapping_Id("country", myRow_Id);

                    MDMSVC.DC_Supplier_DDL sData = new MDMSVC.DC_Supplier_DDL();
                    sData = _objMasterSVC.GetSupplierDataByMapping_Id("COUNTRY", Convert.ToString(myRow_Id));
                    string supCode = sData.Code;
                    //string supCode = masters.GetSupplierCodeById(new Guid(grdCountryMaps.Rows[index].Cells[7].Text));

                    frmEditCountryMap.Visible = true;
                    frmEditCountryMap.ChangeMode(FormViewMode.Edit);
                    frmEditCountryMap.DataSource = obj;
                    frmEditCountryMap.DataBind();

                    Label lblSupplierName = (Label)frmEditCountryMap.FindControl("lblSupplierName");
                    Label lblSupplierCode = (Label)frmEditCountryMap.FindControl("lblSupplierCode");
                    Label lblSupCountryName = (Label)frmEditCountryMap.FindControl("lblSupCountryName");
                    Label lblSupCountryCode = (Label)frmEditCountryMap.FindControl("lblSupCountryCode");
                    DropDownList ddlSystemCountryName = (DropDownList)frmEditCountryMap.FindControl("ddlSystemCountryName");
                    TextBox txtSystemCountryCode = (TextBox)frmEditCountryMap.FindControl("txtSystemCountryCode");
                    DropDownList ddlStatus = (DropDownList)frmEditCountryMap.FindControl("ddlStatus");
                    TextBox txtSystemRemark = (TextBox)frmEditCountryMap.FindControl("txtSystemRemark");
                    TextBox txtISO2CHAR = (TextBox)frmEditCountryMap.FindControl("txtISO2CHAR");
                    TextBox txtISO3CHAR = (TextBox)frmEditCountryMap.FindControl("txtISO3CHAR");
                    fillcountries(ddlSystemCountryName, "");
                    fillmappingstatus(ddlStatus);
                    lblSupplierName.Text = System.Web.HttpUtility.HtmlDecode(grdCountryMaps.Rows[index].Cells[1].Text);
                    lblSupplierCode.Text = "(" + supCode + ")";
                    lblSupCountryName.Text = System.Web.HttpUtility.HtmlDecode(grdCountryMaps.Rows[index].Cells[3].Text);
                    lblSupCountryCode.Text = System.Web.HttpUtility.HtmlDecode(grdCountryMaps.Rows[index].Cells[2].Text);
                    //txtSystemCountryCode.Text = System.Web.HttpUtility.HtmlDecode(grdCountryMaps.Rows[index].Cells[4].Text);
                    txtSystemRemark.Text = masters.GetRemarksForMapping("country", myRow_Id);
                    if (grdCountryMaps.Rows[index].Cells[7].Text.ToString() == "REVIEW" || grdCountryMaps.Rows[index].Cells[7].Text.ToString() == "MAPPED")
                    {
                        ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCountryMaps.Rows[index].Cells[5].Text)));
                        ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCountryMaps.Rows[index].Cells[7].Text)));

                        var result = _objMasterSVC.GetCountryCodes(ddlSystemCountryName.SelectedValue);
                        if (result != null && result.Count > 0)
                        {
                            txtSystemCountryCode.Text = result[0].Code;
                            txtISO2CHAR.Text = result[0].ISO3166_1_Alpha_2;
                            txtISO3CHAR.Text = result[0].ISO3166_1_Alpha_3;
                        }

                    }
                    hdnFlag.Value = "false";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "javascript:showCountryMappingModal();", true);
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Pop1", "javascript:showCountryMappingModal();", false);
                }
            }
            catch
            {
            }
        }

        protected void frmEditCountryMap_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Label lblSupplierName = (Label)frmEditCountryMap.FindControl("lblSupplierName");
            Label lblSupplierCode = (Label)frmEditCountryMap.FindControl("lblSupplierCode");
            Label lblSupCountryName = (Label)frmEditCountryMap.FindControl("lblSupCountryName");
            Label lblSupCountryCode = (Label)frmEditCountryMap.FindControl("lblSupCountryCode");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCountryMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditCountryMap.FindControl("txtSystemCountryCode");
            DropDownList ddlStatus = (DropDownList)frmEditCountryMap.FindControl("ddlStatus");
            TextBox txtSystemRemark = (TextBox)frmEditCountryMap.FindControl("txtSystemRemark");
            TextBox txtISO2CHAR = (TextBox)frmEditCountryMap.FindControl("txtISO2CHAR");
            TextBox txtISO3CHAR = (TextBox)frmEditCountryMap.FindControl("txtISO3CHAR");
            Button btnMatchedMapSelected = (Button)frmEditCountryMap.FindControl("btnMatchedMapSelected");
            Button btnMatchedMapAll = (Button)frmEditCountryMap.FindControl("btnMatchedMapAll");

            if (e.CommandName == "Add")
            {
                Guid? countryid = null;
                string code = string.Empty;
                string name = string.Empty;
                Guid myRow_Id = Guid.Parse(grdCountryMaps.SelectedDataKey.Value.ToString());
                //SupplierMasters sData = new SupplierMasters();
                //sData = masters.GetSupplierDataByMapping_Id("country", myRow_Id);

                MDMSVC.DC_Supplier_DDL sData = new MDMSVC.DC_Supplier_DDL();
                sData = _objMasterSVC.GetSupplierDataByMapping_Id("COUNTRY", Convert.ToString(myRow_Id));

                if (!(ddlSystemCountryName.SelectedIndex == 0))
                {
                    countryid = new Guid(ddlSystemCountryName.SelectedItem.Value);
                    code = masters.GetCodeById("country", new Guid(ddlSystemCountryName.SelectedItem.Value));
                    name = ddlSystemCountryName.SelectedItem.Text;
                }

                MDMSVC.DC_CountryMapping newObj = new MDMSVC.DC_CountryMapping
                {
                    CountryMapping_Id = myRow_Id,
                    Country_Id = countryid,
                    Supplier_Id = sData.Supplier_Id,
                    SupplierName = sData.Name,
                    Code = code,
                    Status = ddlStatus.SelectedItem.Text,
                    Name = name,
                    Remarks = txtSystemRemark.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                };
                List<MDMSVC.DC_CountryMapping> RQUpCountry = new List<MDMSVC.DC_CountryMapping>();
                RQUpCountry.Add(newObj);
                if (mapperSVc.UpdateCountryMappingDatat(RQUpCountry))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been updated successfully", BootstrapAlertType.Success);
                    //frmEditCountryMap.Visible = false;
                    MatchedPageIndex = 0;
                    MappedCountry_ID = countryid;
                    MatchedSupplierName = lblSupCountryName.Text;
                    MatchedStatus = ddlStatus.SelectedItem.Text;
                    //if (ddlStatus.SelectedItem.Text == "MAPPED")
                    if (!(ddlStatus.SelectedItem.Text == "DELETE"))
                    {
                        fillmatchingdata();
                        fillmappingdata();
                        dvMatchingRecords.Visible = true;
                        btnMatchedMapSelected.Visible = true;
                        btnMatchedMapAll.Visible = true;
                    }
                    else if (ddlStatus.SelectedItem.Text == "DELETE")
                    {
                        dvMsgForDelete.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsgForDelete, "Record has been updated successfully", BootstrapAlertType.Success);
                        dvMatchingRecords.Visible = false;
                        btnMatchedMapSelected.Visible = false;
                        btnMatchedMapAll.Visible = false;
                    }
                }
                hdnFlag.Value = "false";
            }
            else if (e.CommandName == "Cancel")
            {
                dvMsg.Style.Add("display", "none");
                frmEditCountryMap.ChangeMode(FormViewMode.Edit);
                frmEditCountryMap.DataSource = null;
                frmEditCountryMap.DataBind();
                frmEditCountryMap.Visible = false;
                dvMatchingRecords.Visible = false;
                btnMatchedMapSelected.Visible = false;
                btnMatchedMapAll.Visible = false;
                fillmappingdata();
                hdnFlag.Value = "false";
            }
            else if (e.CommandName == "MapSelected")
            {
                List<MDMSVC.DC_CountryMapping> RQ = new List<MDMSVC.DC_CountryMapping>();

                Guid myRow_Id = Guid.Empty;
                Guid mySupplier_Id = Guid.Empty;
                Guid? myCountry_Id = Guid.Empty;
                foreach (GridViewRow row in grdMatchingCountry.Rows)
                {
                    //CheckBox chk = row.Cells[7].Controls[1] as CheckBox;
                    HtmlInputCheckBox chk = row.Cells[7].Controls[1] as HtmlInputCheckBox;

                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdMatchingCountry.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdMatchingCountry.DataKeys[index].Values[1].ToString());
                        myCountry_Id = MappedCountry_ID;
                    }
                    if (myRow_Id != Guid.Empty)
                    {
                        MDMSVC.DC_CountryMapping param = new MDMSVC.DC_CountryMapping();
                        param.CountryMapping_Id = myRow_Id;
                        if (mySupplier_Id != null)
                            param.Supplier_Id = mySupplier_Id;
                        if (myCountry_Id != null)
                            param.Country_Id = myCountry_Id;
                        param.Status = MatchedStatus;
                        param.Edit_Date = DateTime.Now;
                        param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                        RQ.Add(param);
                        //res = mapperSVc.UpdateCountryMappingDatat(RQ);
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                    }
                }
                if (mapperSVc.UpdateCountryMappingDatat(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
                    fillmatchingdata();
                    fillmappingdata();
                    hdnFlag.Value = "false";
                }
            }
            else if (e.CommandName == "MapAll")
            {
                List<MDMSVC.DC_CountryMapping> RQ = new List<MDMSVC.DC_CountryMapping>();

                Guid myRow_Id = Guid.Empty;
                Guid mySupplier_Id = Guid.Empty;
                Guid? myCountry_Id = Guid.Empty;
                
                foreach (GridViewRow row in grdMatchingCountry.Rows)
                {
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdMatchingCountry.DataKeys[index].Values[0].ToString());
                    mySupplier_Id = Guid.Parse(grdMatchingCountry.DataKeys[index].Values[1].ToString());
                    myCountry_Id = MappedCountry_ID;
                    if (myRow_Id != Guid.Empty)
                    {
                        MDMSVC.DC_CountryMapping param = new MDMSVC.DC_CountryMapping();
                        param.CountryMapping_Id = myRow_Id;
                        if (mySupplier_Id != null)
                            param.Supplier_Id = mySupplier_Id;
                        if (myCountry_Id != null)
                            param.Country_Id = myCountry_Id;
                        param.Status = MatchedStatus;
                        param.Edit_Date = DateTime.Now;
                        param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                        RQ.Add(param);
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                    }
                }
                if (mapperSVc.UpdateCountryMappingDatat(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
                    fillmatchingdata();
                    fillmappingdata();
                }
                hdnFlag.Value = "false";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2" + DateTime.Today.Ticks.ToString(), "javascript:closeCountryMappingModal();", true);

        }

        protected void ddlSystemCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCountryMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditCountryMap.FindControl("txtSystemCountryCode");
            TextBox txtISO2CHAR = (TextBox)frmEditCountryMap.FindControl("txtISO2CHAR");
            TextBox txtISO3CHAR = (TextBox)frmEditCountryMap.FindControl("txtISO3CHAR");
            //CountryMasterE cmaster = new CountryMasterE();
            //cmaster = masters.getCountryCodes(new Guid(ddlSystemCountryName.SelectedItem.Value));
            //if (cmaster != null)
            //{
            //    txtSystemCountryCode.Text = cmaster.Code;
            //    txtISO2CHAR.Text = cmaster.ISO3166_1_Alpha_2;
            //    txtISO3CHAR.Text = cmaster.ISO3166_1_Alpha_3;
            //}
            var result = _objMasterSVC.GetCountryCodes(ddlSystemCountryName.SelectedValue);
            if (result != null && result.Count > 0)
            {
                txtSystemCountryCode.Text = result[0].Code;
                txtISO2CHAR.Text = result[0].ISO3166_1_Alpha_2;
                txtISO3CHAR.Text = result[0].ISO3166_1_Alpha_3;
            }
            else
            {
                txtSystemCountryCode.Text = string.Empty;
                txtISO2CHAR.Text = string.Empty;
                txtISO3CHAR.Text = string.Empty;
            }
            //txtSystemCountryCode.Text = masters.GetCodeById("country", new Guid(ddlSystemCountryName.SelectedItem.Value));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlMasterCountry.SelectedIndex = 0;
            ddlSupplierName.SelectedIndex = 0;
            ddlShowEntries.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            grdCountryMaps.DataSource = null;
            txtSuppCountry.Text = "";
            grdCountryMaps.DataBind();
            frmEditCountryMap.DataSource = null;
            frmEditCountryMap.DataBind();
            btnMapAll.Visible = false;
            btnMapSelected.Visible = false;
            lblTotalCount.Text = "0";
        }

        protected void grdCountryMaps_Sorting(object sender, GridViewSortEventArgs e)
        {

            SortBy = e.SortExpression;
            if (SortEx == "")
                SortEx = "DESC";
            else
                SortEx = "";
            //SortEx = e.SortDirection.ToString();
            fillmappingdata();
        }

        protected void ddlSystemCountryName_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void grdMatchingCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdMatchingCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            MatchedPageIndex = e.NewPageIndex;
            fillmatchingdata();
        }

        protected void grdMatchingCountry_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdMatchingCountry_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnMapSelected_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_CountryMapping> RQ = new List<MDMSVC.DC_CountryMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid myCountry_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdCountryMaps.Rows)
            {
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    HtmlInputCheckBox chk = row.Cells[9].Controls[1] as HtmlInputCheckBox;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[1].ToString());
                        myCountry_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[2].ToString());
                    }
                }
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    HtmlInputCheckBox chk = row.Cells[9].Controls[1] as HtmlInputCheckBox;
                    DropDownList ddl = row.Cells[6].Controls[1] as DropDownList;
                    HiddenField hdnCountryId = row.Cells[10].Controls[1] as HiddenField; //Set value from ajax changes
                    if (chk != null && chk.Checked)
                    {

                        if (!string.IsNullOrWhiteSpace(hdnCountryId.Value) && hdnCountryId.Value != "0")
                        {
                            if (hdnCountryId != null)
                                myCountry_Id = Guid.Parse(hdnCountryId.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                myCountry_Id = Guid.Parse(ddl.SelectedItem.Value);
                        }

                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[1].ToString());

                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_CountryMapping param = new MDMSVC.DC_CountryMapping();
                    param.CountryMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myCountry_Id != null)
                        param.Country_Id = myCountry_Id;
                    param.Status = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    res = mapperSVc.UpdateCountryMappingDatat(RQ);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                }
            }
            BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Record has been updated successfully", BootstrapAlertType.Success);
            fillmappingdata();
        }

        protected void btnMapAll_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_CountryMapping> RQ = new List<MDMSVC.DC_CountryMapping>();

            Guid myRow_Id;
            Guid mySupplier_Id;
            Guid myCountry_Id;
            bool res = false;
            foreach (GridViewRow row in grdCountryMaps.Rows)
            {
                myRow_Id = Guid.Empty;
                mySupplier_Id = Guid.Empty;
                myCountry_Id = Guid.Empty;
                int index = row.RowIndex;
                DropDownList ddl = row.Cells[6].Controls[1] as DropDownList;
                MDMSVC.DC_CountryMapping param = new MDMSVC.DC_CountryMapping();
                myRow_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[0].ToString());
                mySupplier_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[1].ToString());
                HiddenField hdnCountryId = row.Cells[10].Controls[1] as HiddenField;
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    if (ddl.SelectedItem.Value != "0" || !string.IsNullOrWhiteSpace(hdnCountryId.Value))
                    {
                        if (!string.IsNullOrWhiteSpace(hdnCountryId.Value))
                        {
                            if (hdnCountryId.Value == "0")
                                continue;
                            if (hdnCountryId != null)
                                myCountry_Id = Guid.Parse(hdnCountryId.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                myCountry_Id = Guid.Parse(ddl.SelectedItem.Value);
                        }
                    }
                    else
                    {
                        myCountry_Id = Guid.Empty;
                    }
                }
                else
                    myCountry_Id = Guid.Parse(grdCountryMaps.DataKeys[index].Values[2].ToString());
                param.CountryMapping_Id = myRow_Id;
                if (mySupplier_Id != null)
                    param.Supplier_Id = mySupplier_Id;
                if (myCountry_Id != null)
                    param.Country_Id = myCountry_Id;
                param.Status = "MAPPED";
                param.Edit_Date = DateTime.Now;
                param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                if (myCountry_Id != Guid.Empty)
                    RQ.Add(param);
                res = mapperSVc.UpdateCountryMappingDatat(RQ);
            }
            BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Record has been updated successfully", BootstrapAlertType.Success);
            fillmappingdata();
        }

        protected void grdCountryMaps_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            //foreach (GridViewRow row in myGridView.Rows)
            //{
            if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                myGridView.Columns[5].Visible = true;
                myGridView.Columns[6].Visible = false;
                myGridView.Columns[9].Visible = true;
            }
            else if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                myGridView.Columns[5].Visible = false;
                myGridView.Columns[6].Visible = true;
                myGridView.Columns[9].Visible = true;
            }
            else
            {
                myGridView.Columns[5].Visible = true;
                myGridView.Columns[6].Visible = false;
                myGridView.Columns[9].Visible = false;
            }
            //}
        }

        protected void grdCountryMaps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var myGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                DropDownList ddl = e.Row.Cells[6].Controls[1] as DropDownList;
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    if (ddl != null)
                    {
                        var myCountryId = Convert.ToString(myGridView.DataKeys[index].Values[3]);
                        var myCountryName = Convert.ToString(myGridView.DataKeys[index].Values[4]);
                        if (!string.IsNullOrWhiteSpace(myCountryId) && !string.IsNullOrWhiteSpace(myCountryName))
                        {
                            ddl.Items.Add(new ListItem(myCountryName, myCountryId));
                            if (ddl.Items.FindByValue(myCountryId) != null)
                                ddl.Items.FindByValue(myCountryId).Selected = true;
                        }
                    }
                    ////fillcountries(ddl, "code");
                    ////var res = objMasterDataDAL.GetMasterCountryDataList("code");
                   //var res = _objMasterSVC.GetMasterCountryDataList();
                    //ddl.DataSource = res;
                    //ddl.DataValueField = "Country_ID";
                    //ddl.DataTextField = "NameWithCode";
                    //ddl.DataBind();
                    //string ccode = e.Row.Cells[2].Text.ToUpper();
                    //string cname = e.Row.Cells[3].Text.ToUpper();

                    //if (cname != null)
                    //{
                    //    if (cname != "&nbsp;" && cname != "")
                    //    {
                    //        IEnumerable<MDMSVC.DC_CountryMaster> querynamed = res.Where(country => country.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("country", "").ToUpper().Equals(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("country", "").ToUpper()));
                    //        if (querynamed.Count() > 0)
                    //        {
                    //            foreach (MDMSVC.DC_CountryMaster resc in querynamed)
                    //            {
                    //                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.Country_ID.ToString()));
                    //            }
                    //        }

                    //        if (ddl.SelectedIndex == 0)
                    //        {
                    //            IEnumerable<MDMSVC.DC_CountryMaster> queryname = res.Where(country => country.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("country", "").ToUpper().Contains(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("country", "").ToUpper()));
                    //            if (queryname.Count() > 0)
                    //            {
                    //                foreach (MDMSVC.DC_CountryMaster resc in queryname)
                    //                {
                    //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.Country_ID.ToString()));
                    //                }
                    //            }
                    //        }
                    //    }
                    //    if (ccode != "&nbsp;" && ccode != "")
                    //    {
                    //        if (ddl.SelectedIndex == 0)
                    //        {
                    //            IEnumerable<MDMSVC.DC_CountryMaster> queryname = res.Where(country => country.Name.ToUpper().Equals(ccode));
                    //            if (queryname.Count() > 0)
                    //            {
                    //                foreach (MDMSVC.DC_CountryMaster resc in queryname)
                    //                {
                    //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.Country_ID.ToString()));
                    //                }
                    //            }
                    //        }
                    //        if (ddl.SelectedIndex == 0)
                    //        {
                    //            IEnumerable<MDMSVC.DC_CountryMaster> queryname = res.Where(country => country.Code.ToUpper().Equals(ccode));
                    //            if (queryname.Count() > 0)
                    //            {
                    //                foreach (MDMSVC.DC_CountryMaster resc in queryname)
                    //                {
                    //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.Country_ID.ToString()));
                    //                }
                    //            }
                    //        }
                    //        if (ddl.SelectedIndex == 0)
                    //        {
                    //            IEnumerable<MDMSVC.DC_CountryMaster> querycode2 = res.Where(country => country.ISO3166_1_Alpha_2.ToUpper().Equals(ccode));
                    //            if (querycode2.Count() > 0)
                    //            {
                    //                foreach (MDMSVC.DC_CountryMaster resc in querycode2)
                    //                {
                    //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.Country_ID.ToString()));
                    //                }
                    //            }
                    //        }
                    //        if (ddl.SelectedIndex == 0)
                    //        {
                    //            IEnumerable<MDMSVC.DC_CountryMaster> querycode3 = res.Where(country => country.ISO3166_1_Alpha_3.ToUpper().Equals(ccode));
                    //            if (querycode3.Count() > 0)
                    //            {
                    //                foreach (MDMSVC.DC_CountryMaster resc in querycode3)
                    //                {
                    //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.Country_ID.ToString()));
                    //                }
                    //            }
                    //        }
                    //        //ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText());
                    //    }
                    //}

                    //
                }
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            var myCheckBox = (CheckBox)sender;
            int rowIndex = ((GridViewRow)myCheckBox.BindingContainer).RowIndex;
            GridView myGridView = ((GridView)myCheckBox.DataKeysContainer);
            int nextRowIndex = rowIndex + 1;
            if (nextRowIndex != myGridView.Rows.Count)
            {
                GridViewRow nextRow = myGridView.Rows[nextRowIndex];
                if (nextRow != null)
                {
                    if (ddlStatus.SelectedItem.Text == "REVIEW")
                    {
                        CheckBox chk = nextRow.Cells[9].Controls[1] as CheckBox;
                        if (chk != null)
                            chk.Focus();
                    }
                    else if (ddlStatus.SelectedItem.Text == "UNMAPPED")
                    {
                        DropDownList ddl = nextRow.Cells[6].Controls[1] as DropDownList;
                        if (ddl != null)
                            ddl.Focus();
                    }
                }
            }
            else
                btnMapSelected.Focus();

        }

        protected void ddlMatchingPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            fillmatchingdata();
        }

        protected void chkMatchedSelect_CheckedChanged(object sender, EventArgs e)
        {
            //var myCheckBox = (CheckBox)sender;
            //int rowIndex = ((GridViewRow)myCheckBox.BindingContainer).RowIndex;
            //GridView myGridView = ((GridView)myCheckBox.DataKeysContainer);
            //int nextRowIndex = rowIndex + 1;
            //if (nextRowIndex != myGridView.Rows.Count)
            //{
            //    GridViewRow nextRow = myGridView.Rows[nextRowIndex];
            //    if (nextRow != null)
            //    {
            //        CheckBox chk = nextRow.Cells[7].Controls[1] as CheckBox;
            //        if (chk != null)
            //            chk.Focus();
            //    }
            //}
        }

    }
}