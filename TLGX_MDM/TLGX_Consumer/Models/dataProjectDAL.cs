using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Models
{
    public class dataProjectDAL
    {
        public List<dataProject.Project> getAllProjects()
        {
            try
            {
                using (TLGX_MAPPEREntities1 context = new TLGX_MAPPEREntities1())
                {
                    var projectData = (from s in context.Projects
                                       orderby s.CREATE_DATE descending
                                       select new Models.dataProject.Project
                                       {
                                           Project_Id = s.Project_ID,
                                           Project_Name = s.Project_Name,
                                           Status = s.Status,
                                           Create_Date = s.CREATE_DATE,
                                           Create_User = s.CREATE_USER,
                                           Update_Date = s.UPDATE_DATE,
                                           Update_User = s.UPDATE_USER
                                       }
                                  ).ToList();
                    return projectData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}