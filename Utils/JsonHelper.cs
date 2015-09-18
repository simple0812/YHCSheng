using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace YHCSheng.Utils {

    public sealed class JsonHelper {
        private JsonHelper() {}
        public static readonly JsonHelper Instance = new JsonHelper();

        public string SerializeObject(Object obj, bool convertFirstLetter = true) {
            if(obj == null) return "";
            if(!convertFirstLetter)
                return JsonConvert.SerializeObject(obj);

            return JsonConvert.SerializeObject(ConvertToDictionary(obj)); 
        }


        public string SerializeObject(ICollection collection, bool convertFirstLetter = true) {
            if(collection == null) return "";
            if(!convertFirstLetter) return JsonConvert.SerializeObject(collection);

           return JsonConvert.SerializeObject(ConvertToDictionary(collection));
        }

        public Dictionary<string, object> ConvertToDictionary(Object obj) {
            var type = obj.GetType();
            Dictionary<string, object> dic = new Dictionary<string, object>();

            foreach (PropertyInfo p in type.GetProperties()) {
                string key = "";
                bool ignore = false;

                var chars = p.Name.ToCharArray();
                chars[0] = char.ToLower(chars[0]);
                key = new string(chars);

                var attribute = p.GetCustomAttribute(typeof(CustomJsonAttribute), false) as CustomJsonAttribute;
                if (attribute != null) {
                    if(!string.IsNullOrEmpty(attribute.Key)) key = attribute.Key;
                    ignore = attribute.Ignore;
                }

                if(!ignore) {
                    dic.Add(key, p.GetValue(obj, null));
                }
            }

           return dic;
        }

        public IList<Dictionary<string, object>> ConvertToDictionary(ICollection collection) {
            IList<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (var obj in collection){
                list.Add(ConvertToDictionary(obj));
            }

            return list;
        }
        
    }
}
