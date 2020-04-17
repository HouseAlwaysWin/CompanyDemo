using CompanyDemo.Domain.DTOs;
using CompanyDemoAdmin.Controllers.Base;
using CompanyDemoAdmin.Models;
using DBAccess.Dapper.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CompanyDemoAdmin.Controllers
{

    //[Authorize(Roles = "Administrators")]
    public class AdminController : BaseController
    {
        private UserStore<AppMember, AppRole> _userManager;
        private RoleStore<AppRole> _roleManager;

        public AdminController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _userManager = new UserStore<AppMember, AppRole>(new DbManager(connectionString));
            _roleManager = new RoleStore<AppRole>(new DbManager(connectionString));
        }


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ShowFrontUsersLogin()
        {
            return View();
        }

        public ActionResult ShowBackUsersLogin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FindAllUsers(int currentPage, int itemsPerPage, bool isDesc = false)
        {
            Jsend<OneToManyMap<AppMember>> result = null;
            try
            {
                result = _userManager.FindAllUsers(currentPage, itemsPerPage, isDesc);
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("FindAllUsers occured error"));
        }


        [HttpGet]
        public ActionResult FindAllRoles(int currentPage, int itemsPerPage, bool isDesc = false)
        {
            Jsend<OneToManyMap<AppRole>> result = null;
            try
            {
                result = _roleManager.FindAllRoles(currentPage, itemsPerPage, isDesc);
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("FindAllRoles occured error"));
        }



        [HttpGet]
        public ActionResult GetRolesByUser(int id, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            Jsend<OneToManyMap<IdentityRole>> result = null;

            try
            {
                result = _userManager.GetRolesByUser(new AppMember { Id = id }, currentPage, itemsPerPage, isDesc);
                return Jsend(result);

            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("GetRolesByUser occured error"));
        }


        [HttpGet]
        public ActionResult GetNotInRolesByUser(int id, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            Jsend<OneToManyMap<IdentityRole>> result = null;

            try
            {
                result = _userManager.GetNotInRolesByUser(new AppMember { Id = id }, currentPage, itemsPerPage, isDesc);
                return Jsend(result);

            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("GetRolesByUser occured error"));
        }


        [HttpPost]
        public ActionResult AddRole(string roleName)
        {
            try
            {
                _roleManager.CreateAsync(new AppRole
                {
                    Name = roleName
                });
                return Jsend(JsendResult.Success());
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("AddRole occured error"));
        }

        [HttpPost]
        public ActionResult UpdateRole(int roleId, string roleName)
        {

            try
            {
                _roleManager.UpdateAsync(new AppRole
                {
                    Id = roleId,
                    Name = roleName
                });
                return Jsend(JsendResult.Success());
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("UpdateRole occured error"));
        }

        [HttpPost]
        public ActionResult DeleteRoleByID(int id)
        {
            try
            {
                _roleManager.DeleteAsync(new AppRole
                {
                    Id = id,
                });
                return Jsend(JsendResult.Success());

            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("DeleteRole occured error"));
        }


        [HttpPost]
        public ActionResult AddRoleToUser(int userId, string roleName)
        {
            try
            {
                _userManager.AddToRoleAsync(new AppMember
                {
                    Id = userId
                }, roleName);
                return Jsend(JsendResult.Success());

            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("AddRoleToUser occured error"));

        }


        [HttpPost]
        public ActionResult AddRolesToUser(int userId, List<int> roleIds)
        {
            try
            {
                _userManager.AddMultipleRoles(new AppMember
                {
                    Id = userId
                }, roleIds);
                return Jsend(JsendResult.Success());

            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("AddRoleToUser occured error"));

        }

        [HttpPost]
        public ActionResult RemoveRoleFromUser(int userId, int roleId)
        {
            try
            {
                _userManager.RemoveRoleFromUser(userId, roleId);
                return Jsend(JsendResult.Success());
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("RemoveRoleFromUser occured error"));
        }


        [HttpGet]
        public ActionResult GetUsersByTypeAndLoginState(int memberType, bool isLogin, int currentPage, int itemsPerPage, bool isDesc)
        {
            try
            {
                var result = _userManager.GetUsersByTypeAndLoginState(memberType, isLogin, currentPage, itemsPerPage, isDesc);
                Jsend<OneToManyMap<UserViewModel>> jsendUserViewModel = new Jsend<OneToManyMap<UserViewModel>>()
                {
                    data = new OneToManyMap<UserViewModel>
                    {
                        List = new List<UserViewModel>()
                    }
                };
                List<UserViewModel> mapData = new List<UserViewModel>();
                foreach (var user in result.data.List)
                {
                    mapData.Add(new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        MemberType = user.MemberType,
                        IsLoggined = user.IsLogined
                    });
                }
                jsendUserViewModel.status = result.status;
                jsendUserViewModel.message = result.message;
                jsendUserViewModel.code = result.code;
                if (result.data != null)
                {
                    jsendUserViewModel.data.List = mapData;
                    jsendUserViewModel.data.TotalCount = mapData.Count;
                }

                return Jsend(jsendUserViewModel);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
            return Jsend(JsendResult.Error("GetUsersByTypeAndLoginState occured error"));

        }




    }


}
