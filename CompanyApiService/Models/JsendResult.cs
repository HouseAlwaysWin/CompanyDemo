using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyApiService.Models
{
    public enum JsendResultStatus
    {
        success,
        fail,
        error
    }

    public class Jsend
    {
        public string status { get; set; }
        public string data { get; set; }
        public string message { get; set; }
        public string code { get; set; }
    }


    public static class JsendResult
    {

        public static Jsend Success(string data)
        {
            return new Jsend()
            {
                status = JsendResultStatus.success.ToString().ToLower(),
                data = data
            };
        }

        public static Jsend Success()
        {
            return new Jsend()
            {
                status = JsendResultStatus.success.ToString().ToLower(),
            };
        }


        public static Jsend Fail(string data)
        {

            return new Jsend()
            {
                status = JsendResultStatus.fail.ToString(),
                data = data
            };
        }

        public static Jsend Error(string message)
        {
            return new Jsend()
            {
                status = JsendResultStatus.error.ToString(),
                message = message,
            };
        }

        public static Jsend Error(string message, string code, string data)
        {

            return new Jsend()
            {
                status = JsendResultStatus.error.ToString(),
                message = message,
                data = data,
                code = code
            };
        }
    }

    public static class JsendResult<T>
    {

        public static Jsend Success(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            return new Jsend()
            {
                status = JsendResultStatus.success.ToString().ToLower(),
                data = jsonData
            };
        }

        public static Jsend Success()
        {
            return new Jsend()
            {
                status = JsendResultStatus.success.ToString().ToLower(),
            };
        }


        public static Jsend Fail(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            return new Jsend()
            {
                status = JsendResultStatus.fail.ToString(),
                data = jsonData
            };
        }

        public static Jsend Error(string message)
        {
            return new Jsend()
            {
                status = JsendResultStatus.error.ToString(),
                message = message,
            };
        }

        public static Jsend Error(string message, string code, T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            return new Jsend()
            {
                status = JsendResultStatus.error.ToString(),
                message = message,
                data = jsonData,
                code = code
            };
        }
    }
}