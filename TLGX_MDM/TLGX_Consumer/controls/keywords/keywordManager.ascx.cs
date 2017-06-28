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

            List<MDMSVC.DC_Keyword> lstAlias = new List<MDMSVC.DC_Keyword>();
            Guid keywrd_Id = Guid.NewGuid();

            MDMSVC.DC_Keyword keywordObj = new MDMSVC.DC_Keyword
            {
                Keyword_Id = keywrd_Id,
                Keyword = txtAddKeyword.Text,
                Create_Date = DateTime.Now,
                Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                Status = "ACTIVE"
            };

            lstAlias.Add(keywordObj);
            MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
            dc = mappingScv.SaveKeyword(lstAlias);

            for (int i = 1; i <= count; i++)
            {
                string strId = "DynamicTxt" + i.ToString();
                string aliasValue = Request.Form[strId].ToString();

                MDMSVC.DC_Keyword aliasObj = new MDMSVC.DC_Keyword
                {
                    Keyword_Id = keywrd_Id,
                    AliasKeywordAlias_Id = Guid.NewGuid(),
                    AliasValue = aliasValue,
                    AliasStatus="ACTIVE",
                    AliasCreate_Date=DateTime.Now,
                    AliasCreate_User= System.Web.HttpContext.Current.User.Identity.Name
                };
                
                lstAlias.Add(aliasObj);
                MDMSVC.DC_Message dcAlias = new MDMSVC.DC_Message();
                dcAlias = mappingScv.SaveKeyword(lstAlias);
            }
            

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
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            TextBox txtAddKeyword = (TextBox)frmAddKeyword.FindControl("txtAddKeyword");
            HtmlTextArea txtAddAlias = (HtmlTextArea)frmAddKeyword.FindControl("txtAddAlias");
            frmAddKeyword.ChangeMode(FormViewMode.Edit);
            frmAddKeyword.DataBind();

            if (txtAddKeyword.Text != String.Empty || txtAddAlias.InnerText != String.Empty)
            {
                List<MDMSVC.DC_Keyword> lstobj = new List<MDMSVC.DC_Keyword>();
                MDMSVC.DC_Keyword obj = new MDMSVC.DC_Keyword
                {
                    Keyword = txtAddKeyword.Text,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Status = "ACTIVE",
                    AliasValue = txtAddAlias.InnerText,
                    AliasStatus = "ACTIVE",
                    AliasEdit_Date = DateTime.Now,
                    AliasEdit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                lstobj.Add(obj);
                MDMSVC.DC_Message dc = new MDMSVC.DC_Message();
                dc = mappingScv.UpdateKeyword(lstobj);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MDMSVC.DC_Keyword_RQ RQParam = new MDMSVC.DC_Keyword_RQ();
            if (txtKeyword.Text != String.Empty)
                RQParam.SystemWord = txtKeyword.Text;
            if (txtAlias.Text != String.Empty)
                RQParam.Alias = txtAlias.Text;
            if (ddlStatus.SelectedItem.Value != "0")
                RQParam.Status = ddlStatus.SelectedItem.Text;
            
            var result = mappingScv.SearchKeyword(RQParam);

            if (result != null)
            {
                gvSearchResult.DataSource = result;
                gvSearchResult.DataBind();
            }
            else
            {
                gvSearchResult.DataSource = null;
                gvSearchResult.DataBind();
            }
        }

        protected void gvSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName=="EditKeyWordMgmr")
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
                Guid myRow_Id=Guid.Parse(e.CommandArgument.ToString());

                var result = mappingScv.SearchKeyword(new MDMSVC.DC_Keyword_RQ() { AliasKeywordAlias_Id = myRow_Id });

                if (result!=null && result.Count > 0)
                {
                    txtAddKeyword.Text = Convert.ToString(result[0].Keyword);
                    txtAddAlias.InnerText = Convert.ToString(result[0].AliasValue);
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
    }
}