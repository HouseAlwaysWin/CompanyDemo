using CompanyDemo.Domain.DTOs;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Text;

namespace CompanyDemoAdmin.Helpers
{
    public class RequestHelper
    {

        public static T MakeGetWebRequest<T>(string url, string contentType = "application/json")
        {
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers[HttpRequestHeader.ContentType] = contentType;
                wc.Credentials = new NetworkCredential("martin", "123456");
                var result = wc.DownloadString(url);
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static O MakePostWebRequest<I, O>(string url, I data, string method = "POST", string contentType = "application/json")
        {
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers[HttpRequestHeader.ContentType] = contentType;
                wc.Credentials = new NetworkCredential("martin", "123456");
                var jsonData = JsonConvert.SerializeObject(data);
                var result = wc.UploadString(url, method, jsonData);
                return JsonConvert.DeserializeObject<O>(result);
            }
        }

        public static TokenResult GetToken(string username, string password)
        {
            string data = $"username={username}&password={password}&grant_type=password";
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = wc.UploadString(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, data);
                return JsonConvert.DeserializeObject<TokenResult>(result);
            }
        }

    }
}