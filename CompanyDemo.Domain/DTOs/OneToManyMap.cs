using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDemo.Domain.DTOs
{

    public class OneToManyMap<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
    }
    public class OneToManyMap<T, T2>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
        public T2 MapData { get; set; }
    }
}
