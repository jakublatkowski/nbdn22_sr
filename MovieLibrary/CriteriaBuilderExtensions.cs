using System;
using TrainingPrep.collections;

public static class CriteriaBuilderExtensions
{
    public static Criteria<TItem> EqualTo<TItem, TProperty>(this SelectorHolder<TItem, TProperty> selectorHolder, TProperty property)
    {
        var criteria = new AnonymousCriteria<TItem>(movie =>
            selectorHolder._selector(movie).Equals(property)

        );
        return selectorHolder.AplyNegation(criteria);
    }

    public static Criteria<TItem> GreaterThan<TItem, TProperty>(this SelectorHolder<TItem, TProperty> selectorHolder, TProperty property) where TProperty : IComparable<TProperty>
    {
        var criteria = new AnonymousCriteria<TItem>(movie => property.CompareTo(selectorHolder._selector(movie))<0);
        return selectorHolder.AplyNegation(criteria);
    }
}