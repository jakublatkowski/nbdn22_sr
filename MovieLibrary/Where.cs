using System;

namespace TrainingPrep.collections
{
    public class Where<TItem>  
    {
        public static SelectorHolder<TItem,TProperty> HasAn<TProperty>(Func<TItem, TProperty> selector) 
        {
            return new SelectorHolder<TItem,TProperty>(selector);
        }
    }
}