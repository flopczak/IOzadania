using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Program
    {
        static async Task serverTask()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                    async (t) =>
                    {
                        int i = t.Result;
                        while (true)
                        {
                            client.GetStream().Write(buffer, 0, i);
                            i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                            String mssgBack = Encoding.ASCII.GetString(buffer);
                            mssgBack = "Wiadomosc zwrotna";
                            buffer = Encoding.ASCII.GetBytes(mssgBack);
                        }
                    });
            }
        }

        static async Task client()
        {
            TcpClient client = new TcpClient("127.0.0.1", 2048);
            String message = "jkb";
            int x;
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            for (int i = 0; i < 5; i++)
            {
                client.GetStream().Write(buffer, 0, buffer.Length);
                x = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                String mssgBack = Encoding.ASCII.GetString(buffer);
                Console.WriteLine("Wiadomosc od servera: " + mssgBack);
            }

        }



        static void Main(string[] args)
        {
            Task t = serverTask();
            Task c = client();
            Task.WaitAll(new Task[] { t, client(), client() });
        }
    }
}
