using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace FangyouBackup
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            int runTime = 0;
            var firstRunBool = int.TryParse(ConfigurationManager.AppSettings["runTime"].ToString(), out runTime);
            

            GlobleVariable.RunTime = runTime;
            

            GlobleVariable.DatabasePassword = ConfigurationManager.AppSettings["DatabsaePwd"];
            GlobleVariable.DatabaseUser = ConfigurationManager.AppSettings["DatabaseUser"];

            DateTime lasttime = DateTime.Now;
            DateTime.TryParse(ConfigurationManager.AppSettings["LastBackupTime"].ToString(), out lasttime);
            GlobleVariable.LastBackupTime = lasttime;

            GlobleVariable.StartTime = DateTime.Now;
            GlobleVariable.FangyouClient = ConfigurationManager.AppSettings[""];
            GlobleVariable.FangyouVer = ConfigurationManager.AppSettings[""];

            Application.Run(new FormMain());
        }
    }
}
