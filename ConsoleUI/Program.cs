﻿using System;
using System.Numerics;
using System.Collections.Generic;
using BinarySearchTreeLogic;
using BookLogic;
using static GenerateFibonacciLogic.Fibonacci;
using SetLogic;

namespace ConsoleUI
{
    public struct Point2D
    {
        public int X { get; }
        public int Y { get; }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X},{Y})";
    }

    #region custom comparer

    public class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y) => Math.Abs(x) - Math.Abs(y);
    }

    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y) => x.Length - y.Length;
    }

    public class Point2DComparer : IComparer<Point2D>
    {
        public int Compare(Point2D x, Point2D y) => x.Y - y.Y;
    }

    public class BookComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y) => x.Pages - y.Pages;
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            
            Console.ReadKey();
        }

        private static void SetTest()
        {
            var set = new Set<string> {"A", "Add", "Bet", "M", "ER", "console"};

            //set.Add("A"); //throws exception
            Console.WriteLine(set.Contains("ER"));
            set.Remove("ER");
            Console.WriteLine(set.Contains("ER"));
            Console.WriteLine(set.Count);

            set.UnionWith(new[] { "A", "B", "C", "D", "F", "G", "H", "I" });
            foreach (var elem in set)
            {
                Console.WriteLine(elem);
            }
            //_____________________
            var set2 = set.Intersection(new[] { "A", "B", "K", "M", "T", "I" });
            Console.WriteLine();
            foreach (var elem in set2)
            {
                Console.WriteLine(elem);
            }
        }

        private static void FibonacciTest()
        {
            foreach (var number in GenerateFibonacci(125))
            {
                Console.WriteLine(number);
            }
        }

        private static void TreeTest()
        {
            var intTree = IntInitializer();
            var dblTree = StringInitializer();
           var bookTree = BookInitializer();
            var pointTree = PointInitializer();

            TraverseTest(intTree);
            TraverseTest(dblTree);
            TraverseTest(bookTree);
            TraverseTest(pointTree);
        }

        private static void TraverseTest<T>(BinarySearchTree<T> tree)
        {
            Console.WriteLine("____START_TRAVERSE____");
            foreach (var elem in tree.TraverseInorder())
                Console.WriteLine(elem);
            Console.WriteLine("_____END_TRAVERSE____");
        }

        #region Initializers
        private static BinarySearchTree<int> IntInitializer()
        {
            return new BinarySearchTree<int>(new[]
                    { 0, -15, 20, -3, -2, -5, 17, 22, 4, 19, 16, 0},
                new IntComparer());
        }

        private static BinarySearchTree<string> StringInitializer()
        {
            return new BinarySearchTree<string>(new[]
                { "A", "B", "C", "T", "F", "K", "Z", "@", "$", "4", "ab", "ba"});
        }

        private static BinarySearchTree<Book> BookInitializer()
        {
            return new BinarySearchTree<Book>(new[]
            {
                    new Book(null, "A", null, 1000),
                    new Book(null, "X", null, 2014),
                    new Book(null, "C", null, 1344),
                    new Book(null, "B", null, 443)});//,
            //new BookComparer());
        }

        private static BinarySearchTree<Point2D> PointInitializer()
        {
            return new BinarySearchTree<Point2D>(new[] {
                    new Point2D(18, 10),
                    new Point2D(22, 60),
                    new Point2D(2, 2),
                    new Point2D(15, 41)});//,
                //new Point2DComparer());
        }
        #endregion
    }

    

    

    //class Listener<T>
    //{
    //    public void Register(Matrix<T> matrix)  => matrix.IndexChanged += ListenerMsg;
    //    public void Unregister(Matrix<T> matrix) => matrix.IndexChanged -= ListenerMsg;

    //    private void ListenerMsg(object sender, ElementChangedEventArgs eventArgs)
    //    {
    //        Console.WriteLine("Element changed!");
    //        Console.WriteLine($"{eventArgs.OldElement} was changed in the row {eventArgs.Row} column {eventArgs.Column} with {eventArgs.Element}");
    //    }
    //}
}
