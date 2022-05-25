using System;
using TrainingPrep.collections;

namespace TrainingPrep.collections
{
    public class SelectorHolder<TItem, TProperty> {
        public readonly Func<TItem, TProperty> _selector;
        public bool _negation;

        public SelectorHolder(Func<TItem, TProperty> selector)
        {
            _negation = false;
            _selector = selector;
        }

        public SelectorHolder<TItem,TProperty> Not()
        {
            _negation = !_negation;
            return this;
        }
    }
}