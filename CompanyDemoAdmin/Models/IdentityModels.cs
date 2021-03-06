﻿using System.Security.Claims;
using System.Threading.Tasks;
using System.Configuration;
using DBAccess.Dapper.Identity;
using IdentityMember = DBAccess.Dapper.Identity.IdentityMember;
using Microsoft.AspNet.Identity;

namespace CompanyDemoAdmin.Models
{
    // You can add profile data for the AppMember by adding more properties to your AppMember class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppMember : IdentityMember
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppMember, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom AppMember claims here
            return userIdentity;
        }
    }

    public class AppRole : IdentityRole
    {

        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }
    }

    /// <summary>
    /// Create and opens a connection to a MSSql database
    /// </summary>

    public class ApplicationDbContext : DbManager
    {
        public ApplicationDbContext(string connectionName)
            : base(connectionName)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<AppMember>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}