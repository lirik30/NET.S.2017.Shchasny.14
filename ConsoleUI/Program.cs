using System;
using BinarySearchTreeLogic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinarySearchTree<int>(new[] {0, 15, -10, 7, -5, 3, 22, -17, -2});

            foreach (var elem in tree.TraversePostorder())
            {
                Console.WriteLine(elem);
            }

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
