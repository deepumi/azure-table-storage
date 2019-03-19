using System.Collections.Generic;

namespace Azure.TableStorage.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static string First(this IEnumerable<string> source)
        {
            if (source == null) return string.Empty;
             
            using (IEnumerator<string> e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    return e.Current;
                }
            }

            return string.Empty; 
        }
    }
}