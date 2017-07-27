using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetLogic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = new Set<string> {"first"};
            
            set.Add("second");
            set.Add("third");
            set.AddBefore("first", "zero");
            set.AddAfter("second", "second and a half");
            set.Add("fourth");
            set.AddInTheBeginning("minus first");
            set.Remove("minus first");


            foreach (var elem in set)
            {
                Console.WriteLine(elem);
            }


            Console.WriteLine($"{set.FindFirstOrDefault((x) => x.Length == 5)} -> length == 5");

            Console.ReadKey();
        }
    }
}
