using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.keywords
{
    public partial class keywordManager : System.Web.UI.UserControl
    {
        Controller.MasterDataSVCs masterscv = new Controller.MasterDataSVCs();
        Controller.MappingSVCs mappingScv = new Controller.MappingSVCs();
        public static int PageNo = 0;
        public static int AliasPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillddlstatus();
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
                //gvSearchResult.Columns[5].ItemStyle.Width = new Unit(50, UnitType.Percentage);
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
            }
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

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                txtAddNewKeyword.Text = row.Cells[0].Text;
                chkNewKeywordAttribute.Checked = Convert.ToBoolean(row.Cells[1].Text);
                txtKeywordSequence.Text = row.Cells[2].Text;

                AliasPageNo = 0;
                ddlShowEntriesAlias.SelectedIndex = 0;
                
                Label lblIconText = (Label)row.FindControl("lblIconText");
                if (lblIconText != null)
                {
                    spanglyphicon.Attributes.Add("class", "glyphicon glyphicon-" + lblIconText.Text);
                    fillGlyphiconForAttributes(Convert.ToString(lblIconText.Text));
                }
                else
                {
                    fillGlyphiconForAttributes(string.Empty);
                }

                fillkeywordalias();
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
                fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);

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
                fillkeyword(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);

                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
            }
        }

        private void fillGlyphiconForAttributes(string strIcon)
        {
            try
            {
                MDMSVC.DC_MasterAttribute RQ = new MDMSVC.DC_MasterAttribute();
                RQ.MasterFor = "Icons";
                RQ.Name = "GlyphIcons";
                var res = masterscv.GetAllAttributeAndValues(RQ);

                //foreach (var item in res)
                //{
                //    ListItem lstitem = new ListItem();
                //    lstitem.Value = Convert.ToString(item.MasterAttributeValue_Id);
                //    //  lstitem.Text = Server.HtmlDecode(@"<span class=""" + item.AttributeValue + @"""></span>") + Convert.ToString(item.AttributeValue).Split(' ')[1];
                //    lstitem.Text = Convert.ToString(item.AttributeValue);
                //    // lstitem.Attributes.Add("data-icon", Convert.ToString(item.AttributeValue).Split(' ')[1]);
                //    ddlglyphiconForAttributes.Items.Add(lstitem);
                //}

                ddlglyphiconForAttributes.DataSource = res;
                ddlglyphiconForAttributes.DataTextField = "AttributeValue";
                ddlglyphiconForAttributes.DataValueField = "MasterAttributeValue_Id";
                ddlglyphiconForAttributes.DataBind();

                ddlglyphiconForAttributes.Items.Insert(0, new ListItem("---ALL---", "0"));
                if (!String.IsNullOrEmpty(strIcon))
                {
                    if (ddlglyphiconForAttributes.Items.FindByText(strIcon) != null)
                        ddlglyphiconForAttributes.Items.FindByText(strIcon).Selected = true;
                }
            }
            catch (Exception)
            {

                throw;
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

            List<MDMSVC.DC_keyword_alias> aliasList = new List<MDMSVC.DC_keyword_alias>();
            aliasList.Add(Row);
            MDMSVC.DC_Keyword Keyword = new MDMSVC.DC_Keyword
            {
                Alias = aliasList.ToArray(),
                Attribute = chkAttribute.Checked,
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                Edit_Date = DateTime.Now,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                Keyword = txtAddNewKeyword.Text,
                Keyword_Id = Keyword_Id,
                Sequence = Convert.ToInt32(txtKeywordSequence.Text),
                Status = "ACTIVE"
            };

            var result = mappingScv.AddUpdateKeyword(Keyword);

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
            Guid Keyword_Id = Guid.Parse(hdnKeywordId.Value);

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
                Icon = ddlglyphiconForAttributes.SelectedItem.Text
            };

            var result = mappingScv.AddUpdateKeyword(Keyword);

            BootstrapAlert.BootstrapAlertMessage(dvMsgAlias, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
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