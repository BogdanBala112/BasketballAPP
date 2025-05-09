using System;
using System.IO;
using System.Net.Sockets;

namespace Server_Connection
{
    public class clienthandler
    {
        private readonly Socket socket;
        private readonly StreamReader reader;
        private readonly StreamWriter writer;
        private String username = "Unknown";

        public clienthandler(Socket socket)
        {
            this.socket = socket;
            var stream = new NetworkStream(socket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        public void HandleClient()
        {
            try
            {
                username = reader.ReadLine();
                Console.WriteLine(username + " connected");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(username +" says: " + line);

                    if (line == "REFRESH_MATCHES")
                    {
                        Server.Broadcast("REFRESH_MATCHES");
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine(username + " disconnected.");
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                writer.WriteLine(message);
            }
            catch
            {
                Console.WriteLine("Failed to send message to client.");
            }
        }
    }
}