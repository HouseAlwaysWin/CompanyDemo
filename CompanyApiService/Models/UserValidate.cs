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
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        private UserStore<AppMember, AppRole> _userStore;
        public UserValidate()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _userStore = new UserStore<AppMember, AppRole>(new DbManager(connectionString));
        }

        //This method is used to check the user credentials
        public bool ValidateUser(string username, string password)
        {
            string hash = _userStore.GetPasswordHashAsync(new AppMember { UserName = username, Email = username }).Result;
            bool result = Crypto.VerifyHashedPassword(hash, password);
            return result;
        }

    }
}