using System;
using TrainingPrep.collections;

public static class CriteriaBuilderExtensions
{
    public static Criteria<TItem> EqualTo<TItem, TProperty>(this SelectorHolder<TItem, TProperty> selectorHolder, TProperty property)
    {
        return new AnonymousCriteria<TItem>(movie =>
        {
            if (selectorHolder._negation)
            {
                return !selectorHolder._selector(movie).Equals(property);
            }
            else
            {
                return selectorHolder._selector(movie).Equals(property);
            }
        });
    }

    public static Criteria<TItem> GreaterThan<TItem, TProperty>(this SelectorHolder<TItem, TProperty> selectorHolder, TProperty property) where TProperty : IComparable<TProperty>
    {
        return new AnonymousCriteria<TItem>(movie => property.CompareTo(selectorHolder._selector(movie))<0);
    }
}