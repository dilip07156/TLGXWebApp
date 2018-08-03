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
using TLGX_Consumer.Controller;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.Service;
using System.Net;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class AccoMap : System.Web.UI.UserControl
    {
        MasterDataDAL masterdata = new MasterDataDAL();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        public DataTable dtCountryMappingSearchResults = new DataTable();
        public DataTable dtCountrMappingDetail = new DataTable();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        Controller.AccomodationSVC accoSVc = new Controller.AccomodationSVC();
        Controller.MasterDataSVCs _objMasterRef = new Controller.MasterDataSVCs();
        public static List<MDMSVC.DC_Accomodation_Search_RS> accoSuggRes = new List<MDMSVC.DC_Accomodation_Search_RS>();

        protected string contex = "<country>~<city>~<brand>~<chain>~<name>";
        public const string controlpath = "~/controls/staticdata/bulkHotelMapping.ascx";
        public const string controlID = "MyUserControl";

        public bool isDataExist = false;

        MasterDataSVCs _objMasterData = new MasterDataSVCs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();
                fillcountries(ddlCountry);
                fillcountries(ddlCountryName);
                fillmappingstatus(ddlMappingStatus);
                fillmasterstatus(ddlProductMappingStatus);
                fillchain(ddlChain);
                fillbrands(ddlBrand);
                fillMatchedBy(ddlMatchedBy);
                fillsuppliersproductType(string.Empty);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "javascript:callajax();", true);
                pnlLoadControl.Visible = false;
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
                //hdnContext.Value = contex;
                fillAccomodationPriority(ddlAccoPriority);
                fillAccomodationPriority(ddlAccoPrioritySearchByProd);
            }
        }

        private void fillsuppliersproductType(string supplierId)
        {
            ddlProductType.DataSource = _objMasterData.GetAllAttributeAndValues(new DC_MasterAttribute
            {
                MasterFor = "HotelInfo",
                Name = "ProductCategorySubType"
            });
            ddlProductType.DataValueField = "MasterAttributeValue_Id";
            ddlProductType.DataTextField = "AttributeValue";
            ddlProductType.DataBind();
        }

        private void fillmasterstatus(DropDownList ddl)
        {
            //ddl.DataSource = masterdata.getAllStatuses();
            ddl.DataSource = _objMasterData.GetAllStatuses();
            ddl.DataTextField = "Status_Name";
            ddl.DataValueField = "Status_Short";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText("ACTIVE"));
        }

        private void fillbrands(DropDownList ddl)
        {
            fillAttributeValues(ddl, "Brand", "HotelInfo");
        }

        private void fillchain(DropDownList ddl)
        {
            fillAttributeValues(ddl, "Chain", "HotelInfo");
        }
        private void fillmappingstatus(DropDownList ddl)
        {
            fillAttributeValues(ddl, "MappingStatus", "ProductSupplierMapping");
        }

        private void fillMatchedBy(DropDownList ddl)
        {
            fillAttributeValues(ddl, "MatchingPriority", "ProductSupplierMapping");

        }

        private void fillAttributeValues(DropDownList ddl, string Attribute_Name, string OptionFor)
        {
            MDMSVC.DC_M_masterattributelists list = LookupAtrributes.GetAllAttributeAndValuesByFOR(OptionFor, Attribute_Name);
            ddl.DataSource = list.MasterAttributeValues;

            if (Attribute_Name == "MatchingPriority")
            {
                ddl.DataTextField = "OTA_CodeTableValue";
                ddl.DataValueField = "AttributeValue";
                ddl.DataBind();

                if (list.MasterAttributeValues.Length > 0)
                {
                    MDMSVC.DC_M_masterattributevalue[] vals = list.MasterAttributeValues;

                    foreach (MDMSVC.DC_M_masterattributevalue val in vals)
                    {
                        if (!string.IsNullOrWhiteSpace(val.OTA_CodeTableValue) && ddl.Items.FindByValue(val.AttributeValue) != null)
                            ddl.Items.FindByValue(val.AttributeValue).Attributes.Add("title", val.AttributeValue);
                    }
                }
            }
            else
            {
                ddl.DataTextField = "AttributeValue";
                ddl.DataValueField = "MasterAttributeValue_Id";
                ddl.DataBind();
            }

        }
        private void fillcountries(DropDownList ddl)
        {
            //ddl.DataSource = masterdata.GetMasterCountryData("");
            //ddl.DataValueField = "Country_ID";
            //ddl.DataTextField = "Name";
            ddl.DataSource = _objMasterRef.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
        }

        public void fillcities(DropDownList ddl, DropDownList ddlp)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {

                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddl.DataSource = _objMasterRef.GetMasterCityData(ddlp.SelectedItem.Value);
                ddl.DataValueField = "City_Id";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = _objMasterData.GetSupplierMasterData();
            MDMSVC.DC_Supplier_Search_RQ RQ = new MDMSVC.DC_Supplier_Search_RQ();
            RQ.EntityType = "Accommodation";
            RQ.StatusCode = "ACTIVE";
            ddlSupplierName.DataSource = _objMasterData.GetSupplierByEntity(RQ);
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }
        public void fillproducts(DropDownList ddl, DropDownList ddlp, DropDownList ddlc)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {
                //ddl.DataSource = masterdata.GetProductByCity(ddlp.SelectedItem.Text, ddlc.SelectedItem.Text);
                ddl.DataSource = _objMasterRef.GetProductByCity(new MDMSVC.DC_Accomodation_DDL() { CityName = ddlp.SelectedItem.Text, CountryName = ddlc.SelectedItem.Text, IsActive = true });

                ddl.DataValueField = "Accommodation_Id";
                ddl.DataTextField = "HotelName";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        private void fillAccomodationPriority(DropDownList ddl)
        {
            MDMSVC.DC_M_masterattributelists list = LookupAtrributes.GetAllAttributeAndValuesByFOR("Accommodation", "Priority");

            try
            {
                list.MasterAttributeValues = list.MasterAttributeValues.OrderBy(x => Convert.ToInt32(x.AttributeValue)).ToArray();
            }
            catch (Exception ex)
            {

            }
            ddl.Items.Clear();
            ddl.DataSource = list.MasterAttributeValues;
            ddl.DataValueField = "AttributeValue";
            ddl.DataTextField = "OTA_CodeTableValue";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--ALL--", "0"));
        }

        //private static void AddSuperHeader(GridView gridView)
        //{
        //    var myTable = (Table)gridView.Controls[0];
        //    var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
        //    myNewRow.Cells.Add(MakeCell("Supplier", 10));
        //    myNewRow.Cells.Add(MakeCell("System", 6));
        //    myNewRow.Cells.Add(MakeCell("", 1));

        //    myTable.Rows.AddAt(0, myNewRow);
        //}

        //private static TableHeaderCell MakeCell(string text = null, int span = 1)
        //{
        //    return new TableHeaderCell() { ColumnSpan = span, Text = text ?? string.Empty, CssClass = "table-header" };
        //}

        protected void grdAccoMaps_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            //if (myGridView.Controls.Count > 0)
            //    AddSuperHeader(myGridView);
            if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                myGridView.Columns[11].Visible = true;
                myGridView.Columns[12].Visible = false;
                myGridView.Columns[16].Visible = true;
            }
            else if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                if (ddlSupplierCity.SelectedItem.Value != "0")
                {
                    myGridView.Columns[11].Visible = false;
                    myGridView.Columns[12].Visible = true;
                    myGridView.Columns[16].Visible = true;
                }
                else
                {
                    myGridView.Columns[11].Visible = true;
                    myGridView.Columns[12].Visible = false;
                    myGridView.Columns[16].Visible = false;
                }
            }
            else
            {
                myGridView.Columns[11].Visible = true;
                myGridView.Columns[12].Visible = false;
                myGridView.Columns[16].Visible = false;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities(ddlSupplierCity, ddlCountry);
            ddlSupplierCity.Focus();
        }

        protected void ddlSupplierCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillproducts(ddlProduct, ddlSupplierCity, ddlCountry);
            ddlProduct.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //PageIndex = 0;
            //SortBy = "HotelName";
            //SortEx = "";
            //via = "supplier";
            bool _isDataExist = false;
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");

            fillproductdata(ref _isDataExist, "supplier", 0);
            if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                btnMapSelected.Visible = _isDataExist;
                btnMapAll.Visible = _isDataExist;
            }
            else if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                if (ddlCountry.SelectedValue == "0" && ddlSupplierCity.SelectedValue == "0")
                {
                    btnMapSelected.Visible = false;
                    btnMapAll.Visible = false;
                }
                else
                {
                    btnMapSelected.Visible = _isDataExist;
                    btnMapAll.Visible = _isDataExist;
                }
            }
            else
            {
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
            }
        }

        
        private void fillproductdata(ref bool isDataExist, string pVia, int pPageIndex)
        {
            MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQ = new MDMSVC.DC_Mapping_ProductSupplier_Search_RQ();

            if (pVia == "supplier")
            {
                if (ddlSupplierName.SelectedItem.Value != "0")
                {
                    RQ.SupplierName = ddlSupplierName.SelectedItem.Text;
                    RQ.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value);
                }
                //if (ddlCountry.SelectedItem.Value != "0")
                //    RQ.CountryName = ddlCountry.SelectedItem.Text;
                if (ddlCountry.SelectedItem.Value != "0")
                    RQ.Country_Id = Guid.Parse(ddlCountry.SelectedItem.Value);

                if (ddlSupplierCity.SelectedItem.Value != "0")
                {
                    RQ.CityName = ddlSupplierCity.SelectedItem.Text;
                    RQ.City_Id = Guid.Parse(ddlSupplierCity.SelectedItem.Value);
                }

                if (ddlProduct.SelectedItem.Value != "0")
                    RQ.ProductName = ddlProduct.SelectedItem.Text;

                if (ddlMappingStatus.SelectedItem.Value != "0")
                    RQ.Status = ddlMappingStatus.SelectedItem.Text;

                RQ.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);

                if (!string.IsNullOrWhiteSpace(txtSuppCountry.Text))
                    RQ.SupplierCountryName = txtSuppCountry.Text;

                if (!string.IsNullOrWhiteSpace(txtSuppCity.Text))
                    RQ.SupplierCityName = txtSuppCity.Text;

                if (!string.IsNullOrWhiteSpace(txtSuppProduct.Text))
                    RQ.SupplierProductName = txtSuppProduct.Text;

                if (ddlMatchedBy.SelectedItem.Value != "99")
                    RQ.MatchedBy = Convert.ToInt32(ddlMatchedBy.SelectedValue);

                if (ddlProductType.SelectedValue != "0")
                {
                    RQ.ProductType = Convert.ToString(ddlProductType.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(txtSuppProdCode.Text))
                {
                    RQ.SupplierProductCode = txtSuppProdCode.Text.Trim();
                }
                if(ddlAccoPriority.SelectedValue != "0")
                {
                    RQ.Priority = Convert.ToInt32(ddlAccoPriority.SelectedValue);
                }
                    

                RQ.Source = "SYSTEMDATA";
            }
            else
            {


                if (ddlCountryName.SelectedItem.Value != "0")
                    RQ.CountryName = ddlCountryName.SelectedItem.Text;

                if (ddlCity.SelectedItem.Value != "0")
                    RQ.CityName = ddlCity.SelectedItem.Text;

                if (txtSearch.Text != "")
                {
                    string str = txtSearch.Text;
                    int ix1 = str.LastIndexOf(',');
                    int ix2 = ix1 > 0 ? str.LastIndexOf(',', ix1 - 1) : -1;
                    RQ.ProductName = str.Substring(0, ix2);
                }

                if (ddlProductMappingStatus.SelectedItem.Value != "0")
                    RQ.Status = ddlProductMappingStatus.SelectedItem.Text;

                if (ddlChain.SelectedItem.Value != "0")
                    RQ.Chain = ddlChain.SelectedItem.Text;

                if (ddlBrand.SelectedItem.Value != "0")
                    RQ.Brand = ddlBrand.SelectedItem.Text;

                if (ddlMatchedBy.SelectedItem.Value != "99")
                    RQ.MatchedBy = Convert.ToInt32(ddlMatchedBy.SelectedValue);

                RQ.PageSize = Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Text);
            }
            RQ.PageNo = pPageIndex;
            //RQ.SortBy = (SortBy + " " + SortEx).Trim();

            var res = mapperSVc.GetProductMappingData(RQ);
            grdAccoMaps.Visible = true;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    isDataExist = true;
                    grdAccoMaps.VirtualItemCount = res[0].TotalRecords;
                    lblAccoMaps.Text = res[0].TotalRecords.ToString();
                    //To handle last page index after status changes
                    pPageIndex = res[0].PageIndex;
                }
                else
                {
                    lblAccoMaps.Text = "0";
                    isDataExist = false;
                }
            }
            else
            {
                lblAccoMaps.Text = "0";
                isDataExist = false;
            }
            grdAccoMaps.DataSource = res;
            grdAccoMaps.PageIndex = pPageIndex;
            if (pVia == "supplier")
                grdAccoMaps.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
            else
                grdAccoMaps.PageSize = Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Text);
            //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
            grdAccoMaps.DataBind();
        }

        protected void grdAccoMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                if (e.CommandName == "Select")
                {
                    hdnIsAnyChanges.Value = "false";
                    UpdateSupplierProductMapping.CalledFrom = "APM";
                    UpdateSupplierProductMapping.Accommodation_ProductMapping_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                    UpdateSupplierProductMapping.SupplierId = grdAccoMaps.Rows[index].Cells[1].Text;
                    UpdateSupplierProductMapping.SupplierName = grdAccoMaps.Rows[index].Cells[1].Text;
                    UpdateSupplierProductMapping.ProductId = grdAccoMaps.Rows[index].Cells[2].Text;
                    UpdateSupplierProductMapping.ProductName = grdAccoMaps.Rows[index].Cells[3].Text;
                    UpdateSupplierProductMapping.ProductType = grdAccoMaps.Rows[index].Cells[4].Text;
                    UpdateSupplierProductMapping.Street = grdAccoMaps.Rows[index].Cells[5].Text;
                    UpdateSupplierProductMapping.TelephoneNumber = grdAccoMaps.Rows[index].Cells[6].Text;
                    UpdateSupplierProductMapping.CountryCode = grdAccoMaps.Rows[index].Cells[7].Text;
                    UpdateSupplierProductMapping.CountryName = grdAccoMaps.Rows[index].Cells[8].Text;
                    UpdateSupplierProductMapping.CityCode = grdAccoMaps.Rows[index].Cells[9].Text;
                    UpdateSupplierProductMapping.CityName = grdAccoMaps.Rows[index].Cells[10].Text;
                    UpdateSupplierProductMapping.Status = grdAccoMaps.Rows[index].Cells[14].Text;
                   

                    if (grdAccoMaps.DataKeys[index].Values[1] != null)
                    {
                        UpdateSupplierProductMapping.Accommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString());
                    }
                    UpdateSupplierProductMapping.BindData();
                }
                //hdnFlag.Value = "false";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showCityMappingModal();", true);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void fillStates(DropDownList ddlSystemCountryName, DropDownList ddlSystemStateName)
        {
            ddlSystemStateName.Items.Clear();
            if (ddlSystemCountryName.SelectedItem.Value != "0")
            {
                MDMSVC.DC_State_Search_RQ RQ = new MDMSVC.DC_State_Search_RQ();
                RQ.Country_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value);

                var res = _objMasterRef.GetStatesMaster(RQ);
                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddlSystemStateName.DataSource = res;
                ddlSystemStateName.DataValueField = "State_Id";
                ddlSystemStateName.DataTextField = "State_Name";
                ddlSystemStateName.DataBind();
            }
            ddlSystemStateName.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void grdAccoMaps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            //PageIndex = e.NewPageIndex;
            hdnPageNumber.Value = e.NewPageIndex.ToString();
            fillproductdata(ref isDataExist, "supplier", Convert.ToInt32(hdnPageNumber.Value ?? "0"));
        }

        protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities(ddlCity, ddlCountryName);
            ddlCity.Focus();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fillproducts(ddlProductName, ddlCity, ddlCountryName);
            ddlChain.Focus();
        }

        public void CallHotelSearch(int pageSize, out MDMSVC.DC_Accomodation_Search_RQ RQ, int pPageIndex)
        {
            RQ = new MDMSVC.DC_Accomodation_Search_RQ();
            RQ.ProductCategory = "Accommodation";
            RQ.ProductCategorySubType = "Hotel"; //txtProdCategoryt.Text;
            if (ddlProductMappingStatus.SelectedValue != "0")
                RQ.Status = ddlProductMappingStatus.SelectedItem.Text;

            if (txtSearch.Text != "")
            {
                string str = txtSearch.Text;
                int ix1 = str.LastIndexOf(',');
                int ix2 = ix1 > 0 ? str.LastIndexOf(',', ix1 - 1) : -1;
                RQ.HotelName = str.Substring(0, ix2);
            }
            if (ddlCountryName.SelectedItem.Value != "0")
                RQ.Country = ddlCountryName.SelectedItem.Text;
            if (ddlCity.SelectedItem.Value != "0")
                RQ.City = ddlCity.SelectedItem.Text;
            if (ddlChain.SelectedItem.Value != "0")
                RQ.Chain = ddlChain.SelectedItem.Text;
            if (ddlBrand.SelectedItem.Value != "0")
                RQ.Brand = ddlBrand.SelectedItem.Text;
            if (!string.IsNullOrWhiteSpace(txtCompanyHotelId.Text))
                RQ.CompanyHotelId = Convert.ToInt32(txtCompanyHotelId.Text.Trim());
            if (!string.IsNullOrWhiteSpace(txtTLGXAccoId.Text))
                RQ.TLGXAccoId = txtTLGXAccoId.Text.Trim();
            if (ddlAccoPrioritySearchByProd.SelectedValue != "0")
            {
                RQ.Priority = Convert.ToInt32(ddlAccoPrioritySearchByProd.SelectedValue);
            }

            RQ.PageNo = pPageIndex;
            RQ.PageSize = pageSize;
        }

        public void fillHotelSearchGrid(int pageSize, int pPageIndex)
        {
            CallHotelSearch(pageSize, out RQParams, pPageIndex);
            var res = AccSvc.SearchHotels(RQParams);
            grdTLGXProdData.Visible = true;
            grdTLGXProdData.DataSource = res;
            if (res != null && res.Count > 0)
            {
                grdTLGXProdData.VirtualItemCount = res[0].TotalRecords;
                lblTLGXProdData.Text = res[0].TotalRecords.ToString();
            }
            else
                lblTLGXProdData.Text = "0";
            grdTLGXProdData.PageIndex = pPageIndex;
            grdTLGXProdData.PageSize = pageSize;
            grdTLGXProdData.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //PageIndex = 0;
            //SortBy = "HotelName";
            //SortEx = "";
            //via = "product";
            //fillproductdata();
            //PageIndex = 0;
            fillHotelSearchGrid(Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Value), 0);
            pnlLoadControl.Visible = false;
        }

        
       
       

        

       

        protected void grdTLGXProdData_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdTLGXProdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //Guid myRow_Id = Guid.Parse(grdTLGXProdData.DataKeys[index].Value.ToString());
                pnlLoadControl.Visible = true;
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                //dataGridView.Rows[4].Cells["Name"].Value.ToString();

                if (myRow_Id != null)
                {
                    string supCountry = grdTLGXProdData.Rows[0].Cells[1].Text.ToString();
                    string supCity = grdTLGXProdData.Rows[0].Cells[2].Text.ToString();
                    string supHotel = grdTLGXProdData.Rows[0].Cells[3].Text.ToString();
                    bulkHotelMapping.Visible = true;
                    bulkHotelMapping.fillproductmappingdata(myRow_Id, grdTLGXProdData.PageIndex, supCountry, supCity, supHotel);

                    //bulkHotelMapping ucbulkHotelMapping = LoadControl(controlpath) as bulkHotelMapping;
                    //ucbulkHotelMapping.Accomodation_ID = myRow_Id;

                    //pnlLoadControl.Controls.Add(ucbulkHotelMapping);
                    //Session.Add((Session.Count + 1).ToString(), ucbulkHotelMapping);
                    //createAgain = true;
                }
            }
        }

        protected void grdTLGXProdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //PageIndex = e.NewPageIndex;
            fillHotelSearchGrid(Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Value), e.NewPageIndex);
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public List<string> GetProductListAutoComplete(string prefixText)
        {
            MDMSVC.DC_Accomodation_Search_RQ RQNames = new MDMSVC.DC_Accomodation_Search_RQ();
            CallHotelSearch(500, out RQNames, 0);
            var res = AccSvc.GetAccomodationNames(RQNames);

            return res;
        }

        protected void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            ResetAllPageDataTab2();
        }

        private void ResetAllPageDataTab2()
        {
            try
            {
                ddlCountryName.SelectedIndex = 0;
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("-select-", "0"));
                ddlCity.SelectedIndex = 0;
                ddlChain.SelectedIndex = 0;
                ddlBrand.SelectedIndex = 0;
                ddlMatchedBy.SelectedIndex = 0;
                txtSearch.Text = "";
                ddlProductMappingStatus.SelectedIndex = 0;
                ddlProductBasedPageSize.SelectedIndex = 0;
                //Clear remaining page data
                //1. Button Hide
                Button3.Visible = false;
                //2. Search Result Count
                lblTLGXProdData.Text = "0";
                //3. Grid Hide and bind with  null
                grdTLGXProdData.Visible = false;
                bulkHotelMapping.Visible = false;
                txtCompanyHotelId.Text = string.Empty;
                txtTLGXAccoId.Text = string.Empty;
                ddlAccoPrioritySearchByProd.SelectedIndex = 0;

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllPageData();
        }

        private void ResetAllPageData()
        {
            try
            {
                ddlSupplierName.SelectedIndex = 0;
                ddlCountry.SelectedIndex = 0;
                ddlSupplierCity.Items.Clear();
                ddlSupplierCity.Items.Insert(0, new ListItem("-select-", "0"));
                ddlSupplierCity.SelectedIndex = 0;
                ddlProduct.SelectedIndex = 0;
                ddlMappingStatus.SelectedIndex = 0;
                ddlPageSize.SelectedIndex = 0;
                txtSuppCountry.Text = "";
                txtSuppCity.Text = "";
                txtSuppProduct.Text = "";
                //Clear remaining page data
                //1. Button Hide
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
                //2. Search Result Count
                lblAccoMaps.Text = "0";
                //3. Grid Hide and bind with  null
                grdAccoMaps.Visible = false;
                txtSuppProdCode.Text = string.Empty;
                ddlAccoPriority.SelectedIndex = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grdTLGXProdData_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdTLGXProdData.Rows)
            {
                if (row.RowIndex == grdTLGXProdData.SelectedIndex)
                {
                    row.BackColor = System.Drawing.Color.DarkTurquoise;
                }
                else
                {
                    row.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }

        protected void btnMapSelected_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid myAccommodation_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdAccoMaps.Rows)
            {
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    HtmlInputCheckBox chk = row.Cells[16].Controls[1] as HtmlInputCheckBox;
                    //CheckBox chk = row.Cells[15].Controls[1] as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        myAccommodation_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                        myAccommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString());
                        mySupplier_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[2].ToString());
                    }
                }
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    HtmlInputCheckBox chk = row.Cells[16].Controls[1] as HtmlInputCheckBox;
                    //CheckBox chk = row.Cells[15].Controls[1] as CheckBox;
                    DropDownList ddl = row.Cells[12].Controls[1] as DropDownList;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        myAccommodation_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                        HiddenField hdnAccp_Id = row.Cells[17].Controls[1] as HiddenField; //Set value from ajax changes

                        if (!string.IsNullOrWhiteSpace(hdnAccp_Id.Value) && hdnAccp_Id.Value != "0")
                        {
                            if (hdnAccp_Id != null)
                                myAccommodation_Id = Guid.Parse(hdnAccp_Id.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                myAccommodation_Id = Guid.Parse(ddl.SelectedItem.Value);
                        }
                        //if (ddl != null)
                        //    myAccommodation_Id = Guid.Parse(ddl.SelectedItem.Value);
                        mySupplier_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[2].ToString());
                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Accomodation_ProductMapping param = new MDMSVC.DC_Accomodation_ProductMapping();
                    param.Accommodation_ProductMapping_Id = myRow_Id;
                    param.Accommodation_Id = myAccommodation_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    param.Status = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    res = mapperSVc.UpdateProductMappingData(RQ);
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, "Record mapped successfully", BootstrapAlertType.Success);
                    myRow_Id = Guid.Empty;
                    myAccommodation_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                }

            }
            fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
        }

        protected void btnMapAll_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid myAccommodation_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdAccoMaps.Rows)
            {
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    myRow_Id = Guid.Empty;
                    myAccommodation_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                    myAccommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString());
                    mySupplier_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[2].ToString());
                }
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    HtmlInputCheckBox chk = row.Cells[16].Controls[1] as HtmlInputCheckBox;
                    //CheckBox chk = row.Cells[15].Controls[1] as CheckBox;
                    DropDownList ddl = row.Cells[12].Controls[1] as DropDownList;
                    HiddenField hdnAccp_Id = row.Cells[17].Controls[1] as HiddenField; //Set value from ajax changes
                    if (ddl.SelectedItem.Value != "0" || !string.IsNullOrWhiteSpace(hdnAccp_Id.Value))
                    {
                        myRow_Id = Guid.Empty;
                        myAccommodation_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                        if (!string.IsNullOrWhiteSpace(hdnAccp_Id.Value) && hdnAccp_Id.Value != "0")
                        {
                            if (hdnAccp_Id != null)
                                myAccommodation_Id = Guid.Parse(hdnAccp_Id.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                myAccommodation_Id = Guid.Parse(ddl.SelectedItem.Value);
                        }
                        //if (ddl != null)
                        //    myAccommodation_Id = Guid.Parse(ddl.SelectedItem.Value);
                        mySupplier_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[2].ToString());
                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Accomodation_ProductMapping param = new MDMSVC.DC_Accomodation_ProductMapping();
                    param.Accommodation_ProductMapping_Id = myRow_Id;
                    param.Accommodation_Id = myAccommodation_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    param.Status = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    res = mapperSVc.UpdateProductMappingData(RQ);
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, "Record mapped successfully", BootstrapAlertType.Success);
                    myRow_Id = Guid.Empty;
                    myAccommodation_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                }
            }
            fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void grdAccoMaps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var myGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "onMouseOver('" + (e.Row.RowIndex + 1) + "')";
                e.Row.Attributes["onmouseout"] = "onMouseOut('" + (e.Row.RowIndex + 1) + "')";
                int index = e.Row.RowIndex;
                Guid myCountryId = Guid.Empty;
                string CountryName = e.Row.Cells[8].Text.ToUpper();
                string CityName = e.Row.Cells[10].Text.ToUpper();
                MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
                DropDownList ddl = e.Row.Cells[12].Controls[1] as DropDownList;
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    if (ddlSupplierCity.SelectedItem.Value != "0")
                    {
                        if (ddl != null)
                        {
                            var mstAcco_Id = Convert.ToString(grdAccoMaps.DataKeys[index].Values[3]);
                            var mstHotelName = Convert.ToString(grdAccoMaps.DataKeys[index].Values[4]);
                            if (!string.IsNullOrWhiteSpace(mstAcco_Id) && !string.IsNullOrWhiteSpace(mstHotelName))
                            {
                                ddl.Items.Add(new ListItem(mstHotelName, mstAcco_Id));
                                if (ddl.Items.FindByValue(mstAcco_Id) != null)
                                    ddl.Items.FindByValue(mstAcco_Id).Selected = true;
                            }
                        }
                    }
                    //{
                    //    if (index == 0)
                    //    {
                    //        accoSuggRes = null;
                    //        RQParams.ProductCategory = "Accommodation";
                    //        RQParams.ProductCategorySubType = "Hotel";
                    //        RQParams.Status = "ACTIVE";
                    //        if (!string.IsNullOrWhiteSpace(CountryName))
                    //        {
                    //            if (CountryName != "&NBSP;")
                    //                RQParams.Country = CountryName.ToString();
                    //        }
                    //        if (!string.IsNullOrWhiteSpace(CityName))
                    //        {
                    //            if (CityName != "&NBSP;")
                    //                RQParams.City = CityName.ToString();
                    //        }
                    //        RQParams.PageNo = 0;
                    //        RQParams.PageSize = int.MaxValue;
                    //accoSuggRes = accoSVc.SearchHotels(RQParams);
                    //    }

                    //    if (accoSuggRes.Count > 0)
                    //    {
                    //        ddl.DataSource = accoSuggRes;
                    //        ddl.DataValueField = "AccomodationId";
                    //        ddl.DataTextField = "HotelName";
                    //        ddl.DataBind();
                    //    }
                }

                //string prodcode = e.Row.Cells[2].Text.ToUpper();
                //string prodname = e.Row.Cells[3].Text.ToUpper();

                //if (!string.IsNullOrWhiteSpace(prodname) && accoSuggRes != null)
                //{
                //    if (prodname.ToUpper() != "&NBSP;" && prodname != "")
                //    {
                //        IEnumerable<MDMSVC.DC_Accomodation_Search_RS> querynamed = accoSuggRes.Where(prod => (prod.HotelName ?? string.Empty).Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("hotel", "").ToUpper().Equals(prodname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("hotel", "").ToUpper()));
                //        if (querynamed.Count() > 0)
                //        {
                //            foreach (MDMSVC.DC_Accomodation_Search_RS resc in querynamed)
                //            {
                //                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.AccomodationId.ToString()));
                //            }
                //        }
                //        if (ddl.SelectedIndex == 0)
                //        {
                //            IEnumerable<MDMSVC.DC_Accomodation_Search_RS> queryname = accoSuggRes.Where(prod => (prod.HotelName ?? string.Empty).Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("hotel", "").ToUpper().Contains(prodname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("hotel", "").ToUpper()));
                //            if (queryname.Count() > 0)
                //            {
                //                foreach (MDMSVC.DC_Accomodation_Search_RS resc in queryname)
                //                {
                //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.AccomodationId.ToString()));
                //                }
                //            }
                //        }
                //    }
                //    if (!string.IsNullOrWhiteSpace(prodcode))
                //    {
                //        if (prodcode.ToUpper() != "&NBSP;" && prodcode != "")
                //        {
                //            if (ddl.SelectedIndex == 0)
                //            {
                //                IEnumerable<MDMSVC.DC_Accomodation_Search_RS> queryname = accoSuggRes.Where(prod => (prod.HotelName ?? string.Empty).ToUpper().Equals(prodcode));
                //                if (queryname.Count() > 0)
                //                {
                //                    foreach (MDMSVC.DC_Accomodation_Search_RS resc in queryname)
                //                    {
                //                        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.AccomodationId.ToString()));
                //                    }
                //                }
                //            }
                //            if (ddl.SelectedIndex == 0)
                //            {
                //                IEnumerable<MDMSVC.DC_Accomodation_Search_RS> queryname = accoSuggRes.Where(prod => (prod.CompanyHotelId ?? string.Empty).ToUpper().Equals(prodcode));
                //                if (queryname.Count() > 0)
                //                {
                //                    foreach (MDMSVC.DC_Accomodation_Search_RS resc in queryname)
                //                    {
                //                        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(resc.AccomodationId.ToString()));
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }
        }
        
       

        protected void ddlAddCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdTLGXProdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var myGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text != "Active")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }

       

       

        

        //protected Control GetPostBackControl(System.Web.UI.Control page)
        //{
        //    Control control = null;
        //    try
        //    {
        //        string ctrlName = page.Request.Params.Get("__EVENTTARGET");

        //        if (ctrlName != null && ctrlName != String.Empty)
        //        {
        //            control = page.FindControl(ctrlName);
        //        }
        //        else
        //        {
        //            ContentPlaceHolder cph =
        //              (ContentPlaceHolder)page.FindControl("Main");
        //            for (int i = 0, len = page.Request.Form.Count; i < len; i++)
        //            {
        //                string[] ctl = page.Request.Form.AllKeys[i].Split('$');
        //                if (ctl.Length > 2)
        //                {
        //                    control = cph.FindControl(ctl[2])
        //                              as System.Web.UI.WebControls.Button;
        //                }

        //                if (control != null) break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return control;
        //}


        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    //base.OnInit(e);

        //    Control control = GetPostBackControl(this.Parent);

        //    // Check if the postback is caused by the button 
        //    // Titled "Click to Create a Dynamic Control"
        //    // OR
        //    // createAgain field is true 
        //    // which means a call is made to the server while the 
        //    // user control is active  

        //    if ((control != null && control.ClientID ==
        //                    grdTLGXProdData.ClientID) || createAgain)
        //    {
        //        //should be set before the CreateUserControl method
        //        createAgain = true;

        //        CreateUserControl(controlID);
        //    }
        //}

        //protected void CreateUserControl(string controlID)
        //{
        //    // createAgain field is set to true in the OnPreInit method
        //    // when the 'Create User Control' button is clicked 

        //    // the createAgain field is used to check if the
        //    // user control is on the page before the call 
        //    // if so create the control again and add it to the
        //    // Control Hierarchy again
        //    try
        //    {
        //        if (createAgain && pnlLoadControl != null)
        //        {
        //            if (Session.Count > 0)
        //            {
        //                pnlLoadControl.Controls.Clear();
        //                for (int i = 0; i < Session.Count; i++)
        //                {
        //                    switch (Session[i].ToString())
        //                    {
        //                        case "ASP.bulkHotelMapping.ascx":
        //                            {
        //                                // Create instance of the UserControl SimpleControl
        //                                bulkHotelMapping ucSimpleControl =
        //                                  LoadControl(controlpath)
        //                                  as bulkHotelMapping;

        //                                //// Set the Public Properties
        //                                //ucSimpleControl.FirstName.Text =
        //                                //  ((usercontrols_SimpleControl)(Session[i])).FirstName.Text;
        //                                //ucSimpleControl.LastName.Text =
        //                                //  ((usercontrols_SimpleControl)(Session[i])).LastName.Text;

        //                                ////Create Event Handler for btnPost Click 
        //                                //ucSimpleControl.btnPostClk +=
        //                                //  new btnPost_Click(ucSimpleControl_btnPostClk);

        //                                //Add the SimpleControl to Placeholder
        //                                pnlLoadControl.Controls.Add(ucSimpleControl);
        //                                break;
        //                            }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void GridRefersh(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(hdnIsAnyChanges.Value))
            {
                bool _isDataExist = false;
                divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");

                fillproductdata(ref _isDataExist, "supplier", Convert.ToInt32(hdnPageNumber.Value ?? "0"));
                if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    btnMapSelected.Visible = _isDataExist;
                    btnMapAll.Visible = _isDataExist;
                }
                else if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    if (ddlCountry.SelectedValue == "0" && ddlSupplierCity.SelectedValue == "0")
                    {
                        btnMapSelected.Visible = false;
                        btnMapAll.Visible = false;
                    }
                    else
                    {
                        btnMapSelected.Visible = _isDataExist;
                        btnMapAll.Visible = _isDataExist;
                    }
                }
                else
                {
                    btnMapSelected.Visible = false;
                    btnMapAll.Visible = false;
                }
            }
        }
    }
}