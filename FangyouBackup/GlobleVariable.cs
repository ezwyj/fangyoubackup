using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FangyouBackup
{
    public static class GlobleVariable
    {
        public static string SqlServerType { get; set; }
        public static DateTime LastBackupTime { get; set; }
        public static DateTime StartTime { get; set; }

        public static string SavePath { get; set; }
        public static string DatabaseName { get; set; }
        public static string DatabaseUser { get; set; }
        public static string DatabasePassword { get; set; }
        public static string FangyouVer { get; set; }
        public static string FangyouClient { get; set; }
        public static int RunTime { get; set; }
        public static int LocalKeeyDay { get; internal set; }

        public static string ConnString { get; internal set; }
    }
}
