using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FangyouCoreEntity
{
    public static class GlobleVariable
    {
        public static SqlTypeEnum SqlServerType { get; set; }
        public static DateTime? LastBackupTime { get; set; }
        public static DateTime StartTime { get; set; }
        /// <summary>
        /// 本地保留位置
        /// </summary>
        public static string LocalSavePath { get; set; }
        /// <summary>
        /// 异地保留位置
        /// </summary>
        public static string YunSavePath { get; set; }

        public static string DatabaseAddress { get; set; }
        public static string DatabaseName { get; set; }
        public static string DatabaseUser { get; set; }
        public static string DatabasePassword { get; set; }

        public static string FangyouVer { get; set; }
        public static string FangyouClient { get; set; }
        /// <summary>
        /// 定时备份时间
        /// </summary>
        public static int RunTime { get; set; }
        /// <summary>
        /// 本地保存天数
        /// </summary>
        public static int LocalKeeyDay { get; set; }
        /// <summary>
        /// 异地保存天数
        /// </summary>
        public static int YunKeepDay { get; set; }
        


    }
}
