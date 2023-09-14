using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace server
{
    internal class server
    {
        static void Main(string[] args)
        {
            ConnectionInfo client_info = new ConnectionInfo();

            // Get a client IP address
            Console.Write("Input a client IP address > ");
            client_info.ip_address_str = Console.ReadLine();

            // Get the client port number
            Console.Write("Input the client port number > ");
            client_info.port = Int32.Parse(Console.ReadLine());

            TcpListener listener = null;

            try
            {
                // Make a listener for the client
                client_info.set_ip_end_point();
                listener = new TcpListener(client_info.ip_end_point);
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
