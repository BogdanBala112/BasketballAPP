using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Server_Connection
{
    public class Server
    {
        private static List<clienthandler> clients = new List<clienthandler>();
        private static readonly object clientLock = new();

        public static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1234);
            listener.Start();
            Console.WriteLine("Server started on port 1234...");

            while (true)
            {
                Socket clientSocket = listener.AcceptSocket();

                var handler = new clienthandler(clientSocket);
                lock (clientLock)
                {
                    clients.Add(handler);
                }

                new Thread(handler.HandleClient).Start();
            }
        }

        public static void Broadcast(string message)
        {
            lock (clientLock)
            {
                foreach (var client in clients)
                {
                    client.SendMessage(message);
                }
            }
        }
    }
}