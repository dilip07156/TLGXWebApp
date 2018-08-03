using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.roomtype
{
    public partial class searchRoomTypes : System.Web.UI.UserControl
    {
        #region Variables 
        #region Tab1
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MappingSVCs _mapping = new MappingSVCs();
        AccomodationSVC _accoService = new AccomodationSVC();



        #endregion
        #region Tab2
        #endregion
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Tab1
                BindPageDataForSearchBySupplier();
                #endregion
                #region Tab2
                BindPageDataForSearchByProduct();
                #endregion
            }

        }

        #region Tab1
        private void BindPageDataForSearchBySupplier()
        {
            try
            {
                fillsuppliers();
                fillcountries(ddlCountryBySupplier);
                fillmappingstatus(ddlStatusBySupplier);
                fillAccomodationPriority(ddlPriority);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void fillmappingstatus(DropDownList ddl)
        {
            fillAttributeValues(ddl, "MappingStatus", "ProductSupplierMapping");
        }
        private void fillAttributeValues(DropDownList ddl, string Attribute_Name, string OptionFor)
        {
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(OptionFor, Attribute_Name).MasterAttributeValues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        private void fillsuppliers()
        {
            //ddl.Items.Clear();
            //ddl.DataSource = 
            //ddl.DataTextField = "Name";
            //ddl.DataValueField = "Supplier_Id";
            //ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("-ALL-", "0"));
            ddlSupplierNameBySupplier.DataSource = masterSVc.GetSupplierByEntity(new MDMSVC.DC_Supplier_Search_RQ { PageNo = 0, PageSize = int.MaxValue, EntityType = "Accommodation", CategorySubType_ID = "Apartments" });
            ddlSupplierNameBySupplier.DataValueField = "Supplier_Id";
            ddlSupplierNameBySupplier.DataTextField = "Name";
            ddlSupplierNameBySupplier.DataBind();
        }
        private void fillcountries(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = masterSVc.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        protected void ddlCountryBySupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcities(ddlCityBySupplier, ddlCountryBySupplier);
        }
        public void fillcities(DropDownList ddl, DropDownList ddlp)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {
                //ddl.DataSource = masterdata.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddl.DataSource = masterSVc.GetMasterCityData(ddlp.SelectedItem.Value);
                ddl.DataValueField = "City_ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        protected void btnSearchBySupplier_Click(object sender, EventArgs e)
        {
            bool blnDataExist = false;
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            //intPageIndex = 0;
            SearchRoomTypeMappingData(ref blnDataExist, 0);
        }
        public void MappingButtonShowHide(bool blnStatus)
        {
            btnMapSelectedBySupplier.Visible = blnStatus;
            btnMapAllBySupplier.Visible = blnStatus;
            btnTTFUAllBySupplier.Visible = blnStatus;
            btnTTFUSelectedBySupplier.Visible = blnStatus;
            if (blnStatus)
                divPagging.Style.Add(HtmlTextWriterStyle.Display, "block");
            else if (!blnStatus)
                divPagging.Style.Add(HtmlTextWriterStyle.Display, "none");
            ddlPageSizeBySupplier.Visible = blnStatus;
        }
        private void SearchRoomTypeMappingData(ref bool blnDataExist, int pageIndex)
        {
            try
            {
                MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRQ _objSearch = new MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRQ();

                //For Binding
                if (ddlSupplierNameBySupplier.SelectedValue != "0")
                    _objSearch.Supplier_Id = Guid.Parse(ddlSupplierNameBySupplier.SelectedValue);
                if (ddlCountryBySupplier.SelectedValue != "0")
                    _objSearch.Country = Guid.Parse(ddlCountryBySupplier.SelectedValue);
                if (ddlCityBySupplier.SelectedValue != "0")
                    _objSearch.City = Guid.Parse(ddlCityBySupplier.SelectedValue);
                if (ddlMappingTypeBySupplier.SelectedValue != "0")
                    _objSearch.MappingType = ddlMappingTypeBySupplier.SelectedItem.Text;
                if (ddlStatusBySupplier.SelectedValue != "0")
                    _objSearch.Status = ddlStatusBySupplier.SelectedItem.Text;
                if (!string.IsNullOrWhiteSpace(txtProductNameBySupplier.Text))
                    _objSearch.ProductName = txtProductNameBySupplier.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtSupplierRoomName.Text))
                    _objSearch.SupplierRoomName = txtSupplierRoomName.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtTLGXAccoId.Text))
                    _objSearch.TLGXAccoId = txtTLGXAccoId.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtTLGXAccoRoomId.Text))
                    _objSearch.TLGXAccoRoomId = txtTLGXAccoRoomId.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtCompanyHotelId.Text))
                    _objSearch.CompanyHotelID = Convert.ToInt32(txtCompanyHotelId.Text.Trim());

                _objSearch.PageSize = Convert.ToInt32(ddlPageSizeBySupplier.SelectedItem.Text);

                if (ddlPriority.SelectedValue != "0")
                    _objSearch.Priority = Convert.ToInt32(ddlPriority.SelectedValue);

                _objSearch.PageNo = pageIndex;
                var res = _mapping.GetAccomodationSupplierRoomTypeMapping_Search(_objSearch);
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        blnDataExist = true;
                        MappingButtonShowHide(blnDataExist);
                        grdRoomTypeMappingSearchResultsBySupplier.VirtualItemCount = res[0].TotalRecords;
                        lblSupplierRoomSearchCount.Text = res[0].TotalRecords.ToString();
                    }
                    else
                    {
                        lblSupplierRoomSearchCount.Text = "0";
                        blnDataExist = false;
                        MappingButtonShowHide(blnDataExist);
                    }
                }
                else
                {
                    lblSupplierRoomSearchCount.Text = "0";
                    blnDataExist = false;
                    MappingButtonShowHide(blnDataExist);
                }
                grdRoomTypeMappingSearchResultsBySupplier.DataSource = res;
                grdRoomTypeMappingSearchResultsBySupplier.PageIndex = pageIndex;
                grdRoomTypeMappingSearchResultsBySupplier.PageSize = Convert.ToInt32(ddlPageSizeBySupplier.SelectedItem.Text);
                grdRoomTypeMappingSearchResultsBySupplier.DataBind();
            }
            catch (Exception ex)
            {



                throw;
            }
        }
        protected void btnResetBySupplier_Click(object sender, EventArgs e)
        {
            ClearSelectionForSearchBySupplier();
        }
        private void ClearSelectionForSearchBySupplier()
        {
            ddlSupplierNameBySupplier.SelectedValue = "0";
            ddlCountryBySupplier.SelectedValue = "0";
            ddlCityBySupplier.Items.Clear();
            ddlCityBySupplier.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlMappingTypeBySupplier.SelectedValue = "0";
            ddlStatusBySupplier.SelectedValue = "0";
            ddlPageSizeBySupplier.SelectedValue = "5";
            txtProductNameBySupplier.Text = string.Empty;
            txtSupplierRoomName.Text = string.Empty;
            MappingButtonShowHide(false);
            lblSupplierRoomSearchCount.Text = "0";
            grdRoomTypeMappingSearchResultsBySupplier.DataSource = null;
            grdRoomTypeMappingSearchResultsBySupplier.DataBind();
            txtTLGXAccoId.Text = string.Empty;
            txtTLGXAccoRoomId.Text = string.Empty;
            txtCompanyHotelId.Text = string.Empty;

        }
        protected void grdRoomTypeMappingSearchResultsBySupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            Guid myRow_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[0].ToString());
            if (e.CommandName == "OPENSPM")
            {
                MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQ = new MDMSVC.DC_Mapping_ProductSupplier_Search_RQ();
                if (grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[1] != null)
                {
                    RQ.Accommodation_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[1].ToString());
                }
                if (grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[2] != null)
                {
                    RQ.Supplier_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[2].ToString());
                }
                if (grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[3] != null)
                {
                    RQ.SupplierProductCode = grdRoomTypeMappingSearchResultsBySupplier.DataKeys[index].Values[3].ToString();
                }

                RQ.PageSize = int.MaxValue;

                var searchAPM = _mapping.GetProductMappingData(RQ);
                if (searchAPM != null && searchAPM.Count > 0)
                {
                    UpdateSupplierProductMapping.CalledFrom = "RTM";
                    UpdateSupplierProductMapping.Accommodation_ProductMapping_Id = searchAPM[0].Accommodation_ProductMapping_Id;
                    UpdateSupplierProductMapping.SupplierId = searchAPM[0].SupplierId;
                    UpdateSupplierProductMapping.SupplierName = searchAPM[0].SupplierName;
                    UpdateSupplierProductMapping.ProductId = searchAPM[0].ProductId;
                    UpdateSupplierProductMapping.ProductName = searchAPM[0].ProductName;
                    UpdateSupplierProductMapping.ProductType = searchAPM[0].ProductType;
                    UpdateSupplierProductMapping.Street = searchAPM[0].FullAddress;
                    UpdateSupplierProductMapping.TelephoneNumber = searchAPM[0].TelephoneNumber;
                    UpdateSupplierProductMapping.CountryCode = searchAPM[0].CountryCode;
                    UpdateSupplierProductMapping.CountryName = searchAPM[0].CountryName;
                    UpdateSupplierProductMapping.CityCode = searchAPM[0].CityCode;
                    UpdateSupplierProductMapping.CityName = searchAPM[0].CityName;
                    UpdateSupplierProductMapping.Status = searchAPM[0].Status;
                }
                UpdateSupplierProductMapping.BindData();
                ScriptManager.RegisterStartupScript((Control)grdRoomTypeMappingSearchResultsBySupplier, ((Control)grdRoomTypeMappingSearchResultsBySupplier).GetType(), "Pop" + index.ToString(), "showProductMappingModal();", true);
                //ClientScriptManager.RegisterStartupScript(this.GetType(), "tmp", "CreateClickableLinks();", false);
            }
        }
        protected void grdRoomTypeMappingSearchResultsBySupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bool _blnDataExist = false;
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            //intPageIndex = e.NewPageIndex;
            SearchRoomTypeMappingData(ref _blnDataExist, e.NewPageIndex);

        }
        protected void grdRoomTypeMappingSearchResultsBySupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField hdnRoomDescription = (HiddenField)e.Row.FindControl("hdnRoomDescription");
                //if (hdnRoomDescription != null)
                //{
                //    e.Row.Cells[3].ToolTip = HttpUtility.HtmlEncode(hdnRoomDescription.Value);
                //}
                Guid Accoid = Guid.Parse(Convert.ToString(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[e.Row.RowIndex][1]));
                var result = _accoService.GetRoomDetails_RoomCategory(Accoid);
                DropDownList ddlSuggestedRoomInGridBySupplier = (DropDownList)e.Row.FindControl("ddlSuggestedRoomInGridBySupplier");
                DropDownList ddlMappingStatusInGridBySupplier = (DropDownList)e.Row.FindControl("ddlMappingStatusInGridBySupplier");
                // HtmlControl aHelp = (HtmlControl)e.Row.FindControl("aHelp");

                HtmlControl ddlSuggestions = (HtmlControl)e.Row.FindControl("ddlSuggestions");
                var mappingStatus = ((TLGX_Consumer.MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS)e.Row.DataItem).MappingStatus;

                if (ddlSuggestions != null)
                {
                    HtmlContainerControl btnSuggestionis = (HtmlContainerControl)(ddlSuggestions.FindControl("btnSuggestionis"));
                    if (mappingStatus.ToLower() == "mapped")
                        btnSuggestionis.Attributes.Add("class", "btn btn-success dropdown-toggle roomtype");

                    if (mappingStatus.ToLower() == "review")
                        btnSuggestionis.Attributes.Add("class", "btn btn-warning dropdown-toggle roomtype");

                    if (mappingStatus.ToLower() == "unmapped")
                        btnSuggestionis.Attributes.Add("class", "btn btn-danger dropdown-toggle roomtype");
                }

                //  Label lblSupplierRoomTypeName = (Label)e.Row.FindControl("lblSupplierRoomTypeName");
                HtmlInputHidden hdnRoomCount = (HtmlInputHidden)e.Row.FindControl("hdnRoomCount");
                HtmlTextArea txtSuggestedRoomInfoInGridBySupplier = (HtmlTextArea)e.Row.FindControl("txtSuggestedRoomInfoInGridBySupplier");
                //HtmlInputHidden hdnRoomDescription = (HtmlInputHidden)e.Row.FindControl("hdnRoomDescription");
                int intRoomCount = 0;
                //if(lblSupplierRoomTypeName != null && hdnRoomDescription != null)
                //    lblSupplierRoomTypeName.ToolTip = HttpUtility.HtmlEncode(hdnRoomDescription.Value);
                bool hasRoom = int.TryParse(hdnRoomCount.Value, out intRoomCount);
                if (hdnRoomCount != null && hasRoom && intRoomCount > 0)
                {
                    if (ddlSuggestedRoomInGridBySupplier != null)
                    {
                        ddlSuggestedRoomInGridBySupplier.Items.Clear();

                        //ddlSuggestedRoomInGridBySupplier.Style.Add(HtmlTextWriterStyle.Display, "block");
                        ddlSuggestedRoomInGridBySupplier.Style.Add(HtmlTextWriterStyle.Display, "none");
                        if (ddlSuggestions != null)
                        {
                            ddlSuggestions.Style.Add(HtmlTextWriterStyle.Display, "block");
                        }

                        bool blnHaveRoomInfo_Id = ((TLGX_Consumer.MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS)e.Row.DataItem).Accommodation_RoomInfo_Id.HasValue;
                        string RoomInfo_Name = Convert.ToString(((TLGX_Consumer.MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS)e.Row.DataItem).Accommodation_RoomInfo_Name);
                        string RoomInfo_Category = Convert.ToString(((TLGX_Consumer.MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS)e.Row.DataItem).Accommodation_RoomInfo_Category);
                        if (blnHaveRoomInfo_Id && RoomInfo_Name != null && RoomInfo_Name != "")
                        {
                            string Accommodation_RoomInfo_Id = Convert.ToString(((TLGX_Consumer.MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS)e.Row.DataItem).Accommodation_RoomInfo_Id.Value);
                            string strRoomInfoName = RoomInfo_Name + (string.IsNullOrEmpty(RoomInfo_Category) ? string.Empty : "<br/>(" + RoomInfo_Category + ")");
                            ddlSuggestedRoomInGridBySupplier.Items.Add(new ListItem(strRoomInfoName, Accommodation_RoomInfo_Id));
                            if ((System.Web.UI.HtmlControls.HtmlContainerControl)(ddlSuggestions.FindControl("btnSuggestionis")) != null)
                            {
                                var innerHtml = ((System.Web.UI.HtmlControls.HtmlContainerControl)(ddlSuggestions.FindControl("btnSuggestionis"))).InnerHtml;
                                ((System.Web.UI.HtmlControls.HtmlContainerControl)(ddlSuggestions.FindControl("btnSuggestionis"))).InnerHtml = innerHtml.Replace("-Select-", strRoomInfoName);
                            }

                            //ddlSuggestions.F

                            //if (ddlSuggestedRoomInGridBySupplier.Items.FindByValue(Accommodation_RoomInfo_Id) != null)
                            //    ddlSuggestedRoomInGridBySupplier.Items.FindByValue(Accommodation_RoomInfo_Id).Selected = true;
                            //ddlSuggestedRoomInGridBySupplier.Items.FindByValue(Accommodation_RoomInfo_Id).Selected = true;
                        }
                        else
                        {
                            ddlSuggestedRoomInGridBySupplier.Items.Add(new ListItem("Select", "0"));
                        }

                    }
                }
                else
                {
                    if (ddlSuggestedRoomInGridBySupplier != null)
                        ddlSuggestedRoomInGridBySupplier.Style.Add(HtmlTextWriterStyle.Display, "none");
                    if (ddlSuggestions != null)
                        ddlSuggestions.Style.Add(HtmlTextWriterStyle.Display, "none");
                }

                if (ddlMappingStatusInGridBySupplier != null)
                {
                    //var mappingStatus = ((TLGX_Consumer.MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRS)e.Row.DataItem).MappingStatus;
                    if (mappingStatus != null)
                    {
                        foreach (ListItem item in ddlMappingStatusInGridBySupplier.Items)
                        {
                            if (item.Text.ToLower() == mappingStatus.ToLower())
                            {
                                ddlMappingStatusInGridBySupplier.SelectedIndex = ddlMappingStatusInGridBySupplier.Items.IndexOf(ddlMappingStatusInGridBySupplier.Items.FindByText(System.Web.HttpUtility.HtmlDecode(item.Text)));
                            }
                        }
                        //if (ddlMappingStatusInGridBySupplier.Items.FindByValue(Convert.ToString(mappingStatus)) != null)
                        //    ddlMappingStatusInGridBySupplier.Items.FindByValue(Convert.ToString(mappingStatus)).Selected = true;

                        //To Show Suggested Room Info for all (Suggested By Matt)
                        //if (mappingStatus.ToUpper() == "MAPPED")
                        //{
                        //    if (txtSuggestedRoomInfoInGridBySupplier != null)
                        //    {
                        //        txtSuggestedRoomInfoInGridBySupplier.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //    }
                        //    //else { txtSuggestedRoomInfoInGridBySupplier.Style.Add(HtmlTextWriterStyle.Display, "block"); }
                        //}
                        //else 

                        //Commented for not getting set on basic of room count
                        //if (mappingStatus.ToUpper() == "UNMAPPED")
                        //{
                        //    if (hasRoom && intRoomCount == 0)
                        //    {
                        //        ddlMappingStatusInGridBySupplier.ClearSelection();
                        //        //ddlMappingStatusInGridBySupplier.Items.FindByValue("ADD").Selected = true;
                        //        //If no data found the only select is selected
                        //        ddlMappingStatusInGridBySupplier.Items.FindByValue("0").Selected = true;

                        //    }
                        //    else if (hasRoom && intRoomCount > 0 && ddlSuggestedRoomInGridBySupplier.SelectedValue != "0")
                        //    {
                        //        ddlMappingStatusInGridBySupplier.ClearSelection();
                        //        ddlMappingStatusInGridBySupplier.Items.FindByValue("REVIEW").Selected = true;

                        //        if (ddlSuggestions != null)
                        //        {
                        //            HtmlContainerControl btnSuggestionis = (HtmlContainerControl)(ddlSuggestions.FindControl("btnSuggestionis"));
                        //            btnSuggestionis.Attributes.Add("class", "btn btn-warning dropdown-toggle roomtype");
                        //        }

                        //    }
                        //    else if (hasRoom && intRoomCount > 0 && ddlSuggestedRoomInGridBySupplier.SelectedValue == "0")
                        //    {

                        //    }
                        //}
                    }
                }
            }
        }
        protected void btnMapSelectedBySupplier_Click(object sender, EventArgs e)
        {
            Guid myRow_Id;
            Guid myAcco_Id;
            MDMSVC.DC_Message _newmsg = new MDMSVC.DC_Message();
            List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update> _lstUpdate = new List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update>();
            int localPageIndex = grdRoomTypeMappingSearchResultsBySupplier.PageIndex;
            foreach (GridViewRow row in grdRoomTypeMappingSearchResultsBySupplier.Rows)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    int rowindex = row.RowIndex;
                    myRow_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[rowindex].Values[0].ToString());
                    myAcco_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[rowindex].Values[1].ToString());
                    //Check dropdown
                    DropDownList ddlMappingStatusInGridBySupplier = (DropDownList)row.FindControl("ddlMappingStatusInGridBySupplier");
                    if (ddlMappingStatusInGridBySupplier != null)
                    {
                        if (ddlMappingStatusInGridBySupplier.SelectedValue == "0")
                        {

                        }
                        else if (ddlMappingStatusInGridBySupplier.SelectedValue.ToUpper() == "ADD") //ADD
                        {
                            HtmlTextArea txtSuggestedRoomInfoInGridBySupplier = (HtmlTextArea)row.FindControl("txtSuggestedRoomInfoInGridBySupplier");
                            if (txtSuggestedRoomInfoInGridBySupplier != null && !(string.IsNullOrWhiteSpace(txtSuggestedRoomInfoInGridBySupplier.Value)))
                            {
                                _lstUpdate.Add(new MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update()
                                {
                                    Accommodation_Id = myAcco_Id,
                                    RoomCategory = txtSuggestedRoomInfoInGridBySupplier.Value.Trim(),
                                    Accommodation_SupplierRoomTypeMapping_Id = myRow_Id,
                                    Status = ddlMappingStatusInGridBySupplier.SelectedValue,
                                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                                });
                            }
                        }
                        else
                        {
                            DropDownList ddlSuggestedRoomInGridBySupplier = (DropDownList)row.FindControl("ddlSuggestedRoomInGridBySupplier");
                            HtmlInputHidden hdnAccommodation_RoomInfo_Id = (HtmlInputHidden)row.FindControl("hdnAccommodation_RoomInfo_Id");
                            Guid? Accommodation_RoomInfo_Id = Guid.Empty;
                            if ((ddlSuggestedRoomInGridBySupplier != null && ddlSuggestedRoomInGridBySupplier.SelectedValue != "0") || (hdnAccommodation_RoomInfo_Id != null && !String.IsNullOrWhiteSpace(hdnAccommodation_RoomInfo_Id.Value)))
                            {
                                //if (ddlSuggestedRoomInGridBySupplier.SelectedValue != "0")
                                //    Accommodation_RoomInfo_Id = Guid.Parse(ddlSuggestedRoomInGridBySupplier.SelectedValue);
                                //else if (hdnAccommodation_RoomInfo_Id.Value != null)
                                //    Accommodation_RoomInfo_Id = Guid.Parse(hdnAccommodation_RoomInfo_Id.Value);

                                if (hdnAccommodation_RoomInfo_Id.Value != null && hdnAccommodation_RoomInfo_Id.Value != string.Empty)
                                {
                                    Accommodation_RoomInfo_Id = Guid.Parse(hdnAccommodation_RoomInfo_Id.Value);

                                    _lstUpdate.Add(new MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update()
                                    {
                                        Accommodation_Id = myAcco_Id,
                                        Accommodation_RoomInfo_Id = Accommodation_RoomInfo_Id,
                                        Accommodation_SupplierRoomTypeMapping_Id = myRow_Id,
                                        //Status = ddlMappingStatusInGridBySupplier.SelectedValue == "REVIEW" ? "MAPPED" : ddlMappingStatusInGridBySupplier.SelectedValue,
                                        Status = ddlMappingStatusInGridBySupplier.SelectedValue,
                                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                                    });
                                }
                            }
                        }
                    }

                }
            }
            if (_lstUpdate.Count > 0)
            {
                _newmsg = _mapping.AccomodationSupplierRoomTypeMapping_UpdateMap(_lstUpdate);
                if (_newmsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    bool blnDataExist = false;
                    SearchRoomTypeMappingData(ref blnDataExist, localPageIndex);
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, (BootstrapAlertType)_newmsg.StatusCode);
                }
            }

        }
        protected void btnMapAllBySupplier_Click(object sender, EventArgs e)
        {
            Guid myRow_Id;
            Guid myAcco_Id;
            MDMSVC.DC_Message _newmsg = new MDMSVC.DC_Message();
            List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update> _lstUpdate = new List<MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update>();
            int localPageIndex = grdRoomTypeMappingSearchResultsBySupplier.PageIndex;
            foreach (GridViewRow row in grdRoomTypeMappingSearchResultsBySupplier.Rows)
            {

                int rowindex = row.RowIndex;
                myRow_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[rowindex].Values[0].ToString());
                myAcco_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[rowindex].Values[1].ToString());
                //Check dropdown
                DropDownList ddlMappingStatusInGridBySupplier = (DropDownList)row.FindControl("ddlMappingStatusInGridBySupplier");
                if (ddlMappingStatusInGridBySupplier != null)
                {
                    if (ddlMappingStatusInGridBySupplier.SelectedValue == "0")
                    {

                    }
                    else if (ddlMappingStatusInGridBySupplier.SelectedValue.ToUpper() == "ADD") //ADD
                    {
                        HtmlTextArea txtSuggestedRoomInfoInGridBySupplier = (HtmlTextArea)row.FindControl("txtSuggestedRoomInfoInGridBySupplier");
                        if (txtSuggestedRoomInfoInGridBySupplier != null && !(string.IsNullOrWhiteSpace(txtSuggestedRoomInfoInGridBySupplier.Value)))
                        {
                            _lstUpdate.Add(new MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update()
                            {
                                Accommodation_Id = myAcco_Id,
                                RoomCategory = txtSuggestedRoomInfoInGridBySupplier.Value.Trim(),
                                Accommodation_SupplierRoomTypeMapping_Id = myRow_Id,
                                Status = ddlMappingStatusInGridBySupplier.SelectedValue,
                                Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                            });
                        }
                    }
                    else
                    {
                        DropDownList ddlSuggestedRoomInGridBySupplier = (DropDownList)row.FindControl("ddlSuggestedRoomInGridBySupplier");
                        HtmlInputHidden hdnAccommodation_RoomInfo_Id = (HtmlInputHidden)row.FindControl("hdnAccommodation_RoomInfo_Id");
                        Guid? Accommodation_RoomInfo_Id = Guid.Empty;
                        if (ddlSuggestedRoomInGridBySupplier != null && ddlSuggestedRoomInGridBySupplier.SelectedValue != "0" || (hdnAccommodation_RoomInfo_Id != null && !String.IsNullOrWhiteSpace(hdnAccommodation_RoomInfo_Id.Value)))
                        {
                            //if (ddlSuggestedRoomInGridBySupplier.SelectedValue != "0")
                            //    Accommodation_RoomInfo_Id = Guid.Parse(ddlSuggestedRoomInGridBySupplier.SelectedValue);
                            //else 
                            if (hdnAccommodation_RoomInfo_Id.Value != null && hdnAccommodation_RoomInfo_Id.Value != string.Empty)
                            {
                                Accommodation_RoomInfo_Id = Guid.Parse(hdnAccommodation_RoomInfo_Id.Value);

                                _lstUpdate.Add(new MDMSVC.DC_Accommodation_SupplierRoomTypeMap_Update()
                                {
                                    Accommodation_Id = myAcco_Id,
                                    // Accommodation_RoomInfo_Id = Guid.Parse(ddlSuggestedRoomInGridBySupplier.SelectedValue),
                                    Accommodation_RoomInfo_Id = Accommodation_RoomInfo_Id,
                                    Accommodation_SupplierRoomTypeMapping_Id = myRow_Id,
                                    Status = ddlMappingStatusInGridBySupplier.SelectedValue == "REVIEW" ? "MAPPED" : ddlMappingStatusInGridBySupplier.SelectedValue,
                                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                                });
                            }
                        }
                    }
                }
            }
            if (_lstUpdate.Count > 0)
            {
                _newmsg = _mapping.AccomodationSupplierRoomTypeMapping_UpdateMap(_lstUpdate);
                if (_newmsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    bool blnDataExist = false;
                    SearchRoomTypeMappingData(ref blnDataExist, localPageIndex);
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, (BootstrapAlertType)_newmsg.StatusCode);
                }
            }

        }
        protected void btnTTFUAllBySupplier_Click(object sender, EventArgs e)
        {
            Guid Acco_RoomTypeMap_Id;
            MDMSVC.DC_Message _newmsg = new MDMSVC.DC_Message();
            List<MDMSVC.DC_SupplierRoomType_TTFU_RQ> _lstUpdate = new List<MDMSVC.DC_SupplierRoomType_TTFU_RQ>();
            int localPageIndex = grdRoomTypeMappingSearchResultsBySupplier.PageIndex;
            foreach (GridViewRow row in grdRoomTypeMappingSearchResultsBySupplier.Rows)
            {
                //HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)row.FindControl("chkSelect");
                //if (chkSelect != null && chkSelect.Checked)
                //{
                int rowindex = row.RowIndex;
                Acco_RoomTypeMap_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[rowindex].Values[0].ToString());
                _lstUpdate.Add(new MDMSVC.DC_SupplierRoomType_TTFU_RQ()
                {
                    Acco_RoomTypeMap_Id = Acco_RoomTypeMap_Id,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                //}
            }
            if (_lstUpdate.Count > 0)
            {
                _newmsg = _mapping.AccomodationSupplierRoomTypeMapping_TTFUALL(_lstUpdate);
                if (_newmsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    bool blnDataExist = false;
                    SearchRoomTypeMappingData(ref blnDataExist, localPageIndex);
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, (BootstrapAlertType)_newmsg.StatusCode);
                }
            }
        }
        protected void btnTTFUSelectedBySupplier_Click(object sender, EventArgs e)
        {
            Guid Acco_RoomTypeMap_Id;
            MDMSVC.DC_Message _newmsg = new MDMSVC.DC_Message();
            List<MDMSVC.DC_SupplierRoomType_TTFU_RQ> _lstUpdate = new List<MDMSVC.DC_SupplierRoomType_TTFU_RQ>();
            int localPageIndex = grdRoomTypeMappingSearchResultsBySupplier.PageIndex;

            foreach (GridViewRow row in grdRoomTypeMappingSearchResultsBySupplier.Rows)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    int rowindex = row.RowIndex;
                    Acco_RoomTypeMap_Id = Guid.Parse(grdRoomTypeMappingSearchResultsBySupplier.DataKeys[rowindex].Values[0].ToString());
                    _lstUpdate.Add(new MDMSVC.DC_SupplierRoomType_TTFU_RQ()
                    {
                        Acco_RoomTypeMap_Id = Acco_RoomTypeMap_Id,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                    });
                }
            }
            if (_lstUpdate.Count > 0)
            {
                _newmsg = _mapping.AccomodationSupplierRoomTypeMapping_TTFUALL(_lstUpdate);
                if (_newmsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    bool blnDataExist = false;
                    SearchRoomTypeMappingData(ref blnDataExist, localPageIndex);
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(divMsgForMapping, _newmsg.StatusMessage, (BootstrapAlertType)_newmsg.StatusCode);
                }
            }
        }
        protected void ddlPageSizeBySupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool blnFlag = false;
            SearchRoomTypeMappingData(ref blnFlag, 0);
        }
        #endregion

        #region Tab2
        private void BindPageDataForSearchByProduct()
        {
            try
            {
                BindCountryByProduct();
                BindMappingStatusByProduct();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BindMappingStatusByProduct()
        {
            ddlStatusByProduct.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("ProductSupplierMapping", "MappingStatus").MasterAttributeValues;
            ddlStatusByProduct.DataTextField = "AttributeValue";
            ddlStatusByProduct.DataValueField = "MasterAttributeValue_Id";
            ddlStatusByProduct.DataBind();
            ddlStatusByProduct.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        private void BindCountryByProduct()
        {
            ddlCountryByProduct.Items.Clear();
            ddlCountryByProduct.DataSource = masterSVc.GetAllCountries();
            ddlCountryByProduct.DataValueField = "Country_Id";
            ddlCountryByProduct.DataTextField = "Country_Name";
            ddlCountryByProduct.DataBind();
            ddlCountryByProduct.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void btnSearchByProduct_Click(object sender, EventArgs e)
        {
            bool blnDataExist = false;
            divMsgForMapping.Style.Add(HtmlTextWriterStyle.Display, "none");
            SearchRoomTypeMappingDataByProduct(ref blnDataExist, 0);
        }

        private void SearchRoomTypeMappingDataByProduct(ref bool blnDataExist, int pageindex = 0)
        {
            try
            {
                MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRQ _objSearch = new MDMSVC.DC_Accommodation_SupplierRoomTypeMap_SearchRQ();

                //For Binding
                if (ddlCountryByProduct.SelectedValue != "0")
                    _objSearch.Country = Guid.Parse(ddlCountryByProduct.SelectedValue);
                if (ddlCityByProduct.SelectedValue != "0")
                    _objSearch.City = Guid.Parse(ddlCityByProduct.SelectedValue);
                if (ddlStatusByProduct.SelectedValue != "0")
                    _objSearch.Status = ddlStatusBySupplier.SelectedItem.Text;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(txtCommonByProduct.Text)))

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(txtHotelNameByProduct.Text)))


                        _objSearch.PageSize = Convert.ToInt32(ddlPageSizeBySupplier.SelectedItem.Text);

                _objSearch.PageNo = pageindex;
                var res = _mapping.GetAccomodationSupplierRoomTypeMapping_Search(_objSearch);
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        blnDataExist = true;
                        MappingButtonShowHide(blnDataExist);
                        grdRoomTypeMappingSearchResultsBySupplier.VirtualItemCount = res[0].TotalRecords;
                        lblSupplierRoomSearchCount.Text = res[0].TotalRecords.ToString();
                    }
                    else
                    {
                        lblSupplierRoomSearchCount.Text = "0";
                        blnDataExist = false;
                        MappingButtonShowHide(blnDataExist);
                    }
                }
                else
                {
                    lblSupplierRoomSearchCount.Text = "0";
                    blnDataExist = false;
                    MappingButtonShowHide(blnDataExist);
                }
                grdRoomTypeMappingSearchResultsBySupplier.DataSource = res;
                grdRoomTypeMappingSearchResultsBySupplier.PageIndex = pageindex;
                grdRoomTypeMappingSearchResultsBySupplier.PageSize = Convert.ToInt32(ddlPageSizeBySupplier.SelectedItem.Text);
                grdRoomTypeMappingSearchResultsBySupplier.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnResetByProduct_Click(object sender, EventArgs e)
        {
            ClearSelectionForSearchByProduct();

        }

        private void ClearSelectionForSearchByProduct()
        {
            ddlCountryByProduct.SelectedValue = "0";
            ddlCityByProduct.Items.Clear();
            ddlCityByProduct.Items.Insert(0, new ListItem("---ALL---", "0"));
            ddlStatusByProduct.SelectedValue = "0";
        }

        protected void ddlCountryByProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCityByProduct();
        }

        private void BindCityByProduct()
        {
            ddlCityByProduct.Items.Clear();
            if (ddlCountryByProduct.SelectedItem.Value != "0")
            {
                ddlCityByProduct.DataSource = masterSVc.GetMasterCityData(ddlCountryByProduct.SelectedItem.Value);
                ddlCityByProduct.DataValueField = "City_ID";
                ddlCityByProduct.DataTextField = "Name";
                ddlCityByProduct.DataBind();
            }
            ddlCityByProduct.Items.Insert(0, new ListItem("---ALL---", "0"));
        }


        #endregion

        protected void grdRoomTypeMappingSearchResultsByProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdRoomTypeMappingSearchResultsByProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdRoomTypeMappingSearchResultsByProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
    }
}