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
        public SocketClient sock_obj;
        
        
        public SaveFile(string user_info)
        {
            InitializeComponent();
            this.my_uid = user_info.Split('#')[0];
            this.my_fname = user_info.Split('#')[1];
            this.my_lname = user_info.Split('#')[2];
            
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            namesender.Enabled = false;

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
           
            
            this.sock_obj.StartClient();
            this.sock_obj.Send("Lock");

            MessageBox.Show("sent lock");
            string user_info_string = this.sock_obj.Recv(); // user_info_string = uid@fname@lname@uname@ph#.....
            MessageBox.Show("received data");
            string[] users = user_info_string.Split('#');


            // Shutdown the painting of the ListBox as items are added.
            UserData.BeginUpdate();
            // Loop through and add items to the listbox
            
            foreach (string user_string in users)
            {
                
                if (user_string != "")
                {
                    if (!(this.my_uid == user_string.Split('@')[0]))
                    {
                        string user_data = user_string.Split('@')[1] + " " + user_string.Split('@')[2] + " " + user_string.Split('@')[0];
                        UserData.Items.Add(user_data);
                    }
                }
            }
            // Allow the ListBox to repaint and display the new items.
            UserData.EndUpdate();
            UserData.Show();
            namesender.Enabled = true;
            

            

            

            
        }
        private void continue_lock()
        {
            //MessageBox.Show(this.to_send);
            string message = this.to_send;           
            this.sock_obj.Send(message);
            string ack = this.sock_obj.Recv();
            MessageBox.Show(ack);
            this.sock_obj.CloseClient();
            
            

            

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
            string rbac;
            if (rwbox.Checked)
                rbac = "0";
            else
                rbac = "1";
            string optionality = "0";
            string str_to_send = "LockReady#" + this.my_uid + "#" + path + "#";
            string uid_str = "";
            foreach (string uid in this.uid_list)
            {
                uid_str += uid + "@";
            }
            
            str_to_send = str_to_send + uid_str + "#" + rbac + "#" + optionality;
            this.to_send = str_to_send;
            this.continue_lock();
            



        }

        private void browse2unlock_Click_1(object sender, EventArgs e)
        {

            
            OpenFileDialog Unlocker = new OpenFileDialog();

            Unlocker.ShowDialog();
            Unlocker.InitialDirectory = @"C:\";
            Unlocker.Title = "Browse Files to Unlock";
            string file_to_unlock = Unlocker.FileName;

            string uid = this.my_uid; // need to get the current user uid
            string information_string = "Unlock#" + uid + "#" + file_to_unlock;
            this.sock_obj.StartClient();
            this.sock_obj.Send(information_string);
            

            string message = this.sock_obj.Recv();
            

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


            this.sock_obj.CloseClient();


        }

        

    }
}
