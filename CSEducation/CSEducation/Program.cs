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

            Student ivanov = new Student("Ivan", "Ivanov", "G01", generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));
            Student petrov = new Student("Petr", "Petrov", "G01", generateMark(Subjects.Arts), generateMark(Subjects.Singing), generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));
            // EXAMPLE parameter with default value
            Student sidorov = new Student("Sidor", "Sidorov", "G01");
            // EXAMPLE using named arguments
            Student doe = new Student(groupId: "G01", firstName: "John", lastName: "Doe");

            // EXAMPLE using params
            Student smith = new Student("John", "Smith", "G01",  generateMark(Subjects.Arts), generateMark(Subjects.Singing), generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));

            Student[] studs = { ivanov, petrov, sidorov, doe, smith};


            foreach (var stud in studs)
            {
                Console.WriteLine("Student Details: " + stud);
            }


            // finding highest Avg
            decimal highestMathMark = 0;
            foreach (var stud in studs)
            {
                stud.FindHighestSubjMark(Subjects.Maths, ref highestMathMark);
            }
            Console.WriteLine("Highest mark for {0} is {1}", Subjects.Maths,  highestMathMark);


            petrov.ResetAllMarks();
            Console.WriteLine("Student Details after resetting all marks: " + petrov);
            sidorov.ResetAllMarks();
            Console.WriteLine("Student Details after resetting all marks: " + sidorov);

            // DO WHILE loop example
            char ch;
            do
            {
                Console.WriteLine("\nPress 'x' or 'X' for exit");
                ch = Char.ToUpper(Console.ReadKey().KeyChar);
                if (ch.Equals(' '))
                {
                    Console.WriteLine("SPACE won't work");
                    continue; // CONTINUE statement example
                }
            } while (!ch.Equals('X'));

        }

        static Mark generateMark(Subjects subj)
        {
            return new Mark(subj, (byte)rnd.Next(1, 11));
        }

        // helper for marks array generation
        static Mark[] generateMarks(Subjects[] subjs)
        {
            if (subjs == null)
                return null;

            Mark[] newMarks = new Mark[subjs.Length];

            // FOR LOOP example
            for (int i = 0; i < newMarks.Length; i++)
            {
                newMarks[i] = generateMark(subjs[i]);
            }

            return newMarks;
        }
    }


    class Student
    {
        private static ulong totalCount = 0;

        private ulong id;
        public string firstName;
        public string lastName;
        public string groupId;
        Mark[] marks;

        public Student(string firstName, string lastName, string groupId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.groupId = groupId;            

            this.id = ++Student.totalCount;
        }

        // PARAMS example

        public Student(string firstName, string lastName, string groupId, params Mark[] marks) : this(firstName, lastName, groupId)
        {
            this.marks = marks;
        }

        // returning average of all marks of the student
        public decimal GetAvgMark(out decimal marksSum, out decimal count)
        {
            marksSum = 0;
            count = 0;

            // IF statement example
            if (this.marks == null)
            {
                return 0;
            }

            count = this.marks.Length;
            // FOREACH LOOP example
            foreach (var mark in this.marks)
            {
                marksSum += mark.mark;
            }

            // ?: operator example
            return (this.marks.Length > 0) ? marksSum / this.marks.Length : 0;

        }

        // making all student’s marks equal to zero
        public void ResetAllMarks()
        {
            if (this.marks == null)
                return;

            int marksCount = this.marks.Length;

            if (marksCount == 0)
                return;

            int i = 0;
            // WHILE LOOP example
            while (i < marksCount)
            {
                this.marks[i].mark = 0;
                i++;

                // LOOP BREAK example
                if (i >= marksCount)
                    break;
                
            }
        }

        // Example using ref parameter
        public void FindHighestSubjMark(Subjects subj, ref decimal subjMark)
        {

            if (this.marks == null)
                return;

            foreach (var mark in this.marks)
            {
                if (mark.subject == subj)
                {
                    if (mark.mark > subjMark)
                    {
                        // EXAMPLE change external varialbe passed by reference
                        subjMark = mark.mark;
                    }
                }
            }

        }


        public override string ToString()
        {
            // DECLARATION statement example
            string sMarks = "";

            // IF/ELSE statement example
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

            decimal sum, count;

            // OUT PARAMETERS example
            decimal avg = this.GetAvgMark(out sum, out count);

            return String.Format("{0} {1} (Group {2}), marks: {3}, Avg: {4:0.0} = {5:0.0}/{6:0.0}", this.firstName, this.lastName, this.groupId, sMarks, avg, sum, count);
        }



    }


    class Mark
    {
        public Subjects subject;
        public byte mark;

        public String subjectString
        {
            get
            {
                string result;

                // SWITCH statement example
                switch (subject)
                {
                    case Subjects.Physics:
                        result = "CLASSICAL PHYSICS";
                        break; // SWITCH BREAK example

                    case Subjects.Maths:
                        result = "HIGHEST MATHEMATICS";
                        break;

                    case Subjects.English:
                        result = "BRITISH ENGLISH LANGUAGE";
                        break;

                    case Subjects.Arts:
                        result = "WORLD ARTS";
                        break;

                    case Subjects.Singing:
                        result = "SINGING";
                        break;

                    default:
                        return "UNDEFINED";
                }

                return result;
            }
            
        }

        public Mark(Subjects subject, byte mark)
        {
            this.subject = subject;
            this.mark = mark;
        }

        public override string ToString()
        {
            return String.Format("{0} = {1}", this.subjectString, this.mark);
        }

    }

    // available subjects
    enum Subjects { Physics, Maths, English, Arts, Singing };

}
