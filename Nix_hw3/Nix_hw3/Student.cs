using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw3
{
    class Student
    {
        public string Name //Name property
        {
            get;
            private set;
        }

        public string Surname //Surname property
        {
            get;
            private set;
        }

        public string Group //Group property
        {
            get;
            private set;
        }

        public Student(string name, string surname, string group) //Constructor
        {
            Name = name;
            Surname = surname;
            Group = group;
        }
    }
}
