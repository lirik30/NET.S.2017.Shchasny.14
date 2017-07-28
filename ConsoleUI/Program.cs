using System;
using MatrixLogic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var matrix = new Matrix<int>(5, 7);

            //matrix.SetElement(4, 5, 3);
            //Console.WriteLine();
            ////_________________//

            //var sqMatrix = new SquareMatrix<int>(5);
            //sqMatrix.SetElement(8, 2, 2);
            //Console.WriteLine(sqMatrix.Order);
            //_____________________//

            //var symmMatrix = new SymmetricMatrix<int>(6);
            //Console.WriteLine(symmMatrix);
            //var listener = new Listener<int>();
            //listener.Register(symmMatrix);

            //symmMatrix.SetElement(9, 0, 0);
            //symmMatrix.SetElement(4, 3, 5);
            //symmMatrix.SetElement(3, 5, 5);
            //Console.WriteLine();
            //Console.WriteLine(symmMatrix);

            //var dMatrix = new DiagonalMatrix<int>(6);
            //Console.WriteLine(dMatrix);
            //dMatrix.SetElement(3, 5, 5);
            //Console.WriteLine(dMatrix.GetElement(5, 5));
            //Console.WriteLine();
            //Console.WriteLine(dMatrix);

            Console.ReadKey();
        }
    }



    class Listener<T>
    {
        public void Register(Matrix<T> matrix)  => matrix.IndexChanged += ListenerMsg;
        public void Unregister(Matrix<T> matrix) => matrix.IndexChanged -= ListenerMsg;

        private void ListenerMsg(object sender, ElementChangedEventArgs eventArgs)
        {
            Console.WriteLine("Element changed!");
            Console.WriteLine($"{eventArgs.OldElement} was changed in the row {eventArgs.Row} column {eventArgs.Column} with {eventArgs.Element}");
        }
    }
}
