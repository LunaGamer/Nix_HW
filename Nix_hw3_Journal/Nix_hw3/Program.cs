using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw3
{
    class Program
    {
        static void Main(string[] args)
        {
            Student st1 = new Student("Vasya", "Pupkin", "123");
            Student st2 = new Student("Ilya", "Rezin", "321");
            Student st3 = new Student("Anya", "Solovina", "123");
            Student st4 = new Student("Andrey", "Yeger", "232");
            Student st5 = new Student("Katya", "Ovalina", "321");

            Journal journal = new Journal();
            Console.WriteLine($"Avg Journal mark = {journal.AvgJournalMark()}");
            journal.BadStudents();

            journal.AddStudent(st1);
            journal.AddStudent(st2);
            journal.AddStudent(st3);
            journal.AddStudent(st4);
            journal.AddStudent(st5);

            journal.AddMark(st1, 89);
            journal.AddMark(st1, 77);
            journal.AddMark(st1, 68);
            journal.AddMark(st2, 59);
            journal.AddMark(st2, 66);
            journal.AddMark(st2, 75);
            journal.AddMark(st3, 90);
            journal.AddMark(st3, 90);
            journal.AddMark(st3, 90);
            journal.AddMark(st5, 101);


            Console.WriteLine($"Avg mark of {st1.Name} {st1.Surname} = {journal.AvgStudentMark(st1)}");
            Console.WriteLine($"Avg mark of {st2.Name} {st2.Surname} = {journal.AvgStudentMark(st2)}");
            Console.WriteLine($"Avg mark of {st3.Name} {st3.Surname} = {journal.AvgStudentMark(st3)}");
            Console.WriteLine($"Avg mark of {st4.Name} {st4.Surname} = {journal.AvgStudentMark(st4)}");
            Console.WriteLine($"Avg mark of {st5.Name} {st5.Surname} = {journal.AvgStudentMark(st5)}");
            Console.WriteLine($"Avg Journal mark = {journal.AvgJournalMark()}");
            journal.BadStudents();
            Console.ReadKey();
        }
    }
}
