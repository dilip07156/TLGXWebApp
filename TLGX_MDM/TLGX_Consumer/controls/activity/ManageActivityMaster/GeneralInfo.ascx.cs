using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityMaster
{
    public partial class GeneralInfo : System.Web.UI.UserControl
    {
        //Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        //public Guid Activity_Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetAllHotelAttributes();
            }
        }

        //protected void GetAllHotelAttributes()
        //{
        //    Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
        //    grdDynamicAttributeList.DataSource = ActSVC.GetHotelDynamicAttributes(Activity_Id, Activity_Id, Guid.Empty);
        //    grdDynamicAttributeList.DataBind();
        //}

        //protected void frmDynamicAttributeDetail_ItemCommand(object sender, FormViewCommandEventArgs e)
        //{
        //    TextBox txtAttributeName = (TextBox)frmDynamicAttributeDetail.FindControl("txtAttributeName");
        //    TextBox txtAttributeDescription = (TextBox)frmDynamicAttributeDetail.FindControl("txtAttributeDescription");
        //    //dvMsg.Style.Add("display", "none");
        //    if (e.CommandName.ToString() == "Add")
        //    {
        //        TLGX_Consumer.MDMSVC.DC_DynamicAttributes newObj = new MDMSVC.DC_DynamicAttributes()
        //        {
        //            DynamicAttribute_Id = Guid.NewGuid(),
        //            Object_Id = new Guid(Request.QueryString["Hotel_Id"]),
        //            ObjectSubElement_Id = new Guid(Request.QueryString["Hotel_Id"]),
        //            AttributeName = txtAttributeName.Text,
        //            AttributeValue = txtAttributeDescription.Text,
        //            ObjectType = "Hotel",
        //            AttributeClass = "HotelProperty",
        //            IsActive = true,
        //            Create_Date = DateTime.Now,
        //            Create_User = System.Web.HttpContext.Current.User.Identity.Name

        //        };

        //        if (ActSVC.AddHotelDynamicAttributes(newObj))
        //        {
        //            frmDynamicAttributeDetail.ChangeMode(FormViewMode.Insert);
        //            frmDynamicAttributeDetail.DataBind();
        //            GetAllHotelAttributes();
        //            BootstrapAlert.BootstrapAlertMessage(dvMsgDynamicAttributesForHotel, "Dynamic Attributes has been added successfully", BootstrapAlertType.Success);
        //        }
        //        else
        //        {
        //            BootstrapAlert.BootstrapAlertMessage(dvMsgDynamicAttributesForHotel, "Error Occurred", BootstrapAlertType.Warning);
        //        }
        //    }


        //    if (e.CommandName.ToString() == "Save")
        //    {
        //        Activity_Id = new Guid(Request.QueryString["Hotel_Id"]);
        //        Guid myRow_Id = Guid.Parse(grdDynamicAttributeList.SelectedDataKey.Value.ToString());

        //        var result = ActSVC.GetHotelDynamicAttributes(Activity_Id, Activity_Id, myRow_Id);

        //        if (result.Count > 0)
        //        {
        //            TLGX_Consumer.MDMSVC.DC_DynamicAttributes newObj = new MDMSVC.DC_DynamicAttributes
        //            {
        //                DynamicAttribute_Id = myRow_Id,
        //                Object_Id = Activity_Id,
        //                AttributeName = txtAttributeName.Text,
        //                AttributeValue = txtAttributeDescription.Text,
        //                ObjectType = result[0].ObjectType,
        //                AttributeClass = result[0].AttributeClass,
        //                ObjectSubElement_Id = result[0].ObjectSubElement_Id,
        //                IsActive = true,
        //                Edit_Date = DateTime.Now,
        //                Edit_User = System.Web.HttpContext.Current.User.Identity.Name
        //            };

        //            if (ActSVC.UpdateHotelDynamicAttributes(newObj))
        //            {
        //                frmDynamicAttributeDetail.ChangeMode(FormViewMode.Insert);
        //                frmDynamicAttributeDetail.DataBind();
        //                GetAllHotelAttributes();
        //                BootstrapAlert.BootstrapAlertMessage(dvMsgDynamicAttributesForHotel, "Dynamic Attributes has been updated successfully", BootstrapAlertType.Success);
        //            }
        //            else
        //            {
        //                BootstrapAlert.BootstrapAlertMessage(dvMsgDynamicAttributesForHotel, "Error Occurred", BootstrapAlertType.Warning);
        //            }
        //        }
        //    };

        //}

        //protected void grdDynamicAttributeList_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //dvMsg.Style.Add("display", "none");
        //    Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
        //    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //    int index = row.RowIndex;

        //    if (e.CommandName == "Select")
        //    {
        //        Activity_Id = new Guid(Request.QueryString["Hotel_Id"]);

        //        List<MDMSVC.DC_DynamicAttributes> obj = new List<MDMSVC.DC_DynamicAttributes>();
        //        obj.Add(new MDMSVC.DC_DynamicAttributes
        //        {
        //            DynamicAttribute_Id = myRow_Id,
        //            Object_Id = Activity_Id,
        //            ObjectSubElement_Id = Activity_Id,
        //            AttributeName = grdDynamicAttributeList.Rows[index].Cells[0].Text,
        //            AttributeValue = grdDynamicAttributeList.Rows[index].Cells[1].Text,
        //            ObjectType = "Hotel",
        //            AttributeClass = "HotelProperty"
        //        });

        //        frmDynamicAttributeDetail.ChangeMode(FormViewMode.Edit);

        //        frmDynamicAttributeDetail.DataSource = obj;
        //        frmDynamicAttributeDetail.DataBind();

        //        TextBox txtAttributeName = (TextBox)frmDynamicAttributeDetail.FindControl("txtAttributeName");
        //        TextBox txtAttributeDescription = (TextBox)frmDynamicAttributeDetail.FindControl("txtAttributeDescription");

        //        txtAttributeName.Text = System.Web.HttpUtility.HtmlDecode(grdDynamicAttributeList.Rows[index].Cells[0].Text);
        //        txtAttributeDescription.Text = System.Web.HttpUtility.HtmlDecode(grdDynamicAttributeList.Rows[index].Cells[1].Text);

        //    }

        //    else if (e.CommandName.ToString() == "SoftDelete")
        //    {
        //        TLGX_Consumer.MDMSVC.DC_DynamicAttributes newObj = new MDMSVC.DC_DynamicAttributes
        //        {
        //            DynamicAttribute_Id = myRow_Id,
        //            IsActive = false,
        //            Edit_Date = DateTime.Now,
        //            Edit_User = System.Web.HttpContext.Current.User.Identity.Name
        //        };

        //        if (ActSVC.UpdateHotelDynamicAttributes(newObj))
        //        {
        //            BootstrapAlert.BootstrapAlertMessage(dvMsgDynamicAttributesForHotel, "Dynamic Attributes has been deleted successfully", BootstrapAlertType.Success);
        //            GetAllHotelAttributes();
        //        };
        //    }

        //    else if (e.CommandName.ToString() == "UnDelete")
        //    {
        //        TLGX_Consumer.MDMSVC.DC_DynamicAttributes newObj = new MDMSVC.DC_DynamicAttributes
        //        {
        //            DynamicAttribute_Id = myRow_Id,
        //            IsActive = true,
        //            Edit_Date = DateTime.Now,
        //            Edit_User = System.Web.HttpContext.Current.User.Identity.Name
        //        };

        //        if (ActSVC.UpdateHotelDynamicAttributes(newObj))
        //        {
        //            BootstrapAlert.BootstrapAlertMessage(dvMsgDynamicAttributesForHotel, "Dynamic Attributes has been retrieved successfully", BootstrapAlertType.Success);
        //            GetAllHotelAttributes();
        //        };

        //    }
        //}

        //protected void grdDynamicAttributeList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.DataItem != null)
        //    {
        //        LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
        //        if (btnDelete.CommandName == "UnDelete")
        //        {
        //            e.Row.Font.Strikeout = true;
        //        }
        //    }
        //}
    }
}