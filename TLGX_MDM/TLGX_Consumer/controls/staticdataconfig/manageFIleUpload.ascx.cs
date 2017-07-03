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
            ddlSupplierList.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierList.DataValueField = "Supplier_Id";
            ddlSupplierList.DataTextField = "Name";
            ddlSupplierList.DataBind();

            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        protected void fillEntity()
        {
            fillattributes("MappingFileConfig", "MappingEntity", ddlMasterCountry);
            fillattributes("MappingFileConfig", "MappingEntity", ddlEntityList);
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
            MDMSVC.DC_SupplierImportFileDetails_RQ RQParam = new MDMSVC.DC_SupplierImportFileDetails_RQ();

            if (ddlSupplierName.SelectedItem.Value != "0")
                RQParam.Supplier_Id = Guid.Parse(ddlSupplierName.SelectedItem.Value);
            if (ddlMasterCountry.SelectedItem.Value != "0")
                RQParam.Entity = ddlMasterCountry.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.STATUS = ddlStatus.SelectedItem.Text;

            RQParam.PageNo = PageNo;
            RQParam.PageSize = PageSize;// Convert.ToInt32(ddlShowEntries.SelectedItem.Text);

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
            //hdnFlag1.Value = "true";

        }

        //protected void btnNewUpload_Click(object sender, EventArgs e)
        //{
        //    fillattributes("MappingFileConfig", "MappingEntity", ddlEntityList);
        //}
        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    //string FolderPath = Server.MapPath("~/STATIC_FILES");
        //    if (btnUpload.CommandName == "AddMedia")
        //    {
        //        if (Page.IsValid)
        //        {
        //            string ServerPath = ConfigurationManager.AppSettings["STATIC_FILESAbsPath"];
        //            //string savePath = Server.MapPath(ServerPath);
        //            //string savePath = ConfigurationManager.AppSettings["STATIC_SERVERPATH"];  // This is for test only. this should be revert back to Server.MapPath while publishing code
        //            //if (FileUpld.HasFiles)
        //            //{
        //            string FileName = string.Empty;// Path.GetFileName(FileUpld.FileName);
        //            string fileName = string.Empty; // FileUpld.PostedFile.FileName;
        //                try
        //                {
        //                    var destinationDir = Server.MapPath(ServerPath) + ddlSupplierList.SelectedItem.Text + "//" + ddlEntityList.SelectedItem.Text;
        //                    if (!Directory.Exists(destinationDir))
        //                    {
        //                        Directory.CreateDirectory(destinationDir);
        //                    }

        //                    var newFileID = Guid.NewGuid();
        //                    string fileSavePath = destinationDir + "//" + Path.GetFileNameWithoutExtension(fileName) + "-" + newFileID.ToString() + Path.GetExtension(fileName);

        //                    FileUpld.SaveAs(fileSavePath);

        //                    MappingSVCs _objMappingSVCs = new MappingSVCs();

        //                    MDMSVC.DC_SupplierImportFileDetails _objFileDetails = new MDMSVC.DC_SupplierImportFileDetails();
        //                    _objFileDetails.SupplierImportFile_Id = newFileID;
        //                    _objFileDetails.Supplier_Id = Guid.Parse(ddlSupplierList.SelectedValue);
        //                    _objFileDetails.Entity = ddlEntityList.SelectedItem.Text;
        //                    _objFileDetails.OriginalFilePath = fileName;
        //                    _objFileDetails.SavedFilePath = fileSavePath;
        //                    _objFileDetails.STATUS = "UPLOADED";
        //                    _objFileDetails.CREATE_DATE = DateTime.Now;
        //                    _objFileDetails.CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name;

        //                    MDMSVC.DC_Message _objMsg = _objMappingSVCs.SaveSupplierStaticFileDetails(_objFileDetails);

        //                    if (_objMsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
        //                    {
        //                        hdnFlag.Value = "true";

        //                        BootstrapAlert.BootstrapAlertMessage(dvMsg, _objMsg.StatusMessage, BootstrapAlertType.Success);
        //                    }
        //                    else
        //                    {
        //                        hdnFlag.Value = "false";
        //                        BootstrapAlert.BootstrapAlertMessage(dvMsg, _objMsg.StatusMessage, BootstrapAlertType.Danger);
        //                    }

        //                    _objFileDetails = null;

        //                    fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        //                    clearControls();
        //                }

        //                catch (Exception ex)
        //                {
        //                    clearControls();
        //                    BootstrapAlert.BootstrapAlertMessage(dvMsg, ex.Message, BootstrapAlertType.Danger);
        //                }
        //            //}
        //        }
        //    }
        //}

        public void FileUpload(string filename)
        {
            string ServerPath = ConfigurationManager.AppSettings["STATIC_FILESAbsPath"];
            string fileName = filename;
            try
            {
                var destinationDir = Server.MapPath(ServerPath) + Convert.ToString(Session["SupplierListSelected"]) + "//" + Convert.ToString(Session["EntityListSelected"]);
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                var newFileID = Guid.NewGuid();
                string fileSavePath = destinationDir + "//" + Path.GetFileNameWithoutExtension(fileName) + "-" + newFileID.ToString() + Path.GetExtension(fileName);

                FileUpld.SaveAs(fileSavePath);

                MappingSVCs _objMappingSVCs = new MappingSVCs();

                MDMSVC.DC_SupplierImportFileDetails _objFileDetails = new MDMSVC.DC_SupplierImportFileDetails();
                _objFileDetails.SupplierImportFile_Id = newFileID;
                _objFileDetails.Supplier_Id = Guid.Parse(Convert.ToString(Session["SupplierListSelectedValue"]));
                _objFileDetails.Entity = Convert.ToString(Session["EntityListSelected"]);
                _objFileDetails.OriginalFilePath = fileName;
                _objFileDetails.SavedFilePath = fileSavePath;
                _objFileDetails.STATUS = "UPLOADED";
                _objFileDetails.CREATE_DATE = DateTime.Now;
                _objFileDetails.CREATE_USER = System.Web.HttpContext.Current.User.Identity.Name;

                MDMSVC.DC_Message _objMsg = _objMappingSVCs.SaveSupplierStaticFileDetails(_objFileDetails);

                if (_objMsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    btnReset_Click(null, EventArgs.Empty);
                    BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, _objMsg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    //hdnFlag.Value = "false";
                    BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, _objMsg.StatusMessage, BootstrapAlertType.Danger);
                }

                _objFileDetails = null;
                fillmatchingdata(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                clearControls();
            }

            catch (Exception ex)
            {
                clearControls();
                BootstrapAlert.BootstrapAlertMessage(dvMsg, ex.Message, BootstrapAlertType.Danger);
            }
        }

        protected void clearControls()
        {
            Session.Remove("SupplierListSelected");
            Session.Remove("SupplierListSelectedValue");
            Session.Remove("EntityListSelected");
            ddlSupplierList.ClearSelection();
            ddlSupplierList.SelectedIndex = 0;
            ddlEntityList.ClearSelection();
            ddlEntityList.SelectedIndex = 0;
            FileUpld.Attributes.Add("OnClientUploadComplete", "Return  OnClientUploadComplete");
            FileUpld.Enabled = false;
            //ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('From Code behind');</script>", false);
            ////Page.ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToString(), "FileUploadCompleted()", true);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlSupplierName.SelectedIndex = 0;
            ddlMasterCountry.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            gvFileUploadSearch.DataSource = null;
            gvFileUploadSearch.DataBind();
            lblTotalRecords.Text = "0";
        }

        protected void btnNewReset_Click(object sender, EventArgs e)
        {
            Session.Remove("SupplierListSelected");
            Session.Remove("SupplierListSelectedValue");
            Session.Remove("EntityListSelected");
            ddlSupplierList.SelectedIndex = 0;
            ddlEntityList.SelectedIndex = 0;
            FileUpld.Dispose();
            FileUpld.Enabled = false;
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

                    frmViewDetailsConfig.Visible = true;
                    frmViewDetailsConfig.DataSource = res;
                    frmViewDetailsConfig.DataBind();

                    if (res != null && res.Count > 0)
                    {
                        hdnViewDetailsFlag.Value = "false";
                        frmErrorlog();
                    }

                }
                if(e.CommandName.ToString()== "Process")
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
                    obj.SupplierImportFile_Id = myRowId;
                    obj.Supplier_Id = myRowId;
                    obj.SavedFilePath = res[0].SavedFilePath;
                    obj.PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name;
                    obj.Entity = res[0].Entity;
                    obj.STATUS = res[0].STATUS;
                    var result = _objMappingSVCs.StaticFileUploadProcessFile(obj);
                    
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void frmErrorlog()
        {
            Repeater rptrErrorLog = (Repeater)frmViewDetailsConfig.FindControl("rptrErrorLog");
            Button btnPrevious = (Button)frmViewDetailsConfig.FindControl("btnPrevious");
            Button btnNext = (Button)frmViewDetailsConfig.FindControl("btnNext");
            Label lblTotalCount = (Label)frmViewDetailsConfig.FindControl("lblTotalCount");

            MDMSVC.DC_SupplierImportFile_ErrorLog_RQ _objSearch = new MDMSVC.DC_SupplierImportFile_ErrorLog_RQ();
            _objSearch.SupplierImportFile_Id = Guid.Parse(frmViewDetailsConfig.DataKey[0].ToString());
            _objSearch.PageNo = intActivityPageIndex;
            _objSearch.PageSize = 5;

            var result = _objMappingSVCs.GetStaticDataUploadErrorLog(_objSearch);
            if (result != null && result.Count > 0)
            {
                TotalCountActivity = result[0].TotalCount;
                lblTotalCount.Text = "Total Record Count: " + TotalCountActivity.ToString();
                intTotalPage = (int)Math.Ceiling((double)TotalCountActivity / 5);
                if (intTotalPage > intActivityPageIndex + 1)
                    btnNext.Enabled = true;
                rptrErrorLog.DataSource = result;
                rptrErrorLog.DataBind();
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                btnDownload.Visible = true;
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Button btnPrevious = (Button)frmViewDetailsConfig.FindControl("btnPrevious");
            if (intActivityPageIndex >= 0)
            {
                intActivityPageIndex = intActivityPageIndex - 1;
                if (intActivityPageIndex == 0)
                    btnPrevious.Enabled = false;
                else
                    btnPrevious.Enabled = true;
                frmErrorlog();
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Button btnNext = (Button)frmViewDetailsConfig.FindControl("btnNext");
            Button btnPrevious = (Button)frmViewDetailsConfig.FindControl("btnPrevious");
            if (intActivityPageIndex <= intTotalPage)
            {
                intActivityPageIndex++;
                btnPrevious.Enabled = true;
                if (intActivityPageIndex + 1 >= intTotalPage)
                    btnNext.Enabled = false;
                else
                    btnNext.Enabled = true;
                frmErrorlog();
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            MDMSVC.DC_SupplierImportFile_ErrorLog_RQ _objSearch = new MDMSVC.DC_SupplierImportFile_ErrorLog_RQ();
            TextBox txtPath = (TextBox)frmViewDetailsConfig.FindControl("txtPath");
            var filename = Path.GetFileNameWithoutExtension(txtPath.Text);

            _objSearch.SupplierImportFile_Id = SupplierImportFile_Id;
            var result = _objMappingSVCs.GetStaticDataUploadErrorLog(_objSearch);
            if (result != null && result.Count > 0)
            {
                //Writeing CSV file
                StringBuilder sb = new StringBuilder();

                string csv = string.Empty;
                List<string> lstFileHeader = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SupplierFileDetails_GetErrorLog_ColumnHeader"]).Split(',').ToList();

                foreach (var item in result[0].GetType().GetProperties())
                {
                    if (lstFileHeader.Contains(item.Name))
                        csv += item.Name + ',';
                }
                sb.Append(string.Format("{0}", csv) + Environment.NewLine);
                foreach (var item in result)
                {
                    sb.Append(string.Format("{0},{1},{2},{3}", Convert.ToString(item.Error_DATE), Convert.ToString(item.ErrorCode), Convert.ToString(item.ErrorType), Convert.ToString(item.ErrorDescription)));
                    sb.Append(Environment.NewLine);
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());
                sb = null;
                if (bytes != null)
                {
                    //Download the CSV file.
                    var response = HttpContext.Current.Response;
                    response.Clear();
                    response.ContentType = "text/csv";
                    response.AddHeader("Content-Length", bytes.Length.ToString());
                    response.AddHeader("Content-disposition", "attachment; filename=\"" + filename + ".csv" + "\"");
                    response.BinaryWrite(bytes);
                    response.Flush();
                    response.End();
                }
                // hdnFlag1.Value = "true";
            }
        }

        protected void btnArchive_Click(object sender, EventArgs e)
        {
            TextBox txtSupplier = (TextBox)frmViewDetailsConfig.FindControl("txtSupplier");
            TextBox txtEntity = (TextBox)frmViewDetailsConfig.FindControl("txtEntity");
            TextBox txtPath = (TextBox)frmViewDetailsConfig.FindControl("txtPath");
            TextBox txtStatus = (TextBox)frmViewDetailsConfig.FindControl("txtStatus");

            string serverPath = ConfigurationManager.AppSettings["STATIC_FILES_ARCHIVEDAbsPath"];
            string archivePath;
            string status;

            archivePath = txtPath.Text;
            status = "ARCHIVED";
            var fileName = Path.GetFileName(txtPath.Text);
            var directoryName = Path.GetDirectoryName(txtPath.Text);
            var fullSourcePathWithFilename = directoryName + "\\" + fileName;
            var destinationDir = Server.MapPath(serverPath) + txtSupplier.Text + "//" + txtEntity.Text;
            if (File.Exists(fullSourcePathWithFilename))
            {
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                string fileSavePath = destinationDir + "//" + fileName;

                File.Move(fullSourcePathWithFilename, fileSavePath);
                MDMSVC.DC_SupplierImportFileDetails obj = new MDMSVC.DC_SupplierImportFileDetails();
                MappingSVCs _objMappingSVCs = new MappingSVCs();
                var fullPath = fileSavePath;

                obj.ArchiveFilePath = fullPath;
                obj.STATUS = status;
                obj.PROCESS_DATE = DateTime.Now;
                obj.PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name;
                obj.SupplierImportFile_Id = Guid.Parse(frmViewDetailsConfig.DataKey[0].ToString());

                List<MDMSVC.DC_SupplierImportFileDetails> _lstDC_SupplierImportFileDetails = new List<MDMSVC.DC_SupplierImportFileDetails>();
                _lstDC_SupplierImportFileDetails.Add(obj);
                MDMSVC.DC_Message _objMsg = _objMappingSVCs.UpdateSupplierStaticFileDetails(obj);

                if (_objMsg.StatusCode == MDMSVC.ReadOnlyMessageStatusCode.Success)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _objMsg.StatusMessage, BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, _objMsg.StatusMessage, BootstrapAlertType.Danger);
                }
                obj = null;
                fillmatchingdataArchive(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                hdnViewDetailsFlag.Value = "true";
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "File have been already archived.", BootstrapAlertType.Danger);
                hdnViewDetailsFlag.Value = "true";
            }
        }

        protected void FileUpld_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {

            FileUpload(e.FileName);
        }

        protected void FileUpld_UploadStart(object sender, AjaxControlToolkit.AjaxFileUploadStartEventArgs e)
        {
            //hdnEntityListSelected.Value = ddlEntityList.SelectedItem.Text;
            //hdnSupplierListSelected.Value = ddlSupplierList.SelectedItem.Text;

        }

        protected void ddlEntityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["EntityListSelected"] = ddlEntityList.SelectedItem.Text;
            //if (Session["EntityListSelected"] != null && Session["SupplierListSelected"] != null)
            if (ddlSupplierList.SelectedValue != "0" && ddlEntityList.SelectedValue != "0")
                FileUpld.Enabled = true;
            if (ddlEntityList.SelectedValue == "0")
            {
                Session.Remove("EntityListSelected");
            }
        }

        protected void ddlSupplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SupplierListSelected"] = ddlSupplierList.SelectedItem.Text;
            Session["SupplierListSelectedValue"] = ddlSupplierList.SelectedValue;
            //if (Session["EntityListSelected"] != null && Session["SupplierListSelected"] != null)
            if(ddlSupplierList.SelectedValue!="0" && ddlEntityList.SelectedValue!="0")
                FileUpld.Enabled = true;
            if (ddlSupplierList.SelectedValue == "0")
            {
                Session.Remove("SupplierListSelected");
                Session.Remove("SupplierListSelectedValue");

            }
        }

        protected void btnNewUpload_Click(object sender, EventArgs e)
        {
            ddlSupplierList.SelectedIndex = 0;
            ddlEntityList.SelectedIndex = 0;
            if (ddlSupplierList.SelectedValue != "0" && ddlEntityList.SelectedValue != "0")
                FileUpld.Enabled = true;
            else
                FileUpld.Enabled = false;
        }
    }
}

