using System;

namespace TrainingPrep.collections
{
    public class Where<TItem>  
    {
        public static CriteriaBuilder<TItem,TProperty> HasAn<TProperty>(Func<TItem, TProperty> selector) where TProperty:IComparable<TProperty>
        {
            return new CriteriaBuilder<TItem,TProperty>(selector);
        }
    }
}