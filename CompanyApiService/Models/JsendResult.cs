using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public class Jsend<T>
    {
        public string status { get; set; }
        public T data { get; set; }
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

        public static Jsend<T> Success(T data)
        {
            return new Jsend<T>()
            {
                status = JsendResultStatus.success.ToString().ToLower(),
                data = data
            };
        }


        public static Jsend<T> Success()
        {
            return new Jsend<T>()
            {
                status = JsendResultStatus.success.ToString().ToLower(),
            };
        }


        public static Jsend<T> Fail(T data)
        {

            return new Jsend<T>()
            {
                status = JsendResultStatus.fail.ToString(),
                data = data
            };
        }

        public static Jsend<T> Error(string message)
        {
            return new Jsend<T>()
            {
                status = JsendResultStatus.error.ToString(),
                message = message,
            };
        }

        public static Jsend<T> Error(string message, string code, T data)
        {

            return new Jsend<T>()
            {
                status = JsendResultStatus.error.ToString(),
                message = message,
                data = data,
                code = code
            };
        }
    }
}