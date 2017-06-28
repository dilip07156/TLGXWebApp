using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TLGX_Consumer.Models
{
    [DataContract]
    public class MasterDataContract
    {
        List<MasterattributeE> _m_MasterAttributes;
        List<MasterAttributeValuesE> _m_MasterAttributeValues;
        List<CountryMasterE> _CountryMaster;
        List<CityMasterE> _CityMaster;
        List<CountryMappingE> _CountryMapping;
        List<CityMappingE> _CityMapping;
        List<Geo_StatesE> _Geo_States;
        List<CityAreaE> _CityArea;
        List<CityAreaLocationE> _CityAreaLocation;


        [DataMember]
        public List<MasterattributeE> m_MasterAttributesL
        {
            get
            {
                return _m_MasterAttributes;
            }

            set
            {
                _m_MasterAttributes = value;
            }
        }

        [DataMember]
        public List<MasterAttributeValuesE> m_MasterAttributeValuesL
        {
            get
            {
                return _m_MasterAttributeValues;
            }

            set
            {
                _m_MasterAttributeValues = value;
            }
        }

        [DataMember]
        public List<CountryMasterE> CountryMasterL
        {
            get
            {
                return _CountryMaster;
            }

            set
            {
                _CountryMaster = value;
            }
        }

        [DataMember]
        public List<CityMasterE> CityMasterL
        {
            get
            {
                return _CityMaster;
            }

            set
            {
                _CityMaster = value;
            }
        }

        [DataMember]
        public List<CountryMappingE> CountryMappingL
        {
            get
            {
                return _CountryMapping;
            }

            set
            {
                _CountryMapping = value;
            }
        }

        [DataMember]
        public List<CityMappingE> CityMappingL
        {
            get
            {
                return _CityMapping;
            }

            set
            {
                _CityMapping = value;
            }
        }

        [DataMember]
        public List<Geo_StatesE> Geo_StatesL
        {
            get
            {
                return _Geo_States;
            }

            set
            {
                _Geo_States = value;
            }

        }
        [DataMember]
        public List<CityAreaE> CityAreaL
        {

            get
            {
                return _CityArea;
            }

            set
            {
                _CityArea = value;
            }


        }

        [DataMember]
        public List<CityAreaLocationE> CityAreaLocationL
            {
            
            get
            {
                return _CityAreaLocation;
            }

    set
            {
                _CityAreaLocation = value;
            }
            
            }

    }

    public class MasterattributeE
    {
        Guid _m_ma_id;
        string _name;
        string _masterfor;

        public Guid m_MasterAttrribute_ID
        {
            get
            {
                return _m_ma_id;
            }

            set
            {
                _m_ma_id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string MasterFor
        {
            get
            {
                return _masterfor;
            }

            set
            {
                _masterfor = value;
            }
        }
    }

    public class MasterAttributeValuesE
    {
        Guid _m_mav_id;
        Guid? _m_ma_id;
        string _attributevalue;

        public Guid m_MasterAttrributeValues_ID
        {
            get
            {
                return _m_mav_id;
            }

            set
            {
                _m_mav_id = value;
            }
        }

        public Guid? m_MasterAttrribute_ID
        {
            get
            {
                return _m_ma_id;
            }

            set
            {
                _m_ma_id = value;
            }
        }

        public string AttributeValues
        {
            get
            {
                return _attributevalue;
            }

            set
            {
                _attributevalue = value;
            }
        }

    }

    public class CountryMasterE
    {
        Guid _Country_Id;
        string _name;
        string _code;
        string _status;
        DateTime? _create_date;
        string _create_user;
        DateTime? _edit_date;
        string _edit_user;
        string _ISO3166_1_Alpha_2;
        string _ISO3166_1_Alpha_3;
        string _NameWithCode;
        public Guid Country_ID
        {
            get
            {
                return _Country_Id;
            }

            set
            {
                _Country_Id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public DateTime? Create_Date
        {
            get
            {
                return _create_date;
            }

            set
            {
                _create_date = value;
            }
        }

        public string Create_User
        {
            get
            {
                return _create_user;
            }

            set
            {
                _create_user = value;
            }
        }

        public DateTime? Edit_Date
        {
            get
            {
                return _edit_date;
            }

            set
            {
                _edit_date = value;
            }
        }

        public string Edit_User
        {
            get
            {
                return _edit_user;
            }

            set
            {
                _edit_user = value;
            }
        }

        public string ISO3166_1_Alpha_2
        {
            get
            {
                return _ISO3166_1_Alpha_2;
            }

            set
            {
                _ISO3166_1_Alpha_2 = value;
            }
        }

        public string ISO3166_1_Alpha_3
        {
            get
            {
                return _ISO3166_1_Alpha_3;
            }

            set
            {
                _ISO3166_1_Alpha_3 = value;
            }
        }

        public string NameWithCode
        {
            get
            {
                return _NameWithCode;
            }

            set
            {
                _NameWithCode = value;
            }
        }
    }

    public class CityMasterE
    {
        Guid _city_id;
        Guid _Country_Id;
        string _name;
        string _countryname;
        string _code;
        string _status;
        DateTime? _create_date;
        string _create_user;
        DateTime? _edit_date;
        string _edit_user;
        string _StateCode;
        string _StateName;
        string _NameWithCode;

        public Guid City_ID
        {
            get
            {
                return _city_id;
            }

            set
            {
                _city_id = value;
            }
        }

        public Guid Country_ID
        {
            get
            {
                return _Country_Id;
            }

            set
            {
                _Country_Id = value;
            }
        }

        public string CountryName
        {
            get
            {
                return _countryname;
            }

            set
            {
                _countryname = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public DateTime? Create_Date
        {
            get
            {
                return _create_date;
            }

            set
            {
                _create_date = value;
            }
        }

        public string Create_User
        {
            get
            {
                return _create_user;
            }

            set
            {
                _create_user = value;
            }
        }

        public DateTime? Edit_Date
        {
            get
            {
                return _edit_date;
            }

            set
            {
                _edit_date = value;
            }
        }

        public string Edit_User
        {
            get
            {
                return _edit_user;
            }

            set
            {
                _edit_user = value;
            }
        }

        public string StateCode
        {
            get
            {
                return _StateCode;
            }

            set
            {
                _StateCode = value;
            }
        }

        public string StateName
        {
            get
            {
                return _StateName;
            }

            set
            {
                _StateName = value;
            }
        }

        public string NameWithCode
        {
            get
            {
                return _NameWithCode;
            }

            set
            {
                _NameWithCode = value;
            }
        }
    }

    public class CountryMappingE
    {
        Guid _countrymapping_id;
        Guid? _Country_Id;
        Guid? _supplier_Id;
        string _suppliername;
        string _countryname;
        string _countrycode;
        string _status;
        DateTime? _create_date;
        string _create_user;
        DateTime? _edit_date;
        string _edit_user;

        public Guid CountryMapping_ID
        {
            get
            {
                return _countrymapping_id;
            }

            set
            {
                _countrymapping_id = value;
            }
        }
        public Guid? Country_ID
        {
            get
            {
                return _Country_Id;
            }

            set
            {
                _Country_Id = value;
            }
        }
        public Guid? Supplier_ID
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
        public string Supplier_Name
        {
            get
            {
                return _suppliername;
            }

            set
            {
                _suppliername = value;
            }
        }
        public string CountryName
        {
            get
            {
                return _countryname;
            }

            set
            {
                _countryname = value;
            }
        }

        public string CountryCode
        {
            get
            {
                return _countrycode;
            }

            set
            {
                _countrycode = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public DateTime? Create_Date
        {
            get
            {
                return _create_date;
            }

            set
            {
                _create_date = value;
            }
        }

        public string Create_User
        {
            get
            {
                return _create_user;
            }

            set
            {
                _create_user = value;
            }
        }

        public DateTime? Edit_Date
        {
            get
            {
                return _edit_date;
            }

            set
            {
                _edit_date = value;
            }
        }

        public string Edit_User
        {
            get
            {
                return _edit_user;
            }

            set
            {
                _edit_user = value;
            }
        }
    }

    public class CityMappingE
    {
        Guid _citymapping_id;
        Guid? _city_id;
        Guid? _Country_Id;
        string _cityname;
        string _citycode;
        Guid? _supplier_Id;

        public Guid CityMapping_ID
        {
            get
            {
                return _citymapping_id;
            }

            set
            {
                _citymapping_id = value;
            }
        }
        public Guid? Country_ID
        {
            get
            {
                return _Country_Id;
            }

            set
            {
                _Country_Id = value;
            }
        }
        public Guid? City_ID
        {
            get
            {
                return _city_id;
            }

            set
            {
                _city_id = value;
            }
        }
        public Guid? Supplier_ID
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
        public string CityName
        {
            get
            {
                return _cityname;
            }

            set
            {
                _cityname = value;
            }
        }
        public string CityCode
        {
            get
            {
                return _citycode;
            }

            set
            {
                _citycode = value;
            }
        }

    }

    public class Statues
    {
        Guid _status_id;
        string _status_name;
        string _status_short;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Update_Date;
        string _Update_User;

        public Guid Status_ID
        {
            get
            {
                return _status_id;
            }

            set
            {
                _status_id = value;
            }
        }

        public string Status_Name
        {
            get
            {
                return _status_name;
            }

            set
            {
                _status_name = value;
            }
        }

        public string Status_Short
        {
            get
            {
                return _status_short;
            }

            set
            {
                _status_short = value;
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

        public DateTime? Update_Date
        {
            get
            {
                return _Update_Date;
            }

            set
            {
                _Update_Date = value;
            }
        }

        public string Update_User
        {
            get
            {
                return _Update_User;
            }

            set
            {
                _Update_User = value;
            }
        }
    }

    // handles geographic states, prefaced with Geo_ as State is a not a classname you should use!
    public class Geo_StatesE
    {
        Guid _State_Id;
        Guid _Country_Id;
        string _StateCode;
        string _StateName;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;
        string _StateNameLocalLanguage;

        public Guid State_Id
        {
            get
            {
                return _State_Id;
            }

            set
            {
                _State_Id = value;
            }
        }

        public Guid Country_Id
        {
            get
            {
                return _Country_Id;
            }

            set
            {
                _Country_Id = value;
            }
        }

        public string StateCode
        {
            get
            {
                return _StateCode;
            }

            set
            {
                _StateCode = value;
            }
        }

        public string StateName
        {
            get
            {
                return _StateName;
            }

            set
            {
                _StateName = value;
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

        public string StateNameLocalLanguage
        {
            get
            {
                return _StateNameLocalLanguage;
            }

            set
            {
                _StateNameLocalLanguage = value;
            }
        }
    }

    // handles tier1 geo below CITY
    public class CityAreaE
    {
        Guid _CityArea_Id;
        Guid? _City_Id;
        string _Name;
        string _Code;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;

        public Guid CityArea_Id
        {
            get
            {
                return _CityArea_Id;
            }

            set
            {
                _CityArea_Id = value;
            }
        }

        public Guid? City_Id
        {
            get
            {
                return _City_Id;
            }

            set
            {
                _City_Id = value;
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
    }

    // handles tier2 geo below city/cityarea
    public class CityAreaLocationE
    {

        Guid _CityAreaLocation_Id;
        Guid _City_Id;
        string _Name;
        string _Code;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;
        Guid _CityArea_Id;

        public Guid CityAreaLocation_Id
        {
            get
            {
                return _CityAreaLocation_Id;
            }

            set
            {
                _CityAreaLocation_Id = value;
            }
        }

        public Guid City_Id
        {
            get
            {
                return _City_Id;
            }

            set
            {
                _City_Id = value;
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

        public Guid CityArea_Id
        {
            get
            {
                return _CityArea_Id;
            }

            set
            {
                _CityArea_Id = value;
            }
        }
    }

    public class AccommodationRoomInfo
    {
        Guid _Accommodation_RoomInfo_Id;
        Guid? _Accommodation_Id;
        string _RoomCategory;

        public Guid Accommodation_RoomInfo_Id
        {
            get
            {
                return _Accommodation_RoomInfo_Id;
            }

            set
            {
                _Accommodation_RoomInfo_Id = value;
            }
        }

        public Guid? Accommodation_Id
        {
            get
            {
                return _Accommodation_Id;
            }

            set
            {
                _Accommodation_Id = value;
            }
        }
        public string RoomCategory
        {
            get
            {
                return _RoomCategory;
            }

            set
            {
                _RoomCategory = value;
            }
        }
    }

    public class SupplierMasters
    {
        Guid _Supplier_Id;
        string _Name;
        string _Code;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;
        string _SupplierType;
        string _SupplierOwener;

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

        public string SupplierOwener
        {
            get
            {
                return _SupplierOwener;
            }

            set
            {
                _SupplierOwener = value;
            }
        }
    }

    public class AccomodationE
    {
        Guid _Accommodation_Id;
        string _HotelName;

        public Guid Accommodation_Id
        {
            get
            {
                return _Accommodation_Id;
            }

            set
            {
                _Accommodation_Id = value;
            }
        }

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
    }

}