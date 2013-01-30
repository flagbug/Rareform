using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Rareform.Collections
{
    public class ObservableList<T> : IList<T>, INotifyCollectionChanged
    {
        private readonly List<T> list;
        private double resetThreshold;

        public ObservableList()
        {
            this.list = new List<T>();
            this.resetThreshold = 0.3;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Capacity
        {
            get { return this.list.Capacity; }
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int index]
        {
            get { return this.list[index]; }
            set
            {
                T objectToReplace = this.list[index];

                this.list[index] = value;

                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, objectToReplace, index));
            }
        }

        public void Add(T item)
        {
            this.list.Add(item);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, this.list.Count - 1));
        }

        public void AddRange(IEnumerable<T> collection)
        {
            int currentCount = this.Count;
            IList<T> itemsToAdd = collection.ToList();

            this.list.AddRange(itemsToAdd);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)itemsToAdd, currentCount));
        }

        public void Clear()
        {
            this.list.Clear();

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            IList<T> collectionToInsert = collection.ToList();

            this.list.InsertRange(index, collectionToInsert);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)collectionToInsert, index));
        }

        public bool Remove(T item)
        {
            bool removed = this.list.Remove(item);

            if (removed)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }

            return removed;
        }

        public int RemoveAll(Func<T, bool> match)
        {
            var removedList = new List<KeyValuePair<int, T>>(this.list.Capacity);
            int previousCount = this.Count;

            for (int i = 0; i < this.list.Count; i++)
            {
                T item = this.list[i];

                if (match(item))
                {
                    this.list.RemoveAt(i);
                    removedList.Add(new KeyValuePair<int, T>(i, item));
                    i--;
                }
            }

            if (this.ShouldReset(removedList.Count, previousCount))
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            else
            {
                foreach (KeyValuePair<int, T> pair in removedList)
                {
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, pair.Value, pair.Key));
                }
            }

            return removedList.Count;
        }

        public void RemoveAt(int index)
        {
            T objectToRemove = this.list[index];

            this.list.RemoveAt(index);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, objectToRemove, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }

        private bool ShouldReset(int changeLength, int currentLength)
        {
            return (double)changeLength / currentLength > this.resetThreshold
        }
    }
}