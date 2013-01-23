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
            this.backingSet = new HashSet<T>();
            this.backingCollection = new ObservableCollection<T>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { this.backingCollection.CollectionChanged += value; }
            remove { this.backingCollection.CollectionChanged -= value; }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { ((INotifyPropertyChanged)this.backingCollection).PropertyChanged += value; }
            remove { ((INotifyPropertyChanged)this.backingCollection).PropertyChanged -= value; }
        }

        public int Count
        {
            get { return this.backingSet.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Add(T item)
        {
            bool added = this.backingSet.Add(item);

            if (added)
            {
                this.backingCollection.Add(item);
            }

            return added;
        }

        public void Clear()
        {
            this.backingSet.Clear();
            this.backingCollection.Clear();
        }

        public bool Contains(T item)
        {
            return this.backingSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.backingSet.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.backingSet.GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            if (this.backingSet.Add(item))
            {
                this.backingCollection.Add(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Remove(T item)
        {
            bool removed = this.backingSet.Remove(item);

            if (removed)
            {
                this.backingCollection.Remove(item);
            }

            return removed;
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