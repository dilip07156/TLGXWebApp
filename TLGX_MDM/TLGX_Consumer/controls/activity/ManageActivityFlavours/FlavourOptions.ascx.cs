using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class FlavourOptions : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Flavour_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindActivityFlavourOptions();
            }
        }

        protected void BindActivityFlavourOptions()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            MDMSVC.DC_Activity_Flavour_Options_RQ _obj = new MDMSVC.DC_Activity_Flavour_Options_RQ();
            _obj.Activity_Flavour_Id = Activity_Flavour_Id;
            var result = ActSVC.GetActivityFlavourOptions(_obj);
            if (result != null)
            {
                //List<MDMSVC.DC_Activity_Flavour_Options> res = new List<MDMSVC.DC_Activity_Flavour_Options>();
                //if (res != null || res.Count != 0)
                if (result != null || result.Count != 0)
                {
                    gvActFlavourOptins.DataSource = result;
                    gvActFlavourOptins.DataBind();
                    if (result.Count() > 0)
                    {
                        lblTotalRecords.Text = Convert.ToString(result[0].TotalRecords);
                    }
                }
                else
                {
                    gvActFlavourOptins.DataSource = null;
                    gvActFlavourOptins.DataBind();
                    divDropdownForEntries.Visible = false;
                }
            }
            else
            {
                gvActFlavourOptins.DataSource = null;
                gvActFlavourOptins.DataBind();
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
    }
}