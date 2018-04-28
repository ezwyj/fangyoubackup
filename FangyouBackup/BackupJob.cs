using FangyouCoreEntity;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FangyouBackup
{
    public class BackupJob : IJob
    {
        public void Execute()
        {
            var checkSql =new SqlBase();

            switch (checkSql.GetSqlVersion())
            {
                case SqlTypeEnum.Sql2000:
                    var backup2000 = new Sql2000();
                    backup2000.Backup();
                    break;
                case SqlTypeEnum.Sql2005:
                    var backup2005 = new Sql2005();
                    backup2005.Backup();
                    break;
                case SqlTypeEnum.Sql2008:
                    var backup2008 = new Sql2008();
                    backup2008.Backup();
                    break;
            }
        }
    }

    internal class BackupJobFactory : Registry
    {
        public BackupJobFactory()
        {


            // 在一个指定时间执行计划任务（最常用。这里是在每天的下午 1:10 分执行）
            var min = int.Parse(ConfigurationManager.AppSettings["BackupTimeMin"].ToString());
            Schedule<BackupJob>().ToRunEvery(1).Days().At(GlobleVariable.BackupTime, min);

          
        }
    }

}
