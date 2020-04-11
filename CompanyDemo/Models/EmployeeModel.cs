using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDemo.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public DateTime BirthdayDate { get; set; }
        public DateTime SignInDate { get; set; }
        public DateTime ResignedDate { get; set; }
        public bool IsResigned { get; set; }
        public int Salary { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}