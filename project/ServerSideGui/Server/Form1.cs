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

        public Form1()
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

            
            
            return str;
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
                // Open the named pipe.

                var server = new NamedPipeServerStream("Communicate");
                server.WaitForConnection();     
                var br = new BinaryReader(server);
                var bw = new BinaryWriter(server); 
                send(bw, info);
                string message_to_split = recv(br);
                message_to_split = message_to_split + recv(br);
                string message = message_to_split.Split('#')[0];
                if (message_to_split.Split('#')[1] != "0")
                {
                    this.my_uid = message_to_split.Split('#')[1];
                    this.firstname = message_to_split.Split('#')[2];
                    this.lastname = message_to_split.Split('#')[3];
                }
                server.Close();
                server.Dispose();
                
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
        private void AddUser(string firstname, string lastname, string username, string password, string confirmPass)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.* ).{8,15}$";
            
            //asks the database if the username exists already
            if (password != confirmPass)
            {
                MessageBox.Show("Passwords do not match");
            }
            else if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show(password);
                
                MessageBox.Show("Password must be between 8-15 characters and must contain at least one uppercase letter, one lowercase letter, one number and no special characters besides space");
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

                var server = new NamedPipeServerStream("Communicate");
                server.WaitForConnection();
                var br = new BinaryReader(server);
                var bw = new BinaryWriter(server);
                send(bw, information_string);
                string message = recv(br);
                server.Close();
                server.Dispose();
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
        private void registerbutton_Click(object sender, EventArgs e)
        {
            AddUser(Fname.Text, Lname.Text, Uname.Text, Password.Text, ConfirmPass.Text);
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
            Fname.Clear();
            Lname.Clear();
            Uname.Clear();
            Password.Clear();
            ConfirmPass.Clear();
        }

        

        

       

        
    }
}
