using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata
{
    public partial class velocityReport : System.Web.UI.Page
    {
        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();

        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillsuppliers();
            }
        }
        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }
    }
}