using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetLogic
{
    public class Set<T> : IEnumerable<T> where T : class, IEquatable<T>
    {
        #region private fields

        private Node<T> _startSentinel;
        private Node<T> _endSentinel;
        private int _size;
        #endregion

        #region properties

        public int Count => _size;
        private Node<T> FirstNode => _startSentinel.Next;
        private Node<T> LastNode => _endSentinel.Prev;
        #endregion

        #region private methods

        private void Initialization()
        {
            _startSentinel = new Node<T>();
            _endSentinel = new Node<T>();
            _startSentinel.Next = _endSentinel;
            _endSentinel.Prev = _startSentinel;
        }

        private Node<T> Search(T valueToSearch)
        {
            if (ReferenceEquals(valueToSearch, null))
                throw new ArgumentNullException($"{nameof(valueToSearch)} must be not null");

            for (var i = FirstNode; i.Next != null; i = i.Next)
                if (valueToSearch.Equals(i.Value))
                    return i;
            return null;
        }
        #endregion

        #region ctors

        public Set()
        {
            Initialization();
        }

        public Set(IEnumerable<T> collection)
        {
            Initialization();

            foreach (T value in collection)
                Add(value);
        }
        #endregion

        #region public methods

        public void Add(T value) => Add(value, _endSentinel.Prev);

        private void Add(T valueToAdd, Node<T> addAfterThat)
        {
            if(ReferenceEquals(valueToAdd, null))
                throw new ArgumentNullException($"{nameof(valueToAdd)} must be not null");

            var toAdd = new Node<T>(valueToAdd, addAfterThat.Next, addAfterThat);
            addAfterThat.Next.Prev = toAdd;
            addAfterThat.Next = toAdd;
            _size++;
        }

        public void AddAfter(T valueToSearch, T valueToAdd)
        {
            var addAfterThat = Search(valueToSearch);

            if (ReferenceEquals(addAfterThat, null))
                throw new InvalidOperationException();

            Add(valueToAdd, addAfterThat);
        }

        public void AddBefore(T valueToSearch, T valueToAdd)
        {
            var addBeforeThat = Search(valueToSearch);

            if (ReferenceEquals(addBeforeThat, null))
                throw new InvalidOperationException();

            Add(valueToAdd, addBeforeThat.Prev);
        }


        public void AddInTheBeginning(T value) => Add(value, _startSentinel);

        public void Remove(T value)
        {
            var toDelete = Search(value);

            if (toDelete == null)
                throw new InvalidOperationException();

            toDelete.Prev.Next = toDelete.Next;
            toDelete.Next.Prev = toDelete.Prev;
            _size--;
        }

        public T FindFirstOrDefault(Predicate<T> predicate) => this.FirstOrDefault(elem => predicate(elem));

        #endregion

        #region GetEnumeratorы

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = FirstNode; i.Next != null; i = i.Next)
                yield return i.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
