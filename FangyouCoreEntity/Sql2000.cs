using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FangyouCoreEntity
{
    public class Sql2000 : SqlBase
    {

        public override void Backup()
        {
            



            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync("Tank");
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            _conn = string.Format("server={0};database={1};uid={2};pwd={3}",GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);
            string file = System.Environment.CurrentDirectory+ "\\Backup\\"+ DateTime.Now.ToString("yyyyMMdd")+".bak";
            
           

            try
            {
                if (GetFileSize() > GetHardDiskFreeSpace())
                {
                    GlobleVariable.RunLog.Append("备份文件大于磁盘空间,无法备份！");
                    return;
                }
                if (CheckFile())
                {
                    File.Delete(file);
                }

                string sql = "BACKUP DATABASE " + GlobleVariable.DatabaseName + " TO DISK = '" + file+"'";
                System.Data.SqlClient.SqlConnection DbConn = new SqlConnection(_conn);
                DbConn.Open();
                SqlCommand comm = DbConn.CreateCommand();
                comm.CommandText = sql;
                comm.CommandTimeout = 3600;
                comm.CommandType = System.Data.CommandType.Text;
                comm.ExecuteNonQuery();
                GlobleVariable.RunLog.Append(DateTime.Now.ToString() +"开始备份");
                GlobleVariable.Progress = true;
                GlobleVariable.InfoLogger.Info("开始2000备份");
            }
            catch(Exception ee )
            {
                GlobleVariable.Progress = false;
                GlobleVariable.InfoLogger.Error(ee.Message);
                throw ee;
            }
           
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GlobleVariable.LastBackupTime = DateTime.Now;
            GlobleVariable.RunLog.Append("调用完成");
            if (CheckFile())
            {
                GlobleVariable.RunLog.Append("备份完成");
                
            }
            GlobleVariable.InfoLogger.Info("备份完成");
            GlobleVariable.Progress = false;

             
            string file = System.Environment.CurrentDirectory + "\\Backup\\" + DateTime.Now.AddDays(GlobleVariable.LocalKeeyDay * -1).ToString("yyyyMMdd") + ".bak";
            File.Delete(file);

            Report();
        }
    }
}
