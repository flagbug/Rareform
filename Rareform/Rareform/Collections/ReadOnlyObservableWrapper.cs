using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Rareform.Collections
{
    public class ReadOnlyObservableWrapper<TCollection, TType> : IReadOnlyObservableWrapper<TType>
        where TCollection : IEnumerable<TType>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly TCollection wrapped;

        public ReadOnlyObservableWrapper(TCollection list)
        {
            wrapped = list;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => wrapped.CollectionChanged += value;
            remove => wrapped.CollectionChanged -= value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => wrapped.PropertyChanged += value;
            remove => wrapped.PropertyChanged -= value;
        }

        public IEnumerator<TType> GetEnumerator()
        {
            return wrapped.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}