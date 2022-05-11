using System.Collections;
using System.Collections.Generic;

namespace TrainingPrep.collections
{
    public class ReadOnlySetOf<TItem> : IEnumerable<TItem>
    {
        private readonly IEnumerable<TItem> _items;

        public ReadOnlySetOf(IEnumerable<TItem> items)
        {
            _items = items;
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}