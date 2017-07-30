namespace BinarySearchTreeLogic
{
    internal sealed class Node<T>
    {
        internal T Value { get; set; }
        internal Node<T> LeftChild { get; set; }
        internal Node<T> RightChild { get; set; }

        public Node() { }

        public Node(T value, Node<T> leftChild, Node<T> rightChild)
        {
            Value = value;
            LeftChild = leftChild;
            RightChild = rightChild;
        }
    }
}
