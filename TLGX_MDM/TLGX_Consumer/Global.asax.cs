using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using TLGX_Consumer;
using TLGX_Consumer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;

namespace TLGX_Consumer
{
    public class Global : HttpApplication
    {
        public static ApplicationDbContext context = Models.ApplicationDbContext.Create();
        public static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
        public static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        //public static SiteMapNode _root = null;
        //public static bool IsLogedIn = true;
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        protected void Session_Start(object sender, EventArgs e)
        {
            //Global.IsLogedIn = false;
            //Global._root = null;
            Session.Abandon();
             //Session["_root"] = null; 
             Session["IsLogedIn"] = true;

        }

        protected void Session_End(object sender, EventArgs e)
        {
            
        }

    }
}