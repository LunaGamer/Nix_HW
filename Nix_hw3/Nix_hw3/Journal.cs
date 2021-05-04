using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw3
{
    class Journal
    {
        private List<Student> Students { get; set; } //private property for List of Students

        private Dictionary<Student, List<int>> StudentMarks{ get; set; } //private property for Dictionary with Students (as a Key) and Lists of marks (as Values)

        public Journal() //public constructor
        {
            Students = new List<Student>();
            StudentMarks = new Dictionary<Student, List<int>>();
        }

        public void AddStudent(Student newStudent) //Add new Students into the journal
        {
            if (!Students.Contains(newStudent))
            {
                Students.Add(newStudent);
                StudentMarks.Add(newStudent, new List<int>());
            }
        }

        public void AddMark(Student student, int mark) //Add new makr to a certain Student
        {
            if (Students.Contains(student) && mark > -1 && mark < 101)
            {
                StudentMarks[student].Add(mark);
            }
        }

        public double AvgStudentMark(Student student) //Calculate average mark of Student
        {
            double avgMark = -1d;
            if (Students.Contains(student) && StudentMarks[student].Any())
            {
                avgMark = StudentMarks[student].Average();
            }
            return avgMark;
        }

        public void BadStudents() //Show "Bad" Students - Students that have at least one mark < 60
        {
            Console.WriteLine("Bad Students:");
            foreach (var student in StudentMarks.Aggregate(new List<Student>(), (x, y) => { if (y.Value.Any(z => z < 60)) x.Add(y.Key); return x; }))
            {                
                    Console.WriteLine($"{student.Name} {student.Surname} from Group {student.Group}");                
            }
        }

        public double AvgJournalMark() //Calculate average journal mark of all students
        {
            double avgMark = -1d;
            var list = StudentMarks.Values.Aggregate(new List<int>(), (x, y) => { x.AddRange(y); return x; }).ToList();
            if (list.Count > 0)
            {
                avgMark = list.Average();
            }
            return avgMark;
        }
    }
}
