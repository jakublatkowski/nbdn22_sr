using System;
using TrainingPrep.collections;

namespace TrainingPrep.collections
{
    public class CriteriaBuilder<TItem, TProperty> {
        private readonly Func<TItem, TProperty> _selector;

        public CriteriaBuilder(Func<TItem, TProperty> selector)
        {
            _selector = selector;
        }

        public Criteria<TItem> EqualTo(TProperty property)
        {
            return new AnonymousCriteria<TItem>(movie => _selector(movie).Equals(property));
        }

        public Criteria<TItem> GreaterThan<TComparableProperty>(TComparableProperty property) 
                                                where TComparableProperty : IComparable<TProperty>
        {
            return new AnonymousCriteria<TItem>(movie => property.CompareTo(_selector(movie))<0);
        }
    }
}