using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FangyouBackup
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMin_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = true;

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)//当用户点击窗体右上角X按钮或(Alt + F4)时 发生          
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                //this.myIcon.Icon = this.Icon;
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void buttonSetup_Click(object sender, EventArgs e)
        {
            FormSetup setup = new FormSetup();
            setup.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (GlobleVariable.RunTime == 0)
            {
                var setup = new FormSetup();
                setup.ShowDialog();
            }
        }
    }
}
