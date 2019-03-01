using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexLock
{
    class Program
    {
        static Mutex mutex = new Mutex(false, "Our Mutex");
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(AcquireMutex);
                thread.Name = string.Format("Thread {0}", i + 1);
                thread.Start();
            }
        }

        private static void AcquireMutex()
        {
            if(!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                Console.WriteLine(string.Format("Mutex failed to aquire by {0}", Thread.CurrentThread.Name));
                return;
            }
            //mutex.WaitOne();
            DoSomething();
            mutex.ReleaseMutex();
            Console.WriteLine(string.Format("Mutex release by {0}", Thread.CurrentThread.Name));
        }

        private static void DoSomething()
        {
            Thread.Sleep(3000);
            Console.WriteLine(string.Format("Mutex aquired by {0}", Thread.CurrentThread.Name));
        }
    }
}
