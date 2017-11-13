using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TLGX_Consumer.App_Code;
using System.Configuration;
using TLGX_Consumer.Controller;
using System.Data;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace TLGX_Consumer.controls.staticdataconfig
{
    public partial class manageFIleUpload : System.Web.UI.UserControl
    {

        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        Controller.MasterDataSVCs mastersvc = new Controller.MasterDataSVCs();
        MappingSVCs _objMappingSVCs = new MappingSVCs();


        public static Guid SelectedSupplierImportAttributeValue_Id = Guid.Empty;
        public static string MatchedStatus = "";
        public static int intPageIndexforErrorLogGrid = 0;
        public static int intActivityPageIndex = 0;
        public static int TotalCountActivity = 0;
        public static int intTotalPage = 1;

        public static Guid? SupplierImportFile_Id { get; private set; }

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
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
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
            fillattributes("MappingFileConfig", "MappingStatus", ddlStatus);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        private void fillmatchingdata(int PageSize, int PageNo)
        {
            lblTotalRecords.Text = string.Empty;
            MDMSVC.DC_SupplierImportFileDetails_RQ RQParam = new MDMSVC.DC_SupplierImportFileDetails_RQ();

            if (ddlSupplierName.SelectedItem.Value != "0")
                RQParam.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value);
            if (ddlMasterCountry.SelectedItem.Value != "0")
                RQParam.Entity = ddlMasterCountry.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.STATUS = ddlStatus.SelectedItem.Text;
            if (txtFrom.Text != String.Empty && txtTo.Text != String.Empty)
            {
                RQParam.From_Date = DateTime.Parse(txtFrom.Text);
                RQParam.TO_Date = DateTime.Parse(txtTo.Text).AddDays(1); //added 1 day to include all the files added on 'To date' (upto time '23:59:59' by default it takes '00:00:00')
            }

            RQParam.PageNo = PageNo;
            RQParam.PageSize = PageSize;

            var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQParam);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    gvFileUploadSearch.VirtualItemCount = res[0].TotalRecords;

                    lblTotalRecords.Text = res[0].TotalRecords.ToString();
                }

                gvFileUploadSearch.DataSource = (from a in res orderby a.CREATE_DATE descending select a).ToList();
                gvFileUploadSearch.PageIndex = PageNo;
                gvFileUploadSearch.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                gvFileUploadSearch.DataBind();
            }
            else
            {
                gvFileUploadSearch.DataSource = null;
                gvFileUploadSearch.DataBind();
                lblTotalRecords.Text = string.Empty;
            }

        }

        private void fillmatchingdataArchive(int PageSize, int PageNo)
        {
            MDMSVC.DC_SupplierImportFileDetails_RQ RQParam = new MDMSVC.DC_SupplierImportFileDetails_RQ();

            if (ddlSupplierName.SelectedItem.Value != "0")
                RQParam.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value);
            if (ddlMasterCountry.SelectedItem.Value != "0")
                RQParam.Entity = ddlMasterCountry.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.STATUS = ddlStatus.SelectedItem.Text;
            RQParam.PageNo = PageNo;
            RQParam.PageSize = PageSize;

            var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQParam);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    gvFileUploadSearch.VirtualItemCount = res[0].TotalRecords;

                    lblTotalRecords.Text = res[0].TotalRecords.ToString();
                }

                gvFileUploadSearch.DataSource = (from a in res orderby a.CREATE_DATE descending select a).ToList();
                gvFileUploadSearch.PageIndex = PageNo;
                gvFileUploadSearch.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                gvFileUploadSearch.DataBind();
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "File has been archived successfully.", BootstrapAlertType.Success);
            }
            else
            {
                gvFileUploadSearch.DataSource = null;
                gvFileUploadSearch.DataBind();
                lblTotalRecords.Text = string.Empty;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlSupplierName.SelectedIndex = 0;
            ddlMasterCountry.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtFrom.Text = String.Empty;
            txtTo.Text = String.Empty;
            gvFileUploadSearch.DataSource = null;
            gvFileUploadSearch.DataBind();
            lblTotalRecords.Text = "0";
        }

        protected void gvFileUploadSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void gvFileUploadSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRowId = Guid.Parse(gvFileUploadSearch.DataKeys[index].Values[0].ToString());

                if (e.CommandName.ToString() == "ViewDetails")
                {
                    List<MDMSVC.DC_SupplierImportFileDetails> lstobj = new List<MDMSVC.DC_SupplierImportFileDetails>();
                    MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails();
                    MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
                    SelectedSupplierImportAttributeValue_Id = myRowId;
                    SupplierImportFile_Id = myRowId;
                    RQ.SupplierImportFile_Id = myRowId;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQ);

                    if (res != null && res.Count > 0)
                    {
                        hdnViewDetailsFlag.Value = "false";
                    }

                }
                if (e.CommandName.ToString() == "Process")
                {
                    MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails();
                    MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
                    SelectedSupplierImportAttributeValue_Id = myRowId;
                    SupplierImportFile_Id = myRowId;
                    RQ.SupplierImportFile_Id = myRowId;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQ);


                    SelectedSupplierImportAttributeValue_Id = myRowId;
                    obj.SupplierImportFile_Id = res[0].SupplierImportFile_Id;
                    obj.Supplier_Id = res[0].Supplier_Id;
                    obj.Supplier = res[0].Supplier;
                    obj.SavedFilePath = res[0].SavedFilePath;
                    obj.PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name;
                    obj.Entity = res[0].Entity;
                    obj.STATUS = res[0].STATUS;
                    var result = _objMappingSVCs.StaticFileUploadProcessFile(obj);

                    fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), gvFileUploadSearch.PageIndex);

                    BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);

                }
                if (e.CommandName == "SoftDelete")
                {
                    MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
                    RQ.SupplierImportFile_Id = myRowId;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQ);

                    MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails
                    {
                        SupplierImportFile_Id = myRowId,
                        STATUS = res[0].STATUS,
                        PROCESS_DATE = DateTime.Now,
                        PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = false
                    };

                    var result = _objMappingSVCs.UpdateSupplierStaticFileDetails(obj);
                    fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);

                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "File has been deleted Successfully", (BootstrapAlertType)result.StatusCode);
                }
                if (e.CommandName == "UnDelete")
                {
                    MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
                    RQ.SupplierImportFile_Id = myRowId;
                    RQ.PageNo = 0;
                    RQ.PageSize = int.MaxValue;
                    var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQ);
                    MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails
                    {
                        SupplierImportFile_Id = myRowId,
                        STATUS = res[0].STATUS,
                        PROCESS_DATE = DateTime.Now,
                        PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };
                    var result = _objMappingSVCs.UpdateSupplierStaticFileDetails(obj);
                    fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);

                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "File has been un deleted Successfully", (BootstrapAlertType)result.StatusCode);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvFileUploadSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                // Retrieve the key value for the current row. Here it is an int.
                string SupplierImportFile_Id = gvFileUploadSearch.DataKeys[e.Row.RowIndex].Values[0].ToString();// Convert.ToString(rowView["SupplierImportFile_Id"]);
                LinkButton btnViewDetail = (LinkButton)e.Row.FindControl("btnViewDetail");
                if (btnViewDetail != null)
                {
                    if (btnViewDetail.CommandName == "ViewDetails")
                    {
                        btnViewDetail.Attributes.Add("onclick", "showDetailsModal('" + SupplierImportFile_Id + "');");
                    }
                }
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }
    }
}

