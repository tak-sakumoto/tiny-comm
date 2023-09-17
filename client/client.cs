using System;
using System.Net.Sockets;
using System.Text;

internal class client
{
    static void Main(string[] args)
    {
        ConnectionInfo server_info = new ConnectionInfo();
        TcpClient tcp_client = null;

        while (true)
        {
            while (true)
            {
                Console.WriteLine("Attempt to connect with server.");

                // Get a server IP address
                server_info.GetIPAddress();

                // Get the server port number
                server_info.GetPortNumber();

                try
                {
                    // Make a client for the server
                    tcp_client = new TcpClient(server_info.ip_address_str, server_info.port);
                    break;
                }
                catch (SocketException)
                {
                    Console.WriteLine("Cannot establish a connection. Please try again.");
                }
            }

            try
            {
                // Send text messages to the server
                SendTextToConsole(tcp_client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    static void SendTextToConsole(TcpClient tcp_client)
    {
        // Buffer
        Byte[] bytes = new Byte[Constants.BUFFER_SIZE];
        String recieved_data = null;

        while (true)
        {
            NetworkStream stream = tcp_client.GetStream();

            Console.Write("Type a message > ");
            string message = Console.ReadLine();

            // Get a now date time
            var now_date = DateTime.Now.ToString();

            // Send a messege
            var sent_data = Encoding.UTF8.GetBytes(message);
            stream.Write(sent_data, 0, sent_data.Length);
            Console.WriteLine("{0} Sent to the server: {1}", now_date, message);

            // Loop to receive data from the server
            int i;
            if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Convert data to a string.
                recieved_data = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                Console.WriteLine("Received from the server: {0}", recieved_data);
            }
        }
    }
}
