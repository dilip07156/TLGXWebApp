using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;
using TLGX_Consumer.MDMSVC;
namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ClassificationAttributes : System.Web.UI.UserControl
    {
        public Guid Activity_Flavour_Id;
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "ClassificationAttributes";

        protected void GetLookUpValues(String BindTime, String AttributeType)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            if (BindTime == "Page")
            {
                // only the first ddlAttributeType is set, the other is handled on selectednidex changed
                DropDownList ddlAttributeType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeType");
                ddlAttributeType.Items.Clear();
                ListItem itm = new ListItem("-Select-", "0");
                ddlAttributeType.Items.Add(itm);
                itm = null;

                ddlAttributeType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "AttributeType").MasterAttributeValues;
                ddlAttributeType.DataTextField = "AttributeValue";
                ddlAttributeType.DataValueField = "MasterAttributeValue_Id";
                ddlAttributeType.DataBind();

                scriptMan.RegisterAsyncPostBackControl(ddlAttributeType);
            }

            if (BindTime == "Form")
            {
                DropDownList ddlAttributeType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeType");
                ddlAttributeType.Items.Clear();
                ListItem itm = new ListItem("-Select-", "0");
                ddlAttributeType.Items.Add(itm);
                itm = null;

                ddlAttributeType.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "AttributeType").MasterAttributeValues;
                ddlAttributeType.DataTextField = "AttributeValue";
                ddlAttributeType.DataValueField = "MasterAttributeValue_Id";
                ddlAttributeType.DataBind();

                scriptMan.RegisterAsyncPostBackControl(ddlAttributeType);

                DropDownList ddlAttributeSubType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeSubType");
                ddlAttributeSubType.Items.Clear();
                itm = new ListItem("-Select-", "0");
                ddlAttributeSubType.Items.Add(itm);
                itm = null;

                var result = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "AttributeSubType").MasterAttributeValues;

                ddlAttributeSubType.DataSource = (from r in result where r.ParentAttributeValue == AttributeType select new { r.MasterAttributeValue_Id, r.AttributeValue }).ToList();
                ddlAttributeSubType.DataTextField = "AttributeValue";
                ddlAttributeSubType.DataValueField = "MasterAttributeValue_Id";
                ddlAttributeSubType.DataBind();

            }

        }

        protected void GetClassificationAttributeDetails()
        {
            
            MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ = new DC_Activity_ClassificationAttributes_RQ();
            RQ.Activity_Flavour_Id  = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            var result = AccSvc.GetActivityClasificationAttributes(RQ);

            List<DC_ClassificationAttributes_Type> _newObj = new List<DC_ClassificationAttributes_Type>();
            foreach (string at in (from r in result select r.AttributeType).Distinct())
            {
                DC_ClassificationAttributes_Type type = new DC_ClassificationAttributes_Type();
                type.AttributeType = at;
                List<DC_ClassificationAttributes_SubType> lstSubType = new List<DC_ClassificationAttributes_SubType>();

                foreach (string st in (from r in result where r.AttributeType == at select r.AttributeSubType).Distinct())
                {
                    DC_ClassificationAttributes_SubType subtype = new DC_ClassificationAttributes_SubType();
                    subtype.SubAttributeType = st;
                    subtype.ListCA = (from r in result where r.AttributeType == at && r.AttributeSubType == st select r).ToList();

                    lstSubType.Add(subtype);
                }

                type.SubType = lstSubType;

                _newObj.Add(type);
            }

            repCAType.DataSource = _newObj;
            repCAType.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetClassificationAttributeDetails();
                GetLookUpValues("Page", "");

            }
        }

        protected void ddlAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl = (DropDownList)sender;

            DropDownList ddlAttributeSubType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeSubType");
            ddlAttributeSubType.Items.Clear();
            ListItem itm = new ListItem("-Select-", "0");
            ddlAttributeSubType.Items.Add(itm);
            itm = null;

            var result = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, "AttributeSubType").MasterAttributeValues;

            ddlAttributeSubType.DataSource = (from r in result where r.ParentAttributeValue == ddl.SelectedItem.ToString() select new { r.MasterAttributeValue_Id, r.AttributeValue }).ToList();
            ddlAttributeSubType.DataTextField = "AttributeValue";
            ddlAttributeSubType.DataValueField = "MasterAttributeValue_Id";
            ddlAttributeSubType.DataBind();

        }

        protected void frmClassificationAttribute_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

            DropDownList ddlAttributeSubType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeSubType");
            DropDownList ddlAttributeType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeType");
            TextBox txtAttributeValue = (TextBox)frmClassificationAttribute.FindControl("txtAttributeValue");
            CheckBox chkInternalOnly = (CheckBox)frmClassificationAttribute.FindControl("chkInternalOnly");


            if (e.CommandName.ToString() == "Add")
            {

                TLGX_Consumer.MDMSVC.DC_Activity_ClassificationAttributes newObj = new MDMSVC.DC_Activity_ClassificationAttributes
                {
                    Activity_ClassificationAttribute_Id = Guid.NewGuid(),
                    Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]),
                    
                    //ActivityId
                    CreateDate = DateTime.Now,
                    CreateUser = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                    AttributeType = ddlAttributeType.SelectedItem.ToString(),
                    AttributeSubType = ddlAttributeSubType.SelectedItem.ToString(),
                    AttributeValue = txtAttributeValue.Text.Trim(),
                    InternalOnly = chkInternalOnly.Checked
                };
                
                if (AccSvc.AddActivityClassifiationAttributes(newObj))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "javascript:closeClassificationAttributesModal();", true);
                    GetClassificationAttributeDetails();
                    frmClassificationAttribute.DataBind();
                    frmClassificationAttribute.ChangeMode(FormViewMode.Insert);
                    GetLookUpValues("Page", "");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Attribute has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                hdnFlag.Value = "true";
            }


            if (e.CommandName.ToString() == "Modify")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                Guid myRow_Id = Guid.Parse(frmClassificationAttribute.DataKey.Value.ToString());

                TLGX_Consumer.MDMSVC.DC_Activity_ClassificationAttributes newObj = new MDMSVC.DC_Activity_ClassificationAttributes
                {
                    Activity_ClassificationAttribute_Id = myRow_Id,
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    EditDate = DateTime.Now,
                    EditUser = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true,
                    AttributeType = ddlAttributeType.SelectedItem.ToString(),
                    AttributeSubType = ddlAttributeSubType.SelectedItem.ToString(),
                    AttributeValue = txtAttributeValue.Text.Trim(),
                    InternalOnly = chkInternalOnly.Checked
                };

                if (AccSvc.UpdateActivityClassifiationAttributes(newObj))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "javascript:closeClassificationAttributesModal();", true);

                    Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

                    frmClassificationAttribute.ChangeMode(FormViewMode.Insert);
                    GetClassificationAttributeDetails();
                    frmClassificationAttribute.DataBind();
                    GetLookUpValues("Page", "");
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Attribute has been updated successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);

                hdnFlag.Value = "true";
            };
        }

        protected void grdClassificationAttributes_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    btnSelect.Attributes.Add("OnClientClick", "showClassificationAttributesModal();");
                }
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                scriptMan.RegisterAsyncPostBackControl(btnDelete);

            }
        }

        protected void grdClassificationAttributes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //int index = Convert.ToInt32(e.CommandArgument);

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {
                dvMsg.Style.Add("display", "none"); 
                MDMSVC.DC_Activity_ClassificationAttributes_RQ RQ = new DC_Activity_ClassificationAttributes_RQ();
                RQ.Activity_Flavour_Id = Guid.Parse(Request.QueryString["Activity_Flavour_Id"]);
                frmClassificationAttribute.ChangeMode(FormViewMode.Edit);
                frmClassificationAttribute.DataSource = AccSvc.GetActivityClasificationAttributes(RQ);
                frmClassificationAttribute.DataBind();

                MDMSVC.DC_Activity_ClassificationAttributes rowView = (MDMSVC.DC_Activity_ClassificationAttributes)frmClassificationAttribute.DataItem;

                GetLookUpValues("Form", rowView.AttributeType.ToString());

                DropDownList ddlAttributeSubType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeSubType");
                DropDownList ddlAttributeType = (DropDownList)frmClassificationAttribute.FindControl("ddlAttributeType");

                if (ddlAttributeSubType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlAttributeSubType.SelectedIndex = ddlAttributeSubType.Items.IndexOf(ddlAttributeSubType.Items.FindByText(rowView.AttributeSubType.ToString()));
                }

                if (ddlAttributeType.Items.Count > 1) // needs to be 1 to handle the "Select" value
                {
                    ddlAttributeType.SelectedIndex = ddlAttributeType.Items.IndexOf(ddlAttributeType.Items.FindByText(rowView.AttributeType.ToString()));
                }
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_ClassificationAttributes newObj = new MDMSVC.DC_Activity_ClassificationAttributes
                {

                    Activity_ClassificationAttribute_Id = myRow_Id,
                    IsActive = false,
                    EditDate = DateTime.Now,
                    EditUser = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateActivityClassifiationAttributes(newObj))
                {
                    GetClassificationAttributeDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Attribute has been deleted successfully", BootstrapAlertType.Success);
                };

            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_ClassificationAttributes newObj = new MDMSVC.DC_Activity_ClassificationAttributes
                {
                    Activity_ClassificationAttribute_Id = myRow_Id,
                    IsActive = true,
                    EditDate = DateTime.Now,
                    EditUser = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateActivityClassifiationAttributes(newObj))
                {
                    GetClassificationAttributeDetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Attribute has been retrived successfully", BootstrapAlertType.Success);
                };

            }

        }

        protected void btnAddNewAttribute_Click(object sender, EventArgs e)
        {
            frmClassificationAttribute.ChangeMode(FormViewMode.Insert);
            frmClassificationAttribute.DataBind();
            GetLookUpValues("Page", "");
        }
    }

    public class DC_ClassificationAttributes_Type
    {
        string _AttributeType;
        List<DC_ClassificationAttributes_SubType> _SubType;

        public string AttributeType
        {
            get
            {
                return _AttributeType;
            }

            set
            {
                _AttributeType = value;
            }
        }

        public List<DC_ClassificationAttributes_SubType> SubType
        {
            get
            {
                return _SubType;
            }

            set
            {
                _SubType = value;
            }
        }
    }

    public class DC_ClassificationAttributes_SubType
    {
        string _SubAttributeType;
        List<MDMSVC.DC_Activity_ClassificationAttributes> _ListCA;

        public string SubAttributeType
        {
            get
            {
                return _SubAttributeType;
            }

            set
            {
                _SubAttributeType = value;
            }
        }

        public List<DC_Activity_ClassificationAttributes> ListCA
        {
            get
            {
                return _ListCA;
            }

            set
            {
                _ListCA = value;
            }
        }
    }
}