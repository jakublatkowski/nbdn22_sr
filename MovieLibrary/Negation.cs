namespace TrainingPrep.collections
{
    public class Negation<TItem> : Criteria<TItem>
    {
        private readonly Criteria<TItem> _criteriaToNegate;

        public Negation(Criteria<TItem> criteriaToNegate)
        {
            _criteriaToNegate = criteriaToNegate;
        }

        public bool IsSatisfiedBy(TItem item)
        {
            return !_criteriaToNegate.IsSatisfiedBy(item);
        }
    }
}