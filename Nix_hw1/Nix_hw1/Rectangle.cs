using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw1
{
    class Rectagle : MyPoint
    {
        private double width; //private fields for Rectangle width and Height
        private double height;

        public double Width //public properties to interact with private fields
        {
            get 
            { 
                return width; 
            }

            private set
            {
                width = value;
            }
        }

        public double Height
        {
            get
            { 
                return height;
            }

            private set
            {
                height = value;
            }
        }

        public Rectagle(double x, double y, double width, double height) : base(x, y) //Constructor for Rectangle with 4 parameters, 2 base coordinates, Width and Height
        {
            Width = width;
            Height = height;
        }

        public override void Scale(double k) //overriding abstract method to Scale the Rectangle
        {
            Width *= k;
            Height *= k;
        }

        public override void Print() //overriding abstract Print method for Rectangle
        {
            Console.WriteLine($"Rectangle at Point [{X};{Y}] with Width = {Width} and Height = {Height}");
        }
    }
}
