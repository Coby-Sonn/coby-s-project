using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void UserSignIn(string username, string password)
        {
            //send username and password to python and checks if correct
            string info = username + "#" + password;
            //if receives true then send the user to the next gui.
            string received = "1";
            if (received == "1")
            {
                this.Hide();
                SaveFile form = new SaveFile();
                form.Show();
                
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
                Random rnd = new Random();
                int uid = rnd.Next(10000000, 99999999);
                string UID = uid.ToString();
                string information_string = UID + "#" + firstname + "#" + lastname + "#" + username + "#" + password;
                //send python all the information for registration
                MessageBox.Show(information_string);
                MessageBox.Show("Registration Complete");
            }
    
        }
        private void registerbutton_Click(object sender, EventArgs e)
        {
            AddUser(Fname.Text, Lname.Text, Uname.Text, Password.Text, ConfirmPass.Text);
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            UserSignIn(LoginUname.Text, LoginPassword.Text);
        }

        

        

       

        
    }
}
