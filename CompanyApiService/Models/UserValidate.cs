using DBAccess.Dapper.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CompanyApiService.Models
{
    public class UserValidate
    {
        private RoleStore<AppRole> _roleManager;
        private UserStore<AppMember, AppRole> _userManager;
        public UserValidate()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _userManager = new UserStore<AppMember, AppRole>(new DbManager(connectionString));
            _roleManager = new RoleStore<AppRole>(new DbManager(connectionString));
        }
        //This method is used to check the user credentials
        public static bool Login(string username, string password)
        {
            if (username == "martin" && password == "123456")
            {
                return true;
            }
            return false;
        }

     
    }
}