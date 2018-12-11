using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.businessentities
{
    public partial class SupplierTaskLog : System.Web.UI.UserControl
    {
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();
        MasterDataSVCs _objMaster = new MasterDataSVCs();
        ScheduleDataSVCs _objScheduleDataSVCs = new ScheduleDataSVCs();
        string RedirectFromAlert = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                fillsuppliers();
                fillentities();
                if (Request.QueryString.Count > 1)
                {
                    RedirectFromAlert = Request.QueryString["RedirectFromAlert"].ToString();
                    
                }
                fillgriddata(RedirectFromAlert,0,0);
            }
        }

        private void fillsuppliers()
        {
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        private void fillentities()
        {
            var result = _objMaster.GetAllAttributeAndValues(new MDMSVC.DC_MasterAttribute() { MasterFor = "MappingFileConfig", Name = "MappingEntity" });
            if (result != null)
                if (result.Count > 0)
                {
                   
                    ddlEntity.DataSource = result;
                    ddlEntity.DataTextField = "AttributeValue";
                    ddlEntity.DataValueField = "MasterAttributeValue_Id";
                    ddlEntity.DataBind();

                }
        }

        protected void btnSearch_Click(object sender, EventArgs args)
        {
            fillgriddata(RedirectFromAlert,0,0);
        }

        private void fillgriddata(string RedirectFromAlert,int PageIndex,int PageSize)
        {
            Guid selSupplier_ID = Guid.Empty;
            Guid selCountry_ID = Guid.Empty;
            string strUserName = System.Web.HttpContext.Current.User.Identity.Name;
            MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
            RQ.UserName = System.Web.HttpContext.Current.User.Identity.Name;
            string Notification = Request.QueryString["Notification"].ToString();
            if (Notification != string.Empty)
                RQ.Notification = Notification;
                RQ.RedirectFrom = RedirectFromAlert;
            if (txtFrom.Text != string.Empty)

                RQ.FromDate = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", null); // DateTime.ParseExact(txtFrom.Text.Trim(), "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(txtFrom.Text.ToString("yyyy-MM-dd"));
            if (txtTo.Text != string.Empty)
                RQ.ToDate = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", null);// Convert.ToDateTime(txtTo.Text);
            if (ddlSupplierName.SelectedItem.Value != "0")
                    RQ.Supplier_Id = new Guid(ddlSupplierName.SelectedItem.Value);
                if (ddlEntity.SelectedItem.Value != "0")
                    RQ.Entity = ddlEntity.SelectedItem.Text;
                if (ddlstatus.SelectedItem.Value != "0")
                    RQ.Status = ddlstatus.SelectedValue;


                RQ.PageNo = PageIndex;
                RQ.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
                //RQ.SortBy = (SortBy + " " + SortEx).Trim();
          

            var res = _objScheduleDataSVCs.GetScheduleTaskByRoll(RQ);
            if (res != null)
            {
                if (res.Count > 0)
                {
                    grdSupplierScheduleTask.VirtualItemCount = res[0].TotalRecord;
                    lblTotalCount.Text = res[0].TotalRecord.ToString();
                }
                else
                    lblTotalCount.Text = "0";
            }
            else
                lblTotalCount.Text = "0";
            
            grdSupplierScheduleTask.DataSource = res;
            grdSupplierScheduleTask.PageIndex = PageIndex;
            grdSupplierScheduleTask.PageSize = Convert.ToInt32(ddlShowEntries.SelectedItem.Text);
            grdSupplierScheduleTask.DataBind();
        }

        protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);
        }

        protected void grdSupplierScheduleTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), e.NewPageIndex);
        }


        protected void grdSupplierScheduleTask_DataBound(object sender, EventArgs e)
        {
            var myGridView = (GridView)sender;
            
           
        }

        protected void grdSupplierScheduleTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var myGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                string logtype = string.Empty;
                if (myGridView.DataKeys[index].Values[1] != null)
                    logtype = myGridView.DataKeys[index].Values[1].ToString();

                string Status = string.Empty;
                if (myGridView.DataKeys[index].Values[2] != null)
                    Status = myGridView.DataKeys[index].Values[2].ToString();

                LinkButton btnupload = e.Row.FindControl("btnupload") as LinkButton;
                LinkButton btnTaskComp = e.Row.FindControl("btnTaskComp") as LinkButton;
                LinkButton btnDownload = e.Row.FindControl("btnDownload") as LinkButton;
                LinkButton btnDetail = e.Row.FindControl("btnDetail") as LinkButton;
                Label lbldownload = e.Row.FindControl("lbldownload") as Label;
                Label lblUpload = e.Row.FindControl("lblUpload") as Label;
                Label lblTask = e.Row.FindControl("lblTask") as Label;

                btnDownload.PostBackUrl = string.Format("~/suppliers/Manage?Supplier_Id={0}&DownloadInfo=info", myGridView.DataKeys[index].Values[0]);
                if (Status == "Completed")
                {
                    e.Row.CssClass = "alert alert-success";
                }
                else if (Status == "Pending")
                {
                    e.Row.CssClass = "alert alert-warning";
                }
                else if (Status == "Running")
                {
                    e.Row.CssClass = "alert alert-info";
                }
                else if (Status == "Error")
                {
                    e.Row.CssClass = "alert alert-danger";
                }

               

                if (logtype == "API" )
                {
                    
                    if (btnDownload != null)
                    {
                        btnDownload.Visible = false;
                    }

                    if (btnupload != null)
                    {
                        btnupload.Visible = false;
                    }
                    if (btnTaskComp != null)
                    {
                        btnTaskComp.Visible = false;
                    }
                    if (lbldownload != null)
                    {
                        lbldownload.Visible = true;
                    }
                    if (lblUpload !=null)
                    {
                        lblUpload.Visible = true;
                    }
                    if(lblTask !=null)
                    {
                        lblTask.Visible = true;
                    }
                    if (btnDetail != null)
                    {
                        btnDetail.Visible = true;
                    }

                }
                else if(logtype=="File" && Status != "Completed")
                {
                    if (btnDownload != null)
                    {
                        btnDownload.Visible = true;
                    }

                    if (btnupload != null)
                    {
                        btnupload.Visible = true;
                    }
                    if (btnTaskComp != null)
                    {
                        btnTaskComp.Visible = true;
                    }
                    if(btnDetail!=null)
                    {
                        btnDetail.Visible = false;
                    }
                    if (lbldownload != null)
                    {
                        lbldownload.Visible = false;
                    }
                    if (lblUpload != null)
                    {
                        lblUpload.Visible = false;
                    }
                    if (lblTask != null)
                    {
                        lblTask.Visible = false;
                    }
                }

                if (Status == "Completed" && logtype == "File" && btnupload != null && btnTaskComp != null && btnDownload != null && btnDetail!=null)
                {
                    DisableLinkButton(btnDownload);
                    DisableLinkButton(btnupload);
                    DisableLinkButton(btnTaskComp);
                    if (btnDetail != null)
                    {
                        btnDetail.Visible = false;
                    }
                }


            }

        }
                
        private void LoadDownloadData(int pagesize, int pageno)
        {
            fillgriddata(RedirectFromAlert,pageno,pagesize);
        }

        protected void grdSupplierScheduleTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName.ToString() == "TaskComplete")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                MDMSVC.DC_SupplierScheduledTaskRQ RQ = new MDMSVC.DC_SupplierScheduledTaskRQ();
                Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());
               
                RQ.Supplier_Id = (Guid)this.grdSupplierScheduleTask.DataKeys[index]["Suppllier_ID"];

                RQ.Entity = grdSupplierScheduleTask.Rows[index].Cells[1].Text;
                RQ.TaskId = myRow_Id;
                RQ.Edit_User= System.Web.HttpContext.Current.User.Identity.Name;
                RQ.Edit_Date = System.DateTime.Now;
                MDMSVC.DC_Message _objMsg=_objScheduleDataSVCs.UpdateTaskLog(RQ);
                if (_objMsg != null)
                    BootstrapAlert.BootstrapAlertMessage(msgAlert, "" + _objMsg.StatusMessage, (BootstrapAlertType)_objMsg.StatusCode);

                LoadDownloadData(Convert.ToInt32(ddlShowEntries.SelectedItem.Text), 0);


            }


        }

        protected void btnReset_Click(object sender, EventArgs args)
        {
            //fillgriddata("");
        }

        public static void DisableLinkButton(LinkButton linkButton)
        {
            linkButton.Attributes.Remove("href");
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
            if (linkButton.Enabled != false)
            {
                linkButton.Enabled = false;
            }

            if (linkButton.OnClientClick != null)
            {
                linkButton.OnClientClick = null;
            }
        }

    }
}