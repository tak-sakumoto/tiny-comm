using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace server
{
    static class Constants
    {
        public const int BUFFER_SIZE = 256;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Get a client IP address
            Console.Write("Input a client IP address > ");
            string client_ip_address_str = Console.ReadLine();
            IPAddress client_ip_address = IPAddress.Parse(client_ip_address_str);

            // Get the client port number
            Console.Write("Input the client port number > ");
            string client_port_str = Console.ReadLine();
            int client_port = Int32.Parse(client_port_str);

            TcpListener listener = null;

            try
            {
                // Make a listener for the client
                var ip_end_point = new IPEndPoint(client_ip_address, client_port);
                listener = new TcpListener(ip_end_point);
                listener.Start();

                // Buffer for recieved data from the client
                Byte[] bytes = new Byte[Constants.BUFFER_SIZE];
                String recieved_data = null;

                // Listening loop
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    NetworkStream stream = client.GetStream();

                    // Loop to receive data from the client
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Print recieved data
                        recieved_data = Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine("Received from the client: {0}", recieved_data);

                        // Get a now date time
                        var now_date = DateTime.Now.ToString();
                        var now_date_bytes = Encoding.UTF8.GetBytes(now_date);

                        // Send back a response
                        stream.Write(now_date_bytes, 0, now_date_bytes.Length);
                        Console.WriteLine(Encoding.UTF8.GetString(now_date_bytes));
                        Console.WriteLine("Sent to the client: {0}", now_date);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
