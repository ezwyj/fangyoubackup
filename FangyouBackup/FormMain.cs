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

            if (ConfigurationManager.AppSettings["RunTime"] == null || ConfigurationManager.AppSettings["RunTime"]=="")
            {
                var setup = new FormSetup();
                setup.ShowDialog();
            }
            else
            {
                GlobleVariable.DatabaseAddress = ConfigurationManager.AppSettings["DatabaseAddress"].ToString();
                GlobleVariable.DatabaseName = ConfigurationManager.AppSettings["DatabaseName"].ToString();
                GlobleVariable.DatabaseUser = ConfigurationManager.AppSettings["DatabaseUser"].ToString();
                GlobleVariable.DatabasePassword = ConfigurationManager.AppSettings["DatabasePassword"].ToString();
                int outLocalKeepDay = 1;
                if(int.TryParse(ConfigurationManager.AppSettings["LocalKeepDay"], out outLocalKeepDay))
                {
                    GlobleVariable.LocalKeeyDay = outLocalKeepDay;
                }
                else
                {
                    GlobleVariable.LocalKeeyDay = 1;
                }
                
                DateTime outLastBackupTime;
                if (DateTime.TryParse(ConfigurationManager.AppSettings["LastBackupTime"].ToString(),out outLastBackupTime)){
                    GlobleVariable.LastBackupTime = outLastBackupTime;
                }
                
                GlobleVariable.LocalSavePath= ConfigurationManager.AppSettings["LocalSavePath"].ToString();
                GlobleVariable.RunTime=int.Parse( ConfigurationManager.AppSettings["RunTime"].ToString());
            }
            



            string cronExpression = "0 0 "+GlobleVariable.RunTime.ToString()+" * * ? ";　　//=>这是指每天的9点和16点执行任务
            QuartzManager.ExecuteByCron<BackupJob>(cronExpression);　　//=>这是调用Cron计划方法

            labelFangyouClient.Text = GlobleVariable.FangyouClient;
            labelFangyouVer.Text = GlobleVariable.FangyouVer;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            labelLastBackupTime.Text = "最后备份时间:" + GlobleVariable.LastBackupTime.ToString();
            labelLocation.Text = "异地备份位置：" + GlobleVariable.YunSavePath;
            richTextBoxLog.Text = "";
            string fileName = Application.ExecutablePath + "\\logs\\app_log.txt";
            if (File.Exists(fileName))
            {
                richTextBoxLog.Text = File.ReadAllText(fileName);
            }
            var checkSql = new SqlBase();

            switch (checkSql.GetSqlVersion())
            {
                case SqlTypeEnum.Sql2000:
                    var backup2000 = new Sql2000Desktop();
                    backup2000.Backup();
                    break;
                case SqlTypeEnum.Sql2005:
                    var backup2005 = new Sql2005();
                    backup2005.Backup();
                    break;
                case SqlTypeEnum.Sql2008:
                    var backup2008 = new Sql2008();
                    backup2008.Backup();
                    break;
            }
        }
    }
}
