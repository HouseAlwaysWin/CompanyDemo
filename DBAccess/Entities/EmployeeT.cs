﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccess.Entities
{
    public class EmployeeT
    {
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public DateTime BrithdayDate { get; set; }
        public DateTime SignInDate { get; set; }
        public DateTime ResignedDate { get; set; }
        public bool IsResigned { get; set; }
        public int Salary { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
