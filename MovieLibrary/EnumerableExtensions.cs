using System.Collections.Generic;
using TrainingPrep.collections;

public static class EnumerableExtensions
{
    public static IEnumerable<TItem> OneAtATime<TItem>(this IEnumerable<TItem> collection)
    {
        foreach (var item in collection)
        {
            yield return item;
        }
    }
}