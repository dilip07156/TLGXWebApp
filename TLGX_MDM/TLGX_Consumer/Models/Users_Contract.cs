using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TLGX_Consumer.Models
{
    public class Users_Contract
    {
        public string Email { get; set; }
        public Guid? Manager { get; set; }
    }
}