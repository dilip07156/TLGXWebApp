using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.hotel
{
    public partial class media : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        // used for retrieving drop down list attribute values from masters
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();

        protected string MediaAbsPath;
        protected string MediaAbsUrl;
        public static int PageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);

                BindMedia();
                BindCategory();
                //BindFileFormat();
                BindMediaType();
                BindRoomCategory();
                BindSubCategory();
                BindAttributeType();
                BindFileMaster();
                divMediaAttributes.Visible = false;
                BindDateDefaults();
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                var resultAcco = AccSvc.GetHotelDetails(Accomodation_ID);
                MediaAbsPath = System.Configuration.ConfigurationManager.AppSettings["MediaAbsPath"] + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";
                txtMediaPath.Text = MediaAbsPath;

                MediaAbsUrl = System.Configuration.ConfigurationManager.AppSettings["MediaAbsURL"] + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";
                txtMediaURL.Text = MediaAbsUrl;
                txtMediaPosition.Attributes.Add("onkeyup", "CheckMediaPositionDuplicate();");
            }
        }

        private void BindDateDefaults()
        {
            txtValidFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtValidTo.Text = "31/12/2099";
        }

        #region Bind Data
        private void BindMedia()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            var result = AccSvc.GetAccomodation_MediaDetails(Accomodation_ID, Guid.Empty);
            gvMedia.DataSource = result;
            gvMedia.DataBind();
        }

        private void BindMediaAttributes(Guid Accomodation_Media_ID)
        {
            var result = AccSvc.GetAccomodation_MediaAttributesDetails(Accomodation_Media_ID, Guid.Empty, PageIndex, 5);
            gvMediaAttributes.DataSource = result;
            if (result != null)
            {
                if (result.Count > 0)
                    gvMediaAttributes.VirtualItemCount = result[0].TotalRecords;
            }
            gvMediaAttributes.PageIndex = PageIndex;
            gvMediaAttributes.PageSize = 5;
            gvMediaAttributes.DataBind();
        }

        private void BindMediaType()
        {
            ddlMediaType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Media", "MediaType").MasterAttributeValues;
            ddlMediaType.DataTextField = "AttributeValue";
            ddlMediaType.DataValueField = "MasterAttributeValue_Id";
            ddlMediaType.DataBind();
        }

        private void BindRoomCategory()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            var result = AccSvc.GetRoomDetails(Accomodation_ID, Guid.Empty);
            ddlRoomCategory.DataSource = (from r in result where r.IsActive select new { r.Accommodation_RoomInfo_Id, r.RoomCategory }).ToList();
            ddlRoomCategory.DataTextField = "RoomCategory";
            ddlRoomCategory.DataValueField = "Accommodation_RoomInfo_Id";
            ddlRoomCategory.DataBind();
        }

        private void BindCategory()
        {
            ddlFileCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Media", "FileCategory").MasterAttributeValues;
            ddlFileCategory.DataTextField = "AttributeValue";
            ddlFileCategory.DataValueField = "MasterAttributeValue_Id";
            ddlFileCategory.DataBind();
        }

        private void BindSubCategory()
        {
            ddlSubCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Media", "FileCategory").MasterAttributeValues;
            ddlSubCategory.DataTextField = "AttributeValue";
            ddlSubCategory.DataValueField = "MasterAttributeValue_Id";
            ddlSubCategory.DataBind();
        }

        //private void BindFileFormat()
        //{
        //    ddlFileFormat.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Media", "MediaFileFormat").MasterAttributeValues;
        //    ddlFileFormat.DataTextField = "AttributeValue";
        //    ddlFileFormat.DataValueField = "MasterAttributeValue_Id";
        //    ddlFileFormat.DataBind();
        //}

        private void BindFileMaster()
        {
            ddlFileMaster.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("Media", "MediaFileMaster").MasterAttributeValues;
            ddlFileMaster.DataTextField = "AttributeValue";
            ddlFileMaster.DataValueField = "MasterAttributeValue_Id";
            ddlFileMaster.DataBind();
        }

        private void BindAttributeType()
        {
            ddlAttributeType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("MediaAttributes", "MediaDetails").MasterAttributeValues;
            ddlAttributeType.DataTextField = "AttributeValue";
            ddlAttributeType.DataValueField = "MasterAttributeValue_Id";
            ddlAttributeType.DataBind();
        }

        private void ClearMediaControls()
        {
            txtDescription.Text = string.Empty;
            //  txtMediaID.Text = "1";

            //txtMediaName.Text = string.Empty;

            txtMediaPath.Text = string.Empty;
            txtMediaPosition.Text = string.Empty;
            txtMediaURL.Text = string.Empty;

            txtValidFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtValidTo.Text = "31/12/2099";

            ddlFileCategory.ClearSelection();
            ddlFileCategory.SelectedIndex = 0;

            //ddlFileFormat.ClearSelection();
            //ddlFileFormat.SelectedIndex = 0;

            ddlMediaType.ClearSelection();
            ddlMediaType.SelectedIndex = 0;

            ddlRoomCategory.ClearSelection();
            ddlRoomCategory.SelectedIndex = 0;

            ddlSubCategory.ClearSelection();
            ddlSubCategory.SelectedIndex = 0;

            ddlFileMaster.ClearSelection();
            ddlFileMaster.SelectedIndex = 0;

            btnSaveMedia.CommandName = "AddMedia";
            gvMediaAttributes.SelectedIndex = 0;

            imgMedia.ImageUrl = string.Empty;
            imgMedia.Dispose();

            //Delete save media if exist for the same 
            DeleteMedia();

            //fuMedia.Dispose();

            ClearMediaAttributesControls();

            gvMediaAttributes.DataSource = null;
            PageIndex = 0;
            gvMediaAttributes.DataBind();

            divMediaAttributes.Visible = false;

            axAsyncFileUpload.Enabled = true;

            dvMsg.Style.Add("display", "none");
            dvMsgMediaAttribute.Style.Add("display", "none");

            UpMediaModal.Update();

        }

        private void DeleteMedia()
        {
            try
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                var resultAcco = AccSvc.GetHotelDetails(Accomodation_ID);
                string savePath = Server.MapPath(UploadFolderPath);
                //Create file desitnation directory
                savePath = savePath + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";
                if (System.IO.Directory.Exists(savePath))
                {
                    if (Convert.ToString(hdnMediaName.Value) != null)
                    {
                        string pathToCheck = savePath + Convert.ToString(hdnMediaName.Value);
                        if (System.IO.File.Exists(pathToCheck))
                        {
                            System.IO.File.Delete(pathToCheck);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearMediaAttributesControls()
        {
            ddlAttributeType.ClearSelection();
            ddlAttributeType.SelectedIndex = 0;
            txtAttributeValue.Text = string.Empty;
            btnSaveMediaAttributes.CommandName = "AddAttributes";
            gvMediaAttributes.SelectedIndex = 0;
            dvMsgMediaAttribute.Style.Add("display", "none");
        }
        #endregion

        protected void grdMedia_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void grdMedia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            dvMsgMediaAttribute.Style.Add("display", "none");

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);

            if (e.CommandName.ToString() == "Select")
            {
                var record = AccSvc.GetAccomodation_MediaDetails(Accomodation_ID, myRow_Id);

                if (record != null)
                {
                    if (record.Count > 0)
                    {
                        UpMediaModal.Update();
                        ClearMediaControls();
                        axAsyncFileUpload.Enabled = false;

                        txtDescription.Text = System.Web.HttpUtility.HtmlDecode(record[0].Description);
                        // txtMediaID.Text = System.Web.HttpUtility.HtmlDecode(record[0].MediaID);
                        //txtMediaName.Text = record[0].MediaName;
                        txtMediaPath.Text = System.Web.HttpUtility.HtmlDecode(record[0].Media_Path);
                        txtMediaPosition.Text = System.Web.HttpUtility.HtmlDecode(record[0].Media_Position.ToString());
                        txtMediaURL.Text = System.Web.HttpUtility.HtmlDecode(record[0].Media_URL);
                        hdnEditMediaId.Value = Convert.ToString(record[0].Accommodation_Media_Id);
                        imgMedia.ImageUrl = System.Web.HttpUtility.HtmlDecode(record[0].Media_URL);
                        imgMedia.DescriptionUrl = System.Web.HttpUtility.HtmlDecode(record[0].Description);

                        if (record[0].ValidFrom != null)
                        {
                            txtValidFrom.Text = (record[0].ValidFrom ?? DateTime.Now).ToString("dd/MM/yyyy");
                        }

                        if (record[0].ValidTo != null)
                        {
                            txtValidTo.Text = (record[0].ValidTo ?? DateTime.Now).ToString("dd/MM/yyyy");
                        }

                        ddlFileCategory.ClearSelection();
                        if (ddlFileCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].Category)) != null)
                        {
                            ddlFileCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].Category)).Selected = true;
                        }

                        //ddlFileFormat.ClearSelection();
                        //if (ddlFileFormat.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].FileFormat)) != null)
                        //{
                        //    ddlFileFormat.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].FileFormat)).Selected = true;
                        //}

                        ddlMediaType.ClearSelection();
                        if (ddlMediaType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].MediaType)) != null)
                        {
                            ddlMediaType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].MediaType)).Selected = true;
                        }


                        ddlRoomCategory.ClearSelection();
                        if (ddlRoomCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].RoomCategory)) != null)
                        {
                            ddlRoomCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].RoomCategory)).Selected = true;
                        }

                        ddlSubCategory.ClearSelection();
                        if (ddlSubCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].SubCategory)) != null)
                        {
                            ddlSubCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].SubCategory)).Selected = true;
                        }

                        ddlFileMaster.ClearSelection();
                        if (ddlFileMaster.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].MediaFileMaster)) != null)
                        {
                            ddlFileMaster.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].MediaFileMaster)).Selected = true;
                        }



                        btnSaveMedia.CommandName = "UpdateMedia";

                        divMediaAttributes.Visible = true;

                        BindMediaAttributes(record[0].Accommodation_Media_Id);



                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "javascript:showMediaModal();", true);

                    }
                }
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Media newObj = new MDMSVC.DC_Accommodation_Media
                {
                    Accommodation_Media_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateAccomodation_MediaDetails(newObj))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Media details has been deleted successfully", BootstrapAlertType.Success);
                    BindMedia();
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error occured while deleteing media details. Please contact system Admin.", BootstrapAlertType.Danger);

                }
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_Media newObj = new MDMSVC.DC_Accommodation_Media
                {
                    Accommodation_Media_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateAccomodation_MediaDetails(newObj))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Media details has been retrieved successfully", BootstrapAlertType.Success);
                    BindMedia();
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error occured while retrieved media details. Please contact system Admin.", BootstrapAlertType.Danger);

                }


            }
        }

        protected void btnSaveMedia_Click(object sender, EventArgs e)
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            var resultAcco = AccSvc.GetHotelDetails(Accomodation_ID);
            MediaAbsPath = System.Configuration.ConfigurationManager.AppSettings["MediaAbsPath"] + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";
            MediaAbsUrl = System.Configuration.ConfigurationManager.AppSettings["MediaAbsURL"] + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";
            if (Page.IsValid)
            {
                if (btnSaveMedia.CommandName == "AddMedia")
                {
                    MDMSVC.DC_Accommodation_Media newObj = new MDMSVC.DC_Accommodation_Media
                    {
                        Accommodation_Media_Id = Guid.NewGuid(),
                        Category = (ddlFileCategory.SelectedIndex == 0 ? null : ddlFileCategory.SelectedItem.Text),
                        Description = txtDescription.Text,
                        //FileFormat = (ddlFileFormat.SelectedIndex == 0 ? null : ddlFileFormat.SelectedItem.Text),
                        // MediaID = txtMediaID.Text,
                        MediaType = (ddlMediaType.SelectedIndex == 0 ? null : ddlMediaType.SelectedItem.Text),
                        Media_Position = Convert.ToInt32(txtMediaPosition.Text),
                        RoomCategory = (ddlRoomCategory.SelectedIndex == 0 ? null : ddlRoomCategory.SelectedItem.Text),
                        SubCategory = (ddlSubCategory.SelectedIndex == 0 ? null : ddlSubCategory.SelectedItem.Text),
                        Accommodation_Id = Guid.Parse(Request.QueryString["Hotel_Id"]),
                        ValidFrom = DateTime.ParseExact(txtValidFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        ValidTo = DateTime.ParseExact(txtValidTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        IsActive = true,
                        Create_Date = DateTime.Now,
                        Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                        MediaFileMaster = (ddlFileMaster.SelectedIndex == 0 ? null : ddlFileMaster.SelectedItem.Text)
                    };

                    if (!string.IsNullOrWhiteSpace(hdnMediaName.Value))
                    {
                        //if(!string.IsNullOrWhiteSpace(txtMediaName.Text))
                        //{
                        //    newObj.MediaName = System.IO.Path.GetFileNameWithoutExtension(txtMediaName.Text) + System.IO.Path.GetExtension(hdnMediaName.Value);
                        //    newObj.Media_Path = MediaAbsPath + resultAcco[0].CompanyHotelID.ToString() + "_" + System.IO.Path.GetFileNameWithoutExtension(txtMediaName.Text) + "_Ori" + System.IO.Path.GetExtension(hdnMediaName.Value);
                        //    newObj.Media_URL  = MediaAbsUrl + resultAcco[0].CompanyHotelID.ToString() + "_" + System.IO.Path.GetFileNameWithoutExtension(txtMediaName.Text) + "_Ori" + System.IO.Path.GetExtension(hdnMediaName.Value);
                        //}
                        //else
                        //{
                        newObj.MediaName = hdnMediaName.Value;
                        newObj.Media_Path = MediaAbsPath + resultAcco[0].CompanyHotelID.ToString() + "_" + System.IO.Path.GetFileNameWithoutExtension(hdnMediaName.Value) + "_Ori" + System.IO.Path.GetExtension(hdnMediaName.Value);
                        newObj.Media_URL = MediaAbsUrl + resultAcco[0].CompanyHotelID.ToString() + "_" + System.IO.Path.GetFileNameWithoutExtension(hdnMediaName.Value) + "_Ori" + System.IO.Path.GetExtension(hdnMediaName.Value);
                        newObj.FileFormat = System.IO.Path.GetExtension(hdnMediaName.Value);
                        //}
                    }
                    else
                    {
                        newObj.MediaName = string.Empty;
                        newObj.Media_Path = string.Empty;
                        newObj.Media_URL = string.Empty;
                        newObj.FileFormat = string.Empty;
                    }

                    string sMediaPathToCheck = MediaAbsPath + hdnMediaName.Value;
                    if (!string.IsNullOrWhiteSpace(hdnMediaName.Value))
                    {
                        if (!System.IO.Directory.Exists(MapPath(MediaAbsPath)))
                        {
                            System.IO.Directory.CreateDirectory(MapPath(MediaAbsPath));
                        }

                        if (System.IO.File.Exists(MapPath(newObj.Media_Path)))
                        {
                            System.IO.File.Delete(MapPath(newObj.Media_Path));
                        }

                        FileInfo file = new FileInfo(MapPath(sMediaPathToCheck));
                        file.MoveTo(file.Directory.FullName + "\\" + System.IO.Path.GetFileName(MapPath(newObj.Media_Path)));
                        //System.Drawing.Image _img = ThumbnailImage(System.Drawing.Image.FromFile(file.Directory.FullName + "\\" + System.IO.Path.GetFileName(MapPath(newObj.Media_Path))), 90, 60);
                        //file.MoveTo(file.Directory.FullName + "\\" + System.IO.Path.GetFileName(MapPath(newObj.Media_Path)));

                    }

                    if (AccSvc.AddAccomodation_MediaDetails(newObj))
                    {
                        //Auto insert file attributes
                        try
                        {
                            //Shell32.Shell shell = new Shell32.Shell();
                            Type t = Type.GetTypeFromProgID("Shell.Application");

                            dynamic shell = Activator.CreateInstance(t);
                            Shell32.Folder objFolder = shell.NameSpace(System.IO.Path.GetDirectoryName(MapPath(newObj.Media_Path)));
                            Shell32.FolderItem folderItem = objFolder.ParseName(System.IO.Path.GetFileName(MapPath(newObj.Media_Path)));
                            List<string> arrHeaders = new List<string>();
                            //Get file attribute list for consideratioins
                            string strAttributeslist = Convert.ToString(ConfigurationManager.AppSettings["MediaAttributes"]);
                            List<string> _lstAttributes = strAttributeslist.Split(',').ToList();


                            for (int i = 0; i < short.MaxValue; i++)
                            {
                                string header = objFolder.GetDetailsOf(null, i);
                                if (String.IsNullOrEmpty(header))
                                    break;
                                arrHeaders.Add(header);
                            }

                            for (int i = 0; i < arrHeaders.Count; i++)
                            {
                                string AttrTypr = arrHeaders[i];
                                if (!_lstAttributes.Contains(AttrTypr))
                                    continue;
                                string AttrVal = objFolder.GetDetailsOf(folderItem, i);

                                if (!string.IsNullOrWhiteSpace(AttrVal))
                                {
                                    MDMSVC.DC_Accomodation_Media_Attributes newObjAttr = new MDMSVC.DC_Accomodation_Media_Attributes
                                    {
                                        Accomodation_Media_Attributes_Id = Guid.NewGuid(),
                                        Accomodation_Media_Id = newObj.Accommodation_Media_Id,
                                        AttributeType = AttrTypr,
                                        AttributeValue = AttrVal,
                                        IsActive = true,
                                        Create_Date = DateTime.Now,
                                        IsSystemAttribute = true,
                                        Create_User = System.Web.HttpContext.Current.User.Identity.Name
                                    };

                                    AccSvc.AddAccomodation_MediaAttributesDetails(newObjAttr);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }


                        BindMedia();
                        ClearMediaControls();
                        axAsyncFileUpload.Enabled = true;
                        hdnFlag.Value = "true";
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Media has been added successfully", BootstrapAlertType.Success);

                    }
                    else
                    {
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                    }

                }
                else if (btnSaveMedia.CommandName == "UpdateMedia")
                {
                    Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                    Guid myRow_Id = Guid.Parse(gvMedia.SelectedDataKey.Value.ToString());
                    int intMediaPosition = 0;
                    if (Int32.TryParse(txtMediaPosition.Text, out intMediaPosition))
                    {
                        intMediaPosition = Convert.ToInt32(txtMediaPosition.Text);
                    }
                    MDMSVC.DC_Accommodation_Media newObj = new MDMSVC.DC_Accommodation_Media
                    {
                        Accommodation_Media_Id = myRow_Id,
                        Category = (ddlFileCategory.SelectedIndex == 0 ? null : ddlFileCategory.SelectedItem.Text),
                        MediaFileMaster = (ddlFileMaster.SelectedIndex == 0 ? null : ddlFileMaster.SelectedItem.Text),
                        Description = txtDescription.Text,
                        //FileFormat = (ddlFileFormat.SelectedIndex == 0 ? null : ddlFileFormat.SelectedItem.Text),
                        MediaType = (ddlMediaType.SelectedIndex == 0 ? null : ddlMediaType.SelectedItem.Text),
                        Media_Position = intMediaPosition,
                        RoomCategory = (ddlRoomCategory.SelectedIndex == 0 ? null : ddlRoomCategory.SelectedItem.Text),
                        SubCategory = (ddlSubCategory.SelectedIndex == 0 ? null : ddlSubCategory.SelectedItem.Text),
                        ValidFrom = DateTime.ParseExact(txtValidFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        ValidTo = DateTime.ParseExact(txtValidTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        IsActive = true,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    };

                    if (AccSvc.UpdateAccomodation_MediaDetails(newObj))
                    {
                        BindMedia();
                        ClearMediaControls();
                        axAsyncFileUpload.Enabled = true;
                        hdnFlag.Value = "true";
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Media has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                    {
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                    }

                    // btnSaveMedia.CommandName = "AddMedia";

                }
            }
            //hdnFlag.Value = "true";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2" + DateTime.Today.Ticks.ToString(), "javascript:closeMediaModal();", true);
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "javascript:closeMediaModal();", true);

        }

        protected void btnMediaReset_Click(object sender, EventArgs e)
        {
            ClearMediaControls();
        }

        protected void btnResetMediaAttributes_Click(object sender, EventArgs e)
        {
            ClearMediaAttributesControls();
        }

        protected void btnSaveMediaAttributes_Click(object sender, EventArgs e)
        {

            Guid Media_Id = Guid.Parse(gvMedia.SelectedDataKey.Value.ToString());

            if (btnSaveMediaAttributes.CommandName == "AddAttributes")
            {
                MDMSVC.DC_Accomodation_Media_Attributes newObj = new MDMSVC.DC_Accomodation_Media_Attributes
                {
                    Accomodation_Media_Attributes_Id = Guid.NewGuid(),
                    Accomodation_Media_Id = Media_Id,
                    AttributeType = ddlAttributeType.SelectedIndex == 0 ? null : ddlAttributeType.SelectedItem.Text,
                    AttributeValue = txtAttributeValue.Text,
                    IsActive = true,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.AddAccomodation_MediaAttributesDetails(newObj))
                {
                    BindMediaAttributes(Media_Id);
                    ClearMediaAttributesControls();
                    dvMsgMediaAttribute.Style.Add(HtmlTextWriterStyle.Display, "none");
                    BootstrapAlert.BootstrapAlertMessage(dvMsgAttribute, "Media Attribute has been added successfully", BootstrapAlertType.Success);
                }
                else
                {
                    dvMsgMediaAttribute.Style.Add(HtmlTextWriterStyle.Display, "none");
                    BootstrapAlert.BootstrapAlertMessage(dvMsgAttribute, "Error Occurred", BootstrapAlertType.Warning);
                }
            }
            else if (btnSaveMediaAttributes.CommandName == "UpdateAttributes")
            {
                Guid row_Id = Guid.Parse(gvMediaAttributes.SelectedDataKey.Value.ToString());

                MDMSVC.DC_Accomodation_Media_Attributes newObj = new MDMSVC.DC_Accomodation_Media_Attributes
                {
                    Accomodation_Media_Attributes_Id = row_Id,
                    Accomodation_Media_Id = Media_Id,
                    AttributeType = ddlAttributeType.SelectedIndex == 0 ? null : ddlAttributeType.SelectedItem.Text,
                    AttributeValue = txtAttributeValue.Text,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateAccomodation_MediaAttributesDetails(newObj))
                {
                    BindMediaAttributes(Media_Id);
                    ClearMediaAttributesControls();
                    dvMsgMediaAttribute.Style.Add(HtmlTextWriterStyle.Display, "none");
                    BootstrapAlert.BootstrapAlertMessage(dvMsgAttribute, "Media Attribute has been updated successfully", BootstrapAlertType.Success);
                }
                else
                {
                    dvMsgMediaAttribute.Style.Add(HtmlTextWriterStyle.Display, "none");
                    BootstrapAlert.BootstrapAlertMessage(dvMsgAttribute, "Error Occurred", BootstrapAlertType.Warning);
                }

                btnSaveMediaAttributes.CommandName = "AddAttributes";
            }
        }

        protected void gvMediaAttributes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dvMsgMediaAttribute.Style.Add("display", "none");

            Guid myRow_Id;

            if (!Guid.TryParse(e.CommandArgument.ToString(), out myRow_Id))
            {
                return;
            }

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            Accomodation_ID = Guid.Parse(Request.QueryString["Hotel_Id"]);
            Guid Media_Id = Guid.Parse(gvMedia.SelectedDataKey.Value.ToString());

            if (e.CommandName.ToString() == "Select")
            {

                var record = AccSvc.GetAccomodation_MediaAttributesDetails(Media_Id, myRow_Id, 0, 1);

                if (record != null)
                {
                    if (record.Count > 0)
                    {
                        if (ddlAttributeType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].AttributeType)) != null)
                        {
                            ddlAttributeType.ClearSelection();
                            ddlAttributeType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(record[0].AttributeType)).Selected = true;
                        }
                        txtAttributeValue.Text = System.Web.HttpUtility.HtmlDecode(record[0].AttributeValue);

                        btnSaveMediaAttributes.CommandName = "UpdateAttributes";
                    }
                }

            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accomodation_Media_Attributes newObj = new MDMSVC.DC_Accomodation_Media_Attributes
                {
                    Accomodation_Media_Attributes_Id = myRow_Id,
                    Accomodation_Media_Id = Media_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateAccomodation_MediaAttributesDetails(newObj))
                {
                    dvMsgAttribute.Style.Add(HtmlTextWriterStyle.Display, "none");
                    BootstrapAlert.BootstrapAlertMessage(dvMsgMediaAttribute, "Attribute details has been delete successfully", BootstrapAlertType.Success);
                    BindMediaAttributes(Media_Id);
                };
            }
            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accomodation_Media_Attributes newObj = new MDMSVC.DC_Accomodation_Media_Attributes
                {
                    Accomodation_Media_Attributes_Id = myRow_Id,
                    Accomodation_Media_Id = Media_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateAccomodation_MediaAttributesDetails(newObj))
                {
                    dvMsgAttribute.Style.Add(HtmlTextWriterStyle.Display, "none");
                    BootstrapAlert.BootstrapAlertMessage(dvMsgMediaAttribute, "Attribute details has been retrieve successfully", BootstrapAlertType.Success);
                    BindMediaAttributes(Media_Id);
                };
            }

        }

        protected void gvMediaAttributes_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected string UploadFolderPath = System.Configuration.ConfigurationManager.AppSettings["MediaAbsPath"];
        protected void axAsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            var resultAcco = AccSvc.GetHotelDetails(Accomodation_ID);

            string savePath = Server.MapPath(UploadFolderPath);
            string mediaPath = UploadFolderPath;

            //Create file desitnation directory
            savePath = savePath + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";
            mediaPath = mediaPath + resultAcco[0].ProductCategorySubType + "/" + resultAcco[0].CompanyHotelID.ToString() + "/";

            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }

            //Get the file name and extn to upload.
            string fileName = e.FileName;

            //txtMediaName.Text = fileName;
            txtMediaPath.Text = mediaPath;
            txtMediaURL.Text = savePath;

            UploadFolderPath = mediaPath;

            string pathToCheck = savePath + fileName;

            if (!string.IsNullOrWhiteSpace(hdnMediaName.Value))
            {
                if (System.IO.File.Exists(savePath + hdnMediaName.Value))
                {
                    System.IO.File.Delete(savePath + hdnMediaName.Value);
                }
            }

            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                System.IO.File.Delete(pathToCheck);
            }

            hdnMediaName.Value = fileName;
            axAsyncFileUpload.SaveAs(pathToCheck);

        }

        protected void btnAddMedia_Click(object sender, EventArgs e)
        {
            ClearMediaControls();
            UpMediaModal.Update();
            hdnEditMediaId.Value = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "javascript:showMediaModal();", true);
            axAsyncFileUpload.Enabled = true;
        }

        protected void gvMediaAttributes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            Guid Media_Id = Guid.Parse(gvMedia.SelectedDataKey.Value.ToString());
            BindMediaAttributes(Media_Id);
        }

        ////FTP Server URL.
        //string ftp = System.Configuration.ConfigurationManager.AppSettings["MediaFtpUrl"];

        ////FTP Folder name. Leave blank if you want to upload to root folder.
        //string ftpFolder = "Holidays/";

        ////Read the FileName and convert it to Byte array.
        //string fileName1 = Path.GetFileName(fuMedia.FileName);

        //byte[] fileBytes = null;
        //using (StreamReader fileStream = new StreamReader(fuMedia.PostedFile.InputStream))
        //{
        //    fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
        //    fileStream.Close();
        //}

        //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName1);

        //// This example assumes the FTP site uses anonymous logon.  
        //request.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["MediaFtpUser"], System.Configuration.ConfigurationManager.AppSettings["MediaFtpPassword"]);
        //request.Method = WebRequestMethods.Ftp.UploadFile;

        //request.ContentLength = fileBytes.Length;
        //request.UsePassive = true;
        ////request.UseBinary = true;
        ////request.ServicePoint.ConnectionLimit = fileBytes.Length;
        ////request.EnableSsl = false;

        //using (Stream requestStream = request.GetRequestStream())
        //{
        //    requestStream.Write(fileBytes, 0, fileBytes.Length);
        //    requestStream.Close();
        //}

        //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        //dvMsg.Attributes.Remove("class");
        //dvMsg.Attributes.Add("class", "alert alert-info alert-dismissable");
        //dvMsg.InnerHtml = "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + string.Format("Upload File Complete, status {0}", response.StatusDescription);

        //response.Close();
        //public static void Test()
        //{
        //    using (var image = Image.FromFile(@"c:\logo.png"))
        //    using (var newImage = ScaleImage(image, 300, 400))
        //    {
        //        newImage.Save(@"c:\test.png", ImageFormat.Png);
        //    }
        //}

        //public static System.Drawing.Image ThumbnailImage(System.Drawing.Image image, int maxWidth, int maxHeight)
        //{
        //    var ratioX = (double)maxWidth / image.Width;
        //    var ratioY = (double)maxHeight / image.Height;
        //    var ratio = Math.Min(ratioX, ratioY);

        //    var newWidth = (int)(image.Width * ratio);
        //    var newHeight = (int)(image.Height * ratio);

        //    var newImage = new Bitmap(newWidth, newHeight);

        //    using (var graphics = Graphics.FromImage(newImage))
        //        graphics.DrawImage(image, 0, 0, newWidth, newHeight);

        //    return newImage;
        //}
    }
}

