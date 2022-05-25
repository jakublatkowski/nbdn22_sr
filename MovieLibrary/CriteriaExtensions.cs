using TrainingPrep.collections;

public static class CriteriaExtensions
{
    public static Conjunction<TItem> And<TItem>(this Criteria<TItem> c1, Criteria<TItem> c2)
    {
        return new Conjunction<TItem>(c1,c2);
    }

    public static Alternative<TItem> Or<TItem>(this Criteria<TItem> c1, Criteria<TItem> c2)
    {
        return new Alternative<TItem>(c1,
            c2);
    }
}