using System;
namespace Util
{

    class C
    {
        public static void p(String msg)
        {
            Console.WriteLine(msg);
        }

        public static void p(String msg, string[] args)
        {
            p(String.Format(msg, args));
        }

    }


}