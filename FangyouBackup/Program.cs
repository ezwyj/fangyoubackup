﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using FangyouCoreEntity;
using log4net;

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
            // 设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常 
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobleVariable.RunLog = new StringBuilder();
            int runTime = 0;
            var firstRunBool = int.TryParse(ConfigurationManager.AppSettings["runTime"].ToString(), out runTime);

            GlobleVariable.InfoLogger = log4net.LogManager.GetLogger("loginfo");
            GlobleVariable.ErrorLogger = log4net.LogManager.GetLogger("logerror");

            log4net.Config.XmlConfigurator.Configure();



            int BackupTime = 0;
            if (int.TryParse(ConfigurationManager.AppSettings["BackupTime"].ToString(), out BackupTime))
            {
                GlobleVariable.BackupTime = BackupTime;
            }
            else
            {
                GlobleVariable.BackupTime = 1;
            }

            GlobleVariable.DatabaseAddress = ConfigurationManager.AppSettings["DatabaseAddress"];
            if(string.IsNullOrEmpty(GlobleVariable.DatabaseAddress))
            {
                GlobleVariable.DatabaseAddress = "127.0.0.1";
            }
            if (ConfigurationManager.AppSettings["RunTime"] == null || ConfigurationManager.AppSettings["RunTime"] == "0")
            {
                var setup = new FormSetup();
                setup.ShowDialog();
            }
            else
            {
                GlobleVariable.DatabaseAddress = ConfigurationManager.AppSettings["DatabaseAddress"].ToString();
                GlobleVariable.DatabaseName = AESHelper.AESDecrypt(ConfigurationManager.AppSettings["DatabaseName"].ToString(), "adsfadsfadfadsfasasdfads");
                GlobleVariable.DatabaseUser = AESHelper.AESDecrypt(ConfigurationManager.AppSettings["DatabaseUser"], "adsfadsfadfadsfasasdfads");
                GlobleVariable.DatabasePassword = AESHelper.AESDecrypt(ConfigurationManager.AppSettings["DatabasePassword"], "adsfadsfadfadsfasasdfads");

                int outLocalKeepDay = -1;
                if (int.TryParse(ConfigurationManager.AppSettings["LocalKeepDay"], out outLocalKeepDay))
                {
                    GlobleVariable.LocalKeeyDay = outLocalKeepDay;
                }
                else
                {
                    GlobleVariable.LocalKeeyDay = -1;
                }


               

                FluentScheduler.JobManager.Initialize(new BackupJobFactory());

            }

            //DateTime lasttime = DateTime.Now;
            //DateTime.TryParse(ConfigurationManager.AppSettings["LastBackupTime"].ToString(), out lasttime);
            //GlobleVariable.LastBackupTime = lasttime;

            GlobleVariable.StartTime = DateTime.Now;
            GlobleVariable.FangyouClient = ConfigurationManager.AppSettings["FangyouClient"];
            GlobleVariable.FangyouVer = ConfigurationManager.AppSettings["FangyouVer"];


           


            Application.Run(new FormMain());


        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Exception exp = e.ExceptionObject as Exception;
            GlobleVariable.ErrorLogger.Error(exp.Message + exp.StackTrace);
            Application.Exit();
            Application.ExitThread();
            System.Environment.Exit(0);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            GlobleVariable.ErrorLogger.Error(e.Exception.Message + e.Exception.StackTrace);
            Application.Exit();
            Application.ExitThread();
            System.Environment.Exit(0);
        }
        /// <summary> 
        /// 生成自定义异常消息 
        /// </summary> 
        /// <param name="ex">异常对象</param> 
        /// <param name="backStr">备用异常消息：当ex为null时有效</param> 
        /// <returns>异常字符串文本</returns> 
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
