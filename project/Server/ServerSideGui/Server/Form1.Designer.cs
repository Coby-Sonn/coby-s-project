namespace Server
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LoginUname = new System.Windows.Forms.TextBox();
            this.LoginPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SignInButton = new System.Windows.Forms.Button();
            this.resetbuttonregister = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.registerbutton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Fname = new System.Windows.Forms.TextBox();
            this.Lname = new System.Windows.Forms.TextBox();
            this.Uname = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.ConfirmPass = new System.Windows.Forms.TextBox();
            this.PassHelp = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginUname
            // 
            this.LoginUname.Location = new System.Drawing.Point(94, 34);
            this.LoginUname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginUname.Name = "LoginUname";
            this.LoginUname.Size = new System.Drawing.Size(100, 20);
            this.LoginUname.TabIndex = 0;
            this.LoginUname.Text = "coby567";
            // 
            // LoginPassword
            // 
            this.LoginPassword.Location = new System.Drawing.Point(94, 73);
            this.LoginPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginPassword.Name = "LoginPassword";
            this.LoginPassword.PasswordChar = '●';
            this.LoginPassword.Size = new System.Drawing.Size(100, 20);
            this.LoginPassword.TabIndex = 1;
            this.LoginPassword.Text = "Hellocoby1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Password";
            // 
            // SignInButton
            // 
            this.SignInButton.Location = new System.Drawing.Point(72, 118);
            this.SignInButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(75, 24);
            this.SignInButton.TabIndex = 5;
            this.SignInButton.Text = "Sign In";
            this.SignInButton.UseVisualStyleBackColor = true;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // resetbuttonregister
            // 
            this.resetbuttonregister.Location = new System.Drawing.Point(34, 204);
            this.resetbuttonregister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetbuttonregister.Name = "resetbuttonregister";
            this.resetbuttonregister.Size = new System.Drawing.Size(75, 24);
            this.resetbuttonregister.TabIndex = 6;
            this.resetbuttonregister.Text = "Reset";
            this.resetbuttonregister.UseVisualStyleBackColor = true;
            this.resetbuttonregister.Click += new System.EventHandler(this.resetbutton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Last Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Confirm Password";
            // 
            // registerbutton
            // 
            this.registerbutton.Location = new System.Drawing.Point(17, 180);
            this.registerbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registerbutton.Name = "registerbutton";
            this.registerbutton.Size = new System.Drawing.Size(75, 24);
            this.registerbutton.TabIndex = 1;
            this.registerbutton.Text = "Register";
            this.registerbutton.UseVisualStyleBackColor = true;
            this.registerbutton.Click += new System.EventHandler(this.registerbutton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "First Name";
            // 
            // Fname
            // 
            this.Fname.Location = new System.Drawing.Point(122, 17);
            this.Fname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Fname.Name = "Fname";
            this.Fname.Size = new System.Drawing.Size(100, 20);
            this.Fname.TabIndex = 14;
            // 
            // Lname
            // 
            this.Lname.Location = new System.Drawing.Point(122, 48);
            this.Lname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lname.Name = "Lname";
            this.Lname.Size = new System.Drawing.Size(100, 20);
            this.Lname.TabIndex = 15;
            // 
            // Uname
            // 
            this.Uname.Location = new System.Drawing.Point(122, 76);
            this.Uname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Uname.Name = "Uname";
            this.Uname.Size = new System.Drawing.Size(100, 20);
            this.Uname.TabIndex = 16;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(122, 104);
            this.Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '•';
            this.Password.Size = new System.Drawing.Size(100, 20);
            this.Password.TabIndex = 17;
            // 
            // ConfirmPass
            // 
            this.ConfirmPass.Location = new System.Drawing.Point(122, 134);
            this.ConfirmPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConfirmPass.Name = "ConfirmPass";
            this.ConfirmPass.PasswordChar = '•';
            this.ConfirmPass.Size = new System.Drawing.Size(100, 20);
            this.ConfirmPass.TabIndex = 18;
            // 
            // PassHelp
            // 
            this.PassHelp.AutoSize = true;
            this.PassHelp.Location = new System.Drawing.Point(228, 106);
            this.PassHelp.Name = "PassHelp";
            this.PassHelp.Size = new System.Drawing.Size(13, 13);
            this.PassHelp.TabIndex = 19;
            this.PassHelp.Text = "?";
            this.toolTip1.SetToolTip(this.PassHelp, "Password must be between 8-15 characters and must contain at least one uppercase " +
                    "letter, one lowercase letter and one number");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PassHelp);
            this.groupBox1.Controls.Add(this.ConfirmPass);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Uname);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Lname);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Fname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.registerbutton);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(271, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(254, 218);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sign up";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LoginPassword);
            this.groupBox2.Controls.Add(this.LoginUname);
            this.groupBox2.Controls.Add(this.SignInButton);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(235, 177);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sign in";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(545, 241);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetbuttonregister);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Start";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox LoginUname;
        private System.Windows.Forms.TextBox LoginPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.Button resetbuttonregister;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button registerbutton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Fname;
        private System.Windows.Forms.TextBox Lname;
        private System.Windows.Forms.TextBox Uname;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox ConfirmPass;
        private System.Windows.Forms.Label PassHelp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

