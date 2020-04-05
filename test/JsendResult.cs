using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test
{
    public enum JsendResultStatus
    {
        success,
        fail,
        error
    }

    public class Jsend<T>
    {
        public string status { get; set; }
        public T data { get; set; }
        public string message { get; set; }
        public string code { get; set; }
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