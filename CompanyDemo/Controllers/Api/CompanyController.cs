using CompanyDemo.Helpers;
using CompanyDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanyDemo.Controllers.Api
{

    public class CompanyController : ApiBaseController
    {

        [HttpGet]
        public ActionResult GetCompanyList(int page)
        {
            var data = RequestHelper.MakeGetWebRequest<CompanyModel, Jsend<GenericPaginationModel<CompanyModel>>>(
                $"https://localhost:44319/api/Company/GetCompanyAllByPage?current={page}&itemsPerPages=10");
            return Jsend(data);
        }
    }
}