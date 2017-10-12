using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Exclusion : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInclusions();
            }
        }
        protected void BindInclusions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Inclusions_RQ _obj = new MDMSVC.DC_Activity_Inclusions_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = ActSVC.GetActivityInclusions(_obj);
            List<MDMSVC.DC_Activity_Inclusions> res = new List<MDMSVC.DC_Activity_Inclusions>();
            if (res != null)
            {
                foreach(MDMSVC.DC_Activity_Inclusions rs in result)
                {
                    if(rs.IsInclusion!=true)
                    {
                        res.Add(rs);
                    }
                    
                }
                gvActInclusionSearch.DataSource = res;
                gvActInclusionSearch.DataBind();
            }
            else
            {
                gvActInclusionSearch.DataSource = null;
                gvActInclusionSearch.DataBind();
            }
        }
        protected void gvActInclusionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvActInclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        //protected void gvActInclusionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
            //if (e.Row.DataItem != null)
            //{
            //    // Retrieve the key value for the current row. Here it is an int.
            //    string Activity_Inclusions_Id = gvActInclusionSearch.DataKeys[e.Row.RowIndex].Values[0].ToString();// Convert.ToString(rowView["SupplierImportFile_Id"]);
            //    LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            //    if (btnEdit != null)
            //    {
            //        btnEdit.Attributes.Add("onclick", "showDetailsModal('" + Activity_Inclusions_Id + "');");
            //    }
            //    //LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            //    //if (btnDelete.CommandName == "UnDelete")
            //    //{
            //    //    e.Row.Font.Strikeout = true;
            //    //}
            //}
        //}
    }
}