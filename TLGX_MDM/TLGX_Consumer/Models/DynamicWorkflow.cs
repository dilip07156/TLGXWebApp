using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Models.DynamicWorkflow
{
    public class ActivityMaster
    {
        Guid _Activity_Master_Id;
        string _Activity_Name;
        string _Activity_Class_Name;
        string _Activity_Method_Name;
        string _Status;
        string _Status_Message;
        string _Description;
        DateTime? _CREATE_DATE;
        string _CREATE_USER;
        DateTime? _EDIT_DATE;
        string _UPDATE_USER;

        public Guid Activity_Master_Id
        {
            get
            {
                return _Activity_Master_Id;
            }

            set
            {
                _Activity_Master_Id = value;
            }
        }

        public string Activity_Name
        {
            get
            {
                return _Activity_Name;
            }

            set
            {
                _Activity_Name = value;
            }
        }

        public string Activity_Class_Name
        {
            get
            {
                return _Activity_Class_Name;
            }

            set
            {
                _Activity_Class_Name = value;
            }
        }

        public string Activity_Method_Name
        {
            get
            {
                return _Activity_Method_Name;
            }

            set
            {
                _Activity_Method_Name = value;
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

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public DateTime? CREATE_DATE
        {
            get
            {
                return _CREATE_DATE;
            }

            set
            {
                _CREATE_DATE = value;
            }
        }

        public string CREATE_USER
        {
            get
            {
                return _CREATE_USER;
            }

            set
            {
                _CREATE_USER = value;
            }
        }

        public DateTime? EDIT_DATE
        {
            get
            {
                return _EDIT_DATE;
            }

            set
            {
                _EDIT_DATE = value;
            }
        }

        public string UPDATE_USER
        {
            get
            {
                return _UPDATE_USER;
            }

            set
            {
                _UPDATE_USER = value;
            }
        }
    }

    public class ApprovalRoleMaster
    {
        Guid _Appr_Role_ID;
        string _Role_Name;
        string _Status;
        string _Description;
        DateTime? _CREATE_DATE;
        string _CREATE_USER;
        DateTime? _UPDATE_DATE;
        string _UPDATE_USER;

        public Guid Appr_Role_ID
        {
            get
            {
                return _Appr_Role_ID;
            }

            set
            {
                _Appr_Role_ID = value;
            }
        }

        public string Role_Name
        {
            get
            {
                return _Role_Name;
            }

            set
            {
                _Role_Name = value;
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

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public DateTime? CREATE_DATE
        {
            get
            {
                return _CREATE_DATE;
            }

            set
            {
                _CREATE_DATE = value;
            }
        }

        public string CREATE_USER
        {
            get
            {
                return _CREATE_USER;
            }

            set
            {
                _CREATE_USER = value;
            }
        }

        public DateTime? UPDATE_DATE
        {
            get
            {
                return _UPDATE_DATE;
            }

            set
            {
                _UPDATE_DATE = value;
            }
        }

        public string UPDATE_USER
        {
            get
            {
                return _UPDATE_USER;
            }

            set
            {
                _UPDATE_USER = value;
            }
        }
    }

    public class ApprovalStatusMaster
    {
        Guid _Appr_status_id;
        string _Status;
        string _Object_type;
        Guid _Object_Id;
        int _Status_hierarchy;
        DateTime? _CREATE_DATE;
        string _CREATE_USER;
        DateTime? _UPDATE_DATE;
        string _UPDATE_USER;

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

        public Guid Object_Id
        {
            get
            {
                return _Object_Id;
            }

            set
            {
                _Object_Id = value;
            }
        }

        public int Status_hierarchy
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

        public DateTime? CREATE_DATE
        {
            get
            {
                return _CREATE_DATE;
            }

            set
            {
                _CREATE_DATE = value;
            }
        }

        public string CREATE_USER
        {
            get
            {
                return _CREATE_USER;
            }

            set
            {
                _CREATE_USER = value;
            }
        }

        public DateTime? UPDATE_DATE
        {
            get
            {
                return _UPDATE_DATE;
            }

            set
            {
                _UPDATE_DATE = value;
            }
        }

        public string UPDATE_USER
        {
            get
            {
                return _UPDATE_USER;
            }

            set
            {
                _UPDATE_USER = value;
            }
        }
    }

    public class ApprovalStatusFlow
    {
        Guid _Approval_Status_Flow_ID;
        Guid _Object_ID;
        string _Object_Type;
        Guid _Status_id;
        string _Status;
        string Status_Message;
        string Next_Actor;
        DateTime? _CREATE_DATE;
        string _CREATE_USER;
        DateTime? _UPDATE_DATE;
        string _UPDATE_USER;

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

        public Guid Object_ID
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

        public Guid Status_id
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

        public string Status_Message1
        {
            get
            {
                return Status_Message;
            }

            set
            {
                Status_Message = value;
            }
        }

        public string Next_Actor1
        {
            get
            {
                return Next_Actor;
            }

            set
            {
                Next_Actor = value;
            }
        }

        public DateTime? CREATE_DATE
        {
            get
            {
                return _CREATE_DATE;
            }

            set
            {
                _CREATE_DATE = value;
            }
        }

        public string CREATE_USER
        {
            get
            {
                return _CREATE_USER;
            }

            set
            {
                _CREATE_USER = value;
            }
        }

        public DateTime? UPDATE_DATE
        {
            get
            {
                return _UPDATE_DATE;
            }

            set
            {
                _UPDATE_DATE = value;
            }
        }

        public string UPDATE_USER
        {
            get
            {
                return _UPDATE_USER;
            }

            set
            {
                _UPDATE_USER = value;
            }
        }
    }

    public class WorkFlowMessage
    {
        Guid _WorkFlowMessage_Id;
        string _Subject;
        string _Text;
        string _From;
        string _To;
        string _Cc;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Edit_Date;
        string _Edit_User;

       

        public string Subject
        {
            get
            {
                return _Subject;
            }

            set
            {
                _Subject = value;
            }
        }

        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                _Text = value;
            }
        }

        public string From
        {
            get
            {
                return _From;
            }

            set
            {
                _From = value;
            }
        }

        public string To
        {
            get
            {
                return _To;
            }

            set
            {
                _To = value;
            }
        }

        public string Cc
        {
            get
            {
                return _Cc;
            }

            set
            {
                _Cc = value;
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

        public Guid WorkFlowMessage_Id
        {
            get
            {
                return _WorkFlowMessage_Id;
            }

            set
            {
                _WorkFlowMessage_Id = value;
            }
        }
    }

}