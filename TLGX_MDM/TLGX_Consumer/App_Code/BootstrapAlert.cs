using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.App_Code
{
    public static class BootstrapAlert
    {
        public static void BootstrapAlertMessage(System.Web.UI.HtmlControls.HtmlGenericControl dvMsg, string strMessage, BootstrapAlertType MessageType)
        {
            dvMsg.Style.Add("display", "block");
            dvMsg.Attributes.Remove("class");
            string style = "";
            switch (MessageType)
            {
                case BootstrapAlertType.Plain:
                    style = "alert alert-info alert-dismissable";
                    break;
                case BootstrapAlertType.Success:
                    style = "alert alert-success alert-dismissable";
                    break;
                case BootstrapAlertType.Information:
                    style = "alert alert-info alert-dismissable";
                    break;
                case BootstrapAlertType.Warning:
                    style = "alert alert-warning alert-dismissable";
                    break;
                case BootstrapAlertType.Danger:
                    style = "alert alert-danger alert-dismissable";
                    break;
                case BootstrapAlertType.Primary:
                    style = "alert alert-info alert-dismissable";
                    break;
                case BootstrapAlertType.Duplicate:
                    style = "alert alert-warning alert-dismissable";
                    break;
            }
            dvMsg.Attributes.Add("class", style);
            dvMsg.InnerHtml = "";
            string divcontent = "";
            divcontent = "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>" + MessageType + "!</strong> <span> " + strMessage;
            string myScript = "\n<script type=\"text/javascript\">\n";
            myScript += "setTimeout(function () { $(\"#" + dvMsg.ClientID + "\").fadeTo(500, 0).slideUp(500) }, 3000);";
            myScript += "\n\n </script>";
            dvMsg.InnerHtml = divcontent;
            ScriptManager.RegisterClientScriptBlock(dvMsg.Page,dvMsg.Page.GetType(), DateTime.Today.Ticks.ToString(), myScript.ToString(),false);


            //"A file with the same name already exists.<br />Your file was saved as " + fileName;
        }
    }
}

public enum BootstrapAlertType
{

    Plain,

    Success,

    Information,

    Warning,

    Danger,

    Primary,

    Duplicate
}