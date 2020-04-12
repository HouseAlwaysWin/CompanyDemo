using Newtonsoft.Json;
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
                var jsonData = JsonConvert.SerializeObject(data);
                var result = wc.UploadString(url, method, jsonData);
                return JsonConvert.DeserializeObject<O>(result);
            }
        }

    }
}