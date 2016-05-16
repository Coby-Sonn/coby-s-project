using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Client
{
    public partial class FirstPage : Form
    {
        public string my_uid;
        public string firstname;
        public string lastname;
        public SocketClient sock_obj;

        public FirstPage()
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

        private void UserSignIn(string username, string password)
        {
            if (username == "")
            {
                MessageBox.Show("Please enter username");
                this.Show();
            }
            else if (password == "")
            {
                MessageBox.Show("Please enter password");
                this.Show();
            }
            else
            {

                string hashed_password = sha256(password);
                //send username and password to python and checks if correct
                string info = "login#" + username + "#" + hashed_password;

                SocketClient sock_obj = new SocketClient();
                this.sock_obj = sock_obj;
                this.sock_obj.StartClient();
                this.sock_obj.Send(info);

                string message_to_split = this.sock_obj.Recv();
                string message = message_to_split.Split('#')[0];
                if (message_to_split.Split('#')[1] != "0")
                {
                    this.my_uid = message_to_split.Split('#')[2];
                    this.firstname = message_to_split.Split('#')[1];
                    this.lastname = message_to_split.Split('#')[3];
                    //MessageBox.Show(my_uid + firstname + lastname);
                }
                

                //if receives true then send the user to the next gui.
                if (message == "Signed in")
                {
                    this.sock_obj.Send("ok");
                    string available_files = this.sock_obj.Recv();
                    string user_info = this.my_uid + "$" + this.firstname + "$" + this.lastname + "$" + available_files;
                    SaveFile form = new SaveFile(user_info);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect password or username");
                    this.Show();
                }
                this.sock_obj.CloseClient();
                this.sock_obj = null;
            }
        }
            
            
          
        
        private void SignInButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserSignIn(LoginUname.Text, LoginPassword.Text);
        } 
        private void resetbutton_Click(object sender, EventArgs e)
        {
            LoginPassword.Clear();
            LoginUname.Clear();
            
        }

       

        

       
        

        

        

       

        
    }
}
