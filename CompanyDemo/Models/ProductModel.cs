using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanDemo.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}