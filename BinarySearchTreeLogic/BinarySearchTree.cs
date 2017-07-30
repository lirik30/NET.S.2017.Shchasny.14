using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace BinarySearchTreeLogic
{
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        #region private fields
        private Node<T> _top;
        private int _size;
        private IComparer<T> _comparer;
        #endregion

        #region property

        public int Size => _size;
        #endregion

        #region ctors

        public BinarySearchTree(T element) : this(element, Comparer<T>.Default) { }

        public BinarySearchTree(IEnumerable<T> elements) : this(elements, Comparer<T>.Default) { }

        public BinarySearchTree(T element, IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
            _top = new Node<T>(element, null, null);
        }

        public BinarySearchTree(IEnumerable<T> elements, IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
            foreach (var elem in elements)
                Add(elem);
        }
        #endregion

        #region traverse

        private IEnumerable<T> TraversePreorder(Node<T> node)
        {
            if(ReferenceEquals(node, null)) yield break;

            yield return node.Value;
            
            foreach (var elem in TraversePreorder(node.LeftChild))
                yield return elem;
            
            foreach (var elem in TraversePreorder(node.RightChild))
                yield return elem;
        }

        private IEnumerable<T> TraverseInorder(Node<T> node)
        {
            if (ReferenceEquals(node, null)) yield break;

            foreach (var elem in TraverseInorder(node.LeftChild))
                yield return elem;

            yield return node.Value;
            
            foreach (var elem in TraverseInorder(node.RightChild))
                yield return elem;
        }

        private IEnumerable<T> TraversePostorder(Node<T> node)
        {
            if (ReferenceEquals(node, null)) yield break;

            foreach (var elem in TraversePostorder(node.LeftChild))
                yield return elem;
            
            foreach (var elem in TraversePostorder(node.RightChild))
                yield return elem;

            yield return node.Value;
        }
        #endregion

        #region public methods

        public bool Contains(T value) => Contains(_top, value);

        public void Add(T value)
        {
            if (ReferenceEquals(_top, null))
            {
                _top = new Node<T>(value, null, null);
                return;
            }

            AddNode(_top, value);
            _size++;
        }

        public IEnumerable<T> TraversePreorder() => TraversePreorder(_top);

        public IEnumerable<T> TraverseInorder() => TraverseInorder(_top);

        public IEnumerable<T> TraversePostorder() => TraversePostorder(_top);

        #endregion

        #region private methods

        private bool Contains(Node<T> node, T value)
        {
            if (node == null) return false;

            int cmp = _comparer.Compare(node.Value, value);
            if (cmp == 0) return true;
            return cmp > 0 ? Contains(node.LeftChild, value) : Contains(node.RightChild, value);
        }

        private void AddNode(Node<T> node, T value)//
        {
            if (_comparer.Compare(node.Value, value) > 0)
            {
                if (ReferenceEquals(node.LeftChild, null))
                {
                    node.LeftChild = new Node<T>(value, null, null);
                    return;
                }
                AddNode(node.LeftChild, value);
            }
            else
            {
                if (ReferenceEquals(node.RightChild, null))
                {
                    node.RightChild = new Node<T>(value, null, null);
                    return;
                }
                AddNode(node.RightChild, value);
            }
        }
        

        #endregion

        #region enumeratorы

        public IEnumerator<T> GetEnumerator()
        {
            return TraversePreorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
