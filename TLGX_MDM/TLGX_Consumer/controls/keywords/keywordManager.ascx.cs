using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.keywords
{
    public partial class keywordManager : System.Web.UI.UserControl
    {
        Controller.MasterDataSVCs masterscv = new Controller.MasterDataSVCs();
        Controller.MappingSVCs mappingScv = new Controller.MappingSVCs();
        public static int PageNo = 0;
        public static int AliasPageNo = 0;
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillddlstatus();
                fillEntityFor();
                fillIcons();
                fillRoomAmenityTypes();
                fillRoomBedType();
                fillRoomBathroomType();
                fillRoomCategory();
            }
        }

        protected void fillddlstatus()
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = "SystemStatus";
            RQ.Name = "Status";
            var res = masterscv.GetAllAttributeAndValues(RQ);
            ddlStatus.DataSource = res;
            ddlStatus.DataTextField = "AttributeValue";
            ddlStatus.DataValueField = "MasterAttributeValue_Id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void fillEntityFor()
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = "MappingFileConfig";
            RQ.Name = "MappingEntity";
            var res = masterscv.GetAllAttributeAndValues(RQ);
            chklistEntityFor.DataSource = res;
            chklistEntityFor.DataTextField = "AttributeValue";
            chklistEntityFor.DataValueField = "AttributeValue";
            chklistEntityFor.DataBind();
            RQ = null;
        }

        protected void fillIcons()
        {
            MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
            RQ.MasterFor = "Icons";
            RQ.Name = "GlyphIcons";
            var res = masterscv.GetAllAttributeAndValues(RQ);

            ddlglyphiconForAttributes.DataSource = res;
            ddlglyphiconForAttributes.DataTextField = "AttributeValue";
            ddlglyphiconForAttributes.DataValueField = "MasterAttributeValue_Id";
            ddlglyphiconForAttributes.DataBind();

            ddlglyphiconForAttributes.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void fillkeyword(int PageSize, int PageNo)
        {
            MDMSVC.DC_Keyword_RQ RQParam = new MDMSVC.DC_Keyword_RQ();

            RQParam.systemWord = txtKeyword.Text;
            RQParam.Alias = txtAlias.Text;
            RQParam.Attribute = chkAttribute.Checked;

            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlStatus.SelectedItem.Text;

            RQParam.PageNo = PageNo;
            RQParam.PageSize = PageSize;

            var result = mappingScv.SearchKeyword(RQParam);

            if (result != null && result.Count > 0)
            {
                lblTotalCount.Text = result[0].TotalRecords.ToString();
                gvSearchResult.DataSource = result;
                gvSearchResult.VirtualItemCount = result[0].TotalRecords;
                gvSearchResult.PageSize = RQParam.PageSize;
                gvSearchResult.PageIndex = RQParam.PageNo;
                gvSearchResult.DataBind();
            }
            else
            {
                gvSearchResult.DataSource = null;
                gvSearchResult.DataBind();
                lblTotalCount.Text = String.Empty;
            }
        }

        public void fillkeywordalias()
        {
            Guid Keyword_Id = Guid.Parse(hdnKeywordId.Value);

            MDMSVC.DC_Keyword_RQ RQParam = new MDMSVC.DC_Keyword_RQ();

            //RQParam.Alias = txtAlias.Text;
            RQParam.Keyword_Id = Keyword_Id;
            RQParam.PageNo = AliasPageNo;
            RQParam.PageSize = Convert.ToInt32(ddlShowEntriesAlias.SelectedValue);


            var result = mappingScv.SearchKeywordAlias(RQParam);
            if (result != null && result.Count > 0)
            {
                //lblTotalAlias.Text = result[0].TotalRecords.ToString();
                grdAlias.DataSource = result;
                grdAlias.VirtualItemCount = result[0].TotalRecords;
                grdAlias.PageSize = RQParam.PageSize;
                grdAlias.PageIndex = RQParam.PageNo;
                grdAlias.DataBind();
            }
            else
            {
                List<MDMSVC.DC_keyword_alias> aliasList = new List<MDMSVC.DC_keyword_alias>();
                aliasList.Add(new MDMSVC.DC_keyword_alias
                {
                    Keyword_Id = Keyword_Id,
                    KeywordAlias_Id = Guid.NewGuid(),
                    Value = string.Empty,
                    Status = "ACTIVE",
                    Sequence = 0
                });

                grdAlias.DataSource = aliasList;
                grdAlias.DataBind();

                int columncount = grdAlias.Rows[0].Cells.Count;
                grdAlias.Rows[0].Cells.Clear();
                grdAlias.Rows[0].Cells.Add(new TableCell());
                grdAlias.Rows[0].Cells[0].ColumnSpan = columncount;
                grdAlias.Rows[0].Cells[0].Text = "No Alias defined yet.";

                //lblTotalAlias.Text = "0";
            }
        }

        public void fillRoomAmenityTypes()
        {
            ddlAmentityType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("RoomAmenities", "RoomAmenityType").MasterAttributeValues;
            ddlAmentityType.DataTextField = "AttributeValue";
            ddlAmentityType.DataValueField = "MasterAttributeValue_Id";
            ddlAmentityType.DataBind();
        }

        public void fillRoomCategory()
        {
            ddlRoomInfo_Category.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("RoomInfo", "CompanyRoomCategory").MasterAttributeValues;
            ddlRoomInfo_Category.DataTextField = "AttributeValue";
            ddlRoomInfo_Category.DataValueField = "MasterAttributeValue_Id";
            ddlRoomInfo_Category.DataBind();
        }

        public void fillRoomBedType()
        {
            ddlRoomInfo_BedType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("RoomInfo", "BedType").MasterAttributeValues;
            ddlRoomInfo_BedType.DataTextField = "AttributeValue";
            ddlRoomInfo_BedType.DataValueField = "MasterAttributeValue_Id";
            ddlRoomInfo_BedType.DataBind();
        }

        public void fillRoomBathroomType()
        {
            ddlRoomInfo_BathroomType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("RoomInfo", "BathRoomType").MasterAttributeValues;
            ddlRoomInfo_BathroomType.DataTextField = "AttributeValue";
            ddlRoomInfo_BathroomType.DataValueField = "MasterAttributeValue_Id";
            ddlRoomInfo_BathroomType.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void gvSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditKeyWordMgmr")
            {
                dvMsg.InnerText = string.Empty;
                dvMsg.Style.Add("display", "none");

                dvMsgAlias.InnerText = string.Empty;
                dvMsgAlias.Style.Add("display", "none");

                hdnKeywordId.Value = e.CommandArgument.ToString();

                MDMSVC.DC_Keyword_RQ RQParam = new MDMSVC.DC_Keyword_RQ();

                RQParam.Keyword_Id = Guid.Parse(hdnKeywordId.Value);
                RQParam.PageNo = 0;
                RQParam.PageSize = 1;

                var result = mappingScv.SearchKeyword(RQParam);

                if (result != null && result.Count > 0)
                {
                    txtAddNewKeyword.Text = result[0].Keyword;

                    dvAttrDetails.Attributes.Remove("class");
                    if (result[0].Attribute ?? false)
                    {
                        dvAttrDetails.Style.Add("display", "block");
                    }
                    else
                    {
                        dvAttrDetails.Style.Add("display", "none");
                    }

                    chkNewKeywordAttribute.Checked = result[0].Attribute ?? false;

                    txtKeywordSequence.Text = result[0].Sequence.ToString();
                    chklistEntityFor.ClearSelection();

                    if (!string.IsNullOrWhiteSpace(result[0].EntityFor))
                    {
                        var EntityFor = result[0].EntityFor.Split(',');
                        for (int count = 0; count < chklistEntityFor.Items.Count; count++)
                        {
                            if (EntityFor.Contains(chklistEntityFor.Items[count].ToString()))
                            {
                                chklistEntityFor.Items[count].Selected = true;
                            }
                        }
                    }

                    ddlShowEntriesAlias.SelectedIndex = 0;
                    ddlglyphiconForAttributes.ClearSelection();
                    //ddlglyphiconForAttributes.SelectedIndex = 0;
                    spanglyphicon.Attributes.Remove("class");

                    if (result[0].Icon != null)
                    {
                        ddlglyphiconForAttributes.ClearSelection();
                        if (!String.IsNullOrWhiteSpace(result[0].Icon))
                        {
                            if (ddlglyphiconForAttributes.Items.FindByText(result[0].Icon) != null)
                            {
                                ddlglyphiconForAttributes.Items.FindByText(result[0].Icon).Selected = true;
                                spanglyphicon.Attributes.Add("class", "glyphicon glyphicon-" + result[0].Icon);
                            }
                        }
                    }

                    ddlAttrType.ClearSelection();
                    //ddlAttrType.SelectedIndex = 0;
                    if (!string.IsNullOrWhiteSpace(result[0].AttributeType))
                    {
                        if (ddlAttrType.Items.FindByText(result[0].AttributeType) != null)
                        {
                            ddlAttrType.Items.FindByText(result[0].AttributeType).Selected = true;
                        }
                    }

                    ddlAttrLvl.ClearSelection();
                    ddlAmentityType.ClearSelection();
                    ddlRoomSchemaLoc.ClearSelection();
                    ddlRoomInfo_BathroomType.ClearSelection();
                    ddlRoomInfo_BedType.ClearSelection();
                    ddlRoomInfo_Category.ClearSelection();
                    ddlRoomInfo_Smoking.ClearSelection();

                    //ddlAttrLvl.SelectedIndex = 0;
                    //ddlAmentityType.SelectedIndex = 0;
                    //ddlRoomSchemaLoc.SelectedIndex = 0;
                    //ddlRoomInfo_BathroomType.SelectedIndex = 0;
                    //ddlRoomInfo_BedType.SelectedIndex = 0;
                    //ddlRoomInfo_Category.SelectedIndex = 0;
                    //ddlRoomInfo_Smoking.SelectedIndex = 0;

                    if (!string.IsNullOrWhiteSpace(result[0].AttributeLevel))
                    {
                        if (ddlAttrLvl.Items.FindByText(result[0].AttributeLevel) != null)
                        {
                            ddlAttrLvl.Items.FindByText(result[0].AttributeLevel).Selected = true;
                        }

                        if (result[0].AttributeLevel == "Room Info")
                        {

                            if (ddlRoomSchemaLoc.Items.FindByText(result[0].AttributeSubLevel) != null)
                            {
                                ddlRoomSchemaLoc.Items.FindByText(result[0].AttributeSubLevel).Selected = true;
                            }

                            if (result[0].AttributeSubLevel == "Room Category")
                            {
                                if (ddlRoomInfo_Category.Items.FindByText(result[0].AttributeSubLevelValue) != null)
                                {
                                    ddlRoomInfo_Category.Items.FindByText(result[0].AttributeSubLevelValue).Selected = true;
                                }
                            }
                            else if (result[0].AttributeSubLevel == "Bed Type")
                            {
                                if (ddlRoomInfo_BedType.Items.FindByText(result[0].AttributeSubLevelValue) != null)
                                {
                                    ddlRoomInfo_BedType.Items.FindByText(result[0].AttributeSubLevelValue).Selected = true;
                                }
                            }
                            else if (result[0].AttributeSubLevel == "Bathroom Type")
                            {
                                if (ddlRoomInfo_BathroomType.Items.FindByText(result[0].AttributeSubLevelValue) != null)
                                {
                                    ddlRoomInfo_BathroomType.Items.FindByText(result[0].AttributeSubLevelValue).Selected = true;
                                }
                            }
                            else if (result[0].AttributeSubLevel == "Smoking")
                            {
                                if (ddlRoomInfo_Smoking.Items.FindByText(result[0].AttributeSubLevelValue) != null)
                                {
                                    ddlRoomInfo_Smoking.Items.FindByText(result[0].AttributeSubLevelValue).Selected = true;
                                }
                            }
                        }
                        else if (result[0].AttributeLevel == "Room Amenity")
                        {
                            if (ddlAmentityType.Items.FindByText(result[0].AttributeSubLevel) != null)
                            {
                                ddlAmentityType.Items.FindByText(result[0].AttributeSubLevel).Selected = true;
                            }
                        }
                    }

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "hideshowAttrLvl('" + ddlAttrLvl.ClientID + "')", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "hideshowAttrLvlRoomSchema('" + ddlRoomSchemaLoc.ClientID + "')", true);

                    AliasPageNo = 0;
                    fillkeywordalias();
                }

                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                //txtAddNewKeyword.Text = row.Cells[0].Text;
                //chkNewKeywordAttribute.Checked = Convert.ToBoolean(row.Cells[1].Text);
                //txtKeywordSequence.Text = row.Cells[2].Text;
                //chklistEntityFor.ClearSelection();

                //if (!string.IsNullOrWhiteSpace(row.Cells[4].Text))
                //{
                //    var EntityFor = row.Cells[4].Text.Split(',');
                //    for (int count = 0; count < chklistEntityFor.Items.Count; count++)
                //    {
                //        if (EntityFor.Contains(chklistEntityFor.Items[count].ToString()))
                //        {
                //            chklistEntityFor.Items[count].Selected = true;
                //        }
                //    }
                //}

                //AliasPageNo = 0;
                //ddlShowEntriesAlias.SelectedIndex = 0;

                //ddlglyphiconForAttributes.ClearSelection();
                //ddlglyphiconForAttributes.SelectedIndex = 0;
                //spanglyphicon.Attributes.Remove("class");

                //Label lblIconText = (Label)row.FindControl("lblIconText");
                //if (lblIconText != null)
                //{
                //    ddlglyphiconForAttributes.ClearSelection();
                //    if (!String.IsNullOrWhiteSpace(lblIconText.Text))
                //    {
                //        if (ddlglyphiconForAttributes.Items.FindByText(lblIconText.Text) != null)
                //        {
                //            ddlglyphiconForAttributes.Items.FindByText(lblIconText.Text).Selected = true;
                //            spanglyphicon.Attributes.Add("class", "glyphicon glyphicon-" + lblIconText.Text);
                //        }
                //    }

                //}

                //fillkeywordalias();
            }
            else if (e.CommandName == "SoftDelete")
            {

                dvMsgAlias.InnerText = string.Empty;
                dvMsgAlias.Style.Add("display", "none");

                Guid Keyword_Id = Guid.Parse(e.CommandArgument.ToString());
                MDMSVC.DC_Keyword Keyword = new MDMSVC.DC_Keyword
                {
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Keyword_Id = Keyword_Id,
                    Status = "INACTIVE"
                };

                var result = mappingScv.AddUpdateKeyword(Keyword);
                fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), PageNo);

                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            }
            else if (e.CommandName == "UnDelete")
            {
                dvMsgAlias.InnerText = string.Empty;
                dvMsgAlias.Style.Add("display", "none");

                Guid Keyword_Id = Guid.Parse(e.CommandArgument.ToString());

                MDMSVC.DC_Keyword Keyword = new MDMSVC.DC_Keyword
                {
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Keyword_Id = Keyword_Id,
                    Status = "ACTIVE"
                };

                var result = mappingScv.AddUpdateKeyword(Keyword);
                fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), PageNo);

                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            Guid Keyword_Id = Guid.NewGuid();

            hdnKeywordId.Value = Keyword_Id.ToString();
            txtAddNewKeyword.Text = string.Empty;
            txtKeywordSequence.Text = string.Empty;
            chkNewKeywordAttribute.Checked = false;
            dvAttrDetails.Style.Add("display", "none");

            chklistEntityFor.ClearSelection();

            ddlAmentityType.SelectedIndex = 0;
            ddlAttrLvl.SelectedIndex = 0;
            ddlAttrType.SelectedIndex = 0;
            ddlRoomSchemaLoc.SelectedIndex = 0;
            ddlRoomInfo_BathroomType.SelectedIndex = 0;
            ddlRoomInfo_BedType.SelectedIndex = 0;
            ddlRoomInfo_Category.SelectedIndex = 0;
            ddlRoomInfo_Smoking.SelectedIndex = 0;

            List<MDMSVC.DC_keyword_alias> aliasList = new List<MDMSVC.DC_keyword_alias>();
            aliasList.Add(new MDMSVC.DC_keyword_alias
            {
                Keyword_Id = Keyword_Id,
                KeywordAlias_Id = Guid.NewGuid(),
                Value = string.Empty,
                Status = "ACTIVE",
                Sequence = 0
            });

            grdAlias.DataSource = aliasList;
            grdAlias.DataBind();

            int columncount = grdAlias.Rows[0].Cells.Count;
            grdAlias.Rows[0].Cells.Clear();
            grdAlias.Rows[0].Cells.Add(new TableCell());
            grdAlias.Rows[0].Cells[0].ColumnSpan = columncount;
            grdAlias.Rows[0].Cells[0].Text = "No Alias defined yet.";

            //lblTotalAlias.Text = "0";

            ddlglyphiconForAttributes.ClearSelection();
            ddlglyphiconForAttributes.SelectedIndex = 0;
            spanglyphicon.Attributes.Remove("class");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            txtKeyword.Text = String.Empty;
            txtAlias.Text = String.Empty;
            ddlStatus.SelectedIndex = 0;
            lblTotalCount.Text = "0";
            PageNo = 0;

            chklistEntityFor.ClearSelection();

            gvSearchResult.DataSource = null;
            gvSearchResult.DataBind();
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            PageNo = 0;
            fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void gvSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            PageNo = e.NewPageIndex;
            fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }

        protected void UpdateGridView(MDMSVC.DC_keyword_alias Row)
        {
            Guid Keyword_Id = Guid.Parse(hdnKeywordId.Value);

            string[] EntityFor = chklistEntityFor.Items.Cast<ListItem>().Where(x => x.Selected).Select(s => s.Text).ToArray();

            MDMSVC.DC_Keyword Keyword = new MDMSVC.DC_Keyword
            {
                Attribute = chkNewKeywordAttribute.Checked,
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                Edit_Date = DateTime.Now,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                Keyword = txtAddNewKeyword.Text,
                Keyword_Id = Keyword_Id,
                Sequence = Convert.ToInt32(txtKeywordSequence.Text),
                Status = "ACTIVE",
                EntityFor = string.Join(",", EntityFor),
                Icon = ddlglyphiconForAttributes.SelectedIndex == 0 ? string.Empty : ddlglyphiconForAttributes.SelectedItem.Text,
            };

            if (Row != null)
            {
                List<MDMSVC.DC_keyword_alias> aliasList = new List<MDMSVC.DC_keyword_alias>();
                aliasList.Add(Row);
                Keyword.Alias = aliasList.ToArray();
            }

            if (chkNewKeywordAttribute.Checked)
            {
                if (ddlAttrType.SelectedIndex != 0)
                {
                    Keyword.AttributeType = ddlAttrType.SelectedItem.Text;
                }
                if (ddlAttrLvl.SelectedIndex != 0)
                {
                    Keyword.AttributeLevel = ddlAttrLvl.SelectedItem.Text;

                    if (Keyword.AttributeLevel == "Room Info")
                    {
                        Keyword.AttributeSubLevel = ddlRoomSchemaLoc.SelectedIndex == 0 ? string.Empty : ddlRoomSchemaLoc.SelectedItem.Text;
                        if (Keyword.AttributeSubLevel == "Room Category")
                        {
                            Keyword.AttributeSubLevelValue = ddlRoomInfo_Category.SelectedIndex == 0 ? string.Empty : ddlRoomInfo_Category.SelectedItem.Text;
                        }
                        else if (Keyword.AttributeSubLevel == "Bed Type")
                        {
                            Keyword.AttributeSubLevelValue = ddlRoomInfo_BedType.SelectedIndex == 0 ? string.Empty : ddlRoomInfo_BedType.SelectedItem.Text;
                        }
                        else if (Keyword.AttributeSubLevel == "Bathroom Type")
                        {
                            Keyword.AttributeSubLevelValue = ddlRoomInfo_BathroomType.SelectedIndex == 0 ? string.Empty : ddlRoomInfo_BathroomType.SelectedItem.Text;
                        }
                        else if (Keyword.AttributeSubLevel == "Smoking")
                        {
                            Keyword.AttributeSubLevelValue = ddlRoomInfo_Smoking.SelectedIndex == 0 ? string.Empty : ddlRoomInfo_Smoking.SelectedItem.Text;
                        }
                        else
                        {
                            Keyword.AttributeSubLevelValue = string.Empty;
                        }
                    }
                    else if (Keyword.AttributeLevel == "Room Amenity")
                    {
                        Keyword.AttributeSubLevel = ddlAmentityType.SelectedIndex == 0 ? string.Empty : ddlAmentityType.SelectedItem.Text;
                        Keyword.AttributeSubLevelValue = string.Empty;
                    }
                    else
                    {
                        Keyword.AttributeSubLevel = string.Empty;
                        Keyword.AttributeSubLevelValue = string.Empty;
                    }

                }
            }
            else
            {
                Keyword.AttributeType = string.Empty;
                Keyword.AttributeLevel = string.Empty;
                Keyword.AttributeSubLevel = string.Empty;
                Keyword.AttributeSubLevelValue = string.Empty;
            }

            var result = mappingScv.AddUpdateKeyword(Keyword);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "hideshowAttrLvl('" + ddlAttrLvl.ClientID + "')", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "hideshowAttrLvlRoomSchema('" + ddlRoomSchemaLoc.ClientID + "')", true);

            BootstrapAlert.BootstrapAlertMessage(dvMsgAlias, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
        }

        protected void ddlShowEntriesAlias_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            AliasPageNo = 0;
            fillkeywordalias();
        }

        protected void grdAlias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                TextBox txtAlias = (TextBox)grdAlias.HeaderRow.FindControl("txtAlias");
                TextBox txtAliasSequence = (TextBox)grdAlias.HeaderRow.FindControl("txtAliasSequence");
                MDMSVC.DC_keyword_alias newAlias = new MDMSVC.DC_keyword_alias
                {
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Keyword_Id = Guid.Parse(hdnKeywordId.Value),
                    KeywordAlias_Id = Guid.Parse(e.CommandArgument.ToString()),
                    Value = txtAlias.Text,
                    Status = "ACTIVE",
                    Sequence = Convert.ToInt32(txtAliasSequence.Text)
                };
                UpdateGridView(newAlias);
                fillkeywordalias();
            }
            else if (e.CommandName == "UnDelete")
            {
                MDMSVC.DC_keyword_alias newAlias = new MDMSVC.DC_keyword_alias
                {
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Keyword_Id = Guid.Parse(hdnKeywordId.Value),
                    KeywordAlias_Id = Guid.Parse(e.CommandArgument.ToString()),
                    Status = "ACTIVE"
                };
                UpdateGridView(newAlias);
                fillkeywordalias();
            }
        }

        protected void grdAlias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            AliasPageNo = e.NewPageIndex;
            fillkeywordalias();
        }

        protected void grdAlias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            MDMSVC.DC_keyword_alias newAlias = new MDMSVC.DC_keyword_alias
            {
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                Edit_Date = DateTime.Now,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                Keyword_Id = Guid.Parse(hdnKeywordId.Value),
                KeywordAlias_Id = Guid.Parse(grdAlias.DataKeys[e.RowIndex].Value.ToString()),
                Status = "INACTIVE",
            };
            UpdateGridView(newAlias);
            fillkeywordalias();
        }

        protected void grdAlias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            grdAlias.EditIndex = -1;
            fillkeywordalias();
        }

        protected void grdAlias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtAlias = (TextBox)grdAlias.Rows[e.RowIndex].FindControl("txtAlias");
            TextBox txtAliasSequence = (TextBox)grdAlias.Rows[e.RowIndex].FindControl("txtAliasSequence");
            MDMSVC.DC_keyword_alias newAlias = new MDMSVC.DC_keyword_alias
            {
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                Edit_Date = DateTime.Now,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                Keyword_Id = Guid.Parse(hdnKeywordId.Value),
                KeywordAlias_Id = Guid.Parse(grdAlias.DataKeys[e.RowIndex].Value.ToString()),
                Value = txtAlias.Text,
                Status = "ACTIVE",
                Sequence = Convert.ToInt32(txtAliasSequence.Text)
            };
            UpdateGridView(newAlias);

            grdAlias.EditIndex = -1;
            fillkeywordalias();
        }

        protected void grdAlias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dvMsg.InnerText = string.Empty;
            dvMsg.Style.Add("display", "none");

            dvMsgAlias.InnerText = string.Empty;
            dvMsgAlias.Style.Add("display", "none");

            grdAlias.EditIndex = e.NewEditIndex;
            fillkeywordalias();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateGridView(null);
            //Guid Keyword_Id = Guid.Parse(hdnKeywordId.Value);

            //MDMSVC.DC_Keyword Keyword = new MDMSVC.DC_Keyword
            //{
            //    Attribute = chkNewKeywordAttribute.Checked,
            //    Create_Date = DateTime.Now,
            //    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
            //    Edit_Date = DateTime.Now,
            //    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
            //    Keyword = txtAddNewKeyword.Text,
            //    Keyword_Id = Keyword_Id,
            //    Sequence = Convert.ToInt32(txtKeywordSequence.Text),
            //    Status = "ACTIVE",
            //    Icon = ddlglyphiconForAttributes.SelectedIndex == 0 ? string.Empty : ddlglyphiconForAttributes.SelectedItem.Text,
            //    EntityFor = string.Join(",", chklistEntityFor.Items.Cast<ListItem>().Where(x => x.Selected).Select(s => s.Text).ToArray())
            //};

            //var result = mappingScv.AddUpdateKeyword(Keyword);

            //BootstrapAlert.BootstrapAlertMessage(dvMsgAlias, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
        }

        protected void gvSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void grdAlias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete != null)
                {
                    if (btnDelete.CommandName == "UnDelete")
                    {
                        e.Row.Font.Strikeout = true;
                    }
                }
            }
        }
    }
}