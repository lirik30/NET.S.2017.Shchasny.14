using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTreeLogic
{
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        #region private fields
        private Node<T> _top;
        private int _size;
        #endregion

        #region property

        public int Size => _size;
        #endregion

        #region ctors

        public BinarySearchTree(T element)
        {
            _top = new Node<T>(element, null, null);
        }

        public BinarySearchTree(IEnumerable<T> elements)
        {
            foreach ( var elem in elements)
                Add(elem);
        }
        #endregion

        #region traverse

        private IEnumerable<T> TraversePreorder(Node<T> node)
        {
            yield return node.Value;
            //Console.WriteLine($"{node.Value} -> preorder");
            if (!ReferenceEquals(node.LeftChild, null))
                foreach (var elem in TraversePreorder(node.LeftChild))
                {
                    yield return elem;
                }
            if (!ReferenceEquals(node.RightChild, null))
                foreach (var elem in TraversePreorder(node.RightChild))
                {
                    yield return elem;
                }
        }

        private IEnumerable<T> TraverseInorder(Node<T> node)
        {
            if (!ReferenceEquals(node.LeftChild, null))
                foreach (var elem in TraverseInorder(node.LeftChild))
                {
                    yield return elem;
                }
            yield return node.Value;
            if (!ReferenceEquals(node.RightChild, null))
                foreach (var elem in TraverseInorder(node.RightChild))
                {
                    yield return elem;
                }
        }

        private IEnumerable<T> TraversePostorder(Node<T> node)
        {
            if (!ReferenceEquals(node.LeftChild, null))
                foreach (var elem in TraversePostorder(node.LeftChild))
                {
                    yield return elem;
                }
            if (!ReferenceEquals(node.RightChild, null))
                foreach (var elem in TraversePostorder(node.RightChild))
                {
                    yield return elem;
                }
            yield return node.Value;
        }
        #endregion

        #region public methods

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

        public IEnumerable<T> TraversePreorder()
        {
            return TraversePreorder(_top);
        }

        public IEnumerable<T> TraverseInorder()
        {
            return TraverseInorder(_top);
        }

        public IEnumerable<T> TraversePostorder()
        {
            return TraversePostorder(_top);
        }
        #endregion

        #region private methods

        private void AddNode(Node<T> node, T value)
        {
            if (node.Value.CompareTo(value) > 0)
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
