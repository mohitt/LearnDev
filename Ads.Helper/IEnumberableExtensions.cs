using System;
using System.Collections.Generic;

namespace Ads.Helper
{
    public static class IEnumberableExtensions
    {
      public static  IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var e in source) {
                action(e);
                yield return e;
            }
        }
    }
}