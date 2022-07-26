using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Rareform.Collections
{
    public class ObservableHashSet<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly ObservableCollection<T> backingCollection;
        private readonly HashSet<T> backingSet;

        public ObservableHashSet()
        {
            backingSet = new HashSet<T>();
            backingCollection = new ObservableCollection<T>();
        }

        public int Count => backingSet.Count;

        public bool IsReadOnly => false;

        public void Clear()
        {
            backingSet.Clear();
            backingCollection.Clear();
        }

        public bool Contains(T item)
        {
            return backingSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            backingSet.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return backingSet.GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            if (backingSet.Add(item)) backingCollection.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(T item)
        {
            var removed = backingSet.Remove(item);

            if (removed) backingCollection.Remove(item);

            return removed;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => backingCollection.CollectionChanged += value;
            remove => backingCollection.CollectionChanged -= value;
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => ((INotifyPropertyChanged)backingCollection).PropertyChanged += value;
            remove => ((INotifyPropertyChanged)backingCollection).PropertyChanged -= value;
        }

        public bool Add(T item)
        {
            var added = backingSet.Add(item);

            if (added) backingCollection.Add(item);

            return added;
        }

        #region Future .NET 4.0 Support

        /*
        public void ExceptWith(IEnumerable<T> other)
        {
            var list = new List<T>(other);

            this.backingSet.ExceptWith(list);

            foreach (T item in list)
            {
                while (this.backingCollection.Remove(item))
                { }
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            var list = new List<T>(other);

            this.backingSet.IntersectWith(list);

            IEnumerable<T> removable = this.backingCollection.Except(list, this.backingSet.Comparer);

            foreach (T item in removable)
            {
                while (this.backingCollection.Remove(item))
                { }
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.backingSet.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.backingSet.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.backingSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.backingSet.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.backingSet.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return this.backingSet.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            var list = new List<T>(other);

            this.backingSet.SymmetricExceptWith(list);

            IEnumerable<T> removable = this.backingCollection.Except(list);

            foreach (T item in removable)
            {
                while (this.backingCollection.Remove(item))
                { }
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            foreach (T item in other)
            {
                this.Add(item);
            }
        }
        */

        #endregion Future .NET 4.0 Support
    }
}