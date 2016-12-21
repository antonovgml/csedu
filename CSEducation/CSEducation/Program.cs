using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEducation
{
    class Program
    {

        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Student ivanov = new Student("Ivan", "Ivanov", "G01", generateMarks(new Subjects[] { Subjects.English, Subjects.Maths, Subjects.Physics }));
            Student petrov = new Student("Petr", "Petrov", "G01", generateMarks(new Subjects[] { Subjects.Arts, Subjects.Singing, Subjects.English, Subjects.Maths, Subjects.Physics }));
            Student sidorov = new Student("Sidor", "Sidorov", "G01", generateMarks(null));
            Console.WriteLine("Student Details: " + ivanov);
            Console.WriteLine("Student Details: " + petrov);
            Console.WriteLine("Student Details: " + sidorov);

            petrov.ResetAllMarks();
            Console.WriteLine("Student Details after resetting all marks: " + petrov);
            sidorov.ResetAllMarks();
            Console.WriteLine("Student Details after resetting all marks: " + sidorov);

            Console.ReadLine();
        }

        // helper for marks array generation
        static Mark[] generateMarks(Subjects[] subjs)
        {

            if (subjs == null)
                return null;

            Mark[] newMarks = new Mark[subjs.Length];

            for (int i = 0; i < newMarks.Length; i++)
            {
                newMarks[i] = new Mark(Enum.GetName(typeof(Subjects), subjs[i]), (byte)rnd.Next(1, 11));

            }

            return newMarks;
        }
    }


    class Student
    {
        public string firstName;
        public string lastName;
        public string groupId;
        Mark[] marks;

        public Student(string firstName, string lastName, string groupId, Mark[] marks)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.groupId = groupId;
            this.marks = marks;
        }



        // returning average of all marks of the student
        public decimal GetAvgMark()
        {
            decimal marksSum = 0;

            if (this.marks == null)
            {
                return 0;
            }


            foreach (var mark in this.marks)
            {
                marksSum += mark.mark;
            }

            return marksSum / this.marks.Length;

        }

        // making all student’s marks equal to zero
        public void ResetAllMarks()
        {
            if (this.marks == null)
                return;

            foreach (var mark in this.marks)
            {
                mark.mark = 0;
            }
        }


        public override string ToString()
        {
            string sMarks = "";

            if (this.marks != null)
            {
                foreach (var mark in this.marks)
                {
                    sMarks += mark.ToString() + "; ";
                }
            }
            else
            {
                sMarks = "No Marks Available";
            }


            return String.Format("{0} {1} (Group {2}), marks: {3}, Avg: {4:0.0}", this.firstName, this.lastName, this.groupId, sMarks, this.GetAvgMark());
        }

    }


    class Mark
    {
        public string subject;
        public byte mark;

        public Mark(string subject, byte mark)
        {
            this.subject = subject;
            this.mark = mark;
        }

        public override string ToString()
        {
            return String.Format("{0} = {1}", this.subject, this.mark);
        }
    }

    // available subjects
    enum Subjects { Physics, Maths, English, Arts, Singing };

}
