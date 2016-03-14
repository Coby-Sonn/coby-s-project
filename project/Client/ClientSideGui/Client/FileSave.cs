using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;

namespace Client
{
    public partial class FileSave : Form
    {

        public FileSave()
        {
            InitializeComponent();
        }

        

        static string sha256(string password)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        private void send(BinaryWriter bw, string info)
        {
            var buf = Encoding.ASCII.GetBytes(info);     // Get ASCII byte array     
            bw.Write((uint)buf.Length);                // Write string length
            bw.Write(buf);                              // Write string

        }
        private string recv(BinaryReader br)
        {
            var len = (int)br.ReadUInt32();            // Read string length
            var str = new string(br.ReadChars(len));    // Read string

            //Console.WriteLine("Read: \"{0}\"", str);

            return str;
        }

        private void browse2lock_Click(object sender, EventArgs e)
        {
            OpenFileDialog Locker = new OpenFileDialog();

            Locker.ShowDialog();
            Locker.InitialDirectory = @"C:\";
            Locker.Title = "Browse Files";
            string file_to_lock = Locker.FileName;
            var server = new NamedPipeServerStream("Communicate");
            server.WaitForConnection();
            var br = new BinaryReader(server);
            var bw = new BinaryWriter(server);
            send(bw, "Lock");

            string user_info_string = recv(br);

            //need to continue!!!!
        }

        private void browse2unlock_Click(object sender, EventArgs e)
        {
            OpenFileDialog Unlocker = new OpenFileDialog();

            Unlocker.ShowDialog();
            Unlocker.InitialDirectory = @"C:\";
            Unlocker.Title = "Browse Files to Unlock";
            string file_to_unlock = Unlocker.FileName;
            string uid = "245235234"; // need to get the current user uid
            string information_string = "Unlock#" + uid + "#" + file_to_unlock;
            var server = new NamedPipeServerStream("Communicate");
            server.WaitForConnection();
            var br = new BinaryReader(server);
            var bw = new BinaryWriter(server);
            send(bw, information_string);

            string message = recv(br);

            if (message == "File Unlocked")
            {
                MessageBox.Show(message);
            }
            else if (message == "The specified user is not allowed to open the file")
            {
                MessageBox.Show(message);
            }
            else if (message == "path error, can only unlock .cb files")
            {
                MessageBox.Show(message);
            }


            server.Close();
            server.Dispose();


        }

        

        

        
    }
}
