using System;
using System.Text;
using System.Data;
using System.Reflection;

namespace YHCSheng.Utils {

    [System.AttributeUsage(AttributeTargets.Property)]
    public class CustomJsonAttribute : Attribute {
        public string Key { get; set; }
        public bool Ignore { get; set; }

        public CustomJsonAttribute(string key, bool ignore) {
            Key = key;
            Ignore = ignore;
        }

        public CustomJsonAttribute(string key) {
            Key = key;
        }

        public CustomJsonAttribute(bool ignore) {
            Ignore = ignore;
        }
    }
}