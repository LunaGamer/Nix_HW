using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string city1 = "Lubotin";
            string city2 = "Borispol";
            Task3 t = new Task3();
            t.Print();
            int d = t.GetDistance(city1, city2);
            Console.WriteLine($"Distance between {city1} and {city2} = {d}");
            Console.ReadKey();
        }
    }
}
