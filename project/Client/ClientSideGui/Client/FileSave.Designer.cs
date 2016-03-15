namespace Client
{
    partial class FileSave
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
            this.Locker = new System.Windows.Forms.OpenFileDialog();
            this.browse2lock = new System.Windows.Forms.Button();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.browse2unlock = new System.Windows.Forms.Button();
            this.Unlocker = new System.Windows.Forms.OpenFileDialog();
            this.TabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Locker
            // 
            this.Locker.FileName = "openFileDialog1";
            // 
            // browse2lock
            // 
            this.browse2lock.Location = new System.Drawing.Point(316, 277);
            this.browse2lock.Name = "browse2lock";
            this.browse2lock.Size = new System.Drawing.Size(75, 23);
            this.browse2lock.TabIndex = 0;
            this.browse2lock.Text = "Browse Files";
            this.browse2lock.UseVisualStyleBackColor = true;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.tabPage1);
            this.TabControl1.Controls.Add(this.tabPage2);
            this.TabControl1.Location = new System.Drawing.Point(0, -1);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(767, 396);
            this.TabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.browse2lock);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(759, 370);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lock";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.browse2unlock);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(759, 370);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Unlock";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // browse2unlock
            // 
            this.browse2unlock.Location = new System.Drawing.Point(321, 272);
            this.browse2unlock.Name = "browse2unlock";
            this.browse2unlock.Size = new System.Drawing.Size(75, 23);
            this.browse2unlock.TabIndex = 0;
            this.browse2unlock.Text = "Browse Files";
            this.browse2unlock.UseVisualStyleBackColor = true;
            this.browse2unlock.Click += new System.EventHandler(this.browse2unlock_Click_1);
            // 
            // Unlocker
            // 
            this.Unlocker.FileName = "openFileDialog2";
            // 
            // FileSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 392);
            this.Controls.Add(this.TabControl1);
            this.Name = "FileSave";
            this.Text = "FileSave";
            this.TabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog Locker;
        private System.Windows.Forms.Button browse2lock;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button browse2unlock;
        private System.Windows.Forms.OpenFileDialog Unlocker;

    }
}