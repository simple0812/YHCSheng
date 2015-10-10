using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace YHCSheng.Utils {
    public sealed class JsonHelper {
        public static readonly JsonHelper Instance = new JsonHelper();
        private JsonHelper() {}

        public string SerializeObject(object obj, bool convertFirstLetter = true) {
            return obj == null ? "" : JsonConvert.SerializeObject(!convertFirstLetter ? obj : ConvertToDictionary(obj));
        }

        public string SerializeObject(ICollection collection, bool convertFirstLetter = true) {
            if (collection == null) return "";
            return convertFirstLetter ? JsonConvert.SerializeObject(ConvertToDictionary(collection)) : JsonConvert.SerializeObject(collection);
        }

        public Dictionary<string, object> ConvertToDictionary(object obj) {
            var type = obj.GetType();
            var dic = new Dictionary<string, object>();

            foreach (var p in type.GetProperties()) {
                var key = "";
                var ignore = false;

                var chars = p.Name.ToCharArray();
                chars[0] = char.ToLower(chars[0]);
                key = new string(chars);

                var attribute = p.GetCustomAttribute(typeof (CustomJsonAttribute), false) as CustomJsonAttribute;
                if (attribute != null) {
                    if (!string.IsNullOrEmpty(attribute.Key)) key = attribute.Key;
                    ignore = attribute.Ignore;
                }

                if (!ignore) dic.Add(key, p.GetValue(obj, null));
            }

            return dic;
        }

        public IList<Dictionary<string, object>> ConvertToDictionary(ICollection collection) {
            IList<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (var obj in collection) {
                list.Add(ConvertToDictionary(obj));
            }

            return list;
        }
    }
}