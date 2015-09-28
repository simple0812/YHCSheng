using System;

namespace YHCSheng.Utils {
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomJsonAttribute : Attribute {
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

        public string Key { get; set; }
        public bool Ignore { get; set; }
    }
}