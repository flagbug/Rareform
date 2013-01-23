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
            this.wrapped = list;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { this.wrapped.CollectionChanged += value; }
            remove { this.wrapped.CollectionChanged -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { ((INotifyPropertyChanged)this.wrapped).PropertyChanged += value; }
            remove { ((INotifyPropertyChanged)this.wrapped).PropertyChanged -= value; }
        }

        public IEnumerator<TType> GetEnumerator()
        {
            return this.wrapped.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}