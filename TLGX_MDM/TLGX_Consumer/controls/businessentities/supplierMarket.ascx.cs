using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.businessentities
{

    public partial class supplierMarket : System.Web.UI.UserControl
    {
        businessEntityDAL bdal = new businessEntityDAL();
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        public int intPageSize = 10;
        public int intPageNo = 0;

        protected void bindSUpplierMarketsGrid()
        {
            Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
            var result = _objMaster.GetSupplierMarket(new MDMSVC.DC_SupplierMarket() { Supplier_Id = mySupplier_Id, PageSize = intPageSize, PageNo = intPageNo  });
            grdSupplierMarkets.DataSource = result;
            grdSupplierMarkets.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindSUpplierMarketsGrid();
            }
        }

        protected void frmSupplierMarket_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            Guid mySupplier_Id = Guid.Parse(Request.QueryString["Supplier_Id"]);
            TextBox txtSupplierMarketName = (TextBox)frmSupplierMarket.FindControl("txtSupplierMarketName");
            TextBox txtSupplierMarketCode = (TextBox)frmSupplierMarket.FindControl("txtSupplierMarketCode");
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();



            if (e.CommandName.ToString() == "Add")
            {
                MDMSVC.DC_SupplierMarket newObj = new MDMSVC.DC_SupplierMarket
                {
                    Supplier_Id = mySupplier_Id,
                    IsActive = true,
                    Name = txtSupplierMarketName.Text.Trim(),
                    Code = txtSupplierMarketCode.Text.Trim(),
                    Status = "ACT",
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                _msg = _objMaster.AddUpdateSupplierMarket(newObj);
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierMarket.ChangeMode(FormViewMode.Insert);
                    frmSupplierMarket.DataBind();
                    bindSUpplierMarketsGrid();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }


            if (e.CommandName.ToString() == "Modify")
            {

                Guid myRow_Id = Guid.Parse(grdSupplierMarkets.SelectedDataKey.Value.ToString());

                MDMSVC.DC_SupplierMarket newObj = new MDMSVC.DC_SupplierMarket
                {
                    Supplier_Market_Id = myRow_Id,
                    Supplier_Id = mySupplier_Id,
                    IsActive = true,
                    Name = txtSupplierMarketName.Text.Trim(),
                    Code = txtSupplierMarketCode.Text.Trim(),
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                _msg = _objMaster.AddUpdateSupplierMarket(newObj);
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierMarket.ChangeMode(FormViewMode.Insert);
                    frmSupplierMarket.DataBind();
                    bindSUpplierMarketsGrid();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }

        protected void grdSupplierMarkets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            MDMSVC.DC_Message _msg = new MDMSVC.DC_Message();
            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                frmSupplierMarket.ChangeMode(FormViewMode.Edit);
                var result = _objMaster.GetSupplierMarket(new MDMSVC.DC_SupplierMarket() { Supplier_Market_Id = myRow_Id });
                frmSupplierMarket.DataSource = result;
                frmSupplierMarket.DataBind();
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                _msg = _objMaster.SupplierMarketSoftDelete(new MDMSVC.DC_SupplierMarket()
                {
                    Supplier_Market_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierMarket.ChangeMode(FormViewMode.Insert);
                    frmSupplierMarket.DataBind();
                    bindSUpplierMarketsGrid();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Market has been deleted successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                _msg = _objMaster.SupplierMarketSoftDelete(new MDMSVC.DC_SupplierMarket()
                {
                    Supplier_Market_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                });
                if (Convert.ToInt32(_msg.StatusCode) == Convert.ToInt32(BootstrapAlertType.Success))
                {
                    frmSupplierMarket.ChangeMode(FormViewMode.Insert);
                    frmSupplierMarket.DataBind();
                    bindSUpplierMarketsGrid();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Market has been retrived successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _msg.StatusMessage, (BootstrapAlertType)_msg.StatusCode);
                }
            }
        }
        protected void grdSupplierMarkets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }
    }
}