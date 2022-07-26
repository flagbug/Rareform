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
            list = new List<T>();
            resetThreshold = 0.3;
        }

        public int Capacity => list.Capacity;

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => list[index];
            set
            {
                var objectToReplace = list[index];

                list[index] = value;

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value,
                    objectToReplace, index));
            }
        }

        public void Add(T item)
        {
            list.Add(item);

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, list.Count - 1));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void Clear()
        {
            list.Clear();

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Remove(T item)
        {
            var removed = list.Remove(item);

            if (removed)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                OnPropertyChanged("Count");
                OnPropertyChanged("Item[]");
            }

            return removed;
        }

        public void RemoveAt(int index)
        {
            var objectToRemove = list[index];

            list.RemoveAt(index);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                objectToRemove, index));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddRange(IEnumerable<T> collection)
        {
            IList<T> itemsToAdd = collection.ToList();

            if (itemsToAdd.Count != 0)
            {
                var currentCount = Count;

                if (ShouldReset(itemsToAdd.Count, currentCount))
                {
                    list.AddRange(itemsToAdd);
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    OnPropertyChanged("Count");
                    OnPropertyChanged("Item[]");
                }

                else
                {
                    foreach (var item in itemsToAdd) Add(item);
                }
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            IList<T> collectionToInsert = collection.ToList();

            list.InsertRange(index, collectionToInsert);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                (IList)collectionToInsert, index));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public int RemoveAll(Func<T, bool> match)
        {
            var removedList = new List<KeyValuePair<int, T>>(list.Capacity);
            var previousCount = Count;

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];

                if (match(item))
                {
                    list.RemoveAt(i);
                    removedList.Add(new KeyValuePair<int, T>(i, item));
                    i--;
                }
            }

            if (removedList.Count != 0)
            {
                if (ShouldReset(removedList.Count, previousCount))
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                else
                    foreach (var pair in removedList)
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                            pair.Value, pair.Key));

                OnPropertyChanged("Count");
                OnPropertyChanged("Item[]");
            }

            return removedList.Count;
        }

        public void Reverse()
        {
            list.Reverse();

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public void Shuffle()
        {
            Shuffle(x => Guid.NewGuid());
        }

        public void Shuffle<TKey>(Func<T, TKey> keySelector)
        {
            var newList = new List<T>(Capacity);
            newList.AddRange(this.OrderBy(keySelector));

            list.Clear();
            list.AddRange(newList);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public void Sort()
        {
            list.Sort();

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public void Sort(Func<T, T, int> comparison)
        {
            list.Sort((x, y) => comparison(x, y));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null) CollectionChanged(this, e);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool ShouldReset(int changeLength, int currentLength)
        {
            return (double)changeLength / currentLength > resetThreshold;
        }
    }
}