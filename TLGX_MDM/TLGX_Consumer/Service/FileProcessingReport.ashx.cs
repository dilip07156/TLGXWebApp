using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Script.Serialization;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.Data;
using TLGX_Consumer.Controller;
using System.IO;
using Newtonsoft.Json;

namespace TLGX_Consumer.Service
{
    /// <summary>
    /// Summary description for CityAutoComplete
    /// </summary>
   
    public class FileProcessingReport : IHttpHandler
    {
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        MDMSVC.DC_SupplierImportFile_Progress_RQ RQParams = new MDMSVC.DC_SupplierImportFile_Progress_RQ();
        public void ProcessRequest(HttpContext context)
        {
            var SupplierImportFile_Id =  context.Request.QueryString["FileId"];
            RQParams.SupplierImportFile_Id = SupplierImportFile_Id;
            var res = MapSvc.GetStaticDataUploadProcessLog(RQParams);
            context.Response.Write(new JavaScriptSerializer().Serialize(res));
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
