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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.browse2unlock = new System.Windows.Forms.Button();
            this.browse2lock = new System.Windows.Forms.Button();
            this.GreetingLabel = new System.Windows.Forms.Label();
            this.Locker = new System.Windows.Forms.OpenFileDialog();
            this.Unlocker = new System.Windows.Forms.OpenFileDialog();
            this.UserData = new System.Windows.Forms.ListBox();
            this.ChosenFileView = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(496, 373);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ChosenFileView);
            this.tabPage1.Controls.Add(this.UserData);
            this.tabPage1.Controls.Add(this.browse2lock);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(488, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lock";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GreetingLabel);
            this.tabPage2.Controls.Add(this.browse2unlock);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(488, 347);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Unlock";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // browse2unlock
            // 
            this.browse2unlock.Location = new System.Drawing.Point(325, 164);
            this.browse2unlock.Name = "browse2unlock";
            this.browse2unlock.Size = new System.Drawing.Size(75, 23);
            this.browse2unlock.TabIndex = 0;
            this.browse2unlock.Text = "Browse Files";
            this.browse2unlock.UseVisualStyleBackColor = true;
            this.browse2unlock.Click += new System.EventHandler(this.browse2unlock_Click);
            // 
            // browse2lock
            // 
            this.browse2lock.Location = new System.Drawing.Point(292, 196);
            this.browse2lock.Name = "browse2lock";
            this.browse2lock.Size = new System.Drawing.Size(75, 23);
            this.browse2lock.TabIndex = 0;
            this.browse2lock.Text = "Browse Files";
            this.browse2lock.UseVisualStyleBackColor = true;
            this.browse2lock.Click += new System.EventHandler(this.browse2lock_Click);
            // 
            // GreetingLabel
            // 
            this.GreetingLabel.AutoSize = true;
            this.GreetingLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GreetingLabel.Font = new System.Drawing.Font("Cooper Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreetingLabel.Location = new System.Drawing.Point(211, 50);
            this.GreetingLabel.Name = "GreetingLabel";
            this.GreetingLabel.Size = new System.Drawing.Size(0, 15);
            this.GreetingLabel.TabIndex = 1;
            // 
            // Unlocker
            // 
            this.Unlocker.FileName = "openFileDialog1";
            this.Unlocker.Filter = "CB files|*.cb|All files|*.*";
            // 
            // UserData
            // 
            this.UserData.FormattingEnabled = true;
            this.UserData.Location = new System.Drawing.Point(22, 72);
            this.UserData.Name = "UserData";
            this.UserData.Size = new System.Drawing.Size(202, 147);
            this.UserData.TabIndex = 1;
            this.UserData.SelectedIndexChanged += new System.EventHandler(this.UserData_SelectedIndexChanged);
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
            // SaveFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 370);
            this.Controls.Add(this.tabControl1);
            this.Name = "SaveFile";
            this.Text = "SaveFile";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button browse2lock;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button browse2unlock;
        private System.Windows.Forms.Label GreetingLabel;
        private System.Windows.Forms.OpenFileDialog Locker;
        private System.Windows.Forms.OpenFileDialog Unlocker;
        private System.Windows.Forms.ListBox UserData;
        private System.Windows.Forms.Label ChosenFileView;

    }
}