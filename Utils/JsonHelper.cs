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

    public class CustomJsonResult {
        public string Code {get;set;}
        public string Message {get;set;}
        public object Result {get;set;}

        private CustomJsonResult() {}
        public static readonly CustomJsonResult Instance = new CustomJsonResult();

        
        public string GetSuccess(object result) {
            this.Code = "success";
            this.Message = "";
            this.Result = JsonHelper.Instance.ConvertToDictionary(result );

            return JsonHelper.Instance.SerializeObject(this);

        }

        public string GetSuccess(ICollection collection) {
            this.Code = "success";
            this.Message = "";
            this.Result = JsonHelper.Instance.ConvertToDictionary(collection);

            return JsonHelper.Instance.SerializeObject(this);

        }
        
        public string GetError(string message) {
            this.Code = "error";
            this.Message = message;
            this.Result = "";

            return JsonHelper.Instance.SerializeObject(this);
        }


        public string PageError(string message) {
            this.Code = "error";
            this.Message = message;
            this.Result = new {
                entities = "",
                total = 0
            };

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string PageSuccess(ICollection collection, int total = 0) {

            if(total == 0) total = collection.Count;
            this.Code = "error";
            this.Message = "";
            this.Result = new {
                entities = JsonHelper.Instance.ConvertToDictionary(collection),
                total = total
            };

            return JsonHelper.Instance.SerializeObject(this);
        }
    }
}
