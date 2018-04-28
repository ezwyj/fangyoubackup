using FangyouCoreEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
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
            timerRefresh.Enabled = false;
            this.Close();
            Application.Exit();
            Application.ExitThread();
            System.Environment.Exit(0);
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
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 100;
            toolStripProgressBar1.Step = 2;
            
            



        
            labelFangyouClient.Text = GlobleVariable.FangyouClient;
            labelFangyouVer.Text = GlobleVariable.FangyouVer;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
            richTextBoxLog.Text = GlobleVariable.RunLog.ToString();
            if (GlobleVariable.Progress)
            {
                toolStripProgressBar1.PerformStep();
                if(toolStripProgressBar1.Value >= toolStripProgressBar1.Maximum)
                {
                    toolStripProgressBar1.Value = 0;
                }
            }
            else
            {
                toolStripProgressBar1.Value = 0;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
