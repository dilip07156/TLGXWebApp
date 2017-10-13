﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class Inclusion : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInclusions();
                gvActInclusionDetails.DataSource = null;
                gvActInclusionDetails.DataBind();
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
                foreach (MDMSVC.DC_Activity_Inclusions rs in result)
                {
                    if (rs.IsInclusion == true)
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
            if (e.CommandName.ToString() == "Editing")
            {



                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                Guid myRow_Id = Guid.Parse(gvActInclusionSearch.SelectedDataKey.Value.ToString());

                MDMSVC.DC_Activity_InclusionDetails_RQ newObj = new MDMSVC.DC_Activity_InclusionDetails_RQ();
                newObj.Activity_Inclusion_Id = myRow_Id;

                var result = ActSVC.GetActivityInclusionDetails(newObj);
                if (result.Count > 0)
                {

                }
            }
        }

        protected void frmInclusionDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
        {

        }

        protected void gvActInclusionSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}