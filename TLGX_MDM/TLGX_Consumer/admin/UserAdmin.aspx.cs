using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;
using System.Configuration;


namespace TLGX_Consumer.admin
{
    public partial class UserAdmin : System.Web.UI.Page
    {
       
        #region PageLoad
        protected void Page_Init(object sender, EventArgs e)
        {
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        #endregion
        
    }


}
