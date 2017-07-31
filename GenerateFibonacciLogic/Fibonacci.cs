using System;
using System.Numerics;
using System.Collections.Generic;

namespace GenerateFibonacciLogic
{
    public class Fibonacci
    {
        /// <summary>
        /// Generates some count of Fibonacci numbers
        /// </summary>
        /// <param name="count">Count of numbers to generate</param>
        /// <returns>Collection of Fibonacci numbers</returns>
        public static IEnumerable<BigInteger> GenerateFibonacci(int count)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException();

            if (count == 0)
                yield break;

            
            BigInteger a = -1;
            BigInteger b = 1;
            for (int i = 0; i < count; i++)
            {
                yield return a + b;
                var temp = a;
                a = b;
                b = temp + b;
            }


        }
    }
}
