using System;
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
            RQ.Activity_Flavour_Id= Activity_Flavour_Id;
            var result = AccSvc.GetActivityFlavour(RQ);
            frmActivityFlavour.DataSource = result;
            frmActivityFlavour.DataBind();
            if (result != null)
            {
                fillMasterDropdowns();
            }
        }

        private void fillMasterDropdowns()
        {
            fillcoutries();
            fillcities();
            fillproductcaterogysubtype();
            fillProductType();
            fillproductsubtype();
        }

        private void fillcoutries()
        {
            DropDownList ddlCountry = (DropDownList)frmActivityFlavour.FindControl("ddlCountry");
            var a = masterSVc.GetAllCountries();
            ddlCountry.DataSource = a;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
            if (rowView != null)
            {
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
                    ddlProdcategorySubType.SelectedIndex = ddlProdcategorySubType.Items.IndexOf(ddlProdcategorySubType.Items.FindByText(rowView.ProductCategorySubType.ToString()));
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
            var dropdownvalues= LookupAtrributes.GetAllAttributeAndValuesByFOR("Activity", "ActivityProductType").MasterAttributeValues;
            if (ddlProdcategorySubType.SelectedItem.Text != "-Select-")
            {
                var res = (from s in dropdownvalues
                           where s.ParentAttributeValue_Id == new Guid(ddlProdcategorySubType.SelectedValue)
                          select s);
                ddlProductType.DataSource = res;
                ddlProductType.DataTextField = "AttributeValue";
                ddlProductType.DataValueField = "MasterAttributeValue_Id";
                ddlProductType.DataBind();
                ddlProductType.Items.Insert(0, new ListItem("-Select-", "0"));
                MDMSVC.DC_Activity_Flavour rowView = (MDMSVC.DC_Activity_Flavour)frmActivityFlavour.DataItem;
                if (rowView != null)
                {
                    ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText(rowView.ProductType.ToString()));
                }
            }
            //else {
            //    ddlProductType.DataSource = dropdownvalues;
            //    ddlProductType.DataTextField = "AttributeValue";
            //    ddlProductType.DataValueField = "MasterAttributeValue_Id";
            //    ddlProductType.DataBind();
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
                    ddlProdNameSubType.SelectedIndex = ddlProdNameSubType.Items.IndexOf(ddlProdNameSubType.Items.FindByText(rowView.ProductNameSubType.ToString()));
                }
            }
            //else
            //{
            //    ddlProdNameSubType.DataSource = dropdownvalues;
            //    ddlProdNameSubType.DataTextField = "AttributeValue";
            //    ddlProdNameSubType.DataValueField = "MasterAttributeValue_Id";
            //    ddlProdNameSubType.DataBind();
            //}
           
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
                TextBox txtStreet = (TextBox)frmActivityFlavour.FindControl("txtStreet");
                TextBox txtStreet2 = (TextBox)frmActivityFlavour.FindControl("txtStreet2");
                TextBox txtStreet3 = (TextBox)frmActivityFlavour.FindControl("txtStreet3");
                TextBox txtStreet4 = (TextBox)frmActivityFlavour.FindControl("txtStreet4");
                TextBox txtStreet5 = (TextBox)frmActivityFlavour.FindControl("txtStreet5");
                DropDownList ddlCountry = (DropDownList)frmActivityFlavour.FindControl("ddlCountry");
                DropDownList ddlCity = (DropDownList)frmActivityFlavour.FindControl("ddlCity");
                DropDownList ddlArea = (DropDownList)frmActivityFlavour.FindControl("ddlArea");
                DropDownList ddlLocation = (DropDownList)frmActivityFlavour.FindControl("ddlLocation"); 
                TextBox txtPostalCode = (TextBox)frmActivityFlavour.FindControl("txtPostalCode");
                //Classification Attributes
                DropDownList ddlProdCategory = (DropDownList)frmActivityFlavour.FindControl("ddlProdCategory");
                DropDownList ddlProdcategorySubType = (DropDownList)frmActivityFlavour.FindControl("ddlProdcategorySubType");
                DropDownList ddlProductType = (DropDownList)frmActivityFlavour.FindControl("ddlArea");
                DropDownList ddlProdNameSubType = (DropDownList)frmActivityFlavour.FindControl("ddlLocation");
                //key facts
                CheckBox blnCompanyReccom = (CheckBox)frmActivityFlavour.FindControl("blnCompanyReccom");
                CheckBox blnMustSeeInCountry = (CheckBox)frmActivityFlavour.FindControl("blnMustSeeInCountry");
                //General info
                TextBox txtEventPlace = (TextBox)frmActivityFlavour.FindControl("txtEventPlace");
                TextBox txtStartingPoint = (TextBox)frmActivityFlavour.FindControl("txtStartingPoint");
                TextBox txtEndingPoint = (TextBox)frmActivityFlavour.FindControl("txtEndingPoint");
                TextBox txtDuration = (TextBox)frmActivityFlavour.FindControl("txtDuration");

                MDMSVC.DC_Activity_Flavour FlavData = new MDMSVC.DC_Activity_Flavour();

                FlavData.Activity_Flavour_Id = new Guid(txtActivity_Flavour_Id.Text);
                FlavData.Legacy_Product_ID = AccSvc.GetLegacyProductId(Activity_Flavour_Id);
                if (ddlCountry.SelectedIndex != 0)
                {
                    FlavData.Country = ddlCountry.SelectedItem.Text;
                    FlavData.Country = ddlCountry.SelectedItem.Value;
                }   
                if (ddlCity.SelectedIndex != 0)
                {
                    FlavData.City = ddlCity.SelectedItem.Text;
                    FlavData.City = ddlCity.SelectedItem.Value;
                }
                //if (ddlArea.SelectedIndex != 0)
                //{
                //    FlavData.Area = ddlArea.SelectedItem.Text;
                //    FlavData.Area = ddlArea.SelectedItem.Value;
                //}
                //if (ddlLocation.SelectedIndex != 0)
                //{
                //    FlavData.Location = ddlLocation.SelectedItem.Text;
                //    FlavData.Location = ddlLocation.SelectedItem.Value;
                //}
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

                if (!string.IsNullOrEmpty(txtStreet.Text))
                    FlavData.Street = txtStreet.Text.ToString();

                if (!string.IsNullOrEmpty(txtStreet2.Text))
                    FlavData.Street2 = txtStreet2.Text.ToString();

                if (!string.IsNullOrEmpty(txtStreet3.Text))
                    FlavData.Street3 = txtStreet3.Text.ToString();

                if (!string.IsNullOrEmpty(txtStreet4.Text))
                    FlavData.Street4 = txtStreet4.Text.ToString();

                if (!string.IsNullOrEmpty(txtStreet5.Text))
                    FlavData.Street5 = txtStreet5.Text.ToString();

                if (!string.IsNullOrEmpty(txtPostalCode.Text))
                    FlavData.PostalCode = txtPostalCode.Text.ToString();

                if (!string.IsNullOrEmpty(txtEventPlace.Text))
                    FlavData.PlaceOfEvent = txtEventPlace.Text.ToString();

                if (!string.IsNullOrEmpty(txtStartingPoint.Text))
                    FlavData.StartingPoint = txtStartingPoint.Text.ToString();

                if (!string.IsNullOrEmpty(txtEndingPoint.Text))
                    FlavData.EndingPoint = txtEndingPoint.Text.ToString();

                if (!string.IsNullOrEmpty(txtDuration.Text))
                    FlavData.Duration = txtDuration.Text.ToString();
                
                FlavData.CompanyReccom = blnCompanyReccom.Checked;
                FlavData.MustSeeInCountry = blnMustSeeInCountry.Checked;
                FlavData.Edit_Date= DateTime.Now;
                FlavData.Edit_User=System.Web.HttpContext.Current.User.Identity.Name;
                var result = AccSvc.AddUpdateActivityFlavour(FlavData);
                BootstrapAlert.BootstrapAlertMessage(dvMsg,result.StatusMessage, (BootstrapAlertType)result.StatusCode);

            }
            else if (e.CommandName == "CancelProduct")
            {
                TextBox txtProductName = (TextBox)this.Parent.Page.FindControl("txtProductName");
                DropDownList ddlCountry = (DropDownList)this.Parent.Page.FindControl("ddlCountry");
            }
        }
    }
}