using System;
using System.Linq.Expressions;
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

            var sqMatrix1 = new Matrix<double>(3, 3);
            var sqMatrix2 = new SquareMatrix<double>(3);

            sqMatrix1.SetElement(2.0,0,0);
            sqMatrix1.SetElement(7.5, 1, 0);
            sqMatrix1.SetElement(2.1, 0, 1);
            sqMatrix1.SetElement(5.0, 0, 2);
            sqMatrix1.SetElement(4.0, 2, 0);
            sqMatrix1.SetElement(1.0, 1, 1);
            sqMatrix1.SetElement(2.0, 2, 1);



            var sqMatrix = sqMatrix1 + sqMatrix2;
            Console.WriteLine(sqMatrix);


            Console.ReadKey();
        }




        //public static T Add<T>(T lhs, T rhs)
        //{
        //    ParameterExpression paramA = Expression.Parameter(typeof(T), "elem1"),
        //                        paramB = Expression.Parameter(typeof(T), "elem2");
        //    BinaryExpression body = Expression.Add(paramA, paramB);
        //    Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
        //    return add(lhs, rhs);
        //}
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
