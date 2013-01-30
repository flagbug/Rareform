using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Rareform.Collections
{
    public class ObservableList<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly List<T> list;
        private readonly double resetThreshold;

        public ObservableList()
        {
            this.list = new List<T>();
            this.resetThreshold = 0.3;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

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
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }

        public void AddRange(IEnumerable<T> collection)
        {
            IList<T> itemsToAdd = collection.ToList();

            if (itemsToAdd.Count != 0)
            {
                int currentCount = this.Count;

                if (this.ShouldReset(itemsToAdd.Count, currentCount))
                {
                    this.list.AddRange(itemsToAdd);
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    this.OnPropertyChanged("Count");
                    this.OnPropertyChanged("Item[]");
                }

                else
                {
                    foreach (T item in itemsToAdd)
                    {
                        this.Add(item);
                    }
                }
            }
        }

        public void Clear()
        {
            this.list.Clear();

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
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
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            IList<T> collectionToInsert = collection.ToList();

            this.list.InsertRange(index, collectionToInsert);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)collectionToInsert, index));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }

        public bool Remove(T item)
        {
            bool removed = this.list.Remove(item);

            if (removed)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                this.OnPropertyChanged("Count");
                this.OnPropertyChanged("Item[]");
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

            if (removedList.Count != 0)
            {
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

                this.OnPropertyChanged("Count");
                this.OnPropertyChanged("Item[]");
            }

            return removedList.Count;
        }

        public void RemoveAt(int index)
        {
            T objectToRemove = this.list[index];

            this.list.RemoveAt(index);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, objectToRemove, index));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }

        public void Reverse()
        {
            this.list.Reverse();

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            this.OnPropertyChanged("Item[]");
        }

        public void Sort()
        {
            this.list.Sort();

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            this.OnPropertyChanged("Item[]");
        }

        public void Sort(Func<T, T, int> comparison)
        {
            this.list.Sort((x, y) => comparison(x, y));

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            this.OnPropertyChanged("Item[]");
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool ShouldReset(int changeLength, int currentLength)
        {
            return (double)changeLength / currentLength > this.resetThreshold;
        }
    }
}