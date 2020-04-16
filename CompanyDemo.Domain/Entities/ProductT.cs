using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDemo.Domain.Entities
{
    public class ProductT
    {
        public int ProductID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
