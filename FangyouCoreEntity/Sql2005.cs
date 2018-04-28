using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FangyouCoreEntity
{
    public class Sql2005 : SqlBase
    {
        public Sql2005()
        {
            
        }
        public override void Backup()
        {
            if (GetFileSize() > GetHardDiskFreeSpace())
            {
                GlobleVariable.RunLog.Append("备份文件大于磁盘空间,无法备份！");
                return;
            }

            _conn = string.Format("server={0};database={1};uid={2};pwd={3};Asynchronous Processing=true",GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);
            string file = System.Environment.CurrentDirectory + "\\Backup\\"  + DateTime.Now.ToString("yyyyMMdd")+ ".bak";
            if(CheckFile())
            {
                GlobleVariable.InfoLogger.Info("删除同名文件");
                File.Delete(file);
            }
                //还原的数据库MyDataBase
                string sql = "BACKUP DATABASE " + GlobleVariable.DatabaseName + " TO DISK = '" + file + "'";
                System.Data.SqlClient.SqlConnection DbConn = new SqlConnection(_conn);
                DbConn.Open();
                SqlCommand comm = DbConn.CreateCommand();
                comm.CommandText = sql;
                comm.CommandType = System.Data.CommandType.Text;
                comm.BeginExecuteNonQuery(BackupEnd, comm);
                GlobleVariable.RunLog.Append(DateTime.Now.ToString() + "开始异步备份\n\r");
          
            



            return;
        }
        private void BackupEnd(IAsyncResult result)
        {

            using (SqlCommand cmd = result.AsyncState as SqlCommand)
            {
                cmd.Connection.Close();
            }
            if (CheckFile())
            {
                GlobleVariable.RunLog.Append("备份完成");

            }
            GlobleVariable.InfoLogger.Info("备份完成");
            GlobleVariable.Progress = false;
            GlobleVariable.LastBackupTime = DateTime.Now;
            GlobleVariable.RunLog.Append(DateTime.Now.ToString() + "异步备份完成\n\r");
            string file = System.Environment.CurrentDirectory + "\\Backup\\" + DateTime.Now.AddDays(GlobleVariable.LocalKeeyDay * -1).ToString("yyyyMMdd") + ".bak";
            File.Delete(file);
        }
    }
}
