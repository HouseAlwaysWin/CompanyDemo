﻿using CompanyApiService.Models;
using DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CompanyApiService.Services
{
    public interface ICompanyService
    {
        Jsend AddCompany(CompanyModel data);
    }
}
