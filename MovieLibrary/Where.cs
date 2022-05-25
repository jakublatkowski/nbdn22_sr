using System;

namespace TrainingPrep.collections
{
    public class Where<TItem>  
    {
        public static CriteriaBuilder<TItem,TProperty> HasAn<TProperty>(Func<TItem, TProperty> selector) 
        {
            return new CriteriaBuilder<TItem,TProperty>(selector);
        }
    }
}