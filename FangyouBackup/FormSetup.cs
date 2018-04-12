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
using FangyouCoreEntity;
using System.Configuration;

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
            var conn = string.Format("server={0};database={1};uid={2};pwd={3}", GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);
            var db = new Database(conn, "System.Data.SqlClient");
            fangyouVer = "";
            fangyouClient = "";
            try
            {
                fangyouVer = db.ExecuteScalar<string>("select paramData from sysSet where paramName='Version'");
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
            if(comboBoxDatabase.SelectedItem==null)
            {
                MessageBox.Show("未输入数据库名");
                return;
            }
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
            AppSetingHelper.UpdateAppString("DatabaseAddress", GlobleVariable.DatabaseAddress);
            AppSetingHelper.UpdateAppString("DatabaseName", GlobleVariable.DatabaseName);
            AppSetingHelper.UpdateAppString("DatabaseUser", GlobleVariable.DatabaseUser);
            AppSetingHelper.UpdateAppString("DatabasePassword", GlobleVariable.DatabasePassword);
            AppSetingHelper.UpdateAppString("localKeepDay", numericUpDownLocalKeepDay.Value.ToString());
            AppSetingHelper.UpdateAppString("LocalSavePath", labelSavePath.Text);
            AppSetingHelper.UpdateAppString("BackupTime", numericUpDownBackupTime.Value.ToString());


            AppSetingHelper.UpdateAppString("RunTime", "1");


            string outFangyouClient, outFangyouVer;
            GetFangyouInfo(out outFangyouVer, out outFangyouClient);
            GlobleVariable.FangyouClient = outFangyouClient;
            GlobleVariable.FangyouVer = outFangyouVer;
            AppSetingHelper.UpdateAppString("FangyouVer", outFangyouVer);
            AppSetingHelper.UpdateAppString("FangyouClient", outFangyouClient);
            var sqlBase = new SqlBase();

            GlobleVariable.SqlServerType = sqlBase.GetSqlVersion();
            AppSetingHelper.UpdateAppString("SqlType", GlobleVariable.SqlServerType.ToString());
            AppSetingHelper.UpdateAppString("LocalKeeyDay", GlobleVariable.LocalKeeyDay.ToString());


            this.Close();
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            ///本地保存
           
            if (string.IsNullOrEmpty(GlobleVariable.LocalSavePath) || GlobleVariable.LocalSavePath == "")
            {
               // 初始化 
                if (!Directory.Exists(Application.StartupPath + "\\Backup"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Backup");
                }
                folderBrowserDialog1.SelectedPath = Path.Combine(Application.StartupPath, "Backup");
                GlobleVariable.LocalSavePath = folderBrowserDialog1.SelectedPath;
                labelSavePath.Text = GlobleVariable.LocalSavePath;
            }
          
            ///异地

           
            
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnString =
                String.Format("Data Source={0};Initial Catalog=master;User ID={1};PWD={2}", GlobleVariable.DatabaseAddress, textBoxDbUser.Text, textBoxDBPwd.Text);

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
                    GlobleVariable.DatabaseAddress = "192.168.56.2";
                    GlobleVariable.DatabaseUser = textBoxDbUser.Text;
                    GlobleVariable.DatabasePassword = textBoxDBPwd.Text;
                }

                //DriveInfo[] drives = DriveInfo.GetDrives();
                ////检测房友所在磁盘空间
                //Adapter = new SqlDataAdapter("select name,fileName from master..sysaltfiles where name like '{0}%' order by name", new SqlConnection(ConnString));

                //lock (Adapter)
                //{
                //    Adapter.Fill(DBNameTable);
                //}
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

        private void comboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobleVariable.DatabaseName = comboBoxDatabase.SelectedItem.ToString();


        }

        private void numericUpDownLocalKeepDay_ValueChanged(object sender, EventArgs e)
        {
            GlobleVariable.LocalKeeyDay = (int) numericUpDownLocalKeepDay.Value;
        }
    }
}
