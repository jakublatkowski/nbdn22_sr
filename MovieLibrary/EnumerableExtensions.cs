using System;
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

    public static IEnumerable<TItem> ThatSatisfy<TItem>(this IEnumerable<TItem> items, Predicate<TItem> condition)
    {
        foreach (var item in items)
        {
            if (condition(item))
            {
                yield return item;
            }
        }
    }
}