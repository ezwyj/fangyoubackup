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
                GlobleVariable.InfoLogger.Error(e.Message + e.StackTrace);
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

            AppSetingHelper.UpdateAppString("DatabaseName", AESHelper.AESEncrypt(GlobleVariable.DatabaseName, "adsfadsfadfadsfasasdfads"));
            AppSetingHelper.UpdateAppString("DatabaseUser", AESHelper.AESEncrypt(GlobleVariable.DatabaseUser, "adsfadsfadfadsfasasdfads"));
            AppSetingHelper.UpdateAppString("DatabasePassword", AESHelper.AESEncrypt(GlobleVariable.DatabasePassword, "adsfadsfadfadsfasasdfads") );
            AppSetingHelper.UpdateAppString("localKeepDay","-"+ numericUpDownLocalKeepDay.Value.ToString());
            AppSetingHelper.UpdateAppString("BackupTime",  numericUpDownBackupTime.Value.ToString() );


            AppSetingHelper.UpdateAppString("RunTime", "1");

            GlobleVariable.LocalKeeyDay = -1 * (int) numericUpDownLocalKeepDay.Value;

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

            var result = MessageBox.Show("是否立即执行备份工作？", "提示", MessageBoxButtons.YesNo);
            if(result== DialogResult.Yes)
            {
                var checkSql = new SqlBase();
                GlobleVariable.Progress = true;
                switch (checkSql.GetSqlVersion())
                {
                    case SqlTypeEnum.Sql2000:
                        var backup2000 = new Sql2000();
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

            this.Close();
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {

            if (!(ConfigurationManager.AppSettings["RunTime"]== null || ConfigurationManager.AppSettings["RunTime"] == "0"))
            {

                comboBoxDatabase.Items.Add(AESHelper.AESDecrypt(ConfigurationManager.AppSettings["DatabaseName"].ToString(), "adsfadsfadfadsfasasdfads"));
                textBoxDbUser.Text = AESHelper.AESDecrypt(ConfigurationManager.AppSettings["DatabaseUser"].ToString(), "adsfadsfadfadsfasasdfads");
                textBoxDBPwd.Text = AESHelper.AESDecrypt(ConfigurationManager.AppSettings["DatabasePassword"].ToString(), "adsfadsfadfadsfasasdfads");

            }
           
            
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnString =
                String.Format("Data Source={0};Initial Catalog=master;User ID={1};Password={2}", GlobleVariable.DatabaseAddress, textBoxDbUser.Text, textBoxDBPwd.Text);
                var db = new SqlConnection(ConnString);
                db.Open();
                string sql = "select name from master..sysdatabases";
                var cmd = db.CreateCommand();
                cmd.CommandText = sql;
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
                GlobleVariable.InfoLogger.Error(ex.Message + ex.StackTrace);
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
