using System;
using Util;
using System.Collections.Generic;
using Shopping;
using AnimalWorld;
using Inheritance;
using Reflection;

namespace CSEducation
{

    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            taskReflection();
        }

        

        static void taskReflection()
        {

            C.p(Reflection.TypeInfo.AsString<float>());
            C.p(Reflection.TypeInfo.AsString<string[]>());
            C.p(Reflection.TypeInfo.AsString<System.ArgumentOutOfRangeException>());
            C.p(Reflection.TypeInfo.AsString<IComparable>());

        }

        static void task3()
        {            
            Boy vasya = new Boy("Vasya", "Pupkin", (byte) rnd.Next(1, 150), (float) rnd.NextDouble() * 10);
            Girl valya = new Girl("Valya", "Pupkina", (byte) rnd.Next(1, 150), (byte) rnd.Next(1, 5));

            
            C.p("Boy " + vasya);
            C.p("Girl " + valya);
            /* similarities */

            ((Human)vasya).BrushTeeth();
            ((Human)valya).BrushTeeth();

            /* differences */

            vasya.ShaveFace();
            vasya.WashHead();
            valya.ShaveLegs();
            valya.WashHead();

        }

        static void task4()
        {
            Group<Animal> animals = new Group<Animal>();
            Group<Animal> alive = new Group<Animal>();
            animals.add(new Frog("Jimmy")).add(new Crocodile("Billy")).add(new Cat("Kitty")).add(new Penguin("Timmy")).add(new Eagle("Johny"));

            Console.WriteLine("Initial list of animals:");
            displayByType<Animal>(animals);

            Console.WriteLine("\nJumpers:");
            displayByType<IJump>(animals);
            
            Console.WriteLine("\nFliers:");
            displayByType<IFly>(animals);

            Console.WriteLine("\nSwimmers:");
            displayByType<ISwim>(animals);
            
            Console.WriteLine("\nPassing over a stream... ");
            uint streamWidth = (uint)rnd.Next(1, 100);

            foreach (var animal in animals)
            {
                if (animal is ISwim)
                {
                    if (((ISwim)animal).swim(streamWidth))
                    {
                        Console.WriteLine("{0} swimmed the stream", animal);
                        alive.add(animal);
                    }
                    else
                    {
                        Console.WriteLine("{0} died. RIP...", animal);
                    }
                }

                else if (animal is IFly)
                {
                    if (((IFly)animal).fly(streamWidth))
                    {
                        Console.WriteLine("{0} flew the stream", animal);
                        alive.add(animal);
                    }
                    else
                    {
                        Console.WriteLine("{0} died. RIP...", animal);
                    }
                }
                else if (animal is IJump)
                {
                    if (((IJump)animal).jump(streamWidth))
                    {
                        Console.WriteLine("{0} jumped the stream", animal);
                        alive.add(animal);
                    }
                    else
                    {
                        Console.WriteLine("{0} died. RIP...", animal);
                    }

                }
            }


            Console.WriteLine("\nAnimals left:");
            foreach (var animal in alive) { Console.WriteLine(animal); }

            Console.WriteLine("\nFeeding Alive Animals:");
            foreach (var animal in alive) { animal.eat(); }

        }

        static void displayByType<T>(IEnumerable<Animal> collection)
         {
            foreach (var animal in collection) { if (animal is T) Console.WriteLine(animal); }            
        }

        static void taskShop()
        {
            Shop shop = new Shop();

            Console.WriteLine("Display Customers");
            shop.DisplayCustomers();
            Console.WriteLine("\nDisplay Products");
            shop.DisplayProducts();
            Console.WriteLine("\nDisplay Orders");
            shop.DisplayOrders();


            Console.WriteLine("\nMaking some purchases...");
            shop.BuyProducts(shop.customers[rnd.Next(shop.customers.Count)], new Product[] { shop.products[0], shop.products[4] });
            shop.BuyProducts(shop.customers[rnd.Next(shop.customers.Count)], new Product[] { shop.products[1], shop.products[2], shop.products[3] });
            shop.BuyProducts(shop.customers[rnd.Next(shop.customers.Count)], new Product[] { shop.products[1], shop.products[2] });
            shop.BuyProducts(shop.customers[rnd.Next(shop.customers.Count)], new Product[] { shop.products[2], shop.products[3] });

            Console.WriteLine("\nCurrent state after purchases:");

            Console.WriteLine("\nDisplay Products");
            shop.DisplayProducts();
            Console.WriteLine("\nDisplay Orders");
            shop.DisplayOrders();
        }

        static void task6()
        {

            EmployeeGenerator eg = new EmployeeGenerator();

            // generating employees
            List<Employee> employees = eg.generateEmployees(10);
            
            Console.WriteLine("Generated list of employees - unordered");
            foreach (var empl in employees)            
                Console.WriteLine(empl);

            // sorting by salary
            employees.Sort(new SalaryComparer());
            Console.WriteLine("\nList of employees - sorted by salary");
            foreach (var empl in employees)
                Console.WriteLine(empl);

            // sorting by personal info
            employees.Sort(new PIComparer());
            Console.WriteLine("\nList of employees - sorted by personal info");
            foreach (var empl in employees)
                Console.WriteLine(empl);

        }


        static void task2()
        {

            Student ivanov = new Student("Ivan", "Ivanov", "G01", generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));
            Student petrov = new Student("Petr", "Petrov", "G01", generateMark(Subjects.Arts), generateMark(Subjects.Singing), generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));
            // EXAMPLE parameter with default value
            Student sidorov = new Student("Sidor", "Sidorov", "G01");
            // EXAMPLE using named arguments
            Student doe = new Student(groupId: "G01", firstName: "John", lastName: "Doe");

            // EXAMPLE using params
            Student smith = new Student("John", "Smith", "G01", generateMark(Subjects.Arts), generateMark(Subjects.Singing), generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));
            Student smith2 = new Student("John", "Smith", "G01", generateMark(Subjects.Arts), generateMark(Subjects.Singing), generateMark(Subjects.English), generateMark(Subjects.Maths), generateMark(Subjects.Physics));
            Student[] studs = { ivanov, petrov, sidorov, doe, smith };


            foreach (var stud in studs)
            {
                Console.WriteLine("Student Details: " + stud);
            }

            Console.WriteLine("Equality: " + (smith.Equals(smith2)));

            // finding highest Avg
            decimal highestMathMark = 0;
            foreach (var stud in studs)
            {
                stud.FindHighestSubjMark(Subjects.Maths, ref highestMathMark);
            }
            Console.WriteLine("Highest mark for {0} is {1}", Subjects.Maths, highestMathMark);


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


    // Class for task 6
    public class Employee
    {
        private static ulong seqNum = 0;

        private ulong id;
        public string firstName;
        public string lastName;
        public decimal salary;

        public Employee(string firstName, string lastName, decimal salary)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.salary = salary;
            this.id = ++Employee.seqNum;

        }

        public override string ToString()
        {
            return String.Format("{0} {1} ({3}) earns {2:C2}", this.firstName, this.lastName, this.salary, this.id);
        }

    }
    
    // Helper class for task 6
    public class EmployeeGenerator
    {
        static Random rnd = new Random();

        static string[] firstNames = { "John", "Peter", "Thomas", "Alice", "Joan", "Veronica"};
        static string[] lastNames = { "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White" };

        public Employee generateEmployee()
        {
            return new Employee(firstNames[rnd.Next(firstNames.Length)], lastNames[rnd.Next(lastNames.Length)], rnd.Next(2000, 5001));
        }


        public List<Employee> generateEmployees(int count)
        {
            List<Employee> result = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                result.Add(this.generateEmployee());
            }

            return result;
        }

    }

    public class SalaryComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {

            if (x.salary == y.salary)
                return 0;
            else
                return (x.salary > y.salary) ? 1: -1;
            
        }
    }

    public class PIComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return(x.firstName + x.lastName).CompareTo(y.firstName + y.lastName);
        }
    }

}
