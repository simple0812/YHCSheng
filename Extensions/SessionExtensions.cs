using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json;

namespace YHCSheng.Extensions {
    public static class SessionExtensions {
        public static bool? GetBoolean(this ISession session, string key) {
            var data = session.Get(key);
            return data == null ? (bool?) null : BitConverter.ToBoolean(data, 0);
        }

        public static void SetBoolean(this ISession session, string key, bool value) {
            session.Set(key, BitConverter.GetBytes(value));
        }


        public static T Get<T>(this ISession session, string key) where T : class {
            var data = session.GetString(key);
            return data == null ? null : JsonConvert.DeserializeObject<T>(data);
        }

        public static void Set<T>(this ISession session, string key, T value) where T : class => session.SetString(key,JsonConvert.SerializeObject(value));
    }
}
