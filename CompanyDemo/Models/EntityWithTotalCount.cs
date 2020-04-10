using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDemo.Models
{
    public class EntityWithTotalCount<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
    }

    public class EntityWithTotalCount<T, T2>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
        public T2 MapData { get; set; }
    }

}