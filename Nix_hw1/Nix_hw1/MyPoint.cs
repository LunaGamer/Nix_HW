using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw1
{
    abstract class MyPoint
    {
        private double x; //Private fields with coordinates X and Y 
        private double y;

        public double X {  //public properties to interact with private fields
            get 
            { 
                return x;
            }

            private set
            {
                x = value;
            } 
        }

        public double Y
        {
            get 
            { 
                return y;
            }

            private set
            {
                y = value;
            }
        }

        protected MyPoint() //public constructor without parameters, needed for Image class constructor
        {

        }

        protected MyPoint(double x, double y) //public constructor with X,Y coordinates as parameters
        {
            X = x;
            Y = y;
        }

        public virtual void Move(double dx, double dy) //virtual method to move figure
        {
            X += dx;
            Y += dy;
        }

        public abstract void Scale(double k); //abstract method to scale figure

        public abstract void Print(); //abstract method to print figure
    }
}
