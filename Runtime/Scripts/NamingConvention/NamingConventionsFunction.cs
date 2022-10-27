using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Common.Runtime.NamingConvention
{
    public partial class NamingConventions
    {

        public static string GetName(string prefix, string name, string variant = null, string suffix = null)
        {
            List<string> values = new List<string>();
            if (!prefix.IsNullOrEmpty()) values.Add(prefix);
            values.Add(name);
            if (!variant.IsNullOrEmpty()) values.Add(variant);
            if (!suffix.IsNullOrEmpty()) values.Add(suffix);

            string result = "";
            for (var i = 0; i < values.Count; i++)
            {
                bool first = i == 0;
                bool last = i == values.Count - 1;

                result += $"{values[i]}{(last ? "" : "_")}";
            }

            return result;
        }
        
        public static string GetName(Object @object, string name = null, string variant = null, string suffix = null)
        {
            Type objectType = @object.GetType();
            string prefix = "";
            if (_typePrefixDeleration.ContainsKey(objectType)) prefix = _typePrefixDeleration[objectType];
            return GetName(prefix, name.IsNullOrEmpty() ? @object.name : name, variant, suffix);
        }
    }
}