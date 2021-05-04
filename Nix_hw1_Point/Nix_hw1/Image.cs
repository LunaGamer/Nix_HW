using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw1
{
    class Image : MyPoint
    {
        private List<MyPoint> figures; //private List of figures in Image

        public Image() //constructor for Image
        {
            figures = new List<MyPoint>();
        }

        public void Add(MyPoint figure) //Method to Add new figures into the Image
        {
            if (!(figure is null) && figure != this)
            {
                figures.Add(figure);
            }
        }

        public override void Move(double dx, double dy) //overriding method to move the entire list of figures
        {
            foreach (MyPoint f in figures)
            {
                f.Move(dx, dy);
            }
        }

        public override void Scale(double k) //overriding method to Scale the entire list of figures
        {
            foreach (MyPoint f in figures)
            {
                f.Scale(k);
            }
        }

        public override void Print() //overriding method to Print the entire list of figures
        {
            if (figures.Count > 0)
            {
                Console.WriteLine("Image:");
                foreach (MyPoint f in figures)
                {
                    f.Print();
                }
            }
            else
            {
                Console.WriteLine("Image is empty");
            }
        }
    }
}
