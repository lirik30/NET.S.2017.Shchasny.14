﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLinkedListLogic
{
    internal sealed class Node<T>
    {
        internal T Value { get; set; }
        internal Node<T> Next { get; set; }
        internal Node<T> Prev { get; set; }

        public Node() { }

        public Node(T value, Node<T> next, Node<T> prev)
        {
            Value = value;
            Next = next;
            Prev = prev;
        }
    }
}
