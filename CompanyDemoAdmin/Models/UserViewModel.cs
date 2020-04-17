using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDemoAdmin.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int MemberType { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsLoggined { get; set; }
    }
}