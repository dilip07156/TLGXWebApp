using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using System.Configuration;



namespace TLGX_Consumer.geography
{
    public partial class City : System.Web.UI.Page
    {
       

        protected void btnRedirectToSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/geography/SearchCityMaster.aspx");
        }
    }
}