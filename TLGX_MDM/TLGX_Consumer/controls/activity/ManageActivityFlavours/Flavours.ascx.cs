using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();

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
                foreach(DC_Activity_SupplierProductMapping res in result)
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
                ddlProdNameSubType.DataSource = res;
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
                ddlProdNameSubType.DataSource = dropdownvalues;
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
            //else if (e.CommandName == "FillListBox2")
            //{
            //    ListBox lstboxProductType = (ListBox)frmActivityFlavour.FindControl("lstboxProductType");
            //    ListBox lstboxSelectedProductType = (ListBox)frmActivityFlavour.FindControl("lstboxSelectedProductType");
            //    Literal lblmsg2 = (Literal)frmActivityFlavour.FindControl("lblmsg2");

            //    lblmsg2.Visible = false;
            //    if (lstboxProductType.SelectedIndex >= 0)
            //    {
            //        for (int i = 0; i < lstboxProductType.Items.Count; i++)
            //        {
            //            if (lstboxProductType.Items[i].Selected)
            //            {
            //                if (!arraylist1.Contains(lstboxProductType.Items[i]))
            //                {
            //                    arraylist1.Add(lstboxProductType.Items[i]);
            //                }
            //            }
            //        }
            //        for (int i = 0; i < arraylist1.Count; i++)
            //        {
            //            if (!lstboxSelectedProductType.Items.Contains(((ListItem)arraylist1[i])))
            //            {
            //                lstboxSelectedProductType.Items.Add(((ListItem)arraylist1[i]));
            //            }
            //            lstboxProductType.Items.Remove(((ListItem)arraylist1[i]));
            //        }
            //        lstboxProductType.SelectedIndex = -1;
            //    }
            //    else
            //    {
            //        lblmsg2.Visible = true;
            //        lblmsg2.Text = "Please select atleast Product Type to move";
            //    }

            //}
            //else if (e.CommandName == "EmptyListBox2")
            //{
            //    ListBox lstboxProductType = (ListBox)frmActivityFlavour.FindControl("lstboxProductType");
            //    ListBox lstboxSelectedProductType = (ListBox)frmActivityFlavour.FindControl("lstboxSelectedProductType");
            //    Literal lblmsg2 = (Literal)frmActivityFlavour.FindControl("lblmsg2");

            //    lblmsg2.Visible = false;
            //    if (lstboxSelectedProductType.SelectedIndex >= 0)
            //    {
            //        for (int i = 0; i < lstboxSelectedProductType.Items.Count; i++)
            //        {
            //            if (lstboxSelectedProductType.Items[i].Selected)
            //            {
            //                if (!arraylist2.Contains(lstboxSelectedProductType.Items[i]))
            //                {
            //                    arraylist2.Add(lstboxSelectedProductType.Items[i]);
            //                }
            //            }
            //        }
            //        for (int i = 0; i < arraylist2.Count; i++)
            //        {
            //            if (!lstboxProductType.Items.Contains(((ListItem)arraylist2[i])))
            //            {
            //                lstboxProductType.Items.Add(((ListItem)arraylist2[i]));
            //            }
            //            lstboxSelectedProductType.Items.Remove(((ListItem)arraylist2[i]));
            //        }
            //        lstboxProductType.SelectedIndex = -1;
            //    }
            //    else
            //    {
            //        lblmsg2.Visible = true;
            //        lblmsg2.Text = "Please select atleast Product Type remove";
            //    }
            //}
        }

    }
}