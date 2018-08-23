using System;

namespace enki.common.core.WebUtils
{
    [Obsolete("Use Tuple<> instead, this will be serializable.")]
    public class KeyValueSerializable
    {
        public string key { get; set; }
        public object value { get; set; }

        public KeyValueSerializable() { }
        public KeyValueSerializable(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
