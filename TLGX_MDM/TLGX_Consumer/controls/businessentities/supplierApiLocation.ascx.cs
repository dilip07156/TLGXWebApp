using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class supplierApiLocation : System.Web.UI.UserControl
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
            }
        }
        #endregion

        protected void frmSupplierApiLoc_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }
    }
}