using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDemo.Domain.DTOs
{
    public class Jsend
    {
        public string status { get; set; }
        public string data { get; set; }
        public string message { get; set; }
        public string code { get; set; }
    }

    public class Jsend<T>
    {
        public string status { get; set; }
        public T data { get; set; }
        public string message { get; set; }
        public string code { get; set; }
    }
}
