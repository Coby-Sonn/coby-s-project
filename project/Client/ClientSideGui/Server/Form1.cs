using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Server
{
    public partial class Form1 : Form
    {
        public string my_uid;
        public string firstname;
        public string lastname;
        public SocketClient sock_obj;

        public Form1()
        {
            InitializeComponent();
            SocketClient sock_obj = new SocketClient();
            this.sock_obj = sock_obj;
            
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
            else if(password == "")
            {
                MessageBox.Show("Please enter password");
                this.Show();
            }
            else
            {

                string hashed_password = sha256(password);
                //send username and password to python and checks if correct
                string info = "login#" + username + "#" + hashed_password;
                

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
                sock_obj.CloseClient();
                
                //if receives true then send the user to the next gui.
                if (message == "Signed in")
                {
                    string user_info = this.my_uid + "#" + this.firstname + "#" + this.lastname;
                    SaveFile form = new SaveFile(user_info);
                    form.Show();
                }
                else
                {
                
                    MessageBox.Show("incorrect password or username");
                    this.Show();
                }
            
            
            
            }
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
                this.sock_obj.StartClient();
                this.sock_obj.Send(information_string);
                string message = this.sock_obj.Recv();

                this.sock_obj.CloseClient();
                if (message == "Signed up")
                {
                    MessageBox.Show("User Signed Up");
                }
                else if (message == "username exists")
                {
                    this.Show();
                    MessageBox.Show("Username already exists");

                }
                
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
