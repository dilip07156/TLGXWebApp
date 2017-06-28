using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.geography
{
    public partial class supplierCountryMapping : System.Web.UI.UserControl
    {
        // all public so they can be used from outside the control
        public MasterDataDAL.SupplierDataMode SupplierCountryMappingMode;          // used to set the control mode from outside the page as 
                                                                                // this control is used on both SUPPLIER AND COUNTRY MANAGERS
        public Guid? Supplier_Id;                                               // used to set Supplier Id, nullable for get all
        public Guid Country_Id;                                                 // used to set Country_Id

        MasterDataDAL objMasterDataDAL = new MasterDataDAL();                   // used to talk to dal
        protected DataTable dtSupplierCountryMapping = new DataTable();            // used to store SupplierCountryMapping

        // public so it can be callled from the hosting page
        public void bindSupplierCountryMapping(int pageIndex)
        {
            dtSupplierCountryMapping = objMasterDataDAL.GetSupplierCountryMapping(SupplierCountryMappingMode, Supplier_Id,Country_Id);
            grdCountryMapping.DataSource = dtSupplierCountryMapping;
            grdCountryMapping.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // need an elegant solution to handle no GUids, to reduce db lookup
            if (!IsPostBack)
            {
                bindSupplierCountryMapping(0);              
            }
        }



    } }