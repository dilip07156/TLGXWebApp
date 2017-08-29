using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.App_Code;
using System.Web.UI.HtmlControls;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class CityMap : System.Web.UI.UserControl
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        public DataTable dtCountryMappingSearchResults = new DataTable();
        public DataTable dtCountrMappingDetail = new DataTable();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        MasterDataDAL masters = new MasterDataDAL();
        public static string AttributeOptionFor = "ProductSupplierMapping";
        public static string SortBy = "";
        public static string SortEx = "";
        public static int PageIndex = 0;
        public static string MatchedStatus = "";
        public static string MatchedCountryName = "";
        public static string MatchedCityName = "";
        public static int MatchedPageIndex = 0;
        public static int SimilarPageIndex = 0;
        public static string SimilarCountryName = "";
        public static string SimilarCityName = "";
        public static Guid MappedCountry_ID = Guid.Empty;
        public static Guid MappedCity_ID = Guid.Empty;
        public List<MDMSVC.DC_City_Master_DDL> _lstCityMaster = new List<DC_City_Master_DDL>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();
                fillcountries(ddlMasterCountry);
                fillmappingstatus(ddlStatus);
                btnMapSelected.Visible = false;
                btnMapAll.Visible = false;
            }
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
        private void fillcountries(DropDownList ddl)
        {
            //ddl.DataSource = objMasterDataDAL.GetMasterCountryData("");
            //ddl.DataValueField = "Country_ID";
            //ddl.DataTextField = "Name";

            ddl.DataSource = masterSVc.GetAllCountries();
            ddl.DataValueField = "Country_Id";
            ddl.DataTextField = "Country_Name";
            ddl.DataBind();
        }

        private void fillcities(DropDownList ddl, DropDownList ddlp)
        {
            ddl.Items.Clear();
            if (ddlp.SelectedItem.Value != "0")
            {
                //ddl.DataSource = objMasterDataDAL.GetMasterCityData(new Guid(ddlp.SelectedItem.Value));
                ddl.DataSource = masterSVc.GetMasterCityData(ddlp.SelectedItem.Value);
                ddl.DataValueField = "City_ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = objMasterDataDAL.GetSupplierMasterData();
            ddlSupplierName.DataSource = masterSVc.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }


        protected void grdCityMaps_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            if (myGridView.Controls.Count > 0)
                AddSuperHeader(myGridView);
        }


        private static void AddSuperHeader(GridView gridView)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            myNewRow.Cells.Add(MakeCell("Supplier", 5));
            myNewRow.Cells.Add(MakeCell("System", 5));
            myNewRow.Cells.Add(MakeCell("", 2));

            myTable.Rows.AddAt(0, myNewRow);
        }

        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { ColumnSpan = span, Text = text ?? string.Empty, CssClass = "table-header" };
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
        }

        private void fillsimilarcities()
        {
            DropDownList ddlSimilarProducts = (DropDownList)frmEditCityMap.FindControl("ddlSimilarProducts");
            GridView grdSimilarProducts = (GridView)frmEditCityMap.FindControl("grdSimilarProducts");
            dvMsg.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            MDMSVC.DC_CityMapping_RQ RQParam = new MDMSVC.DC_CityMapping_RQ();
            if (ddlMatchingStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlMatchingStatus.SelectedItem.Text;
            RQParam.SupplierCountryName = SimilarCountryName;
            RQParam.SupplierCityName = SimilarCityName;
            RQParam.PageNo = SimilarPageIndex;
            RQParam.PageSize = Convert.ToInt32(ddlSimilarProducts.SelectedItem.Text);
            RQParam.SortBy = "CityName";
            RQParam.ResultSet = "onlycity";
            var res = mapperSVc.GetCityMappingData(RQParam);
            grdSimilarProducts.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdSimilarProducts.VirtualItemCount = res[0].TotalRecords;
                }
            }
            grdSimilarProducts.PageIndex = SimilarPageIndex;
            grdSimilarProducts.PageSize = Convert.ToInt32(ddlSimilarProducts.SelectedItem.Text);
            grdSimilarProducts.DataBind();
        }

        private void fillmatchingdata(string from)
        {
            //dvMsg.Style.Add("display", "none");
            //dvMsg1.Style.Add("display", "none");
            MDMSVC.DC_CityMapping_RQ RQParam = new MDMSVC.DC_CityMapping_RQ();
            if (ddlMatchingStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlMatchingStatus.SelectedItem.Text;
            RQParam.StatusExcept = MatchedStatus;
            RQParam.SupplierCountryName = MatchedCountryName;
            RQParam.SupplierCityName = MatchedCityName;
            RQParam.PageNo = MatchedPageIndex;
            RQParam.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            RQParam.SortBy = (SortBy + " " + SortEx).Trim();
            RQParam.IsExact = ckboxIsExactMatch.Checked;
            var res = mapperSVc.GetCityMappingData(RQParam);
            grdMatchingCity.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    if (from != "status")
                    {
                        ddlMatchingStatus.Items.Clear();
                        var disstatus = (from s in res orderby s.Status select s.Status).Distinct();
                        ddlMatchingStatus.DataSource = disstatus;
                        ddlMatchingStatus.DataBind();
                        ddlMatchingStatus.Items.Insert(0, new ListItem("--ALL--", "0"));
                        //IEnumerable<MDMSVC.DC_CityMapping> filteredList = res
                        //  .GroupBy(stat => stat.Status)
                        //  .Select(group => group.First());
                    }
                    grdMatchingCity.VirtualItemCount = res[0].TotalRecords;
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
            grdMatchingCity.PageIndex = MatchedPageIndex;
            grdMatchingCity.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            grdMatchingCity.DataBind();
        }

        private void fillmappingdata()
        {
            MDMSVC.DC_CityMapping_RQ RQ = new MDMSVC.DC_CityMapping_RQ();
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            Guid selCity_ID = Guid.Empty;
            string selCity = "";
            if (ddlSupplierName.SelectedItem.Value != "0")
                //selSupplier_ID = 
                RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
            if (ddlMasterCountry.SelectedItem.Value != "0")
                //selCountry_ID = 
                RQ.Country_Id = new Guid(ddlMasterCountry.SelectedItem.Value);
            if (ddlCity.SelectedItem.Value != "0")
            {
                //selCity_ID = 
                //selCity = 
                RQ.City_Id = new Guid(ddlCity.SelectedItem.Value);
                RQ.CityName = ddlCity.SelectedItem.Text;
            }
            if (ddlStatus.SelectedItem.Value != "0")
                RQ.Status = ddlStatus.SelectedItem.Text;
            RQ.SupplierCountryName = txtSuppCountry.Text;
            RQ.SupplierCityName = txtSuppCity.Text;

            RQ.PageNo = PageIndex;
            RQ.SortBy = (SortBy + " " + SortEx).Trim();
            RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

            var res = mapperSVc.GetCityMappingData(RQ);
            grdCityMaps.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    //Getting distinct Country form the list
                    //List<string> _lstCountryID = new List<string>();
                    //List<Guid?> _lstCountryID = res.Select(obj => obj.Country_Id).Distinct().ToList();
                    //_lstCityMaster =

                    grdCityMaps.VirtualItemCount = res[0].TotalRecords;
                    lblTotalCount.Text = res[0].TotalRecords.ToString();
                }
                else
                    lblTotalCount.Text = "0";
            }
            else
                lblTotalCount.Text = "0";
            grdCityMaps.PageIndex = PageIndex;
            grdCityMaps.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text); ;
            //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
            grdCityMaps.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            ddlMasterCountry.SelectedIndex = 0;
            ddlSupplierName.SelectedIndex = 0;
            ddlShowEntries.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("---ALL---", "0"));
            grdCityMaps.DataSource = null;
            grdCityMaps.DataBind();
            frmEditCityMap.DataSource = null;
            frmEditCityMap.DataBind();
            btnMapSelected.Visible = false;
            btnMapAll.Visible = false;
            lblTotalCount.Text = "0";
        }

        protected void ddlMasterCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            fillcities(ddlCity, ddlMasterCountry);
        }

        protected void grdCityMaps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            PageIndex = e.NewPageIndex;
            fillmappingdata();
        }

        protected void grdCityMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                if (e.CommandName == "Select")
                {
                    dvMsg.Style.Add("display", "none");
                    dvMsg1.Style.Add("display", "none");
                    grdMatchingCity.DataSource = null;
                    grdMatchingCity.DataBind();
                    dvMatchingRecords.Visible = false;
                    List<MDMSVC.DC_CityMapping> obj = new List<MDMSVC.DC_CityMapping>();
                    obj.Add(new MDMSVC.DC_CityMapping
                    {
                        CityMapping_Id = myRow_Id,
                        //MapID = Convert.ToInt32(grdCityMaps.Rows[index].Cells[0].Text),
                        SupplierName = grdCityMaps.Rows[index].Cells[1].Text,
                        CountryCode = grdCityMaps.Rows[index].Cells[2].Text,
                        CountryName = grdCityMaps.Rows[index].Cells[3].Text,
                        CityCode = grdCityMaps.Rows[index].Cells[4].Text,
                        CityName = grdCityMaps.Rows[index].Cells[5].Text,
                        MasterCountryCode = grdCityMaps.Rows[index].Cells[6].Text,
                        MasterCountryName = grdCityMaps.Rows[index].Cells[7].Text,
                        MasterCityCode = grdCityMaps.Rows[index].Cells[10].Text,
                        Master_CityName = grdCityMaps.Rows[index].Cells[11].Text,
                        Status = grdCityMaps.Rows[index].Cells[12].Text
                    });

                    //SupplierMasters sData = new SupplierMasters();
                    //sData = masters.GetSupplierDataByMapping_Id("city", myRow_Id);
                    //string supCode = sData.Code;

                    var result = masterSVc.GetSupplierDataByMapping_Id("CITY",Convert.ToString(myRow_Id));
                    string supCode = string.Empty;
                    if (result != null)
                        supCode = result.Code;

                    frmEditCityMap.Visible = true;
                    frmEditCityMap.ChangeMode(FormViewMode.Edit);
                    frmEditCityMap.DataSource = obj;
                    frmEditCityMap.DataBind();


                    Label lblSupplierName = (Label)frmEditCityMap.FindControl("lblSupplierName");
                    Label lblSupplierCode = (Label)frmEditCityMap.FindControl("lblSupplierCode");
                    Label lblSupCountryName = (Label)frmEditCityMap.FindControl("lblSupCountryName");
                    Label lblSupCountryCode = (Label)frmEditCityMap.FindControl("lblSupCountryCode");
                    Label lblStateName = (Label)frmEditCityMap.FindControl("lblStateName");
                    Label lblCityName = (Label)frmEditCityMap.FindControl("lblCityName");
                    Label lblCityCode = (Label)frmEditCityMap.FindControl("lblCityCode");
                    DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
                    TextBox txtSystemCountryCode = (TextBox)frmEditCityMap.FindControl("txtSystemCountryCode");
                    DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
                    TextBox txtSystemCityCode = (TextBox)frmEditCityMap.FindControl("txtSystemCityCode");
                    DropDownList ddlStatus = (DropDownList)frmEditCityMap.FindControl("ddlStatus");
                    TextBox txtSystemRemark = (TextBox)frmEditCityMap.FindControl("txtSystemRemark");
                    TextBox txtSystemStateCode = (TextBox)frmEditCityMap.FindControl("txtSystemStateCode");
                    TextBox txtSystemStateName = (TextBox)frmEditCityMap.FindControl("txtSystemStateName");
                    Button btnAddCity = (Button)frmEditCityMap.FindControl("btnAddCity");

                    fillcountries(ddlSystemCountryName);
                    fillmappingstatus(ddlStatus);
                    lblSupplierName.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[1].Text);
                    lblSupplierCode.Text = "(" + supCode + ")"; ;
                    lblSupCountryName.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[3].Text);
                    lblSupCountryCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[2].Text);
                    lblStateName.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[6].Text);
                    lblCityName.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[5].Text);
                    lblCityCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[4].Text);

                    txtSystemCountryCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[7].Text);
                    txtSystemCityCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[10].Text);
                    txtSystemRemark.Text = masters.GetRemarksForMapping("city", myRow_Id);
                    //if (grdCityMaps.Rows[index].Cells[12].Text.ToString() == "REVIEW" || grdCityMaps.Rows[index].Cells[12].Text.ToString() == "MAPPED")
                    //{
                    ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[8].Text)));
                    fillcities(ddlSystemCityName, ddlSystemCountryName);
                    ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[13].Text)));
                    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[11].Text)));
                    txtSystemCityCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[10].Text);
                    //}
                    if (ddlSystemCityName.SelectedIndex == 0)
                    {
                        //var res = objMasterDataDAL.GetMasterCityDatawithCode(new Guid(ddlSystemCountryName.SelectedItem.Value));
                        var res = masterSVc.GetMasterCityData(ddlSystemCountryName.SelectedItem.Value);
                        string ccode = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[4].Text);
                        string cname = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[5].Text);

                        if (cname != null)
                        {
                            if (cname != "&nbsp;" && cname != "")
                            {
                                IEnumerable<MDMSVC.DC_City_Master_DDL> querynamed = res.Where(city => city.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper().Equals(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper()));
                                if (querynamed.Count() > 0)
                                {
                                    foreach (MDMSVC.DC_City_Master_DDL resc in querynamed)
                                    {
                                        ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                                    }
                                }
                                if (ddlSystemCityName.SelectedIndex == 0)
                                {
                                    IEnumerable<MDMSVC.DC_City_Master_DDL> queryname = res.Where(city => city.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper().Contains(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper()));
                                    if (queryname.Count() > 0)
                                    {
                                        foreach (MDMSVC.DC_City_Master_DDL resc in queryname)
                                        {
                                            ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                                        }
                                    }
                                }
                            }
                            if (ccode != "&nbsp;" && ccode != "")
                            {
                                if (ddlSystemCityName.SelectedIndex == 0)
                                {
                                    IEnumerable<MDMSVC.DC_City_Master_DDL> queryname = res.Where(city => city.Name.ToUpper().Equals(ccode));
                                    if (queryname.Count() > 0)
                                    {
                                        foreach (MDMSVC.DC_City_Master_DDL resc in queryname)
                                        {
                                            ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                                        }
                                    }
                                }
                                if (ddlSystemCityName.SelectedIndex == 0)
                                {
                                    IEnumerable<MDMSVC.DC_City_Master_DDL> queryname = res.Where(city => (city.Code ?? string.Empty).ToUpper().Equals(ccode));
                                    if (queryname.Count() > 0)
                                    {
                                        foreach (MDMSVC.DC_City_Master_DDL resc in queryname)
                                        {
                                            ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                                        }
                                    }
                                }
                            }
                        }

                    }
                    if (ddlSystemCityName.SelectedIndex != 0)
                    {
                        // CityMasterE state = masters.GetCityState(Guid.Parse(ddlSystemCityName.SelectedItem.Value));

                        //if (state != null)
                        //{
                        //    txtSystemStateCode.Text = state.StateCode;
                        //    txtSystemStateName.Text = state.StateName;
                        //}

                        var result1 = masterSVc.GetStateByCity(ddlSystemCityName.SelectedValue);
                        if (result1 != null && result1.Count > 0)
                        {
                            txtSystemStateCode.Text = result1[0].StateCode;
                            txtSystemStateName.Text = result1[0].StateName;
                        }
                        if (ddlSystemCityName.SelectedItem.Value == "0")
                            btnAddCity.Visible = true;
                        else
                            btnAddCity.Visible = false;
                    }
                }
                hdnFlag.Value = "false";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showCityMappingModal();", true);
            }
            catch
            { }
        }

        protected void ddlSystemCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCountryCode = (TextBox)frmEditCityMap.FindControl("txtSystemCountryCode");
            TextBox txtSystemCityCode = (TextBox)frmEditCityMap.FindControl("txtSystemCityCode");
            System.Web.UI.HtmlControls.HtmlGenericControl dvAddCity = (System.Web.UI.HtmlControls.HtmlGenericControl)frmEditCityMap.FindControl("dvAddCity");
            txtSystemCountryCode.Text = masters.GetCodeById("country", new Guid(ddlSystemCountryName.SelectedItem.Value));

            fillcities(ddlSystemCityName, ddlSystemCountryName);
            txtSystemCityCode.Text = "";
            //dvAddCity.Visible = false;
            dvAddCity.Style.Add("display", "none");
        }

        protected void ddlSystemCityName_SelectedIndexChanged()
        {
            DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditCityMap.FindControl("txtSystemCityCode");
            TextBox txtSystemStateCode = (TextBox)frmEditCityMap.FindControl("txtSystemStateCode");
            TextBox txtSystemStateName = (TextBox)frmEditCityMap.FindControl("txtSystemStateName");
            Button btnAddCity = (Button)frmEditCityMap.FindControl("btnAddCity");
            System.Web.UI.HtmlControls.HtmlGenericControl dvAddCity = (System.Web.UI.HtmlControls.HtmlGenericControl)frmEditCityMap.FindControl("dvAddCity");

            dvMsg.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            if (ddlSystemCityName.SelectedItem.Value == "0")
            {
                btnAddCity.Visible = true;
            }
            else
            {
                //dvAddCity.Visible = false;
                dvAddCity.Style.Add("display", "none");
                txtSystemCityCode.Text = masters.GetCodeById("city", new Guid(ddlSystemCityName.SelectedItem.Value));
                //CityMasterE state = masters.GetCityState(Guid.Parse(ddlSystemCityName.SelectedItem.Value));
                //if (state != null)
                //{
                //    txtSystemStateCode.Text = state.StateCode;
                //    txtSystemStateName.Text = state.StateName;
                //}
                var result = masterSVc.GetStateByCity(ddlSystemCityName.SelectedValue);
                if (result != null && result.Count > 0)
                {
                    txtSystemStateCode.Text = result[0].StateCode;
                    txtSystemStateName.Text = result[0].StateName;
                }
                btnAddCity.Visible = false;
            }
        }

        protected void ddlSystemCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSystemCityName_SelectedIndexChanged();
        }

        protected void grdCityMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            frmEditCityMap.ChangeMode(FormViewMode.Edit);
        }

        protected void frmEditCityMap_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Label lblSupplierName = (Label)frmEditCityMap.FindControl("lblSupplierName");
            Label lblSupplierCode = (Label)frmEditCityMap.FindControl("lblSupplierCode");
            Label lblSupCountryName = (Label)frmEditCityMap.FindControl("lblSupCountryName");
            Label lblSupCountryCode = (Label)frmEditCityMap.FindControl("lblSupCountryCode");
            Label lblCityName = (Label)frmEditCityMap.FindControl("lblCityName");
            Label lblCityCode = (Label)frmEditCityMap.FindControl("lblCityCode");
            Label lblStateName = (Label)frmEditCityMap.FindControl("lblStateName");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            TextBox txtSystemCountryCode = (TextBox)frmEditCityMap.FindControl("txtSystemCountryCode");
            DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
            TextBox txtSystemCityCode = (TextBox)frmEditCityMap.FindControl("txtSystemCityCode");
            DropDownList ddlStatus = (DropDownList)frmEditCityMap.FindControl("ddlStatus");
            TextBox txtSystemRemark = (TextBox)frmEditCityMap.FindControl("txtSystemRemark");
            TextBox txtSystemStateCode = (TextBox)frmEditCityMap.FindControl("txtSystemStateCode");
            TextBox txtSystemStateName = (TextBox)frmEditCityMap.FindControl("txtSystemStateName");

            TextBox txtAddCityName = (TextBox)frmEditCityMap.FindControl("txtAddCityName");
            TextBox txtAddCode = (TextBox)frmEditCityMap.FindControl("txtAddCode");
            TextBox txtAddCityCName = (TextBox)frmEditCityMap.FindControl("txtAddCityCName");
            TextBox txtAddCityCCode = (TextBox)frmEditCityMap.FindControl("txtAddCityCCode");
            DropDownList ddlAddCityState = (DropDownList)frmEditCityMap.FindControl("ddlAddCityState");
            TextBox txtAddCitySCode = (TextBox)frmEditCityMap.FindControl("txtAddCitySCode");
            TextBox txtAddCityPlaceId = (TextBox)frmEditCityMap.FindControl("txtAddCityPlaceId");
            System.Web.UI.HtmlControls.HtmlGenericControl dvAddCity = (System.Web.UI.HtmlControls.HtmlGenericControl)frmEditCityMap.FindControl("dvAddCity");
            Button btnAddCity = (Button)frmEditCityMap.FindControl("btnAddCity");
            System.Web.UI.HtmlControls.HtmlGenericControl dvMsg2 = (System.Web.UI.HtmlControls.HtmlGenericControl)frmEditCityMap.FindControl("dvMsg2");

            if (e.CommandName == "Add")
            {
                dvMsg2.Style.Add("display", "none");
                //dvAddCity.Visible = false;
                dvAddCity.Style.Add("display", "none");
                List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();
                Guid myRow_Id = Guid.Parse(grdCityMaps.SelectedDataKey.Value.ToString());

                //SupplierMasters sData = new SupplierMasters();
                //sData = masters.GetSupplierDataByMapping_Id("city", myRow_Id);

                MDMSVC.DC_Supplier_DDL sData = new MDMSVC.DC_Supplier_DDL();
                sData = masterSVc.GetSupplierDataByMapping_Id("CITY", Convert.ToString(myRow_Id));
                MDMSVC.DC_CityMapping newObj = new MDMSVC.DC_CityMapping
                {
                    CityMapping_Id = myRow_Id,
                    Supplier_Id = sData.Supplier_Id,
                    SupplierName = sData.Name,
                    Country_Id = new Guid(ddlSystemCountryName.SelectedItem.Value),
                    City_Id = new Guid(ddlSystemCityName.SelectedItem.Value),
                    CountryCode = masters.GetCodeById("country", new Guid(ddlSystemCountryName.SelectedItem.Value)),
                    CityCode = masters.GetCodeById("city", new Guid(ddlSystemCountryName.SelectedItem.Value)),
                    Status = ddlStatus.SelectedItem.Text,
                    MasterCountryName = ddlSystemCountryName.SelectedItem.Text,
                    Remarks = txtSystemRemark.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    StateCode = txtSystemStateCode.Text,
                    StateName = txtSystemStateName.Text
                };
                RQ.Add(newObj);
                if (mapperSVc.UpdateCityMappingDatat(RQ))
                {
                    MatchedPageIndex = 0;
                    MappedCountry_ID = new Guid(ddlSystemCountryName.SelectedItem.Value);
                    MappedCity_ID = new Guid(ddlSystemCityName.SelectedItem.Value);
                    MatchedCountryName = lblSupCountryName.Text;
                    MatchedCityName = lblCityName.Text;
                    MatchedStatus = ddlStatus.SelectedItem.Text;
                    //frmEditCityMap.Visible = false;
                    fillmatchingdata("");
                    fillmappingdata();
                    dvMatchingRecords.Visible = true;
                    btnMatchedMapSelected.Visible = true;
                    btnMatchedMapAll.Visible = true;
                    dvMsg.Style.Add("display", "block");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been mapped successfully", BootstrapAlertType.Success);
                    hdnFlag.Value = "false";
                }
            }
            else if (e.CommandName == "Cancel")
            {
                dvMsg2.Style.Add("display", "none");
                //dvAddCity.Visible = false;
                dvAddCity.Style.Add("display", "none");
                dvMsg.Style.Add("display", "none");
                frmEditCityMap.ChangeMode(FormViewMode.Edit);
                frmEditCityMap.DataSource = null;
                frmEditCityMap.DataBind();
                frmEditCityMap.Visible = false;
                dvMatchingRecords.Visible = false;
                btnMatchedMapSelected.Visible = false;
                btnMatchedMapAll.Visible = false;
                fillmappingdata();
            }
            else if (e.CommandName == "MapSelected")
            {
                dvMsg2.Style.Add("display", "none");
                //dvAddCity.Visible = false;
                dvAddCity.Style.Add("display", "none");
                dvMsg1.Style.Add("display", "none");
                List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();

                Guid myRow_Id = Guid.Empty;
                Guid mySupplier_Id = Guid.Empty;
                Guid myCountry_Id = Guid.Empty;
                Guid myCity_Id = Guid.Empty;
                //string mystateName = txtSystemStateName.Text;
                //string mystateCode = txtSystemStateCode.Text;
                bool res = false;
                foreach (GridViewRow row in grdMatchingCity.Rows)
                {
                    HtmlInputCheckBox chk = row.Cells[13].Controls[1] as HtmlInputCheckBox;

                   // Htmlc chk = row.Cells[12].Controls[1] as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[1].ToString());
                        myCountry_Id = MappedCountry_ID;
                        myCity_Id = MappedCity_ID;
                    }
                    if (myRow_Id != Guid.Empty)
                    {
                        MDMSVC.DC_CityMapping param = new MDMSVC.DC_CityMapping();
                        param.CityMapping_Id = myRow_Id;
                        if (mySupplier_Id != null)
                            param.Supplier_Id = mySupplier_Id;
                        if (myCountry_Id != null)
                            param.Country_Id = myCountry_Id;
                        if (myCity_Id != null)
                            param.City_Id = myCity_Id;
                        //if (mystateName != null)
                        //    param.StateName = mystateName;
                        //if (mystateCode != null)
                        //    param.StateCode = mystateCode;
                        param.Status = MatchedStatus;
                        param.Edit_Date = DateTime.Now;
                        param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                        RQ.Add(param);
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        myCity_Id = Guid.Empty;
                    }
                }
                if (mapperSVc.UpdateCityMappingDatat(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
                    fillmappingdata();
                    fillmatchingdata("");
                    hdnFlag.Value = "false";
                }
            }
            else if (e.CommandName == "MapAll")
            {
                dvMsg2.Style.Add("display", "none");
                //dvAddCity.Visible = false;
                dvAddCity.Style.Add("display", "none");
                dvMsg1.Style.Add("display", "none");
                List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();

                Guid myRow_Id = Guid.Empty;
                Guid mySupplier_Id = Guid.Empty;
                Guid myCountry_Id = Guid.Empty;
                Guid myCity_Id = Guid.Empty;
                //string mystateName = txtSystemStateName.Text;
                //string mystateCode = txtSystemStateCode.Text;
                bool res = false;
                foreach (GridViewRow row in grdMatchingCity.Rows)
                {
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[0].ToString());
                    mySupplier_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[1].ToString());
                    myCountry_Id = MappedCountry_ID;
                    myCity_Id = MappedCity_ID;

                    if (myRow_Id != Guid.Empty)
                    {
                        MDMSVC.DC_CityMapping param = new MDMSVC.DC_CityMapping();
                        param.CityMapping_Id = myRow_Id;
                        if (mySupplier_Id != null)
                            param.Supplier_Id = mySupplier_Id;
                        if (myCountry_Id != null)
                            param.Country_Id = myCountry_Id;
                        if (myCity_Id != null)
                            param.City_Id = myCity_Id;
                        //if (mystateName != null)
                        //    param.StateName = mystateName;
                        //if (mystateCode != null)
                        //    param.StateCode = mystateCode;
                        param.Status = MatchedStatus;
                        param.Edit_Date = DateTime.Now;
                        param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                        RQ.Add(param);
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        myCity_Id = Guid.Empty;
                    }
                }
                if (mapperSVc.UpdateCityMappingDatat(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
                    fillmappingdata();
                    fillmatchingdata("");
                    hdnFlag.Value = "false";
                }
            }
            else if (e.CommandName == "OpenAddCity")
            {
                dvMsg2.Style.Add("display", "none");
                dvMsg.Style.Add("display", "none");
                dvMsg1.Style.Add("display", "none");
                //dvAddCity.Visible = true;
                dvAddCity.Style.Add("display", "block");
                txtAddCityName.Text = lblCityName.Text;
                //txtAddCode.Text = txtCityCode.Text;
                txtAddCityCName.Text = ddlSystemCountryName.SelectedItem.Text;
                txtAddCityCCode.Text = txtSystemCountryCode.Text;
                SimilarCountryName = ddlSystemCountryName.SelectedItem.Text;
                SimilarCityName = lblCityName.Text;
                SimilarPageIndex = 0;
                fillsimilarcities();
                List<MDMSVC.DC_Master_State> statemaster = fillstatedata();
                if (statemaster != null)
                {
                    ddlAddCityState.Items.Clear();
                    ddlAddCityState.DataSource = statemaster;
                    ddlAddCityState.DataTextField = "State_Name";
                    ddlAddCityState.DataValueField = "State_Id";
                    ddlAddCityState.DataBind();
                    ddlAddCityState.Items.Insert(0, new ListItem("--ALL--", "0"));

                    if (!string.IsNullOrWhiteSpace(lblStateName.Text))
                    {
                        ddlAddCityState.SelectedIndex = ddlAddCityState.Items.IndexOf(ddlAddCityState.Items.FindByText(lblStateName.Text));
                    }
                }
            }
            else if (e.CommandName == "AddCity")
            {
                MDMSVC.DC_Message result = new DC_Message();
                MDMSVC.DC_City newObj = new MDMSVC.DC_City
                {
                    City_Id = Guid.NewGuid(),
                    Name = txtAddCityName.Text,
                    Country_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value),
                    //Code = txtAddCode.Text,
                    CountryName = txtAddCityCName.Text,
                    Status = "ACTIVE",
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name

                };
                if (ddlAddCityState.SelectedItem.Value != "0")
                {
                    newObj.State_Id = Guid.Parse(ddlAddCityState.SelectedItem.Value);
                    newObj.StateName = ddlAddCityState.SelectedItem.Text;
                    newObj.StateCode = txtAddCitySCode.Text;
                }
                if (!string.IsNullOrEmpty(txtAddCityPlaceId.Text))
                {
                    newObj.Google_PlaceId = txtAddCityPlaceId.Text;
                }
                result = masterSVc.AddCityMasterData(newObj);
                if (result.StatusCode == ReadOnlyMessageStatusCode.Duplicate)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg2, result.StatusMessage, BootstrapAlertType.Warning);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "New City Master Data added successfully", BootstrapAlertType.Success);
                    dvMsg1.Style.Add("display", "none");
                    //dvAddCity.Visible = false;
                    dvAddCity.Style.Add("display", "none");
                    btnAddCity.Visible = false;
                    fillcities(ddlSystemCityName, ddlSystemCountryName);
                    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByText(txtAddCityName.Text));
                    ddlSystemCityName_SelectedIndexChanged();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg2, result.StatusMessage, BootstrapAlertType.Success);
                    hdnFlag.Value = "false";

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "javascript:closeCityMappingModal();", true);
        }

        private List<DC_Master_State> fillstatedata()
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            MDMSVC.DC_State_Search_RQ RQ = new MDMSVC.DC_State_Search_RQ();
            RQ.Country_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value);

            var res = masterSVc.GetStatesMaster(RQ);
            return res;
        }

        protected void btnBulkMap_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/staticdata/city/bulkCityMap.aspx");
        }

        protected void btnMapSelected_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid myCountry_Id = Guid.Empty;
            Guid myCity_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdCityMaps.Rows)
            {
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    HtmlInputCheckBox chk = row.Cells[15].Controls[1] as HtmlInputCheckBox;
                    if (chk != null && chk.Checked)
                    {
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[1].ToString());
                        myCountry_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[2].ToString());
                        if (grdCityMaps.DataKeys[index].Values[3] != null)
                            myCity_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[3].ToString());
                    }
                }
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    HtmlInputCheckBox chk = row.Cells[15].Controls[1] as HtmlInputCheckBox;
                    DropDownList ddl = row.Cells[12].Controls[1] as DropDownList;
                    HiddenField hdnCityId = row.Cells[16].Controls[1] as HiddenField; //Set value from ajax changes
                    if (chk != null && chk.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(hdnCityId.Value) && hdnCityId.Value != "0")
                        {
                            if (hdnCityId != null)
                                myCity_Id = Guid.Parse(hdnCityId.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                myCity_Id = Guid.Parse(ddl.SelectedItem.Value);
                        }
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[1].ToString());
                        myCountry_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[2].ToString());
                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_CityMapping param = new MDMSVC.DC_CityMapping();
                    param.CityMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myCountry_Id != null)
                        param.Country_Id = myCountry_Id;
                    if (myCity_Id != null)
                        param.City_Id = myCity_Id;
                    param.Status = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    res = mapperSVc.UpdateCityMappingDatat(RQ);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    myCity_Id = Guid.Empty;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Record has been mapped successfully", BootstrapAlertType.Success);
                }
            }

            fillmappingdata();
        }

        protected void btnMapAll_Click(object sender, EventArgs e)
        {
            List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid myCountry_Id = Guid.Empty;
            Guid myCity_Id = Guid.Empty;
            bool res = false;
            foreach (GridViewRow row in grdCityMaps.Rows)
            {
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
                {
                    //CheckBox chk = row.Cells[14].Controls[1] as CheckBox;
                    //if (chk != null && chk.Checked)
                    //{
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[0].ToString());
                    mySupplier_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[1].ToString());
                    myCountry_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[2].ToString());
                    if (grdCityMaps.DataKeys[index].Values[3] != null)
                        myCity_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[3].ToString());
                    //}
                }
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    DropDownList ddl = row.Cells[11].Controls[1] as DropDownList;
                    HiddenField hdnCityId = row.Cells[15].Controls[1] as HiddenField; //Set value from ajax changes
                    if (ddl.SelectedItem.Value != "0" || !string.IsNullOrWhiteSpace(hdnCityId.Value))
                    {
                        if (!string.IsNullOrWhiteSpace(hdnCityId.Value))
                        {
                            if (hdnCityId.Value == "0")
                                continue;
                            if (hdnCityId != null)
                                myCity_Id = Guid.Parse(hdnCityId.Value);
                        }
                        else if (ddl.SelectedItem.Value != "0")
                        {
                            if (ddl != null)
                                myCity_Id = Guid.Parse(ddl.SelectedItem.Value);
                        }
                        myRow_Id = Guid.Empty;
                        mySupplier_Id = Guid.Empty;
                        myCountry_Id = Guid.Empty;
                        int index = row.RowIndex;
                        myRow_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[0].ToString());
                        mySupplier_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[1].ToString());
                        myCountry_Id = Guid.Parse(grdCityMaps.DataKeys[index].Values[2].ToString());
                    }
                }
                if (myRow_Id != Guid.Empty)
                {
                    MDMSVC.DC_CityMapping param = new MDMSVC.DC_CityMapping();
                    param.CityMapping_Id = myRow_Id;
                    if (mySupplier_Id != null)
                        param.Supplier_Id = mySupplier_Id;
                    if (myCountry_Id != null)
                        param.Country_Id = myCountry_Id;
                    if (myCity_Id != null)
                        param.City_Id = myCity_Id;
                    param.Status = "MAPPED";
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    res = mapperSVc.UpdateCityMappingDatat(RQ);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    myCity_Id = Guid.Empty;
                    BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Records has been mapped successfully", BootstrapAlertType.Success);
                }
            }
            fillmappingdata();
        }

        protected void grdCityMaps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var myGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                Guid myCountryId = Guid.Empty;
                if (myGridView.DataKeys[index].Values[2] != null)
                    myCountryId = Guid.Parse(myGridView.DataKeys[index].Values[2].ToString());
                DropDownList ddl = e.Row.Cells[12].Controls[1] as DropDownList;
                if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                {
                    if (ddl != null)
                    {
                        var myCityId = Convert.ToString(myGridView.DataKeys[index].Values[3]);
                        var myCityName = Convert.ToString(myGridView.DataKeys[index].Values[4]);
                        if (!string.IsNullOrWhiteSpace(myCityId) && !string.IsNullOrWhiteSpace(myCityName))
                        {
                            ddl.Items.Add(new ListItem(myCityName, myCityId));
                            if (ddl.Items.FindByValue(myCityId) != null)
                                ddl.Items.FindByValue(myCityId).Selected = true;
                        }
                    }
                }


                //if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
                //{
                //    if (myCountryId != Guid.Empty)
                //    {
                //        var res = masterSVc.GetMasterCityData(Convert.ToString(myCountryId));
                //        var result = (from x in res
                //                      select new
                //                      {
                //                          City_ID = x.City_Id,
                //                          NameWithCode = x.Name + " (" + x.Code + ")"
                //                      });
                //        ddl.DataSource = result;
                //        ddl.DataValueField = "City_ID";
                //        ddl.DataTextField = "NameWithCode";
                //        ddl.DataBind();
                //        string ccode = e.Row.Cells[4].Text.ToUpper();
                //        string cname = e.Row.Cells[5].Text.ToUpper();

                //        if (cname != null)
                //        {
                //            if (cname != "&nbsp;" && cname != "")
                //            {
                //                var CityIdToSelect = res.Where(city => city.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper().Equals(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper())).Select(x => x.City_Id).FirstOrDefault();

                //                if (CityIdToSelect == null)
                //                {
                //                    CityIdToSelect = res.Where(city => city.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper().Contains(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper())).Select(x => x.City_Id).FirstOrDefault();

                //                    if (CityIdToSelect == null)
                //                    {
                //                        if (ccode != "&nbsp;" && ccode != "")
                //                        {
                //                            CityIdToSelect = res.Where(city => (city.Code ?? string.Empty).ToUpper().Equals(ccode.ToUpper())).Select(x => x.City_Id).FirstOrDefault();

                //                            if (CityIdToSelect == null)
                //                            {
                //                                CityIdToSelect = res.Where(city => city.Name.ToUpper().Equals(ccode.ToUpper())).Select(x => x.City_Id).FirstOrDefault();
                //                            }
                //                        }
                //                    }

                //                }

                //                if (CityIdToSelect != null && ddl.Items.FindByValue(CityIdToSelect.ToString()) != null)
                //                {
                //                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(CityIdToSelect.ToString()));
                //                }
                //            }
                //        }
                //    }
                //}
            }

        }

        protected void grdCityMaps_DataBound1(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            //foreach (GridViewRow row in myGridView.Rows)
            //{

            if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                myGridView.Columns[11].Visible = true;
                myGridView.Columns[12].Visible = false;
                myGridView.Columns[15].Visible = true;
            }
            else if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                myGridView.Columns[11].Visible = false;
                myGridView.Columns[12].Visible = true;
                myGridView.Columns[15].Visible = true;
            }
            else
            {
                myGridView.Columns[11].Visible = true;
                myGridView.Columns[12].Visible = false;
                myGridView.Columns[15].Visible = false;
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
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
                        CheckBox chk = nextRow.Cells[14].Controls[1] as CheckBox;
                        if (chk != null)
                            chk.Focus();
                    }
                    else if (ddlStatus.SelectedItem.Text == "UNMAPPED")
                    {
                        DropDownList ddl = nextRow.Cells[11].Controls[1] as DropDownList;
                        if (ddl != null)
                            ddl.Focus();
                    }
                }
            }
            else
                btnMapSelected.Focus();
        }

        protected void grdMatchingCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdMatchingCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg.Style.Add(HtmlTextWriterStyle.Display, "none");
            MatchedPageIndex = e.NewPageIndex;
            fillmatchingdata("");
        }

        protected void grdMatchingCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdMatchingCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdMatchingCity_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlMatchingPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("");
        }

        protected void ddlMatchingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("status");
        }

        protected void ddlAddCityState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAddCityState = (DropDownList)frmEditCityMap.FindControl("ddlAddCityState");
            TextBox txtAddCitySCode = (TextBox)frmEditCityMap.FindControl("txtAddCitySCode");
            if (ddlAddCityState.SelectedItem.Value != "0")
                txtAddCitySCode.Text = masters.GetCodeById("state", Guid.Parse(ddlAddCityState.SelectedItem.Value));
            else
                txtAddCitySCode.Text = "";
        }

        protected void ddlSimilarProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillsimilarcities();
        }

        protected void grdSimilarProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SimilarPageIndex = e.NewPageIndex;
            fillsimilarcities();
        }

        protected void ckboxIsExactMatch_CheckedChanged(object sender, EventArgs e)
        {
            fillmatchingdata("");
            dvMsg.Visible = false;
        }
    }
}