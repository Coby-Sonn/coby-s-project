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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(262, 235);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.resetbuttonregister);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Start";
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
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

