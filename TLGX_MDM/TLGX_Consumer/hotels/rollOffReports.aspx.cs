using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;
using TLGX_Consumer.App_Code;
using System.Configuration;
using System.Text;

namespace TLGX_Consumer.hotels
{
    public partial class rollOffReports : System.Web.UI.Page
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

        }

        protected void btnRuleCsv_Click(object sender, EventArgs e)
        {
            MDMSVC.DC_RollOFParams parm = new MDMSVC.DC_RollOFParams();
            parm.Fromdate = fromDate.Value.ToString();
            parm.ToDate = toDate.Value.ToString();
            MappingSVCs _objmapping = new MappingSVCs();
            var res = _objmapping.getStatisticforRuleReport(parm);

            if (res != null && res.Count > 0)
            {
                //Writeing CSV file
                StringBuilder sb = new StringBuilder();

                string csv = string.Empty;
                List<string> lstFileHeader = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Get_RuleReport"]).Split(',').ToList();

                foreach (var item in res[0].GetType().GetProperties())
                {
                    if (lstFileHeader.Contains(item.Name))
                        csv += item.Name + ',';
                }
                sb.Append(string.Format("{0}", csv) + Environment.NewLine);
                foreach (var item in res)
                {
                    sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6}", Convert.ToString(item.Hotelid), Convert.ToString(item.Hotelname), Convert.ToString(item.RuleName), Convert.ToString(item.Description), Convert.ToString(item.Internal_Flag), Convert.ToString(item.LastupdateDate), Convert.ToString(item.LastupdatedBy)));
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
                    string filename = "RollOff_RuleReport";
                    response.AddHeader("Content-disposition", "attachment; filename=\"" + filename + ".csv" + "\"");
                    response.BinaryWrite(bytes);
                    response.Flush();
                    response.End();
                }
            }
        }
    }
}