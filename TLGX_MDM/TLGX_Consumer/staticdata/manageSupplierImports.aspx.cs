using System;
using System.Web;
using System.Web.UI;
using TLGX_Consumer.App_Code;
using System.Configuration;
using TLGX_Consumer.Controller;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace TLGX_Consumer.staticdata
{
    public partial class manageSupplierImports : System.Web.UI.Page
    {

        Models.MasterDataDAL objMasterDataDAL = new Models.MasterDataDAL();
        MasterDataSVCs _objMasterSVC = new MasterDataSVCs();


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
            if (!IsPostBack)
            {
                fillsuppliers();

            }
        }



        private void fillsuppliers()
        {
            //ddlSupplierName.DataSource = objMasterDataDAL.GetSupplierMasterData();
            ddlSupplierName.DataSource = _objMasterSVC.GetSupplierMasterData();
            ddlSupplierName.DataValueField = "Supplier_Id";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
             var SupplierId = ddlSupplierName.SelectedValue.ToString();
            if (SupplierId == "0")
            {
                SupplierId = "00000000-0000-0000-0000-000000000000";
            }
            MappingSVCs _objmapping = new MappingSVCs();
            var res = _objmapping.GetMappingStatistics(SupplierId);
            var res1 = _objmapping.GetMappingStatisticsForSuppliers();

            if (res != null && res.Count > 0)
            {
                //Writeing CSV file
                StringBuilder sb = new StringBuilder();

                string csv = string.Empty;
                List <string> lstFileHeader = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Mapping_Staitistics_Get"]).Split(',').ToList();

                foreach (var item in res[0].GetType().GetProperties())
                {
                    if (lstFileHeader.Contains(item.Name))
                        csv += item.Name + ',';
                }
                sb.Append(string.Format("{0}", csv) + Environment.NewLine);
                foreach (var item in res)
                {
                    sb.Append(string.Format("{0},{1},{2}", Convert.ToString(item.SupplierId), Convert.ToString(item.SupplierName), Convert.ToString(item.MappingStatsFor)));
                    sb.Append(Environment.NewLine);
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());
                sb = null;
                if (bytes != null)
                {
                    //Download the CSV file.
                    var response = HttpContext.Current.Response;
                    response.Clear();
                    response.ContentType = "text/csv";
                    response.AddHeader("Content-Length", bytes.Length.ToString());
                    string filename = "Data";
                    response.AddHeader("Content-disposition", "attachment; filename=\"" + filename + ".csv" + "\"");
                    response.BinaryWrite(bytes);
                    response.Flush();
                    response.End();
                }
            }

        }
    }
}