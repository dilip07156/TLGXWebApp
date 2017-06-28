using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Models.BusinessEntity
{



    #region Supplier
    public class Suppliers
    {
        string _Name;
        string _Code;
        Guid _Supplier_Id;
        string _SupplierType;
        string _SupplierOwner;
        string _Create_User;
        string _Edit_User;
        DateTime? _Create_Date;
        DateTime? _Edit_Date;


        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }

            set
            {
                _Code = value;
            }
        }

        public Guid Supplier_Id
        {
            get
            {
                return _Supplier_Id;
            }

            set
            {
                _Supplier_Id = value;
            }
        }

        public string SupplierType
        {
            get
            {
                return _SupplierType;
            }

            set
            {
                _SupplierType = value;
            }
        }

        public string SupplierOwner
        {
            get
            {
                return _SupplierOwner;
            }

            set
            {
                _SupplierOwner = value;
            }
        }

        public string Create_User
        {
            get
            {
                return _Create_User;
            }

            set
            {
                _Create_User = value;
            }
        }

        public string Edit_User
        {
            get
            {
                return _Edit_User;
            }

            set
            {
                _Edit_User = value;
            }
        }

        public DateTime? Create_Date
        {
            get
            {
                return _Create_Date;
            }

            set
            {
                _Create_Date = value;
            }
        }

        public DateTime? Edit_Date
        {
            get
            {
                return _Edit_Date;
            }

            set
            {
                _Edit_Date = value;
            }
        }
    }
        
    public class SupplierMarket
    {
        Guid _Supplier_Market_Id;
        Guid _supplier_Id;
        string _Status;
        string _Code;
        string _Name;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;
        bool _IsActive;
        

        public Guid Supplier_Market_Id
        {
            get
            {
                return _Supplier_Market_Id;
            }

            set
            {
                _Supplier_Market_Id = value;
            }
        }

        public Guid Supplier_Id
        {
            get
            {
                return _supplier_Id;
            }

            set
            {
                _supplier_Id = value;
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }

            set
            {
                _Code = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public DateTime? Create_Date
        {
            get
            {
                return _Create_Date;
            }

            set
            {
                _Create_Date = value;
            }
        }

        public string Create_User
        {
            get
            {
                return _Create_User;
            }

            set
            {
                _Create_User = value;
            }
        }

        public DateTime? Edit_Date
        {
            get
            {
                return _Edit_Date;
            }

            set
            {
                _Edit_Date = value;
            }
        }

        public string Edit_User
        {
            get
            {
                return _Edit_User;
            }

            set
            {
                _Edit_User = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _IsActive;
            }

            set
            {
                _IsActive = value;
            }
        }
    }

    public class SupplierProductCategory
    {
        Guid _Supplier_ProductCategory_Id;
        Guid _Supplier_Id;
        bool _IsDefaultSupplier;
        string _ProductCategory;
        string _ProductCategorySubType;
        string _Status;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;
        bool _IsActive;

        public Guid Supplier_ProductCategory_Id
        {
            get
            {
                return _Supplier_ProductCategory_Id;
            }

            set
            {
                _Supplier_ProductCategory_Id = value;
            }
        }

        public Guid Supplier_Id
        {
            get
            {
                return _Supplier_Id;
            }

            set
            {
                _Supplier_Id = value;
            }
        }

        public bool IsDefaultSupplier
        {
            get
            {
                return _IsDefaultSupplier;
            }

            set
            {
                _IsDefaultSupplier = value;
            }
        }

        public string ProductCategory
        {
            get
            {
                return _ProductCategory;
            }

            set
            {
                _ProductCategory = value;
            }
        }

        public string ProductCategorySubType
        {
            get
            {
                return _ProductCategorySubType;
            }

            set
            {
                _ProductCategorySubType = value;
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        public DateTime? Create_Date
        {
            get
            {
                return _Create_Date;
            }

            set
            {
                _Create_Date = value;
            }
        }

        public string Create_User
        {
            get
            {
                return _Create_User;
            }

            set
            {
                _Create_User = value;
            }
        }

        public DateTime? Edit_Date
        {
            get
            {
                return _Edit_Date;
            }

            set
            {
                _Edit_Date = value;
            }
        }

        public string Edit_User
        {
            get
            {
                return _Edit_User;
            }

            set
            {
                _Edit_User = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _IsActive;
            }

            set
            {
                _IsActive = value;
            }
        }
    }  
    
       
    
    
    
    #endregion


    public class Client
    {

    }
    public class OrgStructure
    {

    }
}