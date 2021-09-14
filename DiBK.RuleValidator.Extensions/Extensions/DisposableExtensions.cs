using System;
using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class DisposableExtensions
    {
        public static void Dispose(this IEnumerable<IDisposable> collection)
        {
            foreach (IDisposable item in collection)
            {
                if (item != null)
                {
                    try
                    {
                        item.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public static DisposableList<T> ToDisposableList<T>(this IEnumerable<T> enumerable) where T : IDisposable
        {
            var list = new DisposableList<T>();

            foreach (var item in enumerable)
                list.Add(item);

            return list;
        }
    }
}
