using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw1
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectagle r = new Rectagle(1.0d, 2.0d, 4.0d, 2.0d);
            Circle c = new Circle(2.0d, 3.0d, 1.5d);
            Triangle t = new Triangle(6.0d, 7.0d, 2.0d, 1.0d, 2.3d, 3.2d);
            Image img = new Image();
            r.Print();
            c.Print();
            t.Print();
            img.Print();
            r.Move(3.0d, 2.7d);
            c.Scale(4.2d);
            img.Add(t);
            r.Print();
            c.Print();
            img.Print();
            img.Add(c);
            img.Add(r);
            img.Move(3.3d, 4.4d);
            img.Scale(1.5d);
            img.Print();
            Console.ReadKey();
        }
    }
}
