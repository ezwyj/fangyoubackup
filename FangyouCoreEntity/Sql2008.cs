using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace FangyouCoreEntity
{
    public class Sql2008 : SqlBase
    {
        private FileLog log; 
        public Sql2008(string logFile)
        {
            log = new FileLog(logFile);
        }
        public override void Backup()
        {
            _conn = string.Format("server={0};database={1};uid={2};pwd={3};Asynchronous Processing=true", GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);
            string file = GlobleVariable.LocalSavePath+"\\" + DateTime.Now.ToString("yyyyMMdd") + ".bak";
            if (!File.Exists(file))
            {
                //还原的数据库MyDataBase
                string sql = "BACKUP DATABASE " + GlobleVariable.DatabaseName + " TO DISK = '" + file + "'";
                System.Data.SqlClient.SqlConnection DbConn = new SqlConnection(_conn);
                DbConn.Open();
                SqlCommand comm = DbConn.CreateCommand();
                comm.CommandText = sql;
                comm.CommandType = System.Data.CommandType.Text;

                comm.BeginExecuteNonQuery(BackupEnd, comm);
                log.log("开始备份");
            }
           


            return;
        }
        private void BackupEnd(IAsyncResult result)
        {

            using (SqlCommand cmd = result.AsyncState as SqlCommand)
            {
                cmd.Connection.Close();
            }
            GlobleVariable.LastBackupTime = DateTime.Now;

            log.log("备份完成");
        }
    }
}
