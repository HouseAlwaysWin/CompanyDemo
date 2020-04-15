using CompanyApiService.Models;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using FluentValidation.Results;
using System.Collections.Generic;

namespace CompanyApiService.Services.Interfaces
{
    public interface ICompanyService
    {

        Jsend<OneToManyMap<CompanyT>> FindCompanyListByID(int current, int itemsPerPage, bool isDesc, int? searchText);
        Jsend<OneToManyMap<CompanyT>> FindCompanyListByName(int current, int itemsPerPage, bool isDesc, string searchText);
        Jsend<List<ValidationFailure>> AddCompany(CompanyModel data);

        Jsend<CompanyT> FindComapnyByName(string name);

        Jsend<CompanyT> FindCompanyByID(int Id);

        Jsend<List<ValidationFailure>> UpdateCompany(CompanyModel data);
        Jsend<List<ValidationFailure>> InsertUpdateCompany(CompanyModel data);


        Jsend DeleteCompanyByID(int id);

        Jsend DeleteCompany(CompanyModel data);
    }
}
