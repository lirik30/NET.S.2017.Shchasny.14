using System;
using System.Collections.Generic;
using BinarySearchTreeLogic;
using BookLogic;

namespace ConsoleUI
{
    public class DigitsComparer : IComparer<int>
    {
        public int Compare(int x, int y) => Math.Abs(x).ToString().Length - Math.Abs(y).ToString().Length;
    }

    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y) => x.Length - y.Length;
    }

    //public class Point2DComparer : IComparer<Point2D>
    //{
    //    public int Compare(Point2D x, Point2D y) => (int)(x.Distance - y.Distance);
    //}

    public class BookComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y) => x.Pages - y.Pages;
    }


    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinarySearchTree<Book>(new[] {
                new Book(null, "mike", null, 1),
                new Book(null, "andy", null, 1),
                new Book(null, "zeta", null, 1),
                new Book(null, "paul", null, 1)});
            
            foreach (var elem in tree.TraverseInorder())
            {
                Console.WriteLine(elem);
            }

            //Console.WriteLine(tree.Contains(new Book(){Pages = 434}));

            Console.ReadKey();
        }
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
