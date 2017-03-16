using System;
using Util;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;

namespace FlowNS
{

    /*
     Develop a console application to perform math statistics operations over the set of numbers inputted by user. Implement text menu to organise the program flow.
         */

    public class Flow
    // Summary:
    //     Sample usage of various flow management constuctions    

    {
        private static Flow instance = null;

        private const string PROMPT = "> ";
        private const string MENU_HELP = "help";
        private const string MENU_IN = "in";
        private const string MENU_OUT = "out";
        private const string MENU_QUIT = "quit";

        private const string MENU_CALC_MEAN = "mean";
        private const string MENU_CALC_GEOMETRIC_MEAN = "gm";
        private const string MENU_CALC_HARMONIC_MEAN = "hm";
        private const string MENU_CALC_MIN = "min";
        private const string MENU_CALC_MAX = "max";
  
        private static string MENU = String.Format(@"Supported commands:
{0} - this list of commands
{1}   - input a new set of numbers
{2}  - output current set of numbers
{3} - finish working with the app

Statistics available on entered set of numbers:
{4} - estimates the arithmetic sample mean from the unsorted data array
{5} - evaluates the geometric mean of the unsorted data array
{6} - evaluates the harmonic mean of the unsorted data array
{7} - returns the smallest value from the unsorted data array
{8} - returns the largest value from the unsorted data array
", new string[] { MENU_HELP, MENU_IN, MENU_OUT, MENU_QUIT, MENU_CALC_MEAN, MENU_CALC_GEOMETRIC_MEAN, MENU_CALC_HARMONIC_MEAN, MENU_CALC_MIN, MENU_CALC_MAX });


        private const string WRONG_COMMAND = "This command is not suppored. Please use 'help' to view supported commands.";
        private const string BYE = "Exiting... Good bye";

        private double[] numbers;

        private Flow() {
        }

        public static Flow GetInstance()
        {
            return instance = instance ?? new Flow();
        }

        public void Run()
        {
            string input = "";
            Boolean isExit = false;

            this.showMenu();
            do
            {
                Console.Write(PROMPT);
                input = Console.ReadLine().Trim().ToLower();
                isExit = false;
                try
                {
                    isExit = this.ProcessCommand(input);
                }
                catch (Exception e)
                {
                    C.p("Error occurred durring performing this command: " + e.Message);
                    continue;
                }
            } while (!isExit);
        }

        private Boolean ProcessCommand(string command)
        {
            switch (command)
            {
                case "":
                    break;
                    
                case MENU_HELP:
                    this.showMenu();
                    break;

                case MENU_IN:
                    this.numbers = this.ReadNumbers();
                    break;

                case MENU_OUT:
                    this.PrintNumbers();
                    break;

                case MENU_QUIT:
                    C.p(BYE);
                    return true;

                case MENU_CALC_MEAN:
                    C.p(this.calc(ArrayStatistics.Mean, this.numbers).ToString());
                    break;

                case MENU_CALC_GEOMETRIC_MEAN:
                    C.p(this.calc(ArrayStatistics.GeometricMean, this.numbers).ToString());
                    break;

                case MENU_CALC_HARMONIC_MEAN:
                    C.p(this.calc(ArrayStatistics.HarmonicMean, this.numbers).ToString());
                    break;

                case MENU_CALC_MIN:
                    C.p(this.calc(ArrayStatistics.Minimum, this.numbers).ToString());
                    break;

                case MENU_CALC_MAX:
                    C.p(this.calc(ArrayStatistics.Maximum, this.numbers).ToString());
                    break;


                default:
                    C.p(WRONG_COMMAND);
                    break;
            }
            return false;
        }

        public double[] ReadNumbers()
        {
            C.p("Please enter numbers - one number per line. Empty line finishes the sequence");
            string input = "";
            double num;
            List<double> numbers = new List<double>();

            input = Console.ReadLine().Trim();
            while (!input.Trim().Equals(""))
            {
                if (double.TryParse(input, out num))
                {
                    numbers.Add(num);
                }
                else
                {
                    C.p("{0} is not a number", new string[] { input});
                }

                input = Console.ReadLine().Trim();
            };

            C.p("DEBUG: finished input numbers");

            return numbers.ToArray();

        }

        public void PrintNumbers()
        //
        // Summary:
        //     Prints set of numbers on screen
        //
        {
            if (numbers == null)
            {
                C.p("Empty");
                return;
            }

            C.p("Numbers: ");
            foreach (var num in numbers)
            {
                Console.Out.Write("{0}  ", num);
            }
            C.p("\n");
        }

        public void showMenu()
        {
            C.p(MENU);
            
        }

        public double calc(Func<double[], double> fn, double[] data)
        {
            return fn(data);
        }

        
    }

}