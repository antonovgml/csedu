using System;
using System.Linq;

namespace GCD {

    /*Develop a console application to compute GCD(Greatest Common Divisor) from a set of more than 2 numbers using different algorithms. Test each algorithm performance by using Stopwatch class.*/


    public class GCDCalc
    {

        public static int Recur(int[] numbers)
        {
            return numbers.Aggregate(GCDRecur);
        }

        public static int Loop(int[] numbers)
        {
            return numbers.Aggregate(GCDRecur);
        }

        public static int Euclid(int[] numbers)
        {
            return numbers.Aggregate(GCDEuclid);
        }


        private static int GCDRecur(int a, int b)
        {
            return b == 0 ? Math.Abs(a) : GCDRecur(b, a % b);
        }


        private static int GCDLoop(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }

        public static int GCDEuclid(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }
            return a;
        }

    }

}