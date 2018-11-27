using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class bulkCityMapping : System.Web.UI.UserControl
    {

        public Guid Accomodation_ID;
        MasterDataDAL masterdata = new MasterDataDAL();
        businessEntityDAL bsuinessEntities = new businessEntityDAL();
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();        
        Controller.MappingSVCs mapperSVc = new Controller.MappingSVCs();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public DataTable dtCityMappingSearchResults = new DataTable();
        MDMSVC.DC_City_Search_RQ RQ = new MDMSVC.DC_City_Search_RQ();
        public static string SortBy = "";
        public static string SortEx = "";
        public static int PageIndex = 0;
        public static string AlphaPageIndex = "";
        public static int PageIndexMapped = 0;
        public static int PageIndexMaster = 0;
        public static Guid CityMapping_Id = Guid.Empty;
        public static Guid CountryKey = Guid.Empty;
        public static Guid CityKey = Guid.Empty;
        public static string CityName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                fillSupplierDropDown();
                pnlSuppDump.Visible = false;
                pnlCityMaster.Visible = false;
                pnlCityMap.Visible = false;
            }

        }

        //private void fillcountrydropdown(string source)
        //{
        //    var resSet = masterdata.GetMasterCountryData();
        //    if (source == "search")
        //    {
        //        ddlCountry.DataSource = resSet;
        //        ddlCountry.DataValueField = "Country_ID";
        //        ddlCountry.DataTextField = "Name";
        //        ddlCountry.DataBind();

        //    }

        //}

        protected void fillSupplierDropDown()
        {
            ddlSupplierName.DataSource = bsuinessEntities.GetSupplierList();
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataBind();
            //ddlSupplierName.SelectedIndex = ddlSupplierName.Items.IndexOf(ddlSupplierName.Items.FindByText("dotw"));
        }


        protected void bindSupplierCityMappingGrid()
        {
            dtCityMappingSearchResults = objMasterDataDAL.GetSupplierCountryMapping(Models.MasterDataDAL.SupplierDataMode.AllSupplierAllCountry, Guid.Empty, Guid.Empty);
            grdSupplierDump.DataSource = dtCityMappingSearchResults;
            grdSupplierDump.DataBind();
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SortBy = "CountryName";
            SortEx = "";
            PageIndex = 0;
            PageIndexMapped = 0;
            PageIndexMaster = 0;
            AlphaPageIndex = "";
            fillmappingdata();
            pnlSuppDump.Visible = true;
            pnlCityMaster.Visible = false;
            pnlCityMap.Visible = false;
            ddlPageSize.Focus();
        }

        private void fillmappingdata()
        {
            MDMSVC.DC_CityMapping_RQ RQ = new MDMSVC.DC_CityMapping_RQ();
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            Guid selCity_ID = Guid.Empty;
            if (ddlSupplierName.SelectedItem.Value != "0")
                RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);

            RQ.Status = "UNMAPPED";

            RQ.PageNo = PageIndex;
            RQ.SortBy = (SortBy + " " + SortEx).Trim();
            RQ.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);

            var res = mapperSVc.GetCityMappingData(RQ);
            grdSupplierDump.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdSupplierDump.VirtualItemCount = res[0].TotalRecords;
                    lblSupDumpCount.Text = res[0].TotalRecords.ToString();
                }
                else
                    lblSupDumpCount.Text = "0";
            }
            else
                lblSupDumpCount.Text = "0";
            grdSupplierDump.PageIndex = PageIndex;
            grdSupplierDump.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text); ;
            //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
            grdSupplierDump.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmappingdata();
        }

        protected void grdSupplierDump_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            fillmappingdata();
        }

        private void fillMasterRQ()
        {
            RQ = new MDMSVC.DC_City_Search_RQ();
            RQ.Country_Id = CountryKey;
            //if (CityName != "")
            RQ.City_Name = CityName;
            RQ.PageNo = PageIndexMaster;
            if (AlphaPageIndex != "")
                RQ.AlphaPageIndex = AlphaPageIndex;
            RQ.PageSize = Convert.ToInt32(ddlPageSizeMaster.SelectedItem.Value);
        }

        private void fillalphapagingdata()
        {
            fillMasterRQ();
            MDMSVC.City_AlphaPage allPage = new MDMSVC.City_AlphaPage();
            allPage.Alpha = "All";
            var res = masterSVc.GetCityAlphaPaging(RQ);
            res.Add(allPage);
            if (res != null)
            {
                rptSupplierDump.DataSource = res;
                rptSupplierDump.DataBind();
            }
        }

        
        private void fillmasterdata()
        {
            fillMasterRQ();
            var res = masterSVc.GetCityMasterData(RQ);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdTLGXMasters.VirtualItemCount = res[0].TotalRecords;
                    lblMasterDataCount.Text = res[0].TotalRecords.ToString();
                }
                else
                    lblMasterDataCount.Text = "0";
                grdTLGXMasters.DataSource = res;
                grdTLGXMasters.PageIndex = PageIndexMaster;
                grdTLGXMasters.PageSize = Convert.ToInt32(ddlPageSizeMaster.SelectedItem.Value);
                grdTLGXMasters.DataBind();
            }
        }
        protected void grdSupplierDump_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                var objCountry = ((System.Web.UI.WebControls.TableCell)row.Controls[0]);
                var objCity = ((System.Web.UI.WebControls.TableCell)row.Controls[4]);
                string country = "";
                string city = "";

                if (!string.IsNullOrEmpty(objCountry.Text))
                {
                    country = objCountry.Text;
                }
                if (!string.IsNullOrEmpty(objCity.Text))
                {
                    city = objCity.Text;
                }
                int index = row.RowIndex;
                if (e.CommandName == "Select")
                {
                    if (myRow_Id != null)
                    {
                        CityMapping_Id = Guid.Parse(grdSupplierDump.DataKeys[index].Values[1].ToString());
                        CountryKey = myRow_Id;
                        txtCityName.Text = city;
                        CityName = txtCityName.Text;
                        AlphaPageIndex = "";
                        fillmasterdata();
                        fillalphapagingdata();
                        pnlSuppDump.Visible = true;
                        pnlCityMaster.Visible = true;
                        CityName = "";
                        txtCityName.Focus();
                    }
                }
            }
            catch
            {

            }
        }

        
        private void fillmappeddata()
        {
            MDMSVC.DC_CityMapping_RQ RQ = new MDMSVC.DC_CityMapping_RQ();
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            Guid selCity_ID = Guid.Empty;
            
            if (ddlSupplierName.SelectedItem.Value != "0")
                //selSupplier_ID = 
                RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
            RQ.Status = "MAPPED";
            RQ.City_Id = CityKey;

            RQ.PageNo = PageIndexMapped;
            RQ.SortBy = (SortBy + " " + SortEx).Trim();
            RQ.PageSize = Convert.ToInt32(ddlPageSizeMapped.SelectedItem.Text);

            var res = mapperSVc.GetCityMappingData(RQ);
            grdCityMaps.DataSource = res;
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdCityMaps.VirtualItemCount = res[0].TotalRecords;
                    lblMappedCount.Text = res[0].TotalRecords.ToString();
                }
                else
                    lblMappedCount.Text = "0";
            }
            else
                lblMappedCount.Text = "0";
            grdCityMaps.PageIndex = PageIndex;
            grdCityMaps.PageSize = Convert.ToInt32(ddlPageSizeMapped.SelectedItem.Text); ;
            //grdCityMaps.DataKeyNames = new string[] {"CityMapping_Id"};
            grdCityMaps.DataBind();
            pnlSuppDump.Visible = true;
            pnlCityMaster.Visible = true;
            pnlCityMap.Visible = true;
        }

        protected void ddlPageSizeMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmasterdata();
        }
        

        protected void grdTLGXMasters_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndexMaster = e.NewPageIndex;
            fillmasterdata();
        }

        protected void grdCityMaps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdCityMaps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndexMapped = e.NewPageIndex;
            fillmappeddata();
        }

        protected void grdCityMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            if (e.CommandName == "Remove")
            {
                if (myRow_Id != null)
                {
                    MDMSVC.DC_CityMapping newObj = new MDMSVC.DC_CityMapping
                    {
                        CityMapping_Id = myRow_Id,
                        Country_Id = CountryKey,
                        City_Id = null,
                        Status = "UNMAPPED",
                        Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,

                    };
                    RQ.Add(newObj);
                    if (mapperSVc.UpdateCityMappingDatat(RQ))
                    {
                        fillmappingdata();
                        fillmappeddata();
                        pnlCityMap.Visible = true;
                    }
                }
            }
        }

        protected void ddlPageSizeMapped_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmappeddata();
        }

        protected void grdTLGXMasters_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                List<MDMSVC.DC_CityMapping> RQ = new List<MDMSVC.DC_CityMapping>();
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                if (e.CommandName == "Select")
                {
                    if (myRow_Id != null)
                    {
                        CityKey = myRow_Id;
                        MDMSVC.DC_CityMapping newObj = new MDMSVC.DC_CityMapping
                        {
                            CityMapping_Id = CityMapping_Id,
                            Country_Id = CountryKey,
                            City_Id = CityKey,
                            Status = "MAPPED",
                            Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value),
                            Edit_Date = DateTime.Now,
                            Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        };
                        RQ.Add(newObj);
                        if (mapperSVc.UpdateCityMappingDatat(RQ))
                        {
                            fillmappeddata();
                            SortBy = "CountryName";
                            SortEx = "";
                            fillmappingdata();
                        }


                    }
                }
            }
        }

        protected void txtCityName_TextChanged(object sender, EventArgs e)
        {
            CityName = txtCityName.Text;
            AlphaPageIndex = "";
            fillmasterdata();
            fillalphapagingdata();
            CityName = "";
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            LinkButton lnkAlphabet = (LinkButton)sender;
            AlphaPageIndex = lnkAlphabet.Text;
            fillmasterdata();
        }

        protected void grdSupplierDump_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdSupplierDump.Rows)
            {
                if (row.RowIndex == grdSupplierDump.SelectedIndex)
                {
                    row.BackColor = System.Drawing.Color.DarkTurquoise;
                }
                else
                {
                    row.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
    }
}