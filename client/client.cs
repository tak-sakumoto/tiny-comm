using System;
using System.Net.Sockets;
using System.Text;

internal class client
{
    static void Main(string[] args)
    {
        ConnectionInfo server_info = new ConnectionInfo();

        // Get a server IP address
        Console.Write("Input a server IP address > ");
        server_info.ip_address_str = Console.ReadLine();

        // Get the server port number
        Console.Write("Input the server port number > ");
        server_info.port = Int32.Parse(Console.ReadLine());
            
        TcpClient tcp_client = null;

        try
        {
            // Make a client for the server
            tcp_client = new TcpClient(server_info.ip_address_str, server_info.port);

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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
