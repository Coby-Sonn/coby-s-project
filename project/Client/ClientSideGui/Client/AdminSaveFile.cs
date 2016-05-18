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
using System.Text.RegularExpressions;

namespace Client
{
    public partial class AdminSaveFile : Form
    {
        private string my_uid;
        private string my_fname;
        private string my_lname;
        private string file_to_lock;
        private List<string> uid_list = new List<string>();
        private string to_send;
        public SocketClient sock_obj;
        private string file_to_upload;
        private string available_files;
       
        
        
        public AdminSaveFile(string user_info)
        {
            InitializeComponent();
            this.my_uid = user_info.Split('$')[0];
            this.my_fname = user_info.Split('$')[1];
            this.my_lname = user_info.Split('$')[2];
            this.available_files = user_info.Split('$')[3];
            ResetDriveView();
            namesender.Enabled = false;
        }

        private void ResetDriveView()
        {
            string[] files = this.available_files.Split('#');
            DownloadableBox.BeginUpdate();
            foreach (string file in files)
                DownloadableBox.Items.Add(file);
            DownloadableBox.EndUpdate();




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
            namesender.Enabled = true;
            browse2lock.Enabled = false;
            
            Locker.ShowDialog();
            Locker.InitialDirectory = @"C:\";
            Locker.Title = "Browse Files";
            string filename = "";
            filename += Locker.FileName;
            this.file_to_lock = filename;
            ChosenFile.Text = filename;
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            this.sock_obj.StartClient();
            this.sock_obj.Send("Lock");
            

            string user_info_string = this.sock_obj.Recv(); // user_info_string = uid@fname@lname@uname@ph#.....
            
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
            string message = this.to_send;
            this.sock_obj.Send(message);
            string ack = this.sock_obj.Recv();
            MessageBox.Show(ack);
            this.sock_obj.CloseClient();
            this.sock_obj = null;
            namesender.Enabled = false;
            browse2lock.Enabled = true;
            UserData.Enabled = true;
            ChosenFile.Text = "";
            UserData.ClearSelected();  
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
            //uid_str = uid_str.Remove(uid_str.Length - 1);
            str_to_send = str_to_send + uid_str + "#" + rbac + "#" + optionality;
            this.to_send = str_to_send;
            this.continue_lock();
            



        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            UserDatadel.Enabled = false;
            
            for (int i = 0; i < UserDatadel.Items.Count; i++)
                if (UserDatadel.GetItemCheckState(i) == CheckState.Checked)
                {
                    this.uid_list.Add(UserDatadel.Items[i].ToString().Split(' ')[2]);
                }


            string uid_str = "";
            foreach (string uid in this.uid_list)
            {
                uid_str += uid;
            }

            string user_to_del = uid_str;

            MessageBox.Show(user_to_del);

           
            this.sock_obj.Send(user_to_del);
            MessageBox.Show(this.sock_obj.Recv());
            this.sock_obj.CloseClient();
            this.sock_obj = null;
            
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            this.sock_obj.StartClient();
            this.sock_obj.Send("Delete#");
            string user_info_string = this.sock_obj.Recv(); // user_info_string = uid@fname@lname@uname#.....
            

            string[] users = user_info_string.Split('#');


            // Shutdown the painting of the ListBox as items are added.
            UserDatadel.BeginUpdate();
            // Loop through and add items to the listbox
            foreach (string user_string in users)
            {
                if (!(this.my_uid == user_string.Split('@')[0]))
                {
                    string user_data = user_string.Split('@')[1] + " " + user_string.Split('@')[2] + " " + user_string.Split('@')[0];
                    UserDatadel.Items.Add(user_data);
                }
            }
            // Allow the ListBox to repaint and display the new items.
            UserDatadel.EndUpdate();
            UserDatadel.Show();
            DeleteUser.Visible = true;
            
            

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
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            this.sock_obj.StartClient();
            this.sock_obj.Send(information_string);
            

            string message = this.sock_obj.Recv();

            MessageBox.Show(message);
            


            this.sock_obj.CloseClient();
            this.sock_obj = null;


        }

        private void signout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FirstPage FirstPage = new FirstPage();
            FirstPage.Show();
            try
            {
                this.sock_obj.CloseClient();
            }
            catch
            {
            
            }
        }

        private void showuserschange_Click(object sender, EventArgs e)
        {
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            this.sock_obj.StartClient();
            this.sock_obj.Send("Change#");
            string user_info_string = this.sock_obj.Recv(); // user_info_string = uid@fname@lname@uname#.....


            string[] users = user_info_string.Split('#');


            // Shutdown the painting of the ListBox as items are added.
            UserDatachange.BeginUpdate();
            // Loop through and add items to the listbox
            foreach (string user_string in users)
            {
                string user_data = user_string.Split('@')[1] + " " + user_string.Split('@')[2] + " " + user_string.Split('@')[0];
                UserDatachange.Items.Add(user_data);

            }
            // Allow the ListBox to repaint and display the new items.
            UserDatachange.EndUpdate();
            UserDatachange.Show();
            SendChange.Visible = true;
        }

        private void SendChange_Click(object sender, EventArgs e)
        {
            UserChanges(whattochange.SelectedItem.ToString(), newitem.Text);
        }

