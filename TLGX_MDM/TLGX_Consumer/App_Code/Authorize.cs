using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.App_Code
{
    public class Authorize
    {
        AdminSVCs svc = new AdminSVCs();
        public bool IsRoleAuthorizedForUrl()
        {
            string strUserName = Convert.ToString(System.Web.HttpContext.Current.User.Identity.Name);
            if (string.IsNullOrWhiteSpace(strUserName))
                HttpContext.Current.Response.Redirect("/Account/login", true);
            string requestedUrl = HttpContext.Current.Request.Url.AbsolutePath;
            MDMSVC.DC_RoleAuthorizedForUrl RQ = new MDMSVC.DC_RoleAuthorizedForUrl();
            RQ.Url = "~" + requestedUrl;
            RQ.User = strUserName;
            bool blnIsAuthorized = svc.IsRoleAuthorizedForUrl(RQ);
            return blnIsAuthorized;
        }
    }
}