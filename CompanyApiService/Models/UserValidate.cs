using DBAccess.Dapper.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;

namespace CompanyApiService.Models
{
    public class UserValidate
    {
        //private UserStore<AppMember, AppRole> _userStore;
        public UserValidate()
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //_userStore = new UserStore<AppMember, AppRole>(new DbManager(connectionString));
        }

        //This method is used to check the user credentials
        public bool ValidateUser(string username, string password)
        {
            if (username == "martin" && password == "123456")
            {
                return true;
            }
            return false;
            //string hash = _userStore.GetPasswordHashAsync(new AppMember { UserName = username, Email = username }).Result;
            //bool result = Crypto.VerifyHashedPassword(hash, password);
            //return result;
        }

    }
}