        private void UserChanges(string what_to_change, string new_item)
        {
            
            if (UserDatachange.Items.Count == 0)
                MessageBox.Show("Please choose a user");
            if (whattochange.Items.Count == 0)
                MessageBox.Show("Please pick what you want to change for this user...");
            string user_uid = UserDatachange.SelectedItem.ToString().Split(' ')[2];
            string information = "";
            if (what_to_change == "Last Name")
            {
                information = "Change#lname#" + user_uid + "#" + new_item;
            }
            else if (what_to_change == "First Name")
            {
                information = "Change#fname#" + user_uid + "#" + new_item;
            }
            else if (what_to_change == "Password")
            {
                Confirm.Enabled = true;
                newitem.PasswordChar = '●';
                string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{8,15}$";
                if (!Regex.IsMatch(new_item, pattern))
                {
                    MessageBox.Show("Password must be between 8-15 characters and must contain at least one uppercase letter, one lowercase letter and one number");
                }
                else if (new_item != Confirm.Text)
                {
                    MessageBox.Show("Passwords don't match");
                }
                else
                {
                    information = "Change#password#" + user_uid + "#" + sha256(new_item);
                }

            }

            this.sock_obj.Send(information);
            MessageBox.Show(this.sock_obj.Recv());
            this.sock_obj.CloseClient();
            this.sock_obj = null;
            

            
        }

        private void FileChooser_Click(object sender, EventArgs e)
        {
            OpenFileDialog Uploader = new OpenFileDialog();
            Uploader.ShowDialog();
            Uploader.InitialDirectory = @"C:\";
            Uploader.Title = "Choose file to upload";
            this.file_to_upload = Uploader.FileName;
            MessageBox.Show(this.my_uid);
            string file_information = "Upload#" + this.file_to_upload + "#" + this.my_uid;
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            this.sock_obj.StartClient();
            this.sock_obj.Send(file_information);
            string ack = this.sock_obj.Recv();
            MessageBox.Show(ack);

        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            MessageBox.Show("Please choose a download location");
            folderBrowserDialog.ShowDialog();
            string folder_location = folderBrowserDialog.SelectedPath;
            string file_to_download = "";
            DownloadableBox.Enabled = false;
            for (int i = 0; i < DownloadableBox.Items.Count; i++)
                if (DownloadableBox.GetItemCheckState(i) == CheckState.Checked)
                {
                    file_to_download = DownloadableBox.Items[i].ToString();
                }
            if (!(file_to_download == ""))
            {
                this.sock_obj = new SocketClient();
                this.sock_obj.StartClient();
                this.sock_obj.Send("Download#" + file_to_download + "#" + this.my_uid + "#" + folder_location);
                string ack = this.sock_obj.Recv();
                MessageBox.Show(ack);


            }
            else
                MessageBox.Show("Please choose a file to download");
        }

        private void Register(string firstname, string lastname, string username, string password, string confirmPass)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{8,15}$";

            //asks the database if the username exists already
            if (password != confirmPass)
            {
                MessageBox.Show("Passwords do not match");
            }
            else if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Password must be between 8-15 characters and must contain at least one uppercase letter, one lowercase letter and one number");
            }
            //Password must be at least 8 characters long
            else if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long");
            }
            else if (username == null || username.Length < 2 || username.Length > 15)
            {
                MessageBox.Show("Username missing or too short");
            }
            else if (firstname == null || firstname.Length < 2)
            {
                MessageBox.Show("First name missing or too short");
            }
            else if (lastname == null || lastname.Length < 2)
            {
                MessageBox.Show("Last name missing or too short");
            }
            else
            {
                this.Hide();
                Random rnd = new Random();
                int uid = rnd.Next(10000000, 99999999);
                string UID = uid.ToString();
                string hashed_password = sha256(password);
                string information_string = "register#" + UID + "#" + firstname + "#" + lastname + "#" + username + "#" + hashed_password;
                //send python all the information for registration
                SocketClient sock_obj = new SocketClient();
                this.sock_obj = sock_obj;
                this.sock_obj.StartClient();

                this.sock_obj.Send(information_string);
                string message = this.sock_obj.Recv();


                if (message == "Signed up")
                {
                    MessageBox.Show("User Signed Up");
                    this.sock_obj.CloseClient();
                    this.sock_obj = null;
                    this.Show();
                }
                else if (message == "username exists")
                {
                    MessageBox.Show("Username already exists");
                    this.Show();
                    this.sock_obj.CloseClient();
                    this.sock_obj = null;

                }

            }

        }

        //private void registerbutton_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("sent");
        //    Register(Fname.Text, Lname.Text, Uname.Text, Password.Text, ConfirmPass.Text);
        //}

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (PreClosingConfirmation() == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose(true);
                try
                {
                    sock_obj.CloseClient();
                    Application.Exit();
                }
                catch
                {
                    //Application.Exit();
                    
                }

            }
            else
            {
                e.Cancel = true;
            }
        }

        private DialogResult PreClosingConfirmation()
        {
            DialogResult res = System.Windows.Forms.MessageBox.Show(" Do you want to quit?          ", "Quit...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return res;
        }

        private void registerbutton_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("sent");
            Register(Fname.Text, Lname.Text, Uname.Text, Password.Text, ConfirmPass.Text);
        }

        

       
        
        

        

       
        

    }
}
