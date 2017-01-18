using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InOut
{
    /* Develop a simple console application that allowes to list all files in directory and read contents of chosen file. */

    internal class FSReader
    {

        public FSReader()
        {

        }

        /*async tasks example*/
        public async Task listDir(string dirPath)
        {

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            // listing directories and files as two separate files

            var taskDirs = Task.Run(() =>
            {
                int rowDir = 1;
                int colDir = 0;

                lock (Console.Out)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Directories: ");
                }
                foreach (var dir in dirInfo.GetDirectories())
                {
                    lock (Console.Out)
                    {
                        Console.SetCursorPosition(colDir, rowDir++);
                        Console.Write(dir.Name);
                    }
                    // work hard
                    Thread.Sleep(100);
                }
            });


            var taskFiles = Task.Run(() =>
            {
                int rowFile = 1;
                int colFile = 40;
                lock (Console.Out)
                {
                    Console.SetCursorPosition(colFile, 0);
                    Console.Write("Files: ");
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    lock (Console.Out)
                    {
                        Console.SetCursorPosition(colFile, rowFile++);
                        Console.Write(file.Name);
                    }
                    // work hard
                    Thread.Sleep(50);
                }
            });

            // wait completion of the two above tasks
            await Task.WhenAll(taskDirs, taskFiles);

        }
        

        /* read contents of a chosen file into a TextWriter line by line */
        public async void listFileText(string filePath, TextWriter tw)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        await tw.WriteLineAsync(await sr.ReadLineAsync());
                        // work hard
                        Thread.Sleep(50);
                    }
                    tw.Flush();
                }
            }
            else
            {
                Console.WriteLine("No such file");
            }

            return;

        }

    }
}