using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class FileSave : Form
    {
        public FileSave()
        {
            InitializeComponent();
        }

        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void browse2lock_Click(object sender, EventArgs e)
        {
            OpenFileDialog Locker = new OpenFileDialog();

            Locker.ShowDialog();
            Locker.InitialDirectory = @"C:\";
            Locker.Title = "Browse Files";
            string file_to_lock = Locker.FileName;

        }

        private void Browse2unlock_Click(object sender, EventArgs e)
        {
            OpenFileDialog Unlocker = new OpenFileDialog();

            Unlocker.ShowDialog();
            Unlocker.InitialDirectory = @"C:\";
            Unlocker.Title = "Browse Files to Unlock";
            string file_to_lock = Unlocker.FileName;
        }

        

        
    }
}
