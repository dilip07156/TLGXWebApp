using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.admin
{
    public partial class Sitemap : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindSiteMap();
            }
        }

        private void BindSiteMap()
        {
            try
            {
                Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
                List<MDMSVC.DC_SiteMap> objSiteMap = AccSvc.GetSiteMapByUserRole(System.Web.HttpContext.Current.User.Identity.Name);
                objSiteMap = (from s in objSiteMap orderby s.ID select s).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}