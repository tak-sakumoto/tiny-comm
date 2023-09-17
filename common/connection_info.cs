using System;
using System.Net;

public class ConnectionInfo
{
    private IPAddress _ip_address;
    private string _ip_address_str;

    public string ip_address_str
    {
        set {
            this._ip_address_str = value;
            this._ip_address = IPAddress.Parse(this._ip_address_str);
        }
        get
        {
            return this._ip_address_str;
        }
    }
    public IPAddress ip_address
    {
        set
        {
            this._ip_address = value;
            this._ip_address_str = this._ip_address.ToString();
        }
        get
        {
            return this._ip_address;
        }
    }

    public int port;
    private const int MIN_PORT_NUM = 1;
    private const int MAX_PORT_NUM = 65535;

    public IPEndPoint ip_end_point;

    public void set_ip_end_point()
    {
        this.ip_end_point = new IPEndPoint(this._ip_address, this.port);
    }

    public void GetIPAddress()
    {
        string tmp;
        while (true)
        {
            Console.Write("Enter an IP address > ");
            tmp = Console.ReadLine();
            if (IPAddress.TryParse(tmp, out _ip_address))
            {
                this.ip_address = _ip_address;
                break;
            }
            Console.WriteLine("Invalid IP address. Please try again.");
        }
    }

    public void GetPortNumber()
    {
        string tmp;
        while (true)
        {
            Console.Write("Enter a port number > ");
            tmp = Console.ReadLine();
            if (int.TryParse(tmp, out this.port) 
                && this.port >= MIN_PORT_NUM && this.port <= MAX_PORT_NUM)
            {
                break;
            }
            Console.WriteLine("Invalid port number. Please enter a valid port between 1 and 65535.");
        }
    }
}