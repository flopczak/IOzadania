using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] {100});
            Thread.Sleep(500);
        }
        static void ThreadProc(Object stateInfo)
        {
            var timeToWait = ((object[])stateInfo)[0];
            Thread.Sleep((int)timeToWait);
            Console.WriteLine((int)timeToWait);
            
        }
    }
}
