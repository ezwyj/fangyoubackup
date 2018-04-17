using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
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
            var db = new System.Data.SqlClient.SqlConnection(conn);
            fangyouVer = "";
            fangyouClient = "";
            db.Open();
            var cmd = db.CreateCommand();

            try
            {

                cmd.CommandText = "select paramData from sysSet where paramName = 'Version'";
                fangyouVer = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "select paramData from sysSet where paramName='LicenseUser'";
                fangyouClient = cmd.ExecuteScalar().ToString();

            }
            catch (Exception e)
            {
                GlobleVariable.Logger.Error(e.Message + e.StackTrace);
                try
                {

                    cmd.CommandText = "select paramData from sysSet where paramName = 'Version'";
                    fangyouVer = cmd.ExecuteScalar().ToString();
                    cmd.CommandText = "select paramData from sysSet where paramName='LicenseUser'";
                    fangyouClient = cmd.ExecuteScalar().ToString();
                }
                catch (Exception ee)
                {

                }
            }
            db.Close();

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

            AppSetingHelper.UpdateAppString("DatabaseName", GlobleVariable.DatabaseName);
            AppSetingHelper.UpdateAppString("DatabaseUser", GlobleVariable.DatabaseUser);
            AppSetingHelper.UpdateAppString("DatabasePassword", GlobleVariable.DatabasePassword);
            AppSetingHelper.UpdateAppString("localKeepDay", numericUpDownLocalKeepDay.Value.ToString());
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
            
          
            
           
            
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnString =
                String.Format("Data Source={0};Initial Catalog=master;User ID={1};Password={2}", GlobleVariable.DatabaseAddress, textBoxDbUser.Text, textBoxDBPwd.Text);
                var db = new SqlConnection(ConnString);
                 string sql = "select name from master..sysdatabases";
                var cmd = db.CreateCommand();
                db.Open();
                var reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                
                
                comboBoxDatabase.Items.Clear();
                foreach (DataRow row in dt.Rows)
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
            catch(Exception ex )
            {
                MessageBox.Show("未能成功找到相关数据库，请确认输入正确的数据库用户名与密码"+ex.Message);
                GlobleVariable.Logger.Error(ex.Message + ex.StackTrace);
            }

           
        }

        private void buttonChangePath_Click(object sender, EventArgs e)
        {
            
            var result = folderBrowserDialog1.ShowDialog();
            folderBrowserDialog1.ShowNewFolderButton = false;
           
            if (result == DialogResult.OK)
            {
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
