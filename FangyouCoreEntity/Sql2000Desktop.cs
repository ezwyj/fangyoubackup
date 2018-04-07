using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FangyouCoreEntity
{
    public class Sql2000Desktop : SqlBase
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
            _conn = string.Format("server={0};database={1};uid={2};pwd={3}",GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseName, GlobleVariable.DatabasePassword);
            string file = GlobleVariable.LocalSavePath + DateTime.Now.ToString("yyyyMMdd")+".bak";
            if (File.Exists(file))
            {
                Directory.Delete(file);
            }
            //备份
            string sql = "BACKUP DATABASE " + GlobleVariable.DatabaseName + " TO DISK = '" + file ;
            System.Data.SqlClient.SqlConnection DbConn = new SqlConnection(_conn);
            DbConn.Open();
            SqlCommand comm = DbConn.CreateCommand();
            comm.CommandText = sql;
            comm.CommandType = System.Data.CommandType.Text;
            comm.ExecuteNonQuery();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GlobleVariable.LastBackupTime = DateTime.Now;
        }
    }
}
