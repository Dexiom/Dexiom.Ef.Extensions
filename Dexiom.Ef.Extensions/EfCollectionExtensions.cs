using System;
using System.Collections.Generic;
using System.Linq;

namespace Dexiom.Ef.Extensions
{
    public static class EfCollectionExtensions
    {
        public static void SyncWith<T>(this ICollection<T> source, ICollection<T> newList, Func<T, int> getId)
            where T : class
        {
            if (newList?.Any() ?? false)
            {
                var sourceIds = source.Select(getId).ToList();
                var newIds = newList.Select(getId).ToList();
                var toBeRemoved = sourceIds.Except(newIds).ToList();
                var toBeAdded = newIds.Except(sourceIds).ToList();
                
                foreach (var item in source.Where(n => toBeRemoved.Contains(getId(n))).ToList())
                    source.Remove(item);
                
                foreach (var item in newList.Where(n => toBeAdded.Contains(getId(n))))
                    source.Add(item);
            }
            else
            {
                source?.Clear();
            }
        }
    }
}
