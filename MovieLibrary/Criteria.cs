namespace TrainingPrep.collections
{
    public interface Criteria<TItem>
    {
        public bool IsSatisfiedBy(TItem item);

    }
}