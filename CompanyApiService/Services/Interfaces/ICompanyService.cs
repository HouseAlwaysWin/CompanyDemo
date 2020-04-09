using CompanyApiService.Models;
using DBAccess.DTO;
using DBAccess.Entities;
using FluentValidation.Results;
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

        Jsend<EntityWithTotalCount<CompanyT>> FindCompanyListByID(int current, int itemsPerPages, bool isDesc, int? searchText, string sortBy = "CompanyID");
        Jsend<EntityWithTotalCount<CompanyT>> FindCompanyListByName(int current, int itemsPerPages, bool isDesc, string searchText, string sortBy = "CompanyID");
        Jsend<List<ValidationFailure>> AddCompany(CompanyModel data);

        Jsend<CompanyT> FindComapnyByName(string name);

        Jsend<CompanyT> FindCompanyByID(int Id);

        Jsend<List<ValidationFailure>> UpdateCompany(CompanyModel data);
        Jsend<List<ValidationFailure>> InsertUpdateCompany(CompanyModel data);


        Jsend DeleteCompanyByID(int id);

        Jsend DeleteCompany(CompanyModel data);
    }
}
