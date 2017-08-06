using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListLogic
{
    public class DoubleLinkedList<T> : IEnumerable<T>
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

        #region ctors
        /// <summary>
        /// Create empty double linked list
        /// </summary>
        public DoubleLinkedList() => Initialization();

        /// <summary>
        /// Create double linked list from any other collection
        /// </summary>
        /// <param name="collection"></param>
        public DoubleLinkedList(IEnumerable<T> collection)
        {
            Initialization();

            foreach (T value in collection)
                Add(value);
        }
        #endregion

        #region public methods

        /// <summary>
        /// Add element in the ending of the set
        /// </summary>
        /// <param name="value">Element for adding</param>
        public void Add(T value) => Add(value, LastNode);

        /// <summary>
        /// Add element after any other element(if it exists) in the set
        /// </summary>
        /// <param name="valueToSearch">Insert after that element</param>
        /// <param name="valueToAdd">Element for adding</param>
        public void AddAfter(T valueToSearch, T valueToAdd)
        {
            var addAfterThat = Search(valueToSearch);
            Add(valueToAdd, addAfterThat);
        }

        /// <summary>
        /// Add element before any other element(if it exists) in the set
        /// </summary>
        /// <param name="valueToSearch">Insert before that element</param>
        /// <param name="valueToAdd">Element for adding</param>
        public void AddBefore(T valueToSearch, T valueToAdd)
        {
            var addBeforeThat = Search(valueToSearch);
            Add(valueToAdd, addBeforeThat.Prev);
        }

        /// <summary>
        /// Add element in the beginning of the set
        /// </summary>
        /// <param name="value">Element for adding</param>
        public void AddInTheBeginning(T value) => Add(value, _startSentinel);

        /// <summary>
        /// Remove element in the set, if it exists
        /// </summary>
        /// <param name="value">Element for removing</param>
        public void Remove(T value)
        {
            var toDelete = Search(value);

            if (toDelete == null)
                throw new InvalidOperationException();

            toDelete.Prev.Next = toDelete.Next;
            toDelete.Next.Prev = toDelete.Prev;
            _size--;
        }
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
                if (Equals(valueToSearch, i.Value))
                    return i;
            return null;
        }

        private void Add(T valueToAdd, Node<T> addAfterThat)
        {
            if (ReferenceEquals(valueToAdd, null))
                throw new ArgumentNullException($"{nameof(valueToAdd)} must be not null");

            if (ReferenceEquals(addAfterThat, null))
                throw new InvalidOperationException();

            var toAdd = new Node<T>(valueToAdd, addAfterThat.Next, addAfterThat);
            addAfterThat.Next.Prev = toAdd;
            addAfterThat.Next = toAdd;
            _size++;
        }
        #endregion

        #region IEnumerable/IEnumerable<T> methods

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
