using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw1
{
    class Triangle : Rectagle
    {
        private double dx2; //private fields for 3rd point offset relative to 1st point
        private double dy2;

        public double Dx2 //public properties to interact with private fields
        {
            get
            {
                return dx2;
            }

            private set
            {
                dx2 = value;                
            }
        }

        public double Dy2
        {
            get
            {
                return dy2;
            }

            private set
            {
                dy2 = value;
            }
        }

        public Triangle(double x, double y, double width, double height, double dx2, double dy2) : base(x, y, width, height)
            //Constructor for Rectangle with 6 parameters, 2 base coordinates, Width and Height (2nd point offset), and 3rd point offset
        {
            Dx2 = dx2;
            Dy2 = dy2;
        }

        public override void Scale(double k) //overriding abstract method to Scale the Triangle
        {
            base.Scale(k);
            Dx2 *= k;
            Dy2 *= k;
        }

        public override void Print() //overriding abstract Print method for Triangle
        {
            Console.WriteLine($"Triangle at Point1 = [{X};{Y}] Point2 = [{X + Width};{Y + Height}] Point3 = [{X + Dx2};{Y + Dy2}]");
        }
    }
}
