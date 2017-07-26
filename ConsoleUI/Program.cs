using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GenerateFibonacciLogic.Fibonacci;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var fib in GenerateFibonacci(20))
            {
                Console.WriteLine(fib);
            }

            Console.ReadKey();
        }
    }
}
