using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Data;

namespace EnterpriseMaterial.Common
{
    public class JsonNetHelper
    {
        /// <summary>
        /// 格式化日期+序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializetoJson(object obj)
        {
            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss";//格式化时间，默认是ISO8601格式


            var setting = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"//格式化日期
            };
            string str = JsonConvert.SerializeObject(obj, Formatting.Indented, setting);
            return str;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss";//格式化时间，默认是ISO8601格式
            string str = JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter);
            return JsonConvert.DeserializeObject<List<T>>(str);
        }

        /// <summary>
        /// 小驼峰命名+格式化日期+序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerialzeoJsonForCamelCase(object obj)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"//格式化日期
            };
            string str = JsonConvert.SerializeObject(obj, Formatting.Indented, setting);
            return str;
        }
    }
}
