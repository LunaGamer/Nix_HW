using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw1
{
    class Circle : MyPoint
    {
        private double radius; //private field for Circle Radius

        public double Radius //public property to interact with private field
        {
            get 
            {
                return radius;
            }

            private set
            {
                radius = value;
            }
        }

        public Circle(double x, double y, double radius) : base(x, y) //Constructor for Circle with 3 parameters, 2 base coordinates and Radius
        {
            Radius = radius;
        }

        public override void Scale(double k) //overriding abstract method to Scale the Circle
        {
            Radius *= k;
        }

        public override void Print() //overriding abstract Print method for Circle
        {
            Console.WriteLine($"Circle at Point [{X};{Y}] with Radius = {Radius}");
        }
    }
}
