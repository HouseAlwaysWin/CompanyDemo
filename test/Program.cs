using DBAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = JsendResult<object>.Success(new { test = "test" });
            var result1 = JsendResult<object>.Fail(new { test = "test" });
            var result2 = JsendResult<object>.Error("errormessage");


            string resultJson = JsonConvert.SerializeObject(result);
            string resultJson1 = JsonConvert.SerializeObject(result1);
            string resultJson2 = JsonConvert.SerializeObject(result2);



            Console.WriteLine(resultJson);
            Console.WriteLine(resultJson1);
            Console.WriteLine(resultJson2);

        }
    }
}
