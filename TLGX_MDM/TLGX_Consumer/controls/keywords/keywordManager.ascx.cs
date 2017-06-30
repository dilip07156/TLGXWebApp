using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace TLGX_Consumer.controls.keywords
{
    public partial class keywordManager : System.Web.UI.UserControl
    {
        Controller.MasterDataSVCs masterscv = new Controller.MasterDataSVCs();
        Controller.MappingSVCs mappingScv = new Controller.MappingSVCs();
        public static int PageNo = 0;
        
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            TextBox txtAddKeyword = (TextBox)frmAddKeyword.FindControl("txtAddKeyword");
            HtmlInputText DynamicTextBox = (HtmlInputText)frmAddKeyword.FindControl("DynamicTextBox");
            int count = Convert.ToInt32(hdnFieldTotalTextboxes.Value);
            
            List<MDMSVC.DC_keyword_alias> lstAlias = new List<MDMSVC.DC_keyword_alias>();
            MDMSVC.DC_keyword_alias aliasObj = new MDMSVC.DC_keyword_alias
            {
                //Keyword_Id = keywrd_Id,
                Keyword = txtAddKeyword.Text,
                KeywordAlias_Id = Guid.NewGuid(),
                Value = DynamicTextBox.Value,
                Status = "ACTIVE",
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                Edit_Date = DateTime.Now,
                Edit_User = System.Web.HttpContext.Current.User.Identity.Name
            };
            lstAlias.Add(aliasObj);

            for (int i = 1; i <= count; i++)
            {
                string strId = "DynamicTxt" + i.ToString();
                string aliasValue = Request.Form[strId].ToString();

                MDMSVC.DC_keyword_alias aliasObjD = new MDMSVC.DC_keyword_alias
                {
                    //Keyword_Id = keywrd_Id,
                    Keyword = txtAddKeyword.Text,
                    KeywordAlias_Id = Guid.NewGuid(),
                    Value = aliasValue,
                    Status = "ACTIVE",
                    Create_Date=DateTime.Now,
                    Create_User=System.Web.HttpContext.Current.User.Identity.Name,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                lstAlias.Add(aliasObjD);
            }
            MDMSVC.DC_Message dcAlias = new MDMSVC.DC_Message();
            dcAlias = mappingScv.AddUpdateKeywordAlias(lstAlias);

            #region "Old Code"
            //frmAddKeyword.ChangeMode(FormViewMode.Insert);
            //frmAddKeyword.DataBind();
            //if (txtAddKeyword.Text != String.Empty || DynamicTextBox.Value != String.Empty)
            //{
            //    List<MDMSVC.DC_Keyword> lstobj = new List<MDMSVC.DC_Keyword>();
            //    MDMSVC.DC_Keyword obj = new MDMSVC.DC_Keyword

            //    {
            //Keyword_Id = Guid.NewGuid(),
            //        Keyword = txtAddKeyword.Text,
            //        Create_Date = DateTime.Now,
            //        Create_User = System.Web.HttpContext.Current.User.Identity.Name,
            //        Status = "ACTIVE",
            //        AliasKeywordAlias_Id = Guid.NewGuid(),
            //        //AliasValue = txtAddAlias.InnerText,
            //        AliasValue = DynamicTextBox.Value,
            //        AliasStatus = "ACTIVE",
            //        AliasCreate_Date = DateTime.Now,
            //        AliasCreate_User = System.Web.HttpContext.Current.User.Identity.Name
            //    };
            //    lstobj.Add(obj);
            //    MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
            //    dc = mappingScv.SaveKeyword(lstobj);
            //}
            #endregion
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            TextBox txtAddKeyword = (TextBox)frmAddKeyword.FindControl("txtAddKeyword");
            HtmlTextArea txtAddAlias = (HtmlTextArea)frmAddKeyword.FindControl("txtAddAlias");
            frmAddKeyword.ChangeMode(FormViewMode.Edit);
            frmAddKeyword.DataBind();

            if (txtAddKeyword.Text != String.Empty || txtAddAlias.InnerText != String.Empty)
            {
                List<MDMSVC.DC_keyword_alias> lstobj = new List<MDMSVC.DC_keyword_alias>();
                MDMSVC.DC_keyword_alias obj = new MDMSVC.DC_keyword_alias
                {
                    KeywordAlias_Id= Guid.Parse(hdnAliasId.Value),
                    Keyword = txtAddKeyword.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Status = "ACTIVE",
                    Value=txtAddAlias.InnerText,
                };
                lstobj.Add(obj);
                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                dc = mappingScv.AddUpdateKeywordAlias(lstobj);
            }
        }

        public void fillkeywordalias()
        {

            MDMSVC.DC_Keyword_RQ RQParam = new MDMSVC.DC_Keyword_RQ();
            if (txtKeyword.Text != String.Empty)
                RQParam.systemWord = txtKeyword.Text;
            if (txtAlias.Text != String.Empty)
                RQParam.Alias = txtAlias.Text;
            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlStatus.SelectedItem.Text;

            RQParam.PageNo = PageNo;
            RQParam.PageSize = Convert.ToInt32(ddlShowEntries.SelectedValue);
            var result = mappingScv.SearchKeywordAlias(RQParam);

            if (result != null && result.Count > 0)
            {
                lblTotalCount.Text = result[0].TotalRecords.ToString();
                gvSearchResult.DataSource = result;
                gvSearchResult.DataBind();
            }
            else
            {
                gvSearchResult.DataSource = null;
                gvSearchResult.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillkeywordalias();
        }

        protected void gvSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditKeyWordMgmr")
            {
                frmAddKeyword.ChangeMode(FormViewMode.Edit);
                List<MDMSVC.DC_Keyword_RQ> _blanckDatalst = new List<MDMSVC.DC_Keyword_RQ>();
                _blanckDatalst.Add(new MDMSVC.DC_Keyword_RQ() { });
                frmAddKeyword.DataSource = _blanckDatalst;
                frmAddKeyword.DataBind();
                TextBox txtAddKeyword = (TextBox)frmAddKeyword.FindControl("txtAddKeyword");
                HtmlTextArea txtAddAlias = (HtmlTextArea)frmAddKeyword.FindControl("txtAddAlias");
                //var txtAddAlias = Request.Form["Alias1"];
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                //Guid myRow_Id = Guid.Parse(gvSearchResult.DataKeys[index].Values[0].ToString());
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                hdnAliasId.Value = Convert.ToString(myRow_Id);

                var result = mappingScv.SearchKeywordAlias(new MDMSVC.DC_Keyword_RQ() { AliasKeywordAlias_Id = myRow_Id });

                if (result != null && result.Count > 0)
                {
                    txtAddKeyword.Text = Convert.ToString(result[0].Keyword);
                    txtAddAlias.InnerText = Convert.ToString(result[0].Value);
                }
            }

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddKeyword.ChangeMode(FormViewMode.Insert);
            frmAddKeyword.DataBind();
            Repeater RepeatTextbox = (Repeater)frmAddKeyword.FindControl("RepeatTextbox");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyword.Text = String.Empty;
            txtAlias.Text = String.Empty;
            ddlStatus.SelectedIndex = 0;
            gvSearchResult.DataSource = null;
            gvSearchResult.DataBind();
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillkeywordalias();
        }

        protected void gvSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageNo = e.NewPageIndex;
            fillkeywordalias();
        }
    }
}