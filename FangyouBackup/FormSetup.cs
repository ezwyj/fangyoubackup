using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PetaPoco;

namespace FangyouBackup
{
    public partial class FormSetup : Form
    {
        public FormSetup()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetFangyouInfo (out string fangyouVer,out string fangyouClient)
        {
            var conn = string.Format("server={0};database={1};uid={2};pwd={3};Asynchronous Processing=true", GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseName, GlobleVariable.DatabasePassword);
            var db = new Database(conn, "System.Data.SqlClient");
            fangyouVer = "";
            fangyouClient = "";
            try
            {
                fangyouVer = db.ExecuteScalar<string>("select paramData from sysSet where paramName='mbVersion'");
                fangyouClient = db.ExecuteScalar<string>("select paramData from sysSet where paramName='LicenseUser'");

            }
            catch(Exception e)
            {

            }
            try
            {
                fangyouVer = db.ExecuteScalar<string>("select paramData from sysSet where paramName='Version'");
                fangyouClient = db.ExecuteScalar<string>("select paramData from sysSet where paramName='LicenseUser'");
            }
            catch(Exception e )
            {

            }

             db.Dispose();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(textBoxDbUser.Text == "")
            {
                MessageBox.Show("未输入数据库用户名");
                return;
            }
            if (comboBoxDatabase.SelectedItem.ToString() == "")
            {
                MessageBox.Show("未输入数据库");
                return;
            }

            
            AppSetingHelper.UpdateAppString("DatabaseName", comboBoxDatabase.SelectedText);
            AppSetingHelper.UpdateAppString("DatabaseUser", textBoxDbUser.Text);
            AppSetingHelper.UpdateAppString("DatabsaePwd", textBoxDBPwd.Text);
            AppSetingHelper.UpdateAppString("localKeepDay", numericUpDownLocalKeepDay.Value.ToString());
            AppSetingHelper.UpdateAppString("yunKeepDay", numericUpDownYunKeeyDay.Value.ToString());
            AppSetingHelper.UpdateAppString("yunServer", comboBoxYun.SelectedItem.ToString());
            AppSetingHelper.UpdateAppString("LocalSavePath", labelSavePath.Text);
            AppSetingHelper.UpdateAppString("BackupTime", numericUpDownBackupTime.Value.ToString());
            string outFangyouClient, outFangyouVer;
            GetFangyouInfo(out outFangyouVer, out outFangyouClient);
            AppSetingHelper.UpdateAppString("FangyouVer", outFangyouVer);
            AppSetingHelper.UpdateAppString("FangyouClient", outFangyouClient);
            AppSetingHelper.UpdateAppString("runTime", "");
            AppSetingHelper.UpdateAppString("runTime", "");
            AppSetingHelper.UpdateAppString("runTime", "");
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            ///本地保存
            labelSavePath.Text = GlobleVariable.LocalSavePath;
            if (string.IsNullOrEmpty(GlobleVariable.LocalSavePath) || GlobleVariable.LocalSavePath == "")
            {
                if (!Directory.Exists(Application.StartupPath + "\\Backup"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Backup");
                }
                folderBrowserDialog1.SelectedPath = Path.Combine(Application.StartupPath, "Backup");
                GlobleVariable.LocalSavePath = folderBrowserDialog1.SelectedPath;
                labelSavePath.Text = GlobleVariable.LocalSavePath;
            }
            ///异地

            numericUpDownBackupTime.Value = GlobleVariable.RunTime;
            
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnString =
                String.Format("Data Source={0};Initial Catalog=master;User ID={1};PWD={2}", "192.168.56.2", textBoxDbUser.Text, textBoxDBPwd.Text);

                DataTable DBNameTable = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter("select name from master..sysdatabases", new SqlConnection( ConnString));

                lock (Adapter)
                {
                    Adapter.Fill(DBNameTable);
                }
                comboBoxDatabase.Items.Clear();
                foreach (DataRow row in DBNameTable.Rows)
                {
                    comboBoxDatabase.Items.Add(row["name"]);
                }
                if(comboBoxDatabase.Items.Count>0)
                {
                    comboBoxDatabase.SelectedIndex = comboBoxDatabase.Items.Count - 1;
                }
                if(comboBoxDatabase.Items.Count>0)
                {
                    MessageBox.Show("连接成功，请选择对应数据库");
                }

                DriveInfo[] drives = DriveInfo.GetDrives();
                //检测房友所在磁盘空间
                Adapter = new SqlDataAdapter("select name,fileName from master..sysaltfiles where name like '{0}%' order by name", new SqlConnection(ConnString));

                lock (Adapter)
                {
                    Adapter.Fill(DBNameTable);
                }
            }
            catch
            {
                MessageBox.Show("未能成功找到相关数据库，请确认输入正确的数据库用户名与密码");
            }

           
        }

        private void buttonChangePath_Click(object sender, EventArgs e)
        {
            
            var result = folderBrowserDialog1.ShowDialog();
            folderBrowserDialog1.ShowNewFolderButton = false;
           
            if (result == DialogResult.OK)
            {
                GlobleVariable.LocalSavePath = folderBrowserDialog1.SelectedPath;
                labelSavePath.Text = GlobleVariable.LocalSavePath.Length>30? GlobleVariable.LocalSavePath.Substring(0,30)+"...": GlobleVariable.LocalSavePath;
            }
        }
    }
}
