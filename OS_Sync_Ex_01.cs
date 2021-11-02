using System;
using System.Diagnostics;
using System.Threading;

namespace OS_Sync_Ex_01
{
    class Program
    {
        private static string x = "";
        private static int exitflag = 0;
        private static object _Lock = new object();

        static void ThReadX()
        {
            while (exitflag == 0)
            {
                Thread.Sleep(100);
                lock (_Lock)
                {
                    if (exitflag != 1)
                        Console.WriteLine("X = {0}", x);
                }
            }
        }

        static void ThWriteX()
        {
            string xx;

            while (exitflag == 0)
                lock (_Lock)
                {
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit")
                        exitflag = 1;
                    else
                        x = xx;
                }
        }

        static void Main(string[] args)
        {
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);

            A.Start();
            B.Start();
            A.Join();
            B.Join();
            Console.WriteLine("Thread 1 exit");
        }
    }
}
