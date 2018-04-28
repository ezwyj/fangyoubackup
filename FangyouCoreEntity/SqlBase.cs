using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;

namespace FangyouCoreEntity
{
    public class SqlBase
    {
        protected string _conn;

        public virtual void Backup() { }
        public virtual void Restore() { }

        public SqlTypeEnum GetSqlVersion()
        {

            var conn = string.Format("server={0};database={1};uid={2};pwd={3}", GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);

            System.Data.SqlClient.SqlConnection db = new SqlConnection(conn);
            db.Open();
            SqlCommand command = db.CreateCommand();
            command.CommandText = "select @@version";
            var result = command.ExecuteScalar();
            db.Close();
            db.Dispose();
            if (result.ToString().IndexOf("2000") > 1)
            {
                return SqlTypeEnum.Sql2000;
            }
            if (result.ToString().IndexOf("2005") > 1)
            {
                return SqlTypeEnum.Sql2005;
            }
            if (result.ToString().IndexOf("2008") > 1)
            {
                return SqlTypeEnum.Sql2008;
            }
            return SqlTypeEnum.Sql2000;
        }

        public void Report()
        {
            try
            {
                System.Net.WebClient WebClientObj = new System.Net.WebClient();

                var BackupReportEntity = new BackupReport();
                BackupReportEntity.BackupTime = DateTime.Now;
                BackupReportEntity.BackupState = true;
                BackupReportEntity.FangyouClient = GlobleVariable.FangyouClient;
                BackupReportEntity.FangyouVer = GlobleVariable.FangyouVer;


                WebClient client = new WebClient();
                // client.UseDefaultCredentials = true;
                string URI = System.Configuration.ConfigurationManager.AppSettings["url"] + "?report=" + JsonConvert.SerializeObject(BackupReportEntity);
                client.DownloadData(URI);
                GlobleVariable.InfoLogger.Info("上报服务器成功");
            }
            catch(Exception e)
            {
                GlobleVariable.ErrorLogger.Error("上传出错");
            }
        }


        protected bool CheckFile()
        {
            GlobleVariable.InfoLogger.Info("检查今日备份文件");
            return File.Exists(System.Environment.CurrentDirectory + "\\Backup\\" + DateTime.Now.ToString("yyyyMMdd") + ".bak");
        }
        protected void DeleteOldFile()
        {
            GlobleVariable.InfoLogger.Info("删除历史文件");
            string file = System.Environment.CurrentDirectory + "\\Backup\\" + DateTime.Now.AddDays(GlobleVariable.LocalKeeyDay).ToString("yyyyMMdd") + ".bak";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public static long GetHardDiskFreeSpace()
        {
            long freeSpace = new long();

            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name.Substring(0, 1).ToUpper() == Path.GetPathRoot(System.Environment.CurrentDirectory).Substring(0, 1).ToUpper())
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
            }
            GlobleVariable.InfoLogger.Info(string.Format("空间大小:{0}G",freeSpace.ToString()));
            return freeSpace;
        }

        public static long GetFileSize()
        {

            var conn = string.Format("server={0};database={1};uid={2};pwd={3}", GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);

            System.Data.SqlClient.SqlConnection db = new SqlConnection(conn);
            db.Open();
            SqlCommand command = db.CreateCommand();
            command.CommandText = "select filename from sysfiles where name like '%_Data%' ";
            var fileName = command.ExecuteScalar();
            Console.WriteLine(fileName);
            FileInfo fileInfo = new FileInfo(fileName.ToString());
           
            db.Close();
            db.Dispose();
            long fileSize = fileInfo.Length / (1024 * 1024 * 1024);
            GlobleVariable.InfoLogger.Info(string.Format("空间文件大小:{0}G", fileSize.ToString()));
            return fileSize;
        }

    }
}
