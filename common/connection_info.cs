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

    public int port { set; get; }

    public IPEndPoint ip_end_point;

    public void set_ip_end_point()
    {
        this.ip_end_point = new IPEndPoint(this._ip_address, this.port);
    }
}