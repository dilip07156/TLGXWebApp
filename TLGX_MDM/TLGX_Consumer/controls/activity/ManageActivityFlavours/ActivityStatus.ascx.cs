using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.controls.activity.ManageActivityFlavours
{
    public partial class ActivityStatus : System.Web.UI.UserControl
    {
        public Guid Activity_Flavour_Id;
        //public Guid Activity_Id;
        Controller.ActivitySVC AccSvc = new Controller.ActivitySVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                //Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                fillstatusdetails();
            }
        }
        private void fillstatusdetails()
        {
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            //Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
            grdStatusDetails.DataSource = AccSvc.GetActivityStatusDetails(Activity_Flavour_Id, Guid.Empty);
            grdStatusDetails.DataBind();
            frmStatusDetails.Visible = false;
        }
        protected void frmStatusDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtStatus = (TextBox)frmStatusDetails.FindControl("txtStatus");
            TextBox txtMarket = (TextBox)frmStatusDetails.FindControl("txtMarket");
            TextBox txtFrom = (TextBox)frmStatusDetails.FindControl("txtFrom");
            TextBox txtTo = (TextBox)frmStatusDetails.FindControl("txtTo");
            TextBox txtReason = (TextBox)frmStatusDetails.FindControl("txtReason");
            TextBox txtLegacyProductId = (TextBox)frmStatusDetails.FindControl("txtLegacyProductId");
            Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
            // Activity_Id = new Guid(Request.QueryString["Activity_Id"]);

            if (e.CommandName == "Add")
            {
                MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = Guid.NewGuid(),
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    //Activity_Id=Activity_Id,
                    Legacy_Product_ID = AccSvc.GetLegacyProductId(Activity_Flavour_Id),
                    Status = txtStatus.Text,
                    CompanyMarket = txtMarket.Text,
                    From = DateTime.Parse(txtFrom.Text.Trim()),
                    To = DateTime.Parse(txtTo.Text.Trim()),
                    DeactivationReason = txtReason.Text,
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true
                };
                if (AccSvc.AddActivityStatus(newObj))
                {
                    frmStatusDetails.DataBind();
                    fillstatusdetails();
                    BootstrapAlert.BootstrapAlertMessage(dvMsgStatus, "Status has been added successfully", BootstrapAlertType.Success);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgStatus, "Error Occurred", BootstrapAlertType.Warning);
                }

                btnAddFormView.Visible = true;
                frmStatusDetails.Visible = false;
            }
            else if (e.CommandName == "Select")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);
                // Activity_Id = new Guid(Request.QueryString["Activity_Id"]);
                Guid myRow_Id = Guid.Parse(grdStatusDetails.SelectedDataKey.Value.ToString());

                var result = AccSvc.GetActivityStatusDetails(Activity_Flavour_Id, myRow_Id);
                if (result.Count > 0)
                {
                    MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                    {
                        Activity_Status_Id = myRow_Id,
                        // Activity_Id= Activity_Id,
                        Activity_Flavour_Id = Activity_Flavour_Id,
                        Legacy_Product_ID = AccSvc.GetLegacyProductId(Activity_Flavour_Id),
                        Status = txtStatus.Text,
                        CompanyMarket = txtMarket.Text,
                        DeactivationReason = txtReason.Text,
                        From = DateTime.Parse(txtFrom.Text.Trim()),
                        To = DateTime.Parse(txtTo.Text.Trim()),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };
                    if (AccSvc.UpdateActivityStatus(newObj))
                    {
                        frmStatusDetails.ChangeMode(FormViewMode.Insert);
                        fillstatusdetails();
                        BootstrapAlert.BootstrapAlertMessage(dvMsgStatus, "Status has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                    {
                        BootstrapAlert.BootstrapAlertMessage(dvMsgStatus, "Error Occurred", BootstrapAlertType.Warning);
                    }
                    btnAddFormView.Visible = true;
                    frmStatusDetails.Visible = false;
                }
            }
        }
        protected void grdStatusDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            if (e.CommandName == "Select")
            {
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

                List<MDMSVC.DC_Activity_Status> obj = new List<MDMSVC.DC_Activity_Status>();
                obj.Add(new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = myRow_Id,
                    Activity_Flavour_Id = Activity_Flavour_Id,
                    CompanyMarket = grdStatusDetails.Rows[index].Cells[1].Text,
                    DeactivationReason = grdStatusDetails.Rows[index].Cells[2].Text,
                    Status = grdStatusDetails.Rows[index].Cells[0].Text,
                    From = Convert.ToDateTime(grdStatusDetails.Rows[index].Cells[3].Text),
                    To = Convert.ToDateTime(grdStatusDetails.Rows[index].Cells[4].Text),

                });
                if (!string.IsNullOrEmpty(grdStatusDetails.Rows[index].Cells[5].Text))
                {
                    obj[0].Legacy_Product_ID = Convert.ToInt32(grdStatusDetails.Rows[index].Cells[5].Text);
                }
                frmStatusDetails.ChangeMode(FormViewMode.Edit);
                frmStatusDetails.DataSource = obj;
                frmStatusDetails.DataBind();

                TextBox txtStatus = (TextBox)frmStatusDetails.FindControl("txtStatus");
                TextBox txtMarket = (TextBox)frmStatusDetails.FindControl("txtMarket");
                TextBox txtFrom = (TextBox)frmStatusDetails.FindControl("txtFrom");
                TextBox txtTo = (TextBox)frmStatusDetails.FindControl("txtTo");
                TextBox txtReason = (TextBox)frmStatusDetails.FindControl("txtReason");
                TextBox txtLegacyProductId = (TextBox)frmStatusDetails.FindControl("txtLegacyProductId");
                Activity_Flavour_Id = new Guid(Request.QueryString["Activity_Flavour_Id"]);

                txtStatus.Text = System.Web.HttpUtility.HtmlDecode(grdStatusDetails.Rows[index].Cells[0].Text);
                txtMarket.Text = System.Web.HttpUtility.HtmlDecode(grdStatusDetails.Rows[index].Cells[1].Text);
                txtReason.Text = System.Web.HttpUtility.HtmlDecode(grdStatusDetails.Rows[index].Cells[2].Text);
                txtFrom.Text = System.Web.HttpUtility.HtmlDecode(grdStatusDetails.Rows[index].Cells[3].Text);
                txtTo.Text = System.Web.HttpUtility.HtmlDecode(grdStatusDetails.Rows[index].Cells[4].Text);
                txtLegacyProductId.Text = System.Web.HttpUtility.HtmlDecode(grdStatusDetails.Rows[index].Cells[5].Text);

            }
            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateActivityStatus(newObj))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgStatus, "Status has been deleted successfully", BootstrapAlertType.Success);
                    fillstatusdetails();
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Activity_Status newObj = new MDMSVC.DC_Activity_Status
                {
                    Activity_Status_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateActivityStatus(newObj))
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsgStatus, "Status has been retrieved successfully", BootstrapAlertType.Success);
                    fillstatusdetails();
                };

            }

        }
        protected void grdStatusDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnAddFormView_Click(object sender, EventArgs e)
        {
            frmStatusDetails.ChangeMode(FormViewMode.Insert);
            frmStatusDetails.Visible = true;
            btnAddFormView.Visible = false;
        }
    }
}