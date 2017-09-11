using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.staticdataconfig
{
    public partial class manageAPILocation : System.Web.UI.UserControl
    {

        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();
        MappingSVCs _objMappingSVCs = new MappingSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillSuppliers();
                fillEntity();
                fillStatus();
            }
        }
        protected void fillSuppliers()
        {
            MDMSVC.DC_Supplier_Search_RQ RQParam = new MDMSVC.DC_Supplier_Search_RQ();
            RQParam.SupplierType = "API Static Data";
            RQParam.PageNo = 0;
            RQParam.PageSize = int.MaxValue;
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplier(RQParam);
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }
        protected void fillEntity()
        {
            fillattributes("MappingFileConfig", "MappingEntity", ddlMasterCountry);
        }
        protected void fillStatus()
        {
            ddlStatus.DataSource = _objMasterSVC.Pentaho_SupplierApiCall_Status();
            ddlStatus.DataBind();
        }
        public void fillattributes(string masterfor, string attributename, DropDownList ddl)
        {
            ddl.Items.Clear();
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = masterfor;
            RQ.Name = attributename;
            var resvalues = mastersvc.GetAllAttributeAndValues(RQ);
            ddl.DataSource = resvalues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--ALL--", "0"));
            RQ = null;
            resvalues = null;
        }
        private void fillmatchingdata(int PageSize, int PageNo)
        {
            MDMSVC.DC_PentahoApiCallLogDetails_RQ RQParam = new MDMSVC.DC_PentahoApiCallLogDetails_RQ();

            if (ddlSupplierName.SelectedItem.Value != "0")
                RQParam.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value);
            if (ddlMasterCountry.SelectedItem.Value != "0")
                RQParam.Entity_Id = Guid.Parse(ddlMasterCountry.SelectedValue);
            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlStatus.SelectedItem.Text;

            var res = _objMappingSVCs.Pentaho_SupplierApiCall_List(RQParam);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    gvSupplierApiSearch.VirtualItemCount = res.Count;

                    lblTotalRecords.Text = res.Count.ToString();
                }

                gvSupplierApiSearch.DataSource = (from a in res orderby a.Create_Date descending select a).ToList();
                gvSupplierApiSearch.PageIndex = PageNo;
                gvSupplierApiSearch.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                gvSupplierApiSearch.DataBind();
            }
            else
            {
                gvSupplierApiSearch.DataSource = null;
                gvSupplierApiSearch.DataBind();
                lblTotalRecords.Text = string.Empty;
            }
        }

        protected void gvSupplierApiSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }
       

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlSupplierName.SelectedIndex = 0;
            ddlMasterCountry.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            gvSupplierApiSearch.DataSource = null;
            gvSupplierApiSearch.DataBind();
            lblTotalRecords.Text = "0";
        }

        protected void fillSupplierAddClick()
        {
            MDMSVC.DC_Supplier_Search_RQ RQParam = new MDMSVC.DC_Supplier_Search_RQ();
            RQParam.SupplierType = "API Static Data";
            RQParam.PageNo = 0;
            RQParam.PageSize = int.MaxValue;
            ddlSupplierList.DataSource = _objMasterSVC.GetSupplier(RQParam);
            ddlSupplierList.DataValueField = "Supplier_Id";
            ddlSupplierList.DataTextField = "Name";
            ddlSupplierList.DataBind();
            ddlSupplierList.Items.RemoveAt(0);
            ddlSupplierList.Items.Insert(0, new ListItem("--Select --", "0"));
        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {
            dvError.Style.Add("display", "none");
            fillSupplierAddClick();
            fillattributes("MappingFileConfig", "MappingEntity", ddlEntityList);
            ddlSupplierList.SelectedIndex = 0;
            ddlEntityList.SelectedIndex = 0;
            txtApiLocation.Text = "";
        }
        protected void btnadddetails_Click(object sender, EventArgs e)
        {
            var supplier = ddlSupplierList.SelectedItem.Value;
            var entity = ddlEntityList.SelectedItem.Value;
            var path = txtApiLocation.Text;
            if (supplier == "0" || entity == "0")
            {
                BootstrapAlert.BootstrapAlertMessage(dvError, "Please select both Entity and Supplier name!!", BootstrapAlertType.Warning);
            }
            else if (path == "API Location not found")
            {
                BootstrapAlert.BootstrapAlertMessage(dvError, "API Location not found.Please select again!!", BootstrapAlertType.Danger);
            }
            else
            {
                Guid applicationid = Guid.Parse(btnadddetails.CommandArgument);
                string callby = System.Web.HttpContext.Current.User.Identity.Name;
                var res = _objMappingSVCs.Pentaho_SupplierApi_Call(applicationid, callby);
                if (res != null)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvError, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                }
                else BootstrapAlert.BootstrapAlertMessage(dvError, "Error!!", BootstrapAlertType.Danger);
            }
        }

        //protected void btnNewReset_Click(object sender, EventArgs e)
        //{
        //    ddlSupplierList.SelectedIndex = 0;
        //    ddlEntityList.SelectedIndex = 0;
        //    txtApiLocation.Text = null;
        //}

        protected void ddlSupplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvError.Style.Add("Display", "none");
            txtApiLocation.Text = "";
            if (ddlSupplierList.SelectedIndex != 0 && ddlEntityList.SelectedIndex != 0)
            {
                Guid supplierid = Guid.Parse(ddlSupplierList.SelectedItem.Value);
                Guid entityid = Guid.Parse(ddlEntityList.SelectedItem.Value);
                var res = _objMappingSVCs.Pentaho_SupplierApiLocationId_Get(supplierid, entityid);
                if (res != null && res.Count > 0)
                {
                    btnadddetails.CommandArgument = res[0].ApiLocation_Id.ToString();
                    txtApiLocation.Text = res[0].ApiEndPoint.ToString();
                }
                else
                {
                    txtApiLocation.Text = "API Location not found";
                }
            }

        }

        protected void ddlEntityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvError.Style.Add("Display", "none");
            txtApiLocation.Text = "";
            if (ddlSupplierList.SelectedIndex != 0 && ddlEntityList.SelectedIndex != 0)
            {
                Guid supplierid = Guid.Parse(ddlSupplierList.SelectedItem.Value);
                Guid entityid = Guid.Parse(ddlEntityList.SelectedItem.Value);
                var res = _objMappingSVCs.Pentaho_SupplierApiLocationId_Get(supplierid, entityid);
                if (res != null && res.Count > 0)
                {
                    btnadddetails.CommandArgument = res[0].ApiLocation_Id.ToString();
                    txtApiLocation.Text = res[0].ApiEndPoint.ToString();
                }
                else
                {
                    txtApiLocation.Text = "API Location not found";
                   
                }
            }
        }

        
    }
}