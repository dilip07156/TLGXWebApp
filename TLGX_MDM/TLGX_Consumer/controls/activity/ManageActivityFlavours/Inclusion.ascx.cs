using System;
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
                divDropdownForEntries.Visible = false;
            }
        }
        protected void gvActInclusionSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvActInclusionSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Editing")
            {
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;


                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                //Guid myRow_Id = Guid.Parse(gvActInclusionSearch.SelectedDataKey.Value.ToString());

                //bool isinclusion = false;

                //if (gvActInclusionSearch.Rows[index].Cells[4].Text == "true")
                //    isinclusion = true;

                //List<MDMSVC.DC_Activity_Inclusions> newObj = new List<MDMSVC.DC_Activity_Inclusions>();
                //newObj.Add(new MDMSVC.DC_Activity_Inclusions
                //{
                //    Activity_Inclusions_Id = myRow_Id,
                //    Activity_Flavour_Id = Activity_Flavour_Id,
                //    InclusionType = gvActInclusionSearch.Rows[index].Cells[0].Text,
                //    InclusionName = gvActInclusionSearch.Rows[index].Cells[1].Text,
                //    InclusionDescription = gvActInclusionSearch.Rows[index].Cells[3].Text,
                //    IsInclusion = isinclusion
                //});

                //if (!string.IsNullOrEmpty(gvActInclusionSearch.Rows[index].Cells[0].Text))
                //    newObj[0].InclusionType = gvActInclusionSearch.Rows[index].Cells[0].Text;



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