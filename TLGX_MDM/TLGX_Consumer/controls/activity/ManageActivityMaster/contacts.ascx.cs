using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.activity.ManageActivityMaster
{
    public partial class contacts : System.Web.UI.UserControl
    {
        Controller.ActivitySVC ActSVC = new Controller.ActivitySVC();
        public Guid Activity_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                fillcontactdetails();
            }
        }

        private void fillcontactdetails()
        {
            Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
            grdContactDetails.DataSource = ActSVC.GetActivityContactDetails(Activity_Id, Guid.Empty);
            grdContactDetails.DataBind();
        }

        protected void frmContactDetaiil_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            // dvMsg.Style.Add("display", "none");
            TextBox txtTelCountryCode = (TextBox)frmContactDetaiil.FindControl("txtTelCountryCode");
            TextBox txtTelCityCode = (TextBox)frmContactDetaiil.FindControl("txtTelCityCode");
            TextBox txtTelLocalNUmber = (TextBox)frmContactDetaiil.FindControl("txtTelLocalNUmber");
            TextBox txtFaxCountryCode = (TextBox)frmContactDetaiil.FindControl("txtFaxCountryCode");
            TextBox txtFaxCityCode = (TextBox)frmContactDetaiil.FindControl("txtFaxCityCode");
            TextBox txtFaxLocalNUmber = (TextBox)frmContactDetaiil.FindControl("txtFaxLocalNUmber");
            TextBox txtWebsite = (TextBox)frmContactDetaiil.FindControl("txtWebsite");
            TextBox txtEmail = (TextBox)frmContactDetaiil.FindControl("txtEmail");
            TextBox txtLegacyProductId = (TextBox)frmContactDetaiil.FindControl("txtLegacyProductId");
            Activity_Id = new Guid(Request.QueryString["Activity_Id"]);

            if (e.CommandName == "AddContact")
            {
                MDMSVC.DC_Activity_Contact newObj = new MDMSVC.DC_Activity_Contact
                {
                    Activity_Contact_Id = Guid.NewGuid(),
                    //Legacy_Product_ID = Convert.ToInt32(ActSVC.GetLegacyProductId(Activity_Id)),
                    Telephone = txtTelCountryCode.Text + "-" + txtTelCityCode.Text + "-" + txtTelLocalNUmber.Text,
                    Fax = txtFaxCountryCode.Text + "-" + txtFaxCityCode.Text + "-" + txtFaxLocalNUmber.Text,
                    Activity_Id = Activity_Id,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    Email = txtEmail.Text,
                    WebSiteURL = txtWebsite.Text,
                    IsActive = true
                };
                if (ActSVC.AddActivityContactsDetails(newObj))
                {
                    frmContactDetaiil.DataBind();
                    fillcontactdetails();
                    dvMsgContact.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsgContact, "Contact has been added successfully", BootstrapAlertType.Success);
                }
                else
                {
                    dvMsgContact.Visible = true;
                    BootstrapAlert.BootstrapAlertMessage(dvMsgContact, "Error Occurred", BootstrapAlertType.Warning);
                }
            }
            else if (e.CommandName == "UpdateContact")
            {
                Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                Guid myRow_Id = Guid.Parse(grdContactDetails.SelectedDataKey.Value.ToString());

                var result = ActSVC.GetActivityContactDetails(Activity_Id, myRow_Id);
                if (result.Count > 0)
                {
                    MDMSVC.DC_Activity_Contact newObj = new MDMSVC.DC_Activity_Contact
                    {
                        Activity_Contact_Id = myRow_Id,
                        Activity_Id = Activity_Id,
                        //Legacy_Product_ID = Convert.ToInt32(ActSVC.GetLegacyProductId(Activity_Id)),
                        Email = txtEmail.Text,
                        WebSiteURL = txtWebsite.Text,
                        Telephone = txtTelCountryCode.Text + "-" + txtTelCityCode.Text + "-" + txtTelLocalNUmber.Text,
                        Fax = txtFaxCountryCode.Text + "-" + txtFaxCityCode.Text + "-" + txtFaxLocalNUmber.Text,
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };
                    if (ActSVC.UpdateActivityContactDetails(newObj))
                    {
                        frmContactDetaiil.ChangeMode(FormViewMode.Insert);
                        fillcontactdetails();
                        dvMsgContact.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsgContact, "Contact has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                    {
                        dvMsgContact.Visible = true;
                        BootstrapAlert.BootstrapAlertMessage(dvMsgContact, "Error Occurred", BootstrapAlertType.Warning);
                    }

                }
            }
        }
        protected void grdContactDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // dvMsg.Style.Add("display", "none");

            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName == "Select")
            {
                Activity_Id = new Guid(Request.QueryString["Activity_Id"]);

                List<MDMSVC.DC_Activity_Contact> obj = new List<MDMSVC.DC_Activity_Contact>();
                obj.Add(new MDMSVC.DC_Activity_Contact
                {
                    Activity_Contact_Id = myRow_Id,
                    Activity_Id = Activity_Id,
                    Email = grdContactDetails.Rows[index].Cells[3].Text,
                    WebSiteURL = grdContactDetails.Rows[index].Cells[2].Text,
                    Telephone = grdContactDetails.Rows[index].Cells[0].Text,
                    Fax = grdContactDetails.Rows[index].Cells[1].Text
                });


                if (!string.IsNullOrEmpty(grdContactDetails.Rows[index].Cells[4].Text))
                {
                    obj[0].Legacy_Product_ID = Convert.ToInt32(grdContactDetails.Rows[index].Cells[4].Text);
                }
                frmContactDetaiil.ChangeMode(FormViewMode.Edit);
                frmContactDetaiil.DataSource = obj;
                frmContactDetaiil.DataBind();

                TextBox txtTelCountryCode = (TextBox)frmContactDetaiil.FindControl("txtTelCountryCode");
                TextBox txtTelCityCode = (TextBox)frmContactDetaiil.FindControl("txtTelCityCode");
                TextBox txtTelLocalNUmber = (TextBox)frmContactDetaiil.FindControl("txtTelLocalNUmber");
                TextBox txtFaxCountryCode = (TextBox)frmContactDetaiil.FindControl("txtFaxCountryCode");
                TextBox txtFaxCityCode = (TextBox)frmContactDetaiil.FindControl("txtFaxCityCode");
                TextBox txtFaxLocalNUmber = (TextBox)frmContactDetaiil.FindControl("txtFaxLocalNUmber");
                TextBox txtWebsite = (TextBox)frmContactDetaiil.FindControl("txtWebsite");
                TextBox txtEmail = (TextBox)frmContactDetaiil.FindControl("txtEmail");
                TextBox txtLegacyProductId = (TextBox)frmContactDetaiil.FindControl("txtLegacyProductId");

                string[] brkTelephone = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[0].Text).Split('-');
                string[] brkFax = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[1].Text).Split('-');
                if (brkTelephone.Length == 3)
                {
                    txtTelCountryCode.Text = brkTelephone[0];
                    txtTelCityCode.Text = brkTelephone[1];
                    txtTelLocalNUmber.Text = brkTelephone[2];
                }
                else
                {
                    txtTelCountryCode.Text = "";
                    txtTelCityCode.Text = "";
                    txtTelLocalNUmber.Text = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[0].Text);
                }
                if (brkFax.Length == 3)
                {
                    txtFaxCountryCode.Text = brkFax[0];
                    txtFaxCityCode.Text = brkFax[1];
                    txtFaxLocalNUmber.Text = brkFax[2];
                }
                else
                {
                    txtFaxCountryCode.Text = "";
                    txtFaxCityCode.Text = "";
                    txtFaxLocalNUmber.Text = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[1].Text);
                }
                txtWebsite.Text = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[2].Text);
                txtEmail.Text = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[3].Text);
                txtLegacyProductId.Text = System.Web.HttpUtility.HtmlDecode(grdContactDetails.Rows[index].Cells[4].Text);

                dvMsgContact.Visible = false;
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Contact newObj = new MDMSVC.DC_Activity_Contact
                {
                    Activity_Contact_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (ActSVC.UpdateActivityContactDetails(newObj))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgContact, "Contact has been deleted successfully", BootstrapAlertType.Success);
                    fillcontactdetails();
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Contact newObj = new MDMSVC.DC_Activity_Contact
                {
                    Activity_Contact_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (ActSVC.UpdateActivityContactDetails(newObj))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgContact, "Contact has been retrieved successfully", BootstrapAlertType.Success);
                    fillcontactdetails();
                };

            }
        }
        protected void grdContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }
    }
}