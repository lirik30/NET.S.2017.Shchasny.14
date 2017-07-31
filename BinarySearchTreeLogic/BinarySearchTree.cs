using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

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

        /// <summary>
        /// Count of elements in the tree
        /// </summary>
        public int Size => _size;
        #endregion

        #region ctors

        /// <summary>
        /// Create root of the tree
        /// </summary>
        /// <param name="element">Root element</param>
        public BinarySearchTree(T element) : this(element, Comparer<T>.Default) { }

        /// <summary>
        /// Create tree on the base of some collection of elements. First element of the collection will be the root
        /// </summary>
        /// <param name="elements">Collection of the elements</param>
        public BinarySearchTree(IEnumerable<T> elements) : this(elements, Comparer<T>.Default) { }

        /// <summary>
        /// Create a tree and set the logic, how the elements in the tree will be compared
        /// </summary>
        /// <param name="element">Root element</param>
        /// <param name="comparer">Logic of compare</param>
        public BinarySearchTree(T element, IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
            ValidateComparer();

            _top = new Node<T>(element, null, null);
        }

        /// <summary>
        /// Create tree on the base of some collection of elements and set the logic, how the elements in the tree will be compared
        /// </summary>
        /// <param name="elements">Collection of the elements</param>
        /// <param name="comparer">Logic of compare</param>
        public BinarySearchTree(IEnumerable<T> elements, IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
            ValidateComparer();

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

        /// <summary>
        /// This method determines whether the tree contains the element
        /// </summary>
        /// <param name="value">Element to search</param>
        /// <returns>True if contains, otherwise false</returns>
        public bool Contains(T value) => Contains(_top, value);


        /// <summary>
        /// Add element in the tree
        /// </summary>
        /// <param name="value">Element to addition</param>
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

        /// <summary>
        /// Preorder method of tree bypass
        /// </summary>
        public IEnumerable<T> TraversePreorder() => TraversePreorder(_top);

        /// <summary>
        /// Inorder method of tree bypass
        /// </summary>
        public IEnumerable<T> TraverseInorder() => TraverseInorder(_top);

        /// <summary>
        /// Postorder method of tree bypass
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> TraversePostorder() => TraversePostorder(_top);

        #endregion

        #region private methods
        
        private void ValidateComparer()
        {
            try
            {
                _comparer.Compare(default(T), default(T));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{typeof(T)} doesn't has default comparer", ex);
            }
        }

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
