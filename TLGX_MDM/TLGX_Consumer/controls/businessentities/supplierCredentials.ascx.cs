using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierCredentials : System.Web.UI.UserControl
    {
        #region Variables
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        public static Guid mySupplier_Id = Guid.Empty;

        #endregion
        #region Page Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
                LoadSupplierDetails();
            }
        }
        #endregion
        #region Page Detais Fetching
        private void LoadSupplierDetails()
        {
            try
            {
                var result = _objMaster.GetSupplier(new MDMSVC.DC_Supplier_Search_RQ() { Supplier_Id = mySupplier_Id });
                if (result != null && result.Count > 0)
                {
                    //Fill all dropdown before setting the details
                    //FillSupplierType();
                    FillSupplierEnablerCatgeroyType();
                    FillCredentialsType();
                    FillCredentialsCategoryType();
                    FillClientType();

                }
            }
            catch (Exception)
            {


                throw;
            }
        }
        private void FillSupplierType(DropDownList ddlSupplierTypeForBind)
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "SupplierType" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlSupplierTypeForBind.Items.Clear();
                    ddlSupplierTypeForBind.DataSource = result;
                    ddlSupplierTypeForBind.DataTextField = "AttributeValue";
                    ddlSupplierTypeForBind.DataValueField = "MasterAttributeValue_Id";
                    ddlSupplierTypeForBind.DataBind();
                    ddlSupplierTypeForBind.Items.Insert(0, new ListItem { Text = "--ALL--", Value = "0" });
                }
        }
        private void FillClientType()
        {

        }

        private void FillCredentialsCategoryType()
        {
        }

        private void FillCredentialsType()
        {
        }

        

        private void FillSupplierEnablerCatgeroyType()
        {
        }
        #endregion
    }
}