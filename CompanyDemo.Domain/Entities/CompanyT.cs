using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDemo.Domain.Entities
{
    public class CompanyT
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string TaxID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string WebsiteURL { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
