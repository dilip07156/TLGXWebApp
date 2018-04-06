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
        //Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        public DataTable dtCountryMappingSearchResults = new DataTable();
        public DataTable dtCountrMappingDetail = new DataTable();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        Controller.AccomodationSVC accoSVc = new Controller.AccomodationSVC();
        Controller.MasterDataSVCs _objMasterRef = new Controller.MasterDataSVCs();
        public static List<MDMSVC.DC_Accomodation_Search_RS> accoSuggRes = new List<MDMSVC.DC_Accomodation_Search_RS>();

        // MasterDataDAL masters = new MasterDataDAL();
        //public static string AttributeOptionFor = "ProductSupplierMapping";
        //public static string AttributeOptionFor1 = "HotelInfo";
        // public static string SortBy = "";
        //public static string SortEx = "";
        //public static string via = "";
        //public static int PageIndex = 0;
        protected string contex = "<country>~<city>~<brand>~<chain>~<name>";
        public const string controlpath = "~/controls/staticdata/bulkHotelMapping.ascx";
        public const string controlID = "MyUserControl";
        //public static bool createAgain = false;
        //public static string MatchedStatus = "";
        //public static string MatchedCountryName = "";
        //public static string MatchedCityName = "";
        //public static string MatchedProdName = "";
        //public static int MatchedPageIndex = 0;
        //public static Guid? MappedCountry_ID = Guid.Empty;
        //public static Guid? MappedCity_ID = Guid.Empty;
        //public static Guid? MappedProduct_ID = Guid.Empty;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "javascript:callajax();", true);
                pnlLoadControl.Visible = false;
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
                //hdnContext.Value = contex;
            }
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
                myGridView.Columns[10].Visible = true;
                myGridView.Columns[11].Visible = false;
                myGridView.Columns[15].Visible = true;
            }
            else if (ddlMappingStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                if (ddlSupplierCity.SelectedItem.Value != "0")
                {
                    myGridView.Columns[10].Visible = false;
                    myGridView.Columns[11].Visible = true;
                    myGridView.Columns[15].Visible = true;
                }
                else
                {
                    myGridView.Columns[10].Visible = true;
                    myGridView.Columns[11].Visible = false;
                    myGridView.Columns[15].Visible = false;
                }
            }
            else
            {
                myGridView.Columns[10].Visible = true;
                myGridView.Columns[11].Visible = false;
                myGridView.Columns[15].Visible = false;
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

        private void fillmatchingdata(string from, int pPageIndex)
        {
            Label lblSupCountryName = (Label)frmEditProductMap.FindControl("lblSupCountryName");
            Label lblCityName = (Label)frmEditProductMap.FindControl("lblCityName");
            Label lblProductName = (Label)frmEditProductMap.FindControl("lblProductName");
            Label lblHotelName_TX = (Label)frmEditProductMap.FindControl("lblHotelName_TX");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            dvMsg.Style.Add("display", "none");
            MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQ = new MDMSVC.DC_Mapping_ProductSupplier_Search_RQ();
            if (ddlMatchingStatus.SelectedItem.Value != "0")
                RQ.Status = ddlMatchingStatus.SelectedItem.Text;
            //if (MatchedCountryName.Trim() != "")
            //    RQ.CountryName = MatchedCountryName.Trim();
            //if (MatchedCityName.Trim() != "")
            //    RQ.CityName = MatchedCityName.Trim();
            //if (MatchedProdName.Trim() != "")
            //    RQ.ProductName = MatchedProdName.Trim();
            if (!string.IsNullOrWhiteSpace(lblSupCountryName.Text))
                RQ.CountryName = lblSupCountryName.Text;
            if (ddlCountry.SelectedItem.Value != "0")
                RQ.Country_Id = Guid.Parse(ddlCountry.SelectedItem.Value);
            if (!string.IsNullOrWhiteSpace(lblCityName.Text))
                RQ.CityName = lblCityName.Text;
            if (!string.IsNullOrWhiteSpace(lblProductName.Text))
                RQ.SupplierProductName = lblProductName.Text;
            if (!string.IsNullOrWhiteSpace(lblHotelName_TX.Text))
                RQ.HotelName_TX = lblHotelName_TX.Text;
            RQ.Via = "CROSS";
            RQ.StatusExcept = ddlStatus.SelectedItem.Text.Trim().ToUpper();
            RQ.PageNo = pPageIndex;
            RQ.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            var res = mapperSVc.GetProductMappingData(RQ);
            grdMatchingProducts.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    if (from != "status")
                    {
                        ddlMatchingStatus.Items.Clear();
                        var disstatus = (from s in res where s.Status != null orderby s.Status select s.Status).Distinct();
                        ddlMatchingStatus.DataSource = disstatus;
                        ddlMatchingStatus.DataBind();
                        ddlMatchingStatus.Items.Insert(0, new ListItem("--ALL--", "0"));
                    }
                    grdMatchingProducts.VirtualItemCount = res[0].TotalRecords;
                    if (res[0].TotalRecords > 0)
                    {
                        lblMsg.Text = "There are (" + res[0].TotalRecords.ToString() + ") similar records in system matching with mapped combination. Do you wish to map the same? ";
                    }
                    else
                    {
                        lblMsg.Text = "No similar records found matching with updated combination";
                    }
                }
                else
                {
                    lblMsg.Text = "No similar records found matching with updated combination";
                }
            }
            else
            {
                lblMsg.Text = "No similar records found matching with updated combination";
            }
            grdMatchingProducts.PageIndex = pPageIndex;
            grdMatchingProducts.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            grdMatchingProducts.DataBind();
        }
        private void fillproductdata(ref bool isDataExist, string pVia, int pPageIndex)
        {
            MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQ = new MDMSVC.DC_Mapping_ProductSupplier_Search_RQ();

            if (pVia == "supplier")
            {
                if (ddlSupplierName.SelectedItem.Value != "0")
                {
                    RQ.SupplierName = ddlSupplierName.SelectedItem.Text;
                }
                //if (ddlCountry.SelectedItem.Value != "0")
                //    RQ.CountryName = ddlCountry.SelectedItem.Text;
                if (ddlCountry.SelectedItem.Value != "0")
                    RQ.Country_Id = Guid.Parse(ddlCountry.SelectedItem.Value);
                if (ddlSupplierCity.SelectedItem.Value != "0")
                    RQ.CityName = ddlSupplierCity.SelectedItem.Text;
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
                    dvAddProduct.Visible = false;
                    dvMsg.Style.Add("display", "none");
                    grdMatchingProducts.DataSource = null;
                    grdMatchingProducts.DataBind();
                    dvMatchingRecords.Visible = false;
                    List<MDMSVC.DC_Accomodation_ProductMapping> obj = new List<MDMSVC.DC_Accomodation_ProductMapping>();
                    obj.Add(new MDMSVC.DC_Accomodation_ProductMapping
                    {
                        Accommodation_ProductMapping_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString()),
                        SupplierId = grdAccoMaps.Rows[index].Cells[1].Text,
                        SupplierName = grdAccoMaps.Rows[index].Cells[1].Text,
                        ProductId = grdAccoMaps.Rows[index].Cells[2].Text,
                        ProductName = grdAccoMaps.Rows[index].Cells[3].Text,
                        Street = grdAccoMaps.Rows[index].Cells[4].Text,
                        TelephoneNumber = grdAccoMaps.Rows[index].Cells[5].Text,
                        CountryCode = grdAccoMaps.Rows[index].Cells[6].Text,
                        CountryName = grdAccoMaps.Rows[index].Cells[7].Text,
                        CityCode = grdAccoMaps.Rows[index].Cells[8].Text,
                        CityName = grdAccoMaps.Rows[index].Cells[9].Text,
                        Status = grdAccoMaps.Rows[index].Cells[13].Text
                    });

                    if (grdAccoMaps.DataKeys[index].Values[1] != null)
                    {
                        obj[0].Accommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString());
                    }

                    frmEditProductMap.Visible = true;
                    frmEditProductMap.ChangeMode(FormViewMode.Edit);
                    frmEditProductMap.DataSource = obj;
                    frmEditProductMap.DataBind();

                    #region GetControls
                    Label lblSupplierName = (Label)frmEditProductMap.FindControl("lblSupplierName");
                    Label lblSupplierCode = (Label)frmEditProductMap.FindControl("lblSupplierCode");
                    Label lblSupCountryName = (Label)frmEditProductMap.FindControl("lblSupCountryName");
                    Label lblSupCountryCode = (Label)frmEditProductMap.FindControl("lblSupCountryCode");
                    Label lblSupStateName = (Label)frmEditProductMap.FindControl("lblSupStateName");
                    Label lblSupStateCode = (Label)frmEditProductMap.FindControl("lblSupStateCode");
                    Label lblCityName = (Label)frmEditProductMap.FindControl("lblCityName");
                    Label lblCityCode = (Label)frmEditProductMap.FindControl("lblCityCode");
                    Label lblProductName = (Label)frmEditProductMap.FindControl("lblProductName");
                    Label lblProductCode = (Label)frmEditProductMap.FindControl("lblProductCode");
                    Label lblProductAddress = (Label)frmEditProductMap.FindControl("lblProductAddress");
                    Label lblHotelName_TX = (Label)frmEditProductMap.FindControl("lblHotelName_TX");

                    DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
                    TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
                    Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");

                    DropDownList ddlSystemStateName = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
                    TextBox txtSystemStateCode = (TextBox)frmEditProductMap.FindControl("txtSystemStateCode");
                    Label lblSystemStateCode = (Label)frmEditProductMap.FindControl("lblSystemStateCode");


                    DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
                    TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");
                    Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");
                    DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
                    TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
                    Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");

                    DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
                    TextBox txtSystemRemark = (TextBox)frmEditProductMap.FindControl("txtSystemRemark");
                    Button btnAddProduct = (Button)frmEditProductMap.FindControl("btnAddProduct");
                    TextBox txtAddCityPlaceId = (TextBox)ucAddNew.FindControl("txtAddCityPlaceId");
                    TextBox txtHotelName = (TextBox)ucAddNew.FindControl("txtHotelName");
                    TextBox txtStreet = (TextBox)ucAddNew.FindControl("txtStreet");
                    TextBox txtStreet2 = (TextBox)ucAddNew.FindControl("txtStreet2");
                    TextBox txtPostalCode = (TextBox)ucAddNew.FindControl("txtPostalCode");
                    DropDownList ddlAddCountry = (DropDownList)ucAddNew.FindControl("ddlAddCountry");
                    DropDownList ddlAddState = (DropDownList)ucAddNew.FindControl("ddlAddState");
                    DropDownList ddlAddCity = (DropDownList)ucAddNew.FindControl("ddlAddCity");


                    Label lblProductTelephone = (Label)frmEditProductMap.FindControl("lblProductTelephone");
                    Label lblProductLatitude = (Label)frmEditProductMap.FindControl("lblProductLatitude");
                    Label lblProductLongitude = (Label)frmEditProductMap.FindControl("lblProductLongitude");
                    Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
                    Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
                    Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
                    Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");

                    Label lblpMatchedBy = (Label)frmEditProductMap.FindControl("lblpMatchedBy");
                    Label lblpMatchedByString = (Label)frmEditProductMap.FindControl("lblpMatchedByString");

                    TextBox txtSearchSystemProduct = (TextBox)frmEditProductMap.FindControl("txtSearchSystemProduct");
                    HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");


                    #endregion
                    string street = "";
                    string street2 = "";
                    string street3 = "";
                    string street4 = "";
                    string myCountryName = "";
                    string myStateName = "";
                    string myCityName = "";
                    List<MDMSVC.DC_Accomodation_ProductMapping> masterRoduct = new List<MDMSVC.DC_Accomodation_ProductMapping>();
                    if (grdAccoMaps.DataKeys[index].Values[0] != null)
                    {
                        masterRoduct = mapperSVc.GetProductMappingMasterDataById(Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString()));
                    }

                    fillcountries(ddlSystemCountryName);
                    fillmappingstatus(ddlStatus);
                    if (masterRoduct[0].SupplierName != null)
                        lblSupplierName.Text = masterRoduct[0].SupplierName.ToString(); // System.Web.HttpUtility.HtmlDecode(grdAccoMaps.Rows[index].Cells[2].Text);
                    if (masterRoduct[0].SupplierId != null)
                    {
                        lblSupplierCode.Text = "(" + masterRoduct[0].SupplierId.ToString() + ")";
                    }
                    lblSupCountryName.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].CountryName);
                    if (System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CountryCode)) != "")
                        lblSupCountryCode.Text = Convert.ToString(masterRoduct[0].CountryCode);//"(" + System.Web.HttpUtility.HtmlDecode(grdAccoMaps.Rows[index].Cells[6].Text) + ")";

                    lblCityName.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CityName));
                    if (System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CityCode)) != "")
                        lblCityCode.Text = "(" + System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].CityCode)) + ")";
                    lblProductName.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].ProductName);
                    lblHotelName_TX.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].HotelName_Tx);
                    lblProductCode.Text = System.Web.HttpUtility.HtmlDecode(masterRoduct[0].ProductId);
                    if (masterRoduct[0].Remarks != null)
                        txtSystemRemark.Text = masterRoduct[0].Remarks.ToString(); //masters.GetRemarksForMapping("product", myRow_Id);
                                                                                   //State Name
                    lblSupStateName.Text = Convert.ToString(masterRoduct[0].StateName);
                    lblSupCountryCode.Text = "(" + Convert.ToString(masterRoduct[0].CountryCode) + ")";


                    if (masterRoduct != null)
                    {
                        #region 1
                        lblProductAddress.Text = masterRoduct[0].FullAddress;
                        txtHotelName.Text = masterRoduct[0].ProductName;

                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street))
                            street = masterRoduct[0].Street.ToString();
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street2))
                            street2 = masterRoduct[0].Street2.ToString();
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street3))
                            street3 = masterRoduct[0].Street3.ToString();
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].Street4))
                            street4 = masterRoduct[0].Street4.ToString();
                        txtStreet.Text = street;
                        txtStreet2.Text = street2 + street3 + street4;
                        txtPostalCode.Text = string.IsNullOrWhiteSpace(masterRoduct[0].PostCode) ? string.Empty : masterRoduct[0].PostCode.ToString();
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].TelephoneNumber))
                            lblProductTelephone.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].TelephoneNumber));
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].Latitude))
                            lblProductLatitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].Latitude));
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].Longitude))
                            lblProductLongitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].Longitude));

                        if (masterRoduct[0].MatchedBy != null)
                            lblpMatchedBy.Text = masterRoduct[0].MatchedBy.ToString();
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].MatchedByString))
                            lblpMatchedByString.Text = masterRoduct[0].MatchedByString.ToString();

                        #endregion



                        #region country,StateName, CityName
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemCountryName))
                        { myCountryName = Convert.ToString(masterRoduct[0].SystemCountryName); }
                        else
                        { myCountryName = Convert.ToString(masterRoduct[0].CountryName); }
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemStateName))
                        { myStateName = Convert.ToString(masterRoduct[0].SystemStateName); }
                        else
                        { myStateName = Convert.ToString(masterRoduct[0].StateName); }
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemCityName))
                        { myCityName = Convert.ToString(masterRoduct[0].SystemCityName); }
                        else
                        { myCityName = Convert.ToString(masterRoduct[0].CityName); }

                        #endregion
                        //Guid selSysCountry_ID = masterdata.GetIDByName("COUNTRY", myCountryName);
                        //Guid selSysCity_ID = masterdata.GetIDByName("CITY", myCityName, myCountryName);


                        Guid SystemCountry_Id = Guid.Empty; Guid SystemCity_Id = Guid.Empty;

                        if (masterRoduct[0].Country_Id != null)
                            SystemCountry_Id = masterRoduct[0].Country_Id ?? Guid.Empty;

                        if (masterRoduct[0].City_Id != null)
                            SystemCity_Id = masterRoduct[0].City_Id ?? Guid.Empty;

                        #region Country Mapping data search
                        if (SystemCountry_Id == null || SystemCountry_Id == Guid.Empty)
                        {
                            MDMSVC.DC_CountryMappingRQ RQ = new MDMSVC.DC_CountryMappingRQ();
                            List<MDMSVC.DC_CountryMapping> lstCountry = new List<MDMSVC.DC_CountryMapping>();
                            RQ.Supplier_Id = masterRoduct[0].Supplier_Id;
                            if (!string.IsNullOrWhiteSpace(masterRoduct[0].CountryName) && Convert.ToString(masterRoduct[0].CountryName) != "null")
                                RQ.SupplierCountryName = masterRoduct[0].CountryName;
                            if (!string.IsNullOrWhiteSpace(masterRoduct[0].CountryCode) && Convert.ToString(masterRoduct[0].CountryCode) != "null")
                                RQ.SupplierCountryCode = masterRoduct[0].CountryCode;
                            RQ.PageNo = 0;
                            RQ.PageSize = int.MaxValue;
                            RQ.Status = "ALL";
                            lstCountry = mapperSVc.GetCountryMappingData(RQ);
                            if (lstCountry.Count > 0)
                                SystemCountry_Id = lstCountry[0].Country_Id ?? Guid.Empty;
                        }
                        #endregion
                        if (!string.IsNullOrWhiteSpace(myCountryName) && (SystemCountry_Id == Guid.Empty || SystemCountry_Id == null))
                        {
                            MDMSVC.DC_Country_Search_RQ RQ = new MDMSVC.DC_Country_Search_RQ();
                            List<MDMSVC.DC_Country> lstCountry = new List<MDMSVC.DC_Country>();
                            RQ.Country_Name = myCountryName.Trim();
                            RQ.PageNo = 0;
                            RQ.PageSize = int.MaxValue;
                            lstCountry = _objMasterRef.GetCountryMasterData(RQ);
                            if (lstCountry.Count > 0)
                                SystemCountry_Id = lstCountry[0].Country_Id;
                        }


                        if (SystemCity_Id == null || SystemCity_Id == Guid.Empty)
                        {
                            MDMSVC.DC_CityMapping_RQ RQ = new MDMSVC.DC_CityMapping_RQ();
                            List<MDMSVC.DC_CityMapping> lstCity = new List<MDMSVC.DC_CityMapping>();
                            RQ.Supplier_Id = masterRoduct[0].Supplier_Id;

                            if (SystemCountry_Id != null && SystemCountry_Id != Guid.Empty)
                                RQ.Country_Id = SystemCountry_Id;
                            else if (!string.IsNullOrWhiteSpace(masterRoduct[0].CountryName))
                                RQ.SupplierCountryName = masterRoduct[0].CountryName;

                            if (!string.IsNullOrWhiteSpace(masterRoduct[0].CityName) && Convert.ToString(masterRoduct[0].CityName) != "null")
                                RQ.SupplierCityName = masterRoduct[0].CityName;
                            if (!string.IsNullOrWhiteSpace(masterRoduct[0].CityCode) && Convert.ToString(masterRoduct[0].CityCode) != "null")
                                RQ.SupplierCityCode = masterRoduct[0].CityCode;
                            RQ.PageNo = 0;
                            RQ.PageSize = int.MaxValue;
                            RQ.Status = "ALL";
                            lstCity = mapperSVc.GetCityMappingData(RQ);
                            if (lstCity.Count > 0)
                                SystemCity_Id = lstCity[0].City_Id ?? Guid.Empty;
                        }
                        if (!string.IsNullOrWhiteSpace(myCityName) && (SystemCity_Id == Guid.Empty || SystemCity_Id == null))
                        {
                            MDMSVC.DC_City_Search_RQ RQ = new MDMSVC.DC_City_Search_RQ();
                            List<MDMSVC.DC_City> lstCity = new List<MDMSVC.DC_City>();
                            RQ.City_Name = myCityName.Trim();
                            RQ.Country_Id = SystemCountry_Id;
                            RQ.PageNo = 0;
                            RQ.PageSize = int.MaxValue;
                            lstCity = _objMasterRef.GetCityMasterData(RQ);
                            bool foundExactMatch = false;
                            if (lstCity.Count > 1)
                            {
                                foreach (var item in lstCity)
                                {
                                    if (item.Name.ToString() == myCityName.Trim())
                                    {
                                        SystemCity_Id = item.City_Id;
                                        foundExactMatch = true;
                                        break;
                                    }
                                }
                            }
                            if (lstCity.Count > 0 && !foundExactMatch)
                                SystemCity_Id = lstCity[0].City_Id;

                        }

                        /*var selSysCountry_ID = _objMasterRef.GetDetailsByIdOrName(new MDMSVC.DC_GenericMasterDetails_ByIDOrName()
                        {
                            WhatFor = MDMSVC.DetailsWhatFor.IDByName,
                            Name = myCountryName,
                            ObjName = MDMSVC.EntityType.country
                        });
                        var selSysCity_ID = _objMasterRef.GetDetailsByIdOrName(new MDMSVC.DC_GenericMasterDetails_ByIDOrName()
                        {
                            WhatFor = MDMSVC.DetailsWhatFor.IDByName,
                            Name = myCityName,
                            Optional1 = myCountryName,
                            ObjName = MDMSVC.EntityType.city

                        });*/

                        if (SystemCountry_Id != null && Guid.Parse(Convert.ToString(SystemCountry_Id)) != Guid.Empty)
                            ddlAddCountry.SelectedIndex = ddlAddCountry.Items.IndexOf(ddlAddCountry.Items.FindByValue(Convert.ToString(SystemCountry_Id.ToString())));
                        if (SystemCountry_Id != null && Guid.Parse(Convert.ToString(SystemCountry_Id)) != Guid.Empty)
                            ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByValue(Convert.ToString(SystemCountry_Id.ToString())));

                        //if (selSysCountry_ID != null && Guid.Parse(Convert.ToString(selSysCountry_ID.ID)) != Guid.Empty)
                        //    ddlAddCountry.SelectedIndex = ddlAddCountry.Items.IndexOf(ddlAddCountry.Items.FindByValue(Convert.ToString(selSysCountry_ID.ID)));

                        //Added code for state dropdown
                        var selState_ID = _objMasterRef.GetDetailsByIdOrName(new MDMSVC.DC_GenericMasterDetails_ByIDOrName()
                        {
                            WhatFor = MDMSVC.DetailsWhatFor.IDByName,
                            Name = myStateName,
                            Optional1 = myCountryName,
                            ObjName = MDMSVC.EntityType.state
                        });

                        fillStates(ddlSystemCountryName, ddlSystemStateName);
                        if (ddlSystemStateName.Items.Count > 1)
                        {
                            if (!string.IsNullOrWhiteSpace(masterRoduct[0].StateName))
                            {
                                if (selState_ID != null && selState_ID.ID != null)
                                {
                                    if (Guid.Parse(Convert.ToString(selState_ID.ID)) != Guid.Empty)
                                        ddlSystemStateName.SelectedIndex = ddlSystemStateName.Items.IndexOf(ddlSystemStateName.Items.FindByValue(selState_ID.ID.ToString()));
                                }
                            }
                        }
                        fillStates(ddlAddCountry, ddlAddState);
                        if (ddlAddState.Items.Count > 1)
                        {
                            if (!string.IsNullOrWhiteSpace(masterRoduct[0].StateName))
                            {
                                if (selState_ID != null && selState_ID.ID != null)
                                {
                                    if (Guid.Parse(Convert.ToString(selState_ID.ID)) != Guid.Empty)
                                        ddlAddState.SelectedIndex = ddlAddState.Items.IndexOf(ddlAddState.Items.FindByValue(selState_ID.ID.ToString()));
                                }
                            }
                        }


                        fillcities(ddlAddCity, ddlAddCountry);
                        if (ddlAddCity.Items.Count > 1)
                        {
                            //if (!string.IsNullOrWhiteSpace(masterRoduct[0].CityName))
                            //{
                            if (SystemCity_Id != null && SystemCity_Id != Guid.Empty)
                                ddlAddCity.SelectedIndex = ddlAddCity.Items.IndexOf(ddlAddCity.Items.FindByValue(SystemCity_Id.ToString()));
                            //}
                        }


                        fillcities(ddlSystemCityName, ddlSystemCountryName);
                        ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(System.Web.HttpUtility.HtmlDecode(masterRoduct[0].Status)));
                        if (SystemCity_Id != null && SystemCity_Id != Guid.Empty)
                            ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(SystemCity_Id.ToString()));
                        //if (selSysCity_ID != null && Guid.Parse(Convert.ToString(selSysCity_ID.ID)) != Guid.Empty)
                        //    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(selSysCity_ID.ID.ToString()));

                        //fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);
                        //ddlSystemProductName.SelectedIndex = ddlSystemProductName.Items.IndexOf(ddlSystemProductName.Items.FindByText(masterRoduct[0].SystemProductName.ToString()));

                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemProductName))
                        {
                            string sysproductname = Convert.ToString(masterRoduct[0].SystemProductName);
                            string sysproduct_Id = Convert.ToString(masterRoduct[0].Accommodation_Id);
                            txtSearchSystemProduct.Text = sysproductname;
                            txtSystemProductCode.Text = Convert.ToString(masterRoduct[0].SystemProductCode);
                            ddlSystemProductName.Items.Insert(0, new ListItem("---ALL---", "0"));
                            ddlSystemProductName.Items.Insert(1, new ListItem(sysproductname, sysproduct_Id));
                            ddlSystemProductName.SelectedIndex = 1;
                            hdnSelSystemProduct_Id.Value = sysproduct_Id;
                        }

                        lblSystemProductAddress.Text = masterRoduct[0].SystemFullAddress;
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemTelephone))
                            lblSystemTelephone.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemTelephone));
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemLocation))
                            lblSystemLocation.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemLocation));
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemLatitude))
                            lblSystemLatitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemLatitude));
                        if (!string.IsNullOrWhiteSpace(masterRoduct[0].SystemLongitude))
                            lblSystemLongitude.Text = System.Web.HttpUtility.HtmlDecode(Convert.ToString(masterRoduct[0].SystemLongitude));

                        if (ddlSystemCountryName.SelectedItem.Value != "0")
                            lblSystemCountryCode.Text = masterdata.GetCodeById("country", Guid.Parse(ddlSystemCountryName.SelectedItem.Value));
                        if (lblSystemCountryCode.Text.Replace(" ", "") != "")
                            lblSystemCountryCode.Text = "(" + lblSystemCountryCode.Text + ")";
                        //txtSystemCityCode.Text = masterdata.GetCodeById("city", Guid.Parse(ddlSystemCityName.SelectedItem.Value));
                        lblSystemCityCode.Text = masterdata.GetCodeById("city", Guid.Parse(ddlSystemCityName.SelectedItem.Value));
                        if (lblSystemCityCode.Text.Replace(" ", "") != "")
                        {
                            lblSystemCityCode.Text = "(" + lblSystemCityCode.Text + ")";
                        }
                        txtSystemProductCode.Text = masterdata.GetCodeById("product", Guid.Parse(ddlSystemProductName.SelectedItem.Value));


                    }
                    if (ddlSystemProductName.SelectedItem.Value == "0")
                        btnAddProduct.Attributes.Add("style", "display:block");
                    else
                        btnAddProduct.Attributes.Add("style", "display:none");
                }
                hdnFlag.Value = "false";
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

        protected void frmEditProductMap_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            #region Getting Controls 
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Label lblSupplierName = (Label)frmEditProductMap.FindControl("lblSupplierName");
            Label lblSupplierCode = (Label)frmEditProductMap.FindControl("lblSupplierCode");
            Label lblSupCountryName = (Label)frmEditProductMap.FindControl("lblSupCountryName");
            Label lblSupCountryCode = (Label)frmEditProductMap.FindControl("lblSupCountryCode");
            Label lblCityName = (Label)frmEditProductMap.FindControl("lblCityName");
            Label lblCityCode = (Label)frmEditProductMap.FindControl("lblCityCode");
            Label lblProductName = (Label)frmEditProductMap.FindControl("lblProductName");
            Label lblProductCode = (Label)frmEditProductMap.FindControl("lblProductCode");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");
            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");

            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            DropDownList ddlSystemSystemName = (DropDownList)frmEditProductMap.FindControl("ddlSystemSystemName");


            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");
            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");

            Button btnAddProduct = (Button)frmEditProductMap.FindControl("btnAddProduct");

            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            TextBox txtSystemRemark = (TextBox)frmEditProductMap.FindControl("txtSystemRemark");
            TextBox txtSearchSystemProduct = (TextBox)frmEditProductMap.FindControl("txtSearchSystemProduct");

            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");


            //Button btnMatchedMapSelected = (Button)frmEditProductMap.FindControl("btnMatchedMapSelected");
            //Button btnMatchedMapAll = (Button)frmEditProductMap.FindControl("btnMatchedMapAll");

            #endregion
            if (e.CommandName == "Add")
            {
                hdnIsAnyChanges.Value = "true";
                Guid myRow_Id = Guid.Parse(grdAccoMaps.SelectedDataKey.Value.ToString());
                Guid? AccoId = null;
                string countryname = string.Empty;
                Guid? countryId = null;
                string cityname = string.Empty;
                //Guid? cityId = null;
                //Guid? productId = null;

                if (txtSearchSystemProduct.Text != string.Empty && ((ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedItem.Value != "0") || !string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value)))
                {
                    if (ddlSystemCountryName.SelectedIndex != 0)
                    {

                        countryname = ddlSystemCountryName.SelectedItem.Value;
                        countryId = new Guid(countryname);
                        cityname = ddlSystemCityName.SelectedItem.Value;
                        //cityId = new Guid(cityname);
                        if (ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedItem.Value != "0")
                        {
                            AccoId = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                            //productId = new Guid(ddlSystemProductName.SelectedItem.Value);
                        }
                        else if (!string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value))
                        {
                            AccoId = Guid.Parse(hdnSelSystemProduct_Id.Value);
                            //productId = Guid.Parse(hdnSelSystemProduct_Id.Value);
                        }
                    }
                    MDMSVC.DC_Accomodation_ProductMapping newObj = new MDMSVC.DC_Accomodation_ProductMapping
                    {
                        Accommodation_ProductMapping_Id = myRow_Id,
                        Accommodation_Id = AccoId,
                        SystemCountryName = countryname,
                        SystemCityName = cityname,
                        Status = ddlStatus.SelectedItem.Text,
                        Remarks = txtSystemRemark.Text,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                    };

                    if (!string.IsNullOrEmpty(lblSupplierName.Text))
                    {
                        newObj.SupplierName = lblSupplierName.Text;
                        newObj.SupplierId = lblSupplierCode.Text.Replace("(", "").Replace(")", "");
                    }
                    RQ.Add(newObj);
                    if (mapperSVc.UpdateProductMappingData(RQ))
                    {

                        var result = AccSvc.GetAccomodationBasicInfo(newObj.Accommodation_Id ?? Guid.Empty);
                        if (result != null)
                        {
                            txtSystemProductCode.Text = Convert.ToString(result[0].CompanyHotelID);
                            lblSystemProductAddress.Text = Convert.ToString(result[0].FullAddress);
                            lblSystemLocation.Text = Convert.ToString(result[0].Location);
                            lblSystemTelephone.Text = Convert.ToString(result[0].Telephone_Tx);
                            lblSystemLatitude.Text = Convert.ToString(result[0].Latitude);
                            lblSystemLongitude.Text = Convert.ToString(result[0].Longitude);
                            //if (!string.IsNullOrWhiteSpace(Convert.ToString(result[0].City)))
                            //{
                            //    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByText(Convert.ToString(result[0].City)));
                            //}
                            //if (!string.IsNullOrWhiteSpace(Convert.ToString(result[0].State_Name)))
                            //{
                            //    ddlSystemSystemName.SelectedIndex = ddlSystemSystemName.Items.IndexOf(ddlSystemSystemName.Items.FindByText(Convert.ToString(result[0].State_Name)));
                            //}
                            btnAddProduct.Attributes.Add("style", "display:none");

                        }
                        dvMsg.Style.Add("display", "block");
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been mapped successfully", BootstrapAlertType.Success);
                        if (!(ddlSystemCountryName.SelectedIndex == 0))
                        {
                            //fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
                            fillmatchingdata("", 0);
                            //Setting control value setted in javascript

                            dvMatchingRecords.Visible = true;
                            btnMatchedMapSelected.Visible = true;
                            btnMatchedMapAll.Visible = true;
                            hdnFlag.Value = "false";
                        }
                        else
                        {
                            dvMatchingRecords.Visible = false;
                            btnMatchedMapSelected.Visible = false;
                            btnMatchedMapAll.Visible = false;
                            hdnFlag.Value = "false";
                        }
                    }
                }
                else
                {
                    dvMsg.Style.Add("display", "block");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please select valid Hotel to map", BootstrapAlertType.Warning);

                }
            }
            else if (e.CommandName == "Cancel")
            {
                frmEditProductMap.ChangeMode(FormViewMode.Edit);
                frmEditProductMap.DataSource = null;
                frmEditProductMap.DataBind();
                frmEditProductMap.Visible = false;
                dvMatchingRecords.Visible = false;
                btnMatchedMapSelected.Visible = false;
                btnMatchedMapAll.Visible = false;
                hdnPageNumber.Value = grdAccoMaps.PageIndex.ToString();
                fillproductdata(ref isDataExist, "supplier", Convert.ToInt32(hdnPageNumber.Value ?? "0"));
            }
            else if (e.CommandName == "OpenAddProduct")
            {
                dvAddProduct.Style.Add("display", "block");
                dvAddProduct.Visible = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "javascript:closeCityMappingModal();", true);

        }

        protected void ddlSystemCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");

            DropDownList ddlSystemStateName = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            TextBox txtSystemStateCode = (TextBox)frmEditProductMap.FindControl("txtSystemStateCode");
            Label lblSystemStateCode = (Label)frmEditProductMap.FindControl("lblSystemStateCode");

            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");



            DropDownList ddlAddCountry = (DropDownList)ucAddNew.FindControl("ddlAddCountry");
            DropDownList ddlAddState = (DropDownList)ucAddNew.FindControl("ddlAddState");
            DropDownList ddlAddCity = (DropDownList)ucAddNew.FindControl("ddlAddCity");

            //txtSystemCountryCode.Text = masterdata.GetCodeById("country", Guid.Parse(ddlSystemCountryName.SelectedItem.Value));
            if (ddlSystemCountryName.SelectedIndex != 0)
            {
                lblSystemCountryCode.Text = masterdata.GetCodeById(MDMSVC.EntityType.country, Guid.Parse(ddlSystemCountryName.SelectedItem.Value));
                if (lblSystemCountryCode.Text.Replace(" ", "") != "")
                    lblSystemCountryCode.Text = "(" + lblSystemCountryCode.Text + ")";
                fillStates(ddlSystemCountryName, ddlSystemStateName);

                fillcities(ddlSystemCityName, ddlSystemCountryName);

                //txtSystemCityCode.Text = "";
                ddlAddCountry.SelectedIndex = ddlAddCountry.Items.IndexOf(ddlAddCountry.Items.FindByValue(ddlSystemCountryName.SelectedItem.Value));
                fillStates(ddlAddCountry, ddlAddState);
                fillcities(ddlAddCity, ddlAddCountry);
                ddlSystemStateName.Focus();
            }
            //ddlSystemProductName.SelectedIndex = 0;
            ddlSystemCityName.SelectedIndex = 0;
            lblSystemCountryCode.Text = string.Empty;
            txtSystemProductCode.Text = string.Empty;
            ResetSystemDetails("");
        }
        public void ResetSystemDetails(string byReset)
        {
            DropDownList ddlStystemCity = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            DropDownList ddlStystemState = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");
            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");
            TextBox txtSearchSystemProduct = (TextBox)frmEditProductMap.FindControl("txtSearchSystemProduct");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            HiddenField hdnSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSystemProduct_Id");
            HiddenField hdnSystemProduct = (HiddenField)frmEditProductMap.FindControl("hdnSystemProduct");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");
            lblSystemCityCode.Text = "";
            lblSystemProductAddress.Text = string.Empty;
            lblSystemCityCode.Text = string.Empty;
            lblSystemLocation.Text = string.Empty;
            lblSystemTelephone.Text = string.Empty;
            lblSystemLatitude.Text = string.Empty;
            lblSystemLongitude.Text = string.Empty;
            txtSearchSystemProduct.Text = string.Empty;
            txtSystemProductCode.Text = string.Empty;
            hdnSystemProduct_Id.Value = null;
            hdnSystemProduct.Value = null;
            hdnSelSystemProduct_Id.Value = null;
            if (byReset == "state")
            {
                ddlStystemCity.SelectedIndex = 0;
            }

        }

        protected void ddlSystemCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditProductMap.FindControl("txtSystemCountryCode");
            Label lblSystemCountryCode = (Label)frmEditProductMap.FindControl("lblSystemCountryCode");
            DropDownList ddlSystemCityName = (DropDownList)frmEditProductMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditProductMap.FindControl("txtSystemCityCode");
            Label lblSystemCityCode = (Label)frmEditProductMap.FindControl("lblSystemCityCode");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            DropDownList ddlAddCountry = (DropDownList)ucAddNew.FindControl("ddlAddCountry");
            DropDownList ddlAddCity = (DropDownList)ucAddNew.FindControl("ddlAddCity");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");
            HiddenField hdnIsJavascriptChagedValueddlSystemStateName = (HiddenField)frmEditProductMap.FindControl("hdnIsJavascriptChagedValueddlSystemStateName");
            HiddenField hdnIsJavascriptChagedValueddlSystemCityName = (HiddenField)frmEditProductMap.FindControl("hdnIsJavascriptChagedValueddlSystemCityName");

            lblSystemProductAddress.Text = "";

            //txtSystemCityCode.Text = masterdata.GetCodeById("city", Guid.Parse(ddlSystemCityName.SelectedItem.Value));
            if (ddlSystemCityName.SelectedItem.Value != "0")
                lblSystemCityCode.Text = masterdata.GetCodeById(MDMSVC.EntityType.city, Guid.Parse(ddlSystemCityName.SelectedItem.Value));
            if (lblSystemCityCode.Text.Replace(" ", "") != "")
                lblSystemCityCode.Text = "(" + lblSystemCityCode.Text + ")";
            fillproducts(ddlSystemProductName, ddlSystemCityName, ddlSystemCountryName);
            txtSystemProductCode.Text = "";
            ddlAddCity.SelectedIndex = ddlAddCity.Items.IndexOf(ddlAddCity.Items.FindByValue(ddlSystemCityName.SelectedItem.Value));
            ddlSystemProductName.Focus();
            if (hdnIsJavascriptChagedValueddlSystemCityName.Value != "true")
                ResetSystemDetails("");
            else
                hdnIsJavascriptChagedValueddlSystemCityName.Value = "false";

        }

        protected void ddlSystemProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            Button btnAddProduct = (Button)frmEditProductMap.FindControl("btnAddProduct");
            TextBox txtSystemProductCode = (TextBox)frmEditProductMap.FindControl("txtSystemProductCode");
            Label lblSystemProductAddress = (Label)frmEditProductMap.FindControl("lblSystemProductAddress");

            Label lblSystemTelephone = (Label)frmEditProductMap.FindControl("lblSystemTelephone");
            Label lblSystemLocation = (Label)frmEditProductMap.FindControl("lblSystemLocation");
            Label lblSystemLatitude = (Label)frmEditProductMap.FindControl("lblSystemLatitude");
            Label lblSystemLongitude = (Label)frmEditProductMap.FindControl("lblSystemLongitude");
            if (ddlSystemProductName.SelectedItem.Value == "0")
            {
                btnAddProduct.Visible = true;
            }
            else
            {
                txtSystemProductCode.Text = masterdata.GetCodeById("product", Guid.Parse(ddlSystemProductName.SelectedItem.Value));
                btnAddProduct.Visible = false;
                AccomodationSVC _acmd = new AccomodationSVC();
                var result = _acmd.SearchHotels(new MDMSVC.DC_Accomodation_Search_RQ()
                {
                    AccomodationId = ddlSystemProductName.SelectedItem.Value,
                    PageNo = 0,
                    PageSize = 5
                });
                if (result != null && result.Count > 0)
                {
                    lblSystemProductAddress.Text = result[0].FullAddress;
                    lblSystemTelephone.Text = result[0].Telephone_Tx;
                    lblSystemLatitude.Text = result[0].Latitude;
                    lblSystemLongitude.Text = result[0].Longitude;
                    lblSystemLocation.Text = result[0].Location;
                }
            }
            txtSystemProductCode.Focus();
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
                    HtmlInputCheckBox chk = row.Cells[15].Controls[1] as HtmlInputCheckBox;
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
                    HtmlInputCheckBox chk = row.Cells[15].Controls[1] as HtmlInputCheckBox;
                    //CheckBox chk = row.Cells[15].Controls[1] as CheckBox;
                    DropDownList ddl = row.Cells[11].Controls[1] as DropDownList;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        myAccommodation_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                        HiddenField hdnAccp_Id = row.Cells[16].Controls[1] as HiddenField; //Set value from ajax changes

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
                    HtmlInputCheckBox chk = row.Cells[15].Controls[1] as HtmlInputCheckBox;
                    //CheckBox chk = row.Cells[15].Controls[1] as CheckBox;
                    DropDownList ddl = row.Cells[11].Controls[1] as DropDownList;
                    HiddenField hdnAccp_Id = row.Cells[16].Controls[1] as HiddenField; //Set value from ajax changes
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
                int index = e.Row.RowIndex;
                Guid myCountryId = Guid.Empty;
                string CountryName = e.Row.Cells[7].Text.ToUpper();
                string CityName = e.Row.Cells[9].Text.ToUpper();
                MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
                DropDownList ddl = e.Row.Cells[11].Controls[1] as DropDownList;
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

        protected void grdMatchingProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdMatchingProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //MatchedPageIndex = e.NewPageIndex;
            fillmatchingdata("", e.NewPageIndex);
        }

        protected void grdMatchingProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdMatchingProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdMatchingProducts_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlMatchingPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("", 0);
            ddlMatchingStatus.Focus();
        }

        protected void ddlMatchingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("status", 0);
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

        protected void btnMatchedMapSelected_Click(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");
            List<MDMSVC.DC_Accomodation_ProductMapping> newObj = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid? myAcco_Id = Guid.Empty;
            bool res = false;
            hdnIsAnyChanges.Value = "true";
            foreach (GridViewRow row in grdMatchingProducts.Rows)
            {
                HtmlInputCheckBox chk = row.Cells[11].Controls[1] as HtmlInputCheckBox;
                //   CheckBox chk = row.Cells[11].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myAcco_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[0].ToString());
                    mySupplier_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[1].ToString());
                    if (ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedValue != "0")
                        myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                    else if (!string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value))
                        myAcco_Id = Guid.Parse(hdnSelSystemProduct_Id.Value);
                    //myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Accomodation_ProductMapping param = new MDMSVC.DC_Accomodation_ProductMapping();
                    param.Accommodation_ProductMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myAcco_Id != null)
                        param.Accommodation_Id = myAcco_Id;
                    param.Status = ddlStatus.SelectedItem.Text;
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    newObj.Add(param);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myAcco_Id = Guid.Empty;
                }
            }
            if (mapperSVc.UpdateProductMappingData(newObj))
            {
                fillmatchingdata("", grdMatchingProducts.PageIndex);
                //fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
                hdnFlag.Value = "false";
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
            }
        }

        protected void btnMatchedMapAll_Click(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)frmEditProductMap.FindControl("ddlStatus");
            DropDownList ddlSystemProductName = (DropDownList)frmEditProductMap.FindControl("ddlSystemProductName");
            HiddenField hdnSelSystemProduct_Id = (HiddenField)frmEditProductMap.FindControl("hdnSelSystemProduct_Id");

            hdnIsAnyChanges.Value = "true";
            List<MDMSVC.DC_Accomodation_ProductMapping> newObj = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid? myAcco_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdMatchingProducts.Rows)
            {
                myRow_Id = Guid.Empty;
                mySupplier_Id = Guid.Empty;
                myAcco_Id = Guid.Empty;
                int index = row.RowIndex;
                myRow_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[0].ToString());
                mySupplier_Id = Guid.Parse(grdMatchingProducts.DataKeys[index].Values[1].ToString());

                if (ddlSystemProductName.Items.Count > 0 && ddlSystemProductName.SelectedValue != "0")
                    myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                else if (!string.IsNullOrWhiteSpace(hdnSelSystemProduct_Id.Value))
                    myAcco_Id = Guid.Parse(hdnSelSystemProduct_Id.Value);

                //myAcco_Id = Guid.Parse(ddlSystemProductName.SelectedItem.Value);
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_Accomodation_ProductMapping param = new MDMSVC.DC_Accomodation_ProductMapping();
                    param.Accommodation_ProductMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myAcco_Id != null)
                        param.Accommodation_Id = myAcco_Id;
                    param.Status = ddlStatus.SelectedItem.Text;
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    newObj.Add(param);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myAcco_Id = Guid.Empty;
                }
            }
            if (mapperSVc.UpdateProductMappingData(newObj))
            {
                fillmatchingdata("", grdMatchingProducts.PageIndex);
                //fillproductdata(ref isDataExist, "supplier", grdAccoMaps.PageIndex);
                hdnFlag.Value = "false";
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
            }

        }

        protected void ddlSystemStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSystemStateName = (DropDownList)frmEditProductMap.FindControl("ddlSystemStateName");
            DropDownList ddlAddState = (DropDownList)ucAddNew.FindControl("ddlAddState");
            HiddenField hdnIsJavascriptChagedValueddlSystemStateName = (HiddenField)frmEditProductMap.FindControl("hdnIsJavascriptChagedValueddlSystemStateName");
            if (hdnIsJavascriptChagedValueddlSystemStateName.Value != "true")
                ResetSystemDetails("state");
            else
                hdnIsJavascriptChagedValueddlSystemStateName.Value = "false";
            ddlAddState.SelectedIndex = ddlAddState.Items.IndexOf(ddlAddState.Items.FindByValue(ddlSystemStateName.SelectedItem.Value));

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