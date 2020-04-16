using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDemo.Domain.Entities
{
    public class EmployeeTAndCompanyT
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeePhone { get; set; }

        public string Email { get; set; }
        public DateTime BirthdayDate { get; set; }
        public DateTime SignInDate { get; set; }
        public DateTime ResignedDate { get; set; }
        public bool IsResigned { get; set; }
        public int Salary { get; set; }

        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string TaxID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string WebsiteURL { get; set; }
        public string Owner { get; set; }
    }
}
