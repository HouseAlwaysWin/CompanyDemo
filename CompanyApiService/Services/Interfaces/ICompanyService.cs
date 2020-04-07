﻿using CompanyApiService.Models;
using DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CompanyApiService.Services.Interfaces
{
    public interface ICompanyService
    {

        Jsend FindCompanyListByPage(int current, int itemsPerPages);
        Jsend AddCompany(CompanyModel data);

        Jsend FindComapnyByName(string name);

        Jsend FindCompanyByID(int Id);

        Jsend UpdateCompany(CompanyModel data);
        Jsend InsertUpdateCompany(CompanyModel data);


        Jsend DeleteCompanyByID(int id);

        Jsend DeleteCompany(CompanyModel data);
    }
}
