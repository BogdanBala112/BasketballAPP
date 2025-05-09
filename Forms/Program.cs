// using System;
// using System.Net;
// using System.Net.Sockets;
// using System.Threading;
// using System.Windows.Forms;
// using projectC.Forms;
//
// namespace projectC
// {
//     public static class Program
//     {
//         static void Main()
//         {
//
//             if (!IsPortInUse(5000))
//             {
//                 new Thread(() =>
//                 {
//                     try
//                     {
//                         projectC.Server_Connection.server.StartServer();
//                     }
//                     catch (Exception ex)
//                     {
//                         Console.WriteLine("Server failed to start: " + ex.Message);
//                     }
//                 }).Start();
//             }
//             else
//             {
//                 Console.WriteLine("Server already running.");
//             }
//             
//             Application.SetHighDpiMode(HighDpiMode.SystemAware);
//             Application.EnableVisualStyles();
//             Application.SetCompatibleTextRenderingDefault(false);
//             Application.Run(new UserForm());
//         }
//
//         static bool IsPortInUse(int port)
//         {
//             try
//             {
//                 TcpListener listener = new TcpListener(IPAddress.Loopback, port);
//                 listener.Start();
//                 listener.Stop();
//                 return false;
//             }
//             catch (SocketException)
//             {
//                 return true;
//             }
//         }
//     }
// }