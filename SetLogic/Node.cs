using System;

namespace SetLogic
{
    internal sealed class Node<T>
    {
        internal T Value { get; set; }
        internal Node<T> Next { get; set; }

        public Node() { }

        public Node(T value, Node<T> next)
        {
            Value = value;
            Next = next;
        }
    }
}
