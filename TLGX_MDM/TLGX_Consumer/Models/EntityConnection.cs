using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TLGX_Consumer.Models
{
    public class EntityConnection
    {
    }

    public partial class Entities : DbContext
    {
        public Entities(string ConnectionString)
             : base(ConnectionString)
        {

        }
    }

    public static class ConnString
    {
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TLGX_MAPPEREntities1"].ConnectionString;
    }
}