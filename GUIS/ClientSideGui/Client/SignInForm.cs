using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Pipes;
using System.IO;

namespace Client
{
    public partial class SignInForm : Form
    {
        public SignInForm()
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

            Console.WriteLine("Read: \"{0}\"", str);

            return str;
        }
        private void UserSignIn(string username, string password)
        {
            string hashed_password = sha256(password);
            MessageBox.Show(hashed_password);
            //send username and password to python and checks if correct
            string info = username + "#" + hashed_password;
            // Open the named pipe.

            var server = new NamedPipeServerStream("Communicate");
            server.WaitForConnection();     
            var br = new BinaryReader(server);
            var bw = new BinaryWriter(server); 
            send(bw, info);
            string message = recv(br); 
            server.Close();
            server.Dispose();

            //if receives true then send the user to the next gui.
            if (message == "1")
            {
                
                FileSave form = new FileSave();
                form.Show();

            }
            else
            {
                
                MessageBox.Show("incorrect password or username");
                this.Show();
            }
            
            
        }
        
        private void resetbutton_Click(object sender, EventArgs e)
        {
            username.Clear();
            password.Clear();

        }
        private void signinbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserSignIn(username.Text, password.Text);
        
        }

       

       
    }
}
