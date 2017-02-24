using System;
using Util;
using System.Collections.Generic;
using Shopping;
using AnimalWorld;
using Inheritance;
using Strings;
using InOut;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using ConsumeData;
using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using Collections;
using Validation;
using System.Security.Cryptography;
using System.Text;
using Encryption;

namespace CSEducation
{
    
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            taskEncryption();


        }

        static void taskEncryption()
        {

            C.p("Secure remote console started. Press Ctrl + C or type 'exit' command for exit");
            string message = null;
            string encrypted = null;
            OneTimeCrypto crypter = null;
            byte[] encoded = null;
            string decoded = null;

            do {
                Console.Write("root@remote / $ ");
                message = Console.ReadLine();

                if (message.Length > 50)
                {
                    C.p("Maximum commands length should be <= 50 symbols");                    
                } else if (message.Length == 0)
                {
                    C.p("Empty command. Nothing to encrypt");
                } else if (message.ToLower().Trim().Equals("exit"))
                {
                    break;
                }
                else 
                {
                    // command is OK to be sent to server
                    encoded = Encoding.UTF8.GetBytes(message);
                    decoded = Encoding.UTF8.GetString(encoded);                    
                    crypter = OneTimeCrypto.getInstance();
                    encrypted = crypter.Encrypt(message);
                    C.p("Encrypted command is  being sent to server: " + encrypted);
                    C.p("Server decrypted your command as: \n" + crypter.Decrypt(encrypted));
                }

            } while (true);
            
        }

        static void taskInputValidation()
        {
            IValidate emailV = new EmailValidator();
            IValidate urlV = new URLValidator();
            IValidate pathV = new FilePathValidator();

            C.p("\n\nE-mail validation: \n");
            foreach(var email in getTestEmails())
            {
                C.p("{0,-40} is e-mail valid: {1}", new String[] { email, emailV.validate(email).ToString() });
            }

            C.p("\n\nURL validation: \n");
            foreach (var url in getTestUrls())
            {
                C.p("{0,-40} is URL valid: {1}", new String[] { url, urlV.validate(url).ToString() });
            }

            C.p("\n\nFile path validation: \n");
            foreach (var path in getTestPaths())
            {
                C.p("{0,-80} is path valid: {1}", new String[] { path, pathV.validate(path).ToString() });
            }


        }

        static String[] getTestEmails()
        {
            return new String[]
            {
                "email@domain.com",
                "firstname.lastname@domain.com",
                "email@subdomain.domain.com",
                "firstname+lastname@domain.com",
                "email@123.123.123.123",
                "email@[123.123.123.123]",
                "1234567890@domain.com",
                "email@domain-one.com",
                "_______@domain.com",
                "email@domain.name",
                "email@domain.co.jp",
                "firstname-lastname@domain.com",
                "plainaddress",
                "#@%^%#$@#$@#.com",
                "@domain.com",
                "Joe Smith <email@domain.com>",
                "email.domain.com",
                "email@domain@domain.com",
                "email..email@domain.com",
                "あいうえお@domain.com",
                "email@111.222.333.44444"

            };
        }

        static String[] getTestUrls()
        {
            return new String[] {
                "http://psychopop.org",
                "http://www.edsroom.com/newUser.asp",
                "http://unpleasant.jarrin.net/markov/inde",
                "ftp://psychopop.org",
                "http://www.edsroom/",
                "http://un/pleasant.jarrin.net/markov/index.asp",
                "http://www.myserver.mydomain.com/myfolder/mypage.aspx",
                "www.myserver.mydomain.com/myfolder/mypage.aspx",
                "http://",
                "http://whoisyourdaddy",
                "httpOrhttpsOrftp.com"
            };
        }

        static String[] getTestPaths()
        {
            return new String[] {
                @"c:\Test.txt",
                @"\\server\shared\Test.txt",
                @"\\server\shared\Test.t",
                @"c:\Test",
                @"\\server\shared",
                @"\\server\shared\Test.?",
                @"C:",
                @"\SomeServer",
                @"Hello\\",
                @"../directory/catbus.gif",
                @"C\\bad\test.t"
            };
        }

        static void taskCollections()
        {
            Crew<Worker> crew = new Crew<Worker>();

            crew.Add(new Worker { firstName = "Ivan", lastName = "Ivanov" , age = 20 , workPosition = "Engineer" });
            crew.Add(new Worker { firstName = "Petr", lastName = "Petrov", age = 40, workPosition = "Project Manager" });
            crew.Add(new Worker { firstName = "Sidor", lastName = "Sidorov", age = 30, workPosition = "Architect" });

            var sortedByWorkPosition = crew.OrderBy(w => w, crew).Select(w => w);            

            foreach (var worker in sortedByWorkPosition){
                C.p(worker.ToString());
            }
            
        }

        /* Remake task for "Consuming data" chapter using predefined C# serializers. */
        static void taskSerialization()
        {

            string bookId = "abc";
            // creating book object
            Book book = new Book();
            book.Id = bookId;
            book.Title = "Букварь";
            book.Author = "Иванов И. И.";
            book.PublicationYear = 1980;
            book.Description = "Учебник для 1-го класса";
            book.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse molestie turpis vel dignissim rhoncus. Donec vel efficitur dolor, eget volutpat orci. Mauris eu molestie nisi. Mauris commodo justo eu vehicula imperdiet. Aenean sed malesuada mauris, in pellentesque lorem. Ut eu tempor velit. Donec faucibus sagittis dui sed dictum. Pellentesque sollicitudin nibh ipsum, at efficitur ipsum mattis nec. Ut bibendum consequat leo eget iaculis. Maecenas lacus justo, interdum ut mauris ac, lacinia sodales odio. Suspendisse mauris ante, tincidunt eget molestie gravida, finibus non nisl. Maecenas mattis dolor a erat suscipit pretium. Pellentesque risus risus, rhoncus sed placerat vel, aliquet ut erat. Aliquam pretium nisl efficitur, placerat diam eget, rutrum mauris. Phasellus cursus, est vel commodo lacinia, dolor quam elementum magna, id posuere turpis erat at enim.";

            // XML
            XmlSerializer xser = new XmlSerializer(typeof(Book));

            using (StreamWriter sw = File.CreateText(string.Format(@".\{0}-sn.xml", book.Id)))
            {
                C.p("Saving book to XML...");
                xser.Serialize(sw, book);
                C.p("Book saved to XML. Please check app's current folder");
            }

            using (StreamReader sr = File.OpenText(string.Format(@".\{0}-sn.xml", book.Id)))
            {
                C.p("Reading book from XML...");
                Book deser = (Book)xser.Deserialize(sr);
                C.p("Book details:");
                C.p(deser.ToString());
            }

            // JSON
            DataContractJsonSerializer jser = new DataContractJsonSerializer(typeof(Book));
            using (Stream sw = File.Create(string.Format(@".\{0}-sn.json", book.Id)))
            {
                C.p("\n\nSaving book to JSON...");
                jser.WriteObject(sw,book);
                C.p("Book saved to JSON. Please check app's current folder");
            }

            using (Stream sr = File.OpenRead(string.Format(@".\{0}-sn.json", book.Id)))
            {
                C.p("Reading book from JSON...");
                Book deser = (Book)jser.ReadObject(sr);
                C.p("Book details:");
                C.p(deser.ToString());
            }

        }

        static void taskLINQ()
        {
            Animal[] animals = { new Frog("Jimmy"), new Crocodile("Billy"), new Cat("Kitty"), new Penguin("Timmy"), new Eagle("Johny") };

            C.p("Initial list of animals:");
            displayAnimals(animals);

            IEnumerable<Animal> jumpers = from animal in animals
                                          where animal is IJump
                                          orderby ((IJump)animal).maxDistance
                                          select animal;

            C.p("\nJumpers ordered by max distance:");
            displayAnimals(jumpers);

            IEnumerable<Animal> fliers = animals
                                            .Where(a => a is IFly)
                                            .Select(a => ((Animal)Activator.CreateInstance(a.GetType(), new Object[]{ a.nickName.ToUpper()})));


            C.p("\nFliers CAPITALIZED:");
            displayAnimals(fliers);

            IEnumerable<Animal> swimmers = from a in animals
                                           where a is ISwim
                                           select a;
            C.p("\nSwimmers:");
            displayAnimals(swimmers);

            C.p("\nPassing over a stream... ");
            uint streamWidth = (uint)rnd.Next(1, 100);

            IEnumerable<Animal> swimmedOverStream = animals
                                                    .Where(a => a is ISwim)
                                                    .Where(aSwim => ((ISwim)aSwim).swim(streamWidth))
                                                    .Select(a => a)
                                                    .ToList();
                                                    
            IEnumerable<Animal> flewOverStream = animals
                                                    .Where(a => a is IFly)
                                                    .Where(aFly => ((IFly)aFly).fly(streamWidth))
                                                    .Select(a => a)
                                                    .ToList();

            IEnumerable<Animal> jumpedOverStream = animals
                                                    .Where(a => a is IJump)
                                                    .Where(aJump => ((IJump)aJump).jump(streamWidth))
                                                    .Select(a => a)
                                                    .ToList();


            Console.WriteLine("\nAnimals left:");
            foreach (var animal in swimmedOverStream.Union(flewOverStream).Union(jumpedOverStream)) { Console.WriteLine(animal); }

            


        }

        static void displayAnimals(IEnumerable<Animal> animals)
        {
            foreach (var elem in animals) { Console.WriteLine(elem); }
        }


        static void taskConsumeData()
        {

            string bookId = "abc";
            // creating book object
            Book book = new Book();
            book.Id = bookId;
            book.Title = "Букварь";
            book.Author = "Иванов И. И.";
            book.PublicationYear = 1980;
            book.Description = "Учебник для 1-го класса";
            book.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse molestie turpis vel dignissim rhoncus. Donec vel efficitur dolor, eget volutpat orci. Mauris eu molestie nisi. Mauris commodo justo eu vehicula imperdiet. Aenean sed malesuada mauris, in pellentesque lorem. Ut eu tempor velit. Donec faucibus sagittis dui sed dictum. Pellentesque sollicitudin nibh ipsum, at efficitur ipsum mattis nec. Ut bibendum consequat leo eget iaculis. Maecenas lacus justo, interdum ut mauris ac, lacinia sodales odio. Suspendisse mauris ante, tincidunt eget molestie gravida, finibus non nisl. Maecenas mattis dolor a erat suscipit pretium. Pellentesque risus risus, rhoncus sed placerat vel, aliquet ut erat. Aliquam pretium nisl efficitur, placerat diam eget, rutrum mauris. Phasellus cursus, est vel commodo lacinia, dolor quam elementum magna, id posuere turpis erat at enim.";
            // store in XML
            C.p("Saving book to XML...");
            book.SaveToXML();
            C.p("Book saved to XML. Please check app's current folder");
            // read from XML
            C.p("Reading book from XML...");
            Book bookRestoredXML = Book.LoadFromXML(bookId);
            C.p("Book details:");
            C.p(bookRestoredXML.ToString());
            // store in JSON
            C.p("\n\nSaving book to JSON...");
            bookRestoredXML.SaveToJSON();
            C.p("Book saved to JSON. Please check app's current folder");
            // reading from JSON
            C.p("Reading book from JSON...");
            Book bookRestoredJSON = Book.LoadFromJSON(bookId);
            C.p("Book details:");
            C.p(bookRestoredJSON.ToString());

        }

        static void taskIO()
        {
            FSReader fsr = new FSReader();

            // run the task asynchronously and pause the main thread till its completion
            // wait for completion to not let next operation overwrite console contents
            fsr.listDir(@"C:\Windows").Wait();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            string fileName = @"C:\Windows\win.ini";
            // write to console
            fsr.listFileText(fileName, Console.Out);
            // write (copy) to backup file
            using (StreamWriter sw = new StreamWriter(File.OpenWrite(fileName + ".bak")))
            {
                fsr.listFileText(fileName, sw);
            }
                

        }

        async static Task<string> GetSiteContents(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            WebResponse resp = await req.GetResponseAsync();
            string result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            Console.WriteLine(result);
            return result;
        }

        static void thr()
        {
            

            //Console.WriteLine(Thread.CurrentThread.Name + " started");
            for (int i = 0; i <=10; i++)
            {
                
                Console.SetCursorPosition(i, int.Parse(Thread.CurrentThread.Name));
                Console.Write("{0}", i);
                Thread.Sleep(1000);
            }
            //Console.WriteLine(Thread.CurrentThread.Name + " finished");
        }

        static void taskStrings()
        {
            TextTransformer tt = new TextTransformer("Длинная строка");
            string str1 = "Другая длинная строка";
            C.p("\n\nInitial string: {0} \nTransformed string: {1}", new string[] { tt.InitialText, tt.Transform().TransformedText });            
            C.p("\n\nInitial string: {0} \nTransformed string: {1}", new string[] { str1, tt.TransformWith(str1, Char.ToUpper).TransformedText });
            C.p("\n\nInitial string: {0} \nTransformed string: {1}", new string[] { tt.TransformedText, tt.TransformWith(tt.TransformedText, Char.ToLower).TransformedText });
            C.p("\n\nInitial string: {0} \nTransformed string: {1}", new string[] { str1, tt.TransformWith(str1, c => '*').TransformedText });
            C.p("\n\nInitial string: {0} \nTransformed string: {1}", new string[] { null, tt.Transform(null).TransformedText });
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
