using System;
using TrainingPrep.collections;

namespace TrainingPrep.specs.MovieLibrarySpecs
{
    public class CriteriaBuilder<TItem, TProperty>
    {
        private readonly Func<TItem, TProperty> _selector;

        public CriteriaBuilder(Func<TItem, TProperty> selector)
        {
            _selector = selector;
        }

        public Criteria<TItem> EqualTo(TProperty property)
        {
            return new AnonymousCriteria<TItem>(movie => _selector(movie).Equals(property));
        }
    }
}