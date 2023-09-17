using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Remoting.Contexts;

internal class server
{
    static void Main(string[] args)
    {
        ConnectionInfo client_info = new ConnectionInfo();
        TcpListener listener = null;

        while (true)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Attempt to connect with client.");

                    // Get a client IP address
                    client_info.GetIPAddress();

                    // Get the client port number
                    client_info.GetPortNumber();

                    // Make a listener for the client
                    client_info.set_ip_end_point();

                    listener = new TcpListener(client_info.ip_end_point);
                    listener.Start();
                    break;
                }
                catch (SocketException)
                {
                    Console.WriteLine("Cannot establish a connection. Please try again.");
                }
            }

            Listen(listener);
            Console.WriteLine("Finish listening.");
        }
    }

    static void Listen(TcpListener listener)
    {
        try
        {
            // Listening loop
            while (true)
            {
                // Accept requests from the client
                TcpClient client = listener.AcceptTcpClient();

                // Recieve text messages from the client
                RecieveTextFromClient(client);
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

    static void RecieveTextFromClient(TcpClient client)
    {
        // Buffer for recieved data from the client
        Byte[] bytes = new Byte[Constants.BUFFER_SIZE];
        String recieved_data = null;

        // Get a stream object for the client
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