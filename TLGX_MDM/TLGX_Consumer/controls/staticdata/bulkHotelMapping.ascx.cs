using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class bulkHotelMapping : System.Web.UI.UserControl
    {

        public static Guid Accomodation_ID;
        MasterDataDAL masterdata = new MasterDataDAL();
        businessEntityDAL bsuinessEntities = new businessEntityDAL();

        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQAPM = new MDMSVC.DC_Mapping_ProductSupplier_Search_RQ();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        public static string ParentPageName = "";
        public static int PageIndex = 0;
        public static int PageIndexMapped = 0;
        public static string SearchSource = "";

        public int intMappedgrvwPageNo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setvisibility();
                SetManualSearchVisibility();
                if (ParentPageName == "addProductMapping.aspx")
                {
                    fillcountrydropdown("search");
                    fillstatusdropdown();
                    fillSupplierDropDown();
                }
                else
                {
                    //fillproductmappingdata();
                }


            }
        }

        public void fillproductmappingdata(Guid selectedKey)
        {
            PageIndex = 0;
            PageIndexMapped = 0;
            Accomodation_ID = selectedKey;
            SearchSource = "id";
            fillAPMrequestobject();
            fillmappeddata();
        }

        public void fillmappeddata()
        {
            //RQAPM = null;
            if (Accomodation_ID != null)
            {
            }
            PageIndexMapped = intMappedgrvwPageNo;
            var resmapped = MapSvc.GetProductMappingMasterData(PageIndexMapped, 10, Accomodation_ID, "MAPPED");
            if (resmapped != null)
            {
                grdAccoMaps.DataSource = resmapped;
                if (resmapped.Count > 0)
                {
                    grdAccoMaps.VirtualItemCount = resmapped[0].TotalRecords;
                    lblMappedData.Text = resmapped[0].TotalRecords.ToString();
                }
                else
                    lblMappedData.Text = "0";
            }
            else
                lblMappedData.Text = "0";
            grdAccoMaps.PageIndex = PageIndexMapped;
            grdAccoMaps.PageSize = 10;
            grdAccoMaps.DataBind();
        }
        protected void grdAccoMaps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intMappedgrvwPageNo = e.NewPageIndex;
            fillmappeddata();
        }
        public void fillAPMrequestobject()
        {
            //GridView grdTLGXProdData = (GridView)this.Parent.FindControl("grdTLGXProdData");
            //RQAPM = null;
            if (SearchSource == "id")
            {
                var hoteldetails = AccSvc.GetHotelDetails(Accomodation_ID);
                if (hoteldetails != null)
                {
                    if (!string.IsNullOrEmpty(hoteldetails[0].HotelName))
                        RQAPM.ProductName = hoteldetails[0].HotelName;
                    if (!string.IsNullOrEmpty(hoteldetails[0].StreetName))
                        RQAPM.Street = hoteldetails[0].StreetName;
                    if (!string.IsNullOrEmpty(hoteldetails[0].Country))
                        RQAPM.CountryName = hoteldetails[0].Country;
                    if (!string.IsNullOrEmpty(hoteldetails[0].City))
                        RQAPM.CityName = hoteldetails[0].City;
                    var telephones = hoteldetails[0].Accomodation_Contact;
                    if (telephones != null)
                    {
                        if (telephones.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(telephones[0].Telephone))
                                RQAPM.TelephoneNumber = telephones[0].Telephone;
                        }
                    }
                }
            }
            else if (SearchSource == "manual")
            {
                if (!string.IsNullOrEmpty(txtSupProductName.Text))
                    RQAPM.ProductName = txtSupProductName.Text;
                if (!string.IsNullOrEmpty(txtSupAddress.Text))
                    RQAPM.Street = txtSupAddress.Text;
                if (!string.IsNullOrEmpty(txtSupCountry.Text))
                    RQAPM.CountryName = txtSupCountry.Text;
                if (!string.IsNullOrEmpty(txtSupCity.Text))
                    RQAPM.CityName = txtSupCity.Text;
                //if (ddlSupCountry.SelectedItem.Value != "0")
                //    RQAPM.CountryName = ddlSupCountry.SelectedItem.Text;
                //if (ddlSupCity.SelectedItem.Value != "0")
                //    RQAPM.CityName = ddlSupCity.SelectedItem.Text;
                if (!string.IsNullOrEmpty(txtSupTelephone.Text))
                    RQAPM.TelephoneNumber = txtSupTelephone.Text;
            }
            RQAPM.Source = "SYSTEMDATA";
            RQAPM.StatusExcept = "MAPPED";
            RQAPM.PageNo = PageIndex;
            RQAPM.PageSize = Convert.ToInt32(ddlPageSizeSupDump.SelectedItem.Text);
            var mappingresult = MapSvc.GetProductMappingData(RQAPM);

            if (mappingresult != null)
            {
                grdSupplierDump.DataSource = mappingresult;
                if (mappingresult.Count > 0)
                {
                    grdSupplierDump.VirtualItemCount = mappingresult[0].TotalRecords;
                    lblSupDump.Text = mappingresult[0].TotalRecords.ToString();
                }
                else
                    lblSupDump.Text = "0";
            }
            else
                lblSupDump.Text = "0";
            grdSupplierDump.PageIndex = PageIndex;
            grdSupplierDump.PageSize = Convert.ToInt32(ddlPageSizeSupDump.SelectedItem.Text);
            //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
            grdSupplierDump.DataBind();
            setupmanualsearchfileds();

        }

        private void setupmanualsearchfileds()
        {
            //fillcountrydropdown("manual");
            //if (ddlSupCountry.Items.Count > 1)
            //{
            //    if (RQAPM.CountryName != null)
            //    {
            //        ddlSupCountry.SelectedIndex = ddlSupCountry.Items.IndexOf(ddlSupCountry.Items.FindByText(RQAPM.CountryName.ToString()));
            //        fillcitydropdown("manual", ddlSupCountry.SelectedItem.Value);
            //        if (ddlSupCity.Items.Count > 1)
            //        {
            //            if (RQAPM.CityName != null)
            //            {
            //                ddlSupCity.SelectedIndex = ddlSupCity.Items.IndexOf(ddlSupCity.Items.FindByText(RQAPM.CityName.ToString()));
            //            }
            //        }
            //    }
            //}
            if (RQAPM.CountryName != null)
            {
                txtSupCountry.Text = RQAPM.CountryName.ToString();
                lblSupCountry.Text = RQAPM.CountryName.ToString(); 
            }
            if (RQAPM.CityName != null)
            {
                txtSupCity.Text = RQAPM.CityName.ToString();
                lblSupCity.Text = RQAPM.CityName.ToString();
            }
            if (RQAPM.ProductName != null)
            {
                txtSupProductName.Text = RQAPM.ProductName.ToString();
                lblSupProductName.Text = RQAPM.ProductName.ToString();

            }
            if (RQAPM.Street != null)
            {
                txtSupAddress.Text = RQAPM.Street.ToString();
                lblSupAddress.Text = RQAPM.Street.ToString();

            }
            if (RQAPM.TelephoneNumber != null)
            {
                txtSupTelephone.Text = RQAPM.TelephoneNumber.ToString();
                lblSupTelephone.Text = RQAPM.TelephoneNumber.ToString();

            }
        }

        private void setvisibility()
        {
            var a = this.Parent;
            string vPath = ((System.Web.UI.TemplateControl)this.BindingContainer).AppRelativeVirtualPath;
            string dir = this.NamingContainer.BindingContainer.AppRelativeTemplateSourceDirectory;

            ParentPageName = vPath.Replace(dir, "");
            if (ParentPageName == "addProductMapping.aspx")
            {
                dvLinks.Visible = true;
                dvProdSearch.Visible = true;
                dvProdResult.Visible = true;
                dvProdSupDump.Visible = true;
            }
            else
            {
                dvLinks.Visible = false;
                panSupplierSearch.Visible = false;
                panProductSearch.Visible = true;
                dvProdSearch.Visible = false;
                dvProdResult.Visible = false;
                dvProdSupDump.Visible = true;
            }

        }

        private void fillstatusdropdown()
        {
            //ddlProductStatus.DataSource = masterdata.getAllStatuses();
            MasterDataSVCs _objMasterData = new MasterDataSVCs();
            ddlProductStatus.DataSource = _objMasterData.GetAllStatuses();

            ddlProductStatus.DataTextField = "Status_Name";
            ddlProductStatus.DataValueField = "Status_Short";
            ddlProductStatus.DataBind();
            ddlProductStatus.Items.Insert(0, new ListItem("---ALL---", ""));
            ddlProductStatus.SelectedIndex = ddlProductStatus.Items.IndexOf(ddlProductStatus.Items.FindByText("ACTIVE"));
        }

        private void fillcountrydropdown(string source)
        {
            //var resSet = masterdata.GetMasterCountryData("");
            var result = masterSVc.GetAllCountries();
            if (source == "search")
            {
                //ddlCountry.DataSource = resSet;
                //ddlCountry.DataValueField = "Country_ID";
                //ddlCountry.DataTextField = "Name";
                ddlCountry.DataSource = result;
                ddlCountry.DataValueField = "Country_Id";
                ddlCountry.DataTextField = "Country_Name";
                ddlCountry.DataBind();
            }
            if (source == "manual")
            {
                //ddlSupCountry.DataSource = resSet;
                //ddlSupCountry.DataValueField = "Country_ID";
                //ddlSupCountry.DataTextField = "Name";
                //ddlSupCountry.DataBind();
            }

        }

        private void fillcitydropdown(string source, string Country_ID)
        {
            if (Country_ID != "")
            {
                //var resSet = masterdata.GetMasterCityData(new Guid(Country_ID));
                var resSet = masterSVc.GetMasterCityData(Country_ID);
                if (source == "search")
                {
                    ddlCity.DataSource = resSet;
                    ddlCity.DataValueField = "City_ID";
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
                else if (source == "manual")
                {
                    //ddlSupCity.Items.Clear();
                    //ddlSupCity.DataSource = resSet;
                    //ddlSupCity.DataValueField = "City_ID";
                    //ddlSupCity.DataTextField = "Name";
                    //ddlSupCity.DataBind();
                    //ddlSupCity.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            else
            {
                if (source == "search")
                {
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
                else if (source == "manual")
                {
                    //ddlSupCity.Items.Clear();
                    //ddlSupCity.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            divMsgForMapping.Style.Add("display", "none");
            divMsgForUnMapping.Style.Add("display", "none");
            if (ddlCountry.SelectedValue != "")
            {
                fillcitydropdown("search", ddlCountry.SelectedValue);
            }
            else
            {
                fillcitydropdown("search", "");
            }
            ddlCity.Focus();
        }

        public void CallHotelSearch(int pageIndex, int pageSize)
        {
            RQParams.ProductCategory = "Accommodation";
            RQParams.ProductCategorySubType = "Hotel"; //lazy matthew...
            RQParams.Status = ddlProductStatus.SelectedItem.Text;
            RQParams.Country = ddlCountry.SelectedItem.Text;
            RQParams.City = ddlCity.SelectedItem.Text;

            if (txtProductName.Text.Length != 0)
                RQParams.HotelName = txtProductName.Text;

            //if (ddlChain.SelectedIndex != 0)
            //   ;

            //if (ddlChain.SelectedIndex != 0)
            //    ;

            RQParams.PageNo = pageIndex;
            RQParams.PageSize = pageSize;
        }

        protected void fillSupplierDropDown()
        {
            ddlSupplierName.DataSource = bsuinessEntities.GetSupplierList();
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataBind();

        }

        public void fillHotelSearchGrid(int pageIndex, int pageSize)
        {
            CallHotelSearch(pageIndex, pageSize);
            var res = AccSvc.SearchHotels(RQParams);
            grdProductSearch.DataSource = res;
            if (res != null)
            {
                grdProductSearch.VirtualItemCount = res[0].TotalRecords;
                lblProductSearch.Text = res[0].TotalRecords.ToString();
            }
            else
                lblProductSearch.Text = "0";
            grdProductSearch.PageIndex = pageIndex;
            grdProductSearch.PageSize = pageSize;
            grdProductSearch.DataBind();
        }

        protected void btnSearchHotels_Click(object sender, EventArgs e)
        {
            fillHotelSearchGrid(0, Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Value));
        }

        protected void grdProductSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillHotelSearchGrid(e.NewPageIndex, Convert.ToInt32(ddlProductBasedPageSize.SelectedItem.Value));
        }

        private static void AddSuperHeader(GridView gridView)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            myNewRow.Cells.Add(MakeCell("Supplier", 10));
            myNewRow.Cells.Add(MakeCell("System", 6));
            myNewRow.Cells.Add(MakeCell("", 1));

            myTable.Rows.AddAt(0, myNewRow);
        }

        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { ColumnSpan = span, Text = text ?? string.Empty, CssClass = "table-header" };
        }

        protected void grdAccoMaps_DataBound(object sender, EventArgs e)
        {
            //var myGridView = (GridView)sender;
            //if (myGridView.Controls.Count > 0)
            //    AddSuperHeader(myGridView);
        }

        protected void grdProductSearch_RowCommand(object sender, GridViewCommandEventArgs e)

        {
            if (e.CommandName == "Map")
            {
                // two actions required

                // #1 bind the existing maps grid as we will be adding data to that grid
                int index = Convert.ToInt32(e.CommandArgument);
                Guid myRow_Id = Guid.Parse(grdProductSearch.DataKeys[index].Value.ToString());

                grdAccoMaps.DataSource = AccSvc.GetAccomodation_ProductMapping(myRow_Id, 0, 10);
                grdAccoMaps.DataBind();

                //#2 set the starting search for existing maps - there's an issue on the servie at the moment so this is stub data just to populate the UI
                grdSupplierDump.DataSource = AccSvc.GetAccomodation_ProductMapping(Guid.Empty, 0, 5);
                grdSupplierDump.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            PageIndexMapped = 0;
            // there is an issue with the search at the moment, so this is returning stub data for UI building
            grdSupplierPending.DataSource = AccSvc.GetAccomodation_ProductMapping(Guid.Empty, 0, 1);
            grdSupplierPending.DataBind();

        }

        protected void grdSupplierPending_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Map")
            {
                // two actions required
                // run a hotel search based on the values in the field to populate the hotel list

                // dummy call to generate UI values
                RQParams.City = "High Wycombe";
                RQParams.Country = "United Kingdom";

                var res = AccSvc.SearchHotels(RQParams);
                grdHotelToMap.DataSource = res;
                grdHotelToMap.DataBind();

            }



        }

        protected void grdSupplierDump_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            divMsgForUnMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            PageIndex = e.NewPageIndex;
            fillAPMrequestobject();
        }

        //protected void ddlSupCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    fillcitydropdown("manual", ddlSupCountry.SelectedItem.Value);
        //}

        private void SetManualSearchVisibility()
        {
            if (pnlManualSearch.Visible)
            {
                pnlManualSearch.Visible = false;
                //pnlSupDumpGrid.Visible = true;
            }
            else
            {
                pnlManualSearch.Visible = true;
                //pnlSupDumpGrid.Visible = false;
            }
        }

        protected void btnManualSearch_Click(object sender, EventArgs e)
        {
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            SetManualSearchVisibility();
        }

        protected void btnSupManualSearch_Click(object sender, EventArgs e)
        {
            SearchSource = "manual";
            fillAPMrequestobject();
        }

        protected void btnMap_Click(object sender, EventArgs e)
        {
            divMsgForUnMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();

            Guid myRow_Id;
            Guid myAcco_Id;
            foreach (GridViewRow row in grdSupplierDump.Rows)
            {
                CheckBox chk = row.Cells[5].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    int index = row.RowIndex;
                    MDMSVC.DC_Accomodation_ProductMapping RQParam = new MDMSVC.DC_Accomodation_ProductMapping();
                    myRow_Id = Guid.Parse(grdSupplierDump.DataKeys[index].Values[0].ToString());
                    //myAcco_Id = Guid.Parse(grdSupplierDump.DataKeys[index].Values[1].ToString());
                    RQParam.Accommodation_ProductMapping_Id = myRow_Id;
                    if (Accomodation_ID != null)
                        RQParam.Accommodation_Id = Accomodation_ID;
                    RQParam.Status = "MAPPED";
                    RQParam.IsActive = true;
                    RQParam.Edit_Date = DateTime.Now;
                    RQParam.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

                    RQ.Add(RQParam);
                    RQParam = null;
                }
            }
            if (RQ.Count > 0)
            {
                if (MapSvc.UpdateProductMappingData(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, "Records mapped successfully", BootstrapAlertType.Success);
                    fillAPMrequestobject();
                    fillmappeddata();
                }
            }
        }

        protected void btnMapAll_Click(object sender, EventArgs e)
        {

            divMsgForUnMapping.Style.Add(HtmlTextWriterStyle.Display, "none");

            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Guid myRow_Id;
            Guid myAcco_Id;
            foreach (GridViewRow row in grdSupplierDump.Rows)
            {
                int index = row.RowIndex;
                myRow_Id = Guid.Parse(grdSupplierDump.DataKeys[index].Values[0].ToString());
                //myAcco_Id = Guid.Parse(grdSupplierDump.DataKeys[index].Values[1].ToString());
                MDMSVC.DC_Accomodation_ProductMapping RQParam = new MDMSVC.DC_Accomodation_ProductMapping();
                RQParam.Accommodation_ProductMapping_Id = myRow_Id;
                if (Accomodation_ID != null)
                    RQParam.Accommodation_Id = Accomodation_ID;
                RQParam.Status = "MAPPED";
                RQParam.IsActive = true;
                RQParam.Edit_Date = DateTime.Now;
                RQParam.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

                RQ.Add(RQParam);
                RQParam = null;
            }
            if (MapSvc.UpdateProductMappingData(RQ))
            {
                BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, "Records mapped successfully", BootstrapAlertType.Success);
                fillAPMrequestobject();
                fillmappeddata();
            }
        }

        protected void grdAccoMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();

            if (e.CommandName == "UnMap")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                if (myRow_Id != null)
                {
                    MDMSVC.DC_Accomodation_ProductMapping newObj = new MDMSVC.DC_Accomodation_ProductMapping
                    {
                        Accommodation_ProductMapping_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString()),
                        Accommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString()),
                        Status = "UNMAPPED",
                        IsActive = true,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                    };
                    RQ.Add(newObj);
                    if (MapSvc.UpdateProductMappingData(RQ))
                    {
                        BootstrapAlert.BootstrapAlertMessage(divMsgForUnMapping, "Record unmapped successfully", BootstrapAlertType.Success);
                        fillAPMrequestobject();
                        fillmappeddata();
                    }
                }
            }
        }

        protected void btnUnmapSelected_Click(object sender, EventArgs e)
        {

            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Guid myRow_Id;
            Guid myAcco_Id;
            foreach (GridViewRow row in grdAccoMaps.Rows)
            {
                CheckBox chk = row.Cells[15].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                    if (myRow_Id != null)
                    {
                        MDMSVC.DC_Accomodation_ProductMapping newObj = new MDMSVC.DC_Accomodation_ProductMapping
                        {
                            Accommodation_ProductMapping_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString()),
                            Accommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString()),
                            Status = "UNMAPPED",
                            IsActive = true,
                            Edit_Date = DateTime.Now,
                            Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                        };
                        RQ.Add(newObj);
                    }
                }
            }
            if (MapSvc.UpdateProductMappingData(RQ))
            {
                divMsgForMapping.Style.Add("display", "none");
                BootstrapAlert.BootstrapAlertMessage(divMsgForUnMapping, "Record unmapped successfully", BootstrapAlertType.Success);
                fillAPMrequestobject();
                fillmappeddata();
            }
        }

        protected void btnUnmapAll_Click(object sender, EventArgs e)
        {

            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            List<MDMSVC.DC_Accomodation_ProductMapping> RQ = new List<MDMSVC.DC_Accomodation_ProductMapping>();
            Guid myRow_Id;
            Guid myAcco_Id;
            foreach (GridViewRow row in grdAccoMaps.Rows)
            {
                int index = row.RowIndex;
                myRow_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString());
                if (myRow_Id != null)
                {
                    MDMSVC.DC_Accomodation_ProductMapping newObj = new MDMSVC.DC_Accomodation_ProductMapping
                    {
                        Accommodation_ProductMapping_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[0].ToString()),
                        Accommodation_Id = Guid.Parse(grdAccoMaps.DataKeys[index].Values[1].ToString()),
                        Status = "UNMAPPED",
                        IsActive = true,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                    };
                    RQ.Add(newObj);
                }
            }
            if (MapSvc.UpdateProductMappingData(RQ))
            {
                divMsgForMapping.Style.Add("display", "none");
                BootstrapAlert.BootstrapAlertMessage(divMsgForUnMapping, "Record unmapped successfully", BootstrapAlertType.Success);
                fillAPMrequestobject();
                fillmappeddata();
            }
        }


    }
}