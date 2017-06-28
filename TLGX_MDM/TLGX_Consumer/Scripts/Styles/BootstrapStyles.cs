using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Scripts.Styles
{
    public class BootstrapStyles
    {
        public void DisplayMessage(System.Web.UI.HtmlControls.HtmlGenericControl dvMsg, string strMessage, BootstrapAlertType MessageType)
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
            }
            dvMsg.Attributes.Add("class", style);
            dvMsg.InnerHtml = "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>" + MessageType + "!</strong> <span> " + strMessage;
            //"A file with the same name already exists.<br />Your file was saved as " + fileName;

        }
    }
}