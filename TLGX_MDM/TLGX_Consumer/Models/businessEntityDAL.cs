using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Models
{

    public class businessEntityDAL : IDisposable
    {
        
        public enum operation
        {
            Save,
            Update,
            SoftDelete
        }

        public void Dispose()
        {

        }


        #region Main Supplier Level

        public DataTable GetSupplierList()
        {

            DataTable dtRet = new DataTable();
            using (Models.TLGX_MAPPEREntities1 context = new Models.TLGX_MAPPEREntities1())
            {
                var supplierData = (from s in context.Suppliers
                                    orderby s.Name ascending
                                    select new Models.BusinessEntity.Suppliers
                                    {
                                        Supplier_Id = s.Supplier_Id,
                                        Code = s.Code,
                                        Name = s.Name,
                                        SupplierType = s.SupplierType,
                                        SupplierOwner = s.SupplierOwner,
                                        Create_Date = s.Create_Date,
                                        Create_User = s.Create_User,
                                        Edit_Date = s.Edit_Date,
                                        Edit_User = s.Edit_User
                                    }
                                    ).ToList();

                dtRet = ConversionClass.CreateDataTable(supplierData);

                return dtRet;



            }
        }
      
        
        #endregion

    }
}