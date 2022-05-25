using System;
using TrainingPrep.collections;

namespace TrainingPrep.collections
{
    public class SelectorHolder<TItem, TProperty> {
        public readonly Func<TItem, TProperty> _selector;

        public SelectorHolder(Func<TItem, TProperty> selector)
        {
            _selector = selector;
        }
    }
}