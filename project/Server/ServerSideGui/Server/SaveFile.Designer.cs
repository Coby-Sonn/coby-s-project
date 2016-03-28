namespace Server
{
    partial class SaveFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveFile));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.signout = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.browse2unlock = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UserData = new System.Windows.Forms.CheckedListBox();
            this.namesender = new System.Windows.Forms.Button();
            this.browse2lock = new System.Windows.Forms.Button();
            this.ChosenFileView = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.userchanges = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Confirm = new System.Windows.Forms.TextBox();
            this.SendChange = new System.Windows.Forms.Button();
            this.newitem = new System.Windows.Forms.TextBox();
            this.whattochange = new System.Windows.Forms.ComboBox();
            this.UserDatachange = new System.Windows.Forms.CheckedListBox();
            this.showuserschange = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UserButton = new System.Windows.Forms.Button();
            this.UserDatadel = new System.Windows.Forms.CheckedListBox();
            this.DeleteUser = new System.Windows.Forms.Button();
            this.Locker = new System.Windows.Forms.OpenFileDialog();
            this.Unlocker = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rbox = new System.Windows.Forms.RadioButton();
            this.rwbox = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.userchanges.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(288, 316);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.signout);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.ChosenFileView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(280, 290);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lock";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // signout
            // 
            this.signout.AutoSize = true;
            this.signout.Location = new System.Drawing.Point(232, 0);
            this.signout.Name = "signout";
            this.signout.Size = new System.Drawing.Size(48, 13);
            this.signout.TabIndex = 8;
            this.signout.TabStop = true;
            this.signout.Text = "Sign Out";
            this.signout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.signout_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.browse2unlock);
            this.groupBox2.Location = new System.Drawing.Point(14, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 62);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unlock";
            // 
            // browse2unlock
            // 
            this.browse2unlock.Location = new System.Drawing.Point(14, 24);
            this.browse2unlock.Name = "browse2unlock";
            this.browse2unlock.Size = new System.Drawing.Size(75, 23);
            this.browse2unlock.TabIndex = 5;
            this.browse2unlock.Text = "Browse Files";
            this.browse2unlock.UseVisualStyleBackColor = true;
            this.browse2unlock.Click += new System.EventHandler(this.browse2unlock_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbox);
            this.groupBox1.Controls.Add(this.rwbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.UserData);
            this.groupBox1.Controls.Add(this.namesender);
            this.groupBox1.Controls.Add(this.browse2lock);
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 177);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lock";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "?";
            this.toolTip1.SetToolTip(this.label1, "If you choose to lock the selected file for yourself only, do not choose any user" +
                    " from the list.\r\n");
            // 
            // UserData
            // 
            this.UserData.CheckOnClick = true;
            this.UserData.FormattingEnabled = true;
            this.UserData.Location = new System.Drawing.Point(6, 19);
            this.UserData.Name = "UserData";
            this.UserData.Size = new System.Drawing.Size(115, 124);
            this.UserData.Sorted = true;
            this.UserData.TabIndex = 4;
            this.UserData.ThreeDCheckBoxes = true;
            // 
            // namesender
            // 
            this.namesender.Location = new System.Drawing.Point(127, 120);
            this.namesender.Name = "namesender";
            this.namesender.Size = new System.Drawing.Size(84, 23);
            this.namesender.TabIndex = 3;
            this.namesender.Text = "Send Names";
            this.namesender.UseVisualStyleBackColor = true;
            this.namesender.Click += new System.EventHandler(this.namesender_Click);
            // 
            // browse2lock
            // 
            this.browse2lock.Location = new System.Drawing.Point(127, 91);
            this.browse2lock.Name = "browse2lock";
            this.browse2lock.Size = new System.Drawing.Size(84, 23);
            this.browse2lock.TabIndex = 0;
            this.browse2lock.Text = "Browse Files";
            this.browse2lock.UseVisualStyleBackColor = true;
            this.browse2lock.Click += new System.EventHandler(this.browse2lock_Click);
            // 
            // ChosenFileView
            // 
            this.ChosenFileView.AutoSize = true;
            this.ChosenFileView.Location = new System.Drawing.Point(321, 163);
            this.ChosenFileView.Name = "ChosenFileView";
            this.ChosenFileView.Size = new System.Drawing.Size(0, 13);
            this.ChosenFileView.TabIndex = 2;
            this.ChosenFileView.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.userchanges);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(280, 290);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "User Options";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // userchanges
            // 
            this.userchanges.Controls.Add(this.label2);
            this.userchanges.Controls.Add(this.Confirm);
            this.userchanges.Controls.Add(this.SendChange);
            this.userchanges.Controls.Add(this.newitem);
            this.userchanges.Controls.Add(this.whattochange);
            this.userchanges.Controls.Add(this.UserDatachange);
            this.userchanges.Controls.Add(this.showuserschange);
            this.userchanges.Location = new System.Drawing.Point(14, 162);
            this.userchanges.Name = "userchanges";
            this.userchanges.Size = new System.Drawing.Size(241, 117);
            this.userchanges.TabIndex = 4;
            this.userchanges.TabStop = false;
            this.userchanges.Text = "User Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "confirm password";
            // 
            // Confirm
            // 
            this.Confirm.Enabled = false;
            this.Confirm.Location = new System.Drawing.Point(99, 75);
            this.Confirm.Name = "Confirm";
            this.Confirm.PasswordChar = '●';
            this.Confirm.Size = new System.Drawing.Size(92, 20);
            this.Confirm.TabIndex = 5;
            // 
            // SendChange
            // 
            this.SendChange.Location = new System.Drawing.Point(6, 88);
            this.SendChange.Name = "SendChange";
            this.SendChange.Size = new System.Drawing.Size(75, 23);
            this.SendChange.TabIndex = 4;
            this.SendChange.Text = "Send";
            this.SendChange.UseVisualStyleBackColor = true;
            this.SendChange.Click += new System.EventHandler(this.SendChange_Click);
            // 
            // newitem
            // 
            this.newitem.Location = new System.Drawing.Point(99, 49);
            this.newitem.Name = "newitem";
            this.newitem.Size = new System.Drawing.Size(92, 20);
            this.newitem.TabIndex = 3;
            this.toolTip1.SetToolTip(this.newitem, "Password must be between 8-15 characters and must contain at least one uppercase " +
                    "letter, one lowercase letter and one number");
            // 
            // whattochange
            // 
            this.whattochange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.whattochange.FormattingEnabled = true;
            this.whattochange.Items.AddRange(new object[] {
            "First Name",
            "Last Name",
            "Password"});
            this.whattochange.Location = new System.Drawing.Point(6, 48);
            this.whattochange.Name = "whattochange";
            this.whattochange.Size = new System.Drawing.Size(75, 21);
            this.whattochange.TabIndex = 2;
            // 
            // UserDatachange
            // 
            this.UserDatachange.CheckOnClick = true;
            this.UserDatachange.FormattingEnabled = true;
            this.UserDatachange.Location = new System.Drawing.Point(99, 9);
            this.UserDatachange.Name = "UserDatachange";
            this.UserDatachange.Size = new System.Drawing.Size(120, 34);
            this.UserDatachange.TabIndex = 1;
            // 
            // showuserschange
            // 
            this.showuserschange.Location = new System.Drawing.Point(6, 19);
            this.showuserschange.Name = "showuserschange";
            this.showuserschange.Size = new System.Drawing.Size(75, 23);
            this.showuserschange.TabIndex = 0;
            this.showuserschange.Text = "Show Users";
            this.showuserschange.UseVisualStyleBackColor = true;
            this.showuserschange.Click += new System.EventHandler(this.showuserschange_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UserButton);
            this.groupBox3.Controls.Add(this.UserDatadel);
            this.groupBox3.Controls.Add(this.DeleteUser);
            this.groupBox3.Location = new System.Drawing.Point(14, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 139);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Delete User";
            // 
            // UserButton
            // 
            this.UserButton.Location = new System.Drawing.Point(18, 21);
            this.UserButton.Name = "UserButton";
            this.UserButton.Size = new System.Drawing.Size(75, 23);
            this.UserButton.TabIndex = 2;
            this.UserButton.Text = "Show Users";
            this.UserButton.UseVisualStyleBackColor = true;
            this.UserButton.Click += new System.EventHandler(this.UserButton_Click);
            // 
            // UserDatadel
            // 
            this.UserDatadel.FormattingEnabled = true;
            this.UserDatadel.Location = new System.Drawing.Point(18, 50);
            this.UserDatadel.Name = "UserDatadel";
            this.UserDatadel.ScrollAlwaysVisible = true;
            this.UserDatadel.Size = new System.Drawing.Size(120, 49);
            this.UserDatadel.TabIndex = 1;
            // 
            // DeleteUser
            // 
            this.DeleteUser.Location = new System.Drawing.Point(18, 105);
            this.DeleteUser.Name = "DeleteUser";
            this.DeleteUser.Size = new System.Drawing.Size(75, 23);
            this.DeleteUser.TabIndex = 0;
            this.DeleteUser.Text = "Delete User";
            this.DeleteUser.UseVisualStyleBackColor = true;
            this.DeleteUser.Visible = false;
            this.DeleteUser.Click += new System.EventHandler(this.DeleteUser_Click);
            // 
            // Unlocker
            // 
            this.Unlocker.FileName = "openFileDialog1";
            this.Unlocker.Filter = "CB files|*.cb|All files|*.*";
            // 
            // rbox
            // 
            this.rbox.AutoSize = true;
            this.rbox.Location = new System.Drawing.Point(127, 58);
            this.rbox.Name = "rbox";
            this.rbox.Size = new System.Drawing.Size(75, 17);
            this.rbox.TabIndex = 11;
            this.rbox.TabStop = true;
            this.rbox.Text = "Read Only";
            this.rbox.UseVisualStyleBackColor = true;
            // 
            // rwbox
            // 
            this.rwbox.AutoSize = true;
            this.rwbox.Location = new System.Drawing.Point(127, 35);
            this.rwbox.Name = "rwbox";
            this.rwbox.Size = new System.Drawing.Size(100, 17);
            this.rwbox.TabIndex = 10;
            this.rwbox.TabStop = true;
            this.rwbox.Text = "Read and Write";
            this.rwbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rwbox.UseVisualStyleBackColor = true;
            // 
            // SaveFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 312);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveFile";
            this.Text = "SaveFile";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.userchanges.ResumeLayout(false);
            this.userchanges.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button browse2lock;
        private System.Windows.Forms.OpenFileDialog Locker;
        private System.Windows.Forms.OpenFileDialog Unlocker;
        private System.Windows.Forms.Label ChosenFileView;
        private System.Windows.Forms.Button namesender;
        private System.Windows.Forms.CheckedListBox UserData;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckedListBox UserDatadel;
        private System.Windows.Forms.Button DeleteUser;
        private System.Windows.Forms.Button UserButton;
        private System.Windows.Forms.Button browse2unlock;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel signout;
        private System.Windows.Forms.GroupBox userchanges;
        private System.Windows.Forms.Button SendChange;
        private System.Windows.Forms.TextBox newitem;
        private System.Windows.Forms.ComboBox whattochange;
        private System.Windows.Forms.CheckedListBox UserDatachange;
        private System.Windows.Forms.Button showuserschange;
        private System.Windows.Forms.TextBox Confirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbox;
        private System.Windows.Forms.RadioButton rwbox;

    }
}