﻿using System;
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
   
    public class FileProgressDashboard : IHttpHandler
    {
        Controller.MappingSVCs MapSvc = new Controller.MappingSVCs();
        public void ProcessRequest(HttpContext context)
        {
            string SupplierImportFile_Id =  context.Request.QueryString["FileId"];
            var res = MapSvc.getFileProgressDashBoardData(SupplierImportFile_Id);
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
