using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ActivityDescription : System.Web.UI.UserControl
    {
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        Models.lookupAttributeDAL LookupAtrributes = new Models.lookupAttributeDAL();
        public static string AttributeOptionFor = "Descriptions";
        public Guid Activity_Flavour_Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                GetLookUpValues("Page", "");
            }
           
        }
        protected void GetDescriptionType()
        {

            DropDownList ddlDescriptionType = (DropDownList)frmDescription.FindControl("ddlDescriptionType");
            ddlDescriptionType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionType").MasterAttributeValues;
            ddlDescriptionType.DataTextField = "AttributeValue";
            ddlDescriptionType.DataValueField = "MasterAttributeValue_Id";
            ddlDescriptionType.DataBind();
        }

        protected void GetLookUpValues(String BindTime, String AttributeType)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            if (BindTime == "Page")
            {
                // only the first ddlDescriptionType is set, the other is handled on selectednidex changed
                DropDownList ddlDescriptionType = (DropDownList)frmDescription.FindControl("ddlDescriptionType");
                ddlDescriptionType.Items.Clear();
                ListItem itm = new ListItem("-Select-", "0");
                ddlDescriptionType.Items.Add(itm);
                itm = null;

                ddlDescriptionType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionType").MasterAttributeValues;
                ddlDescriptionType.DataTextField = "AttributeValue";
                ddlDescriptionType.DataValueField = "MasterAttributeValue_Id";
                ddlDescriptionType.DataBind();

                scriptMan.RegisterAsyncPostBackControl(ddlDescriptionType);
            }

            if (BindTime == "Form")
            {
                DropDownList ddlDescriptionType = (DropDownList)frmDescription.FindControl("ddlDescriptionType");
                ddlDescriptionType.Items.Clear();
                ListItem itm = new ListItem("-Select-", "0");
                ddlDescriptionType.Items.Add(itm);
                itm = null;

                ddlDescriptionType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionType").MasterAttributeValues;
                ddlDescriptionType.DataTextField = "AttributeValue";
                ddlDescriptionType.DataValueField = "MasterAttributeValue_Id";
                ddlDescriptionType.DataBind();

                scriptMan.RegisterAsyncPostBackControl(ddlDescriptionType);

                DropDownList ddlDescriptionSubType = (DropDownList)frmDescription.FindControl("ddlDescriptionSubType");
                ddlDescriptionSubType.Items.Clear();
                itm = new ListItem("-Select-", "0");
                ddlDescriptionSubType.Items.Add(itm);
                itm = null;

                //var result = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionSubType").MasterAttributeValues;

                //ddlDescriptionSubType.DataSource = (from r in result where r.ParentAttributeValue == DescriptionT select new { r.MasterAttributeValue_Id, r.AttributeValue }).ToList();
                //ddlDescriptionSubType.DataTextField = "AttributeValue";
                //ddlDescriptionSubType.DataValueField = "MasterAttributeValue_Id";
                //ddlDescriptionSubType.DataBind();

            }

        }

        protected void ddlDescriptionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;

            //DropDownList ddlDescriptionSubType = (DropDownList)frmDescription.FindControl("ddlDescriptionSubType");
            //ddlDescriptionSubType.Items.Clear();
            //ListItem itm = new ListItem("-Select-", "0");
            //ddlDescriptionSubType.Items.Add(itm);
            //itm = null;

            //var result = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "DescriptionSubType").MasterAttributeValues;

            //ddlDescriptionSubType.DataSource = (from r in result where r.ParentAttributeValue == ddl.SelectedItem.ToString() select new { r.MasterAttributeValue_Id, r.AttributeValue }).ToList();
            //ddlDescriptionSubType.DataTextField = "AttributeValue";
            //ddlDescriptionSubType.DataValueField = "MasterAttributeValue_Id";
            //ddlDescriptionSubType.DataBind();
        }

        protected void BindActDescription(int pagesize,int pageno)
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Descriptions_RQ RQ = new MDMSVC.DC_Activity_Descriptions_RQ();
            RQ.Activity_Flavour_Id = Activity_Flavour_Id;
            RQ.PageNo = pageno;
            RQ.PageSize = pagesize;
            var result = AccSvc.GetActivityDescription(RQ);
            if (result != null)
            {
                gvDescriptionSearch.DataSource = result;
                gvDescriptionSearch.DataBind();
                lblTotalRecords.Text = Convert.ToString(result[0].TotalRecords);
            }
            else
            {
                gvDescriptionSearch.DataSource = null;
                gvDescriptionSearch.DataBind();
            }
        }
        protected void btnNewUpload_Click(object sender, EventArgs e)
        {
            frmDescription.ChangeMode(FormViewMode.Insert);
            frmDescription.DataBind();
            GetLookUpValues("Page", "");
        }
       
        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void gvDescriptionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none");
                MDMSVC.DC_Activity_Descriptions_RQ RQ = new MDMSVC.DC_Activity_Descriptions_RQ();
                RQ.Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                RQ.Activity_Description_Id = myRow_Id;
                frmDescription.ChangeMode(FormViewMode.Edit);
                frmDescription.DataSource = AccSvc.GetActivityDescription(RQ);
                frmDescription.DataBind();
                MDMSVC.DC_Activity_Descriptions rowView = (MDMSVC.DC_Activity_Descriptions)frmDescription.DataItem;

                GetLookUpValues("Form", rowView.DescriptionType.ToString());

                DropDownList ddlDescriptionSubType = (DropDownList)frmDescription.FindControl("ddlDescriptionSubType");
                DropDownList ddlDescriptionType = (DropDownList)frmDescription.FindControl("ddlDescriptionType");

                if (ddlDescriptionSubType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlDescriptionSubType.SelectedIndex = ddlDescriptionSubType.Items.IndexOf(ddlDescriptionSubType.Items.FindByText(rowView.DescriptionSubType.ToString()));
                }

                if (ddlDescriptionType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlDescriptionType.SelectedIndex = ddlDescriptionType.Items.IndexOf(ddlDescriptionType.Items.FindByText(rowView.DescriptionType.ToString()));
                }
                TextBox txtFrom = (TextBox)frmDescription.FindControl("txtFrom");
                TextBox txtTo = (TextBox)frmDescription.FindControl("txtTo");
                txtFrom.Text = System.Web.HttpUtility.HtmlDecode(gvDescriptionSearch.Rows[index].Cells[0].Text);
                txtTo.Text = System.Web.HttpUtility.HtmlDecode(gvDescriptionSearch.Rows[index].Cells[1].Text);
            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Descriptions newObj = new MDMSVC.DC_Activity_Descriptions
                {

                    Activity_Description_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = AccSvc.AddUpdateActivityDescription(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Descriptions newObj = new MDMSVC.DC_Activity_Descriptions
                {
                    Activity_Description_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };
                var result = AccSvc.AddUpdateActivityDescription(newObj);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, result.StatusMessage, (BootstrapAlertType)result.StatusCode);
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);

            }
        }

        protected void gvDescriptionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                    btnSelect.Enabled = false;
                    btnSelect.Attributes.Remove("OnClientClick");
                }
                else
                {
                    e.Row.Font.Strikeout = false;
                    btnSelect.Enabled = true;
                    btnSelect.Attributes.Add("OnClientClick", "showDescriptionModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void gvDescriptionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }

        protected void frmDescription_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtName = (TextBox)frmDescription.FindControl("txtName");
            TextBox txtDescriptionFor = (TextBox)frmDescription.FindControl("txtDescriptionFor");
            DropDownList ddlDescriptionType = (DropDownList)frmDescription.FindControl("ddlDescriptionType");
            DropDownList ddlDescriptionSubType = (DropDownList)frmDescription.FindControl("ddlDescriptionSubType");
            TextBox txtFrom = (TextBox)frmDescription.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmDescription.FindControl("txtTo");
            TextBox txtDescription = (TextBox)frmDescription.FindControl("txtDescription");
            TextBox txtSource = (TextBox)frmDescription.FindControl("txtSource");
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            if (e.CommandName.ToString() == "Add")
            {
                MDMSVC.DC_Activity_Descriptions RQ = new MDMSVC.DC_Activity_Descriptions
                {
                    Activity_Description_Id = Guid.NewGuid(),
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    Description_Name = txtName.Text.Trim().TrimStart(),
                    DescriptionFor=txtDescriptionFor.Text.Trim().TrimStart(),
                    DescriptionType= ddlDescriptionType.SelectedItem.ToString(),
                    DescriptionSubType= ddlDescriptionSubType.SelectedItem.ToString(),
                    Description=txtDescription.Text.Trim().TrimStart(),
                    Source=txtSource.Text.Trim().TrimStart(),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    IsActive = true
                };
                var res = AccSvc.AddUpdateActivityDescription(RQ);
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                hdnFlag.Value = "true";
            }

            if (e.CommandName.ToString() == "Modify")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                Guid myRow_Id = Guid.Parse(frmDescription.DataKey.Value.ToString());
                DC_Activity_Descriptions_RQ RQ = new DC_Activity_Descriptions_RQ();
                RQ.Activity_Description_Id = myRow_Id;
                RQ.Activity_Flavour_Id = Activity_Flavour_Id;
                var res = AccSvc.GetActivityDescription(RQ);
                if (res.Count > 0)
                {
                    TLGX_Consumer.MDMSVC.DC_Activity_Descriptions newObj = new MDMSVC.DC_Activity_Descriptions
                    {
                        Activity_Description_Id = myRow_Id,
                        Activity_Flavour_Id = Activity_Flavour_Id,
                        Legacy_Product_ID = AccSvc.GetLegacyProductId(Activity_Flavour_Id),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true,
                        DescriptionType = ddlDescriptionType.SelectedItem.ToString(),
                        DescriptionSubType = ddlDescriptionSubType.SelectedItem.ToString(),
                        Description_Name = txtName.Text.Trim().TrimStart(),
                        DescriptionFor = txtDescriptionFor.Text.Trim().TrimStart(),
                        Description = txtDescription.Text.Trim().TrimStart(),
                        FromDate = DateTime.ParseExact(txtFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        ToDate = DateTime.ParseExact(txtTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        Source = txtSource.Text.Trim().TrimStart(),
                    };
                    var res1 = AccSvc.AddUpdateActivityDescription(newObj);
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, res1.StatusMessage, (BootstrapAlertType)res1.StatusCode);
                    BindActDescription(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
                }
                hdnFlag.Value = "true";
            };
        }
       
    }
}