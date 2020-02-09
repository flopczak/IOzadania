using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie7
{
    class Program
    {
        delegate String DelegateType(byte[] buff);
        static DelegateType delegat;
        static void Main(string[] args)
        {
            byte[] buffer = new byte[128];
            delegat = new DelegateType(Callback);
            IAsyncResult asyncResult = delegat.BeginInvoke(buffer, null, null);
            FileStream fs = new FileStream("C:\\Users\\user\\Desktop\\me\\FlorekIO\\FlorekIO\\IO\\zad6\\file.txt", FileMode.Open, FileAccess.Read);
            fs.Read(buffer,0,buffer.Length);
            string result = delegat.EndInvoke(asyncResult);
            Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, buffer.Length));
            fs.Close();

        }

        static string Callback(byte[] buff)
        {
            return null;
        }
    }
}
