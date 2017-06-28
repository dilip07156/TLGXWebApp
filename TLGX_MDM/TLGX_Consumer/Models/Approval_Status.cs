using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TLGX_Consumer.Models
{
    public class Approval_Status : IDisposable
    {
        public void Dispose()
        {

        }

        public List<Approval_Status_Master_Contract_Record> GetApprovalStatusMasterData(string prodtype)
        {
            //DataTable dtRet = new DataTable();
            try
            {
                using (TLGX_MAPPEREntities1 context = new TLGX_MAPPEREntities1())
                {
                    //var MasterData = new Approval_Status_Master_Contract();
                    var statusmaster = (from ct in context.m_Approval_StatusMaster
                                        where ct.Object_type.Trim().ToUpper() == prodtype.Trim().ToUpper()
                                        orderby ct.Status_hierarchy
                                        select new Approval_Status_Master_Contract_Record
                                        {
                                            Appr_status_id = ct.Appr_status_id,
                                            Object_id = ct.Object_id,
                                            Object_type = ct.Object_type,
                                            Status = ct.Status,
                                            Status_hierarchy = ct.Status_hierarchy
                                        }).ToList();
                    //MasterData.Approval_Status_Master_Contract_List = statusmaster;
                    //dtRet = ConversionClass.CreateDataTable(statusmaster);
                    return statusmaster;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetApprovalStatusMasterData(List<Approval_Status_Master_Contract_Record> statusmaster)
        {
            bool ret = false;

            try
            {
                using (TLGX_MAPPEREntities1 context = new TLGX_MAPPEREntities1())
                {
                    foreach (Approval_Status_Master_Contract_Record statusrec in statusmaster)
                    {
                        Models.m_Approval_StatusMaster nStatRec = context.m_Approval_StatusMaster.Single(u => u.Appr_status_id == statusrec.Appr_status_id);
                        if (nStatRec == null)
                        {
                            nStatRec = new Models.m_Approval_StatusMaster
                            {
                                Appr_status_id = Guid.NewGuid(),
                                Object_id = statusrec.Object_id,
                                Object_type = statusrec.Object_type,
                                Status = statusrec.Status,
                                Status_hierarchy = statusrec.Status_hierarchy
                            };
                            context.m_Approval_StatusMaster.Add(nStatRec);
                        }
                        else
                        {
                            nStatRec.Object_id = statusrec.Object_id;
                            nStatRec.Object_type = statusrec.Object_type;
                            nStatRec.Status = statusrec.Status;
                            nStatRec.Status_hierarchy = statusrec.Status_hierarchy;
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }
    }
}