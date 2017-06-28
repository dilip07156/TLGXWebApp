using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.Models
{
    public class lookupAttributeDAL : IDisposable
    {
        public void Dispose()
        { }
        public MDMSVC.DC_M_masterattributelists GetAllAttributeAndValuesByFOR(string For, string AttributeName)
        {
            MasterDataSVCs _objMaster = new MasterDataSVCs();
            var retClass = new MDMSVC.DC_M_masterattributelists();
            retClass = _objMaster.GetListAttributeAndValuesByFOR(new MDMSVC.DC_MasterAttribute() { MasterFor = For, Name = AttributeName });
            return retClass;
        }
    }
}