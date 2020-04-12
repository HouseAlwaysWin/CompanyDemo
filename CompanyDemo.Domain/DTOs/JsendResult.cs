using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDemo.Domain.DTOs
{
    public static class JsendResult
    {

        public static Jsend Success(string data)
        {
            return new Jsend()
            {
                status = EnumJsendStatus.success.ToString().ToLower(),
                data = data
            };
        }


        public static Jsend Success()
        {
            return new Jsend()
            {
                status = EnumJsendStatus.success.ToString().ToLower(),
            };
        }


        public static Jsend Fail(string data)
        {

            return new Jsend()
            {
                status = EnumJsendStatus.fail.ToString(),
                data = data
            };
        }

        public static Jsend Error(string message)
        {
            return new Jsend()
            {
                status = EnumJsendStatus.error.ToString(),
                message = message,
            };
        }

        public static Jsend Error(string message, string code, string data)
        {

            return new Jsend()
            {
                status = EnumJsendStatus.error.ToString(),
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
                status = EnumJsendStatus.success.ToString().ToLower(),
                data = data
            };
        }


        public static Jsend<T> Success()
        {
            return new Jsend<T>()
            {
                status = EnumJsendStatus.success.ToString().ToLower(),
            };
        }


        public static Jsend<T> Fail(T data)
        {

            return new Jsend<T>()
            {
                status = EnumJsendStatus.fail.ToString(),
                data = data
            };
        }

        public static Jsend<T> Error(string message)
        {
            return new Jsend<T>()
            {
                status = EnumJsendStatus.error.ToString(),
                message = message,
            };
        }

        public static Jsend<T> Error(string message, string code, T data)
        {

            return new Jsend<T>()
            {
                status = EnumJsendStatus.error.ToString(),
                message = message,
                data = data,
                code = code
            };
        }
    }
}
