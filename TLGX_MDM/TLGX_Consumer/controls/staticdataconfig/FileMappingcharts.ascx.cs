using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.controls.staticdataconfig
{
    public partial class FileMappingcharts : System.Web.UI.UserControl
    {
        Controller.MappingSVCs mappingsvc = new Controller.MappingSVCs();
        Guid supplierimportfile_Id = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void getSupplierImportFileId()
        {
            HiddenField hiddenFileId = (HiddenField)this.FindControl("hiddenFileId");
            if (!string.IsNullOrEmpty(hiddenFileId.Value))
            {
                supplierimportfile_Id = Guid.Parse(hiddenFileId.Value);
            }
        }


        protected void btnStop_Click(object sender, EventArgs e)
        {
            divFileProgressStatus.InnerText = "";
            divFileProgressStatus.Visible = true;
            divFileProgressStatus.InnerText = "File Processing Status is Stopped";
            btnRestart.Enabled = true;
            btnStop.Enabled = false;
            btnResume.Enabled = false;
            btnPause.Enabled = false;
            getSupplierImportFileId();
            if (supplierimportfile_Id != Guid.Empty)
            {
                updateData(new DC_SupplierImportFileDetails { SupplierImportFile_Id = supplierimportfile_Id, IsStopped = true, IsRestarted = null, IsPaused = null, IsResumed = null });
            }
        }

        protected void btnRestart_Click(object sender, EventArgs e)
        {
            divFileProgressStatus.InnerText = "";
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnRestart.Enabled = false;
            btnResume.Enabled = false;
            getSupplierImportFileId();
            if (supplierimportfile_Id != Guid.Empty)
            {
                updateData(new DC_SupplierImportFileDetails { SupplierImportFile_Id = supplierimportfile_Id, IsStopped = null, IsRestarted = true, IsPaused = null, IsResumed = null });
            }
        }

        protected void btnPause_Click(object sender, EventArgs e)
        {
            divFileProgressStatus.InnerText = "";
            divFileProgressStatus.Visible = true;
            divFileProgressStatus.InnerText = "File Processing Status is Paused";
            btnResume.Enabled = true;
            btnStop.Enabled = false;
            btnRestart.Enabled = false;
            btnPause.Enabled = false;
            getSupplierImportFileId();
            if (supplierimportfile_Id != Guid.Empty)
            {
                updateData(new DC_SupplierImportFileDetails { SupplierImportFile_Id = supplierimportfile_Id, IsStopped = null, IsRestarted = null, IsPaused = true, IsResumed = null });
            }
        }

        protected void btnResume_Click(object sender, EventArgs e)
        {
            divFileProgressStatus.InnerText = "";
            btnResume.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            btnRestart.Enabled = false;

            divFileProgressStatus.InnerText = "";
            getSupplierImportFileId();
            if (supplierimportfile_Id != Guid.Empty)
            {
                updateData(new DC_SupplierImportFileDetails { SupplierImportFile_Id = supplierimportfile_Id, IsStopped = null, IsRestarted = null, IsPaused = null, IsResumed = true });
            }
        }

        protected void updateData(DC_SupplierImportFileDetails request)
        {


            MDMSVC.DC_SupplierImportFileDetails_RQ RQ = new MDMSVC.DC_SupplierImportFileDetails_RQ();
            if (request != null)
            {
                RQ.SupplierImportFile_Id = request.SupplierImportFile_Id;
                RQ.PageNo = 0;
                RQ.PageSize = int.MaxValue;
                var res = mappingsvc.GetSupplierStaticFileDetails(RQ);


                request.Supplier_Id = res[0].Supplier_Id;
                request.Supplier = res[0].Supplier;
                request.SavedFilePath = res[0].SavedFilePath;
                request.PROCESS_USER = System.Web.HttpContext.Current.User.Identity.Name;
                request.Entity = res[0].Entity;
                request.STATUS = res[0].STATUS;
                request.Mode = res[0].Mode;
                request.CurrentBatch = res[0].CurrentBatch;
            
                var result = mappingsvc.UpdateSupplierImportFileDeails(request);

            }
        }
    }
}