using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Models.dataProject
{
    public class Project
    {

        Guid _Project_Id;
        string _Project_Name;
        string _Status;
        DateTime? _Create_Date;
        string _Create_User;
        DateTime? _Update_Date;
        string _Update_User;

        public Guid Project_Id
        {
            get
            {
                return _Project_Id;
            }

            set
            {
                _Project_Id = value;
            }
        }

        public string Project_Name
        {
            get
            {
                return _Project_Name;
            }

            set
            {
                _Project_Name = value;
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

    public class Teams
    {
        Guid _Team_Id;
        string _Team_Name;
        string _Status;
        DateTime? _CREATE_DATE;
        string _CREATE_USER;
        DateTime? _UPDATE_DATE;
        string _UPDATE_USER;

        public Guid Team_Id
        {
            get
            {
                return _Team_Id;
            }

            set
            {
                _Team_Id = value;
            }
        }

        public string Team_Name
        {
            get
            {
                return _Team_Name;
            }

            set
            {
                _Team_Name = value;
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

}