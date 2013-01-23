using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Rareform.Collections
{
    public interface IReadOnlyObservableWrapper<T> : IEnumerable<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }
}