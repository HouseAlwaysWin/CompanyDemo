using CompanyDemo.Domain.DTOs;
using CompanyDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CompanyDemo.Controllers.Base
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 預設序列化為Json時
        /// 1.設定如果是null的欄位不輸出
        /// 2.DateTime無論其DateTimeKind皆視為UTC時間
        /// 3.Long Integer使用字串表示
        /// </summary>
        private JsonSerializerSettings JsonSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            Converters = { new LongTypeToStringConverter() }
        };


        public ActionResult Jsend(Jsend data)
        {
            switch (data.status)
            {
                case "success":
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    break;

                case "fail":
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case "error":
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var json = JsonConvert.SerializeObject(data);
            return Content(json, "application/json", Encoding.UTF8);
        }

        public ActionResult Jsend<T>(Jsend<T> data)
        {
            switch (data.status)
            {
                case "success":
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    break;

                case "fail":
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case "error":
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var json = JsonConvert.SerializeObject(data);
            return Content(json, "application/json", Encoding.UTF8);
        }

        /// <summary>
        /// 轉換Long為String，避免JS造成精度遺失
        /// </summary>
        internal sealed class LongTypeToStringConverter : JsonConverter
        {
            public override bool CanRead => false;
            public override bool CanWrite => true;
            public override bool CanConvert(Type type) => type == typeof(long);
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var longValue = ((long)value);
                //當JObject包含int數值，會被統一轉成long值，所以要做是否要轉成string的判斷
                writer.WriteValue((longValue > int.MaxValue || longValue < int.MinValue) ? value.ToString() : value);
            }
            public override object ReadJson(JsonReader reader, Type type, object existingValue, JsonSerializer serializer) => throw new NotSupportedException();
        }
    }
}