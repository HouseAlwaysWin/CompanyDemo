using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDemo.Models
{
    public class GenericPaginationModel<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
    }

}