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
    public partial class SaveFile : Form
    {
        private string my_uid;
        private string my_fname;
        private string my_lname;
        private string file_to_lock;
        private string file_to_upload;
        private string available_files;
        private List<string> uid_list = new List<string>();
        private string to_send;
        public SocketClient sock_obj;
        
        
        public SaveFile(string user_info)
        {
            InitializeComponent();
            this.my_uid = user_info.Split('$')[0];
            this.my_fname = user_info.Split('$')[1];
            this.my_lname = user_info.Split('$')[2];
            this.available_files = user_info.Split('$')[3];
            ResetDriveView();

            
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

        private void ResetDriveView()
        {
            string[] files = this.available_files.Split('#');
            DownloadableBox.BeginUpdate();
            foreach (string file in files)
                DownloadableBox.Items.Add(file);
            DownloadableBox.EndUpdate();
            

            

        }

        private void browse2lock_Click(object sender, EventArgs e)
        {
            OpenFileDialog Locker = new OpenFileDialog();
            namesender.Enabled = true;
            browse2lock.Enabled = false;
            Locker.ShowDialog();
            Locker.InitialDirectory = @"C:\";
            Locker.Title = "Browse Files";
            this.file_to_lock = Locker.FileName;
            ChosenFile.Text = Locker.FileName;           
            this.sock_obj = new SocketClient(); 
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
            string ack = "";
            string message = this.to_send;
            if (this.sock_obj != null)
            {
                this.sock_obj.Send(message);
                try
                {
                    ack = this.sock_obj.Recv();
                }
                catch
                {
                    ack = "there was an error, please try again";
                }
                MessageBox.Show(ack);
                this.sock_obj.CloseClient();
                this.sock_obj = null;
                UserData.Items.Clear();
                browse2lock.Enabled = true;
            }
            else
                MessageBox.Show("there was an error, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string rbac = rwbox.Checked ? "0" : "1";
            string optionality = "0";
            string str_to_send = "LockReady#" + this.my_uid + "#" + path + "#";
            string uid_str = "";
            foreach (string uid in this.uid_list)
            {
                uid_str += uid + "@";
                str_to_send = str_to_send + uid_str + "#" + rbac + "#" + optionality;
                this.to_send = str_to_send;
                this.continue_lock();
            }
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
            if (message == "File Unlocked" || message == "File unlocked, user can only read the file")
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
            this.sock_obj = null;
        }

        private void signout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FirstPage FirstPage = new FirstPage();
            FirstPage.Show();
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
                    Application.Exit();
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

       

        

       
        

    }
}
