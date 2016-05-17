namespace Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveFile));
            this.Locker = new System.Windows.Forms.OpenFileDialog();
            this.Unlocker = new System.Windows.Forms.OpenFileDialog();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.signout = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.browse2unlock = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChosenFile = new System.Windows.Forms.TextBox();
            this.rbox = new System.Windows.Forms.RadioButton();
            this.rwbox = new System.Windows.Forms.RadioButton();
            this.UserData = new System.Windows.Forms.CheckedListBox();
            this.namesender = new System.Windows.Forms.Button();
            this.browse2lock = new System.Windows.Forms.Button();
            this.ChosenFileView = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DriveTab = new System.Windows.Forms.TabPage();
            this.RefreshDrive = new System.Windows.Forms.Button();
            this.UploadBox = new System.Windows.Forms.GroupBox();
            this.FileChooser = new System.Windows.Forms.Button();
            this.DownloadBox = new System.Windows.Forms.GroupBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.DownloadableBox = new System.Windows.Forms.CheckedListBox();
            this.Uploader = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.DriveTab.SuspendLayout();
            this.UploadBox.SuspendLayout();
            this.DownloadBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Unlocker
            // 
            this.Unlocker.FileName = "openFileDialog1";
            this.Unlocker.Filter = "CB files|*.cb|All files|*.*";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.signout);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.ChosenFileView);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(426, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File Options";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // signout
            // 
            this.signout.AutoSize = true;
            this.signout.Location = new System.Drawing.Point(350, 0);
            this.signout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.signout.Name = "signout";
            this.signout.Size = new System.Drawing.Size(71, 20);
            this.signout.TabIndex = 9;
            this.signout.TabStop = true;
            this.signout.Text = "Sign Out";
            this.signout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.signout_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.browse2unlock);
            this.groupBox2.Location = new System.Drawing.Point(21, 322);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(362, 95);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unlock";
            // 
            // browse2unlock
            // 
            this.browse2unlock.Location = new System.Drawing.Point(21, 37);
            this.browse2unlock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browse2unlock.Name = "browse2unlock";
            this.browse2unlock.Size = new System.Drawing.Size(112, 35);
            this.browse2unlock.TabIndex = 5;
            this.browse2unlock.Text = "Browse Files";
            this.browse2unlock.UseVisualStyleBackColor = true;
            this.browse2unlock.Click += new System.EventHandler(this.browse2unlock_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChosenFile);
            this.groupBox1.Controls.Add(this.rbox);
            this.groupBox1.Controls.Add(this.rwbox);
            this.groupBox1.Controls.Add(this.UserData);
            this.groupBox1.Controls.Add(this.namesender);
            this.groupBox1.Controls.Add(this.browse2lock);
            this.groupBox1.Location = new System.Drawing.Point(21, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(362, 272);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lock";
            // 
            // ChosenFile
            // 
            this.ChosenFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChosenFile.Location = new System.Drawing.Point(9, 160);
            this.ChosenFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChosenFile.Name = "ChosenFile";
            this.ChosenFile.ReadOnly = true;
            this.ChosenFile.Size = new System.Drawing.Size(294, 19);
            this.ChosenFile.TabIndex = 10;
            // 
            // rbox
            // 
            this.rbox.AutoSize = true;
            this.rbox.Location = new System.Drawing.Point(204, 192);
            this.rbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbox.Name = "rbox";
            this.rbox.Size = new System.Drawing.Size(108, 24);
            this.rbox.TabIndex = 9;
            this.rbox.TabStop = true;
            this.rbox.Text = "Read Only";
            this.rbox.UseVisualStyleBackColor = true;
            // 
            // rwbox
            // 
            this.rwbox.AutoSize = true;
            this.rwbox.Location = new System.Drawing.Point(9, 192);
            this.rwbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rwbox.Name = "rwbox";
            this.rwbox.Size = new System.Drawing.Size(145, 24);
            this.rwbox.TabIndex = 8;
            this.rwbox.TabStop = true;
            this.rwbox.Text = "Read and Write";
            this.rwbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rwbox.UseVisualStyleBackColor = true;
            // 
            // UserData
            // 
            this.UserData.CheckOnClick = true;
            this.UserData.FormattingEnabled = true;
            this.UserData.Location = new System.Drawing.Point(9, 29);
            this.UserData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UserData.Name = "UserData";
            this.UserData.Size = new System.Drawing.Size(292, 109);
            this.UserData.Sorted = true;
            this.UserData.TabIndex = 4;
            this.UserData.ThreeDCheckBoxes = true;
            // 
            // namesender
            // 
            this.namesender.Location = new System.Drawing.Point(9, 228);
            this.namesender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.namesender.Name = "namesender";
            this.namesender.Size = new System.Drawing.Size(126, 35);
            this.namesender.TabIndex = 3;
            this.namesender.Text = "Send Names";
            this.namesender.UseVisualStyleBackColor = true;
            this.namesender.Click += new System.EventHandler(this.namesender_Click);
            // 
            // browse2lock
            // 
            this.browse2lock.Location = new System.Drawing.Point(190, 228);
            this.browse2lock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browse2lock.Name = "browse2lock";
            this.browse2lock.Size = new System.Drawing.Size(126, 35);
            this.browse2lock.TabIndex = 0;
            this.browse2lock.Text = "Browse Files";
            this.browse2lock.UseVisualStyleBackColor = true;
            this.browse2lock.Click += new System.EventHandler(this.browse2lock_Click);
            // 
            // ChosenFileView
            // 
            this.ChosenFileView.AutoSize = true;
            this.ChosenFileView.Location = new System.Drawing.Point(482, 251);
            this.ChosenFileView.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ChosenFileView.Name = "ChosenFileView";
            this.ChosenFileView.Size = new System.Drawing.Size(0, 20);
            this.ChosenFileView.TabIndex = 2;
            this.ChosenFileView.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.DriveTab);
            this.tabControl1.Location = new System.Drawing.Point(-2, -2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 483);
            this.tabControl1.TabIndex = 0;
            // 
            // DriveTab
            // 
            this.DriveTab.Controls.Add(this.RefreshDrive);
            this.DriveTab.Controls.Add(this.UploadBox);
            this.DriveTab.Controls.Add(this.DownloadBox);
            this.DriveTab.Location = new System.Drawing.Point(4, 29);
            this.DriveTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DriveTab.Name = "DriveTab";
            this.DriveTab.Size = new System.Drawing.Size(426, 450);
            this.DriveTab.TabIndex = 1;
            this.DriveTab.Text = "Drive";
            this.DriveTab.UseVisualStyleBackColor = true;
            // 
            // RefreshDrive
            // 
            this.RefreshDrive.Location = new System.Drawing.Point(267, 349);
            this.RefreshDrive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RefreshDrive.Name = "RefreshDrive";
            this.RefreshDrive.Size = new System.Drawing.Size(138, 35);
            this.RefreshDrive.TabIndex = 2;
            this.RefreshDrive.Text = "Refresh";
            this.RefreshDrive.UseVisualStyleBackColor = true;
            // 
            // UploadBox
            // 
            this.UploadBox.Controls.Add(this.FileChooser);
            this.UploadBox.Location = new System.Drawing.Point(6, 312);
            this.UploadBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UploadBox.Name = "UploadBox";
            this.UploadBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UploadBox.Size = new System.Drawing.Size(252, 117);
            this.UploadBox.TabIndex = 1;
            this.UploadBox.TabStop = false;
            this.UploadBox.Text = "Upload";
            // 
            // FileChooser
            // 
            this.FileChooser.Location = new System.Drawing.Point(58, 37);
            this.FileChooser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FileChooser.Name = "FileChooser";
            this.FileChooser.Size = new System.Drawing.Size(112, 35);
            this.FileChooser.TabIndex = 0;
            this.FileChooser.Text = "Choose file";
            this.FileChooser.UseVisualStyleBackColor = true;
            this.FileChooser.Click += new System.EventHandler(this.FileChooser_Click);
            // 
            // DownloadBox
            // 
            this.DownloadBox.Controls.Add(this.DownloadButton);
            this.DownloadBox.Controls.Add(this.DownloadableBox);
            this.DownloadBox.Location = new System.Drawing.Point(6, 5);
            this.DownloadBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DownloadBox.Name = "DownloadBox";
            this.DownloadBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DownloadBox.Size = new System.Drawing.Size(411, 298);
            this.DownloadBox.TabIndex = 0;
            this.DownloadBox.TabStop = false;
            this.DownloadBox.Text = "Download";
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(140, 254);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(112, 35);
            this.DownloadButton.TabIndex = 1;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // DownloadableBox
            // 
            this.DownloadableBox.FormattingEnabled = true;
            this.DownloadableBox.Location = new System.Drawing.Point(9, 29);
            this.DownloadableBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DownloadableBox.Name = "DownloadableBox";
            this.DownloadableBox.Size = new System.Drawing.Size(391, 193);
            this.DownloadableBox.TabIndex = 0;
            // 
            // Uploader
            // 
            this.Uploader.FileName = "openFileDialog1";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Choose a download location";
            // 
            // SaveFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 480);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "SaveFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaveFile";
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.DriveTab.ResumeLayout(false);
            this.UploadBox.ResumeLayout(false);
            this.DownloadBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog Locker;
        private System.Windows.Forms.OpenFileDialog Unlocker;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button browse2unlock;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox UserData;
        private System.Windows.Forms.Button namesender;
        private System.Windows.Forms.Button browse2lock;
        private System.Windows.Forms.Label ChosenFileView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.RadioButton rbox;
        private System.Windows.Forms.RadioButton rwbox;
        private System.Windows.Forms.LinkLabel signout;
        private System.Windows.Forms.TextBox ChosenFile;
        private System.Windows.Forms.TabPage DriveTab;
        private System.Windows.Forms.Button RefreshDrive;
        private System.Windows.Forms.GroupBox UploadBox;
        private System.Windows.Forms.Button FileChooser;
        private System.Windows.Forms.GroupBox DownloadBox;
        private System.Windows.Forms.OpenFileDialog Uploader;
        private System.Windows.Forms.CheckedListBox DownloadableBox;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

    }
}