using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace TLGX_Consumer.Models.DataContracts
{
    public class AccomodationContract
    {
    }

    [DataContract]
    public class DC_Accomodation_Search_RQ
    {
        //string _GroupOfCompanies;
        //string _GroupCompany;
        //string _CompanyId;
        string _ProductCategory;
        string _ProductCategorySubType;
        //string _CommonHotelId;
        int? _CompanyHotelId;
        string _HotelName;
        string _Country;
        string _City;
        string _Location;
        bool _Status;
        int _PageNo;
        int _PageSize;

        //[DataMember]
        //public string GroupOfCompanies
        //{
        //    get
        //    {
        //        return _GroupOfCompanies;
        //    }

        //    set
        //    {
        //        _GroupOfCompanies = value;
        //    }
        //}

        //[DataMember]
        //public string GroupCompany
        //{
        //    get
        //    {
        //        return _GroupCompany;
        //    }

        //    set
        //    {
        //        _GroupCompany = value;
        //    }
        //}

        //[DataMember]
        //public string CompanyId
        //{
        //    get
        //    {
        //        return _CompanyId;
        //    }

        //    set
        //    {
        //        _CompanyId = value;
        //    }
        //}

        [DataMember(IsRequired = true)]
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

        [DataMember(IsRequired = true)]
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

        //[DataMember]
        //public string CommonHotelId
        //{
        //    get
        //    {
        //        return _CommonHotelId;
        //    }

        //    set
        //    {
        //        _CommonHotelId = value;
        //    }
        //}

        [DataMember]
        public int? CompanyHotelId
        {
            get
            {
                return _CompanyHotelId;
            }

            set
            {
                _CompanyHotelId = value;
            }
        }

        [DataMember]
        public string HotelName
        {
            get
            {
                return _HotelName;
            }

            set
            {
                _HotelName = value;
            }
        }

        [DataMember]
        public string Country
        {
            get
            {
                return _Country;
            }

            set
            {
                _Country = value;
            }
        }

        [DataMember]
        public string City
        {
            get
            {
                return _City;
            }

            set
            {
                _City = value;
            }
        }

        [DataMember]
        public string Location
        {
            get
            {
                return _Location;
            }

            set
            {
                _Location = value;
            }
        }

        [DataMember(IsRequired = true)]
        public bool Status
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

        [DataMember(IsRequired = true)]
        public int PageNo
        {
            get
            {
                return _PageNo;
            }

            set
            {
                _PageNo = value;
            }
        }

        [DataMember(IsRequired = true)]
        public int PageSize
        {
            get
            {
                return _PageSize;
            }

            set
            {
                _PageSize = value;
            }
        }
    }

    [DataContract]
    public class DC_Accomodation_Search_RS
    {
        string _AccomodationId;
        string _HotelName;
        string _CompanyName;
        string _CompanyHotelId;
        string _HotelChain;
        string _HotelBrand;
        string _Country;
        string _City;
        string _Location;
        string _Status;

        [DataMember]
        public string AccomodationId
        {
            get
            {
                return _AccomodationId;
            }

            set
            {
                _AccomodationId = value;
            }
        }

        [DataMember]
        public string HotelName
        {
            get
            {
                return _HotelName;
            }

            set
            {
                _HotelName = value;
            }
        }

        [DataMember]
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }

            set
            {
                _CompanyName = value;
            }
        }

        [DataMember]
        public string CompanyHotelId
        {
            get
            {
                return _CompanyHotelId;
            }

            set
            {
                _CompanyHotelId = value;
            }
        }

        [DataMember]
        public string HotelChain
        {
            get
            {
                return _HotelChain;
            }

            set
            {
                _HotelChain = value;
            }
        }

        [DataMember]
        public string HotelBrand
        {
            get
            {
                return _HotelBrand;
            }

            set
            {
                _HotelBrand = value;
            }
        }

        [DataMember]
        public string Country
        {
            get
            {
                return _Country;
            }

            set
            {
                _Country = value;
            }
        }

        [DataMember]
        public string City
        {
            get
            {
                return _City;
            }

            set
            {
                _City = value;
            }
        }

        [DataMember]
        public string Location
        {
            get
            {
                return _Location;
            }

            set
            {
                _Location = value;
            }
        }

        [DataMember]
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
    }
}