﻿using CompanyDemoAdmin.Models;
using DBAccess.Dapper.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CompanyDemoAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private UserStore<AppMember, AppRole> _userStore;
        public MvcApplication()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _userStore = new UserStore<AppMember, AppRole>(new DbManager(connectionString));
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest()
        {
            var loggedInUsers = (Dictionary<string, DateTime>)HttpRuntime.Cache["LoggedInUsers"];

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                if (loggedInUsers != null)
                {
                    loggedInUsers[userName] = DateTime.Now;
                    HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
                }
            }

            if (loggedInUsers != null)
            {
                foreach (var item in loggedInUsers.ToList())
                {
                    if (item.Value < DateTime.Now.AddMinutes(-1))
                    {
                        loggedInUsers.Remove(item.Key);
                        _userStore.SetLoginState(item.Key, false);
                    }
                }
                HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
            }

        }
    }
}
