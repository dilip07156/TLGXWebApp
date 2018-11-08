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
using System.Diagnostics;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class CityMap : System.Web.UI.UserControl
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();

        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        MasterDataDAL masters = new MasterDataDAL();
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
            ddl.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("ProductSupplierMapping", Attribute_Name).MasterAttributeValues;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //SortBy = "CountryName";
            //SortEx = "";
            //PageIndex = 0;
            fillmappingdata(0);

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

        private void fillsimilarcities(int pPageIndex, Guid pCountry_Id, string pSimilarCityName)
        {
            DropDownList ddlSimilarProducts = (DropDownList)frmEditCityMap.FindControl("ddlSimilarProducts");
            GridView grdSimilarProducts = (GridView)frmEditCityMap.FindControl("grdSimilarProducts");
            dvMsg.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            MDMSVC.DC_CityMapping_RQ RQParam = new MDMSVC.DC_CityMapping_RQ();
            if (ddlMatchingStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlMatchingStatus.SelectedItem.Text;
            //RQParam.SupplierCountryName = pSimilarCountryName;
            RQParam.Country_Id = pCountry_Id;
            RQParam.SupplierCityName = pSimilarCityName;
            RQParam.PageNo = pPageIndex;
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
            grdSimilarProducts.PageIndex = pPageIndex;
            grdSimilarProducts.PageSize = Convert.ToInt32(ddlSimilarProducts.SelectedItem.Text);
            grdSimilarProducts.DataBind();
        }

        private void fillmatchingdata(string from, int pPageIndex)
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
            DropDownList ddlStatus = (DropDownList)frmEditCityMap.FindControl("ddlStatus");
            Label lblCityName = (Label)frmEditCityMap.FindControl("lblCityName");

            //dvMsg.Style.Add("display", "none");
            //dvMsg1.Style.Add("display", "none");
            MDMSVC.DC_CityMapping_RQ RQParam = new MDMSVC.DC_CityMapping_RQ();
            if (ddlMatchingStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlMatchingStatus.SelectedItem.Text;
            RQParam.StatusExcept = ddlStatus.SelectedItem.Text;
            //RQParam.SupplierCountryName = MatchedCountryName;
            RQParam.Country_Id = Guid.Parse(ddlSystemCountryName.SelectedValue);
            RQParam.SupplierCityName = lblCityName.Text;
            RQParam.PageNo = pPageIndex;
            RQParam.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            RQParam.SortBy = ("CityName").Trim();
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
            grdMatchingCity.PageIndex = pPageIndex;
            grdMatchingCity.PageSize = Convert.ToInt32(ddlMatchingPageSize.SelectedItem.Text);
            grdMatchingCity.DataBind();
        }

        private void fillmappingdata(int pPageIndex)
        {
            MDMSVC.DC_CityMapping_RQ RQ = new MDMSVC.DC_CityMapping_RQ();
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            Guid selCity_ID = Guid.Empty;
            if (ddlSupplierName.SelectedItem.Value != "0")
                RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
            if (ddlMasterCountry.SelectedItem.Value != "0")
                RQ.Country_Id = new Guid(ddlMasterCountry.SelectedItem.Value);
            if (ddlCity.SelectedItem.Value != "0")
            {
                RQ.City_Id = new Guid(ddlCity.SelectedItem.Value);
                RQ.CityName = ddlCity.SelectedItem.Text;
            }
            if (ddlStatus.SelectedItem.Value != "0")
                RQ.Status = ddlStatus.SelectedItem.Text;
            if (chkListEntityForSearch.Items.Cast<ListItem>().Where(x => x.Selected).Count() > 0)
            {
                string EntityTypes = string.Join(",", chkListEntityForSearch.Items.Cast<ListItem>().Where(x => x.Selected).Select(s => s.Value).ToArray());
                RQ.EntityType = EntityTypes;
            }
            RQ.SupplierCountryName = txtSuppCountry.Text;
            RQ.SupplierCityName = txtSuppCity.Text;
            //EntitySearch
            //RQ.isHotel = chkIsHotel.Checked;
            //RQ.isActivity = chkIsActivity.Checked;
            // RQ.IsPackage = chkIsPackages.Checked;
            // RQ.IsCruise = chkIsCruises.Checked;


            RQ.PageNo = pPageIndex;
            RQ.SortBy = ("CountryName").Trim();
            RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

            var res = mapperSVc.GetCityMappingData(RQ);
            grdCityMaps.DataSource = res;
            Stopwatch timer = new Stopwatch();
            timer.Start();
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
            grdCityMaps.PageIndex = pPageIndex;
            grdCityMaps.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text); ;
            //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
            grdCityMaps.DataBind();
            timer.Stop();
            var ElapsedTicks = timer.ElapsedMilliseconds;
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
            txtSuppCountry.Text = "";
            txtSuppCity.Text = "";
            chkListEntityForSearch.ClearSelection();
            // chkIsHotel.Checked = false;
            //chkIsActivity.Checked = false;

        }

        protected void ddlMasterCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            fillcities(ddlCity, ddlMasterCountry);
        }

        protected void grdCityMaps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            //PageIndex = e.NewPageIndex;
            fillmappingdata(e.NewPageIndex);
        }

        protected void grdCityMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                string strCityCode = string.Empty;
                string strSupplierCode = string.Empty;
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
                        CountryCode = grdCityMaps.Rows[index].Cells[3].Text,
                        CountryName = grdCityMaps.Rows[index].Cells[4].Text,
                        StateName = ((System.Web.UI.DataBoundLiteralControl)(grdCityMaps.Rows[index].Cells[5].Controls[0])).Text.ToString().Trim(), //grdCityMaps.Rows[index].Cells[4].Text,
                        CityCode = ((System.Web.UI.WebControls.LinkButton)(grdCityMaps.Rows[index].Cells[6].Controls[1])).Text,
                        CityName = ((System.Web.UI.WebControls.LinkButton)(grdCityMaps.Rows[index].Cells[7].Controls[1])).Text,
                        MasterCountryCode = grdCityMaps.Rows[index].Cells[8].Text,
                        MasterCountryName = grdCityMaps.Rows[index].Cells[9].Text,
                        MasterCityCode = grdCityMaps.Rows[index].Cells[10].Text,
                        Master_CityName = grdCityMaps.Rows[index].Cells[12].Text ?? grdCityMaps.Rows[index].Cells[12].Text,
                        Status = grdCityMaps.Rows[index].Cells[14].Text
                    });

                    //SupplierMasters sData = new SupplierMasters();
                    //sData = masters.GetSupplierDataByMapping_Id("city", myRow_Id);
                    //string supCode = sData.Code;

                    var result = masterSVc.GetSupplierDataByMapping_Id("CITY", Convert.ToString(myRow_Id));

                    DC_CityMapping_RQ mappingrq = new DC_CityMapping_RQ();
                    List<DC_CityMapping> mappingrow = new List<DC_CityMapping>();

                    mappingrq.CityMapping_Id = myRow_Id;
                    mappingrq.PageSize = 1; //To get selected row details
                    mappingrq.PageNo = 0;
                    mappingrow = mapperSVc.GetCityMappingData(mappingrq);

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
                    TextBox txtSearchCity = (TextBox)frmEditCityMap.FindControl("txtSearchCity");
                    TextBox txtSystemCityCode = (TextBox)frmEditCityMap.FindControl("txtSystemCityCode");
                    DropDownList ddlStatus = (DropDownList)frmEditCityMap.FindControl("ddlStatus");
                    TextBox txtSystemRemark = (TextBox)frmEditCityMap.FindControl("txtSystemRemark");
                    TextBox txtSystemStateCode = (TextBox)frmEditCityMap.FindControl("txtSystemStateCode");
                    TextBox txtSystemStateName = (TextBox)frmEditCityMap.FindControl("txtSystemStateName");
                    HiddenField hdnSelSystemCity_Id = (HiddenField)frmEditCityMap.FindControl("hdnSelSystemCity_Id");



                    Button btnAddCity = (Button)frmEditCityMap.FindControl("btnAddCity");
                    GridView grdvListOfHotelÖnSelection = (GridView)frmEditCityMap.FindControl("grdvListOfHotelOnSelection");

                    fillcountries(ddlSystemCountryName);
                    fillmappingstatus(ddlStatus);
                    lblSupplierName.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[1].Text);
                    lblSupplierCode.Text = "(" + supCode + ")"; ;
                    lblSupCountryCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[3].Text);
                    lblSupCountryName.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[4].Text);
                    lblStateName.Text = System.Web.HttpUtility.HtmlDecode(((System.Web.UI.DataBoundLiteralControl)(grdCityMaps.Rows[index].Cells[5].Controls[0])).Text.ToString().Trim());
                    lblCityCode.Text = System.Web.HttpUtility.HtmlDecode(((LinkButton)(grdCityMaps.Rows[index].Cells[6].Controls[1])).Text);
                    lblCityName.Text = System.Web.HttpUtility.HtmlDecode(((LinkButton)(grdCityMaps.Rows[index].Cells[7].Controls[1])).Text);

                    txtSystemCountryCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[8].Text);
                    txtSystemCityCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[11].Text);
                    txtSystemRemark.Text = masters.GetRemarksForMapping("city", myRow_Id);

                    //City Mapping Data available then fill system data
                    if (mappingrow != null && mappingrow.Count > 0)
                    {
                        txtSystemStateCode.Text = Convert.ToString(mappingrow[0].MasterStateCode);
                        txtSystemStateName.Text = Convert.ToString(mappingrow[0].MasterStateName);
                        string syscityname = Convert.ToString(mappingrow[0].Master_CityName);
                        string stsCity_Id = Convert.ToString(mappingrow[0].Master_City_id);
                        txtSearchCity.Text = syscityname;
                        if (!string.IsNullOrWhiteSpace(syscityname))
                        {
                            ddlSystemCityName.Items.Insert(0, new ListItem("---ALL---", "0"));
                            ddlSystemCityName.Items.Insert(1, new ListItem(syscityname, stsCity_Id));
                            ddlSystemCityName.SelectedIndex = 1;
                            hdnSelSystemCity_Id.Value = stsCity_Id;
                        }

                    }

                    //if (grdCityMaps.Rows[index].Cells[13].Text.ToString() == "REVIEW" || grdCityMaps.Rows[index].Cells[13].Text.ToString() == "MAPPED")
                    //{


                    ddlSystemCountryName.SelectedIndex = ddlSystemCountryName.Items.IndexOf(ddlSystemCountryName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[9].Text)));

                    #region Fill City As per Selected Country
                    //List<MDMSVC.DC_City_Master_DDL> res = new List<DC_City_Master_DDL>();
                    //if (ddlSystemCountryName.SelectedItem.Value != "0")
                    //{
                    //    res = masterSVc.GetMasterCityData(ddlSystemCountryName.SelectedItem.Value);
                    //    ddlSystemCityName.Items.Clear();
                    //    ddlSystemCityName.DataSource = res;
                    //    ddlSystemCityName.DataValueField = "City_ID";
                    //    ddlSystemCityName.DataTextField = "Name";
                    //    ddlSystemCityName.DataBind();
                    //}
                    //ddlSystemCityName.Items.Insert(0, new ListItem("---ALL---", "0"));
                    //fillcities(ddlSystemCityName, ddlSystemCountryName);
                    #endregion

                    ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[14].Text)));
                    //ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[12].Text)));
                    //txtSystemCityCode.Text = System.Web.HttpUtility.HtmlDecode(grdCityMaps.Rows[index].Cells[11].Text);
                    //}

                    //if (ddlSystemCityName.SelectedIndex == 0)
                    //{
                    //    //var res = objMasterDataDAL.GetMasterCityDatawithCode(new Guid(ddlSystemCountryName.SelectedItem.Value));
                    //    //var res = masterSVc.GetMasterCityData(ddlSystemCountryName.SelectedItem.Value);

                    //    string ccode = System.Web.HttpUtility.HtmlDecode(((LinkButton)(grdCityMaps.Rows[index].Cells[6].Controls[1])).Text);
                    //    string cname = System.Web.HttpUtility.HtmlDecode(((LinkButton)(grdCityMaps.Rows[index].Cells[7].Controls[1])).Text);

                    //    if (cname != null)
                    //    {
                    //        if (cname != "&nbsp;" && cname != "")
                    //        {
                    //            IEnumerable<MDMSVC.DC_City_Master_DDL> querynamed = res.Where(city => city.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper().Equals(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper()));

                    //            if (querynamed.Count() > 0)
                    //            {
                    //                foreach (MDMSVC.DC_City_Master_DDL resc in querynamed)
                    //                {
                    //                    ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                    //                    if (ddlSystemCityName.SelectedIndex > 0)
                    //                    {
                    //                        txtSystemCityCode.Text = resc.Code;
                    //                        break;
                    //                    }

                    //                }
                    //            }

                    //            if (ddlSystemCityName.SelectedIndex == 0)
                    //            {
                    //                IEnumerable<MDMSVC.DC_City_Master_DDL> queryname = res.Where(city => city.Name.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper().Contains(cname.Replace("*", "").Replace("-", "").Replace(" ", "").Replace("#", "").Replace("@", "").Replace("(", "").Replace(")", "").Replace("city", "").ToUpper()));
                    //                if (queryname.Count() > 0)
                    //                {
                    //                    foreach (MDMSVC.DC_City_Master_DDL resc in queryname)
                    //                    {
                    //                        ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                    //                        if (ddlSystemCityName.SelectedIndex > 0)
                    //                        {
                    //                            txtSystemCityCode.Text = resc.Code;
                    //                            break;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        if (ccode != "&nbsp;" && ccode != "")
                    //        {
                    //            if (ddlSystemCityName.SelectedIndex == 0)
                    //            {
                    //                IEnumerable<MDMSVC.DC_City_Master_DDL> queryname = res.Where(city => city.Name.ToUpper().Equals(ccode));
                    //                if (queryname.Count() > 0)
                    //                {
                    //                    foreach (MDMSVC.DC_City_Master_DDL resc in queryname)
                    //                    {
                    //                        ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                    //                        if (ddlSystemCityName.SelectedIndex > 0)
                    //                        {
                    //                            txtSystemCityCode.Text = resc.Code;
                    //                            break;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            if (ddlSystemCityName.SelectedIndex == 0)
                    //            {
                    //                IEnumerable<MDMSVC.DC_City_Master_DDL> queryname = res.Where(city => (city.Code ?? string.Empty).ToUpper().Equals(ccode));
                    //                if (queryname.Count() > 0)
                    //                {
                    //                    foreach (MDMSVC.DC_City_Master_DDL resc in queryname)
                    //                    {
                    //                        ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByValue(resc.City_Id.ToString()));
                    //                        if (ddlSystemCityName.SelectedIndex > 0)
                    //                        {
                    //                            txtSystemCityCode.Text = resc.Code;
                    //                            break;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }

                    //}
                    if (ddlSystemCityName.SelectedIndex > 0)
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
                            btnAddCity.Attributes.Add("style", "display:block");
                        else
                            btnAddCity.Attributes.Add("style", "display:none");
                    }


                    BindHotelList(myRow_Id, 0, 5, grdvListOfHotelÖnSelection, string.Empty);

                    hdnFlag.Value = "false";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "javascript:showCityMappingModal();", true);

                }
                if (e.CommandName == "SelectCityCode")
                {
                    BindHotelList(myRow_Id, 0, 5, grdvListOfHotel, "CITYCODE");
                }
                if (e.CommandName == "SelectCityName")
                {
                    BindHotelList(myRow_Id, 0, 5, grdvListOfHotel, "CITYNAME");
                }

            }
            catch
            { }
        }

        private void BindHotelList(Guid _cityMappingid, int hotelListPageIndex, int hotelListPageSize, GridView grdv, string StrDataFor)
        {
            try
            {
                var result = mapperSVc.GetHotelListByCityCode(new DC_HotelListByCityCode_RQ()
                {
                    CityMapping_Id = Convert.ToString(_cityMappingid),
                    GoFor = StrDataFor,
                    PageNo = hotelListPageIndex,
                    PageSize = hotelListPageSize
                });
                grdv.DataSource = result;
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        grdv.VirtualItemCount = result[0].TotalRecords;
                    }
                }
                grdv.PageIndex = hotelListPageIndex;
                grdv.PageSize = hotelListPageSize;
                grdv.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void grdvListOfHotel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Guid gcitymapid = Guid.Parse(Convert.ToString(grdvListOfHotel.DataKeys[0].Values[0]));
            string GoFor = Convert.ToString(grdvListOfHotel.DataKeys[0].Values[1]);
            BindHotelList(gcitymapid, e.NewPageIndex, 5, grdvListOfHotel, GoFor);
        }
        protected void grdvListOfHotelOnSelection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grdvListOfHotelOnSelection = (GridView)frmEditCityMap.FindControl("grdvListOfHotelOnSelection");
            Guid gcitymapid = Guid.Parse(Convert.ToString(grdvListOfHotelOnSelection.DataKeys[0].Values[0]));
            BindHotelList(gcitymapid, e.NewPageIndex, 5, grdvListOfHotelOnSelection, string.Empty);
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
            if (!(ddlSystemCountryName.SelectedIndex == 0))
            {
                txtSystemCountryCode.Text = masters.GetCodeById("country", new Guid(ddlSystemCountryName.SelectedItem.Value));
                fillcities(ddlSystemCityName, ddlSystemCountryName);
            }
            else
            {
                ddlSystemCityName.DataSource = null;
                ddlSystemCityName.DataBind();
            }
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
            TextBox txtSearchCity = (TextBox)frmEditCityMap.FindControl("txtSearchCity");
            TextBox txtAddCitySCode = (TextBox)frmEditCityMap.FindControl("txtAddCitySCode");
            TextBox txtAddCityPlaceId = (TextBox)frmEditCityMap.FindControl("txtAddCityPlaceId");
            System.Web.UI.HtmlControls.HtmlGenericControl dvAddCity = (System.Web.UI.HtmlControls.HtmlGenericControl)frmEditCityMap.FindControl("dvAddCity");
            Button btnAddCity = (Button)frmEditCityMap.FindControl("btnAddCity");
            System.Web.UI.HtmlControls.HtmlGenericControl dvMsg2 = (System.Web.UI.HtmlControls.HtmlGenericControl)frmEditCityMap.FindControl("dvMsg2");


            HiddenField hdnSelSystemCity_Id = (HiddenField)frmEditCityMap.FindControl("hdnSelSystemCity_Id");

            if (e.CommandName == "Add")
            {
                dvMsg2.Style.Add("display", "none");
                dvAddCity.Style.Add("display", "none");
                List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();
                Guid myRow_Id = Guid.Parse(grdCityMaps.SelectedDataKey.Value.ToString());

                Guid? countryId = null;
                Guid? cityId = null;
                string countryCode = string.Empty;
                string cityCode = string.Empty;
                string masterCountryName = string.Empty;
                if(txtSearchCity.Text != string.Empty && ddlSystemCountryName.SelectedIndex != 0 && ((ddlSystemCityName.Items.Count > 0 && ddlSystemCityName.SelectedItem.Value != "0") || !string.IsNullOrWhiteSpace(hdnSelSystemCity_Id.Value)))
                {
                    if (!(ddlSystemCountryName.SelectedIndex == 0))
                    {
                        countryId = new Guid(ddlSystemCountryName.SelectedItem.Value);
                        if (ddlSystemCityName.Items.Count > 0 && ddlSystemCityName.SelectedItem.Value != "0")
                        {
                            cityId = new Guid(ddlSystemCityName.SelectedItem.Value);
                            // cityCode = masters.GetCodeById("city", new Guid(ddlSystemCityName.SelectedItem.Value));
                        }
                        else if (!string.IsNullOrWhiteSpace(hdnSelSystemCity_Id.Value))
                        {
                            cityId = new Guid(hdnSelSystemCity_Id.Value);
                        }
                        // cityId = new Guid(ddlSystemCityName.SelectedItem.Value);
                        cityCode = masters.GetCodeById("city", new Guid(Convert.ToString(cityId)));


                        countryCode = masters.GetCodeById("country", new Guid(ddlSystemCountryName.SelectedItem.Value));
                        masterCountryName = ddlSystemCountryName.SelectedItem.Text;
                    }

                    MDMSVC.DC_Supplier_DDL sData = new MDMSVC.DC_Supplier_DDL();
                    sData = masterSVc.GetSupplierDataByMapping_Id("CITY", Convert.ToString(myRow_Id));
                    MDMSVC.DC_CityMapping newObj = new MDMSVC.DC_CityMapping
                    {
                        CityMapping_Id = myRow_Id,
                        Supplier_Id = sData.Supplier_Id,
                        SupplierName = sData.Name,
                        Country_Id = countryId,
                        City_Id = cityId,
                        CountryCode = countryCode,
                        CityCode = cityCode,
                        Status = ddlStatus.SelectedItem.Text,
                        MasterCountryName = masterCountryName,
                        Remarks = txtSystemRemark.Text,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    };
                    RQ.Add(newObj);
                    if (mapperSVc.UpdateCityMappingDatat(RQ))
                    {
                        //MatchedPageIndex = 0;
                        //MappedCountry_ID = new Guid(ddlSystemCountryName.SelectedItem.Value);
                        //MappedCity_ID = new Guid(ddlSystemCityName.SelectedItem.Value);
                        //MappedCountry_ID = countryId;
                        //MappedCity_ID = cityId;
                        //MatchedCountryName = lblSupCountryName.Text;
                        //MatchedCityName = lblCityName.Text;
                        //MatchedStatus = ddlStatus.SelectedItem.Text;
                        //frmEditCityMap.Visible = false;
                        if (!(ddlSystemCountryName.SelectedIndex == 0))
                        {
                            fillmatchingdata("", 0);
                            fillmappingdata(grdCityMaps.PageIndex);
                            dvMatchingRecords.Visible = true;
                            btnMatchedMapSelected.Visible = true;
                            btnMatchedMapAll.Visible = true;
                        }
                        dvMsg.Style.Add("display", "block");
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Record has been updated successfully", BootstrapAlertType.Success);
                        hdnFlag.Value = "false";
                    }
                }
                else
                {
                    dvMsg.Style.Add("display", "block");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please select valid city to Map", BootstrapAlertType.Warning);
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
                fillmappingdata(grdCityMaps.PageIndex);
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
                Guid? myCountry_Id = Guid.Empty;
                Guid? myCity_Id = Guid.Empty;

                //string mystateName = txtSystemStateName.Text;
                //string mystateCode = txtSystemStateCode.Text;

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
                        myCountry_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value);
                        myCity_Id = Guid.Parse(ddlSystemCityName.SelectedItem.Value);
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
                        param.Status = ddlStatus.SelectedItem.Text;
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
                    fillmappingdata(grdCityMaps.PageIndex);
                    fillmatchingdata("", grdMatchingCity.PageIndex);
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
                Guid? myCountry_Id = Guid.Empty;
                Guid? myCity_Id = Guid.Empty;

                foreach (GridViewRow row in grdMatchingCity.Rows)
                {
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    int index = row.RowIndex;
                    myRow_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[0].ToString());
                    mySupplier_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[1].ToString());
                    myCountry_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value); ;
                    myCity_Id = Guid.Parse(ddlSystemCityName.SelectedItem.Value); ;

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
                        param.Status = ddlStatus.SelectedItem.Text;
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
                    fillmappingdata(grdCityMaps.PageIndex);
                    fillmatchingdata("", grdMatchingCity.PageIndex);
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
                //SimilarCountryName = ddlSystemCountryName.SelectedItem.Text;
                //SimilarCityName = lblCityName.Text;
                //SimilarPageIndex = 0;
                fillsimilarcities(0, Guid.Parse(ddlSystemCountryName.SelectedValue), lblCityName.Text);
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

                    //Getting city data,just added
                    var resultCityMaster = masterSVc.GetCityMasterData(new DC_City_Search_RQ
                    {
                        City_Name = txtAddCityName.Text,
                        Country_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value),
                        PageNo = 0,
                        PageSize = 1
                    });
                    if (resultCityMaster != null && resultCityMaster.Count > 0)
                    {
                        ddlSystemCityName.Items.Insert(0, new ListItem("---ALL---", "0"));
                        string city_id = Convert.ToString(resultCityMaster[0].City_Id);
                        string cityname = resultCityMaster[0].Name;
                        ddlSystemCityName.Items.Insert(1, new ListItem(cityname, city_id));
                        hdnSelSystemCity_Id.Value = city_id;
                        txtSearchCity.Text = cityname;
                    }


                    //fillcities(ddlSystemCityName, ddlSystemCountryName);
                    ddlSystemCityName.SelectedIndex = 1;
                    // ddlSystemCityName.SelectedIndex = ddlSystemCityName.Items.IndexOf(ddlSystemCityName.Items.FindByText(txtAddCityName.Text));
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
                    HtmlInputCheckBox chk = row.Cells[16].Controls[1] as HtmlInputCheckBox;
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
                    //15->16,12->13,16->17
                    HtmlInputCheckBox chk = row.Cells[16].Controls[1] as HtmlInputCheckBox;
                    DropDownList ddl = row.Cells[13].Controls[1] as DropDownList;
                    HiddenField hdnCityId = row.Cells[17].Controls[1] as HiddenField; //Set value from ajax changes
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
                    //res = mapperSVc.UpdateCityMappingDatat(RQ);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    myCity_Id = Guid.Empty;
                    //BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Record has been updated successfully", BootstrapAlertType.Success);
                }
            }
            if (RQ.Count > 0)
            {
                res = mapperSVc.UpdateCityMappingDatat(RQ);
                BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Record has been updated successfully", BootstrapAlertType.Success);
            }
            fillmappingdata(grdCityMaps.PageIndex);
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
                    //DropDownList ddl = row.Cells[11].Controls[1] as DropDownList;
                    DropDownList ddl = row.Cells[13].Controls[1] as DropDownList;
                    //HiddenField hdnCityId = row.Cells[15].Controls[1] as HiddenField; //Set value from ajax changes
                    HiddenField hdnCityId = row.Cells[17].Controls[1] as HiddenField; //Set value from ajax changes
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
                    //res = mapperSVc.UpdateCityMappingDatat(RQ);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    myCity_Id = Guid.Empty;
                    //BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Records has been mapped successfully", BootstrapAlertType.Success);
                }
            }
            if (RQ.Count > 0)
            {
                res = mapperSVc.UpdateCityMappingDatat(RQ);
                BootstrapAlert.BootstrapAlertMessage(dvMsg1, "Records has been mapped successfully", BootstrapAlertType.Success);
            }
            fillmappingdata(grdCityMaps.PageIndex);
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
                DropDownList ddl = e.Row.Cells[13].Controls[1] as DropDownList;
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

        protected void grdCityMaps_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            //foreach (GridViewRow row in myGridView.Rows)
            //{

            if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "REVIEW")
            {
                myGridView.Columns[12].Visible = true;
                myGridView.Columns[13].Visible = false;
                myGridView.Columns[16].Visible = true;
            }
            else if (ddlStatus.SelectedItem.Text.Trim().ToUpper() == "UNMAPPED")
            {
                myGridView.Columns[12].Visible = false;
                myGridView.Columns[13].Visible = true;
                myGridView.Columns[16].Visible = true;
            }
            else
            {
                myGridView.Columns[12].Visible = true;
                myGridView.Columns[13].Visible = false;
                myGridView.Columns[16].Visible = false;
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
            //MatchedPageIndex = e.NewPageIndex;
            fillmatchingdata("", e.NewPageIndex);
        }

        protected void grdMatchingCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dvMsg.Style.Add(HtmlTextWriterStyle.Display, "none");
            if (e.CommandName == "SelectCityCode")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                BindHotelList(myRow_Id, 0, 5, grdvListOfHotel, "CITYCODE");
            }
            else if (e.CommandName == "SelectCityName")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                BindHotelList(myRow_Id, 0, 5, grdvListOfHotel, "CITYNAME");
            }
        }

        protected void grdMatchingCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdMatchingCity_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlMatchingPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("", grdMatchingCity.PageIndex);
        }

        protected void ddlMatchingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata("status", grdMatchingCity.PageIndex);
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
            GridView grdSimilarProducts = (GridView)frmEditCityMap.FindControl("grdSimilarProducts");
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            Label lblCityName = (Label)frmEditCityMap.FindControl("lblCityName");
            fillsimilarcities(grdSimilarProducts.PageIndex, Guid.Parse(ddlSystemCountryName.SelectedValue), lblCityName.Text);
        }

        protected void grdSimilarProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            Label lblCityName = (Label)frmEditCityMap.FindControl("lblCityName");
            //SimilarPageIndex = e.NewPageIndex;
            fillsimilarcities(e.NewPageIndex, Guid.Parse(ddlSystemCountryName.SelectedValue), lblCityName.Text);
        }

        protected void ckboxIsExactMatch_CheckedChanged(object sender, EventArgs e)
        {
            fillmatchingdata("", grdMatchingCity.PageIndex);
            dvMsg.Visible = false;
        }

        protected void btnMatchedMapSelected_Click(object sender, EventArgs e)
        {
            //dvMsg2.Style.Add("display", "none");
            ////dvAddCity.Visible = false;
            //dvAddCity.Style.Add("display", "none");
            dvMsg1.Style.Add("display", "none");
            List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid? myCountry_Id = Guid.Empty;
            Guid? myCity_Id = Guid.Empty;

            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
            DropDownList ddlmatchedddlStatus = (DropDownList)frmEditCityMap.FindControl("ddlStatus");
            HiddenField hdnSelSystemCity_Id = (HiddenField)frmEditCityMap.FindControl("hdnSelSystemCity_Id");

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
                    myCountry_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value);
                    if (ddlSystemCityName.Items.Count > 0)
                        myCity_Id = Guid.Parse(ddlSystemCityName.SelectedItem.Value);
                    else if (!string.IsNullOrWhiteSpace(hdnSelSystemCity_Id.Value))
                        myCity_Id = Guid.Parse(hdnSelSystemCity_Id.Value);



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
                    param.Status = ddlmatchedddlStatus.SelectedItem.Text;
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    myCity_Id = Guid.Empty;
                }
            }
            if (RQ.Count > 0)
            {
                if (mapperSVc.UpdateCityMappingDatat(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
                    fillmappingdata(grdCityMaps.PageIndex);
                    fillmatchingdata("", grdMatchingCity.PageIndex);
                    hdnFlag.Value = "false";
                }
            }
        }

        protected void btnMatchedMapAll_Click(object sender, EventArgs e)
        {
            dvMsg1.Style.Add("display", "none");
            List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();

            Guid myRow_Id = Guid.Empty;
            Guid mySupplier_Id = Guid.Empty;
            Guid? myCountry_Id = Guid.Empty;
            Guid? myCity_Id = Guid.Empty;

            DropDownList ddlSystemCountryName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCountryName");
            DropDownList ddlSystemCityName = (DropDownList)frmEditCityMap.FindControl("ddlSystemCityName");
            DropDownList ddlmatchedddlStatus = (DropDownList)frmEditCityMap.FindControl("ddlStatus");
            HiddenField hdnSelSystemCity_Id = (HiddenField)frmEditCityMap.FindControl("hdnSelSystemCity_Id");
           
            foreach (GridViewRow row in grdMatchingCity.Rows)
            {
                myRow_Id = Guid.Empty;
                mySupplier_Id = Guid.Empty;
                myCountry_Id = Guid.Empty;
                int index = row.RowIndex;
                myRow_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[0].ToString());
                mySupplier_Id = Guid.Parse(grdMatchingCity.DataKeys[index].Values[1].ToString());
                myCountry_Id = Guid.Parse(ddlSystemCountryName.SelectedItem.Value); ;

                if (ddlSystemCityName.Items.Count > 0)
                    myCity_Id = Guid.Parse(ddlSystemCityName.SelectedItem.Value);
                else if (!string.IsNullOrWhiteSpace(hdnSelSystemCity_Id.Value))
                    myCity_Id = Guid.Parse(hdnSelSystemCity_Id.Value);

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
                    param.Status = ddlmatchedddlStatus.SelectedItem.Text;
                    param.Edit_Date = DateTime.Now;
                    param.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;
                    RQ.Add(param);
                    myRow_Id = Guid.Empty;
                    mySupplier_Id = Guid.Empty;
                    myCountry_Id = Guid.Empty;
                    myCity_Id = Guid.Empty;
                }
            }
            if (RQ.Count > 0)
            {
                if (mapperSVc.UpdateCityMappingDatat(RQ))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Matching Records are mapped successfully", BootstrapAlertType.Success);
                    fillmappingdata(grdCityMaps.PageIndex);
                    fillmatchingdata("", grdMatchingCity.PageIndex);
                    hdnFlag.Value = "false";
                }
            }

        }


    }
}