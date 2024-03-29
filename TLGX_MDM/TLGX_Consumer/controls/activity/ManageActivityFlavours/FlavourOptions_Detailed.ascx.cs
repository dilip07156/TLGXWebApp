﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class FlavourOptions_Detailed : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindActivityFlavourOptions();
            }
        }

        protected void BindActivityFlavourOptions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Flavour_Options_RQ _obj = new MDMSVC.DC_Activity_Flavour_Options_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;

            MDMSVC.DC_Activity_ClassificationAttributes_RQ _obj1 = new MDMSVC.DC_Activity_ClassificationAttributes_RQ();
            _obj1.Activity_Flavour_Id = Activity_Flavour_Id;
            _obj1.AttributeType = "ProductOption";
            _obj1.WithActivity_FlavourOptions_Id = false;

            var result = ActSVC.GetActivityFlavourOptions(_obj);

            ViewState["result"] = ActSVC.GetActivityClasificationAttributes(_obj1);



            if (result != null)
            {
                //List<MDMSVC.DC_Activity_Flavour_Options> res = new List<MDMSVC.DC_Activity_Flavour_Options>();
                //if (res != null || res.Count != 0)
                if (result != null && result.Count != 0)
                {
                    rptCustomers.DataSource = result;
                    rptCustomers.DataBind();

                    if (result.Count() > 0)
                    {
                        lblTotalRecords.Text = Convert.ToString(result[0].TotalRecords);
                    }
                }
                else
                {
                    rptCustomers.DataSource = null;
                    rptCustomers.DataBind();
                    divDropdownForEntries.Visible = false;
                }
            }
            else
            {
                rptCustomers.DataSource = null;
                rptCustomers.DataBind();
                divDropdownForEntries.Visible = false;
            }
        }

        protected void gvActFlavourOptins_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvActFlavourOptins_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvActFlavourOptins_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                Label lblId = e.Item.FindControl("lblId") as Label;
                GridView gvattribute = e.Item.FindControl("gvattribute") as GridView;
                List<MDMSVC.DC_Activity_ClassificationAttributes> obj = (List<MDMSVC.DC_Activity_ClassificationAttributes>)(((List<MDMSVC.DC_Activity_ClassificationAttributes>)ViewState["result"]).FindAll(x => x.Activity_FlavourOptions_Id == Guid.Parse(lblId.Text)));

                gvattribute.DataSource = obj;
                gvattribute.DataBind();
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gvattribute = e.Item.FindControl("gvattribute") as GridView;

                if (gvattribute.Rows.Count == 0)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), DateTime.Today.Ticks.ToString(), "hide('disable" + (e.Item.ItemIndex - 1).ToString() + "');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString("N"), "document.getElementById('disable" + (e.Item.ItemIndex - 1).ToString() + "').style.visibility = 'hidden !important';", false);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "hide('disable" + (e.Item.ItemIndex).ToString() + "');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), (e.Item.ItemIndex).ToString(), "hide('disable" + (e.Item.ItemIndex).ToString() + "');", true);


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "disable" + (e.Item.ItemIndex).ToString(), "hide('disable" + (e.Item.ItemIndex).ToString() + "');", true);


                }
            }
            //else
            //{
            //    GridView gvattribute = e.Item.FindControl("gvattribute") as GridView;
            //    gvattribute.DataSource = null;
            //    gvattribute.DataBind();
            //}
        }
    }
}