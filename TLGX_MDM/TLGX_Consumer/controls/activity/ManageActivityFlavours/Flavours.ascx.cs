using System;
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
using System.Drawing;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Flavours : System.Web.UI.UserControl
    {
        public string state1 = "collapse";

        public string icon1 = "glyphicon-plus";

        public Guid Activity_Flavour_Id;

        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Controller.MasterDataSVCs masterSVc = new Controller.MasterDataSVCs();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MDMSVC.DC_Activity_Flavour_RQ RQParams = new MDMSVC.DC_Activity_Flavour_RQ();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getFlavourInfo(string.Empty);
                getNonOperatingDays(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }
        }

        public void getFlavourInfo(string calledfrom)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

            var result = AccSvc.GetActivityFlavour(new DC_Activity_Flavour_RQ { Activity_Flavour_Id = Activity_Flavour_Id });

            if (result != null)
            {
                if (result.Count() > 0)
                {
                    //lblProductName.Text = HttpUtility.HtmlEncode(result[0].ProductName) + "<em> (By: " + result[0].SupplierName + ")</em>";
                    Label ParentProdName = (Label)this.Parent.FindControl("lblProductName");
                    ParentProdName.Text = HttpUtility.HtmlEncode(result[0].ProductName) + "<em> (By: " + result[0].SupplierName + ")</em>";

                    Label ParentProdStatus = (Label)this.Parent.FindControl("lblActivityStatus");
                    ParentProdStatus.Text = HttpUtility.HtmlEncode(result[0].Activity_Status);

                    HtmlGenericControl dvproductheader = (HtmlGenericControl)this.Parent.FindControl("dvproductheader");
                    //Text Colour coding

                    if (ParentProdStatus.Text.ToUpper() == "REVIEW COMPLETED")
                    {
                        dvproductheader.Style.Add(HtmlTextWriterStyle.Color, Color.Green.Name);
                    }
                    else if (ParentProdStatus.Text.ToUpper() == "UNDER REVIEW")
                    {
                        dvproductheader.Style.Add(HtmlTextWriterStyle.Color, Color.Orange.Name);
                    }
                    else if (ParentProdStatus.Text.ToUpper() == "NOT YET REVIEWED")
                    {
                        dvproductheader.Style.Add(HtmlTextWriterStyle.Color, Color.Red.Name);
                    }

                    if (calledfrom == "header")
                        return;

                    DropDownList parentddlActivity_Flavour_Status = (DropDownList)this.Parent.FindControl("ddlActivity_Flavour_Status");
                    parentddlActivity_Flavour_Status.SelectedIndex = parentddlActivity_Flavour_Status.Items.IndexOf(parentddlActivity_Flavour_Status.Items.FindByText(result[0].Activity_Status.ToString()));

                    TextBox txtActivity_Flavour_StatusNotes = (TextBox)this.Parent.FindControl("txtActivity_Flavour_StatusNotes");
                    txtActivity_Flavour_StatusNotes.Text = HttpUtility.HtmlEncode(result[0].Activity_StatusNotes);

                    lblSuppCity.Text = result[0].SupplierCity;
                    lblSuppCountry.Text = result[0].SupplierCountry;
                    lblSuppProductType.Text = result[0].SupplierProductType;
                    lblSuppProdNameSubType.Text = result[0].SupplierProductNameSubType;
                    //TourType
                    lblSupplierTourType.Text = result[0].SupplierTourType;
                    txtTourType.Text = result[0].Activity_TourType;

                    lblSupplierLocation.Text = result[0].SupplierLocation;
                    HtmlTextArea txtLocation = (HtmlTextArea)this.FindControl("txtLocation");
                    txtLocation.InnerText = result[0].Location ?? "";
                    //GetActivityDescriptions(Activity_Flavour_Id);

                    fillcoutries();

                    if (result[0].Country_Id != null)
                    {
                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(result[0].Country_Id.ToString()));

                        fillcities(result[0].Country_Id ?? Guid.Empty);

                        if (result[0].City_Id != null)
                        {
                            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(result[0].City_Id.ToString()));
                        }
                    }

                    fillSuitableFor();

                    fillSpecials();

                    fillPhysicalIntensity();

                    BindClassiFicationAttributes(Activity_Flavour_Id);

                    fillInterestTypedData();

                    //fillproductcaterogysubtype();

                    if (result[0].Categories != null)
                    {
                        if (result[0].Categories.Count() > 0)
                        {
                            BindActivityTypes(result[0].Categories);
                        }
                    }

                    fillOperatingDaysWithWeekdays(Activity_Flavour_Id);

                    BindSupplierClassifications(Activity_Flavour_Id);
                }
            }
        }



        private void fillInterestTypedData()
        {
            try
            {
                ddlInterestType.Items.Clear();
                var result = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "InterestType").MasterAttributeValues;
                ddlInterestType.DataSource = result;
                ddlInterestType.DataTextField = "AttributeValue";
                ddlInterestType.DataValueField = "MasterAttributeValue_Id";
                ddlInterestType.DataBind();
                ddlInterestType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //Bind Method for Classification Mapping
        #region Bind Method for Classification Mapping
        private void BindActivityTypes(DC_Activity_CategoryTypes[] Categories)
        {
            //Fill SubCat
            var selectedInterestType = BindInterestType(Categories.Where(w => w.SysInterestTypeId != null).Select(s => (s.SysInterestTypeId ?? Guid.Empty).ToString()).ToList());
            var selectedSubCat = BindProductSubCat(Categories.Where(w => w.SysProdSubCatId != null).Select(s => (s.SysProdSubCatId ?? Guid.Empty).ToString()).ToList(), selectedInterestType);
            var selectedProdType = BindProductNameType(Categories.Where(w => w.SysProdTypeId != null).Select(s => (s.SysProdTypeId ?? Guid.Empty).ToString()).ToList(), selectedSubCat);
            BindProductNameSubType(Categories.Where(w => w.SysProdSubTypeId != null).Select(s => (s.SysProdSubTypeId ?? Guid.Empty).ToString()).ToList(), selectedProdType);
            //var selectedProdSubType = BindProductNameSubType(Categories.Where(w => w.SysProdSubTypeId != null).Select(s => (s.SysProdSubTypeId ?? Guid.Empty).ToString()).ToList(), selectedProdType);
            // BindProductNameSubType(Categories.Where(w => w.SysProdSubTypeId != null).Select(s => (s.SysProdSubTypeId ?? Guid.Empty).ToString()).ToList(), selectedProdSubType);

        }
        /// <summary>
        /// Bind Interest Type List
        /// </summary>
        /// <param name="InterestTypeList"></param>
        /// <returns></returns>
        private List<string> BindInterestType(List<string> InterestTypeList)
        {
            List<InterestTypeData> ptl = new List<InterestTypeData>();

            foreach (var interesttypeid in InterestTypeList)
            {

                if (ptl.Where(w => w.InterestType_Id == interesttypeid.ToString()).Count() == 0)
                {

                    ptl.Add(new InterestTypeData
                    {
                        InterestType = ddlInterestType.Items.FindByValue(interesttypeid).Text,
                        InterestType_Id = interesttypeid
                    });

                }
            }


            repInterestType.DataSource = ptl;
            repInterestType.DataBind();
            fillproductsubtype(ptl.Select(s => s.InterestType_Id).ToList());

            return ptl.Select(s => s.InterestType_Id).ToList();
        }
        /// <summary>
        /// Bind Product SubCat List
        /// </summary>
        /// <param name="ProductSubCategoryList"></param>
        /// <returns></returns>
        private List<string> BindProductSubCat(List<string> ProductSubCategoryList, List<string> selectedInterestType)
        {
            ddlProdcategorySubType.Items.Clear();
            List<SubCategoryData> ptl = new List<SubCategoryData>();

            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
            var result = (from s in dropdownvalues
                          where selectedInterestType.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                          orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                          select new { AttributeValueOri = s.AttributeValue, AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id });

            ddlProdcategorySubType.DataSource = result;
            ddlProdcategorySubType.DataTextField = "AttributeValue";
            ddlProdcategorySubType.DataValueField = "MasterAttributeValue_Id";
            ddlProdcategorySubType.DataBind();
            ddlProdcategorySubType.Items.Insert(0, new ListItem("-Select-", "0"));

            foreach (var prodtypeid in ProductSubCategoryList)
            {
                var searchRes = result.Where(w => w.MasterAttributeValue_Id.ToString() == prodtypeid).Select(s => s).FirstOrDefault();
                if (searchRes != null)
                {

                    foreach (var subcatid in ProductSubCategoryList)
                    {
                        if (ptl.Where(w => w.SubCategory_Id.ToLower() == subcatid.ToLower()).Count() == 0)
                        {
                            if (ddlProdcategorySubType.Items.FindByValue(subcatid) != null)
                            {
                                ptl.Add(new SubCategoryData
                                {
                                    SubCategory = searchRes.AttributeValue,
                                    SubCategory_Id = Convert.ToString(searchRes.MasterAttributeValue_Id)
                                });
                            }
                        }
                    }
                }
            }

            repSubCategory.DataSource = ptl;
            repSubCategory.DataBind();

            fillProductType(ptl.Select(s => s.SubCategory_Id).ToList());

            return ptl.Select(s => s.SubCategory_Id).ToList();
        }
        /// <summary>
        /// Bind Proudct Type List
        /// </summary>
        /// <param name="ProductNameTypeList"></param>
        /// <param name="selectedSubCat"></param>
        /// <returns></returns>
        private List<string> BindProductNameType(List<string> ProductNameTypeList, List<string> selectedSubCat)
        {
            List<ProductTypeData> ptl = new List<ProductTypeData>();

            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            var result = (from s in dropdownvalues
                          where selectedSubCat.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                          orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                          select new { AttributeValueOri = s.AttributeValue, AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id });

            foreach (var prodtypeid in ProductNameTypeList)
            {
                var searchRes = result.Where(w => w.MasterAttributeValue_Id.ToString() == prodtypeid).Select(s => s).FirstOrDefault();
                if (searchRes != null)
                {
                    if (ptl.Where(w => w.ProductType_Id == searchRes.MasterAttributeValue_Id.ToString()).Count() == 0)
                    {
                        ptl.Add(new ProductTypeData
                        {
                            ProductType = searchRes.AttributeValue,
                            ProductType_Id = searchRes.MasterAttributeValue_Id.ToString()
                        });
                    }
                }


            }

            repProductType.DataSource = ptl;
            repProductType.DataBind();

            fillproductsubtype(ptl.Select(s => s.ProductType_Id).ToList());

            return ptl.Select(s => s.ProductType_Id).ToList();
        }
        /// <summary>
        /// Bind Product SubType List
        /// </summary>
        /// <param name="ProductNameSubTypeList"></param>
        /// <param name="selectedProdName"></param>
        /// <returns></returns>
        private void BindProductNameSubType(List<string> ProductNameSubTypeList, List<string> selectedProdName)
        {
            List<ProductSubTypeData> ptl = new List<ProductSubTypeData>();

            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductSubType").MasterAttributeValues;
            var result = (from s in dropdownvalues
                          where selectedProdName.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                          orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                          select new { AttributeValueOri = s.AttributeValue, AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id }).ToList();

            foreach (var prodsubtypeid in ProductNameSubTypeList)
            {
                var searchRes = result.Where(w => w.MasterAttributeValue_Id.ToString() == prodsubtypeid).Select(s => s).FirstOrDefault();
                if (searchRes != null)
                {
                    if (ptl.Where(w => w.ProductSubType_Id == searchRes.MasterAttributeValue_Id.ToString()).Count() == 0)
                    {
                        ptl.Add(new ProductSubTypeData
                        {
                            ProductSubType = searchRes.AttributeValue,
                            ProductSubType_Id = searchRes.MasterAttributeValue_Id.ToString()
                        });
                    }
                }
            }

            repProductSubType.DataSource = ptl;
            repProductSubType.DataBind();

            // fillInterestType(ptl.Select(s => s.ProductSubType_Id).ToList());

            //return ptl.Select(s => s.ProductSubType_Id).ToList();
        }
        #endregion


        //Done
        //private void GetActivityDescriptions(Guid Activity_Flavour_Id)
        //{
        //    var result = AccSvc.GetActivityDescription(new DC_Activity_Descriptions_RQ
        //    {
        //        Activity_Flavour_Id = Activity_Flavour_Id,
        //        PageNo = 0,
        //        PageSize = int.MaxValue
        //    });

        //    if (result != null)
        //    {
        //        if (result.Count() > 0)
        //        {
        //            var ShortDesc = result.Where(w => w.DescriptionType == "Short").Select(s => s.Description).FirstOrDefault();
        //            ShortDesc = Regex.Replace(ShortDesc, "<.*?>", string.Empty);
        //            txtShortDescription.Text = ShortDesc;

        //            var LongDesc = result.Where(w => w.DescriptionType == "Long").Select(s => s.Description).FirstOrDefault();
        //            LongDesc = Regex.Replace(LongDesc, "<.*?>", string.Empty);
        //            txtLongDescription.Text = LongDesc;
        //        }
        //    }
        //}

        //Done
        private void fillcoutries()
        {
            var countryList = masterSVc.GetAllCountries();
            ddlCountry.DataSource = countryList;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
        }

        //Done
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedIndex > 0)
            {
                Guid Country_Id;
                if (Guid.TryParse(ddlCountry.SelectedValue, out Country_Id))
                {
                    fillcities(Country_Id);
                }
                else
                {
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, new ListItem("-Select-", "0"));
                }

            }

        }

        //Done
        private void fillcities(Guid Country_Id)
        {
            ddlCity.Items.Clear();
            var CityList = masterSVc.GetMasterCityData(Country_Id.ToString());
            if (CityList != null)
            {
                if (CityList.Count() > 0)
                {
                    ddlCity.DataSource = CityList;
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataValueField = "City_Id";
                    ddlCity.DataBind();
                }
            }
            ddlCity.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        //Done
        private void fillSuitableFor()
        {
            var res = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "SuitableFor").MasterAttributeValues;

            if (res != null)
            {
                chklstSuitableFor.DataSource = res;
                chklstSuitableFor.DataTextField = "AttributeValue";
                chklstSuitableFor.DataValueField = "MasterAttributeValue_Id";
                chklstSuitableFor.DataBind();
            }
            else
            {
                chklstSuitableFor.DataSource = null;
                chklstSuitableFor.DataBind();
            }
        }

        private void fillSpecials()
        {
            var res = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "Specials").MasterAttributeValues;

            if (res != null)
            {
                chklstSpecials.DataSource = res;
                chklstSpecials.DataTextField = "AttributeValue";
                chklstSpecials.DataValueField = "MasterAttributeValue_Id";
                chklstSpecials.DataBind();
            }
            else
            {
                chklstSpecials.DataSource = null;
                chklstSpecials.DataBind();
            }
        }

        //Done
        private void fillPhysicalIntensity()
        {
            var res = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "PhysicalIntensity").MasterAttributeValues;
            if (res != null)
            {
                ddlPhysicalIntensity.DataSource = res;
                ddlPhysicalIntensity.DataTextField = "AttributeValue";
                ddlPhysicalIntensity.DataValueField = "MasterAttributeValue_Id";
                ddlPhysicalIntensity.DataBind();
            }
            else
            {
                ddlPhysicalIntensity.DataSource = null;
                ddlPhysicalIntensity.DataBind();
            }
        }





        private void fillOperatingDaysWithWeekdays(Guid Activity_Flavour_Id)
        {
            var result = AccSvc.GetActivityDaysOfWeek(Activity_Flavour_Id);
            if (result != null)
            {
                repOperatingDays.DataSource = result;
                repOperatingDays.DataBind();
            }
        }

        private void BindSupplierClassifications(Guid Activity_Flavour_Id)
        {
            MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ = new DC_Activity_ClassificationAttributes_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            RQ.AttributeType = "Duration";
            var result = AccSvc.GetActivityClasificationAttributes(RQ);
            repSupplierInformation.DataSource = result;
            repSupplierInformation.DataBind();
        }

        //private void BindDataSource()
        //{
        //    Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

        //    MDMSVC.DC_Activity_SupplierProductMapping_RQ _obj = new MDMSVC.DC_Activity_SupplierProductMapping_RQ();
        //    _obj.Activity_ID = Activity_Flavour_Id;
        //    var result = AccSvc.GetActivitySupplierProductMapping(_obj);
        //    if (result.Count > 0 || result != null)
        //    {
        //        foreach (DC_Activity_SupplierProductMapping res in result)
        //        {
        //            lblSuppCountry.Text = res.SupplierCountryName.ToString();
        //            lblSuppCity.Text = res.SupplierCityName.ToString();
        //            lblSuppProductType.Text = res.SupplierProductType.ToString();
        //            lblSuppProdNameSubType.Text = res.SupplierProductName.ToString();
        //            lblSuppSuitableFor.Text = res.SupplierType.ToString();
        //            lblSuppPhysicalIntensity.Text = res.SupplierType.ToString();
        //        }
        //    }

        //}

        private void fillDDLSession(DropDownList ddlSession)
        {
            try
            {
                ddlSession.Items.Clear();
                ddlSession.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivitySession").MasterAttributeValues;
                ddlSession.DataTextField = "AttributeValue";
                ddlSession.DataValueField = "MasterAttributeValue_Id";
                ddlSession.DataBind();
                ddlSession.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fillActivityDuration(DropDownList ddlDuration)
        {
            try
            {
                ddlDuration.Items.Clear();
                ddlDuration.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityDuration").MasterAttributeValues;
                ddlDuration.DataTextField = "AttributeValue";
                ddlDuration.DataValueField = "MasterAttributeValue_Id";
                ddlDuration.DataBind();
                ddlDuration.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void UpdateFlavour(Guid Activity_Flavour_Id)
        {

            #region Update Flavour Table

            MDMSVC.DC_Activity_Flavour FlavData = new MDMSVC.DC_Activity_Flavour();

            FlavData.Activity_Flavour_Id = Activity_Flavour_Id;
            FlavData.Edit_User = System.Web.HttpContext.Current.User.Identity.Name;

            if (ddlCountry.SelectedIndex > 0)
            {
                FlavData.Country = ddlCountry.SelectedItem.Text;
                FlavData.Country_Id = Guid.Parse(ddlCountry.SelectedValue);
            }

            if (ddlCity.SelectedIndex > 0)
            {
                FlavData.City = ddlCity.SelectedItem.Text;
                FlavData.City_Id = Guid.Parse(ddlCity.SelectedValue);
            }

            FlavData.ProductCategory = txtProdCategory.Text;

            List<DC_Activity_CategoryTypes> CategoryTypes = new List<DC_Activity_CategoryTypes>();
            foreach (RepeaterItem item in repProductSubType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveProductSubType = (LinkButton)item.FindControl("btnRemoveProductSubType");
                    Label lblProductSubType = (Label)item.FindControl("lblProductSubType");
                    CategoryTypes.Add(new DC_Activity_CategoryTypes
                    {
                        SysProdSubTypeId = Guid.Parse(btnRemoveProductSubType.CommandArgument),
                        Activity_Flavour_Id = Activity_Flavour_Id,
                        User = System.Web.HttpContext.Current.User.Identity.Name
                    });
                }
            }
            FlavData.Categories = CategoryTypes.ToArray();


            //SubCat
            List<SubCategoryData> ptl = new List<SubCategoryData>();
            foreach (RepeaterItem item in repSubCategory.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveSubCategory = (LinkButton)item.FindControl("btnRemoveSubCategory");
                    Label lblSubCategory = (Label)item.FindControl("lblSubCategory");
                    ptl.Add(new SubCategoryData
                    {
                        SubCategory = lblSubCategory.Text,
                        SubCategory_Id = btnRemoveSubCategory.CommandArgument
                    });
                }
            }
            FlavData.ProductCategorySubType = string.Join(",", ptl.Select(s => s.SubCategory_Id).ToList());

            //ProdType
            List<ProductTypeData> ptyl = new List<ProductTypeData>();
            foreach (RepeaterItem item in repProductType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveProductType = (LinkButton)item.FindControl("btnRemoveProductType");
                    Label lblProductType = (Label)item.FindControl("lblProductType");
                    ptyl.Add(new ProductTypeData
                    {
                        ProductType = lblProductType.Text,
                        ProductType_Id = btnRemoveProductType.CommandArgument
                    });
                }
            }
            FlavData.ProductType = string.Join(",", ptyl.Select(s => s.ProductType_Id).ToList());

            //TourType
            FlavData.Activity_TourType = txtTourType.Text.Trim();
            FlavData.Location = txtLocation.Value.Trim();

            //ProdSubType
            List<ProductSubTypeData> pstl = new List<ProductSubTypeData>();
            foreach (RepeaterItem item in repProductSubType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveProductSubType = (LinkButton)item.FindControl("btnRemoveProductSubType");
                    Label lblProductSubType = (Label)item.FindControl("lblProductSubType");
                    pstl.Add(new ProductSubTypeData
                    {
                        ProductSubType = lblProductSubType.Text,
                        ProductSubType_Id = btnRemoveProductSubType.CommandArgument
                    });
                }
            }
            FlavData.ProductNameSubType = string.Join(",", pstl.Select(s => s.ProductSubType_Id).ToList());

            //InterestType
            List<InterestTypeData> itl = new List<InterestTypeData>();
            foreach (RepeaterItem item in repInterestType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveInterestType = (LinkButton)item.FindControl("btnRemoveInterestType");
                    Label lblInterestType = (Label)item.FindControl("lblInterestType");
                    itl.Add(new InterestTypeData
                    {
                        InterestType = lblInterestType.Text,
                        InterestType_Id = btnRemoveInterestType.CommandArgument
                    });
                }
            }
            FlavData.InterestType = string.Join(",", itl.Select(s => s.InterestType_Id).ToList());

            var result = AccSvc.AddUpdateActivityFlavour(FlavData);

            #endregion

            #region Update SuitableFor & PhysicalIntensity
            List<DC_Activity_CA_CRUD> updateCA = new List<DC_Activity_CA_CRUD>();

            List<string> AttributeValues = new List<string>();
            //SuitableFor
            foreach (ListItem checkedSuitable in chklstSuitableFor.Items)
            {
                if (checkedSuitable.Selected)
                {
                    AttributeValues.Add(checkedSuitable.Text);
                }
            }
            updateCA.Add(new DC_Activity_CA_CRUD
            {
                Activity_Flavour_Id = Activity_Flavour_Id,
                AttributeType = "Product",
                AttributeSubType = "SuitableFor",
                AttributeValues = AttributeValues.ToArray(),
                User = System.Web.HttpContext.Current.User.Identity.Name
            });

            //Specials
            AttributeValues = new List<string>();
            foreach (ListItem checkedSpecial in chklstSpecials.Items)
            {
                if (checkedSpecial.Selected)
                {
                    AttributeValues.Add(checkedSpecial.Text);
                }
            }
            updateCA.Add(new DC_Activity_CA_CRUD
            {
                Activity_Flavour_Id = Activity_Flavour_Id,
                AttributeType = "Product",
                AttributeSubType = "Specials",
                AttributeValues = AttributeValues.ToArray(),
                User = System.Web.HttpContext.Current.User.Identity.Name
            });

            //PhysicalIntensity
            AttributeValues = new List<string>();
            if (ddlPhysicalIntensity.SelectedIndex > 0)
            {
                AttributeValues.Add(ddlPhysicalIntensity.SelectedItem.Text);
            }
            updateCA.Add(new DC_Activity_CA_CRUD
            {
                Activity_Flavour_Id = Activity_Flavour_Id,
                AttributeType = "Product",
                AttributeSubType = "PhysicalIntensity",
                AttributeValues = AttributeValues.ToArray(),
                User = System.Web.HttpContext.Current.User.Identity.Name
            });

            var resultUpdateCA = AccSvc.AddUpdateActivityFlavourCA(updateCA);

            #endregion

            #region Update DaysOfOperation
            var OperatingDaysToUpdate = CollectAllOperatingDaysInfoOnPage();
            repOperatingDays.DataSource = OperatingDaysToUpdate;
            repOperatingDays.DataBind();
            if (OperatingDaysToUpdate.Count == 0)
            {
                OperatingDaysToUpdate.Add(new DC_Activity_OperatingDays
                {
                    Activity_DaysOfOperation_Id = Guid.Empty,
                    Activity_Flavor_ID = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]),
                    DaysOfWeek = new List<DC_Activity_DaysOfWeek>().ToArray()
                });
            }
            var resultUpdateDOO = AccSvc.AddUpdateActivityDaysOfWeek(OperatingDaysToUpdate);


            #endregion

            BootstrapAlert.BootstrapAlertMessage(dvMsg, "Flavour updated successfully", BootstrapAlertType.Success);
        }


        //Fill Method for Classification Mapping
        #region Fill Method Cateories
        private void fillInterestType(List<string> ProductSubType_Ids)
        {
            ddlInterestType.Items.Clear();
            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "InterestType").MasterAttributeValues;
            var result = (from s in dropdownvalues
                          where ProductSubType_Ids.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                          orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                          select new { AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id }).ToList();

            ddlInterestType.DataSource = result;
            ddlInterestType.DataTextField = "AttributeValue";
            ddlInterestType.DataValueField = "MasterAttributeValue_Id";
            ddlInterestType.DataBind();
            ddlInterestType.Items.Insert(0, new ListItem("-Select-", "0"));
            List<InterestTypeData> pstl = new List<InterestTypeData>();
            foreach (RepeaterItem item in repInterestType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveInterestType = (LinkButton)item.FindControl("btnRemoveInterestType");
                    if (result.Where(w => w.MasterAttributeValue_Id == Guid.Parse(btnRemoveInterestType.CommandArgument)).Count() > 0)
                    {
                        Label lblInterestType = (Label)item.FindControl("lblInterestType");
                        pstl.Add(new InterestTypeData
                        {
                            InterestType = lblInterestType.Text,
                            InterestType_Id = btnRemoveInterestType.CommandArgument
                        });
                    }
                }
            }

            repInterestType.DataSource = pstl;
            repInterestType.DataBind();
        }
        private void fillproductcaterogysubtype(List<string> interestTypeids)
        {
            try
            {
                ddlProdcategorySubType.Items.Clear();
                var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductCategory").MasterAttributeValues;
                var result = (from s in dropdownvalues
                              where interestTypeids.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                              orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                              select new { AttributeValueOri = s.AttributeValue, AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id });
                ddlProdcategorySubType.DataSource = result;
                ddlProdcategorySubType.DataTextField = "AttributeValue";
                ddlProdcategorySubType.DataValueField = "MasterAttributeValue_Id";
                ddlProdcategorySubType.DataBind();
                ddlProdcategorySubType.Items.Insert(0, new ListItem("-Select-", "0"));

                List<SubCategoryData> ptl = new List<SubCategoryData>();
                foreach (RepeaterItem item in repSubCategory.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveSubCategory = (LinkButton)item.FindControl("btnRemoveSubCategory");
                        if (result.Where(w => w.MasterAttributeValue_Id == Guid.Parse(btnRemoveSubCategory.CommandArgument)).Count() > 0)
                        {
                            Label lblSubCategory = (Label)item.FindControl("lblSubCategory");
                            ptl.Add(new SubCategoryData
                            {
                                SubCategory = lblSubCategory.Text,
                                SubCategory_Id = btnRemoveSubCategory.CommandArgument
                            });
                        }
                    }
                }
                repSubCategory.DataSource = ptl;
                repSubCategory.DataBind();

                fillProductType(ptl.Select(s => s.SubCategory_Id).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void fillProductType(List<string> SubCategoryIds)
        {
            ddlProductType.Items.Clear();
            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            var result = (from s in dropdownvalues
                          where SubCategoryIds.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                          orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                          select new { AttributeValueOri = s.AttributeValue, AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id });
            ddlProductType.DataSource = result;
            ddlProductType.DataTextField = "AttributeValue";
            ddlProductType.DataValueField = "MasterAttributeValue_Id";
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, new ListItem("-Select-", "0"));

            List<ProductTypeData> ptl = new List<ProductTypeData>();
            foreach (RepeaterItem item in repProductType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveProductType = (LinkButton)item.FindControl("btnRemoveProductType");
                    if (result.Where(w => w.MasterAttributeValue_Id == Guid.Parse(btnRemoveProductType.CommandArgument)).Count() > 0)
                    {
                        Label lblProductType = (Label)item.FindControl("lblProductType");
                        ptl.Add(new ProductTypeData
                        {
                            ProductType = lblProductType.Text,
                            ProductType_Id = btnRemoveProductType.CommandArgument
                        });
                    }
                }
            }
            repProductType.DataSource = ptl;
            repProductType.DataBind();

            fillproductsubtype(ptl.Select(s => s.ProductType_Id).ToList());

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
        private void fillproductsubtype(List<string> ProductType_Ids)
        {
            ddlProdNameSubType.Items.Clear();
            var dropdownvalues = LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductSubType").MasterAttributeValues;
            var result = (from s in dropdownvalues
                          where ProductType_Ids.Contains((s.ParentAttributeValue_Id ?? Guid.Empty).ToString())
                          orderby s.ParentAttributeValue.Trim(), s.AttributeValue.Trim()
                          select new { AttributeValue = (s.ParentAttributeValue.Trim() == string.Empty) ? s.AttributeValue : "[" + s.ParentAttributeValue + "] " + s.AttributeValue, MasterAttributeValue_Id = s.MasterAttributeValue_Id }).ToList();
            ddlProdNameSubType.DataSource = result;
            ddlProdNameSubType.DataTextField = "AttributeValue";
            ddlProdNameSubType.DataValueField = "MasterAttributeValue_Id";
            ddlProdNameSubType.DataBind();
            ddlProdNameSubType.Items.Insert(0, new ListItem("-Select-", "0"));

            List<ProductSubTypeData> pstl = new List<ProductSubTypeData>();
            foreach (RepeaterItem item in repProductSubType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnRemoveProductSubType = (LinkButton)item.FindControl("btnRemoveProductSubType");
                    if (result.Where(w => w.MasterAttributeValue_Id == Guid.Parse(btnRemoveProductSubType.CommandArgument)).Count() > 0)
                    {
                        Label lblProductSubType = (Label)item.FindControl("lblProductSubType");
                        pstl.Add(new ProductSubTypeData
                        {
                            ProductSubType = lblProductSubType.Text,
                            ProductSubType_Id = btnRemoveProductSubType.CommandArgument
                        });
                    }
                }
            }

            repProductSubType.DataSource = pstl;
            repProductSubType.DataBind();
            // fillInterestType(pstl.Select(s => s.ProductSubType_Id).ToList());
        }
        #endregion

        //Add Interest Type, SubCategory, Product Type & Product SubType
        #region Add Category Cateories
        protected void btnAddInterestType_Click(object sender, EventArgs e)
        {
            if (ddlInterestType.SelectedIndex > 0)
            {
                bool bDuplicate = false;

                List<InterestTypeData> itl = new List<InterestTypeData>();
                foreach (RepeaterItem item in repInterestType.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveInterestType = (LinkButton)item.FindControl("btnRemoveInterestType");
                        Label lblInterestType = (Label)item.FindControl("lblInterestType");

                        itl.Add(new InterestTypeData
                        {
                            InterestType = lblInterestType.Text,
                            InterestType_Id = btnRemoveInterestType.CommandArgument
                        });
                        if (ddlInterestType.SelectedValue.ToLower() == btnRemoveInterestType.CommandArgument.ToLower())
                        {
                            bDuplicate = true;
                        };
                    }

                }

                if (!bDuplicate)
                {
                    itl.Add(new InterestTypeData
                    {
                        InterestType = ddlInterestType.SelectedItem.Text,
                        InterestType_Id = ddlInterestType.SelectedValue
                    });
                }
                repInterestType.DataSource = itl;
                repInterestType.DataBind();
                fillproductcaterogysubtype(itl.Select(s => s.InterestType_Id).ToList());

            }
        }
        protected void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            if (ddlProdcategorySubType.SelectedIndex > 0)
            {
                bool bDuplicate = false;

                List<SubCategoryData> ptl = new List<SubCategoryData>();
                foreach (RepeaterItem item in repSubCategory.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveSubCategory = (LinkButton)item.FindControl("btnRemoveSubCategory");
                        Label lblSubCategory = (Label)item.FindControl("lblSubCategory");
                        ptl.Add(new SubCategoryData
                        {
                            SubCategory = lblSubCategory.Text,
                            SubCategory_Id = btnRemoveSubCategory.CommandArgument
                        });

                        if (ddlProdcategorySubType.SelectedValue.ToLower() == btnRemoveSubCategory.CommandArgument.ToLower())
                        {
                            bDuplicate = true;
                        }
                    }
                }

                if (!bDuplicate)
                {
                    ptl.Add(new SubCategoryData
                    {
                        SubCategory = ddlProdcategorySubType.SelectedItem.Text,
                        SubCategory_Id = ddlProdcategorySubType.SelectedValue
                    });
                }

                repSubCategory.DataSource = ptl;
                repSubCategory.DataBind();

                fillProductType(ptl.Select(s => s.SubCategory_Id).ToList());
            }
        }
        protected void btnAddProductType_Click(object sender, EventArgs e)
        {
            if (ddlProductType.SelectedIndex > 0)
            {
                bool bDuplicate = false;

                List<ProductTypeData> ptl = new List<ProductTypeData>();
                foreach (RepeaterItem item in repProductType.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveProductType = (LinkButton)item.FindControl("btnRemoveProductType");
                        Label lblProductType = (Label)item.FindControl("lblProductType");
                        ptl.Add(new ProductTypeData
                        {
                            ProductType = lblProductType.Text,
                            ProductType_Id = btnRemoveProductType.CommandArgument
                        });

                        if (ddlProductType.SelectedValue.ToLower() == btnRemoveProductType.CommandArgument.ToLower())
                        {
                            bDuplicate = true;
                        }
                    }
                }

                if (!bDuplicate)
                {
                    ptl.Add(new ProductTypeData
                    {
                        ProductType = ddlProductType.SelectedItem.Text,
                        ProductType_Id = ddlProductType.SelectedValue
                    });
                }

                repProductType.DataSource = ptl;
                repProductType.DataBind();

                fillproductsubtype(ptl.Select(s => s.ProductType_Id).ToList());
            }
        }
        protected void btnAddProductSubType_Click(object sender, EventArgs e)
        {
            if (ddlProdNameSubType.SelectedIndex > 0)
            {
                bool bDuplicate = false;

                List<ProductSubTypeData> ptl = new List<ProductSubTypeData>();
                foreach (RepeaterItem item in repProductSubType.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveProductSubType = (LinkButton)item.FindControl("btnRemoveProductSubType");
                        Label lblProductSubType = (Label)item.FindControl("lblProductSubType");
                        ptl.Add(new ProductSubTypeData
                        {
                            ProductSubType = lblProductSubType.Text,
                            ProductSubType_Id = btnRemoveProductSubType.CommandArgument
                        });

                        if (ddlProdNameSubType.SelectedValue.ToLower() == btnRemoveProductSubType.CommandArgument.ToLower())
                        {
                            bDuplicate = true;
                        }
                    }
                }

                if (!bDuplicate)
                {
                    ptl.Add(new ProductSubTypeData
                    {
                        ProductSubType = ddlProdNameSubType.SelectedItem.Text,
                        ProductSubType_Id = ddlProdNameSubType.SelectedValue
                    });
                }

                repProductSubType.DataSource = ptl;
                repProductSubType.DataBind();

                // fillInterestType(ptl.Select(s => s.ProductSubType_Id).ToList());
            }
        }
        #endregion


        //ItemCommand for Classification Mapping
        #region Cateories Remove
        protected void repInterestType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveInterestType")
            {
                List<InterestTypeData> ptl = new List<InterestTypeData>();
                foreach (RepeaterItem item in repInterestType.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveInterestType = (LinkButton)item.FindControl("btnRemoveInterestType");
                        if (btnRemoveInterestType.CommandArgument.ToLower() == e.CommandArgument.ToString().ToLower())
                        {
                            continue;
                        }

                        Label lblInterestType = (Label)item.FindControl("lblInterestType");
                        ptl.Add(new InterestTypeData
                        {
                            InterestType = lblInterestType.Text,
                            InterestType_Id = btnRemoveInterestType.CommandArgument
                        });
                    }
                }

                repInterestType.DataSource = ptl;
                repInterestType.DataBind();
                fillproductcaterogysubtype(ptl.Select(s => s.InterestType_Id).ToList());
            }
        }
        protected void repSubCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveSubCategory")
            {
                List<SubCategoryData> ptl = new List<SubCategoryData>();
                foreach (RepeaterItem item in repSubCategory.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveSubCategory = (LinkButton)item.FindControl("btnRemoveSubCategory");
                        if (btnRemoveSubCategory.CommandArgument.ToLower() == e.CommandArgument.ToString().ToLower())
                        {
                            continue;
                        }

                        Label lblSubCategory = (Label)item.FindControl("lblSubCategory");
                        ptl.Add(new SubCategoryData
                        {
                            SubCategory = lblSubCategory.Text,
                            SubCategory_Id = btnRemoveSubCategory.CommandArgument
                        });
                    }
                }

                repSubCategory.DataSource = ptl;
                repSubCategory.DataBind();

                fillProductType(ptl.Select(s => s.SubCategory_Id).ToList());
            }
        }
        protected void repProductType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveProductType")
            {
                List<ProductTypeData> ptl = new List<ProductTypeData>();
                foreach (RepeaterItem item in repProductType.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveProductType = (LinkButton)item.FindControl("btnRemoveProductType");
                        if (btnRemoveProductType.CommandArgument.ToLower() == e.CommandArgument.ToString().ToLower())
                        {
                            continue;
                        }

                        Label lblProductType = (Label)item.FindControl("lblProductType");
                        ptl.Add(new ProductTypeData
                        {
                            ProductType = lblProductType.Text,
                            ProductType_Id = btnRemoveProductType.CommandArgument
                        });
                    }
                }

                repProductType.DataSource = ptl;
                repProductType.DataBind();

                fillproductsubtype(ptl.Select(s => s.ProductType_Id).ToList());

            }
        }
        protected void repProductSubType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveProductSubType")
            {
                List<ProductSubTypeData> ptl = new List<ProductSubTypeData>();
                foreach (RepeaterItem item in repProductSubType.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton btnRemoveProductSubType = (LinkButton)item.FindControl("btnRemoveProductSubType");
                        if (btnRemoveProductSubType.CommandArgument.ToLower() == e.CommandArgument.ToString().ToLower())
                        {
                            continue;
                        }

                        Label lblProductSubType = (Label)item.FindControl("lblProductSubType");
                        ptl.Add(new ProductSubTypeData
                        {
                            ProductSubType = lblProductSubType.Text,
                            ProductSubType_Id = btnRemoveProductSubType.CommandArgument
                        });
                    }
                }

                repProductSubType.DataSource = ptl;
                repProductSubType.DataBind();
            }
        }
        #endregion


        //Done
        private void BindClassiFicationAttributes(Guid Activity_Flavour_Id)
        {
            var result = AccSvc.GetActivityClasificationAttributes(new DC_Activity_ClassificationAttributes_RQ { Activity_Flavour_Id = Activity_Flavour_Id });
            if (result != null)
            {
                if (result.Count() > 0)
                {
                    var SuitableForList = result.Where(w => w.AttributeType == "Product" && w.AttributeSubType == "SuitableFor").Select(s => s.AttributeValue).ToList();
                    chklstSuitableFor.ClearSelection();
                    foreach (var SuitableFor in SuitableForList)
                    {
                        if (!string.IsNullOrWhiteSpace(SuitableFor))
                        {
                            if (chklstSuitableFor.Items.FindByText(SuitableFor) != null)
                            {
                                chklstSuitableFor.Items.FindByText(SuitableFor).Selected = true;
                            }
                        }
                    }

                    var SpecialsForList = result.Where(w => w.AttributeType == "Product" && w.AttributeSubType == "Specials").Select(s => s.AttributeValue).ToList();
                    chklstSpecials.ClearSelection();
                    foreach (var Special in SpecialsForList)
                    {
                        if (!string.IsNullOrWhiteSpace(Special))
                        {
                            if (chklstSpecials.Items.FindByText(Special) != null)
                            {
                                chklstSpecials.Items.FindByText(Special).Selected = true;
                            }
                        }
                    }

                    var PhysicalIntensity = result.Where(w => w.AttributeType == "Product" && w.AttributeSubType == "PhysicalIntensity").Select(s => s.AttributeValue).FirstOrDefault();
                    ddlPhysicalIntensity.ClearSelection();

                    if (!string.IsNullOrWhiteSpace(PhysicalIntensity))
                    {
                        if (ddlPhysicalIntensity.Items.FindByText(PhysicalIntensity) != null)
                        {
                            ddlPhysicalIntensity.Items.FindByText(PhysicalIntensity).Selected = true;
                        }
                    }
                }
            }
        }

        protected void AddOperatingOrNonOperatingDays(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void repOperatingDays_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            List<MDMSVC.DC_Activity_OperatingDays> OpDaysList = new List<DC_Activity_OperatingDays>();
            OpDaysList = CollectAllOperatingDaysInfoOnPage();

            if (e.CommandName == "AddOperatingDays")
            {
                LinkButton btnAddOperatingDays = (LinkButton)e.CommandSource;
                RepeaterItem itemOP = (RepeaterItem)btnAddOperatingDays.Parent;

                CheckBox chkSpecificOperatingDays = (CheckBox)itemOP.FindControl("chkSpecificOperatingDays");
                TextBox txtFrom = (TextBox)itemOP.FindControl("txtFromAdd");
                TextBox txtTo = (TextBox)itemOP.FindControl("txtToAdd");

                DC_Activity_OperatingDays NewOD = new DC_Activity_OperatingDays();
                NewOD.Activity_DaysOfOperation_Id = Guid.NewGuid();
                NewOD.Activity_Flavor_ID = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                NewOD.CreateUser = System.Web.HttpContext.Current.User.Identity.Name;
                NewOD.DaysOfWeek = new List<DC_Activity_DaysOfWeek>().ToArray();
                NewOD.EditUser = System.Web.HttpContext.Current.User.Identity.Name;
                if (!string.IsNullOrWhiteSpace(txtFrom.Text))
                {
                    NewOD.FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(txtTo.Text))
                {
                    NewOD.EndDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                NewOD.IsActive = true;
                NewOD.IsOperatingDays = true;

                OpDaysList.Add(NewOD);
                repOperatingDays.DataSource = OpDaysList;
                repOperatingDays.DataBind();
            }
            else if (e.CommandName == "RemoveOperatingDays")
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, "Locally removed", BootstrapAlertType.Warning);
                Guid DaysOfOperation_Id = Guid.Parse(e.CommandArgument.ToString());
                OpDaysList.Remove(OpDaysList.Where(w => w.Activity_DaysOfOperation_Id == DaysOfOperation_Id).Select(s => s).First());
                repOperatingDays.DataSource = OpDaysList;
                repOperatingDays.DataBind();
            }

            updOp.Update(); updAdding.Update();
        }

        protected void repDaysOfWeek_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            List<MDMSVC.DC_Activity_OperatingDays> OpDaysList = new List<DC_Activity_OperatingDays>();
            OpDaysList = CollectAllOperatingDaysInfoOnPage();

            if (e.CommandName == "AddDaysOfWeek")
            {
                LinkButton btnAddDaysOfWeek = (LinkButton)e.CommandSource;
                RepeaterItem repOpsDay = (RepeaterItem)btnAddDaysOfWeek.Parent.Parent.Parent;
                RepeaterItem itemDOW = (RepeaterItem)btnAddDaysOfWeek.Parent;
                LinkButton btnRemoveOperatingDays = (LinkButton)repOpsDay.FindControl("btnRemoveOperatingDays");

                TextBox txtStartTime = (TextBox)itemDOW.FindControl("txtStartTime");
                TextBox txtEndTime = (TextBox)itemDOW.FindControl("txtEndTime");
                DropDownList ddlSession = (DropDownList)itemDOW.FindControl("ddlSession");
                DropDownList ddlDurationType = (DropDownList)itemDOW.FindControl("ddlDurationType");
                HtmlInputCheckBox chkMon = (HtmlInputCheckBox)itemDOW.FindControl("chkMon");
                HtmlInputCheckBox chkTues = (HtmlInputCheckBox)itemDOW.FindControl("chkTues");
                HtmlInputCheckBox chkWed = (HtmlInputCheckBox)itemDOW.FindControl("chkWed");
                HtmlInputCheckBox chkThurs = (HtmlInputCheckBox)itemDOW.FindControl("chkThurs");
                HtmlInputCheckBox chkFri = (HtmlInputCheckBox)itemDOW.FindControl("chkFri");
                HtmlInputCheckBox chkSat = (HtmlInputCheckBox)itemDOW.FindControl("chkSat");
                HtmlInputCheckBox chkSun = (HtmlInputCheckBox)itemDOW.FindControl("chkSun");

                DropDownList ddlDurationDay = (DropDownList)itemDOW.FindControl("ddlDurationDay");
                DropDownList ddlDurationHour = (DropDownList)itemDOW.FindControl("ddlDurationHour");
                DropDownList ddlDurationMinute = (DropDownList)itemDOW.FindControl("ddlDurationMinute");

                foreach (var od in OpDaysList)
                {
                    if (od.Activity_DaysOfOperation_Id == Guid.Parse(btnRemoveOperatingDays.CommandArgument))
                    {
                        var DaysOfWeek = od.DaysOfWeek.ToList();
                        DaysOfWeek.Add(new DC_Activity_DaysOfWeek
                        {
                            Activity_DaysOfOperation_Id = Guid.Parse(btnRemoveOperatingDays.CommandArgument),
                            Activity_DaysOfWeek_ID = Guid.NewGuid(),
                            Activity_Flavor_ID = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]),
                            IsActive = true,
                            Fri = chkFri.Checked,
                            Mon = chkMon.Checked,
                            Sat = chkSat.Checked,
                            Sun = chkSun.Checked,
                            Thur = chkThurs.Checked,
                            Tues = chkTues.Checked,
                            Wed = chkWed.Checked,
                            Session = ddlSession.SelectedItem.Text,
                            Duration = (ddlDurationDay.SelectedItem.Text + "." + ddlDurationHour.SelectedItem.Text + ":" + ddlDurationMinute.SelectedItem.Text),
                            EndTime = txtEndTime.Text,
                            StartTime = txtStartTime.Text,
                            DurationType = ddlDurationType.SelectedItem.Text
                        });
                        od.DaysOfWeek = DaysOfWeek.ToArray();
                    }
                }

                repOperatingDays.DataSource = OpDaysList;
                repOperatingDays.DataBind();

            }
            else if (e.CommandName == "RemoveDaysOfWeek")
            {
                LinkButton btnRemoveDaysOfWeek = (LinkButton)e.CommandSource;
                RepeaterItem repOpsDay = (RepeaterItem)btnRemoveDaysOfWeek.Parent.Parent.Parent;
                LinkButton btnRemoveOperatingDays = (LinkButton)repOpsDay.FindControl("btnRemoveOperatingDays");

                foreach (var od in OpDaysList)
                {
                    if (od.Activity_DaysOfOperation_Id == Guid.Parse(btnRemoveOperatingDays.CommandArgument))
                    {
                        var DaysOfWeek = od.DaysOfWeek.ToList();
                        DaysOfWeek.Remove(DaysOfWeek.Where(w => w.Activity_DaysOfWeek_ID == Guid.Parse(btnRemoveDaysOfWeek.CommandArgument)).Select(s => s).First());
                        od.DaysOfWeek = DaysOfWeek.ToArray();
                    }
                }

                
                repOperatingDays.DataSource = OpDaysList;
                repOperatingDays.DataBind();
            }
        }

        protected void repDaysOfWeek_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Footer)
            {
                DropDownList ddlSession = (DropDownList)e.Item.FindControl("ddlSession");
                if (ddlSession != null)
                {
                    fillDDLSession(ddlSession);
                    if (e.Item.ItemType != ListItemType.Footer)
                    {
                        HiddenField hdnSession = (HiddenField)e.Item.FindControl("hdnSession");
                        if (hdnSession != null)
                        {
                            if (ddlSession.Items.FindByText(hdnSession.Value) != null)
                            {
                                ddlSession.ClearSelection();
                                ddlSession.Items.FindByText(hdnSession.Value).Selected = true;
                            }
                        }

                    }
                }

                DropDownList ddlDurationType = (DropDownList)e.Item.FindControl("ddlDurationType");
                if (ddlDurationType != null)
                {
                    fillActivityDuration(ddlDurationType);
                    if (e.Item.ItemType != ListItemType.Footer)
                    {
                        HiddenField hdnDurationType = (HiddenField)e.Item.FindControl("hdnDurationType");
                        if (hdnDurationType != null)
                        {
                            if (ddlDurationType.Items.FindByText(hdnDurationType.Value) != null)
                            {
                                ddlDurationType.ClearSelection();
                                ddlDurationType.Items.FindByText(hdnDurationType.Value).Selected = true;
                            }
                        }

                    }
                }

                if (e.Item.ItemType != ListItemType.Footer)
                {
                    HiddenField hdnDuration = (HiddenField)e.Item.FindControl("hdnDuration");
                    if (hdnDuration.Value != null)
                    {
                        DropDownList ddlDurationDay = (DropDownList)e.Item.FindControl("ddlDurationDay");
                        DropDownList ddlDurationHour = (DropDownList)e.Item.FindControl("ddlDurationHour");
                        DropDownList ddlDurationMinute = (DropDownList)e.Item.FindControl("ddlDurationMinute");

                        string Day = string.Empty, Hour = string.Empty, Minute = string.Empty;
                        string[] dur = hdnDuration.Value.Split('.');
                        if (dur.Length == 2)
                        {
                            Day = dur[0];

                            string[] durTime = dur[1].Split(':');
                            if (dur.Length == 2)
                            {
                                Hour = durTime[0];
                                Minute = durTime[1];
                            }
                        }
                        else if (dur.Length == 1)
                        {
                            if (hdnDuration.Value.Length == 5)
                            {
                                hdnDuration.Value = "0" + hdnDuration.Value;
                                Day = hdnDuration.Value.Substring(0, 2);
                                Hour = hdnDuration.Value.Substring(2, 2);
                                Minute = hdnDuration.Value.Substring(4, 2);
                            }
                        }

                        if (Day.Length == 1)
                        {
                            Day = "0" + Day;
                        }

                        if (ddlDurationDay.Items.FindByText(Day) != null)
                        {
                            ddlDurationDay.ClearSelection();
                            ddlDurationDay.Items.FindByText(Day).Selected = true;
                        }

                        if (ddlDurationHour.Items.FindByText(Hour) != null)
                        {
                            ddlDurationHour.ClearSelection();
                            ddlDurationHour.Items.FindByText(Hour).Selected = true;
                        }

                        if (ddlDurationMinute.Items.FindByText(Minute) != null)
                        {
                            ddlDurationMinute.ClearSelection();
                            ddlDurationMinute.Items.FindByText(Minute).Selected = true;
                        }
                    }
                }

            }
        }

        private List<MDMSVC.DC_Activity_OperatingDays> CollectAllOperatingDaysInfoOnPage()
        {
            List<MDMSVC.DC_Activity_OperatingDays> OpDaysList = new List<DC_Activity_OperatingDays>();

            foreach (RepeaterItem item in repOperatingDays.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {


                    MDMSVC.DC_Activity_OperatingDays OpDay = new DC_Activity_OperatingDays();
                    CheckBox chkToDeleteOperation = (CheckBox)item.FindControl("chkToDeleteOperation");
                    //Check Checkbox to delete operation
                    if (chkToDeleteOperation != null && !chkToDeleteOperation.Checked)
                    {

                        CheckBox chkSpecificOperatingDays = (CheckBox)item.FindControl("chkSpecificOperatingDays");

                        TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                        TextBox txtTo = (TextBox)item.FindControl("txtTo");
                        Repeater repDaysOfWeek = (Repeater)item.FindControl("repDaysOfWeek");
                        LinkButton btnRemoveOperatingDays = (LinkButton)item.FindControl("btnRemoveOperatingDays");

                        OpDay.Activity_DaysOfOperation_Id = Guid.Parse(btnRemoveOperatingDays.CommandArgument);
                        OpDay.Activity_Flavor_ID = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                        OpDay.IsOperatingDays = true;
                        if (!string.IsNullOrWhiteSpace(txtFrom.Text))
                        {
                            OpDay.FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        if (!string.IsNullOrWhiteSpace(txtTo.Text))
                        {
                            OpDay.EndDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        OpDay.EditUser = System.Web.HttpContext.Current.User.Identity.Name;
                        OpDay.IsActive = true;

                        List<MDMSVC.DC_Activity_DaysOfWeek> WeekDayList = new List<MDMSVC.DC_Activity_DaysOfWeek>();

                        foreach (RepeaterItem itemDOW in repDaysOfWeek.Items)
                        {
                            if (itemDOW.ItemType == ListItemType.Item || itemDOW.ItemType == ListItemType.AlternatingItem)
                            {
                                CheckBox chkToDeleteDays = (CheckBox)itemDOW.FindControl("chkToDeleteDays");
                                //Check Checkbox to delete operation
                                if (chkToDeleteDays != null && !chkToDeleteDays.Checked)
                                {
                                    TextBox txtStartTime = (TextBox)itemDOW.FindControl("txtStartTime");
                                    TextBox txtEndTime = (TextBox)itemDOW.FindControl("txtEndTime");
                                    HiddenField hdnDuration = (HiddenField)itemDOW.FindControl("hdnDuration");
                                    DropDownList ddlSession = (DropDownList)itemDOW.FindControl("ddlSession");
                                    DropDownList ddlDurationType = (DropDownList)itemDOW.FindControl("ddlDurationType");
                                    HtmlInputCheckBox chkMon = (HtmlInputCheckBox)itemDOW.FindControl("chkMon");
                                    HtmlInputCheckBox chkTues = (HtmlInputCheckBox)itemDOW.FindControl("chkTues");
                                    HtmlInputCheckBox chkWed = (HtmlInputCheckBox)itemDOW.FindControl("chkWed");
                                    HtmlInputCheckBox chkThurs = (HtmlInputCheckBox)itemDOW.FindControl("chkThurs");
                                    HtmlInputCheckBox chkFri = (HtmlInputCheckBox)itemDOW.FindControl("chkFri");
                                    HtmlInputCheckBox chkSat = (HtmlInputCheckBox)itemDOW.FindControl("chkSat");
                                    HtmlInputCheckBox chkSun = (HtmlInputCheckBox)itemDOW.FindControl("chkSun");
                                    LinkButton btnRemoveDaysOfWeek = (LinkButton)itemDOW.FindControl("btnRemoveDaysOfWeek");

                                    Label lblSupplierStartTime = (Label)itemDOW.FindControl("lblSupplierStartTime");
                                    Label lblSupplierDuration = (Label)itemDOW.FindControl("lblSupplierDuration");
                                    Label lblSupplierSession = (Label)itemDOW.FindControl("lblSupplierSession");
                                    Label lblSupplierFrequency = (Label)itemDOW.FindControl("lblSupplierFrequency");

                                    DropDownList ddlDurationDay = (DropDownList)itemDOW.FindControl("ddlDurationDay");
                                    DropDownList ddlDurationHour = (DropDownList)itemDOW.FindControl("ddlDurationHour");
                                    DropDownList ddlDurationMinute = (DropDownList)itemDOW.FindControl("ddlDurationMinute");

                                    WeekDayList.Add(new DC_Activity_DaysOfWeek
                                    {
                                        Activity_DaysOfOperation_Id = OpDay.Activity_DaysOfOperation_Id,
                                        IsActive = true,
                                        Activity_Flavor_ID = OpDay.Activity_Flavor_ID,
                                        Activity_DaysOfWeek_ID = Guid.Parse(btnRemoveDaysOfWeek.CommandArgument),
                                        //  Duration = (hdnDuration.Value == string.Empty ? (ddlDurationDay.SelectedItem.Text + "." + ddlDurationHour.SelectedItem.Text + ":" + ddlDurationMinute.SelectedItem.Text) : hdnDuration.Value),
                                        Duration = (ddlDurationDay.SelectedItem.Text + "." + ddlDurationHour.SelectedItem.Text + ":" + ddlDurationMinute.SelectedItem.Text),

                                        Fri = chkFri.Checked,
                                        Mon = chkMon.Checked,
                                        Sat = chkSat.Checked,
                                        Session = (ddlSession.SelectedIndex == 0 ? string.Empty : ddlSession.SelectedItem.Text),
                                        Sun = chkSun.Checked,
                                        StartTime = txtStartTime.Text,
                                        EndTime = txtEndTime.Text,
                                        Thur = chkThurs.Checked,
                                        Tues = chkTues.Checked,
                                        Wed = chkWed.Checked,
                                        SupplierDuration = lblSupplierDuration.Text,
                                        SupplierFrequency = lblSupplierFrequency.Text,
                                        SupplierSession = lblSupplierSession.Text,
                                        SupplierStartTime = lblSupplierStartTime.Text,
                                        DurationType = (ddlDurationType.SelectedIndex == 0 ? string.Empty : ddlDurationType.SelectedItem.Text)
                                    });
                                }
                            }
                        }

                        OpDay.DaysOfWeek = WeekDayList.ToArray();

                        OpDaysList.Add(OpDay);
                    }
                }
            }

            return OpDaysList;
        }

        //ToDO
        protected void repOperatingDays_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    HtmlButton iCalFrom = (HtmlButton)e.Item.FindControl("iCalFrom");
            //    TextBox txtFrom = (TextBox)e.Item.FindControl("txtFrom");
            //    CalendarExtender calFromDate = (CalendarExtender)e.Item.FindControl("calFromDate");
            //    FilteredTextBoxExtender axfte_txtFrom = (FilteredTextBoxExtender)e.Item.FindControl("axfte_txtFrom");
            //    calFromDate.TargetControlID = txtFrom.ClientID;
            //    calFromDate.PopupButtonID = iCalFrom.ClientID;
            //    axfte_txtFrom.TargetControlID = txtFrom.ClientID;

            //    HtmlButton iCalTo = (HtmlButton)e.Item.FindControl("iCalTo");
            //    TextBox txtTo = (TextBox)e.Item.FindControl("txtTo");
            //    CalendarExtender calToDate = (CalendarExtender)e.Item.FindControl("calToDate");
            //    FilteredTextBoxExtender axfte_txtTo = (FilteredTextBoxExtender)e.Item.FindControl("axfte_txtTo");
            //    calToDate.TargetControlID = txtFrom.ClientID;
            //    calToDate.PopupButtonID = iCalFrom.ClientID;
            //    axfte_txtTo.TargetControlID = txtFrom.ClientID;
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            UpdateFlavour(Activity_Flavour_Id);

        }

        protected void btnChangeActivityStatus_Click(object sender, EventArgs e)
        {

        }

        protected void btnRemoveSelectedOperationDays_Click(object sender, EventArgs e)
        {
            repOperatingDays.DataSource = CollectAllOperatingDaysInfoOnPage();
            repOperatingDays.DataBind();
        }


        protected void NonOperatingDays()
        {
            string FromDate = txtFromAdd.Text;
            string EndDate = txtToAdd.Text;

            if (string.IsNullOrWhiteSpace(FromDate))
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please Select From Date", BootstrapAlertType.Danger);
                txtFromAdd.Focus();
            }
            else if (string.IsNullOrWhiteSpace(EndDate))
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Please Select To Date", BootstrapAlertType.Danger);
                txtToAdd.Focus();
            }
            else if (DateDifference(FromDate, EndDate))
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "To date should be less than From date", BootstrapAlertType.Danger);
            }
            else
            {
                dvMsg.Style.Add("display", "none");
                var nonOperatingDays = new DC_Activity_OperatingDays();
                Guid ActivityDaysOfOperationId = Guid.NewGuid();

                nonOperatingDays.Activity_DaysOfOperation_Id = ActivityDaysOfOperationId;
                nonOperatingDays.Activity_Flavor_ID = new Guid(Request.QueryString["Activity_Flavour_Id"]); ;
                nonOperatingDays.FromDate = Convert.ToDateTime(FromDate);
                nonOperatingDays.EndDate = Convert.ToDateTime(EndDate);
                nonOperatingDays.IsOperatingDays = false;
                nonOperatingDays.IsActive = false;
                nonOperatingDays.CreateUser = System.Web.HttpContext.Current.User.Identity.Name;
                nonOperatingDays.EditUser = System.Web.HttpContext.Current.User.Identity.Name;
                List<DC_Activity_OperatingDays> nonOperatingDaysList = new List<DC_Activity_OperatingDays>();

                nonOperatingDaysList.Add(nonOperatingDays);
                if (nonOperatingDays.Activity_Flavor_ID != Guid.Empty)
                {
                    var result = AccSvc.AddUpdateActivityNonOperatingDays(nonOperatingDaysList);
                    BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, result.StatusMessage, BootstrapAlertType.Success);
                    getNonOperatingDays(gvNonOperatingData.PageSize, gvNonOperatingData.PageIndex);

                }
            }
        }

        protected void btnAddOperatingDays_Click(object sender, EventArgs e)
        {
            if (chkSpecificOperatingDays.Checked) { OperatingDays(); updOp.Update(); }
            else
            {
                NonOperatingDays(); updNonOp.Update();
            }

            updAdding.Update();
        }

        protected void OperatingDays()
        {
            List<MDMSVC.DC_Activity_OperatingDays> OpDaysList = new List<DC_Activity_OperatingDays>();
            OpDaysList = CollectAllOperatingDaysInfoOnPage();

            //RepeaterItem itemOP = (RepeaterItem)btnAddOperatingDays.Parent;
            //CheckBox chkSpecificOperatingDays = (CheckBox)itemOP.FindControl("chkSpecificOperatingDays");
            //TextBox txtFrom = (TextBox)itemOP.FindControl("txtFromAdd");
            //TextBox txtTo = (TextBox)itemOP.FindControl("txtToAdd");

            DC_Activity_OperatingDays NewOD = new DC_Activity_OperatingDays();
            NewOD.Activity_DaysOfOperation_Id = Guid.NewGuid();
            NewOD.Activity_Flavor_ID = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
            NewOD.CreateUser = System.Web.HttpContext.Current.User.Identity.Name;
            NewOD.DaysOfWeek = new List<DC_Activity_DaysOfWeek>().ToArray();
            NewOD.EditUser = System.Web.HttpContext.Current.User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(txtFromAdd.Text))
            {
                NewOD.FromDate = DateTime.ParseExact(txtFromAdd.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(txtToAdd.Text))
            {
                NewOD.EndDate = DateTime.ParseExact(txtToAdd.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            NewOD.IsActive = true;
            NewOD.IsOperatingDays = true;

            OpDaysList.Add(NewOD);
            repOperatingDays.DataSource = OpDaysList;
            repOperatingDays.DataBind();

            BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, "Success! Data added successfully", BootstrapAlertType.Success);
        }

        public void getNonOperatingDays(int pagesize, int pageno)
        {
            try
            {
                Guid? Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                if (Activity_Flavour_Id != Guid.Empty)
                {
                    Guid guidActivity_Flavour_Id = Activity_Flavour_Id ?? Guid.Empty;
                    var result = AccSvc.GetActivityNonOperatingDays(guidActivity_Flavour_Id, pagesize, pageno);

                    if (result != null)
                    {
                        gvNonOperatingData.DataSource = result;
                        if (result.Count() > 0)
                        {
                            gvNonOperatingData.VirtualItemCount = result[0].TotalRecords ?? 0;
                            gvNonOperatingData.PageIndex = pageno;
                            gvNonOperatingData.PageSize = pagesize;
                        }
                        gvNonOperatingData.DataBind();

                    }
                    else
                    {
                        gvNonOperatingData.DataSource = null;
                        gvNonOperatingData.DataBind();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool DateDifference(string FromDate, string EndDate)
        {
            DateTime fromdate = Convert.ToDateTime(FromDate);
            DateTime todate = Convert.ToDateTime(EndDate);

            if (fromdate <= todate) { return false; } else { return true; }
        }

        protected void deleteNonOperatingDate_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);

            //string RemoveNonOperatingDays = btn.CommandArgument;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string pk = gvNonOperatingData.DataKeys[row.RowIndex].Values[0].ToString();
            gvNonOperatingData.PageSize = Convert.ToInt16(ddlShowEntries.SelectedValue);
            Guid ActivityDaysOfOperationId = Guid.Parse(pk);

            if (ActivityDaysOfOperationId != Guid.Empty)
            {
                var result = AccSvc.DeleteActivityNonOperatingDays(ActivityDaysOfOperationId);
                BootstrapAlert.BootstrapAlertMessage(dvMsgAlert, result.StatusMessage, BootstrapAlertType.Success);
                getNonOperatingDays(gvNonOperatingData.PageSize, gvNonOperatingData.PageIndex);
            }

            HtmlControl ctrl = (HtmlControl)this.FindControl("h1");

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "toggleChevron(" + ctrl + ");", true);
            updAdding.Update();
            //ctrl.Attributes.Add("onclick", "javascript:return toggleChevron(" + ctrl + ");");

        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            getNonOperatingDays(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), gvNonOperatingData.PageIndex);
        }

        protected void gvNonOperatingData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            dvMsgAlert.Style.Add("display", "none");
            getNonOperatingDays(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }
    }

    public class ProductTypeData
    {
        public string ProductType_Id { get; set; }
        public string ProductType { get; set; }
    }

    public class ProductSubTypeData
    {
        public string ProductSubType_Id { get; set; }
        public string ProductSubType { get; set; }
    }

    public class SubCategoryData
    {
        public string SubCategory_Id { get; set; }
        public string SubCategory { get; set; }
    }
    public class InterestTypeData
    {
        public string InterestType_Id { get; set; }
        public string InterestType { get; set; }
    }



}
