using System;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Linq;
using Microsoft.AspNet.Identity.Owin;

using TLGX_Consumer.App_Code;
using System.IO;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {

            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;

        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<MDMSVC.DC_SiteMap> objSiteMap = GetData();
                PopulateMenu(objSiteMap);
            }
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //if (!IsPostBack)
            //{
            //    Session["Reset"] = true;
            //    Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            //    SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            //    Int32 timeout = (Int32)section.Timeout.TotalMinutes * 1000 * 60;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
            //}

        }

        private List<MDMSVC.DC_SiteMap> GetData()
        {
            try
            {
                Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
                if (String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                    Response.Redirect("/Account/Login",true);
                List<MDMSVC.DC_SiteMap> objSiteMap = AccSvc.GetSiteMapByUserRole(System.Web.HttpContext.Current.User.Identity.Name);
                objSiteMap = (from s in objSiteMap where s.ID != 1 orderby s.ID select s).ToList();
                return objSiteMap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateMenu(List<MDMSVC.DC_SiteMap> objSiteMap)
        {
            try
            {
                Menu SiteMenu = (Menu)LoginViewForSiteMap.FindControl("SiteMenu");
                if (objSiteMap != null && objSiteMap.Count > 0)
                {
                    int iCounter = 1;
                    foreach (var row in objSiteMap)
                    {
                        string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
                        if (iCounter == 1)
                        {
                            SiteMenu.Items.Add(CreateSiteMapNode(row, currentPage));
                        }
                        else
                        {
                            var parentMenu = SiteMenu.FindItem(Convert.ToString(row.ParentID));// GetParentMenu((from x in objSiteMap where x.ID == row.ParentID select x).FirstOrDefault());
                            if (parentMenu != null)
                                parentMenu.ChildItems.Add(CreateSiteMapNode(row, currentPage));
                            else
                                SiteMenu.Items.Add(CreateSiteMapNode(row, currentPage));
                        }
                        iCounter++;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private MenuItem CreateSiteMapNode(MDMSVC.DC_SiteMap SM, string currentPage)
        {
            // Get the node ID from the DataReader
            int id = SM.ID;

            // Make sure the node ID is unique
            //if (_nodes.ContainsKey(SM.ID))
            //{
            //    throw new ProviderException(_errmsg2);
            //}

            // Get title, URL, description, and roles from the DataReader
            string title = null;
            if (SM.Title == null)
            {
                title = null;
            }
            else
            {
                if (SM.ParentID != 1)
                    title = SM.Title.Trim();
                else
                    title = SM.Title.Trim() + "<b class='caret'></b>";
            }
            string url = null;
            if (SM.Url == null)
            {
                url = null;
            }
            else
            {
                url = SM.Url.Trim();
            }
            string description = null;
            if (SM.Description == null)
            {
                description = null;
            }
            else
            {
                description = SM.Description.Trim();
            }
            string roles = null;
            if (SM.Roles == null)
            {
                roles = null;
            }
            else
            {
                roles = SM.Roles.Trim();
            }

            // If roles were specified, turn the list into a string array
            string[] rolelist = null;
            if ((!string.IsNullOrEmpty(roles)))
            {
                rolelist = roles.Split(new char[] { ',', ';' }, 512);
            }

            // Create a SiteMapNode
            // SiteMapNode node = new SiteMapNode(this, id.ToString(), url, title, description, rolelist, null, null, null);
            MenuItem menuItem = new MenuItem
            {
                Value = id.ToString(),
                Text = title.ToString(),
                NavigateUrl = url.ToString(),
                Selected = url.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
            };


            // Record the node in the _nodes dictionary
            //_nodes.Add(id, node);

            // Return the node        
            return menuItem;
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {

            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon();
            //Global.IsLogedIn = false;
            //Global._root = null;
        }


        //protected void Unnamed_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        //{
        //    if (Session.IsNewSession)
        //        MasterScriptManager.AsyncPostBackErrorMessage = "Session Expired";
        //    else
        //        MasterScriptManager.AsyncPostBackErrorMessage = "Error Occured!!\n" + e.Exception.Message;
        //}
    }

}