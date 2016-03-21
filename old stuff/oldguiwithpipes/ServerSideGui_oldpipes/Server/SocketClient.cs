using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class SocketClient
    {
        private string host;
        private int port = 1234;
        private TcpClient client;
        private Stream stm;
        private ASCIIEncoding asen;
        


        public SocketClient()
        {
            TcpClient client = new TcpClient();
            this.client = client;

        }
        private void StartClient()
        {
            this.client.Connect(GetLocalIPAddress(), this.port);
            Stream stm = this.client.GetStream();
            this.stm = stm;
            ASCIIEncoding asen = new ASCIIEncoding();
            this.asen = asen;
        }
        public string GetLocalIPAddress()
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
        private void Send(string data)
        {
            byte[] ba = this.asen.GetBytes(data);
            this.stm.Write(ba, 0, ba.Length);
        }
        private string Recv()
        {
            byte[] bb = new byte[100];
            int k = this.stm.Read(bb, 0, 100);
            string info = "";
            for (int i = 0; i < k; i++)
                info = info + Convert.ToChar(bb[i]);
            return info;

        }
        private void CloseClient()
        {
            this.client.Close();
        }
    }
}
