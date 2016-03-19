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

namespace Server
{
    public partial class SaveFile : Form
    {
        private string my_uid;
        private string my_fname;
        private string my_lname;
        private string file_to_lock;
        private List<string> uid_list = new List<string>();
        private string to_send;
        private BinaryReader br;
        private BinaryWriter bw;
        private NamedPipeServerStream server; 
        
        public SaveFile(string user_info)
        {
            InitializeComponent();
            this.my_uid = user_info.Split('#')[0];
            this.my_fname = user_info.Split('#')[1];
            this.my_lname = user_info.Split('#')[2];
            string greeting_str = "Hello, " + this.my_lname + " " + this.my_fname + ".";
            greeting_str = this.GreetingLabel.Text;

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
            string filename = "";
            filename += Locker.FileName;
            this.file_to_lock = filename;
            filename = ChosenFileView.Text;
            ChosenFileView.Show();
           
            //var server = new NamedPipeServerStream("Communicate");
            this.server = new NamedPipeServerStream("Communicate");
            server.WaitForConnection();
            this.br = new BinaryReader(server);
            this.bw = new BinaryWriter(server);
            send(bw, "Lock");

            string user_info_string = recv(br); // user_info_string = uid@fname@lname@uname#.....
            user_info_string = user_info_string + recv(br);
            string[] users = user_info_string.Split('#');


            // Shutdown the painting of the ListBox as items are added.
            UserData.BeginUpdate();
            // Loop through and add items to the listbox
            foreach (string user_string in users)
            {
                if (!(this.my_uid == user_string.Split('@')[0]))
                {
                    string user_data = user_string.Split('@')[1] + " " + user_string.Split('@')[2] + " " + user_string.Split('@')[0];
                    UserData.Items.Add(user_data);
                }
            }
            // Allow the ListBox to repaint and display the new items.
            UserData.EndUpdate();
            UserData.Show();
            

            

            
        }
        private void continue_lock()
        {
            //MessageBox.Show(this.to_send);

            BinaryWriter bw = this.bw;
            BinaryReader br = this.br;

            string message = this.to_send;
            string first_part, second_part;
            MessageBox.Show("splitting parts");
            if (message.Length % 2 == 0)
            {
                first_part = message.Substring(0, message.Length / 2);
                second_part = message.Substring(message.Length / 2);
            }
            else
            {
                int n = (message.Length / 2) + 1;
                first_part = message.Substring(0, n);
                second_part = message.Substring(n);
            }
            MessageBox.Show(first_part);
            MessageBox.Show(second_part);
            this.send(bw, first_part);
            string ack = this.recv(br);
            ack += this.recv(br);
            MessageBox.Show(ack);
            
            this.send(bw, second_part);

            MessageBox.Show("sent both parts");
            ack = recv(br); 
            ack += recv(br);
            MessageBox.Show(ack);

        }
        private void namesender_Click(object sender, EventArgs e)
        {
            //str_to_send = LockReady#uid#path#uid@uid@...#rbac#optionality
            UserData.Enabled = false;
            for (int i = 0; i < UserData.Items.Count; i++)
                if (UserData.GetItemCheckState(i) == CheckState.Checked)
                {
                   this.uid_list.Add(UserData.Items[i].ToString().Split(' ')[2]);
                }
           
            string path = this.file_to_lock;
            string rbac = "1";
            string optionality = "0";
            string str_to_send = "LockReady#" + this.my_uid + "#" + path + "#";
            string uid_str = "";
            foreach (string uid in this.uid_list)
            {
                uid_str += uid + "@";
            }
            //uid_str = uid_str.Remove(uid_str.Length - 1);
            str_to_send = str_to_send + uid_str + "#" + rbac + "#" + optionality;
            this.to_send = str_to_send;
            this.continue_lock();
            



        }

            





            //need to continue!!!!
        

        private void browse2unlock_Click(object sender, EventArgs e)
        {
            OpenFileDialog Unlocker = new OpenFileDialog();

            Unlocker.ShowDialog();
            Unlocker.InitialDirectory = @"C:\";
            Unlocker.Title = "Browse Files to Unlock";
            string file_to_unlock = Unlocker.FileName;

            string uid = this.my_uid; // need to get the current user uid
            string information_string = "Unlock#" + uid + "#" + file_to_unlock;
            var server = new NamedPipeServerStream("Communicate");
            server.WaitForConnection();
            var br = new BinaryReader(server);
            var bw = new BinaryWriter(server);
            send(bw, information_string);

            string message = recv(br);
            message = message + recv(br);

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
