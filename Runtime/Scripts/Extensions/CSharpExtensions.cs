using System.Collections.Generic;

namespace Common.Runtime.Extensions
{
    public static class CSharpExtensions
    {
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        
        public static void CreateDefault<TSource>(this IList<TSource> list) where TSource : new() => list.Add(new TSource());
    }
}