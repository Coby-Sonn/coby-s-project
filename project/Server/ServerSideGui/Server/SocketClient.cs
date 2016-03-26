using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Server
{
    public class SocketClient
    {
        
        private int port = 12344;
        public TcpClient client;
        public Stream stm;
        public ASCIIEncoding asen;
        


        public SocketClient()
        {
            TcpClient client = new TcpClient();
            this.client = client;

        }
        public void StartClient()
        {
            this.client.Connect(GetLocalIPAddress(), this.port);
            Stream stm = this.client.GetStream();
            this.stm = stm;
            ASCIIEncoding asen = new ASCIIEncoding();
            this.asen = asen;
        }
        private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }
        public void Send(string data)
        {
            byte[] ba = this.asen.GetBytes(data);
            this.stm.Write(ba, 0, ba.Length);
        }
        public string Recv()
        {
            byte[] bb = new byte[1024];
            int k = this.stm.Read(bb, 0, 1024);
            string info = "";
            for (int i = 0; i < k; i++)
                info = info + Convert.ToChar(bb[i]);
            return info;

        }
        public void CloseClient()
        {
            this.client.Close();
        }
    }
}
