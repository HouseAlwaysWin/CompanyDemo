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
            var data = RequestHelper.MakeGenericWebRequest<CompanyModel, Jsend>("", new CompanyModel { });
            return Jsend(data);
        }
    }
}