using System;
using TrainingPrep.collections;

namespace TrainingPrep.collections
{
    public class SelectorHolder<TItem, TProperty> {
        public readonly Func<TItem, TProperty> _selector;
        private bool _negation;

        public SelectorHolder(Func<TItem, TProperty> selector): this(selector, false)
        {
        }

        private SelectorHolder(Func<TItem, TProperty> selector, bool negation) 
        {
            _selector = selector;
            _negation = negation;
        }

        public SelectorHolder<TItem, TProperty> Not()
        {
            return new SelectorHolder<TItem, TProperty>(_selector,!_negation);
        }

        public Criteria<TItem> AplyNegation(AnonymousCriteria<TItem> criteria)
        {
            if (_negation)
                return new Negation<TItem>(criteria);
            else
                return criteria;
        }
    }
}