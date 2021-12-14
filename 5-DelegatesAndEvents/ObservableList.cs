using System.Linq;

namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private List<TItem> _list = new List<TItem>();
        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count
        {
            get => _list.Count;
        }

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly
        {
            get => false;
        }

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => _list.ToArray()[index];
            set
            {
                if (index < Count)
                {
                    List<TItem> list = new List<TItem>();
                    int i = 0;
                    foreach (var elem in _list)
                    {
                        if (i == index)
                        {
                            list.Add(value);
                            ElementChanged?.Invoke(this, value, elem, index);
                        }
                        else
                        {
                            list.Add(elem);
                        }
                        i++;
                    }

                    _list = list;
                }
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => _list.GetEnumerator();


        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            List<TItem> list = _list;
            _list = new List<TItem>();
            int i = 0;
            foreach (var elem in list)
            {
                ElementRemoved?.Invoke(this, elem, i);
                i++;
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => _list.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (Count > array.Length-arrayIndex)
            {
                throw new ArgumentException();
            }
            foreach (var elem in _list)
            {
                array[arrayIndex] = elem;
                arrayIndex++;
            }
        }

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            int i = _list.IndexOf(item);
            bool ret = _list.Remove(item);
            ElementRemoved?.Invoke(this, item, i);
            return ret;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item)
        {
            int i = 0;
            foreach (var elem in _list)
            {
                if (elem.Equals(item))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        /// <inheritdoc cref="IList{T}.Insert" />
        public void Insert(int index, TItem item)
        {
            List<TItem> list = new List<TItem>();
            int i = 0;
            foreach (var elem in list)
            {
                if (i == index)
                {
                    list.Add(item);
                }
                list.Add(elem);
                i++;
            }

            _list = list;
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            List<TItem> list = new List<TItem>();
            int i = 0;
            TItem item = default(TItem);
            foreach (var elem in list)
            {
                if (i != index)
                {
                    list.Add(elem);
                }
                else
                {
                    item = elem;
                }
                i++;
            }

            _list = list;
            ElementRemoved?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ObservableList<TItem>) obj);
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return (_list != null ? _list.GetHashCode() : 0);
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            String res = "[";
            foreach (var elem in _list)
            {
                res = $"{res}, {elem.ToString()}";
            }

            res = res.Substring(0, res.Length - 1);
            res = $"{res}]";
            return res;
        }

        protected bool Equals(ObservableList<TItem> other)
        {
            return Equals(_list, other._list);
        }
    }
}
