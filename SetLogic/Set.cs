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

        public Set() : this(EqualityComparer<T>.Default, Comparer<T>.Default) { }

        public Set(IEqualityComparer<T> eqComparer) : this(eqComparer, Comparer<T>.Default) { }

        public Set(IComparer<T> comparer) : this(EqualityComparer<T>.Default, comparer) { }

        public Set(IEqualityComparer<T> eqComparer, IComparer<T> comparer)
        {
            _eqComparer = eqComparer ?? EqualityComparer<T>.Default;

            if (ReferenceEquals(comparer, null))
                ValidateComparer();
            _comparer = comparer ?? Comparer<T>.Default;
            
            Initialization();
        }

        public Set(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default, Comparer<T>.Default) { }

        public Set(IEnumerable<T> collection, IEqualityComparer<T> eqComparer) : this(collection, eqComparer, Comparer<T>.Default) { }

        public Set(IEnumerable<T> collection, IComparer<T> comparer) : this(collection, EqualityComparer<T>.Default, comparer) { }

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

        public void Add(T value)
        {
            if (Contains(value))
                throw new InvalidOperationException("Set contains the same value");
            _size++;
            Add(value, _buckets[Math.Abs(value.GetHashCode() % _bucketsCount)]);
        }

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

        public void Remove(T value)
        {
            if (!Contains(value))
                throw new InvalidOperationException("Set doesn't contain such element");

            var nodeBefore = Search(value);
            nodeBefore.Next = nodeBefore.Next.Next;
            _size--;
        }

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

        private void Initialization()
        {
            _buckets = new Node<T>[_bucketsCount];
            for(int i = 0; i < _bucketsCount; i++)
                _buckets[i] = new Node<T>();
        }


        private Node<T> Search(T value)
        {
            for (var i = _buckets[Math.Abs(value.GetHashCode() % _bucketsCount)]; i != null; i = i.Next)
            {
                if (_eqComparer.Equals(i.Next.Value, value))
                    return i;
            }
            return null;
        }


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
