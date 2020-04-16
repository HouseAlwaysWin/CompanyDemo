using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDemo.Domain.Entities
{
    public class ProductTAndCompanyT
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }

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
