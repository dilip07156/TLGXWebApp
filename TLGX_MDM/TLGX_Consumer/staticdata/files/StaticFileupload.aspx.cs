using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.staticdata.files
{
    public partial class StaticFileupload : System.Web.UI.Page
    {
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MappingSVCs _objMappingSVCs = new MappingSVCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdown();
                //btnUploadCompleted.Style.Add("display", "none");
            }
        }

        private void BindDropdown()
        {
            try
            {
                MDMSVC.DC_Supplier_Search_RQ RQParam = new MDMSVC.DC_Supplier_Search_RQ();
                RQParam.SupplierType = "File Static Data";
                RQParam.PageNo = 0;
                RQParam.PageSize = int.MaxValue;

                var resultSet = _objMasterSVC.GetSupplier(RQParam);

                RQParam.SupplierType = "File & API Static Data";
                var resultSet2 = _objMasterSVC.GetSupplier(RQParam);
                resultSet.AddRange(resultSet2);

                resultSet = resultSet.OrderBy(o => o.Name).ToList();

                ddlSupplierList.DataSource = resultSet;
                ddlSupplierList.DataValueField = "Supplier_Id";
                ddlSupplierList.DataTextField = "Name";
                ddlSupplierList.DataBind();
                ddlSupplierList.Items.RemoveAt(0);
                ddlSupplierList.Items.Insert(0, new ListItem("--Select --", "0"));
                fillattributes("MappingFileConfig", "MappingEntity", ddlEntityList);
            }
            catch (Exception)
            {

            }
        }
        public void fillattributes(string masterfor, string attributename, DropDownList ddl)
        {
            ddl.Items.Clear();
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = masterfor;
            RQ.Name = attributename;
            var resvalues = _objMasterSVC.GetAllAttributeAndValues(RQ);
            ddl.DataSource = resvalues;
            ddl.DataTextField = "AttributeValue";
            ddl.DataValueField = "MasterAttributeValue_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--ALL--", "0"));
            RQ = null;
            resvalues = null;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ddlSupplierList.SelectedValue == "0" || ddlEntityList.SelectedValue == "0")
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "Please select valid supplier & entity before upload.", BootstrapAlertType.Danger);
                return;
            }
            if (FileUpload1.HasFile)
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).Replace(".", "").ToLower();
                string strSavedValidFileExtension = ValidFile();
                if (strFileType == string.Empty)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "Invalid file.", BootstrapAlertType.Danger);
                }
                else if (strSavedValidFileExtension == string.Empty)
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "No file type has been defined.", BootstrapAlertType.Danger);
                }
                else if (strSavedValidFileExtension == strFileType)
                {
                    Guid FileUploadId = Guid.NewGuid();
                    long ActualFileSize = FileUpload1.PostedFile.ContentLength;
                    var response = UploadFileInChunks(FileUpload1.PostedFile, ActualFileSize, FileUploadId);

                    if (response.UploadSucceeded)
                    {
                        MappingSVCs _objMappingSVCs = new MappingSVCs();
                        MDMSVC.DC_SupplierImportFileDetails _objFileDetails = new MDMSVC.DC_SupplierImportFileDetails();
                        _objFileDetails.SupplierImportFile_Id = FileUploadId;
                        _objFileDetails.Supplier_Id = Guid.Parse(ddlSupplierList.SelectedValue);
                        _objFileDetails.Entity = ddlEntityList.SelectedItem.Text;
                        _objFileDetails.OriginalFilePath = FileUpload1.FileName;
                        _objFileDetails.SavedFilePath = response.UploadedPath;
                        //_objFileDetails.STATUS = "UPLOADED";
                        _objFileDetails.STATUS = "NEW";
                        _objFileDetails.Mode = "ALL";
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
                            BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, _objMsg.StatusMessage, BootstrapAlertType.Danger);
                        }
                        _objFileDetails = null;
                        clearControls();
                    }
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "Please select valid file.", BootstrapAlertType.Danger);
                }
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "No file has been selected to upload.", BootstrapAlertType.Danger);
            }

        }

        private string ValidFile()
        {
            string strFileType = string.Empty;
            if (ddlSupplierList.SelectedValue != "0" && ddlEntityList.SelectedValue != "0")
            {
                MDMSVC.DC_SupplierImportAttributeValues_RQ RQ = new MDMSVC.DC_SupplierImportAttributeValues_RQ();
                RQ.PageNo = 0;
                RQ.PageSize = 1;
                RQ.AttributeType = "FileDetails";
                RQ.AttributeValue = "FORMAT";
                RQ.Status = "ACTIVE";
                RQ.Entity = ddlEntityList.SelectedItem.Text;
                RQ.Supplier_Id = Guid.Parse(ddlSupplierList.SelectedItem.Value);
                var res = _objMappingSVCs.GetStaticDataMappingAttributeValues(RQ);
                if (res != null && res.Count > 0)
                {
                    if (res.First().AttributeName == "TEXT")
                        strFileType = "txt";
                    else if (res.First().AttributeName == string.Empty)
                        strFileType = string.Empty;
                    else
                        strFileType = res.First().AttributeName.ToLower();
                }
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "Please select valid supplier & entity before upload.", BootstrapAlertType.Danger);
            }
            return strFileType;
        }

        protected void clearControls()
        {
            ddlSupplierList.ClearSelection();
            ddlSupplierList.SelectedIndex = 0;
            ddlEntityList.ClearSelection();
            ddlEntityList.SelectedIndex = 0;
            FileUpload1.Dispose();
            gvFileUploadSearchForNew.DataSource = null;
            gvFileUploadSearchForNew.DataBind();
        }

        private MDMSVC.DC_UploadResponse UploadFileInChunks(HttpPostedFile file, long actualFileSize, Guid FileUploadId)
        {
            MDMSVC.DC_UploadResponse returnResponse = new MDMSVC.DC_UploadResponse();
            string fileNameNew = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + "_" + FileUploadId.ToString().Replace("-", "_") + "." + System.IO.Path.GetExtension(file.FileName).Replace(".", "");

            long filePosition = 0;
            int filePart = 16 * 1024; //Each hit 16 kb file to avoid any serialization issue when transfering  data across WCF

            //Create buffer size to send to service based on filepart size
            byte[] bufferData = new byte[filePart];

            //Set the posted file data to file stream.
            Stream fileStream = file.InputStream;

            try
            {
                long actualFileSizeToUpload = actualFileSize;
                //Start reading the file from the specified position.
                fileStream.Position = filePosition;
                int fileBytesRead = 0;

                FileTransferSVC trfSvc = new FileTransferSVC();
                //Upload file data in parts until filePosition reaches the actual file end or size.
                while (filePosition != actualFileSizeToUpload)
                {
                    // read the next file part i.e. another 100 kb of data 
                    fileBytesRead = fileStream.Read(bufferData, 0, filePart);
                    if (fileBytesRead != bufferData.Length)
                    {
                        filePart = fileBytesRead;
                        byte[] bufferedDataToWrite = new byte[fileBytesRead];
                        //Copy the buffered data into bufferedDataToWrite
                        Array.Copy(bufferData, bufferedDataToWrite, fileBytesRead);
                        bufferData = bufferedDataToWrite;
                    }

                    //Populate the data contract to send it to the service method
                    returnResponse = trfSvc.TransferFileInChunks(new MDMSVC.DC_FileData { FileName = fileNameNew, BufferData = bufferData, FilePostition = filePosition });
                    if (!returnResponse.UploadSucceeded)
                    {
                        break;
                    }

                    //Update the filePosition position to continue reading data from that position back to server
                    filePosition += fileBytesRead;
                }
            }
            catch
            {
                return new MDMSVC.DC_UploadResponse { UploadSucceeded = false, UploadedPath = string.Empty };
            }
            finally
            {
                fileStream.Close();
            }
            return returnResponse;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearControls();
        }

        private void fillmatchingdataForNew(int PageSize, int PageNo)
        {
            //lblTotalRecords.Text = string.Empty;
            MDMSVC.DC_SupplierImportFileDetails_RQ RQParam = new MDMSVC.DC_SupplierImportFileDetails_RQ();

            if (ddlSupplierList.SelectedItem.Value != "0")
                RQParam.Supplier_Id = Guid.Parse(ddlSupplierList.SelectedItem.Value);
            if (ddlEntityList.SelectedItem.Value != "0")
                RQParam.Entity = ddlEntityList.SelectedItem.Text;


            RQParam.PageNo = PageNo;
            RQParam.PageSize = PageSize;
            RQParam.STATUS = "NEW";

            var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQParam);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    gvFileUploadSearchForNew.VirtualItemCount = res[0].TotalRecords;

                    gvFileUploadSearchForNew.DataSource = (from a in res orderby a.CREATE_DATE descending select a).ToList();
                    gvFileUploadSearchForNew.PageIndex = PageNo;
                    gvFileUploadSearchForNew.PageSize = 10;//Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                    gvFileUploadSearchForNew.DataBind();
                    btnUploadCompleted.Visible = true;
                    //lblTotalRecords.Text = res[0].TotalRecords.ToString();
                }
                else
                {
                    gvFileUploadSearchForNew.DataSource = null;
                    gvFileUploadSearchForNew.DataBind();
                    btnUploadCompleted.Visible = false;
                }

              
                //btnUploadCompleted.Style.Add("display", "");
            }
            else
            {
                gvFileUploadSearchForNew.DataSource = null;
                gvFileUploadSearchForNew.DataBind();
                btnUploadCompleted.Visible = false;
                //lblTotalRecords.Text = string.Empty;
            }

        }

        protected void ddlSupplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplierList.SelectedIndex != 0 && ddlEntityList.SelectedIndex != 0)
            {
                fillmatchingdataForNew(10, 0);
            }


        }

        protected void ddlEntityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplierList.SelectedIndex != 0 && ddlEntityList.SelectedIndex != 0)
            {
                fillmatchingdataForNew(10, 0);
            }

        }

        protected void gvFileUploadSearchForNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                // Retrieve the key value for the current row. Here it is an int.
                string SupplierImportFile_Id = gvFileUploadSearchForNew.DataKeys[e.Row.RowIndex].Values[0].ToString();// Convert.ToString(rowView["SupplierImportFile_Id"]);

                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }   
        }

        public static Guid SelectedSupplierImportAttributeValue_Id = Guid.Empty;
        public static Guid? SupplierImportFile_Id { get; private set; }
        protected void gvFileUploadSearchForNew_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Guid myRowId = Guid.Parse(gvFileUploadSearchForNew.DataKeys[index].Values[0].ToString());

             
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
                    fillmatchingdataForNew(10, 0);

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
                    fillmatchingdataForNew(10, 0);

                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "File has been un deleted Successfully", (BootstrapAlertType)result.StatusCode);
                }
            }
            catch
            {

            }
        }


        protected void btnUploadComplete_Click(object sender, EventArgs e)
        {
            if (ddlSupplierList.SelectedValue == "0" || ddlEntityList.SelectedValue == "0")
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "Please select valid supplier & entity before upload.", BootstrapAlertType.Danger);
                return;
            }

            MDMSVC.DC_SupplierImportFileDetails_RQ RQParam = new MDMSVC.DC_SupplierImportFileDetails_RQ();

            if (ddlSupplierList.SelectedItem.Value != "0")
                RQParam.Supplier_Id = Guid.Parse(ddlSupplierList.SelectedItem.Value);
            if (ddlEntityList.SelectedItem.Value != "0")
                RQParam.Entity = ddlEntityList.SelectedItem.Text;

            var res = _objMappingSVCs.GetSupplierStaticFileDetails(RQParam);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    MDMSVC.DC_SupplierImportFileDetails RQParams = new MDMSVC.DC_SupplierImportFileDetails();
                    RQParams.Supplier_Id = Guid.Parse(ddlSupplierList.SelectedItem.Value);
                    RQParams.Entity = ddlEntityList.SelectedItem.Text;
                    RQParams.STATUS = "NEW";
                    RQParams.IsActive = true;

                    gvFileUploadSearchForNew.VirtualItemCount = res[0].TotalRecords;

                    //lblTotalRecords.Text = res[0].TotalRecords.ToString();
                    _objMappingSVCs.UpdateSupplierImportFileDetailsFromNewToUploaded(RQParams);
                    fillmatchingdataForNew(10, 0);
                    //clearControls();
                    //BindDropdown();
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "No file has been selected to upload.", BootstrapAlertType.Danger);
                }


            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvmsgUploadCompleted, "No file has been selected to upload.", BootstrapAlertType.Danger);
            }

        }
    }
}