using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.DTO
{
    public class EntityWithTotalCount<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
