using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.staticdata
{
    public partial class allSupplierDataChart : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMaster = new MasterDataSVCs();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindProductCategory(ddlProductCategory);
                fillsuppliers("0");
            }
        }
        private void BindProductCategory(DropDownList ddlProductCategoryBind)
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "ProductCategory" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlProductCategoryBind.Items.Clear();
                    ddlProductCategoryBind.DataSource = result;
                    ddlProductCategoryBind.DataTextField = "AttributeValue";
                    ddlProductCategoryBind.DataValueField = "AttributeValue";
                    ddlProductCategoryBind.DataBind();
                    ddlProductCategoryBind.Items.Insert(0, new ListItem { Text = "--All Category--",Value="0" });
                }
        }

        private void fillsuppliers(string productCategory)
        {
            ddlPriority.Items.Clear();
            var res = _objMaster.GetSuppliersByProductCategory(productCategory);
            ddlPriority.DataSource = (from r in res orderby r.Priority select new { Priority = r.Priority }).Distinct().ToList();
            ddlPriority.DataValueField = "Priority";
            ddlPriority.DataTextField = "Priority";
            ddlPriority.DataBind();
            ddlPriority.Items.Remove(ddlPriority.Items.FindByValue("0"));
            ddlPriority.Items.Insert(0, new ListItem { Text = "--All Priority--", Value = "0" });
        }

        protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillsuppliers(ddlProductCategory.SelectedValue);
        }
    }
}