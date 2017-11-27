using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Configuration;
using System.Runtime.CompilerServices;
using System.Configuration.Provider;
using System.Security.Permissions;
using System.Data.Common;
using System.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using System.Drawing;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Caching;
using System.Web.SessionState;


namespace TLGX_Consumer
{
    [SqlClientPermission(SecurityAction.Demand, Unrestricted = true)]
    public class SqlSiteMapProvider : StaticSiteMapProvider
    {
        const string _errmsg1 = "Missing node ID";
        const string _errmsg2 = "Duplicate node ID";
        const string _errmsg3 = "Missing parent ID";
        const string _errmsg4 = "Invalid parent ID";
        const string _errmsg5 = "Empty or missing connectionStringName";
        const string _errmsg6 = "Missing connection string";
        const string _errmsg7 = "Empty connection string";
        const string _errmsg8 = "Error while building sitemap";

        string _connect;
        public bool _dependencyRequested = false;

        SiteMapNode _root;
        bool IsLogedIn = false;
        Dictionary<int, SiteMapNode> _nodes = new Dictionary<int, System.Web.SiteMapNode>(16);
        public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Assign the provider a default name if it doesn't have one
            if (string.IsNullOrEmpty(name))
            {
                name = "SqlSiteMapProvider";
            }

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "SQL site map provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _connect
            string connect = config["connectionStringName"];

            if (string.IsNullOrEmpty(connect))
            {
                throw new ProviderException(_errmsg5);
            }

            config.Remove("connectionStringName");

            if (WebConfigurationManager.ConnectionStrings[connect] == null)
            {
                throw new ProviderException(_errmsg6);
            }

            _connect = WebConfigurationManager.ConnectionStrings[connect].ConnectionString;

            if (string.IsNullOrEmpty(_connect))
            {
                throw new ProviderException(_errmsg7);
            }

            // In beta 2, SiteMapProvider processes the
            // securityTrimmingEnabled attribute but fails to remove it.
            // Remove it now so we can check for unrecognized
            // configuration attributes.

            if ((config["securityTrimmingEnabled"] != null))
            {
                config.Remove("securityTrimmingEnabled");
            }

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if ((!string.IsNullOrEmpty(attr)))
                {
                    throw new ProviderException("Unrecognized attribute: " + attr);
                }
            }
        }

        public override SiteMapNode BuildSiteMap()
        {
            //lock (this)
            //{
            // Return immediately if this method has been called before

            if (HttpContext.Current.Session["_root_" + System.Web.HttpContext.Current.User.Identity.Name] != null)
            {
                _root = (SiteMapNode)(HttpContext.Current.Session["_root_" + System.Web.HttpContext.Current.User.Identity.Name]);
            }
            if ((_root != null))
            {
                return _root;
            }
            try
            {
                this.Clear();
                Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
                List<MDMSVC.DC_SiteMap> objSiteMap = AccSvc.GetSiteMapByUserRole(System.Web.HttpContext.Current.User.Identity.Name);
                objSiteMap = (from s in objSiteMap orderby s.ID select s).ToList();
                if (objSiteMap != null)
                {
                    if (objSiteMap.Count > 0)
                    {
                        int iCounter = 1;
                        foreach (MDMSVC.DC_SiteMap SM in objSiteMap)
                        {
                            // Create the root SiteMapNode and add it to
                            // the site map
                            if (iCounter == 1)
                            {
                                _nodes.Clear();
                                _root = CreateSiteMapNode(SM);
                                AddNode(_root, null);
                            }
                            else
                            {
                                // Create another site map node and
                                // add it to the site map
                                SiteMapNode node = CreateSiteMapNode(SM);
                                AddNode(node, GetParentNode(SM));
                            }

                            iCounter++;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw new ProviderException(_errmsg8);
            }

            // Return the root SiteMapNode
            HttpContext.Current.Session["_root_" + System.Web.HttpContext.Current.User.Identity.Name] = _root;
            return _root;
            //}
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            if (HttpContext.Current.Session["IsLogedIn"] != null)
            {
                IsLogedIn = (bool)HttpContext.Current.Session["IsLogedIn"];
                if (HttpContext.Current.Session["_root_" + System.Web.HttpContext.Current.User.Identity.Name] == null)
                {
                    _root = null;
                }
            }
            else
            {
                _root = null;
            }

            if (IsLogedIn)
            {
                BuildSiteMap();
            }
            return _root;
        }

        // Helper methods

        private SiteMapNode CreateSiteMapNode(MDMSVC.DC_SiteMap SM)
        {
            //// Make sure the node ID is present
            //if (SM.ID == null)
            //{
            //    throw new ProviderException(_errmsg1);
            //}

            // Get the node ID from the DataReader
            int id = SM.ID;

            // Make sure the node ID is unique
            if (_nodes.ContainsKey(SM.ID))
            {
                throw new ProviderException(_errmsg2);
            }

            // Get title, URL, description, and roles from the DataReader
            string title = null;
            if (SM.Title == null)
            {
                title = null;
            }
            else
            {
                title = SM.Title.Trim();
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
            SiteMapNode node = new SiteMapNode(this, id.ToString(), url, title, description, rolelist, null, null, null);

            // Record the node in the _nodes dictionary
            _nodes.Add(id, node);

            // Return the node        
            return node;
        }

        private SiteMapNode GetParentNode(MDMSVC.DC_SiteMap SM)
        {
            // Make sure the parent ID is present
            if (SM.ParentID == null)
            {
                throw new ProviderException(_errmsg3);
            }

            // Get the parent ID from the DataReader
            int pid = int.Parse(SM.ParentID.ToString());

            // Make sure the parent ID is valid
            if ((!_nodes.ContainsKey(pid)))
            {
                throw new ProviderException(_errmsg4);
            }

            // Return the parent SiteMapNode
            return _nodes[pid];
        }

    }
}