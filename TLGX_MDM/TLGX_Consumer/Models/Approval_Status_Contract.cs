using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Models
{
    public class Approval_Status_Master_Contract
    {
        public List<Approval_Status_Master_Contract_Record> _Approval_Status_Master_Contract;

        public List<Approval_Status_Master_Contract_Record> Approval_Status_Master_Contract_List
        {
            get
            {
                return _Approval_Status_Master_Contract;
            }

            set
            {
                _Approval_Status_Master_Contract = value;
            }
        }
    }

    public class Approval_Status_Master_Contract_Record
    {
        public Guid _Appr_status_id;
        public string _Status;
        public string _Object_type;
        public Guid? _Object_id;
        public int? _Status_hierarchy;

        public Guid Appr_status_id
        {
            get
            {
                return _Appr_status_id;
            }

            set
            {
                _Appr_status_id = value;
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

        public string Object_type
        {
            get
            {
                return _Object_type;
            }

            set
            {
                _Object_type = value;
            }
        }

        public Guid? Object_id
        {
            get
            {
                return _Object_id;
            }

            set
            {
                _Object_id = value;
            }
        }

        public int? Status_hierarchy
        {
            get
            {
                return _Status_hierarchy;
            }

            set
            {
                _Status_hierarchy = value;
            }
        }
    }

    public class Approval_Status_Contract_Record
    {
        public Guid _Approval_Status_Flow_ID;
        public Guid? _Object_ID;
        public string _Object_Type;
        public Guid? _Status_id;
        public string _Status;
        public string _Status_Message;
        public DateTime _Update_Date;
        public string _Update_By;
        public string _Next_Actor;

        public Guid Approval_Status_Flow_ID
        {
            get
            {
                return _Approval_Status_Flow_ID;
            }

            set
            {
                _Approval_Status_Flow_ID = value;
            }
        }

        public Guid? Object_ID
        {
            get
            {
                return _Object_ID;
            }

            set
            {
                _Object_ID = value;
            }
        }

        public string Object_Type
        {
            get
            {
                return _Object_Type;
            }

            set
            {
                _Object_Type = value;
            }
        }

        public Guid? Status_id
        {
            get
            {
                return _Status_id;
            }

            set
            {
                _Status_id = value;
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

        public string Status_Message
        {
            get
            {
                return _Status_Message;
            }

            set
            {
                _Status_Message = value;
            }
        }

        public DateTime Update_Date
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

        public string Update_By
        {
            get
            {
                return _Update_By;
            }

            set
            {
                _Update_By = value;
            }
        }

        public string Next_Actor
        {
            get
            {
                return _Next_Actor;
            }

            set
            {
                _Next_Actor = value;
            }
        }
    }
}