using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata
{    
    public enum ReRunMode
    {
        RERUN = 1,
        SCHEDULE = 2
    }

    public partial class manageSupplierImports : System.Web.UI.Page
    {
        private Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        private MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        private Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
       
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
                fillsuppliers("0");
                BindProductCategory(ddlProductCategory);
            }
            //if (ddlProductCategory.SelectedValue == "0")
            //    fillsuppliers("0");
            //else fillsuppliers(ddlProductCategory.SelectedValue);

            supplierwisedata.Visible = true;
            report.Visible = false;
            if (ddlSupplierName.SelectedValue == "0")
            {
                allsupplierdata.Visible = true;
                //ddlPriority.Visible = true;
            }
            else
            {
                allsupplierdata.Visible = false;
                //ddlPriority.Visible = false;
            }
        }

        private void BindProductCategory(DropDownList ddlProductCategoryBind)
        {
            var result = _objMasterSVC.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "SupplierInfo", Name = "ProductCategory" });
            if (result != null)
                if (result.Count > 0)
                {
                    ddlProductCategoryBind.Items.Clear();
                    ddlProductCategoryBind.DataSource = result;
                    ddlProductCategoryBind.DataTextField = "AttributeValue";
                    ddlProductCategoryBind.DataValueField = "AttributeValue";
                    ddlProductCategoryBind.DataBind();
                    ddlProductCategoryBind.Items.Insert(0, new ListItem { Text = "--All Category--", Value = "0" });
                }
        }

        private void fillsuppliers(string productCategory)
        {
            ddlSupplierName.Items.Clear();
            ddlPriority.Items.Clear();
            var res = _objMasterSVC.GetSuppliersByProductCategory(productCategory);
            //for allsupplier
            // var res = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataSource = res;
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
            ddlSupplierName.Items.Insert(0, new ListItem { Text = "--All Suppliers--", Value = "0" });
            //for Priority
            ddlPriority.DataSource = (from r in res orderby r.Priority select new { Priority = r.Priority }).Distinct().ToList();
            ddlPriority.DataValueField = "Priority";
            ddlPriority.DataTextField = "Priority";
            ddlPriority.DataBind();
            ddlPriority.Items.Remove(ddlPriority.Items.FindByValue("0"));
            ddlPriority.Items.Insert(0, new ListItem { Text = "--All Priority--", Value = "0" });
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
            supplierwisedata.Visible = false;
            allsupplierdata.Visible = false;
            report.Visible = true;
            if (ddlSupplierName.SelectedValue == "0")
            {
                var DataSet1 = MapSvc.GetsupplierwiseSummaryReport();
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptAllSupplierReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }
            else
            {
                string supplierid = ddlSupplierName.SelectedValue;
                var DataSet1 = MapSvc.GetsupplierwiseUnmappedSummaryReport(supplierid);
                ReportDataSource rds = new ReportDataSource("DataSet1", DataSet1);
                ReportViewersupplierwise.LocalReport.DataSources.Clear();
                ReportViewersupplierwise.LocalReport.ReportPath = "staticdata/rptSupplierwiseReport.rdlc";
                ReportViewersupplierwise.LocalReport.DataSources.Add(rds);
                ReportViewersupplierwise.Visible = true;
                ReportViewersupplierwise.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.PageWidth;
                ReportViewersupplierwise.DataBind();
                ReportViewersupplierwise.LocalReport.Refresh();
            }
        }

        public MDMSVC.DC_Message InsertFileRecord(string Entity, ReRunMode Mode)
        {
            Guid SupplierImportFile_Id = Guid.NewGuid();
            MappingSVCs _objMappingSVCs = new MappingSVCs();
            MDMSVC.DC_SupplierImportFileDetails _objFileDetails = new MDMSVC.DC_SupplierImportFileDetails();
            _objFileDetails.SupplierImportFile_Id = SupplierImportFile_Id;
            _objFileDetails.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedValue);
            _objFileDetails.Entity = Entity;
            _objFileDetails.OriginalFilePath = "";
            _objFileDetails.SavedFilePath = "";

            if (Mode == ReRunMode.RERUN)
            {
                _objFileDetails.STATUS = "UPLOADED";
            }
            else if (Mode == ReRunMode.SCHEDULE)
            {
                _objFileDetails.STATUS = "SCHEDULED";
            }

            _objFileDetails.Mode = "RE_RUN";
            _objFileDetails.CREATE_DATE = DateTime.Now;
            _objFileDetails.CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name;
            MDMSVC.DC_Message _objMsg = _objMappingSVCs.SaveSupplierStaticFileDetails(_objFileDetails);

            if (Mode == ReRunMode.RERUN)
            {
                //file Process logic
                MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails();
                MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
                RQ.SupplierImportFile_Id = SupplierImportFile_Id;
                RQ.PageNo = 0;
                RQ.PageSize = int.MaxValue;
                var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQ);
                obj.SupplierImportFile_Id = res[0].SupplierImportFile_Id;
                obj.Supplier_Id = res[0].Supplier_Id;
                obj.Supplier = res[0].Supplier;
                obj.SavedFilePath = res[0].SavedFilePath;
                obj.PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name;
                obj.Entity = res[0].Entity;
                obj.STATUS = res[0].STATUS;
                obj.Mode = res[0].Mode;
                var result = _objMappingSVCs.StaticFileUploadProcessFile(obj);
                //end
                //view File  details
                hdnFileId.Value = SupplierImportFile_Id.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "showDetailsModal", "showDetailsModal('" + SupplierImportFile_Id + "');", true);
            }
            // Response.Redirect("~/staticdata/files/upload.aspx");

            return _objMsg;
        }

        protected void btnCityReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("City", ReRunMode.RERUN);
            }
        }

        protected void btnCountryReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("Country", ReRunMode.RERUN);
            }
        }

        protected void btnHotelReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("Hotel", ReRunMode.RERUN);
            }
        }

        protected void btnRoomTypeReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("RoomType", ReRunMode.RERUN);
            }
        }

        protected void btnActivityReRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                InsertFileRecord("Activity", ReRunMode.RERUN);
            }
        }

        protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillsuppliers(ddlProductCategory.SelectedValue);
        }

        protected void btnActivityReRunSchedule_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                var msg = InsertFileRecord("Activity", ReRunMode.SCHEDULE);
                dvMsgActivity.Style.Add("display", "block");
                BootstrapAlert.BootstrapAlertMessage(dvMsgActivity, msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(msg.StatusCode));
            }
        }

        protected void btnRoomTypeReRunSchedule_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                var msg = InsertFileRecord("RoomType", ReRunMode.SCHEDULE);
                dvMsgRoomType.Style.Add("display", "block");
                BootstrapAlert.BootstrapAlertMessage(dvMsgRoomType, msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(msg.StatusCode));
            }
        }

        protected void btnHotelReRunSchedule_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                var msg = InsertFileRecord("Hotel", ReRunMode.SCHEDULE);
                dvMsgHotel.Style.Add("display", "block");
                BootstrapAlert.BootstrapAlertMessage(dvMsgHotel, msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(msg.StatusCode));
            }
        }

        protected void btnCityReRunSchedule_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                var msg = InsertFileRecord("City", ReRunMode.SCHEDULE);
                dvMsgCity.Style.Add("display", "block");
                BootstrapAlert.BootstrapAlertMessage(dvMsgCity, msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(msg.StatusCode));
            }
        }

        protected void btnCountryReRunSchedule_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedValue != "0")
            {
                var msg = InsertFileRecord("Country", ReRunMode.SCHEDULE);
                dvMsgCountry.Style.Add("display", "block");
                BootstrapAlert.BootstrapAlertMessage(dvMsgCountry, msg.StatusMessage, (BootstrapAlertType)Convert.ToInt32(msg.StatusCode));
            }
        }

        protected void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            dvMsgCountry.Style.Add("display", "none");
            dvMsgHotel.Style.Add("display", "none");
            dvMsgCity.Style.Add("display", "none");
            dvMsgRoomType.Style.Add("display", "none");
            dvMsgActivity.Style.Add("display", "none");
        }

        protected void btnHotelGeoCode_Click(object sender, EventArgs e)
        {
          
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = "MappingFileConfig";
            RQ.Name = "MappingEntity";
            var resvalues = _objMasterSVC.GetAllAttributeAndValues(RQ);

            if (resvalues != null && resvalues.Count > 0)
            {
                Guid entityId = Guid.Parse((resvalues.Where(x => x.AttributeValue == "GeoCode").Select(y => y.MasterAttributeValue_Id).FirstOrDefault()).ToString());
                Guid SupplierId = Guid.Parse(ddlSupplierName.SelectedItem.Value);
                var res = MapSvc.Pentaho_SupplierApiLocationId_Get(SupplierId, entityId);

                if (res != null && res.Count > 0)
                {
                    string callby = System.Web.HttpContext.Current.User.Identity.Name;
                    var ApplicationCallId = Guid.Parse(res[0].ApiLocation_Id.ToString());
                    var Geo_res = MapSvc.Pentaho_SupplierApi_Call(ApplicationCallId, callby);
                    BootstrapAlert.BootstrapAlertMessage(dvMsgHotel, Geo_res.StatusMessage, BootstrapAlertType.Information);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgHotel, "API Location not found", BootstrapAlertType.Information);
                }
            }
        }

        protected void btnHotelRun_Click(object sender, EventArgs e)
        {            
            if (ddlSupplierName.SelectedItem.Text != "--All Suppliers--")
            {
                Guid SupplierId = Guid.Parse(ddlSupplierName.SelectedItem.Value);                
                var result = MapSvc.KeywordReRun("HotelName", "SUPPLIER", SupplierId);             
                BootstrapAlert.BootstrapAlertMessage(dvMsgHotel, result.StatusMessage, BootstrapAlertType.Information);
            }
        }

        protected void btnRoomRun_Click(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedItem.Text != "--All Suppliers--")
            {
                Guid SupplierId = Guid.Parse(ddlSupplierName.SelectedItem.Value);
                var result = MapSvc.KeywordReRun("RoomType", "SUPPLIER", SupplierId);
                BootstrapAlert.BootstrapAlertMessage(dvMsgRoomType, result.StatusMessage, BootstrapAlertType.Information);
            }
        }
    }
}   