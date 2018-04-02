using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FangyouBackup
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
            _conn = string.Format("server=.;database={0};uid={1};pwd={2}", GlobleVariable.DatabaseName, GlobleVariable.DatabaseName, GlobleVariable.DatabasePassword);
            string file = GlobleVariable.SavePath + DateTime.Now.ToString("yyyyMMdd");
            if (File.Exists(file))
            {
                Directory.Delete(file);
            }
            //还原的数据库MyDataBase
            string sql = "BACKUP DATABASE " + GlobleVariable.DatabaseName + " TO DISK = '" + file + ".bak' ";
            System.Data.SqlClient.SqlConnection DbConn = new SqlConnection(_conn);
            DbConn.Open();
            SqlCommand comm = DbConn.CreateCommand();
            comm.CommandText = sql;
            comm.CommandType = System.Data.CommandType.Text;
            comm.ExecuteNonQuery();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
