using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDemo.Domain.DTOs
{
    public class ManyToManyMap<T, T2>
    {
        public int TotalCount1 { get; set; }
        public int TotalCount2 { get; set; }
        public IEnumerable<T> List { get; set; }
        public IEnumerable<T2> List2 { get; set; }
    }
}
