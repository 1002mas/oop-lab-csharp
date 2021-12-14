using System.Linq;

namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private IList<TItem> _list = new List<TItem>();
        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => _list.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => _list.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => _list[index];
            set
            {
                if (index < Count)
                {
                    TItem elem = _list[index];
                    _list[index] = value;
                    ElementChanged?.Invoke(this, value, elem, index);
                }
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => _list.GetEnumerator();


        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            IList<TItem> list = new List<TItem>(_list);
            _list.Clear();
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
        public void CopyTo(TItem[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            int i = _list.IndexOf(item);
            bool ret = _list.Remove(item);
            if (ret)
            {
                ElementRemoved?.Invoke(this, item, i);
            }
            return ret;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => _list.IndexOf(item);

        /// <inheritdoc cref="IList{T}.Insert" />
        public void Insert(int index, TItem item)
        {
            _list.Insert(index, item);
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            TItem item = _list[index];
            _list.RemoveAt(index);
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
