using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateFibonacciLogic
{
    public class Fibonacci
    {
        /// <summary>
        /// Generates some count of Fibonacci numbers
        /// </summary>
        /// <param name="count">Count of numbers to generate</param>
        /// <returns>Collection of Fibonacci numbers</returns>
        public static IEnumerable<int> GenerateFibonacci(int count)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException();

            if (count == 0)
                yield break;

            int a = -1;
            int b = 1;
            for (int i = 0; i < count; i++)
            {
                yield return a + b;
                int temp = a;
                a = b;
                b = temp + b;
            }


        }
    }
}
