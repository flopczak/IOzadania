using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zad6
{
    class Program
    {
        
        static void Main(string[] args)
        {
            AsyncCallback callBack = new AsyncCallback(readFileCallback);
            byte[] buffer = new byte[128];
            //--------------waitAll----------------//
            FileStream fs = new FileStream("C:\\Users\\user\\Desktop\\me\\FlorekIO\\FlorekIO\\IO\\zad6\\file.txt", FileMode.Open, FileAccess.Read);
            fs.BeginRead(buffer, 0, buffer.Length, readFileCallback, new object[] { fs, buffer, waitHandles[0] });
            fs = new FileStream("C:\\Users\\user\\Desktop\\me\\FlorekIO\\FlorekIO\\IO\\zad6\\file.txt", FileMode.Open, FileAccess.Read);
            fs.BeginRead(buffer, 0, buffer.Length, readFileCallback, new object[] { fs, buffer, waitHandles[1] });
            WaitHandle.WaitAll(waitHandles);

            //-----------waitAny----------------//
            fs = new FileStream("C:\\Users\\user\\Desktop\\me\\FlorekIO\\FlorekIO\\IO\\zad6\\file.txt", FileMode.Open, FileAccess.Read);
            DateTime dt = DateTime.Now;
            fs.BeginRead(buffer, 0, buffer.Length, readFileCallback, new object[] { fs, buffer, waitHandles[0] });
            fs = new FileStream("C:\\Users\\user\\Desktop\\me\\FlorekIO\\FlorekIO\\IO\\zad6\\file.txt", FileMode.Open, FileAccess.Read);
            fs.BeginRead(buffer, 0, buffer.Length, readFileCallback, new object[] { fs, buffer, waitHandles[1] });
            int index = WaitHandle.WaitAny(waitHandles);
            Console.WriteLine("Task {0} finished first (time waited={1}).",
                index + 1, (DateTime.Now - dt).TotalMilliseconds);
        }
        static WaitHandle[] waitHandles = new WaitHandle[]
   {
        new AutoResetEvent(false),
        new AutoResetEvent(false)
   };

        static void readFileCallback(IAsyncResult result)
        {
            
            FileStream fs = (FileStream)((object[])result.AsyncState)[0];
            byte[] fileText = (byte[])((object[])result.AsyncState)[1];
            AutoResetEvent are = (AutoResetEvent)((object[])result.AsyncState)[2];
            Console.WriteLine(Encoding.UTF8.GetString(fileText, 0, fileText.Length));
            are.Set();
            fs.Close();
        }
    }
}
