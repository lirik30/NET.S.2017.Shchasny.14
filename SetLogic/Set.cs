using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetLogic
{
    /// <summary>
    /// Generalized class-collection Set
    /// </summary>
    /// <typeparam name="T">Variable of reference type with comparison semantics by value</typeparam>
    public class Set<T> : IEnumerable<T> where T : class
    {
        #region private fields

        private int _bucketsCount = 100;
        private Node<T>[] _buckets;
        private int _size;
        private readonly IEqualityComparer<T> _eqComparer;
        private readonly IComparer<T> _comparer;
        #endregion

        #region properties
        
        public int Count => _size;
        #endregion

        #region ctors

        /// <summary>
        /// Create empty set with default logic of compare and default logic of equality
        /// </summary>
        public Set() : this(EqualityComparer<T>.Default, Comparer<T>.Default) { }

        /// <summary>
        /// Create empty set with default logic of compare and custom logic of equality
        /// </summary>
        /// <param name="eqComparer">Equality comparer</param>
        public Set(IEqualityComparer<T> eqComparer) : this(eqComparer, Comparer<T>.Default) { }

        /// <summary>
        /// Create empty set with custom logic of compare and default logic of equality
        /// </summary>
        /// <param name="comparer">Comparer</param>
        public Set(IComparer<T> comparer) : this(EqualityComparer<T>.Default, comparer) { }

        /// <summary>
        /// Create empty set with custom logic of compare and custom logic of equality
        /// </summary>
        /// <param name="eqComparer">Equality comparer</param>
        /// <param name="comparer">Comparer</param>
        public Set(IEqualityComparer<T> eqComparer, IComparer<T> comparer)
        {
            _eqComparer = eqComparer ?? EqualityComparer<T>.Default;

            if (ReferenceEquals(comparer, null))
                ValidateComparer();
            _comparer = comparer ?? Comparer<T>.Default;
            
            Initialization();
        }

        /// <summary>
        /// Create set from any other collection with default logic of compare and default logic of equality
        /// </summary>
        /// <param name="collection">Base collection</param>
        public Set(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default, Comparer<T>.Default) { }

        /// <summary>
        /// Create set from any other collection with default logic of compare and custom logic of equality
        /// </summary>
        /// <param name="collection">Base collection</param>
        /// <param name="eqComparer">Equality comparer</param>
        public Set(IEnumerable<T> collection, IEqualityComparer<T> eqComparer) : this(collection, eqComparer, Comparer<T>.Default) { }

        /// <summary>
        /// Create set from any other collection with custom logic of compare and default logic of equality
        /// </summary>
        /// <param name="collection">Base collection</param>
        /// <param name="comparer">Comparer</param>
        public Set(IEnumerable<T> collection, IComparer<T> comparer) : this(collection, EqualityComparer<T>.Default, comparer) { }

        /// <summary>
        /// Create set from any other collection with custom logic of compare and custom logic of equality
        /// </summary>
        /// <param name="collection">Base collection</param>
        /// <param name="eqComparer">Equality comparer</param>
        /// <param name="comparer">Comparer</param>
        public Set(IEnumerable<T> collection, IEqualityComparer<T> eqComparer, IComparer<T> comparer)
        {
            _eqComparer = eqComparer ?? EqualityComparer<T>.Default;

            if (ReferenceEquals(comparer, null))
                ValidateComparer();
            _comparer = comparer ?? Comparer<T>.Default;

            Initialization();

            foreach (var value in collection)
                Add(value);
        }
        #endregion

        #region public methods

        /// <summary>
        /// Add element in the set, if it doesn't contain this element
        /// </summary>
        /// <param name="value">Value to adding</param>
        public void Add(T value)
        {
            if (Contains(value))
                throw new InvalidOperationException("Set contains the same value");
            _size++;
            Add(value, _buckets[Math.Abs(value.GetHashCode() % _bucketsCount)]);
        }

        /// <summary>
        /// Checks if the set contains an element passed as a parameter
        /// </summary>
        /// <param name="value">Value to search</param>
        /// <returns>True if value exists, otherwise return false</returns>
        public bool Contains(T value)
        {
            for (var i = _buckets[Math.Abs(value.GetHashCode() % _bucketsCount)].Next; i != null; i = i.Next)
            {
                if (_comparer.Compare(value, i.Value) > 0)
                    return false;
                if (_eqComparer.Equals(i.Value, value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Create new set on the base of elements, that are present in both collections.
        /// </summary>
        /// <param name="collection">Other collection</param>
        /// <returns>Intersection of 2 collections</returns>
        public Set<T> Intersection(IEnumerable<T> collection)
        {
            var newSet = new Set<T>();
            foreach (var element in collection)
            {
                if (Contains(element))
                    newSet.Add(element);
            }

            return newSet;
        }

        /// <summary>
        /// Remove element from the set
        /// </summary>
        /// <param name="value">Value to removing</param>
        public void Remove(T value)
        {
            if (!Contains(value))
                throw new InvalidOperationException("Set doesn't contain such element");

            var nodeBefore = Search(value);
            nodeBefore.Next = nodeBefore.Next.Next;
            _size--;
        }

        /// <summary>
        /// Add element from the other collection (without duplicates)
        /// </summary>
        /// <param name="collection">Collection to union</param>
        public void UnionWith(IEnumerable<T> collection)
        {
            foreach (var element in collection)
            {
                if (Contains(element))
                    continue;
                Add(element);
            }
        }
        #endregion

        #region private methods

        private void Add(T value, Node<T> node)
        {
            while (node.Next != null && _comparer.Compare(node.Next.Value, value) < 0)
                node = node.Next;

            node.Next = new Node<T>(value, node.Next);
        }

        /// <summary>
        /// Start initialization
        /// </summary>
        private void Initialization()
        {
            _buckets = new Node<T>[_bucketsCount];
            for(int i = 0; i < _bucketsCount; i++)
                _buckets[i] = new Node<T>();
        }

        /// <summary>
        /// Search value
        /// </summary>
        /// <param name="value">Value to search</param>
        /// <returns>Node BEFORE node with value to search, otherwise null</returns>
        private Node<T> Search(T value)
        {
            for (var i = _buckets[Math.Abs(value.GetHashCode() % _bucketsCount)]; i != null; i = i.Next)
            {
                if (_eqComparer.Equals(i.Next.Value, value))
                    return i;
            }
            return null;
        }

        /// <summary>
        /// Check type for presence of comparison support
        /// </summary>
        private void ValidateComparer()
        {
            Console.WriteLine($"Check for {typeof(T)}");
            var interfacesOfT = typeof(T).GetInterfaces();
            if (!(interfacesOfT.Contains(typeof(IComparer<T>)) || interfacesOfT.Contains(typeof(IComparer)) ||
                  interfacesOfT.Contains(typeof(IComparable<T>)) || interfacesOfT.Contains(typeof(IComparable))))
                throw new InvalidOperationException($"{typeof(T)} doesn't has default comparer");
        }
        #endregion

        #region IEnumerable/Ienumerable<T> methods
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _bucketsCount; i++)
                for (var node = _buckets[i]; node != null; node = node.Next)
                    if (node.Value != null)
                        yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
