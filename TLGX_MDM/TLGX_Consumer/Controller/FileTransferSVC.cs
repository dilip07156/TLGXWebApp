using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Controller
{
    public class FileTransferSVC
    {
        #region Transfer File In Chunks
        public MDMSVC.DC_UploadResponse TransferFileInChunks(MDMSVC.DC_FileData fileData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["File_TransferInChunks"], fileData, fileData.GetType(), typeof(MDMSVC.DC_UploadResponse)  , out result);
            return result as MDMSVC.DC_UploadResponse;
        }
        #endregion
    }
}