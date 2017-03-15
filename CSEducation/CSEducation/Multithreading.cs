using System;
using System.Diagnostics;
using System.Threading;

namespace Multithreading
{

    /*
     Develop a stopwatch console application using threading.
     */
     public class ThreadedStopwatch:Stopwatch
    {
        [ThreadStatic]
        private int left;
        [ThreadStatic]
        private int top;

        TimeSpan zero = new TimeSpan(0);

        public ThreadedStopwatch():this(Console.BufferWidth - 20, 0) {

        }

        public ThreadedStopwatch(int left, int top) {
            this.left = left;
            this.top = top;

            Thread t = new Thread(new ThreadStart(() => {
                int tmpLeft, tmpTop;
                
                Thread.Sleep(100);
                do
                {
                    if (this.IsRunning || this.Elapsed.Equals(zero))
                    {
                        lock (Console.Out)
                        {
                            tmpLeft = Console.CursorLeft;
                            tmpTop = Console.CursorTop;
                            Console.SetCursorPosition(this.left, this.top);
                            Console.Write("Elapsed {0}", this.Elapsed.ToString(@"hh\:mm\:ss\.ff"));
                            Console.SetCursorPosition(tmpLeft, tmpTop);
                        }
                            Thread.Sleep(50);
                        
                    }
                } while (true);
            }));
            t.IsBackground = true;
            t.Start();
            
        }
    }
}