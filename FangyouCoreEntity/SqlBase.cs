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

            System.Data.SqlClient.SqlConnection db= new SqlConnection(conn);
            db.Open();
            SqlCommand command = db.CreateCommand();
            command.CommandText= "select @@version";
            var result = command.ExecuteScalar();
            db.Close();
            db.Dispose();
            if (result.ToString().IndexOf("2000")>1)
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

        public void Report ()
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            var BackupReportEntity = new BackupReport();
            BackupReportEntity.BackupTime = DateTime.Now;
            BackupReportEntity.BackupState =true;
            BackupReportEntity.FangyouClient = GlobleVariable.FangyouClient;
            BackupReportEntity.FangyouVer = GlobleVariable.FangyouVer;


            WebClient client = new WebClient();
            // client.UseDefaultCredentials = true;
            string URI = System.Configuration.ConfigurationManager.AppSettings["url"] + "?report=" + JsonConvert.SerializeObject(BackupReportEntity);
            client.DownloadData( URI);

        }

        protected bool CheckFile()
        {
            return File.Exists(System.Environment.CurrentDirectory + "\\Backup\\" + DateTime.Now.ToString("yyyyMMdd") + ".bak");
        }

    }
}
